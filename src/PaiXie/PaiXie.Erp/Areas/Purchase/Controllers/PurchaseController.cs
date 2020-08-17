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

namespace PaiXie.Erp.Areas.Purchase
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
			data.From = @"warehousePurchase wp
LEFT JOIN warehouse w ON wp.WarehouseCode=w.Code
LEFT JOIN suppliers supp ON wp.SuppliersID=supp.ID";
			data.Select = "wp.ID,wp.BillNo,wp.PlanID,wp.PlanBillNo,wp.WarehouseCode,w.Name AS WarehouseName,supp.AliasName,wp.Num,wp.InStockNum,wp.InStockOrderCount,wp.CreateDate,wp.CreatePerson,wp.Status";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehousePurchaseList> list = BaseService<WarehousePurchaseList>.GetQueryManyForPage(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);

		}

		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string warehouseCode = ZConvert.ToString(Request["warehouseCode"]);
			int suppliersID=ZConvert.StrToInt(Request["suppliersID"]);
			string state = ZConvert.ToString(Request["state"]);
			string whereSql = "1=1";

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
					case "商品SKU码":
						whereSql += string.Format(" and wp.ID IN (SELECT PurchaseID FROM warehousePurchaseItem Where ProductsSkuCode like '%{0}%')", keyWord);
						break;
				}
			}

			if (warehouseCode != "" && warehouseCode != "0") {
				whereSql += string.Format(" and wp.WarehouseCode = '{0}'", warehouseCode);
			}
			if (suppliersID > 0) {
				whereSql += string.Format(" and wp.SuppliersID = {0}", suppliersID);
			}
			if (state != "") {
				whereSql += string.Format(" and wp.Status IN ({0})", state);
			}
			return whereSql;
		}

		#endregion

		#region 添加采购单

		public ActionResult Add() {
			return View();
		}

		#endregion

		#region 保存采购单

		/// <summary>
		/// 保存采购单
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="suppliersID">供应商ID</param>
		/// <returns></returns>
		public ActionResult Save(string warehouseCode, int suppliersID) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = PurchaseManager.AddPurchase(userCode, warehouseCode, suppliersID);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 确认采购单

		public ActionResult Confirm(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> purchaseIDList = new List<int>();
			purchaseIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = PurchaseManager.ConfirmPurchase(userCode, purchaseIDList);
			return JsonDate(resultInfo);
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

		#region 删除采购单

		public ActionResult Delete(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> purchaseIDList = new List<int>();
			purchaseIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = PurchaseManager.DelPurchase(userCode, purchaseIDList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region  重新采购

		public ActionResult RePurchase(int purchaseID) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = PurchaseManager.RePurchase(userCode, purchaseID);
			return JsonDate(resultInfo);
		}

		#endregion
	}
}
