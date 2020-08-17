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
	public class LocationProductsController : BaseController
    {
		#region Index

		public ActionResult Index(int parentID=0) {
			ViewBag.ParentID = parentID;
			WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(parentID);
			ViewBag.WarehouseLocation = warehouseLocation;
			ViewBag.LocationNum = WarehouseLocationService.GetLocationNum(parentID);
			ViewBag.ProductsNum = WarehouseLocationProductsService.GetProductsNum(parentID);
			return View();
		}

		#endregion

		#region 某个库区下的库位商品列表

		/// <summary>
		/// 某个库区下的库位商品列表
		/// </summary>
		/// <param name="parentID">库区ID</param>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();

			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wl.Seq,wl.Code ASC,wlp.ProductsID DESC";
			data.From = @"warehouseLocation wl LEFT JOIN 
(SELECT LocationID, ProductsID, ProductsSkuID,SUM(ZkNum) AS ZkNum FROM warehouseLocationProducts GROUP BY LocationID, ProductsID, ProductsSkuID) wlp
			ON wl.ID=wlp.LocationID
			LEFT JOIN products p ON wlp.ProductsID=p.ID
			LEFT JOIN productsSku ps ON wlp.ProductsSkuID=ps.ID";
			data.Select = @"wl.ID,wl.Code,wl.Name,p.Name AS ProductsName,p.Code AS ProductsCode,ps.Saleprop,ps.Code AS ProductsSkuCode,IFNULL(wlp.ZkNum,0) AS ZkNum";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseLocationProductsList> list = BaseService<WarehouseLocationProductsList>.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		private string GetWhereSql() {
			int parentID = ZConvert.StrToInt(Request["parentID"]);
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int categoryID = ZConvert.StrToInt(Request["categoryID"]);
			int brandID = ZConvert.StrToInt(Request["brandID"]);
			int showEmptyLocation = ZConvert.StrToInt(Request["showEmptyLocation"]);
			string whereSql = string.Format("wl.ParentID={0}", parentID);

			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and p.Name like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and p.Code like '%{0}%'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and p.No like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and ps.Code like '%{0}%'", keyWord);
						break;
					case "库位编码":
						whereSql += string.Format(" and wl.Code like '%{0}%'", keyWord);
						break;
					case "库位名称":
						whereSql += string.Format(" and wl.Name like '%{0}%'", keyWord);
						break;
				}
			}

			if (categoryID > 0) {
				whereSql += string.Format(" and p.CategoryID={0}", categoryID);
			}
			if (brandID > 0) {
				whereSql += string.Format(" and p.BrandID={0}", brandID);
			}
			if (showEmptyLocation > 0) {
				whereSql += string.Format(" and p.code<>{0}", "''");
			}
			return whereSql;
		}

		#endregion
	}
}
