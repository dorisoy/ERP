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
using System.Data;
using PaiXie.Api.Bll; 
#endregion
namespace PaiXie.Erp.Areas.SysWarehouse
{
	public class RoleController : BaseController {

		#region 设置角色菜单事件
			[MvcMenuFilter(false)]
		public ActionResult SetRoleButton(string mids, string rcode) {
			BaseResult BaseResult = new BaseResult();
			
			BaseResult = PaiXie.Api.Bll.Users.SetRoleButton(mids, rcode);
			return JsonDate(BaseResult);

		}
		#endregion          

		#region 菜单事件权限

		public ActionResult Button(string roleid) {
			ViewBag.roleid = roleid;
			ViewBag.rolecode = SysroleService.GetSysrole(roleid).Code;

			return View();
		}
		#endregion

		#region 菜单事件列表
			[MvcMenuFilter(false)]
		public ActionResult Gettreebutton(string roleid = "") {
			string rolecode = roleid;
			DataTable dt = new DataTable("Table_AX");
			dt.Columns.Add("ID", System.Type.GetType("System.String"));
			dt.Columns.Add("TEXT", System.Type.GetType("System.String"));
			dt.Columns.Add("ParentID", System.Type.GetType("System.String"));
			dt.Columns.Add("state", System.Type.GetType("System.String"));
			dt.Columns.Add("attr", System.Type.GetType("System.String"));
			dt.Columns.Add("flag", System.Type.GetType("System.String"));

			//List<Sysmenu>
			DataTable	Sysmenulist = SysmenuService.GetDataTable(FormsAuth.GetModeType(),"ck");// GetSysmenulist();
			//foreach(var item in Sysmenulist)
			//{
			for (int i = 0; i < Sysmenulist.Rows.Count; i++) {
				DataRow dr = dt.NewRow();
				dr["ID"] = Sysmenulist.Rows[i]["ID"];// item.Code;
				dr["TEXT"] = Sysmenulist.Rows[i]["TEXT"];// item.Name;
				dr["ParentID"] = Sysmenulist.Rows[i]["ParentID"]; //item.ParentCode;
				dr["state"] = "open";
				dr["attr"] = "0";
				dr["flag"] = "0";
				dt.Rows.Add(dr);

			}

			List<Sysbutton> Sysbuttonlist = SysbuttonService.GetSysbuttonlist();
			foreach (var item in Sysbuttonlist) {
				DataRow dr = dt.NewRow();
				dr["ID"] = item.Code;
				dr["TEXT"] = item.Name;
				dr["ParentID"] = item.MenuCode;
				dr["state"] = "open";
				dr["attr"] = "0";
				dr["flag"] = "1";
				dt.Rows.Add(dr);

			}
			

			EasyUITree EUItree = new EasyUITree();
			for (int i = 0; i < dt.Rows.Count; i++) {
				string buttoncode = dt.Rows[i]["ID"].ToString();
				int c = SysroleMenuButtonMapService.IsroleMenuButtonMap(rolecode, buttoncode);

				if (c > 0 && dt.Rows[i]["flag"].ToString() == "1")
					dt.Rows[i]["attr"] = "1";
			}

		List<JsonTree> list = EUItree.initTree(dt, "parentid='-1'",1);

			return JsonDate(list);
		}

		#endregion

		#region 设置角色菜单
		[MvcMenuFilter(false)]
		public ActionResult SetRoleMenu(string mids, string rcode) {
			BaseResult BaseResult = new BaseResult();
		
			BaseResult = Users.SetRoleMenu(mids, rcode);
		
			return JsonDate(BaseResult);

		} 
		#endregion          

		#region 菜单列表
			[MvcMenuFilter(false)]
		public ActionResult Gettreegrid(string roleid="") {
			string rolecode = roleid;

			EasyUITree EUItree = new EasyUITree();
			DataTable dt = SysmenuService.GetDataTable(FormsAuth.GetModeType(),"ck");
			for (int i=0;i<dt.Rows.Count;i++) {
				string menucode = dt.Rows[i]["ID"].ToString();
			   int c=	SysroleMenuMapService.IsroleMenuMap( menucode,  rolecode);
		
			   if (c > 0)
				   dt.Rows[i]["attr"] = "1";
			}

			List<JsonTree> list = EUItree.initTree(dt, "parentid='-1'", 1);
			
			return JsonDate(list);
		}

		#endregion

		#region 菜单权限

		public ActionResult Menu(string roleid) {
			ViewBag.roleid = roleid;
			ViewBag.rolecode = SysroleService.GetSysrole(roleid).Code;
			return View();
		}
		#endregion

		#region 角色下用户列表
		public ActionResult RoleUser(string roleid) {
			
			ViewBag.roleid = roleid;
			ViewBag.rolecode = SysroleService.GetSysrole(roleid).Code;
			return View();
		}
		#endregion

		#region 用户下设置角色
		/// <summary>
		/// 用户下设置角色
		/// </summary>
		/// <param name="rids"></param>
		/// <param name="ucode"></param>
		/// <returns></returns>
			[MvcMenuFilter(false)]
		public ActionResult SetUserRole(string rids, string ucode) {
			BaseResult BaseResult = new BaseResult();
		
			BaseResult = Users.SetUserRole(rids, ucode);		
			return JsonDate(BaseResult);
		}
			[MvcMenuFilter(false)]
		public ActionResult SetRoleUser(string uids, string rcode) {
			BaseResult BaseResult = new BaseResult();
		
			BaseResult = Users.SetRoleUser(uids, rcode);

			return JsonDate(BaseResult);

		} 
		#endregion

		#region Index

		public ActionResult Index() {
			
			return View();
		} 
		#endregion

		#region 角色列表
		/// <summary>
		/// 角色列表
		/// </summary>
		/// <returns></returns>
		[MvcMenuFilter(false)]
		public ActionResult search(string UCode = null) {
			//   Json格式的要求{total:22,rows:{}}
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = "1=1";
			#region whereSql
			Object[] objects = new Object[3];
			string Code = Request["Code"];
			if (!string.IsNullOrEmpty(Code)) {
				whereSql += " and a.CODE LIKE @0";
				objects[0] = "%" + Code + "%";
			}

			whereSql += " and a.ModeType = @1";
			objects[1] = (int)ProjectType.仓库端;
			whereSql += "  and  a.WarehouseCode = @2";
			objects[2] = FormsAuth.GetWarehouseCode();

			
			#endregion
			
				
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = @"sys_role  a
						LEFT JOIN  sys_user b ON a.CreatePerson=b.Code	
						LEFT JOIN  sys_user c ON a.UpdatePerson=c.Code";
			data.Select = "	a.*,b.Name AS cname,c.Name AS uname,'0' AS part1";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<SysroleInfo> list = BaseService<SysroleInfo>.GetQueryManyForPage(data, out total, null, objects);
			//   构造成Json的格式传递
			if (UCode != null) {
				for (int i = 0; i < list.Count(); i++) {

					int z = SysuserRoleMapService.Getsys_userRoleMapCount( UCode ,list[i].Code );
					if (z > 0) {
						list[i].part1 = "1";
					}
				}
			}

			var result = new { total = total, rows = list };
			return JsonDate(result);
		}
		
		#endregion

		#region 角色编辑
		/// <summary>
		/// 角色编辑
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Edit(string id) {
			Sysrole objSysuser = new Sysrole();
			if (id != "0") objSysuser = SysroleService.GetSysrole(id);
			ViewBag.Sysuser = objSysuser;
			return View();
		}

		public ActionResult add(string id) {
			Sysrole objSysuser = new Sysrole();
			if (id != "0") objSysuser = SysroleService.GetSysrole(id);
			ViewBag.Sysuser = objSysuser;
			return View("Edit");
		} 
		#endregion

		#region 保存
		[HttpPost]
		[MvcMenuFilter(false)]
		public ActionResult Save(Sysrole obj) {
			BaseResult BaseResult = new BaseResult();
			int result = 1;
			try {


				if (obj.ID == 0) {
					
					obj.CreatePerson = FormsAuth.GetUserCode();
					obj.CreateDate = System.DateTime.Now;
					obj.UpdatePerson = FormsAuth.GetUserCode();
					obj.UpdateDate = System.DateTime.Now;					
					obj.ModeType = (int)ProjectType.仓库端;										
					obj.WarehouseCode = FormsAuth.GetWarehouseCode();
					result = SysroleService.Add(obj);
				}
				else {
				
                    Sysrole objSysuser = SysroleService.GetSysrole(ZConvert.ToString(obj.ID));
					objSysuser.Code = obj.Code;
					objSysuser.Name = obj.Name;
					objSysuser.IsEnable = obj.IsEnable;
					objSysuser.Seq = obj.Seq;
					objSysuser.Description = obj.Description;
					objSysuser.UpdatePerson = FormsAuth.GetUserCode();
					objSysuser.UpdateDate = System.DateTime.Now;

					result = SysroleService.Update(objSysuser);
				}
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "操作失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "保存角色", FormsAuth.GetUserCode());

			}
			return JsonDate(BaseResult);


		}
		
		#endregion

		#region 角色删除
		public ActionResult Delete(string id) {
			BaseResult BaseResult = new BaseResult();
		
			try {
				int result = SysroleService.DelSysrole(id);
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "操作失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "角色删除", FormsAuth.GetUserCode());
			}
			return JsonDate(BaseResult);

		} 
		#endregion

		#region 角色代码唯一性检查
		[MvcMenuFilter(false)]
		public ActionResult CheckroleCode(string roleCode, int ID) {
			BaseResult BaseResult = new BaseResult();
			if (ID > 0) {
			if (SysroleService.GetsysroleCount( ID,  roleCode)> 0) {
					BaseResult.result = -1;
				}

			}
			else {
				if (SysroleService.GetsysroleCount(roleCode) > 0) {
					BaseResult.result = -1;
				}
			}
			return JsonDate(BaseResult);
		} 
		#endregion
    }
}