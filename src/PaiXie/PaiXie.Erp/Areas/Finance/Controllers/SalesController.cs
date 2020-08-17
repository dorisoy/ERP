using FluentData;
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

namespace PaiXie.Erp.Areas.Finance.Controllers {
	public class SalesController : BaseController {
		//
		// GET: /Finance/Sales/

		public ActionResult Index() {
			ViewBag.StartDate = ZDateTime.DayOfMonth(DateTime.Now, true).ToString("yyyy-MM-dd");
			ViewBag.EndDate = DateTime.Now.AddMinutes(-30);
			return View();
		}

		/// <summary>
		/// 订单列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "BillDate DESC";
			data.From = @"(SELECT CONCAT('X',obpi.ID) AS ID,1 AS BillType,ob.BillNo,ob.DeliveryDate AS BillDate,i.ProductsCode,i.ProductsNo,i.ProductsName,i.CategoryID,i.CategoryName,i.BrandID,i.BrandName,i.ProductsSkuSaleprop,i.ProductsSkuCode,i.ProductsNum,(i.ProductsNum * i.ActualSellingPrice) ProductsAmount,i.TaxRate,obpi.ProductsBatchID,obpi.ProductsBatchCode FROM warehouseOutbound ob INNER JOIN warehouseOutboundPickItem obpi ON ob.ID = obpi.OutboundID INNER JOIN ord_item i ON obpi.OrdItemID = i.ID  WHERE STATUS = " + (int)WarehouseOutboundStatus.已发货 + @"
                          UNION ALL
                          SELECT CONCAT('S',i.ID) AS ID,-1 AS BillType,r.BillNo,r.ReceiveDate AS BillDate,i.ProductsCode,i.ProductsNo,i.ProductsName,i.CategoryID,i.CategoryName,i.BrandID,i.BrandName,i.ProductsSkuSaleprop,i.ProductsSkuCode,ri.RefundNum ProductsNum,(i.ProductsNum * i.ActualSellingPrice) ProductsAmount,i.TaxRate,ri.ProductsBatchID,ri.ProductsBatchCode FROM ord_refund r INNER JOIN ord_refundItem ri ON r.ID = ri.OrdRefundID INNER JOIN ord_item i ON ri.OrdItemID = i.ID WHERE STATUS = " + (int)OrdRefundStatus.已完成 + @"
                          ) AS t INNER JOIN warehouseProductsBatch wpb ON t.ProductsBatchID = wpb.ID ";
			data.Select = "t.ID,t.BillType,t.BillNo,t.BillDate,t.ProductsCode,t.ProductsName,t.CategoryName,t.BrandName,t.ProductsSkuSaleprop,t.ProductsSkuCode,t.ProductsNum,t.ProductsAmount,t.TaxRate,t.ProductsBatchCode,wpb.CostPrice";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<SalesList> list = BaseService<SalesList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				item.BillDate = ZConvert.StrToDateTime(item.BillDate, DateTime.Now).ToString("yyyy-MM-dd");
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		/// <summary>
		/// 获取查询条件
		/// </summary>
		/// <returns></returns>
		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int categoryID = ZConvert.StrToInt(Request["categoryID"]);
			int brandID = ZConvert.StrToInt(Request["brandID"]);
			string startDate = ZConvert.ToString(Request["startDate"]);
			string EndDate = ZConvert.ToString(Request["endDate"]);

			string whereSql = string.Format(" BillDate >= '{0}' and BillDate <= '{1} 23:59:59'", startDate, EndDate);

			if (keyWord != "") {
				switch (keyWordType) {
					case "商品编码":
						whereSql += string.Format(" and ProductsCode like '%{0}%'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and ProductsNo like '%{0}%'", keyWord);
						break;
					case "商品名称":
						whereSql += string.Format(" and ProductsName like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and ProductsSkuCode like '%{0}%", keyWord);
						break;
					case "关联单号":
						whereSql += string.Format(" and BillNo like '%{0}%'", keyWord);
						break;
				}
			}

			if (categoryID > 0) {
				whereSql += string.Format(" and CategoryID={0}", categoryID);
			}
			if (brandID > 0) {
				whereSql += string.Format(" and BrandID={0}", brandID);
			}

			return whereSql;
		}

		#region 导出销售明细

		/// <summary>
		/// 初始化参数
		/// </summary>
		public string Export() {
			string fileName, fileMapPath, downTaskId;
			downTaskId = NewKey.guid();
			fileName = "销售明细(" + NewKey.datetime() + ")";
			fileMapPath = System.Web.HttpContext.Current.Server.MapPath("../../Down/" + fileName + "");
			string ids = ZConvert.ToString(Request["ids"]);
			string startDate = ZConvert.ToString(Request["startDate"]);
			string EndDate = ZConvert.ToString(Request["endDate"]);
			string strSql = ids == "" ? GetWhereSql() : " FIND_IN_SET(t.ID, '" + ids + "')";
			string json = "";
			IDictionary<string, string> dicts = new Dictionary<string, string>();
			dicts.Add("fileName", fileName);
			dicts.Add("fileMapPath", fileMapPath);
			dicts.Add("downTaskId", downTaskId);
			dicts.Add("filter", strSql);
			dicts.Add("reportName", "销售明细(" + startDate + "至" + EndDate + ")");

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
					string format = "ProductsName|商品名称;ProductsCode|商品编码;CategoryName|分类;BrandName|品牌;TaxRate|税率;ProductsSkuSaleprop|商品属性;ProductsSkuCode|商品SKU码;BillType|类型;ProductsNum|数量;ProductsAmount|销售额;ProductsBatchCode|批次;CostPrice|成本价;BillNo|单据号;BillDate|发货时间";
					DataTable exportTable = Common.CreateCustomTable("", "BillType,BillNo,BillDate,ProductsCode,ProductsName,CategoryName,BrandName,ProductsSkuSaleprop,ProductsSkuCode,ProductsNum,ProductsAmount,TaxRate,ProductsBatchCode,CostPrice");
					SelectBuilder data = new SelectBuilder();
					data.Having = "";
					data.GroupBy = "";
					data.OrderBy = "BillDate DESC";
					data.From = @"(SELECT CONCAT('X',obpi.ID) AS ID,1 AS BillType,ob.BillNo,ob.DeliveryDate AS BillDate,i.ProductsCode,i.ProductsNo,i.ProductsName,i.CategoryID,i.CategoryName,i.BrandID,i.BrandName,i.ProductsSkuSaleprop,i.ProductsSkuCode,i.ProductsNum,(i.ProductsNum * i.ActualSellingPrice) ProductsAmount,i.TaxRate,obpi.ProductsBatchID,obpi.ProductsBatchCode FROM warehouseOutbound ob INNER JOIN warehouseOutboundPickItem obpi ON ob.ID = obpi.OutboundID INNER JOIN ord_item i ON obpi.OrdItemID = i.ID  WHERE STATUS = " + (int)WarehouseOutboundStatus.已发货 + @"
								  UNION ALL
								  SELECT CONCAT('S',i.ID) AS ID,-1 AS BillType,r.BillNo,r.ReceiveDate AS BillDate,i.ProductsCode,i.ProductsNo,i.ProductsName,i.CategoryID,i.CategoryName,i.BrandID,i.BrandName,i.ProductsSkuSaleprop,i.ProductsSkuCode,ri.RefundNum ProductsNum,(i.ProductsNum * i.ActualSellingPrice) ProductsAmount,i.TaxRate,ri.ProductsBatchID,ri.ProductsBatchCode FROM ord_refund r INNER JOIN ord_refundItem ri ON r.ID = ri.OrdRefundID INNER JOIN ord_item i ON ri.OrdItemID = i.ID WHERE STATUS = " + (int)OrdRefundStatus.已完成 + @"
								  ) AS t INNER JOIN warehouseProductsBatch wpb ON t.ProductsBatchID = wpb.ID ";
					data.Select = "t.BillType,t.BillNo,t.BillDate,t.ProductsCode,t.ProductsName,t.CategoryName,t.BrandName,t.ProductsSkuSaleprop,t.ProductsSkuCode,t.ProductsNum,t.ProductsAmount,t.TaxRate,t.ProductsBatchCode,wpb.CostPrice";
					data.WhereSql = filter;
					data.PagingCurrentPage = 0;
					data.PagingItemsPerPage = 0;
					DataTable dt = WarehouseStocktakingItemService.GetDataTableForPage(data, context);
					foreach (DataRow dr in dt.Rows) {
						DataRow erportDr = exportTable.NewRow();
						erportDr["BillNo"] = dr["BillNo"];
						erportDr["BillDate"] = dr["BillDate"];
						erportDr["ProductsCode"] = dr["ProductsCode"];
						erportDr["ProductsName"] = dr["ProductsName"];
						erportDr["CategoryName"] = dr["CategoryName"];
						erportDr["BrandName"] = dr["BrandName"];
						erportDr["ProductsSkuSaleprop"] = dr["ProductsSkuSaleprop"];
						erportDr["ProductsSkuCode"] = dr["ProductsSkuCode"];
						erportDr["ProductsNum"] = dr["ProductsNum"];
						erportDr["ProductsBatchCode"] = dr["ProductsBatchCode"];
						erportDr["CostPrice"] = dr["CostPrice"];
						erportDr["CostPrice"] = dr["CostPrice"];

						if (ZConvert.StrToInt(dr["BillType"]) == 1) {
							erportDr["BillType"] = "销售";
						}
						else {
							erportDr["BillType"] = "售后";
						}
						erportDr["ProductsAmount"] = ZConvert.StrToDecimal(dr["ProductsAmount"]).ToString("F3");
						erportDr["TaxRate"] = (ZConvert.StrToDecimal(dr["TaxRate"]) * 100) + "%";
						exportTable.Rows.Add(erportDr);

						PaiXie.Utils.Export.add_Down_Task_Progress(downTaskId);
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
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "导出销售明细", FormsAuth.GetUserCode());
			}
		}

		#endregion
	}
}
