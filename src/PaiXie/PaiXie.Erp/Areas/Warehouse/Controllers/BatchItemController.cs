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
	public class BatchItemController : BaseController
    {
        //
		// GET: /Warehouse/BatchItem/

        public ActionResult Index()
        {
			ViewBag.BatchCode = ZConvert.ToString(Request["batchCode"]);
			ViewBag.BatchSource = ZConvert.ToString(Request["batchSource"]);
			ViewBag.CreateDate = ZConvert.ToString(Request["createDate"]);
            return View();
        }

		/// <summary>
		/// 批次商品列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = "warehouseProductsBatch b INNER JOIN warehouselocationproducts wlp ON b.BatchCode = wlp.ProductsBatchCode AND b.ProductsSkuID = wlp.ProductsSkuID INNER JOIN products p ON b.ProductsID = p.ID INNER JOIN productsSku ps ON wlp.ProductsSkuID = ps.ID";
			data.Select = "p.ID AS ProductsID,p.Code AS ProductsCode,p.Name AS ProductsName,p.BrandID,p.CategoryID,p.TaxRate,ps.ID AS ProductsSkuID,ps.Code AS ProductsSkuCode,ps.Saleprop AS ProductsSkuSaleprop,b.BatchCode,b.CostPrice";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<BatchItemList> list = WarehouseProductsBatchService.GetQueryManyForPageBatchItemList(data, out total);
			foreach (var item in list) {
				Brand brand = BrandService.GetSingleBrand(item.BrandID);
				Category category = CategoryService.GetSingleCategory(item.CategoryID);
				if (brand != null) {
					item.BrandName = brand.Name;
				}
				if (category != null) {
					item.CategoryName = category.Name;
				}

				DataTable dt = WarehouseOutInStockLogService.GetManyOutInStockLog(item.BatchCode, item.ProductsID, item.ProductsSkuID);
				if (dt.Rows.Count == 1) {
					item.OutboundNum = ZConvert.StrToInt(dt.Rows[0]["OutboundNum"]);
					item.StorageNum = ZConvert.StrToInt(dt.Rows[0]["StorageNum"]);
					item.AdjustQuantity = ZConvert.StrToInt(dt.Rows[0]["AdjustQuantity"]);
					item.RollOutQuantity = ZConvert.StrToInt(dt.Rows[0]["RollOutQuantity"]);
					item.QuantityOfTransfer = ZConvert.StrToInt(dt.Rows[0]["QuantityOfTransfer"]);
					item.SalesVolumes = ZConvert.StrToInt(dt.Rows[0]["SalesVolumes"]);
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
			string batchCode = ZConvert.ToString(Request["batchCode"]);

			string whereSql = " b.BatchCode = '" + batchCode + "'";

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
