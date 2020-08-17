using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace PaiXie.Erp {
	// 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
	// 请访问 http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication {

		/// <summary>
		/// 移除WebForm视图引擎
		/// </summary>
		void RemoveWebFormEngines() {
			var viewEngines = ViewEngines.Engines;
			var webFormEngines = viewEngines.OfType<WebFormViewEngine>().FirstOrDefault();
			if (webFormEngines != null) {
				viewEngines.Remove(webFormEngines);
			}
		}

		protected void Application_Start() {
			RemoveWebFormEngines(); //移除WebForm视图引擎
			MvcHandler.DisableMvcResponseHeader = true;  // 隐藏ASP.NET MVC版本
			AreaRegistration.RegisterAllAreas();
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			BundleTable.EnableOptimizations = true;   //是否启用压缩
		}

		#region 解决Firefox掉Session
		void Application_BeginRequest(Object sender, EventArgs e) {
			//当前是否ajax 请求  查看请求记录   测试
			PlanLog.WriteLog("Is a ajax Request" + "\n" +
				(new HttpRequestWrapper(Request)).IsAjaxRequest() + "\n" + Request.Url.AbsoluteUri, LogType.ajax.ToString());

			try {
				string session_param_name = "ASPSESSID";
				string session_cookie_name = "ASP.NET_SessionId";
				if (HttpContext.Current.Request.Form[session_param_name] != null) {
					UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
				}
				else if (HttpContext.Current.Request.QueryString[session_param_name] != null) {
					UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
				}
			}
			catch {
			}


			//此处是身份验证
			try {
				string auth_param_name = "AUTHID";
				string auth_cookie_name = FormsAuthentication.FormsCookieName;
				if (HttpContext.Current.Request.Form[auth_param_name] != null) {
					UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
				}
				else if (HttpContext.Current.Request.QueryString[auth_param_name] != null) {
					UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
				}
			}
			catch { }
		}

		private void UpdateCookie(string cookie_name, string cookie_value) {
			HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
			if (null == cookie) {
				cookie = new HttpCookie(cookie_name);
			}
			cookie.Value = cookie_value;
			HttpContext.Current.Request.Cookies.Set(cookie);//重新设定请求中的cookie值，将服务器端的session值赋值给它
		}

		#endregion 
	}
}