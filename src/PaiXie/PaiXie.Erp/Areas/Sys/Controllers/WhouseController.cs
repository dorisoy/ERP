#region using
using PaiXie.Data;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Service;
using PaiXie.Core;
#endregion
namespace PaiXie.Erp.Areas.Sys {
	public class WhouseController : BaseController {
	

		#region Index

		public ActionResult Index() {
			return View();
		}
		#endregion

		#region SetArea

		public ActionResult SetArea() {
			return View();
		}
		#endregion

		#region 列表
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ActionResult search(string Code = null) {
			//   Json格式的要求{total:22,rows:{}}
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = "";

			Object[] objects = new Object[1];
			
			string lCode = Request["Code"];
			if (!string.IsNullOrEmpty(Code)) {

				whereSql += "Code LIKE @0";
				objects[0] = "%" + lCode + "%";
			}

			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = "logistics";
			data.Select = "	*";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<Logistics> list = BaseService<Logistics>.GetQueryManyForPage(data, out total, null, objects);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 编辑
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Edit(int id) {
			Logistics objSysuser = new Logistics();
			if (id > 0) objSysuser = LogisticsService.GetLogistics(id);
			ViewBag.Sysuser = objSysuser;
			return View();
		}
		#endregion

		#region 保存
		[HttpPost]
		public ActionResult Save(Logistics obj) {
			BaseResult BaseResult = new BaseResult();
			int result = 1;
			try {
				if (obj.ID == 0) {
					result=LogisticsService.Add(obj);
				}
				else {
					
					Logistics objSysuser = LogisticsService.GetLogistics(obj.ID);
					objSysuser.Code = obj.Code.ToUpper();
					objSysuser.Name = obj.Name;
					objSysuser.IsEnable = obj.IsEnable;
					objSysuser.Seq = obj.Seq;
					objSysuser.WebUrl = obj.WebUrl;
					objSysuser.IsSetArea = obj.IsSetArea;
					objSysuser.Tags = obj.Tags;
					objSysuser.KeyWords = obj.KeyWords;
				result=	LogisticsService.Update(objSysuser);
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "操作失败";
				}
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "保存物流信息", FormsAuth.GetUserCode());

			}
			return JsonDate(BaseResult);


		}

		#endregion

		#region 删除
		public ActionResult Delete(int id) {
			BaseResult BaseResult = new BaseResult();
			try {
				int result = LogisticsService.SetIsEnable(id);
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "操作失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "删除物流信息", FormsAuth.GetUserCode());

			}
			
			return JsonDate(BaseResult);

		}
		#endregion

		#region 代码唯一性检查
		public ActionResult CheckCode(string roleCode, int ID) {
			BaseResult BaseResult = new BaseResult();
			if (ID > 0) {
			if (LogisticsService.GetLogisticsCount( ID,  roleCode) > 0) {
					BaseResult.result = -1;
				}
			}
			else {
				if (LogisticsService.GetLogisticsCount( roleCode)  > 0) {
					BaseResult.result = -1;
				}
			}
			return JsonDate(BaseResult);
		}
		#endregion
	}
}