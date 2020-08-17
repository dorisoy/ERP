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
    public class PurchaseController : BaseController
    {
		#region Index

		public ActionResult Index() {
			return View();
		}

		#endregion

		#region 采购单列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wp.ID DESC";
			data.From = @"warehousePurchase wp LEFT JOIN suppliers supp ON wp.SuppliersID = supp.ID";
			data.Select = "wp.ID,wp.BillNo,wp.PlanID,wp.PlanBillNo,wp.WarehouseCode,wp.Num,wp.InStockNum,wp.InStockOrderCount,wp.CreateDate,wp.CreatePerson,wp.Status,supp.AliasName";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehousePurchaseList> list = BaseService<WarehousePurchaseList>.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);

		}

		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int suppliersID=ZConvert.StrToInt(Request["suppliersID"]);
			string status = ZConvert.ToString(Request["status"]);

			string whereSql = string.Format("wp.WarehouseCode = '{0}' and Status > 0", FormsAuth.GetWarehouseCode());

			if (keyWord != "") {
				switch (keyWordType) {
					case "采购计划单号":
						whereSql += string.Format(" and wp.PlanBillNo like '%{0}%'", keyWord);
						break;
					case "采购单号":
						whereSql += string.Format(" and wp.BillNo like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and wp.ID IN (SELECT PurchaseID FROM warehousePurchaseItem Where ProductsCode like '%{0}%')", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and wp.ID IN (SELECT PurchaseID FROM warehousePurchaseItem Where ProductsNo like '%{0}%')", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and wp.ID IN (SELECT PurchaseID FROM warehousePurchaseItem Where ProductsSkuCode like '%{0}%')", keyWord);
						break;
				}
			}

			if (suppliersID > 0) {
				whereSql += string.Format(" and wp.SuppliersID = {0}", suppliersID);
			}
			if (status != "") {
				whereSql += string.Format(" and wp.Status IN ({0})", status);
			}
			return whereSql;
		}

		#endregion

		#region 结束采购单

		public ActionResult End(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> purchaseIDList = new List<int>();
			purchaseIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = PurchaseManager.EndPurchase(userCode, purchaseIDList);
			return JsonDate(resultInfo);
		}

		#endregion
    }
}
