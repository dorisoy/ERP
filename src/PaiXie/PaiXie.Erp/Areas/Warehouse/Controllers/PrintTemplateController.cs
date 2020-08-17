using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace PaiXie.Erp.Areas.Warehouse
{
    public class PrintTemplateController : BaseController
    {
		#region Index

		public ActionResult Index() {
			return View();
		}

		#endregion

		#region 打印模版列表

		/// <summary>
		/// 快递列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string whereSql = string.Format("wpt.WarehouseCode='{0}'", warehouseCode);

			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wpt.ID DESC";
			data.From = "warehousePrintTemplate wpt";
			data.Select = @"wpt.ID, wpt.WarehouseCode, wpt.Name, wpt.TypeID,'' as TypeName, wpt.IsEnable, wpt.IsDefault";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<PrintTemplateList> list = BaseService<PrintTemplateList>.GetQueryManyForPage(data, out total);
			for (int i = 0; i < list.Count(); i++) {
				list[i].TypeName = PrintTemplateManager.GetTypeName(list[i].TypeID);
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 编辑、添加模版

		public ActionResult Edit(int id = 0) {
			WarehousePrintTemplate objWarehousePrintTemplate = new WarehousePrintTemplate();
			if (id > 0) {
				objWarehousePrintTemplate = WarehousePrintTemplateService.GetQuerySingleByID(id);
			}
			ViewBag.WarehousePrintTemplate = objWarehousePrintTemplate;
			return View();
		}

		#endregion

		#region 检查当前仓库是否已经存在同类型的模版名称

		/// <summary>
		/// 检查当前仓库是否已经存在同类型的模版名称
		/// </summary>
		/// <param name="id">模版ID</param>
		/// <param name="typeID">模版类型 枚举值</param>
		/// <param name="name">模版名称</param>
		/// <returns></returns>
		public ActionResult CheckName(int id, int typeID, string name) {
			BaseResult resultInfo = new BaseResult();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			if (id > 0) {
				if (WarehousePrintTemplateService.GetPrintTemplateID(warehouseCode, name, typeID, id) > 0) {
					resultInfo.result = -1;
				}

			}
			else {
				if (WarehousePrintTemplateService.GetPrintTemplateID(warehouseCode, name, typeID) > 0) {
					resultInfo.result = -1;
				}
			}
			return JsonDate(resultInfo);
		}

		#endregion

		#region 保存模版

		[HttpPost]
		public ActionResult Save(WarehousePrintTemplate warehousePrintTemplate) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/PrintTemplateController/Save";
			string buttonName = "保存模版";
			string target = "打印模版";
			BaseResult resultInfo = PrintTemplateManager.Save(userCode, warehouseCode, position, target, buttonName, warehousePrintTemplate);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 启用、禁用模版

		/// <summary>
		/// 启用、禁用模版
		/// </summary>
		/// <param name="id">模版ID</param>
		/// <param name="isEnable"> 0：禁用 1：启用</param>
		/// <returns></returns>
		public ActionResult SetIsEnable(int id, int isEnable) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/PrintTemplateController/SetIsEnable";
			string buttonName = isEnable == 0 ? "禁用" : "启用";
			string target = "打印模版";
			BaseResult resultInfo = PrintTemplateManager.SetIsEnable(userCode, warehouseCode, position, target, buttonName, id, isEnable);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 设置、取消 默认模版

		/// <summary>
		/// 设置、取消 默认模版
		/// </summary>
		/// <param name="typeID">模版类型 枚举值</param>
		/// <param name="id">模版ID</param>
		/// <param name="isDefault"> 0：取消默认 1：设置默认</param>
		/// <returns></returns>
		public ActionResult SetIsDefault(int typeID, int id, int isDefault) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/PrintTemplateController/SetIsEnable";
			string buttonName = isDefault == 0 ? "取消默认" : "设置默认";
			string target = "打印模版";
			BaseResult resultInfo = PrintTemplateManager.SetIsDefault(userCode, warehouseCode, position, target, buttonName, typeID, id, isDefault);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 设置模版

		/// <summary>
		/// 设置模版
		/// </summary>
		/// <param name="id">打印模版ID</param>
		/// <returns></returns>
		public ActionResult SetPrintTemplate(int id) {
			WarehousePrintTemplate printTemplate = WarehousePrintTemplateService.GetQuerySingleByID(id);
			if (printTemplate == null) printTemplate = new WarehousePrintTemplate();
			if (ZConvert.ToString(printTemplate.TemplateContent) == string.Empty) {
				string printProField = string.Empty;
				string printProFieldWidth = string.Empty;
				decimal width = 0;
				decimal height = 0;
				decimal secondPageOffset = 0;
				string templateXmlUrl = "../../Xml/PrintTemplate/PrintTemplate.xml";
				printTemplate.TemplateContent = PrintTemplateManager.GetPrintTemplate(printTemplate.TypeID, templateXmlUrl, out width, out height, out secondPageOffset, out printProField, out printProFieldWidth);
				//默认打印商品明细
				printTemplate.IsPrintPro = 1;
				printTemplate.Width = width;
				printTemplate.Height = height;
				printTemplate.SecondPageOffset = secondPageOffset;
			}
			string typeName = PrintTemplateManager.GetTypeName(printTemplate.TypeID);
			//读取模板，前面3行不允许改动
			string templateContent = "LODOP.PRINT_INITA(\"0mm\",\"0mm\",\"" + printTemplate.Width + "mm\",\"" + printTemplate.Height + "mm\",\"" + typeName + "模板设计_" + NewKey.datetime() + "\");" + (char)13;
			templateContent += "LODOP.SET_PRINT_PAGESIZE(1,\"" + printTemplate.Width + "mm\",\"" + printTemplate.Height + "mm\",\"\");" + (char)13;
			templateContent += "LODOP.SET_PRINT_STYLE(\"FontSize\",12);";//注意单位是pt
			templateContent += printTemplate.TemplateContent;
			ViewBag.TypeName = typeName;
			ViewBag.WarehousePrintTemplate = printTemplate;
			ViewBag.TemplateContent = templateContent;
			return View();
		}

		/// <summary>
		/// 保存打印模版
		/// </summary>
		/// <param name="id">打印模版ID</param>
		/// <param name="width">宽度</param>
		/// <param name="height">高度</param>
		/// <param name="templateContent">模版内容</param>
		/// <param name="secondPageOffset">次页打印偏移</param>
		/// <param name="printerName">默认打印机名称</param>
		/// <param name="isPrintPro">是否打印商品明细 0否 1是</param>
		/// <returns></returns>
		public ActionResult SavePrintTemplate(int id, decimal width, decimal height, string templateContent, decimal secondPageOffset, string printerName, int isPrintPro) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			templateContent = HttpUtility.UrlDecode(templateContent);
			try {
				string[] arrTemplateContent = templateContent.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
				List<string> list = new List<string>(arrTemplateContent);
				list.RemoveRange(0, 3);
				templateContent = string.Join(";", list.ToArray()).Replace("<script>window.alert = null;window.confirm = null;window.open = null;window.showModalDialog = null;</script>", "");
			}
			catch { }
			BaseResult resultInfo = PrintTemplateManager.SavePrintTemplate(userCode, warehouseCode, id, width, height, templateContent, secondPageOffset, printerName, isPrintPro);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 恢复默认设置
		/// </summary>
		/// <param name="id">打印模版ID</param>
		/// <returns></returns>
		public ActionResult ResetDefault(int id) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			decimal width = 0;
			decimal height = 0;
			decimal secondPageOffset = 0;
			string templateContent = string.Empty;
			BaseResult resultInfo = PrintTemplateManager.SavePrintTemplate(userCode, warehouseCode, id, width, height, templateContent, secondPageOffset);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 设置明细项
		/// </summary>
		/// <param name="id">打印模版ID</param>
		/// <returns></returns>
		public ActionResult SetPrintPro(int id) {
			WarehousePrintTemplate printTemplate = WarehousePrintTemplateService.GetQuerySingleByID(id);
			if (printTemplate == null) printTemplate = new WarehousePrintTemplate();
			if (ZConvert.ToString(printTemplate.PrintProField) == string.Empty) {
				string templateXmlUrl = "../../Xml/PrintTemplate/PrintTemplate.xml";
				string printProField = string.Empty;
				string printProFieldWidth = string.Empty;
				decimal width = 0;
				decimal height = 0;
				decimal secondPageOffset = 0;
				PrintTemplateManager.GetPrintTemplate(printTemplate.TypeID, templateXmlUrl, out width, out height, out secondPageOffset, out printProField, out printProFieldWidth);
				printTemplate.PrintProField = printProField;
				printTemplate.PrintProFieldWidth = printProFieldWidth;
			}
			ViewBag.WarehousePrintTemplate = printTemplate;
			return View();
		}

		/// <summary>
		/// 保存明细项
		/// </summary>
		/// <param name="id">打印模版ID</param>
		/// <param name="skuFields">明细字段</param>
		/// <param name="printProFieldWidth">明细字段宽度占比</param>
		/// <returns></returns>
		public ActionResult SavePrintPro(int id, string skuFields, string printProFieldWidth) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = PrintTemplateManager.SavePrintPro(userCode, warehouseCode, id, skuFields, printProFieldWidth);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 打印模版下拉列表

		/// <summary>
		/// 打印模版下拉列表
		/// </summary>
		/// <param name="printTemplateType">枚举值 0发货单 1拣货单</param>
		/// <returns></returns>
		public ActionResult JsonTree(int printTemplateType) {
			List<CListItem> treeList = new List<CListItem>();
			Object[] objects = new Object[2];
			objects[0] = FormsAuth.GetWarehouseCode();
			objects[1] = printTemplateType;
			string sqlStr = @"SELECT ID AS Value ,Name AS Text,IsDefault as IsChecked FROM warehousePrintTemplate WHERE WarehouseCode=@0 AND TypeID=@1 AND IsEnable=" + (int)IsEnable.是 + " ORDER BY ID DESC";
			List<CListItem> objlist = BaseService<CListItem>.GetQueryMany(sqlStr, null, objects);
			if (objlist.Count > 0) {
				//只有一个模版默认选中
				if (objlist.Count == 1) objlist[0].IsChecked = 1;
				treeList.AddRange(objlist);
			}
			else {
				//未添加模版，从xml读取默认模版
				CListItem cListItem = new CListItem();
				string defaultTempName = "默认" + (printTemplateType == (int)PrintTemplateType.发货单 ? PrintTemplateType.发货单.ToString() : PrintTemplateType.拣货单.ToString());
				cListItem.Text = defaultTempName;
				cListItem.Value = "0";
				cListItem.IsChecked = 1;
				treeList.Add(cListItem);
			}
			return JsonDate(treeList);
		}

		#endregion
    }
}
