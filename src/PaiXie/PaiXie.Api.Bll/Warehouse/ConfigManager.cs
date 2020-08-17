using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace PaiXie.Api.Bll {
	/// <summary>
	/// 仓库配置管理
	/// </summary>
	public class ConfigManager {

		#region 保存仓库寄件和售后地址

		/// <summary>
		/// 保存仓库寄件和售后地址
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="objWebInfo">仓库地址管理信息实体类</param>
		/// <returns></returns>
		public static BaseResult Save(string userCode, string warehouseCode, string position, string target, string buttonName, WarehouseAddressWebInfo objWebInfo) {
			BaseResult resultInfo = new BaseResult();
			string oldMessage = string.Empty;
			string newMessage = string.Empty;
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					bool isExists = true;
					WarehouseConfig warehouseConfig = WarehouseConfigService.GetQuerySingleByWarehouseCode(warehouseCode, context);
					if (warehouseConfig == null) {
						isExists = false;
						warehouseConfig = new WarehouseConfig();
					}
					else {
						oldMessage = JsonConvert.SerializeObject(warehouseConfig, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
					}
					#region 实体赋值

					warehouseConfig.WarehouseCode = warehouseCode;
					warehouseConfig.SendPerson = objWebInfo.SendPerson;
					warehouseConfig.SendTel = objWebInfo.SendTel;
					warehouseConfig.SendProvinceID = objWebInfo.SendProvinceID;
					warehouseConfig.SendProvince = objWebInfo.SendProvince;
					warehouseConfig.SendCityID = objWebInfo.SendCityID;
					warehouseConfig.SendCity = objWebInfo.SendCity;
					warehouseConfig.SendDistrictID = objWebInfo.SendDistrictID;
					warehouseConfig.SendDistrict = objWebInfo.SendDistrict;
					warehouseConfig.SendAddressDetail = objWebInfo.SendAddressDetail;
					warehouseConfig.SendAddress = objWebInfo.SendProvince + objWebInfo.SendCity + objWebInfo.SendDistrict + objWebInfo.SendAddressDetail;
					warehouseConfig.SendPostCode = objWebInfo.SendPostCode;
					warehouseConfig.IsSame = objWebInfo.IsSame;
					if (objWebInfo.IsSame == 1) {
						warehouseConfig.ReceivePerson = objWebInfo.SendPerson;
						warehouseConfig.ReceiveTel = objWebInfo.SendTel;
						warehouseConfig.ReceiveProvinceID = objWebInfo.SendProvinceID;
						warehouseConfig.ReceiveProvince = objWebInfo.SendProvince;
						warehouseConfig.ReceiveCityID = objWebInfo.SendCityID;
						warehouseConfig.ReceiveCity = objWebInfo.SendCity;
						warehouseConfig.ReceiveDistrictID = objWebInfo.SendDistrictID;
						warehouseConfig.ReceiveDistrict = objWebInfo.SendDistrict;
						warehouseConfig.ReceiveAddressDetail = objWebInfo.SendAddressDetail;
						warehouseConfig.ReceiveAddress = objWebInfo.SendProvince + objWebInfo.SendCity + objWebInfo.SendDistrict + objWebInfo.SendAddressDetail;
						warehouseConfig.ReceivePostCode = objWebInfo.SendPostCode;
					}
					else {
						warehouseConfig.ReceivePerson = objWebInfo.ReceivePerson;
						warehouseConfig.ReceiveTel = objWebInfo.ReceiveTel;
						warehouseConfig.ReceiveProvinceID = objWebInfo.ReceiveProvinceID;
						warehouseConfig.ReceiveProvince = objWebInfo.ReceiveProvince;
						warehouseConfig.ReceiveCityID = objWebInfo.ReceiveCityID;
						warehouseConfig.ReceiveCity = objWebInfo.ReceiveCity;
						warehouseConfig.ReceiveDistrictID = objWebInfo.ReceiveDistrictID;
						warehouseConfig.ReceiveDistrict = objWebInfo.ReceiveDistrict;
						warehouseConfig.ReceiveAddressDetail = objWebInfo.ReceiveAddressDetail;
						warehouseConfig.ReceiveAddress = objWebInfo.ReceiveProvince + objWebInfo.ReceiveCity + objWebInfo.ReceiveDistrict + objWebInfo.ReceiveAddressDetail;
						warehouseConfig.ReceivePostCode = objWebInfo.ReceivePostCode;
					}

					#endregion
					newMessage = JsonConvert.SerializeObject(warehouseConfig, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
					bool tempFlag = false;
					if (!isExists) {
						warehouseConfig.CreatePerson = userCode;
						warehouseConfig.CreateDate = DateTime.Now;
						tempFlag = WarehouseConfigService.Add(warehouseConfig, context) > 0;
					}
					else {
						warehouseConfig.UpdatePerson = userCode;
						warehouseConfig.UpdateDate = DateTime.Now;
						tempFlag = WarehouseConfigService.Update(warehouseConfig, context) > 0;
					}
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "保存地址失败！";
					}
					else {
						Sys.WriteSyslog(position, target, buttonName, oldMessage, newMessage, (int)SyslogList.基础, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
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
				Sys.SaveErrorLog(ex, "保存仓库寄件和售后地址", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存称重校验设置

		/// <summary>
		/// 保存称重校验设置
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="isScanDelivery">是否先校验后发货 0否 1是</param>
		/// <param name="isOpenWeightWarn">是否开启称重预警 0否 1是</param>
		/// <param name="deviationWeight">称重误差重量</param>
		/// <param name="isWeightDelivery">是否先称重后发货 0否 1是</param>
		/// <returns></returns>
		public static BaseResult Save(string userCode, string warehouseCode, string position, string target, string buttonName, int isScanDelivery, int isOpenWeightWarn, decimal deviationWeight, int isWeightDelivery) {
			BaseResult resultInfo = new BaseResult();
			string oldMessage = string.Empty;
			string newMessage = string.Empty;
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					bool isExists = true;
					WarehouseConfig warehouseConfig = WarehouseConfigService.GetQuerySingleByWarehouseCode(warehouseCode, context);
					if (warehouseConfig == null) {
						isExists = false;
						warehouseConfig = new WarehouseConfig();
					}
					else {
						oldMessage = JsonConvert.SerializeObject(warehouseConfig, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
					}
					#region 实体赋值

					warehouseConfig.WarehouseCode = warehouseCode;
					warehouseConfig.IsScanDelivery = isScanDelivery;
					warehouseConfig.IsOpenWeightWarn = isOpenWeightWarn;
					warehouseConfig.DeviationWeight = deviationWeight;
					warehouseConfig.IsWeightDelivery = isWeightDelivery;

					#endregion
					newMessage = JsonConvert.SerializeObject(warehouseConfig, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
					bool tempFlag = false;
					if (!isExists) {
						warehouseConfig.CreatePerson = userCode;
						warehouseConfig.CreateDate = DateTime.Now;
						tempFlag = WarehouseConfigService.Add(warehouseConfig, context) > 0;
					}
					else {
						warehouseConfig.UpdatePerson = userCode;
						warehouseConfig.UpdateDate = DateTime.Now;
						tempFlag = WarehouseConfigService.Update(warehouseConfig, context) > 0;
					}
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "保存设置失败！";
					}
					else {
						Sys.WriteSyslog(position, target, buttonName, oldMessage, newMessage, (int)SyslogList.基础, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
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
				Sys.SaveErrorLog(ex, "保存称重校验设置", userCode);
			}
			return resultInfo;
		}

		#endregion
	}
}
