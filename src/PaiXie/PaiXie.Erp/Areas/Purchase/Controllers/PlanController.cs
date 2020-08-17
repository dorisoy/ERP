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
    public class PlanController : BaseController {

		#region Index

		public ActionResult Index()
        {
            return View();
		}

		#endregion

		#region 采购计划单列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wpp.ID DESC";
			data.From = @"warehousePurchasePlan wpp
LEFT JOIN warehouse w ON wpp.WarehouseCode=w.Code";
			data.Select = "wpp.ID,wpp.BillNo,wpp.Name,wpp.WarehouseCode,w.Name AS WarehouseName,wpp.Num,wpp.PurchasedNum,wpp.PurchaseOrderCount,wpp.CreateDate,wpp.CreatePerson,wpp.Status";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehousePurchasePlanList> list = BaseService<WarehousePurchasePlanList>.GetQueryManyForPage(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);

		}

		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string warehouseCode = ZConvert.ToString(Request["warehouseCode"]);
			int noPurchase = ZConvert.StrToInt(Request["noPurchase"]);
			int purchased = ZConvert.StrToInt(Request["purchased"]);
			int end = ZConvert.StrToInt(Request["end"]);
			string whereSql = string.Format("wpp.Status>{0}", (int)PurchasePlanStatus.未提交);

			if (keyWord != "") {
				switch (keyWordType) {
					case "计划单名称":
						whereSql += string.Format(" and wpp.Name like '%{0}%'", keyWord);
						break;
					case "采购计划单号":
						whereSql += string.Format(" and wpp.BillNo like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and wpp.ID IN (SELECT PlanID FROM warehousePurchasePlanItem Where ProductsCode like '%{0}%')", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and wpp.ID IN (SELECT PlanID FROM warehousePurchasePlanItem Where ProductsSkuCode like '%{0}%')", keyWord);
						break;
				}
			}

			if (warehouseCode != "" && warehouseCode != "0") {
				whereSql += string.Format(" and wpp.WarehouseCode = '{0}'", warehouseCode);
			}

			if (noPurchase == 1 && purchased == 1 && end == 1) {
				//三个都勾选，则显示已提交和已结束
			}
			else if (noPurchase == 1 && purchased == 1) {
				whereSql += string.Format(" and wpp.Status={0}", (int)PurchasePlanStatus.已提交);
			}
			else if (noPurchase == 1 && end == 1) {
				whereSql += string.Format(" and (wpp.PurchaseOrderCount=0 or wpp.Status={0})", (int)PurchasePlanStatus.已结束);
			}
			else if (purchased == 1 && end == 1) {
				whereSql += string.Format(" and (wpp.PurchaseOrderCount>0 or wpp.Status={0})", (int)PurchasePlanStatus.已结束);
			}
			else if (noPurchase == 1 && end == 0) {
				whereSql += string.Format(" and wpp.PurchaseOrderCount=0");
			}
			else if (purchased == 1 && end == 0) {
				whereSql += string.Format(" and wpp.PurchaseOrderCount>0");
			}
			else if (noPurchase == 0 && purchased == 0 && end == 1) {
				whereSql += string.Format(" and wpp.Status={0}", (int)PurchasePlanStatus.已结束);
			}
			return whereSql;
		}

		#endregion

		#region 添加采购计划单

		public ActionResult Add() {
			return View();
		}

		#endregion

		#region 保存采购计划单

		/// <summary>
		/// 保存采购计划单
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="name">采购计划单名称</param>
		/// <returns></returns>
		public ActionResult Save(string warehouseCode, string name) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = PurchaseManager.AddPlan(userCode, warehouseCode, name, (int)ProjectType.管理端);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 删除采购计划单

		public ActionResult Delete(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> planIDList = new List<int>();
			planIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = PurchaseManager.DelPlan(userCode, (int)ProjectType.管理端, planIDList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 结束采购计划单

		public ActionResult End(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> planIDList = new List<int>();
			planIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = PurchaseManager.EndPlan(userCode, planIDList);
			return JsonDate(resultInfo);
		}

		#endregion
	}
}
