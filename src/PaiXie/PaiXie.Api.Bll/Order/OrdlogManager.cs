using PaiXie.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using PaiXie.Utils;
using PaiXie.Service;
using FluentData;

namespace PaiXie.Api.Bll {

	/// <summary>
	/// 订单操作日志管理类
	/// </summary>
	public class OrdlogManager {

		#region 保存订单操作日志

		/// <summary>
		/// 保存订单操作日志
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="outOrderCode">外部订单号</param>
		/// <param name="message">操作内容</param>
		/// <param name="context">数据库连接对象</param>
		/// <param name="warehouseCode">仓库编号 没有可以不传</param>
		/// <param name="billNo">出库单号 没有可以不传</param>
		/// <returns></returns>
		public static BaseResult Save(string userCode, string userName, string erpOrderCode, string outOrderCode, string message, IDbContext context = null, string warehouseCode = "", string billNo = "") {
			BaseResult resultInfo = new BaseResult();
			string orderStr = "系统订单号：" + erpOrderCode + "，外部订单号：" + outOrderCode;
			if (billNo != "") orderStr += "，出库单号：" + billNo;
			try {
				Ordlog ordlog = new Ordlog();
				ordlog.ErpOrderCode = erpOrderCode;
				ordlog.OutOrderCode = outOrderCode;
				ordlog.BillNo = billNo;
				ordlog.WarehouseCode = warehouseCode;
				ordlog.UserCode = userCode;
				ordlog.UserName = userName;
				ordlog.Message = message;
				ordlog.CreateDate = DateTime.Now;
				try {
					ordlog.UserIP = ZHttp.ClientIP;
				}
				catch {
					ordlog.UserIP = "0.0.0.0";
				}
				int ordlogID = OrdlogService.Add(ordlog, context);
				if (ordlogID == 0) {
					resultInfo.result = 0;
					resultInfo.message = orderStr + "保存订单操作日志失败！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = orderStr + " 保存订单操作日志时，程序出现异常！";
				Sys.SaveErrorLog(ex, orderStr + " 保存订单操作日志", userCode);
			}
			return resultInfo;
		}

		#endregion
	}
}
