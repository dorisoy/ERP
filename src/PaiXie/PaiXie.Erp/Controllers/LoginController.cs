#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Utils;
using System.Web.Security; 
#endregion
namespace PaiXie.Erp.Controllers
{
	[AllowAnonymous]
	[MvcMenuFilter(false)]
    public class LoginController : Controller
    {
		#region Index

		public ActionResult Index() {
			ViewBag.ucode =  ZCookies.GetCookies("ucode");
			ViewBag.upwd =  ZCookies.GetCookies("upwd");
			return View();
		} 
		#endregion

		#region 验证码
		/// <summary>
		/// 验证码
		/// </summary>
		/// <returns>返回验证码</returns>
		public ActionResult CheckCode() {
			//首先实例化验证码的类
			ValidateCode validateCode = new ValidateCode();
			//生成验证码指定的长度
			string code = validateCode.CreateValidateCode(5);	
			//将验证码赋值给Session变量
		//	Session["ValidateCode"] = code;
		//	Session["ValidateCode"].t = 30;　
			this.TempData["ValidateCode"] = code;
			//创建验证码的图片
			byte[] bytes = validateCode.CreateValidateGraphic(code);
			//最后将验证码返回
			return File(bytes, @"image/jpeg");
		} 
		#endregion

		#region 登录

		public ActionResult DoAction(string UserCode,string yzm, string UserPassword, string checkme, string Area) {
			if (String.IsNullOrEmpty(yzm))
				return Content("验证码不能为空！");
			try {
				string vcode = TempData["ValidateCode"].ToString();
				if (yzm.Trim() != vcode)
					return Content("验证码不正确！");
			}
			catch {
				return Content("验证码不正确！ ");
			}
			
			string message = new Users().Login(UserCode,yzm, UserPassword, checkme, Area);
			return Content(message);
		} 
		#endregion

		#region 登出

		public ActionResult Logout() {
		            ZCookies.DelCookie("wid");
					FormsAuth.SingOut();
					return Redirect("~/Login");
		} 
		#endregion
    }
}