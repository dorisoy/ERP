using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using System.Linq;
using PaiXie.Service;
using PaiXie.Utils;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;


namespace PaiXie.Erp.Areas.Order.Controllers {
	public class DistributionWarehouseController : BaseController {

		//
		// GET: /Order/DistributionWarehouse/
		public ActionResult Index(string erpOrderCode = "") {
			Ordbase ordbase = null;
			List<WarehouseOutbound> outboundList = new List<WarehouseOutbound>();
			List<WarehouseOutboundPickItemWebInfo> outboundItemList = new List<WarehouseOutboundPickItemWebInfo>();
			ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode);
			if (ordbase == null) {
				ordbase = new Ordbase();
			}
			else {
				outboundList = WarehouseOutboundService.GetWarehouseOutboundByErpOrderCode(ordbase.ErpOrderCode);
				outboundItemList = OrditemService.GetManyOutboundItem(ordbase.ErpOrderCode);
			}

			DataTable matchingWarehouseList = OrdbaseManager.AutoMatchingWarehouse(ordbase.ErpOrderCode, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
			List<DistributionWarehouseInfo> distributionWarehouseList = OrditemService.GetManyDistributionWarehouseInfo(ordbase.ID);
			if (distributionWarehouseList == null) {
				distributionWarehouseList = new List<DistributionWarehouseInfo>();
			}
			else {
				foreach (var item in distributionWarehouseList) {
					item.warehouseProductsSkuInventoryList = ProductsSkuService.GetWarehouseSkuInventory(item.ProductsSkuID, "").Where(r => (r.KyNum - r.ZyNum - r.OrdZyNum + r.BookingKyNum) > 0).ToList();
					int kfhNum = item.warehouseProductsSkuInventoryList.Sum(r => r.KyNum - r.ZyNum - r.OrdZyNum + r.BookingKyNum);
					if (item.WfpNum > kfhNum) {
						item.CheckStatus = 1;
					}
				}
			}

			ViewBag.Ordbase = ordbase;
			ViewBag.OutboundList = outboundList;
			ViewBag.OutboundItemList = outboundItemList;
			ViewBag.MatchingWarehouseList = matchingWarehouseList;
			ViewBag.DistributionWarehouseList = distributionWarehouseList;
			return View();
		}

		/// <summary>
		/// 生成出库单
		/// </summary>
		/// <param name="distributionWarehouseWebInfo"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult CreateOutbound(DistributionWarehouseWebInfo distributionWarehouseWebInfo) {
			BaseResult resultInfo = OutboundManager.CreateOutbound(FormsAuth.GetUserCode(), FormsAuth.GetUserName(), distributionWarehouseWebInfo);
			int IsGenerateComplete = 0;
			if (resultInfo.result == 1) {
				IsGenerateComplete = OrdbaseService.IsGenerateComplete(distributionWarehouseWebInfo.OrdbaseID);
			}
			var result = new { result = resultInfo.result, message = resultInfo.message, isGenerateComplete = IsGenerateComplete };
			return JsonDate(result);
		}

		/// <summary>
		/// 生成出库单
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="warehouseCode"></param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult CreateOutbound(string erpOrderCode,string warehouseCode) {
			BaseResult resultInfo = OutboundManager.CreateOutbound(FormsAuth.GetUserCode(), FormsAuth.GetUserName(),erpOrderCode,warehouseCode);
			var result = new { result = resultInfo.result, message = resultInfo.message, isGenerateComplete = 1 };
			return JsonDate(result);
		}
	}
}
