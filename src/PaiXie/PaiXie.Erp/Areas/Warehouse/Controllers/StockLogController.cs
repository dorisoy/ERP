using FluentData;
using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Data.ViewModel;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Warehouse.Controllers
{
    public class StockLogController : BaseController
    {
        //
		// GET: /Warehouse/StockLog/

        public ActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// 库位日志列表
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
			data.OrderBy = " woil.ID DESC";
			data.From = "warehouseOutInStockLog woil LEFT JOIN warehouseLocation wl ON woil.LocationID=wl.ID";
			data.Select = @"woil.ID,woil.BillType,woil.SourceNo,woil.ProductsNo,woil.ProductsName,woil.ProductsCode,woil.ProductsSkuCode,woil.ProductsSkuSaleprop,wl.`Code` AS LocationCode,woil.ProductsBatchCode,woil.Num,woil.StockWay,woil.CreatePerson,woil.CreateDate";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseOutInStockLogList> list = BaseService<WarehouseOutInStockLogList>.GetQueryManyForPage(data, out total);
			for (int i = 0; i < list.Count(); i++) {
				list[i].BillTypeName = BillTypeConvert.GetBillTypeName(list[i].BillType);
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		/// <summary>
		/// 获取单据类型
		/// </summary>
		/// <returns></returns>
		public ActionResult GetWarehouseBillTypeJson() {
			return JsonDate(EnumManager<BillType>.GetDataTable(0, true));
		}

		/// <summary>
		/// 获取搜索条件
		/// </summary>
		/// <returns></returns>
		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int billType = ZConvert.StrToInt(Request["billType"]);
			int stockWay = ZConvert.StrToInt(Request["stockWay"]);
			string startDate = ZConvert.ToString(Request["startDate"]);
			string endDate = ZConvert.ToString(Request["endDate"]);

			string whereSql = string.Format(" woil.WarehouseCode = '{0}'", FormsAuth.GetWarehouseCode());

			if (keyWord != "") {
				switch (keyWordType) {
					case "商品货号":
						whereSql += string.Format(" and woil.ProductsNo like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and woil.ProductsCode like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and woil.ProductsSkuCode like '%{0}%'", keyWord);
						break;
					case "单据编号":
						whereSql += string.Format(" and woil.SourceNo like '%{0}%'", keyWord);
						break;
				}
			}
			if (billType > 0) {
				whereSql += string.Format(" and woil.BillType = {0}", billType);
			}
			if (stockWay != 0) {
				whereSql += string.Format(" and woil.stockWay = {0}", stockWay);
			}
			if (startDate != "") {
				whereSql += string.Format(" AND woil.CreateDate >= '{0}'", startDate);
			}
			if (endDate != "") {
				whereSql += string.Format(" AND woil.CreateDate <= '{0} 23:59:59'", endDate);
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
			fileName = "库存日志(" + NewKey.datetime() + ")";
			fileMapPath = System.Web.HttpContext.Current.Server.MapPath("../../Down/" + fileName + "");
			string ids = ZConvert.ToString(Request["ids"]);
			string startDate = ZConvert.ToString(Request["startDate"]);
			string endDate = ZConvert.ToString(Request["endDate"]);

			string strSql = (ids == "" || ids ==  "0" ) ? GetWhereSql() : "woil.ID IN (" + ids + ")";
			string json = "";
			IDictionary<string, string> dicts = new Dictionary<string, string>();
			dicts.Add("fileName", fileName);
			dicts.Add("fileMapPath", fileMapPath);
			dicts.Add("downTaskId", downTaskId);
			dicts.Add("filter", strSql);
			dicts.Add("reportName", "库存日志(" + startDate + "至" + endDate + ")");

			Common.RunAsyn(obj => { ExportTask((IDictionary<string, string>)obj); }, dicts);
			json = Newtonsoft.Json.JsonConvert.SerializeObject(dicts.Where(dic => { return dic.Key != "filter" && dic.Key != "reportName"; }).ToDictionary(data => data.Key, data => data.Value));
			return json;
		}

		protected void ExportTask(IDictionary<string, string> dicts) {
			string fileName = dicts["fileName"];
			string fileMapPath = dicts["fileMapPath"];
			string downTaskId = dicts["downTaskId"];
			string filter = dicts["filter"];
			string reportName = dicts["reportName"];

			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					string format = "BillTypeName|单据类型;SourceNo|单据编号;ProductsName|商品名称;ProductsCode|商品编码;ProductsSkuCode|商品SKU码;ProductsSkuSaleprop|商品属性;LocationCode|库位编码;ProductsBatchCode|批次号;Num|数量;StockWayName|出入方向;CreatePerson|创建人;CreateDate|创建时间";
					
					SelectBuilder data = new SelectBuilder();
					data.Having = "";
					data.GroupBy = "";
					data.OrderBy = " woil.ID DESC";
					data.From = "warehouseOutInStockLog woil LEFT JOIN warehouseLocation wl ON woil.LocationID=wl.ID";
					data.Select = @"woil.BillType,woil.SourceNo,woil.ProductsName,woil.ProductsCode,woil.ProductsSkuCode,woil.ProductsSkuSaleprop,wl.`Code` AS LocationCode,woil.ProductsBatchCode,woil.Num,woil.StockWay,woil.CreatePerson,woil.CreateDate,'000000000' as BillTypeName,'000000000' as StockWayName";
					data.WhereSql = filter;
					data.PagingCurrentPage = 0;
					data.PagingItemsPerPage = 0;
					DataTable exportTable = WarehouseOutInStockLogService.GetDataTableForPage(data, context);
					for (int i = 0; i < exportTable.Rows.Count; i++) {
						exportTable.Rows[i]["BillTypeName"] = BillTypeConvert.GetBillTypeName(ZConvert.StrToInt(exportTable.Rows[i]["BillType"]));
						exportTable.Rows[i]["StockWayName"] = ZConvert.StrToInt(exportTable.Rows[i]["StockWay"]) == 1 ? "入库" : "出库";
					}
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
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "导出库存日志", FormsAuth.GetUserCode());
			}
		}

		#endregion

    }
}
