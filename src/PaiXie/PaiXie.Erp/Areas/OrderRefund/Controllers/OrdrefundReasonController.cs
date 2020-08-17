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

namespace PaiXie.Erp.Areas.OrderRefund.Controllers
{
	public class OrdrefundReasonController : BaseController
    {
        //
        // GET: /OrderRefund/OrdrefundReason/

        public ActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// 售后原因列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = "";
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "ID DESC";
			data.From = "ord_refundReason";
			data.Select = "*";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<OrdrefundReason> list = OrdrefundReasonService.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		/// <summary>
		/// 添加原因
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Add(int id = 0) {
			OrdrefundReason reason = OrdrefundReasonService.GetQuerySingleByID(id);
			if (reason == null) {
				reason = new OrdrefundReason();
			}
			ViewBag.Reason = reason;
			return View();
		}

		/// <summary>
		///  保存原因
		/// </summary>
		/// <returns></returns>
		public ActionResult Save(OrdrefundReason reason) {
			BaseResult resultInfo = new BaseResult();
			if (reason.ID == 0) {
				int ID = OrdrefundReasonService.Add(reason);
				if (ID == 0) {
					resultInfo.result = 0;
					resultInfo.message = "修改失败！";
				}
			}
			else {
				int rowsAffected = OrdrefundReasonService.Update(reason);
				if (rowsAffected == 0) {
					resultInfo.result = 0;
					resultInfo.message = "添加失败！";
				}
			}
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Delete(int id) {
			BaseResult resultInfo = new BaseResult();
			int rowsAffected = OrdrefundReasonService.DelByID(id);
			if (rowsAffected == 0) {
				resultInfo.result = 0;
				resultInfo.message = "删除失败！";
			}
			return JsonDate(resultInfo);
		}
    }
}
