#region using
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Core;
#endregion
namespace PaiXie.Erp.Areas.Sys {
	public class LogsController : BaseController {
		#region Index
		//
		// GET: /Sys/Logs/

		public ActionResult Index() {
			if (string.IsNullOrEmpty(Request["cid"]))
				ZCookies.WriteCookies("ck", 1, "no");
			return View();
		}
		#endregion

		#region SeeMessage
		//
		// GET: /Sys/Logs/SeeMessage

		public ActionResult SeeMessage(int id) {
			 Syslog  objSyslog= SyslogService.GetQuerySingleByID(id);
			 if (objSyslog != null) {
				 ViewBag.Message = objSyslog.Message;
				 ViewBag.OldMessage = objSyslog.OldMessage;
			 }
			 else {
				 ViewBag.Message = "";
				 ViewBag.OldMessage = "";
			 }
			return View();
		}
		#endregion
	
		#region 登录日志
		/// <summary>
		/// 登录日志
		/// </summary>
		/// <returns></returns>
		public ActionResult hislogsearch() {
			//   Json格式的要求{total:22,rows:{}}
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
		
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "     LoginDate DESC, id desc ";
			data.From = "sys_loginHistory";
			data.Select = "	*";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<SysloginHistory> list = BaseService<SysloginHistory>.GetQueryManyForPage(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		private string GetWhereSql() {	
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string whereSql = " ModeType=" + (int)ProjectType.管理端+" ";			
			if (keyWord != "") {
				switch (keyWordType) {
					case "用户名":
						whereSql += string.Format(" and UserCode like '%{0}%'", keyWord);
						break;
				}
			}

		

			if (!string.IsNullOrEmpty(Request["StartDate"])) {
				whereSql += string.Format(" and LoginDate > '{0}'", Request["StartDate"]);
			}

			if (!string.IsNullOrEmpty(Request["EndDate"])) {
				whereSql += string.Format(" and LoginDate < '{0}'", Request["EndDate"]);
			}



				
		
			return whereSql;
		}


		#endregion

		#region 操作日志
		/// <summary>
		/// 操作日志
		/// </summary>
		/// <returns></returns>
		public ActionResult syslogsearch() {
			//   Json格式的要求{total:22,rows:{}}
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql2();
		
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "      Date DESC, id desc ";
			data.From = "sys_log";
			data.Select = "	*";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<Syslog> list = BaseService<Syslog>.GetQueryManyForPage(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		private string GetWhereSql2() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string whereSql = " ModeType=" + (int)ProjectType.管理端 + " ";	
			if (keyWord != "") {
				switch (keyWordType) {
					case "用户名":
						whereSql += string.Format(" and UserCode like '%{0}%'", keyWord);
						break;
					case "操作对象":
						whereSql += string.Format(" and Target like '%{0}%'", keyWord);
						break;
					case "操作内容":
						whereSql += string.Format(" and Message like '%{0}%'", keyWord);
						break;

				}
			}


			if (!string.IsNullOrEmpty(Request["StartDate"])) {
				whereSql += string.Format(" and Date > '{0}'", Request["StartDate"]);
			}

			if (!string.IsNullOrEmpty(Request["EndDate"])) {
				whereSql += string.Format(" and Date < '{0}'", Request["EndDate"]);
			}

			return whereSql;
		}
		#endregion

		#region 错误日志

		public ActionResult SyserrorLog() {
			return View();
		}

		/// <summary>
		/// 错误日志
		/// </summary>
		/// <returns></returns>
		public ActionResult SyserrorLogSearch() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetSysWhereSql();

			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "ID DESC";
			data.From = "Sys_errorLog";
			data.Select = "*";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<SyserrorLog> list = BaseService<SyserrorLog>.GetQueryManyForPage(data, out total);
			//构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		public string GetSysWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string whereSql = " 1=1";
			if (keyWord != "") {
				switch (keyWordType) {
					case "错误地址":
						whereSql += string.Format(" and ErrorUrl like '%{0}%'", keyWord);
						break;
					case "错误信息":
						whereSql += string.Format(" and FriendlyMessage like '%{0}%'", keyWord);
						break;
					case "错误详情":
						whereSql += string.Format(" and StackTrace like '%{0}%'", keyWord);
						break;

				}
			}
			if (!string.IsNullOrEmpty(Request["StartDate"])) {
				whereSql += string.Format(" and CreateDate >= '{0}'", Request["StartDate"]);
			}
			if (!string.IsNullOrEmpty(Request["EndDate"])) {
				whereSql += string.Format(" and CreateDate <= '{0}'", Request["EndDate"]);
			}
			return whereSql;
		}

		#endregion
	}
}