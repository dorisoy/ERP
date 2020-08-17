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

namespace PaiXie.Erp.Areas.Warehouse
{
    public class WasteBinController : BaseController
    {
        //
        // GET: /Warehouse/WasteBin/

        public ActionResult Index()
        {
			WarehouseLocation warehouseLocation = WarehouseLocationService.GetSingleSubWarehouseLocation(FormsAuth.GetWarehouseCode(), (int)LocationType.废品区);
			ViewBag.WarehouseLocation = warehouseLocation;
			ViewBag.ProductsNum = WarehouseLocationProductsService.GetProductsNum(warehouseLocation.ParentID);
            return View();
        }

		/// <summary>
		/// 商品列表
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
			data.From = "warehouseLocationProducts w inner join productsSku ps on w.ProductsSkuID = ps.ID inner join products p on ps.ProductsID = p.ID LEFT JOIN warehouseLocation wl ON w.LocationID=wl.ID";
			data.Select = "p.Code AS ProductsCode,p.Name,ps.ID,ps.Saleprop,ps.Code AS ProductsSkuCode,w.ProductsBatchCode,ZkNum,wl.`Code` AS LocationCode";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseLocationProductsList> list = BaseService<WarehouseLocationProductsList>.GetQueryManyForPage(data, out total);
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
			int locationID = ZConvert.StrToInt(Request["locationID"]);

			string whereSql = " w.WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "' and LocationID = " + locationID;

			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and p.Name like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and p.Code = '{0}'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and p.No = '{0}'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and ps.Code = '{0}'", keyWord);
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
			fileName = "废品区(" + NewKey.datetime() + ")";
			fileMapPath = System.Web.HttpContext.Current.Server.MapPath("../../Down/" + fileName + "");
			string ids = ZConvert.ToString(Request["ids"]);

			string strSql = (ids == "" || ids == "0") ? GetWhereSql() : "ps.ID IN (" + ids + ")";
			string json = "";
			IDictionary<string, string> dicts = new Dictionary<string, string>();
			dicts.Add("fileName", fileName);
			dicts.Add("fileMapPath", fileMapPath);
			dicts.Add("downTaskId", downTaskId);
			dicts.Add("filter", strSql);
			dicts.Add("reportName", "废品区");

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
					string format = "Name|商品名称;ProductsCode|商品编码;Saleprop|商品属性;ProductsSkuCode|商品SKU码;ProductsBatchCode|批次;ZkNum|数量;LocationCode|库位编码";

					SelectBuilder data = new SelectBuilder();
					data.Having = "";
					data.GroupBy = "";
					data.OrderBy = "";
					data.From = "warehouseLocationProducts w inner join productsSku ps on w.ProductsSkuID = ps.ID inner join products p on ps.ProductsID = p.ID LEFT JOIN warehouseLocation wl ON w.LocationID=wl.ID";
					data.Select = "p.Code AS ProductsCode,p.Name,ps.ID,ps.Saleprop,ps.Code AS ProductsSkuCode,w.ProductsBatchCode,ZkNum,wl.`Code` AS LocationCode";
					data.WhereSql = filter;
					data.PagingCurrentPage = 0;
					data.PagingItemsPerPage = 0;
					int total = 0;
					List<WarehouseLocationProductsList> list = BaseService<WarehouseLocationProductsList>.GetQueryManyForPage(data, out total);
					DataTable exportTable = ZConvert.ListToDataTable(list, "WarehouseLocationProductsList");
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
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "导出废品区", FormsAuth.GetUserCode());
			}
		}

		#endregion
    }
}
