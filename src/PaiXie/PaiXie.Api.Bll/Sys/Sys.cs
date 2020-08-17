using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using PaiXie.Core;
using PaiXie.Utils;
using PaiXie.Service;
using Newtonsoft.Json;
using FluentData;
using System.Web;
namespace PaiXie.Api.Bll {
	public class Sys {
		#region 获取单据编号
		/// <summary>
		/// 获取单据编号
		/// </summary>
		/// <param name="typeNo">单据开头编码 例如：XSC 不需要开头就传 string.Empty</param>
		/// <returns></returns>
		public static string GetBillNo(string typeNo) {
			return GetBillNo(typeNo, 100);
		}

		/// <summary>
		/// 获取单据编号
		/// </summary>
		/// <param name="typeNo">单据开头编码 例如：XSC 不需要开头就传 string.Empty</param>
		/// <param name="maxLoop">出错时，递归最大次数，防止无限循环</param>
		/// <returns></returns>
		public static string GetBillNo(string typeNo, int maxLoop) {
			maxLoop--;//防止无限循环
			if (maxLoop < 1) {
				return "0";
			}
			string billNo = string.Empty;
			Sysbillno sysBillInfo = new Sysbillno();
			try {
				sysBillInfo.TypeNo = typeNo;
				sysBillInfo.BillNo = NewKey.datetime();
				sysBillInfo.CreateDate = DateTime.Now;
				SysbillnoService.Add(sysBillInfo);
				billNo = sysBillInfo.TypeNo + sysBillInfo.BillNo;
			}
			catch (Exception ex) {
				PlanLog.WriteLog(ex.ToString(), LogType.Error.ToString());
				billNo = GetBillNo(typeNo, maxLoop);
			}
			return billNo;
		} 
		#endregion

		#region 系统日志
		/// <summary>
		/// 系统日志
		/// </summary>
		/// <param name="Position">位置 例： api/mms/send</param>
		/// <param name="Target">名称 例： 菜单管理</param>
		/// <param name="ButtonName">事件名称 例：修改</param>
		/// <param name="OldMessage">旧的实体字符串 例： JsonConvert.SerializeObject(new Sysuser(), Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" })</param>
		/// <param name="Message">新的实体字符串 例： JsonConvert.SerializeObject(new Sysuser(), Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" })</param>
		/// <param name="Type">单据类型 例： (int)SyslogList.订单</param>
		/// <param name="Part1">扩展字段1</param>
		/// <param name="Part2">扩展字段2</param>
		/// <param name="Part3">扩展字段3</param>
		/// <param name="ModeType">系统类型 例：(int)ProjectType.管理端</param>
		/// <param name="WarehouseCode">仓库代码</param>
		/// <param name="context">数据库连接对象</param>
		public static void WriteSyslog(string Position, string Target, string ButtonName, string OldMessage, string Message,
			int Type, string Part1, string Part2, string Part3, int ModeType = (int)ProjectType.管理端, string WarehouseCode = "0", IDbContext context = null) {
			if (!OldMessage.Equals(Message)) {
				Syslog Syslog = new Syslog();
				Syslog.UserCode = FormsAuth.GetUserCode();
				Syslog.UserName = FormsAuth.GetUserName();
				Syslog.Position = Position;
				Syslog.Target = Target;
				Syslog.ButtonName = ButtonName;
				Syslog.OldMessage = OldMessage;
				Syslog.Message = Message;
				Syslog.Type = Type;
				Syslog.Part1 = Part1;
				Syslog.Part2 = Part2;
				Syslog.Part3 = Part3;
				Syslog.Date = System.DateTime.Now;
				Syslog.ModeType = ModeType;
				Syslog.WarehouseCode = WarehouseCode;
				SyslogService.Add(Syslog, context);
			}
		}
		#endregion

		#region 系统错误日志

		/// <summary>
		/// 保存系统错误日志
		/// </summary>
		/// <param name="ex">异常对象</param>
		/// <param name="createPerson">操作人</param>
		/// <returns>错误记录ID</returns>
		public static int SaveErrorLog(Exception currentError, string createPerson = null) {
			string Remark = string.Empty;
			return SaveErrorLog(currentError, Remark, createPerson);
		}

		/// <summary>
		/// 保存系统错误日志
		/// </summary>
		/// <param name="currentError">异常对象</param>
		/// <param name="Remark">备注</param>
		/// <param name="createPerson">操作人</param>
		/// <returns></returns>
		public static int SaveErrorLog(Exception currentError, string Remark, string createPerson = null) {
			HttpContext context = HttpContext.Current;
			string errorUrl = context == null || context.Handler == null ? string.Empty : context.Request.Url.ToString();
			string targetSite = currentError.TargetSite == null ? string.Empty : currentError.TargetSite.ToString();
			string stackTrace = currentError.StackTrace == null ? string.Empty : currentError.StackTrace;
			string friendlyMessage = currentError.Message == null ? string.Empty : currentError.Message;
			SyserrorLog entity = new SyserrorLog();
			entity.ErrorTitle = string.Empty;
			entity.ErrorUrl = errorUrl;
			entity.FriendlyMessage = friendlyMessage;
			entity.StackTrace = stackTrace;
			entity.TargetSite = targetSite;
			entity.Remark = Remark;
			entity.CreatePerson = createPerson;
			entity.CreateDate = DateTime.Now;
			int errorID = SyserrorLogService.Add(entity);
			return errorID;
		}

		#endregion


	}
}
