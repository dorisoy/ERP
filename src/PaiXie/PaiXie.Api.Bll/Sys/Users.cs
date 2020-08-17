using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Core;
using System.Threading;
using FluentData;
namespace PaiXie.Api.Bll {
	public class Users {
		#region 登录
		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="UserName"></param>
		/// <param name="UserPassword"></param>
		/// <returns></returns>
		public string Login(string Code,string yzm, string Pwd, string checkme, string Area) {
			try 
			{
				
			var UserCode = Code.Trim();
			var Password = ZEncypt.MD5(Pwd.Trim());
			//用户名密码检查
			if (String.IsNullOrEmpty(UserCode) || String.IsNullOrEmpty(Password))
				return "用户名或密码不能为空！";

		


			//用户名密码验证
			var result = SysuserService.Login(UserCode, Password);
			if (result != null) {
				if (result.IsEnable == (int)IsEnable.否) {
					return "该用户已被禁用";
				}
			}
			if (result == null || String.IsNullOrEmpty(result.Code))
				return "用户名或密码不正确";
			//调用框架中的登陆机制
			var loginer = new LoginerBase { UserCode = result.Code, UserName = result.Name, ModeType = result.ModeType, WarehouseCode = result.WarehouseCode,IsSupper=result.IsSupper };
			UpdateUserLoginInfo(loginer);
			if (checkme == "checked") {
				ZCookies.WriteCookies("ucode", 1, Code);
				ZCookies.WriteCookies("upwd", 1, Pwd);
			}
			else {
				ZCookies.DelCookie("ucode");
				ZCookies.DelCookie("upwd");
			}
			//登陆后处理
			this.UpdateUserLoginCountAndDate(UserCode); //更新用户登陆次数及时间
			this.AppendLoginHistory(UserCode, Area, result.Name, result.ModeType, result.WarehouseCode);           //添加登陆日志
			//返回登陆成功
			return "OK";
			}
			catch (Exception ex) {
				Sys.SaveErrorLog(ex, "用户登录", FormsAuth.GetUserCode());
				return "登录失败";
			}
		}
		//调用框架中的登陆机制
		public void UpdateUserLoginInfo(LoginerBase objLoginerBase) {
			var loginer = objLoginerBase;// new LoginerBase { UserCode = result.Code, UserName = result.Name, ModeType = result.ModeType, WarehouseCode = result.WarehouseCode };
			var effectiveHours = ZConfig.GetConfigInt("LoginEffectiveHours");
			FormsAuth.SignIn(loginer.UserCode, loginer, 60 * effectiveHours);
		}
		/// <summary>
		/// //更新用户登陆次数及时间
		/// </summary>
		/// <param name="UserCode"></param>
		public void UpdateUserLoginCountAndDate(string UserCode) {
			SysuserService.UpdateLoginStatus(UserCode);
		}
		/// <summary>
		/// 添加登陆日志
		/// </summary>
		/// <param name="UserCode"></param>
		public void AppendLoginHistory(string UserCode, string Area, string UName, int ModeType, string WarehouseCode) {
			var lanIP = ZHttp.ClientIP;
			var hostName = ZHttp.IsLanIP(lanIP) ? ZHttp.ClientHostName : string.Empty;
			var UserName = UName;// FormsAuth.GetUserName();
			var IP = "";
			var City = Area;
			if (IP != lanIP)
				IP = string.Format("{0}/{1}", IP, lanIP).Trim('/').Replace("::1", "localhost");
			var item = new SysloginHistory();
			item.UserCode = UserCode;
			item.UserName = UserName;
			item.HostName = hostName;
			item.HostIP = IP;
			item.LoginCity = City;
			item.LoginDate = DateTime.Now;
			item.ModeType = ModeType;// FormsAuth.GetModeType();
			item.WarehouseCode = WarehouseCode;// FormsAuth.GetWarehouseCode();
			SysloginHistoryService.Add(item);
		}
		#endregion

		#region 权限判断
		/// <summary>
		/// control
		/// </summary>
		/// <param name="ac"></param>i
		/// <returns>false 没权限</returns>
		public bool IsAuth(string ac) {
			
			int i = SysbuttonService.GetPower(FormsAuth.GetUserCode(), ac);
			if (i == 0) return false;
			else return true;
		}
		/// <summary>
		/// js
		/// </summary>
		/// <param name="ac"></param>
		/// <returns>false 没权限</returns>
		public bool IsAuthJs(string Code) {
			//超级管理员直接有权限
			string UserCode = FormsAuth.GetUserCode();
			Sysuser objSysuser = SysuserService.GetSysuserbyUserCode(UserCode);
			if (objSysuser != null) {
				if (objSysuser.IsSupper == 1) {
					return true;
				}
			}
			Sysbutton obj = SysbuttonService.GetSysbuttonUrl(Code);
			if (obj != null) {
				List<Sysbutton> list = SysbuttonService.GetSysbuttonlist(FormsAuth.GetUserCode(), Code);
				if (list.Count() == 0) return false;
				else return true;
			}
			else {
				return false;
			}
		}
		#endregion

		#region 修改密码
		/// <summary>
		/// 
		/// </summary>
		/// <param name="opwd">旧密码</param>
		/// <param name="npwd">新密码</param>
		/// <param name="cpwd">确认密码</param>
		/// <returns></returns>
		public BaseResult UpdatePwd(string opwd, string npwd, string cpwd) {
			BaseResult BaseResult = new Core.BaseResult();
			Sysuser obj = SysuserService.GetSysuserbyUserCode(FormsAuth.GetUserCode());
			if (obj != null) {
				if (obj.Password != ZEncypt.MD5(opwd)) {
					BaseResult.result = -1;
					BaseResult.message = "原密码错误";
				}
				else {
					if (ZEncypt.MD5(cpwd) != ZEncypt.MD5(npwd)) {
						BaseResult.result = -1;
						BaseResult.message = "新密码不一致";
					}
					else {
						obj.Password = ZEncypt.MD5(npwd);
						if (SysuserService.Update(obj) > 0) {
							BaseResult.result = 1;
							BaseResult.message = "OK";
						}
					}
				}
			}
			return BaseResult;
		}
		#endregion

		#region 修改姓名
		/// <summary>
		/// 
		/// </summary>
		/// <param name="opwd">旧密码</param>
		/// <param name="npwd">新密码</param>
		/// <param name="cpwd">确认密码</param>
		/// <returns></returns>
		public BaseResult UpdateNmae(string Name) {
			BaseResult BaseResult = new Core.BaseResult();
			Sysuser obj = SysuserService.GetSysuserbyUserCode(FormsAuth.GetUserCode());
			if (obj != null) {
						obj.Name = Name;
						if (SysuserService.Update(obj) > 0) {
							BaseResult.result = 1;
							BaseResult.message = "OK";
						}
					}
			return BaseResult;
		}
		#endregion

		#region 设置角色按钮
		/// <summary>
		/// 设置角色按钮
		/// </summary>
		/// <param name="mids"></param>
		/// <param name="rcode"></param>
		/// <returns></returns>
		public static BaseResult SetRoleButton(string mids, string rcode) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					 SysroleMenuButtonMapService.DeleteroleMenuButtonMap(rcode, context);								
					string[] sArray = mids.Split(',');
					foreach (string i in sArray) {
						if (i.Contains("b")) {
							SysroleMenuButtonMap SysroleMenuMap = new SysroleMenuButtonMap();
							SysroleMenuMap.RoleCode = rcode;
							SysroleMenuMap.ButtonCode = i;
							int  result = SysroleMenuButtonMapService.Add(SysroleMenuMap,context);
							if (result == 0) {
								resultInfo.result = 0;
								resultInfo.message = "设置角色按钮添加失败！";
								break;
							}
						}					
				}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "设置角色按钮失败！";
				Sys.SaveErrorLog(ex, "设置角色按钮", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}
		#endregion

		#region 设置角色菜单
		/// <summary>
		/// 设置角色菜单
		/// </summary>
		/// <param name="mids"></param>
		/// <param name="rcode"></param>
		/// <returns></returns>
		public static BaseResult SetRoleMenu(string mids, string rcode) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					SysroleMenuMapService.DeleteroleMenuMap(rcode, context);				
						string[] sArray = mids.Split(',');
						foreach (string i in sArray) {
							SysroleMenuMap SysroleMenuMap = new SysroleMenuMap();
							SysroleMenuMap.MenuCode = i;
							SysroleMenuMap.RoleCode = rcode;
							int result = SysroleMenuMapService.Add(SysroleMenuMap,context);
							if (result == 0) {
								resultInfo.result = 0;
								resultInfo.message = "设置角色菜单添加失败！";
								break;
							}
						}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "设置角色菜单失败！";
				Sys.SaveErrorLog(ex, "设置角色菜单", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}
		#endregion

		#region 用户下设置角色
		/// <summary>
		/// 用户下设置角色
		/// </summary>
		/// <param name="mids"></param>
		/// <param name="rcode"></param>
		/// <returns></returns>
		public static BaseResult SetUserRole(string rids, string ucode) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					SysuserRoleMapService.DelsysuserRoleMap(ucode, context);
					string[] sArray = rids.Split(',');
					foreach (string i in sArray) {
						SysuserRoleMap SysuserRoleMap = new SysuserRoleMap();
						SysuserRoleMap.UserCode = ucode;
						SysuserRoleMap.RoleCode = i;
						int result = SysuserRoleMapService.Add(SysuserRoleMap, context);
						if (result == 0) {
							resultInfo.result = 0;
							resultInfo.message = "用户下设置角色添加失败！";
							break;
						}
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "用户下设置角色失败！";
				Sys.SaveErrorLog(ex, "用户下设置角色", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}
		#endregion

		#region 设置角色下用户
		/// <summary>
		/// 设置角色下用户
		/// </summary>
		/// <param name="mids"></param>
		/// <param name="rcode"></param>
		/// <returns></returns>
		public static BaseResult SetRoleUser(string uids, string rcode) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					SysuserRoleMapService.DelsysuserRoleMapbyrole(rcode, context);
						string[] sArray = uids.Split(',');
						foreach (string i in sArray) {
							SysuserRoleMap SysuserRoleMap = new SysuserRoleMap();
							SysuserRoleMap.UserCode = i;
							SysuserRoleMap.RoleCode = rcode;
							int result = SysuserRoleMapService.Add(SysuserRoleMap, context);
							if (result == 0) {
								resultInfo.result = 0;
								resultInfo.message = "设置角色下用户添加失败！";
								break;
							}
						}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "设置角色下用户失败！";
				Sys.SaveErrorLog(ex, "设置角色下用户", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}
		#endregion

		#region 当前用户 是否超级用户
		/// <summary>
		/// 当前用户 是否超级用户
		/// </summary>
		/// <returns> true 是  false  否</returns>
		public static bool IsSupper() {
			bool result = false;
			string usercode = FormsAuth.GetUserCode();
			Sysuser Sysuser = SysuserService.GetSysuserbyUserCode(usercode);
			if (Sysuser != null) {
				if (Sysuser.IsSupper == 1) {
					result = true;
				}
			}
			return result;

		}
		#endregion
	}
}