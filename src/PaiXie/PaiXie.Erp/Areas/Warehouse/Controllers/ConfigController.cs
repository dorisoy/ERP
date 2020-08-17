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

namespace PaiXie.Erp.Areas.Warehouse
{
	public class ConfigController : BaseController {

		#region Index

		public ActionResult Index() {
			string warehouseCode = FormsAuth.GetWarehouseCode();
			WarehouseConfig warehouseConfig = WarehouseConfigService.GetQuerySingleByWarehouseCode(warehouseCode);
			if (warehouseConfig == null) warehouseConfig = new WarehouseConfig();
			ViewBag.WarehouseConfig = warehouseConfig;
			return View();
		}

		#endregion

		#region 保存称重校验设置

		/// <summary>
		/// 保存称重校验设置
		/// </summary>
		/// <param name="isScanDelivery">是否先校验后发货 0否 1是</param>
		/// <param name="isOpenWeightWarn">是否开启称重预警 0否 1是</param>
		/// <param name="deviationWeight">称重误差重量</param>
		/// <param name="isWeightDelivery">是否先称重后发货 0否 1是</param>
		/// <returns></returns>
		public ActionResult Save(string isScanDelivery, string isOpenWeightWarn, string deviationWeight, string isWeightDelivery) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/ConfigController/Save";
			string buttonName = "保存称重校验设置";
			string target = "基础管理";
			BaseResult resultInfo = ConfigManager.Save(userCode, warehouseCode, position, target, buttonName, ZConvert.StrToInt(isScanDelivery), ZConvert.StrToInt(isOpenWeightWarn), ZConvert.StrToDecimal(deviationWeight), ZConvert.StrToInt(isWeightDelivery));
			return JsonDate(resultInfo);
		}

		#endregion
	}
}
