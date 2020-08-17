using FluentData;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PaiXie.Api.Bll {

	/// <summary>
	/// 打印模版管理
	/// </summary>
	public class PrintTemplateManager {

		#region 获取模版类型名称

		/// <summary>
		/// 获取模版类型名称
		/// </summary>
		/// <param name="typeID">模版类型枚举值</param>
		/// <returns></returns>
		public static string GetTypeName(int typeID) {
			string typeName = string.Empty;
			switch (typeID) {
				case (int)PrintTemplateType.发货单:
					typeName = PrintTemplateType.发货单.ToString();
					break;
				case (int)PrintTemplateType.拣货单:
					typeName = PrintTemplateType.拣货单.ToString();
					break;
			}
			return typeName;
		}

		#endregion
		
		#region 获取默认打印模版

		/// <summary>
		/// 获取默认打印模版
		/// </summary>
		/// <param name="typeID">模版类型 枚举值</param>
		/// <param name="templateXmlUrl">打印模版Url路径</param>
		/// <param name="width">快递模版宽度</param>
		/// <param name="height">快递模版高度</param>
		/// <param name="secondPageOffset">次页打印偏移</param>
		/// <param name="printProField">打印商品明细字段</param>
		/// <param name="printProFieldWidth">打印商品明细字段宽度百分比</param>
		/// <returns></returns>
		public static string GetPrintTemplate(int typeID, string templateXmlUrl, out decimal width, out decimal height, out decimal secondPageOffset, out string printProField, out string printProFieldWidth) {
			string result = string.Empty;
			width = 0;
			height = 0;
			printProField = string.Empty;
			printProFieldWidth = string.Empty;
			secondPageOffset = 0;
			XmlDocument TempXml = new XmlDocument();
			TempXml.Load(System.Web.HttpContext.Current.Server.MapPath(templateXmlUrl));
			XmlNodeList TempNodes = TempXml.SelectNodes("//Temp");
			for (int i = 0; i < TempNodes.Count; i++) {
				int tempTypeID = ZConvert.StrToInt(TempNodes[i].Attributes["TypeID"].InnerText.Trim());
				width = ZConvert.StrToDecimal(TempNodes[i].Attributes["Width"].InnerText.Trim());
				height = ZConvert.StrToDecimal(TempNodes[i].Attributes["Height"].InnerText.Trim());
				secondPageOffset = ZConvert.StrToDecimal(TempNodes[i].Attributes["SecondPageOffset"].InnerText.Trim());
				printProField = TempNodes[i].Attributes["PrintProField"].InnerText.Trim();
				printProFieldWidth = TempNodes[i].Attributes["PrintProFieldWidth"].InnerText.Trim();
				if (typeID == tempTypeID) {
					result = TempNodes[i].InnerText;
					break;
				}
			}
			return result;
		}

		#endregion

		#region 保存模版

		/// <summary>
		/// 保存模版
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="obj">打印模版实体类</param>
		/// <returns></returns>
		public static BaseResult Save(string userCode, string warehouseCode, string position, string target, string buttonName, WarehousePrintTemplate obj) {
			BaseResult resultInfo = new BaseResult();
			string oldMessage = string.Empty;
			string newMessage = string.Empty;
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					if (obj.ID == 0) {
						obj.WarehouseCode = warehouseCode;
						obj.CreatePerson = userCode;
						obj.CreateDate = DateTime.Now;
						bool tempFlag = WarehousePrintTemplateService.Add(obj, context) > 0;
						if (tempFlag) {
							newMessage = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
							Sys.WriteSyslog(position, target, buttonName, oldMessage, newMessage, (int)SyslogList.基础, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "添加模版失败！";
						}
					}
					else {
						WarehousePrintTemplate objWarehousePrintTemplate = WarehousePrintTemplateService.GetQuerySingleByID(obj.ID, context);
						oldMessage = JsonConvert.SerializeObject(objWarehousePrintTemplate, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
						objWarehousePrintTemplate.Name = obj.Name;
						objWarehousePrintTemplate.TypeID = obj.TypeID;
						objWarehousePrintTemplate.IsEnable = obj.IsEnable;
						objWarehousePrintTemplate.UpdatePerson = userCode;
						objWarehousePrintTemplate.UpdateDate = DateTime.Now;
						bool tempFlag = WarehousePrintTemplateService.Update(objWarehousePrintTemplate, context) > 0;
						if (tempFlag) {
							newMessage = JsonConvert.SerializeObject(objWarehousePrintTemplate, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
							Sys.WriteSyslog(position, target, buttonName, oldMessage, newMessage, (int)SyslogList.基础, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "修改模版失败！";
						}

					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存模版", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 启用、禁用模版

		/// <summary>
		/// 启用、禁用模版
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="id">模版ID</param>
		/// <param name="isEnable"> 0：禁用 1：启用</param>
		/// <returns></returns>
		public static BaseResult SetIsEnable(string userCode, string warehouseCode, string position, string target, string buttonName, int id, int isEnable) {
			BaseResult resultInfo = new BaseResult();
			string oldMessage = string.Empty;
			string newMessage = string.Empty;
			string operateName = isEnable == 0 ? "禁用" : "启用";
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehousePrintTemplate printTemplate = WarehousePrintTemplateService.GetQuerySingleByID(id, context);
					oldMessage = JsonConvert.SerializeObject(printTemplate, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
					printTemplate.IsEnable = isEnable;
					printTemplate.UpdatePerson = userCode;
					printTemplate.UpdateDate = DateTime.Now;
					newMessage = JsonConvert.SerializeObject(printTemplate, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
					int count = WarehousePrintTemplateService.Update(printTemplate, context);
					if (count > 0) {
						Sys.WriteSyslog(position, target, buttonName, oldMessage, newMessage, (int)SyslogList.基础, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode);
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = operateName + "失败！";
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, operateName, userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 设置、取消 默认模版

		/// <summary>
		/// 设置、取消 默认模版
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="typeID">模版类型 枚举值</param>
		/// <param name="id">模版ID</param>
		/// <param name="isDefault"> 0：取消默认 1：设置默认</param>
		/// <returns></returns>
		public static BaseResult SetIsDefault(string userCode, string warehouseCode, string position, string target, string buttonName,int typeID, int id, int isDefault) {
			BaseResult resultInfo = new BaseResult();
			string oldMessage = string.Empty;
			string newMessage = string.Empty;
			string operateName = isDefault == 0 ? "取消默认" : "设置默认";
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehousePrintTemplate printTemplate = WarehousePrintTemplateService.GetQuerySingleByID(id, context);
					oldMessage = JsonConvert.SerializeObject(printTemplate, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
					printTemplate.IsDefault = isDefault;
					printTemplate.UpdatePerson = userCode;
					printTemplate.UpdateDate = DateTime.Now;
					newMessage = JsonConvert.SerializeObject(printTemplate, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
					int count = WarehousePrintTemplateService.Update(printTemplate, context);
					if (count > 0) {
						WarehousePrintTemplateService.CancelDefault(userCode, warehouseCode, typeID, id, context);
						Sys.WriteSyslog(position, target, buttonName, oldMessage, newMessage, (int)SyslogList.基础, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode);
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = operateName + "失败！";
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, operateName, userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存打印模版

		/// <summary>
		/// 保存打印模版
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="width">宽度</param>
		/// <param name="height">高度</param>
		/// <param name="id">打印模版ID</param>
		/// <param name="templateContent">模版内容</param>
		/// <param name="secondPageOffset">次页打印偏移</param>
		/// <param name="printerName">默认打印机名称 如果是恢复默认设置，不要传该参数</param>
		/// <param name="isPrintPro">是否打印商品明细 0否 1是 如果是恢复默认设置，不要传该参数</param>
		/// <returns></returns>
		public static BaseResult SavePrintTemplate(string userCode, string warehouseCode, int id, decimal width, decimal height, string templateContent, decimal secondPageOffset, string printerName = null, int? isPrintPro = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				int count = WarehousePrintTemplateService.SavePrintTemplate(userCode, warehouseCode, id, width, height, templateContent, secondPageOffset, printerName, isPrintPro);
				if (count == 0) {
					resultInfo.result = 0;
					resultInfo.message = "操作失败！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存打印模版", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存打印明细字段

		/// <summary>
		/// 保存打印明细字段
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="id">打印模版ID</param>
		/// <param name="skuFields">明细字段</param>
		/// <param name="printProFieldWidth">明细字段宽度占比</param>
		/// <returns></returns>
		public static BaseResult SavePrintPro(string userCode, string warehouseCode, int id, string skuFields, string printProFieldWidth) {
			BaseResult resultInfo = new BaseResult();
			try {
				int count = WarehousePrintTemplateService.SavePrintPro(userCode, warehouseCode, id, skuFields, printProFieldWidth);
				if (count == 0) {
					resultInfo.result = 0;
					resultInfo.message = "保存打印明细字段失败！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存打印明细字段", userCode);
			}
			return resultInfo;
		}

		#endregion
	}
}
