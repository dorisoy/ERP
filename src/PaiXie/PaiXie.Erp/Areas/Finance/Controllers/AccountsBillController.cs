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

namespace PaiXie.Erp.Areas.Finance.Controllers {
	public class AccountsBillController : BaseController {
		//
		// GET: /Order/AccountsBill/

		public ActionResult Index() {
			ViewBag.StartDate = ZDateTime.DayOfMonth(DateTime.Now, true).ToString("yyyy-MM-dd");
			ViewBag.EndDate = DateTime.Now.AddMinutes(-30);
			return View();
		}

		/// <summary>
		/// 收退款列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "ID DESC";
			data.From = "ord_accountsBill";
			data.Select = "*";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<OrdaccountsBillList> list = BaseService<OrdaccountsBillList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				if (item.Status == 0) {
					item.StatusName = "未付款";
				}
				else if (item.Status == 1) {
					item.StatusName = "已付未审";
				}
				else {
					item.StatusName = "已付已审";
				}
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
			string status = ZConvert.ToString(Request["status"]);
			int paymentMethod = ZConvert.StrToInt(Request["paymentMethod"]);
			int billType = ZConvert.StrToInt(Request["billType"]);
			int dateType = ZConvert.StrToInt(Request["dateType"]);
			string startDate = ZConvert.ToString(Request["startDate"]);
			string EndDate = ZConvert.ToString(Request["endDate"]);

			string whereSql = "";
			if (dateType == 1) {
				whereSql = string.Format(" CreateDate >= '{0}' and CreateDate <= '{1} 23:59:59'", startDate, EndDate);
			}
			else {
				whereSql = string.Format(" PayDate >= '{0}' and PayDate <= '{1} 23:59:59'", startDate, EndDate);
			}

			if (keyWord != "") {
				switch (keyWordType) {
					case "订单编号":
						whereSql += string.Format(" and ErpOrderCode like '%{0}%'", keyWord);
						break;
					case "外部订单号":
						whereSql += string.Format(" and EXISTS(SELECT 1 FROM ord_base Where ErpOrderCode = ord_accountsBill.ErpOrderCode and OutOrderCode like '%{0}%')", keyWord);
						break;
					case "单据号":
						whereSql += string.Format(" and BillNo like '%{0}%')", keyWord);
						break;
					case "售后单号":
						whereSql += string.Format(" and AssociatedCode like '%{0}%'", keyWord);
						break;
					case "交易号":
						whereSql += string.Format(" and TradingNumber like '%{0}%')", keyWord);
						break;
				}
			}

			if (status != "") {
				whereSql += string.Format(" and Status in ({0})", status);
			}
			if (paymentMethod != -1) {
				whereSql += string.Format(" and PaymentMethod = {0}", paymentMethod);
			}
			if (billType != 0) {
				whereSql += string.Format(" and BillType = {0}", billType);
			}

			return whereSql;
		}

		/// <summary>
		/// 查看
		/// </summary>
		/// <param name="billNo"></param>
		/// <returns></returns>
		public ActionResult ShowPayInfo(string billNo) {
			OrdaccountsBill accountBill = OrdaccountsBillService.GetSingleByBillNo(billNo);
			ViewBag.AccountBill = accountBill;
			return View();
		}

		/// <summary>
		/// 付款
		/// </summary>
		/// <param name="billNo"></param>
		/// <returns></returns>
		public ActionResult EditPayInfo(string billNo) {
			OrdaccountsBill accountBill = OrdaccountsBillService.GetSingleByBillNo(billNo);
			ViewBag.AccountBill = accountBill;
			return View();
		}

		/// <summary>
		/// 保存付款
		/// </summary>
		/// <param name="accountBill"></param>
		/// <returns></returns>
		public ActionResult SavePayInfo(OrdaccountsBill accountBillWebInfo) {
			BaseResult resultInfo = OrdbaseManager.SavePayInfo(accountBillWebInfo, FormsAuth.GetUserCode());
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 审核付款
		/// </summary>
		/// <param name="billNo"></param>
		/// <returns></returns>
		public ActionResult AuditPayInfo(string billNo) {
			BaseResult resultInfo = new BaseResult();
			OrdaccountsBill accountBill = OrdaccountsBillService.GetSingleByBillNo(billNo);
			if (accountBill.Status == 1) {
				accountBill.Status = 2;
				int rowsAffected = OrdaccountsBillService.Update(accountBill);
				if (rowsAffected == 0) {
					resultInfo.result = 0;
					resultInfo.message = "审核失败！";
				}
			}
			return JsonDate(resultInfo);
		}
	}
}
