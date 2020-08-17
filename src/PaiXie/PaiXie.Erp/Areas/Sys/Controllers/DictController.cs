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
	public class DictController : BaseController {
		#region Index
		//
		// GET: /Sys/Dict/

		public ActionResult Index() {
			return View();
		}
		#endregion

		#region 字典
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Code(string codetype, string pcode, string code = "") {
			Syscode objSyscode = new Syscode();
			if (code.Trim() != "") {
				objSyscode = SyscodeService.GetSyscodeByCode(code);
			}
			ViewBag.codetype = codetype;
			ViewBag.pcode = pcode;
			ViewBag.Syscode = objSyscode;
			return View();
		}
		#endregion

		#region 属性列表

		public ActionResult Gettreegrid(string CodeType = "") {
			EasyUITree EUItree = new EasyUITree();
			DataTable dt = SyscodeService.GetSyscodeTree(CodeType);
			List<JsonTree> list = EUItree.initTree(dt,"parentid=0",0,1);		
			return JsonDate(list);
		}

		#endregion

		#region 字典类型列表

		public ActionResult JsonTreeSyscodeType(string name="") {

			EasyUITree EUItree = new EasyUITree();
			DataTable dt = SyscodeTypeService.GetJsonTreeSyscodeType(name);
			List<JsonTree> list = EUItree.initTree(dt);
			return JsonDate(list);
		}
		#endregion

		#region 字典类型
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult CodeType(string codetype) {
			SyscodeType objSyscodeType = new SyscodeType();
			if (codetype.Trim() != "") objSyscodeType = SyscodeTypeService.CodeType(codetype);
			ViewBag.SyscodeType = objSyscodeType;
			return View();
		}
		#endregion

		#region 保存
		[HttpPost]
		public ActionResult Save(SyscodeType obj) {
			BaseResult BaseResult = new BaseResult();
			int result = 1;
			try {
				if (obj.ID == 0) {
					obj.CreatePerson = FormsAuth.GetUserCode();
					obj.CreateDate = System.DateTime.Now;
					obj.UpdatePerson = FormsAuth.GetUserCode();
					obj.UpdateDate = System.DateTime.Now;
					obj.IsEnable = 1;
					result = SyscodeTypeService.Add(obj);
				}
				else {
					SyscodeType objSysuser = SyscodeTypeService.CodeType(obj.ID);
					objSysuser.Code = obj.Code;
					objSysuser.Name = obj.Name;		
					objSysuser.UpdatePerson = FormsAuth.GetUserCode();
					objSysuser.UpdateDate = System.DateTime.Now;

					result = SyscodeTypeService.Update(objSysuser);
				}
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "操作失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "保存字典", FormsAuth.GetUserCode());

			}
			return JsonDate(BaseResult);


		}

		[HttpPost]
		public ActionResult Savecode(Syscode obj) {
			BaseResult BaseResult = new BaseResult();
			int result = 1;
			try {
				if (obj.ID == 0) {
					obj.ParentCode = "0";
					obj.CreatePerson = FormsAuth.GetUserCode();
					obj.CreateDate = System.DateTime.Now;
					obj.UpdatePerson = FormsAuth.GetUserCode();
					obj.UpdateDate = System.DateTime.Now;
					result = SyscodeService.Add(obj);
				}
				else {

					Syscode objSysuser = SyscodeService.Code(obj.ID);
					objSysuser.Code = obj.Code;
					objSysuser.Text = obj.Text;
					objSysuser.IsEnable = obj.IsEnable;
					objSysuser.Seq = obj.Seq;
					objSysuser.Description = obj.Description;
					objSysuser.UpdatePerson = FormsAuth.GetUserCode();
					objSysuser.UpdateDate = System.DateTime.Now;

					result = SyscodeService.Update(objSysuser);
				}
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "操作失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "保存代码", FormsAuth.GetUserCode());

			}
			return JsonDate(BaseResult);


		}


		#endregion

		#region 删除
		public ActionResult Delete(string id) {
			BaseResult BaseResult = new BaseResult();
			try {
				int result = SyscodeTypeService.deleteCodeType(id);
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "操作失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "删除代码类型", FormsAuth.GetUserCode());

			}

			return JsonDate(BaseResult);

		}
		public ActionResult Deletecode(string id) {
			BaseResult BaseResult = new BaseResult();
			try {
				int result = SyscodeService.deleteCode(id);
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "操作失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "删除代码", FormsAuth.GetUserCode());

			}

			return JsonDate(BaseResult);

		}
		#endregion

		#region 代码唯一性检查
		public ActionResult CheckCode(string Code, int ID) {
			BaseResult BaseResult = new BaseResult();
			if (ID > 0) {
				if (SyscodeTypeService.CheckCode(Code, ID) > 0) {
					BaseResult.result = -1;
				}

			}
			else {
				if (SyscodeTypeService.CheckCode(Code) > 0) {
					BaseResult.result = -1;
				}
			}
			return JsonDate(BaseResult);
		}
		public ActionResult CheckCodes(string Code, int ID) {
			BaseResult BaseResult = new BaseResult();
			if (ID > 0) {
				if (SyscodeService.CheckCode(Code, ID) > 0) {
					BaseResult.result = -1;
				}

			}
			else {
				if (SyscodeService.CheckCode(Code) > 0) {
					BaseResult.result = -1;
				}
			}
			return JsonDate(BaseResult);
		}

		#endregion
	}
}