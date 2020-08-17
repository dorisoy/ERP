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

namespace PaiXie.Erp.Areas.Finance
{
	public class ExpressCostController : BaseController {
		
		#region Index

		public ActionResult Index()
        {
            return View();
		}

		#endregion

		#region 快递费用列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wob.DeliveryDate,wob.ID DESC";
			data.From = @" warehouseOutbound wob
			LEFT JOIN warehouse w ON w.Code = wob.WarehouseCode
			LEFT JOIN warehouseExpress we ON we.ID = wob.DeliveryExpressID";
			data.Select = @"wob.ID, wob.BillNo, wob.ErpOrderCode, w.Name AS WarehouseName, we.Name AS ExpressName, wob.WaybillNo, wob.ExpressFreight, wob.BuyCodFee, wob.DeliveryDate";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<ExpressCostList> list = BaseService<ExpressCostList>.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		private string GetWhereSql(){
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int logisticsID = ZConvert.StrToInt(Request["logisticsID"]);
			string warehouseCode = ZConvert.ToString(Request["warehouseCode"]);
			string startDate = ZConvert.ToString(Request["startDate"]);
			string endDate = ZConvert.ToString(Request["endDate"]);
			string whereSql = string.Format("wob.Status={0}", (int)WarehouseOutboundStatus.已发货);
			if (keyWord != "") {
				switch (keyWordType) {
					case "出库单号":
						whereSql += string.Format(" AND wob.BillNo like '%{0}%'", keyWord);
						break;
					case "订单编号":
						whereSql += string.Format(" AND wob.ErpOrderCode like '%{0}%'", keyWord);
						break;
					case "运单号":
						whereSql += string.Format(" AND wob.WaybillNo like '%{0}%'", keyWord);
						break;
				}
			}
			if (logisticsID > 0) {
				whereSql += string.Format(" AND wob.DeliveryExpressID IN (SELECT ID FROM warehouseExpress WHERE LogisticsID={0})", logisticsID);
			}
			if (warehouseCode != "" && warehouseCode != "0") {
				whereSql += string.Format(" AND wob.WarehouseCode = '{0}'", warehouseCode);
			}
			if (startDate != "") {
				whereSql += string.Format(" AND wob.DeliveryDate >= '{0}'", startDate);
			}
			if (endDate != "") {
				whereSql += string.Format(" AND wob.DeliveryDate <= '{0} 23:59:59'", endDate);
			}
			return whereSql;
		}

		#endregion

		#region 获取汇总金额

		public ActionResult GetTotalAmount() {
			string sqlStr = @"SELECT Sum(ExpressFreight) as TotalExpressFreight,Sum(BuyCodFee) as TotalBuyCodFee FROM warehouseOutbound wob WHERE " + GetWhereSql();
			TotalInfo totalInfo = BaseService<TotalInfo>.GetQuerySingle(sqlStr);
			return JsonDate(totalInfo);
		}

		#endregion

		#region 导出

		/// <summary>
		/// 初始化参数
		/// </summary>
		public string Export() {
			string fileName, fileMapPath, downTaskId;
			downTaskId = NewKey.guid();
			fileName = "快递费用(" + NewKey.datetime() + ")";
			fileMapPath = System.Web.HttpContext.Current.Server.MapPath("../../Down/" + fileName + "");
			string ids = ZConvert.ToString(Request["ids"]);
			string startDate = ZConvert.ToString(Request["startDate"]);
			string endDate = ZConvert.ToString(Request["endDate"]);

			string strSql = ids == "" ? GetWhereSql() : "wob.ID IN (" + ids + ")";
			string json = "";
			IDictionary<string, string> dicts = new Dictionary<string, string>();
			dicts.Add("fileName", fileName);
			dicts.Add("fileMapPath", fileMapPath);
			dicts.Add("downTaskId", downTaskId);
			dicts.Add("filter", strSql);
			dicts.Add("reportName", "快递费用(" + startDate + "至" + endDate + ")");

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
					string format = "BillNo|出库单号;ErpOrderCode|订单号;WarehouseName|发货仓库;ExpressName|发货快递;WaybillNo|运单号;ExpressFreight|运费;BuyCodFee|手续费;DeliveryDate|发货时间";
					SelectBuilder data = new SelectBuilder();
					data.Having = "";
					data.GroupBy = "";
					data.OrderBy = "wob.DeliveryDate,wob.ID DESC";
					data.From = @" warehouseOutbound wob
					LEFT JOIN warehouse w ON w.Code = wob.WarehouseCode
					LEFT JOIN warehouseExpress we ON we.ID = wob.DeliveryExpressID";
					data.Select = @"wob.BillNo, wob.ErpOrderCode, w.Name AS WarehouseName, we.Name AS ExpressName, wob.WaybillNo, wob.ExpressFreight, wob.BuyCodFee, wob.DeliveryDate";
					data.WhereSql = filter;
					data.PagingCurrentPage = 0;
					data.PagingItemsPerPage = 0;
					DataTable exportTable = WarehouseOutboundService.GetDataTableForPage(data, context);
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
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "导出快递费用", FormsAuth.GetUserCode());
			}
		}

		#endregion
	}

	public class TotalInfo {
		public decimal TotalExpressFreight { get; set; }
		public decimal TotalBuyCodFee { get; set; }
	}
}
