using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Data;
using PaiXie.Core;
using PaiXie.Service;
using PaiXie.Api.Bll;
using PaiXie.Utils;
using System.Text;

namespace PaiXie.Erp.Areas.Warehouse
{
    public class AddressController : BaseController
    {
		#region Index

		public ActionResult Index() {
			string warehouseCode = FormsAuth.GetWarehouseCode();
			WarehouseConfig warehouseConfig = WarehouseConfigService.GetQuerySingleByWarehouseCode(warehouseCode);
			if (warehouseConfig == null) warehouseConfig = new WarehouseConfig();
			ViewBag.WarehouseConfig = warehouseConfig;
			return View();
		}

		#endregion

		#region 保存仓库寄件和售后地址

		public ActionResult Save(WarehouseAddressWebInfo objWebInfo) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/AddressController/Save";
			string buttonName = "保存仓库寄件和售后地址";
			string target = "基础管理";
			BaseResult resultInfo = ConfigManager.Save(userCode, warehouseCode, position, target, buttonName, objWebInfo);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 获取邮政编码

		public ContentResult GetPostCode(int sysareaID) {
			string postCode = string.Empty;
			Sysarea sysarea = SysareaService.GetArea(sysareaID);
			if (sysarea != null) postCode = sysarea.PostCode;
			return Content(postCode);
		}

		#endregion
	}
}
