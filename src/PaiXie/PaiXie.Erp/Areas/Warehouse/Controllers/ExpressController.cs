using PaiXie.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Service;
using PaiXie.Utils;
using PaiXie.Data;
using PaiXie.Api.Bll;
using System.Xml;
namespace PaiXie.Erp.Areas.Warehouse
{
	public class ExpressController : BaseController {

		#region Index

		public ActionResult Index() {
			return View();
		}

		#endregion

		#region 快递列表

		/// <summary>
		/// 快递列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string whereSql = string.Format("we.WarehouseCode='{0}'", warehouseCode);

			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "we.Seq ASC,we.ID DESC";
			data.From = "warehouseExpress we LEFT JOIN logistics ON we.LogisticsID=logistics.ID";
			data.Select = @"we.ID,we.WarehouseCode,logistics.Name AS LogisticsName,logistics.Code AS LogisticsCode,we.Name,we.PrinterType,'' as PrinterTypeName,we.IsEnable";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseExpressList> list = BaseService<WarehouseExpressList>.GetQueryManyForPage(data, out total);
			for (int i = 0; i < list.Count(); i++) {
				list[i].PrinterTypeName = ExpressManager.GetPrinterTypeName(list[i].PrinterType);
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 编辑、添加快递

		public ActionResult Edit(int id = 0) {
			WarehouseExpress objWarehouseExpress = new WarehouseExpress();
			if (id > 0) {
				objWarehouseExpress = WarehouseExpressService.GetQuerySingleByID(id);
			}
			ViewBag.WarehouseExpress = objWarehouseExpress;
			return View();
		}

		#endregion

		#region 检查当前仓库是否已经存在快递名称

		/// <summary>
		/// 检查当前仓库是否已经存在快递名称
		/// </summary>
		/// <param name="id">快递公司ID</param>
		/// <param name="name">快递名称</param>
		/// <returns></returns>
		public ActionResult CheckName(int id, string name) {
			BaseResult resultInfo = new BaseResult();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			if (id > 0) {
				if (WarehouseExpressService.GetExpressID(warehouseCode, name, id) > 0) {
					resultInfo.result = -1;
				}

			}
			else {
				if (WarehouseExpressService.GetExpressID(warehouseCode, name) > 0) {
					resultInfo.result = -1;
				}
			}
			return JsonDate(resultInfo);
		}

		#endregion

		#region 保存快递

		[HttpPost]
		public ActionResult Save(WarehouseExpress warehouseExpress) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/ExpressController/Save";
			string buttonName = "保存快递";
			string target = "快递管理";
			BaseResult resultInfo = ExpressManager.Save(userCode, warehouseCode, position, target, buttonName, warehouseExpress);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 启用、禁用快递

		/// <summary>
		/// 启用、禁用快递
		/// </summary>
		/// <param name="id">快递ID</param>
		/// <param name="isEnable"> 0：禁用 1：启用</param>
		/// <returns></returns>
		public ActionResult SetIsEnable(int id, int isEnable) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/ExpressController/SetIsEnable";
			string buttonName = (isEnable == 0 ? "禁用" : "启用") + "快递";
			string target = "快递管理";
			BaseResult resultInfo = ExpressManager.SetIsEnable(userCode, warehouseCode, position, target, buttonName, id, isEnable);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 下拉列表

		/// <summary>
		/// 下拉列表
		/// </summary>
		/// <returns></returns>
		public ActionResult JsonTree() {
			CListItem cListItem = new CListItem();
			cListItem.Text = "请选择";
			cListItem.Value = "0";
			List<CListItem> treeList = new List<CListItem>();
			treeList.Add(cListItem);
			Object[] objects = new Object[1];
			objects[0] = FormsAuth.GetWarehouseCode();
			string sqlStr = @"SELECT ID AS Value ,Name AS Text FROM warehouseExpress WHERE WarehouseCode=@0 AND IsEnable=" + (int)IsEnable.是 + " ORDER BY Seq ASC,ID DESC";
			List<CListItem> objlist = BaseService<CListItem>.GetQueryMany(sqlStr, null, objects);
			treeList.AddRange(objlist);
			return JsonDate(treeList);
		}

		#endregion

		#region 设置快递打印模版

		/// <summary>
		/// 设置快递打印模版
		/// </summary>
		/// <param name="id">快递公司ID</param>
		/// <param name="logisticsCode">物流代码</param>
		/// <param name="printerType">打印类型 0针式 1热敏</param>
		/// <returns></returns>
		public ActionResult SetExpressPrintTemplate(int id, string logisticsCode, string logisticsName, int printerType) {
			string templateContent = string.Empty;
			WarehouseExpress express = WarehouseExpressService.GetQuerySingleByID(id);
			if (express != null) {
				if (ZConvert.ToString(express.TemplateContent) == string.Empty) {
					decimal width = 0;
					decimal height = 0;
					express.TemplateContent = ExpressManager.GetExpressPrintTemplate(logisticsCode, printerType, "../../Xml/PrintTemplate/Express.xml", out width, out height);
					express.Width = width;
					express.Height = height;
				}
				//读取模板，前面3行不允许改动
				templateContent = "LODOP.PRINT_INITA(\"0mm\",\"0mm\",\"" + express.Width + "mm\",\"" + express.Height + "mm\",\"快递模板设计_" + NewKey.datetime() + "\");" + (char)13;
				templateContent += "LODOP.SET_PRINT_PAGESIZE(1,\"" + express.Width + "mm\",\"" + express.Height + "mm\",\"\");" + (char)13;
				templateContent += "LODOP.SET_PRINT_STYLE(\"FontSize\",12);";//注意单位是pt
				templateContent += express.TemplateContent;
			}
			ViewBag.LogisticsCode = logisticsCode;
			ViewBag.LogisticsName = logisticsName;
			ViewBag.WarehouseExpress = express;
			ViewBag.TemplateContent = templateContent;
			return View();
		}

		/// <summary>
		/// 保存打印模版
		/// </summary>
		/// <param name="id">快递公司ID</param>
		/// <param name="width">宽度</param>
		/// <param name="height">高度</param>
		/// <param name="templateContent">模版内容</param>
		/// <param name="printerName">默认打印机名称</param>
		/// <param name="isPrintPro">是否打印商品明细 0否 1是</param>
		/// <returns></returns>
		public ActionResult SavePrintTemplate(int id, decimal width, decimal height, string templateContent, string printerName, int isPrintPro) {
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
			BaseResult resultInfo = ExpressManager.SavePrintTemplate(userCode, warehouseCode, id, width, height, templateContent, printerName, isPrintPro);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 恢复默认设置
		/// </summary>
		/// <param name="id">快递公司ID</param>
		/// <returns></returns>
		public ActionResult ResetDefault(int id) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			decimal width = 0;
			decimal height = 0;
			string templateContent = string.Empty;
			BaseResult resultInfo = ExpressManager.SavePrintTemplate(userCode, warehouseCode, id, width, height, templateContent);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 设置明细项
		/// </summary>
		/// <param name="id">快递公司ID</param>
		/// <returns></returns>
		public ActionResult SetPrintPro(int id) {
			ViewBag.WarehouseExpress = WarehouseExpressService.GetQuerySingleByID(id);
			return View();
		}

		/// <summary>
		/// 保存明细项
		/// </summary>
		/// <param name="id">快递公司ID</param>
		/// <param name="skuFields">明细字段</param>
		/// <returns></returns>
		public ActionResult SavePrintPro(int id, string skuFields) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = ExpressManager.SavePrintPro(userCode, warehouseCode, id, skuFields);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 设置运费

		/// <summary>
		/// 设置运费
		/// </summary>
		/// <param name="id">快递公司ID</param>
		/// <param name="logisticsCode">物流代码</param>
		/// <param name="printerType">打印类型 0针式 1热敏</param>
		/// <returns></returns>
		public ActionResult SetExpressPrice(int id, string logisticsCode, string logisticsName, int printerType) {
			WarehouseExpress express = WarehouseExpressService.GetQuerySingleByID(id);
			if (express == null) express = new WarehouseExpress();
			List<WarehouseExpressPrice> expressPriceList = WarehouseExpressPriceService.GetQueryManyByExpressID(id);
			ViewBag.LogisticsCode = logisticsCode;
			ViewBag.LogisticsName = logisticsName;
			ViewBag.WarehouseExpress = express;
			ViewBag.WarehouseExpressPriceList = expressPriceList;
			return View();
		}

		/// <summary>
		/// 保存运费
		/// </summary>
		/// <param name="obj">快递公司运费信息类</param>
		/// <returns></returns>
		public ActionResult SaveExpressPrice(WarehouseExpressPriceWebInfo obj) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/ExpressController/SaveExpressPrice";
			string buttonName = "保存运费";
			string target = "快递管理";
			BaseResult resultInfo = ExpressManager.SaveExpressPrice(userCode, warehouseCode, position, target, buttonName, obj);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 选择地区
		/// </summary>
		/// <param name="warehouseExpressPriceID">快递计费表主键ID</param>
		/// <param name="rowIndex">表格行索引</param>
		/// <param name="checkedIDs">选中的ID 编辑还未保存的地区，设置选中</param>
		/// <returns></returns>
		public ActionResult SelectArea(int warehouseExpressPriceID, int rowIndex, string checkedIDs = "") {
			WarehouseExpressPrice warehouseExpressPrice = new WarehouseExpressPrice();
			List<int> checkedAreaIDList = new List<int>();
			if (warehouseExpressPriceID > 0) {
				warehouseExpressPrice = WarehouseExpressPriceService.GetQuerySingleByID(warehouseExpressPriceID);
				checkedAreaIDList.AddRange(warehouseExpressPrice.SysAreaIDs.Split(',').Select(areaID => ZConvert.StrToInt(areaID)));
			}
			else if (rowIndex > 0) {
				checkedAreaIDList.AddRange(checkedIDs.Split(',').Select(areaID => ZConvert.StrToInt(areaID)));
			}
			SelectAreaWebInfo selectAreaWebInfo = new SelectAreaWebInfo();
			selectAreaWebInfo.LargeAreaList = new List<LargeArea>();
			List<Sysarea> largeAreaList = SysareaService.GetLargeAreaList();
			foreach (var largeAreaItem in largeAreaList) {
				LargeArea largeArea = new LargeArea();
				largeArea.Name = largeAreaItem.LargeArea;
				largeArea.ProvinceList = new List<Province>();
				List<Sysarea> provinceList = SysareaService.GetProvinceList(largeArea.Name);
				foreach (var provinceItem in provinceList) {
					Province province = new Province();
					province.ID = provinceItem.ID;
					province.AliasName = provinceItem.AliasName;
					province.IsChecked = checkedAreaIDList.Contains(province.ID);
					province.CityList = new List<City>();
					List<Sysarea> cityList = SysareaService.GetCityList(province.ID);
					foreach (var cityItem in cityList) {
						City city = new City();
						city.ID = cityItem.ID;
						city.Name = cityItem.Name;
						city.IsChecked = checkedAreaIDList.Contains(city.ID);
						province.CityList.Add(city);
					}
					largeArea.ProvinceList.Add(province);
				}
				selectAreaWebInfo.LargeAreaList.Add(largeArea);
			}
			ViewBag.SelectAreaWebInfo = selectAreaWebInfo;
			ViewBag.WarehouseExpressPrice = warehouseExpressPrice;
			ViewBag.RowIndex = rowIndex;
			return View();
		}

		/// <summary>
		/// 删除运费记录
		/// </summary>
		/// <param name="warehouseExpressPriceID">快递计费表主键ID</param>
		/// <returns></returns>
		public ActionResult DeleteExpressPrice(int warehouseExpressPriceID) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/ExpressController/DeleteExpressPrice";
			string buttonName = "删除运费记录";
			string target = "快递管理";
			BaseResult resultInfo = ExpressManager.DeleteExpressPrice(userCode, warehouseCode, position, target, buttonName, warehouseExpressPriceID);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 检查快递打印类型是否热敏

		/// <summary>
		/// 检查快递打印类型是否热敏
		/// </summary>
		/// <param name="id">快递公司ID</param>
		/// <returns></returns>
		public ActionResult CheckIsHot(int id) {
			BaseResult resultInfo = new BaseResult();
			WarehouseExpress express = WarehouseExpressService.GetQuerySingleByID(id);
			if (express != null) {
				resultInfo.result = express.PrinterType == (int)PrinterType.热敏 ? 1 : 0;
			}
			else {
				resultInfo.result = 0;
			}
			return JsonDate(resultInfo);
		}

		#endregion
	}
}
