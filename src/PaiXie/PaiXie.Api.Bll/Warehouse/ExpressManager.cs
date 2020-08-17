using FluentData;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Xml;

namespace PaiXie.Api.Bll {

	/// <summary>
	/// 快递管理类
	/// </summary>
	public class ExpressManager {

		#region 获取打印类型名称

		/// <summary>
		/// 获取打印类型名称
		/// </summary>
		/// <param name="printerType">打印类型 枚举值</param>
		/// <returns></returns>
		public static string GetPrinterTypeName(int printerType) {
			string printerTypeName = string.Empty;
			switch (printerType) {
				case (int)PrinterType.针式:
					printerTypeName = PrinterType.针式.ToString();
					break;
				case (int)PrinterType.热敏:
					printerTypeName = PrinterType.热敏.ToString();
					break;
			}
			return printerTypeName;
		}

		#endregion

		#region 保存快递公司

		/// <summary>
		/// 保存快递公司
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="obj">快递信息实体类</param>
		/// <returns></returns>
		public static BaseResult Save(string userCode, string warehouseCode, string position, string target, string buttonName, WarehouseExpress obj) {
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
						bool tempFlag = WarehouseExpressService.Add(obj, context) > 0;
						if (tempFlag) {
							newMessage = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
							Sys.WriteSyslog(position, target, buttonName, oldMessage, newMessage, (int)SyslogList.基础, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "添加快递失败！";
						}
					}
					else {
						WarehouseExpress objWarehouseExpress = WarehouseExpressService.GetQuerySingleByID(obj.ID, context);
						oldMessage = JsonConvert.SerializeObject(objWarehouseExpress, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
						objWarehouseExpress.Name = obj.Name;
						objWarehouseExpress.LogisticsID = obj.LogisticsID;
						objWarehouseExpress.PrinterType = obj.PrinterType;
						objWarehouseExpress.IsEnable = obj.IsEnable;
						objWarehouseExpress.UpdatePerson = userCode;
						objWarehouseExpress.UpdateDate = DateTime.Now;
						bool tempFlag = WarehouseExpressService.Update(objWarehouseExpress, context) > 0;
						if (tempFlag) {
							newMessage = JsonConvert.SerializeObject(objWarehouseExpress, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
							Sys.WriteSyslog(position, target, buttonName, oldMessage, newMessage, (int)SyslogList.基础, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "修改快递失败！";
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
				Sys.SaveErrorLog(ex, "保存快递信息", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 启用、禁用快递

		/// <summary>
		/// 启用、禁用快递
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="WarehouseCode">仓库编码</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="id">快递ID</param>
		/// <param name="isEnable"> 0：禁用 1：启用</param>
		/// <returns></returns>
		public static BaseResult SetIsEnable(string userCode, string WarehouseCode, string position, string target, string buttonName, int id, int isEnable) {
			BaseResult resultInfo = new BaseResult();
			string oldMessage = string.Empty;
			string newMessage = string.Empty;
			string operateName = isEnable == 0 ? "禁用" : "启用";
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseExpress express = WarehouseExpressService.GetQuerySingleByID(id, context);
					oldMessage = JsonConvert.SerializeObject(express, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
					express.IsEnable = isEnable;
					express.UpdatePerson = userCode;
					express.UpdateDate = DateTime.Now;
					newMessage = JsonConvert.SerializeObject(express, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
					int count = WarehouseExpressService.Update(express, context);
					if (count > 0) {
						Sys.WriteSyslog(position, target, buttonName, oldMessage, newMessage, (int)SyslogList.基础, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, WarehouseCode);
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = operateName + "快递失败！";
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
				Sys.SaveErrorLog(ex, operateName + "快递", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存快递打印模版

		/// <summary>
		/// 保存快递打印模版
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="width">宽度</param>
		/// <param name="height">高度</param>
		/// <param name="id">快递公司ID</param>
		/// <param name="templateContent">模版内容</param>
		/// <param name="expressPrinterName">默认打印机名称 如果是恢复默认设置，不要传该参数</param>
		/// <param name="isPrintPro">是否打印商品明细 0否 1是 如果是恢复默认设置，不要传该参数</param>
		/// <returns></returns>
		public static BaseResult SavePrintTemplate(string userCode, string warehouseCode, int id, decimal width, decimal height, string templateContent, string expressPrinterName = null, int? isPrintPro = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				int count = WarehouseExpressService.SavePrintTemplate(userCode, warehouseCode, id, width, height, templateContent, expressPrinterName, isPrintPro);
				if (count == 0) {
					resultInfo.result = 0;
					resultInfo.message = "操作失败！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存快递打印模版", userCode);
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
		/// <param name="id">快递公司ID</param>
		/// <param name="skuFields">明细字段</param>
		/// <returns></returns>
		public static BaseResult SavePrintPro(string userCode, string warehouseCode, int id, string skuFields) {
			BaseResult resultInfo = new BaseResult();
			try {
				int count = WarehouseExpressService.SavePrintPro(userCode, warehouseCode, id, skuFields);
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

		#region 保存运费记录

		/// <summary>
		/// 保存运费记录
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="objWebInfo">快递公司运费信息类</param>
		/// <returns></returns>
		public static BaseResult SaveExpressPrice(string userCode, string warehouseCode, string position, string target, string buttonName, WarehouseExpressPriceWebInfo objWebInfo) {
			BaseResult resultInfo = new BaseResult();
			string oldMessage = string.Empty;
			string newMessage = string.Empty;
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					for (int i = 0; i < objWebInfo.ID.Count; i++) {
						int id = objWebInfo.ID[i];
						List<string> existsList = new List<string>();
						#region 检查地区是否已存在，修改时要排除自己
						string[] arrSysAreaName = objWebInfo.SysAreaNames[i].Split(',');
						foreach (var sysAreaName in arrSysAreaName) {
							bool isExists = WarehouseExpressPriceService.IsExists(warehouseCode, objWebInfo.ExpressID, sysAreaName, id, context);
							if (isExists) {
								existsList.Add(sysAreaName);
							}
						}
						#endregion
						if (existsList.Count == 0) {
							if (id == 0) {
								#region 添加
								WarehouseExpressPrice warehouseExpressPrice = new WarehouseExpressPrice();
								warehouseExpressPrice.WarehouseCode = warehouseCode;
								warehouseExpressPrice.ExpressID = objWebInfo.ExpressID;
								warehouseExpressPrice.SysAreaNames = objWebInfo.SysAreaNames[i];
								warehouseExpressPrice.SysAreaIDs = objWebInfo.SysAreaIDs[i];
								warehouseExpressPrice.FirstPrice = objWebInfo.FirstPrice[i];
								warehouseExpressPrice.FirstWeight = objWebInfo.FirstWeight[i];
								warehouseExpressPrice.ContinuePrice = objWebInfo.ContinuePrice[i];
								warehouseExpressPrice.ContinueWeight = objWebInfo.ContinueWeight[i];
								warehouseExpressPrice.CreatePerson = userCode;
								warehouseExpressPrice.CreateDate = DateTime.Now;
								id = WarehouseExpressPriceService.Add(warehouseExpressPrice, context);
								if (id > 0) {
									oldMessage = string.Empty;
									newMessage = JsonConvert.SerializeObject(warehouseExpressPrice, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
									Sys.WriteSyslog(position, target, buttonName, oldMessage, newMessage, (int)SyslogList.基础, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "地区：" + warehouseExpressPrice.SysAreaNames + " 保存失败！";
									break;
								}
								#endregion
							}
							else {
								#region 修改
								WarehouseExpressPrice warehouseExpressPrice = WarehouseExpressPriceService.GetQuerySingleByID(id, context);
								if (warehouseExpressPrice != null) {
									oldMessage = JsonConvert.SerializeObject(warehouseExpressPrice, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
									warehouseExpressPrice.SysAreaNames = objWebInfo.SysAreaNames[i];
									warehouseExpressPrice.SysAreaIDs = objWebInfo.SysAreaIDs[i];
									warehouseExpressPrice.FirstPrice = objWebInfo.FirstPrice[i];
									warehouseExpressPrice.FirstWeight = objWebInfo.FirstWeight[i];
									warehouseExpressPrice.ContinuePrice = objWebInfo.ContinuePrice[i];
									warehouseExpressPrice.ContinueWeight = objWebInfo.ContinueWeight[i];
									warehouseExpressPrice.UpdatePerson = userCode;
									warehouseExpressPrice.UpdateDate = DateTime.Now;
									int count = WarehouseExpressPriceService.Update(warehouseExpressPrice, context);
									if (count > 0) {
										newMessage = JsonConvert.SerializeObject(warehouseExpressPrice, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
										Sys.WriteSyslog(position, target, buttonName, oldMessage, newMessage, (int)SyslogList.基础, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
									}
									else {
										resultInfo.result = 0;
										resultInfo.message = "地区：" + objWebInfo.SysAreaNames[i] + " 保存失败！";
										break;
									}
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "地区：" + objWebInfo.SysAreaNames[i] + " 已被删除！";
									break;
								}
								#endregion
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "地区：" + string.Join(",", existsList.ToArray()) + " 已存在！";
							break;
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
				Sys.SaveErrorLog(ex, "保存运费记录", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除运费记录

		/// <summary>
		/// 删除运费记录
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="warehouseExpressPriceID">快递计费表主键ID</param>
		/// <returns></returns>
		public static BaseResult DeleteExpressPrice(string userCode, string warehouseCode,string position, string target, string buttonName, int warehouseExpressPriceID) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseExpressPrice expressPrice = WarehouseExpressPriceService.GetQuerySingleByID(warehouseExpressPriceID, context);
					if (expressPrice != null) {
						int count = WarehouseExpressPriceService.DelByID(warehouseExpressPriceID, context);
						if (count > 0) {
							string oldMessage = JsonConvert.SerializeObject(expressPrice, Newtonsoft.Json.Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
							Sys.WriteSyslog(position, target, buttonName, oldMessage, string.Empty, (int)SyslogList.基础, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "删除运费记录失败！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "运费记录不存在或已删除！";
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
				Sys.SaveErrorLog(ex, "删除运费记录", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 获取快递默认打印模版

		/// <summary>
		/// 获取快递默认打印模版
		/// </summary>
		/// <param name="logisticsCode">物流代码</param>
		/// <param name="printerType">0代表针式，1代表热敏</param>
		/// <param name="templateXmlUrl">模版Url地址</param>
		/// <param name="width">快递模版宽度</param>
		/// <param name="height">快递模版高度</param>
		/// <returns></returns>
		public static string GetExpressPrintTemplate(string logisticsCode, int printerType, string templateXmlUrl, out decimal width, out decimal height) {
			string result = string.Empty;
			width = 0;
			height = 0;
			XmlDocument TempXml = new XmlDocument();
			TempXml.Load(System.Web.HttpContext.Current.Server.MapPath(templateXmlUrl));
			XmlNodeList TempNodes = TempXml.SelectNodes("//Temp");
			for (int i = 0; i < TempNodes.Count; i++) {
				string code = TempNodes[i].Attributes["Code"].InnerText;
				width = ZConvert.StrToDecimal(TempNodes[i].Attributes["Width"].InnerText.Trim());
				height = ZConvert.StrToDecimal(TempNodes[i].Attributes["Height"].InnerText.Trim());
				int tempPrinterType = ZConvert.StrToInt(TempNodes[i].Attributes["PrinterType"].InnerText.Trim());
				if (logisticsCode.ToUpper().IndexOf(code) >= 0 && printerType == tempPrinterType) {
					result = TempNodes[i].InnerText;
					break;
				}
			}
			if (result == string.Empty) {
				width = ZConvert.StrToDecimal(TempNodes[TempNodes.Count - 1].Attributes["Width"].InnerText.Trim());
				height = ZConvert.StrToDecimal(TempNodes[TempNodes.Count - 1].Attributes["Height"].InnerText.Trim());
				result = TempNodes[TempNodes.Count - 1].InnerText;
			}
			return result;
		}

		#endregion

		#region 根据快递公司ID、重量、省市ID获取快递运费

		/// <summary>
		/// 根据快递公司ID、重量、省市ID获取快递运费
		/// </summary>
		/// <param name="expressID">快递公司ID</param>
		/// <param name="totalWeight">重量</param>
		/// <param name="cityID">市级地区ID</param>
		/// <param name="provinceID">省级地区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static decimal GetExpressPrice(int expressID, decimal totalWeight, int provinceID, int cityID, IDbContext context = null) {
			decimal expressPrice = 0;
			if (totalWeight > 0) {
				//获取市级地区运费记录
				WarehouseExpressPrice objExpressPrice = WarehouseExpressPriceService.GetQuerySingleByExpressIDAndCityID(expressID, cityID, context);
				if (objExpressPrice == null) {
					//获取省级地区运费记录
					objExpressPrice = WarehouseExpressPriceService.GetQuerySingleByExpressIDAndProvinceID(expressID, provinceID, context);
					if (objExpressPrice == null) {
						//获取全国运费记录
						objExpressPrice = WarehouseExpressPriceService.GetQuerySingleByExpressID(expressID, context);
					}
				}
				if (objExpressPrice != null) {
					//超过首重且续重和续价大于0
					if (totalWeight > objExpressPrice.FirstWeight && objExpressPrice.ContinueWeight > 0 && objExpressPrice.ContinuePrice > 0) {
						//计算续重费用
						decimal num = 0;
						if ((totalWeight - objExpressPrice.FirstWeight) % objExpressPrice.ContinueWeight > 0) {
							num = Math.Ceiling((totalWeight - objExpressPrice.FirstWeight) / objExpressPrice.ContinueWeight);
						}
						else {
							num = Math.Floor((totalWeight - objExpressPrice.FirstWeight) / objExpressPrice.ContinueWeight);
						}
						expressPrice = objExpressPrice.FirstPrice + (num * objExpressPrice.ContinuePrice);
					}
					else {
						expressPrice = objExpressPrice.FirstPrice;
					}
				}
			}
			return expressPrice;
		}

		#endregion
	}
}
