using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Api.Bll;
using PaiXie.Utils;

namespace PaiXie.Erp.Areas.Warehouse
{
	public class PurchasePlanController : BaseController
    {
        //
        // GET: /Warehouse/PurchasePlan/

        public ActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// 采购计划单列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string status = ZConvert.ToString(Request["status"]);
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));

			string whereSql = string.Format(" WarehouseCode = '{0}'", FormsAuth.GetWarehouseCode());

			if (keyWord != "") {
				switch (keyWordType) {
					case "计划单名称":
						whereSql += string.Format(" and Name like '%{0}%'", keyWord);
						break;
					case "采购计划单号":
						whereSql += string.Format(" and BillNo = '{0}'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and ID IN (SELECT PlanID FROM warehousePurchasePlanItem Where ProductsCode like '%{0}%')", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and ID IN (SELECT PlanID FROM warehousePurchasePlanItem Where ProductsSkuCode like '%{0}%')", keyWord);
						break;
				}
			}
			if (!string.IsNullOrEmpty(status)) {
				whereSql += string.Format(" and Status in ({0})", status);
			}

			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "ID DESC";
			data.From = "warehousePurchasePlan";
			data.Select = "ID,BillNo,Name,WarehouseCode,Num,Status,CreatePerson,CreateDate";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehousePurchasePlanList> list = BaseService<WarehousePurchasePlanList>.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		/// <summary>
		/// 添加采购计划单
		/// </summary>
		/// <returns></returns>
		public ActionResult Add() {
			return View();
		}

		/// <summary>
		/// 添加采购计划单
		/// </summary>
		/// <param name="name">计划单名称</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Save(string name) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = PurchaseManager.AddPlan(userCode, warehouseCode, name, (int)ProjectType.仓库端);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 删除采购计划单
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Delete(int id) {
			string userCode = FormsAuth.GetUserCode();
			List<int> planIDList = new List<int>() { id };
			BaseResult resultInfo = PurchaseManager.DelPlan(userCode, (int)ProjectType.仓库端, planIDList);
			return JsonDate(resultInfo);
		}
        
		/// <summary>
		/// 提交单据
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Submit(int id) {
			BaseResult resultInfo = new BaseResult();
			if (WarehousePurchasePlanItemService.GetCount(FormsAuth.GetWarehouseCode(), id, 0) == 0) {
				resultInfo.result = 0;
				resultInfo.message = "请先添加计划采购商品！";
			}
			else {
				WarehousePurchasePlan warehousePurchasePlan = WarehousePurchasePlanService.GetSingleWarehousePurchasePlan(id);
				if (warehousePurchasePlan.Status == (int)PurchasePlanStatus.未提交) {
					warehousePurchasePlan.Status = (int)PurchasePlanStatus.已提交;
					int rowsAffected = WarehousePurchasePlanService.Update(warehousePurchasePlan);
					resultInfo.result = rowsAffected;
				}
				if (resultInfo.result == 0) {
					resultInfo.result = 0;
					resultInfo.message = "操作失败！";
				}
				else {
					resultInfo.message = "操作成功！";
				}
			}
			return JsonDate(resultInfo);
		}
    }
}
