#region using
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Data;
using PaiXie.Core;
using PaiXie.Utils;
#endregion
namespace PaiXie.Erp.Areas.Sys {
	public class SetAreaController : BaseController {
		#region Index
		//
		// GET: /Sys/SetArea/Index

		public ActionResult Index() {
			return View();
		}
		#endregion

		#region 区域列表
		// GET: /Sys/SetArea/Search
		/// <summary>
		/// 区域列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			EasyUITree EUItree = new EasyUITree();
			DataTable dt = SysareaService.GetareaDataTable();
			List<JsonTree> list = EUItree.initTree(dt);
			return JsonDate(list);
		}

		#endregion

		#region 删除区域
		//   /Sys/SetArea/Delete
		/// <summary>
		/// 删除区域
		/// </summary>
		/// <param name="id">主键id</param>
		/// <param name="level">等级  0  省级  1  市级  2  区级</param>
		/// <returns></returns>
		public ActionResult Delete(int id,int level) {
			BaseResult BaseResult = new BaseResult();
			try {
				int result = SysareaService.DelArea(id,level);
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "删除失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "删除区域", FormsAuth.GetUserCode());

			}
			return JsonDate(BaseResult);

		}

		#endregion

		#region Edit
		//
		// GET: /Sys/SetArea/Edit

		public ActionResult Edit(int  id=0,int level=0) {
			Sysarea Sysarea = new Sysarea();
			//添加
		   if (level == -1) {
			   Sysarea = new Sysarea();
			   Sysarea.ParentID = id;
			   ViewBag.Sysarea = Sysarea;
		   }
			   //编辑
		   else {
			   Sysarea = SysareaService.GetArea(id);
			   if (Sysarea != null) {
				   ViewBag.Sysarea = Sysarea;
			   }
		   }
			return View();
		}
		#endregion

		#region 保存区域
		//   /Sys/SetArea/Save
		[HttpPost]
		public ActionResult Save(Sysarea obj) {
			BaseResult BaseResult = new BaseResult();
			int result = 1;
			try {
				if (obj.ID == 0) {
					result = SysareaService.Add(obj);
				}
				else {
					result = SysareaService.EditArea(obj.Name,obj.ID);
				}
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "操作失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "保存区域", FormsAuth.GetUserCode());
			}
			return JsonDate(BaseResult);
		}
		#endregion

		#region 恢复初始化区域设置
		//   /Sys/SetArea/initArea
		public ActionResult initArea() {
			BaseResult BaseResult = new BaseResult();
			int result = 1;
			try {
				result = SysareaService.initArea();
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "操作失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "恢复初始化区域设置", FormsAuth.GetUserCode());
			}
			return JsonDate(BaseResult);
		}
		#endregion
	
	}
}