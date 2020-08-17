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
using PaiXie.Api.Bll;
#endregion
namespace PaiXie.Erp.Areas.SysWarehouse
{
	public class UserController : BaseController
    {	

		#region 用户下角色列表
		public ActionResult UserRole(string UID) {
			List<Sysrole> obj = new List<Sysrole>();		 
			ViewBag.UID = UID;
			ViewBag.UCode = SysuserService.GetSysuserlist( UID).Code;
			return View();
		} 
		#endregion

		#region Index

		public ActionResult Index() {
			if (string.IsNullOrEmpty(Request["cid"])) {
               	ZCookies.WriteCookies("ck", 1, "no");			
			}
			return View();
		} 
		#endregion

		#region 用户列表
		/// <summary>
		/// 用户列表
		/// </summary>
		/// <returns></returns>
		[MvcMenuFilter(false)]
		public ActionResult search(string rolecode = null) {
			//   Json格式的要求{total:22,rows:{}}
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = "1=1 ";
			Object[] objects = new Object[3];
			string Code =  Request["Code"];
			if (!string.IsNullOrEmpty(Code)) {
				whereSql += " and  a.CODE like @0";
				objects[0] = "%" + Code + "%";
			}
	        whereSql += " and a.ModeType = @1";
			objects[1] =(int)ProjectType.仓库端;
			whereSql += "  and  a.WarehouseCode = @2";
			objects[2] = FormsAuth.GetWarehouseCode();		
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = " id desc";
			data.From = @"sys_user  a
	        LEFT JOIN  sys_user b ON a.CreatePerson=b.Code	
	        LEFT JOIN  sys_user c ON a.UpdatePerson=c.Code";
			data.Select = "	a.*,b.Name AS cname,c.Name AS uname,'0' AS part1";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<SysuserInfo> list = BaseService<SysuserInfo>.GetQueryManyForPage(data, out total, null, objects);
			//   构造成Json的格式传递
			if (rolecode != null) {
				for (int i = 0; i < list.Count(); i++) {
					int z = SysuserRoleMapService.Getsys_userRoleMapCount( list[i].Code , rolecode );
					if (z > 0) {
						list[i].part1 = "1";
					}
				}
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}		
		#endregion

		#region 用户编辑

		public ActionResult Edit(string id) {
			Sysuser objSysuser = new Sysuser();
			if (id != "0") objSysuser = SysuserService.GetSysuserlist(id) ;
			ViewBag.Sysuser = objSysuser;
			return View();
		}

		public ActionResult add(string id) {
			Sysuser objSysuser = new Sysuser();
			if (id != "0") objSysuser = SysuserService.GetSysuserlist(id);
			ViewBag.Sysuser = objSysuser;
			return View("Edit");
		}
		
		#endregion

		#region 保存
		[MvcMenuFilter(false)]
		[HttpPost]
		public ActionResult Save(Sysuser obj, string pwd) {
			BaseResult BaseResult = new BaseResult();
			int result = 1;
			try {


				if (obj.ID == 0) {
				


					obj.Password = ZEncypt.MD5(pwd);
					obj.CreatePerson = FormsAuth.GetUserCode();
					obj.CreateDate = System.DateTime.Now;
					obj.UpdatePerson = FormsAuth.GetUserCode();
					obj.UpdateDate = System.DateTime.Now;				
					obj.ModeType = (int)ProjectType.仓库端;									
					obj.WarehouseCode = FormsAuth.GetWarehouseCode();
					result = SysuserService.Add(obj);
				}
				else {
				
				    Sysuser objSysuser = SysuserService.GetSysuserlist(ZConvert.ToString(obj.ID));
					objSysuser.Code = obj.Code;
					objSysuser.Name = obj.Name;
					objSysuser.IsEnable = obj.IsEnable;
					objSysuser.Seq = obj.Seq;
					objSysuser.Description = obj.Description;
					objSysuser.UpdatePerson = FormsAuth.GetUserCode();
					objSysuser.UpdateDate = System.DateTime.Now;
					if (pwd.Trim() != "******") {
						objSysuser.Password = ZEncypt.MD5(pwd);
					}
					result = SysuserService.Update(objSysuser);
				}
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "保存失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "保存用户", FormsAuth.GetUserCode());

			}
			return JsonDate(BaseResult);


		}
		
		#endregion

		#region 删除

		public ActionResult Delete(string id) {
			BaseResult BaseResult = new BaseResult();
				try {
		
			int  result=SysuserService.Deletsysuser(id);
			if (result == 0) {
				BaseResult.result = -1;
				BaseResult.message = "删除失败";
			}
				}
				catch (Exception ex) {
					BaseResult.result = -1;
					BaseResult.message = ex.Message;
					PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "删除 用户", FormsAuth.GetUserCode());

				}
			return JsonDate(BaseResult);

		}
		
		#endregion

		#region 检查代码唯一性
		[MvcMenuFilter(false)]
		public ActionResult CheckUserCode(string UserCode, int ID) {
			BaseResult BaseResult = new BaseResult();
			if (ID > 0) {
				if (SysuserService.Getsysusercount( ID,  UserCode)  > 0) {
					BaseResult.result = -1;
				}
			}
			else {
				if (SysuserService.Getsysusercount( UserCode) > 0) {
					BaseResult.result = -1;
				}
			}
			return JsonDate(BaseResult);
		} 
		#endregion
    }
}