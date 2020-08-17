using FluentData;
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
	public class StockController : BaseController
    {
        //
        // GET: /Warehouse/Stock/

        public ActionResult Index()
        {
			ViewBag.StartDate = ZDateTime.DayOfMonth(DateTime.Now, true).ToString("yyyy-MM-dd");
			ViewBag.EndDate = ZDateTime.GetDate();
            return View();
        }

		/// <summary>
		/// 库存情况列表
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
			data.From = "products p inner join warehouseProducts wp on p.ID = wp.ProductsID";
			data.Select = "wp.ID,p.Code,p.Name,p.BrandID,p.CategoryID,p.TaxRate";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<StockList> list = BaseService<StockList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				Brand brand = BrandService.GetSingleBrand(item.BrandID);
				Category category = CategoryService.GetSingleCategory(item.CategoryID);
				if (brand != null) {
					item.BrandName = brand.Name;
				}
				if (category != null) {
					item.CategoryName = category.Name;
				}
				DataTable initialDt = WarehouseOutInStockLogService.GetInitialOutInStockLog(FormsAuth.GetWarehouseCode(), item.ID, 0, startDate, "");
				if (initialDt.Rows.Count == 1) {
					item.InitialInventory = ZConvert.StrToInt(initialDt.Rows[0]["InitialInventory"]);
					item.InitialCost = ZConvert.StrToDecimal(initialDt.Rows[0]["InitialCost"]);
				}


				DataTable finalDt = WarehouseOutInStockLogService.GetInitialOutInStockLog(FormsAuth.GetWarehouseCode(), item.ID, 0, "", endDate);
				if (finalDt.Rows.Count == 1) {
					item.FinalQuantity = ZConvert.StrToInt(finalDt.Rows[0]["InitialInventory"]);
					item.FinalCost = ZConvert.StrToDecimal(finalDt.Rows[0]["InitialCost"]);
				}

				DataTable dt = WarehouseOutInStockLogService.GetManyOutInStockLog(FormsAuth.GetWarehouseCode(), item.ID, 0, startDate, endDate);
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
						whereSql += string.Format(" and p.ID in (select ProductsID from productsSku where Code like '%{0}%')", keyWord);
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

		#region 导出

		/// <summary>
		/// 初始化参数
		/// </summary>
		public string Export() {
			string fileName, fileMapPath, downTaskId;
			downTaskId = NewKey.guid();
			fileName = "库存情况(" + NewKey.datetime() + ")";
			fileMapPath = System.Web.HttpContext.Current.Server.MapPath("../../Down/" + fileName + "");
			string ids = ZConvert.ToString(Request["ids"]);
			string startDate = ZConvert.ToString(Request["startDate"]);
			string endDate = ZConvert.ToString(Request["endDate"]);

			string strSql = (ids == "" || ids == "0") ? GetWhereSql() : "wp.ID IN (" + ids + ")";
			string json = "";
			IDictionary<string, string> dicts = new Dictionary<string, string>();
			dicts.Add("fileName", fileName);
			dicts.Add("fileMapPath", fileMapPath);
			dicts.Add("downTaskId", downTaskId);
			dicts.Add("filter", strSql);
			dicts.Add("reportName", "库存情况(" + startDate + "至" + endDate + ")");

			Common.RunAsyn(obj => { ExportTask((IDictionary<string, string>)obj); }, dicts);
			json = Newtonsoft.Json.JsonConvert.SerializeObject(dicts.Where(dic => { return dic.Key != "filter" && dic.Key != "reportName"; }).ToDictionary(data => data.Key, data => data.Value));
			return json;
		}

		protected void ExportTask(IDictionary<string, string> dicts) {
			string startDate = ZConvert.ToString(Request["startDate"]);
			string endDate = ZConvert.ToString(Request["endDate"]);
			string fileName = dicts["fileName"];
			string fileMapPath = dicts["fileMapPath"];
			string downTaskId = dicts["downTaskId"];
			string filter = dicts["filter"];
			string reportName = dicts["reportName"];

			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					string format = "Name|商品名称;Code|商品编码;CategoryName|分类;BrandName|品牌;TaxRateConvert|税率;InitialInventory|期初库存;InitialCost|期初成本;OutboundNum|出库数量;StorageNum|入库数量;SalesVolumes|销售数量;SalesAmount|销售金额;AdjustQuantity|调整数量;RollOutQuantity|转出数量;QuantityOfTransfer|转入数量;FinalQuantity|期末数量;FinalCost|期末成本";

					SelectBuilder data = new SelectBuilder();
					data.Having = "";
					data.GroupBy = "";
					data.OrderBy = "";
					data.From = "products p inner join warehouseProducts wp on p.ID = wp.ProductsID";
					data.Select = "wp.ID,p.Code,p.Name,p.BrandID,p.CategoryID,p.TaxRate";
					data.WhereSql = filter;
					data.PagingCurrentPage = 0;
					data.PagingItemsPerPage = 0;
					int total = 0;
					List<StockList> list = BaseService<StockList>.GetQueryManyForPage(data, out total);
					foreach (var item in list) {
						Brand brand = BrandService.GetSingleBrand(item.BrandID);
						Category category = CategoryService.GetSingleCategory(item.CategoryID);
						if (brand != null) {
							item.BrandName = brand.Name;
						}
						if (category != null) {
							item.CategoryName = category.Name;
						}

						item.TaxRateConvert = ZConvert.StrToInt(item.TaxRate * 100) + "%";
						
						DataTable initialDt = WarehouseOutInStockLogService.GetInitialOutInStockLog(FormsAuth.GetWarehouseCode(), item.ID, 0, startDate, "");
						if (initialDt.Rows.Count == 1) {
							item.InitialInventory = ZConvert.StrToInt(initialDt.Rows[0]["InitialInventory"]);
							item.InitialCost = ZConvert.StrToDecimal(initialDt.Rows[0]["InitialCost"]);
						}


						DataTable finalDt = WarehouseOutInStockLogService.GetInitialOutInStockLog(FormsAuth.GetWarehouseCode(), item.ID, 0, "", endDate);
						if (finalDt.Rows.Count == 1) {
							item.FinalQuantity = ZConvert.StrToInt(finalDt.Rows[0]["InitialInventory"]);
							item.FinalCost = ZConvert.StrToDecimal(finalDt.Rows[0]["InitialCost"]);
						}

						DataTable dt = WarehouseOutInStockLogService.GetManyOutInStockLog(FormsAuth.GetWarehouseCode(), item.ID, 0, startDate, endDate);
						if (dt.Rows.Count == 1) {
							item.OutboundNum = ZConvert.StrToInt(dt.Rows[0]["OutboundNum"]);
							item.StorageNum = ZConvert.StrToInt(dt.Rows[0]["StorageNum"]);
							item.AdjustQuantity = ZConvert.StrToInt(dt.Rows[0]["AdjustQuantity"]);
							item.RollOutQuantity = ZConvert.StrToInt(dt.Rows[0]["RollOutQuantity"]);
							item.QuantityOfTransfer = ZConvert.StrToInt(dt.Rows[0]["QuantityOfTransfer"]);
						}
					}
					DataTable exportTable = ZConvert.ListToDataTable(list, "StockList");					
					if (exportTable.Rows.Count > 60000) {
						fileName += ".csv";
						fileMapPath += ".csv";
					}
					else {
						fileMapPath += ".xls";
						fileName += ".xls";
					}

					Excel.ExcelHelp.exportMin.GenerateXlsFormat(format, fileMapPath, exportTable, reportName); GC.Collect();
				}
			}
			catch (Exception ex) {
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "导出库存情况", FormsAuth.GetUserCode());
			}
		}

		#endregion
    }
}
