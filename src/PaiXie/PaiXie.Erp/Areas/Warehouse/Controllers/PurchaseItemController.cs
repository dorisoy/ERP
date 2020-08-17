#region using
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
#endregion

namespace PaiXie.Erp.Areas.Warehouse
{
    public class PurchaseItemController : BaseController
    {
		#region Index

		public ActionResult Index(string aliasName, int purchaseID) {
			WarehousePurchase warehousePurchase = WarehousePurchaseService.GetQuerySingleByID(purchaseID);
			ViewBag.AliasName = aliasName;
			ViewBag.WarehousePurchase = warehousePurchase;
			ViewBag.ExpectedAmount = PurchaseManager.GetExpectedAmount(purchaseID, warehousePurchase.SuppliersID);
			return View();
		}

		#endregion

		#region 采购单商品列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			int suppliersID = ZConvert.StrToInt(Request["suppliersID"]);
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wpi.ID DESC";
			data.From = @"warehousePurchaseItem wpi";
			data.Select = "wpi.ID,wpi.ProductsCode,wpi.ProductsName,wpi.ProductsSkuID,wpi.ProductsSkuSaleprop,wpi.ProductsSkuCode,wpi.Num,wpi.InStockNum";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehousePurchaseItemList> list = BaseService<WarehousePurchaseItemList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				item.KyNum = ProductsSkuService.GetKfhNumByProductsSkuID(FormsAuth.GetWarehouseCode(), item.ProductsSkuID);
				decimal price = SuppliersManager.GetPurchasePrice(item.ProductsSkuID, suppliersID);
				item.ExpectedAmount = item.Num * price;
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);

		}

		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int purchaseID = ZConvert.StrToInt(Request["purchaseID"]);
			string whereSql = string.Format("wpi.purchaseID={0}", purchaseID);
			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and wpi.ProductsName like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and wpi.ProductsCode like '%{0}%'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and wpi.ProductsNo like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and wpi.ProductsSkuCode like '%{0}%'", keyWord);
						break;
				}
			}
			return whereSql;
		}

		#endregion

		#region 采购单商品入库

		public ActionResult Storage(int purchaseID, string ids) {
			List<int> purchaseItemIDList = new List<int>();
			if (ids == "") {
				SelectBuilder data = new SelectBuilder();
				data.Having = "";
				data.GroupBy = "";
				data.OrderBy = "";
				data.From = "warehousePurchaseItem wpi";
				data.Select = "wpi.ID";
				data.WhereSql = GetWhereSql(); ;
				data.PagingCurrentPage = 0;
				data.PagingItemsPerPage = 0;
				int total = 0;
				purchaseItemIDList = ProductsService.GetProductsIDListForPage(data, out total);
			}
			else {
				purchaseItemIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			}
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = OutInStockManager.PurchaseItemStorage(userCode, warehouseCode, purchaseID, purchaseItemIDList);
			return JsonDate(resultInfo);
		}

		#endregion
	}
}