using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Warehouse
{
	public class StockItemController : BaseController
    {
        //
        // GET: /Warehouse/StockItem/

        public ActionResult Index()
        {
			ViewBag.ProductsCode = ZConvert.ToString(Request["productsCode"]);
			ViewBag.StartDate = ZConvert.ToString(Request["startDate"]);
			ViewBag.EndDate = ZConvert.ToString(Request["endDate"]);
            return View();
        }

		/// <summary>
		/// SKU库存情况列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			string startDate = ZConvert.ToString(Request["startDate"]);
			string endDate = ZConvert.ToString(Request["endDate"]);
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = "products p inner join warehouseProducts wp on p.ID = wp.ProductsID inner join productsSku ps on p.ID = ps.ProductsID";
			data.Select = "p.ID,p.Code,p.Name,p.BrandID,p.CategoryID,p.TaxRate,ps.ID ProductsSkuID,ps.Code ProductsSkuCode,ps.Saleprop ProductsSkuSaleprop";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<StockItemList> list = BaseService<StockItemList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				Brand brand = BrandService.GetSingleBrand(item.BrandID);
				Category category = CategoryService.GetSingleCategory(item.CategoryID);
				if (brand != null) {
					item.BrandName = brand.Name;
				}
				if (category != null) {
					item.CategoryName = category.Name;
				}
				DataTable initialDt = WarehouseOutInStockLogService.GetInitialOutInStockLog(FormsAuth.GetWarehouseCode(), item.ID, item.ProductsSkuID, startDate, "");
				if (initialDt.Rows.Count == 1) {
					item.InitialInventory = ZConvert.StrToInt(initialDt.Rows[0]["InitialInventory"]);
					item.InitialCost = ZConvert.StrToDecimal(initialDt.Rows[0]["InitialCost"]);
				}


				DataTable finalDt = WarehouseOutInStockLogService.GetInitialOutInStockLog(FormsAuth.GetWarehouseCode(), item.ID, item.ProductsSkuID, "", endDate);
				if (finalDt.Rows.Count == 1) {
					item.FinalQuantity = ZConvert.StrToInt(finalDt.Rows[0]["InitialInventory"]);
					item.FinalCost = ZConvert.StrToDecimal(finalDt.Rows[0]["InitialCost"]);
				}

				DataTable dt = WarehouseOutInStockLogService.GetManyOutInStockLog(FormsAuth.GetWarehouseCode(), item.ID, item.ProductsSkuID, startDate, endDate);
				if (dt.Rows.Count == 1) {
					item.OutboundNum = ZConvert.StrToInt(dt.Rows[0]["OutboundNum"]);
					item.StorageNum = ZConvert.StrToInt(dt.Rows[0]["StorageNum"]);
					item.AdjustQuantity = ZConvert.StrToInt(dt.Rows[0]["AdjustQuantity"]);
					item.RollOutQuantity = ZConvert.StrToInt(dt.Rows[0]["RollOutQuantity"]);
					item.QuantityOfTransfer = ZConvert.StrToInt(dt.Rows[0]["QuantityOfTransfer"]);
				}
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		/// <summary>
		/// 获取搜索条件
		/// </summary>
		/// <returns></returns>
		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int categoryID = ZConvert.StrToInt(Request["categoryID"]);
			int brandID = ZConvert.StrToInt(Request["brandID"]);

			string whereSql = " wp.WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "'";

			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and p.Name like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and p.Code like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and ps.Code like '%{0}%'", keyWord);
						break;
				}
			}
			if (categoryID > 0) {
				whereSql += string.Format(" and p.CategoryID = {0}", categoryID);
			}
			if (brandID > 0) {
				whereSql += string.Format(" and p.BrandID = {0}", brandID);
			}

			return whereSql;
		}
    }
}
