using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Web;
using System.Web.Security;
using System.Threading;
using PaiXie.Utils;
namespace PaiXie.Core {
	public static class FormsAuth {

		/// <summary>
		/// 是否超级用户 
		/// </summary>
		/// <returns>true 是  false 否</returns>
		public static bool GetIsSupper() {
			int i = FormsAuth.GetUserData().IsSupper;
			return i == 1 ? true : false;
		}


		/// <summary>
		/// 获取当前用户是否那个子系统  仓库端  管理端  枚举
		/// </summary>
		/// <returns></returns>
		public static int  GetModeType() {
			return FormsAuth.GetUserData().ModeType;
		}

		/// <summary>
		/// ModeType=仓库端    仓库编号
		/// </summary>
		/// <returns></returns>
		public static string GetWarehouseCode() {
			//仓库端的用户的话  从登陆信息获取
		
              return FormsAuth.GetUserData().WarehouseCode;
			
			
		}

		/// <summary>
		/// 获取当前用户代码
		/// </summary>
		/// <returns></returns>
		public static string GetUserCode() {
			return FormsAuth.GetUserData().UserCode;
		}
		/// <summary>
		/// 获取当前用户姓名
		/// </summary>
		/// <returns></returns>
		public static string GetUserName() {
			return FormsAuth.GetUserData().UserName;
		}


		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="loginName"></param>
		/// <param name="userData"></param>
		/// <param name="expireMin"></param>
		public static void SignIn(string loginName, object userData, int expireMin) {
			var data = JsonConvert.SerializeObject(userData);

			//创建一个FormsAuthenticationTicket，它包含登录名以及额外的用户数据。
			var ticket = new FormsAuthenticationTicket(2,
				loginName, DateTime.Now, DateTime.Now.AddDays(1), true, data);

			//加密Ticket，变成一个加密的字符串。
			var cookieValue = FormsAuthentication.Encrypt(ticket);

			//根据加密结果创建登录Cookie
			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue) {
				HttpOnly = true,
				Secure = FormsAuthentication.RequireSSL,
				Domain = FormsAuthentication.CookieDomain,
				Path = FormsAuthentication.FormsCookiePath
			};
			if (expireMin > 0)
				cookie.Expires = DateTime.Now.AddMinutes(expireMin);

			var context = HttpContext.Current;
			if (context == null)
				throw new InvalidOperationException();

			//写登录Cookie
		//	HttpContext.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
			context.Response.Cookies.Remove(cookie.Name);
			context.Response.Cookies.Add(cookie);
		}
		/// <summary>
		/// 注销
		/// </summary>
		public static void SingOut() {
			FormsAuthentication.SignOut();
		}
		/// <summary>
		/// 获取用户消息
		/// </summary>
		/// <returns></returns>
		public static LoginerBase GetUserData() {
			if (GetUserData<LoginerBase>()!=null)
			return GetUserData<LoginerBase>();
			else {
				Thread.Sleep(1000);
				return GetUserData<LoginerBase>();

			}
		}

		public static T GetUserData<T>() where T : class, new() {
			var UserData = new T();
			try {
				var context = HttpContext.Current;
				var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
				var ticket = FormsAuthentication.Decrypt(cookie.Value);
				UserData = JsonConvert.DeserializeObject<T>(ticket.UserData);
			}
			catch { }

			return UserData;
		}
	}
	public class LoginerBase {
		public string UserName { get; set; }
		public string UserCode { get; set; }
		public int ModeType { get; set; }
		public string WarehouseCode { get; set; }
		//是否超级用户 1 是 0 否
		public int IsSupper { get; set; }
	}
}
