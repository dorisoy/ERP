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
	public class BatchController : BaseController
    {
        //
        // GET: /Warehouse/Batch/

        public ActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// 批次情况列表
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
			data.GroupBy = "l.ProductsBatchCode";
			data.OrderBy = "b.ID DESC";
			data.From = "warehouseProductsBatch b INNER JOIN warehouseOutInStockLog l ON b.ID = l.ProductsBatchID";
			data.Select = @"b.BatchCode,
                            SUM(b.ZkNum) Inventory,
							COUNT(DISTINCT b.ProductsID) AS ProductsCount,
							MIN(b.CreateDate) AS CreateDate,
                            MIN(CASE WHEN l.BillType = " + (int)BillType.CGR + " OR l.BillType = " + (int)BillType.QTR + @" THEN 1 WHEN l.BillType = " + (int)BillType.PD + @" AND l.StockWay = " + (int)StockWay.入库 + @" THEN 2 WHEN l.BillType = " + (int)BillType.ZH + @" AND l.StockWay = " + (int)StockWay.入库 + @" THEN 3 ELSE 0 END) AS BatchSource,
						    SUM(CASE WHEN l.BillType = " + (int)BillType.CGC + " OR l.BillType = " + (int)BillType.QTC + @" THEN l.Num * l.StockWay ELSE 0 END) AS OutboundNum,
							SUM(CASE WHEN l.BillType = " + (int)BillType.CGR + " OR l.BillType = " + (int)BillType.QTR + @" THEN l.Num * l.StockWay ELSE 0 END) AS StorageNum,
							SUM(CASE WHEN l.BillType = " + (int)BillType.PD + @" THEN l.Num * l.StockWay ELSE 0 END) AS AdjustQuantity,
							SUM(CASE WHEN l.BillType = " + (int)BillType.ZH + @" AND l.StockWay = " + (int)StockWay.出库 + @" THEN l.Num ELSE 0 END) AS RollOutQuantity,
							SUM(CASE WHEN l.BillType = " + (int)BillType.ZH + @" AND l.StockWay = " + (int)StockWay.入库 + @" THEN l.Num ELSE 0 END) AS QuantityOfTransfer,
                            SUM(CASE WHEN l.BillType = " + (int)BillType.XSC + @" AND l.StockWay = " + (int)StockWay.出库 + @" THEN l.Num ELSE 0 END) AS SalesVolumes";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
		    List<BatchList> list = WarehouseProductsBatchService.GetQueryManyForPageList(data, out total);
		
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
			int batchSource = ZConvert.StrToInt(Request["batchSource"]);
			string whereSql = " b.WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "'";

			if (keyWord != "") {
				switch (keyWordType) {
					case "批次号":
						whereSql += string.Format(" and b.BatchCode like '%{0}%'", keyWord);
						break;
					case "商品名称":
						whereSql += string.Format(" and b.ProductsID in (select ID from products where Name like '%{0}%')", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and b.ProductsID in (select ID from products where Code like '%{0}%')", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and b.ProductsSkuID in (select ID from productsSku where Code like '%{0}%')", keyWord);
						break;
				}
			}
			if (batchSource > 0) {
				switch (batchSource) {
					case 1:
						whereSql += string.Format(" and (l.BillType = " + (int)BillType.CGR + " OR l.BillType = " + (int)BillType.QTR + ")");
						break;
					case 2:
						whereSql += string.Format(" and (l.BillType = " + (int)BillType.PD + @" AND l.StockWay = " + (int)StockWay.入库 + ")");
						break;
					case 3:
						whereSql += string.Format(" and (l.BillType = " + (int)BillType.ZH + @" AND l.StockWay = " + (int)StockWay.入库 + ")");
						break;
				}
			}

			return whereSql;
		}
    }
}
