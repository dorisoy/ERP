using PaiXie.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using PaiXie.Service;
using FluentData;

namespace PaiXie.Api.Bll {

	/// <summary>
	/// 同步店铺发货管理类
	/// </summary>
	public class SendShopManager {

		/// <summary>
		/// 增加发货信息记录 一笔订单只记录 第一次发货
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static BaseResult Add(string userCode, string erpOrderCode, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				SendShop sendShop = SendShopService.GetQuerySingleByErpOrderCode(erpOrderCode, context);
				if (sendShop == null) {
					Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode, context);
					if (ordbase != null) {
						sendShop = new SendShop();
						sendShop.ErpOrderCode = erpOrderCode;
						sendShop.OutOrderCode = ordbase.OutOrderCode;
						sendShop.OrderSource = ordbase.OrderSource;
						sendShop.ShopID = ordbase.ShopID;
						sendShop.IsCod = ordbase.IsCod;
						sendShop.ExpressID = ordbase.ExpressID;
						sendShop.DeliveryExpressID = ordbase.DeliveryExpressID;
						sendShop.WaybillNo = ordbase.WaybillNo;
						sendShop.DeliveryDate = ordbase.DeliveryDate;
						sendShop.CreatePerson = userCode;
						sendShop.CreateDate = DateTime.Now;
						int sendShopID = SendShopService.Add(sendShop, context);
						if (sendShopID == 0) {
							resultInfo.result = 0;
							resultInfo.message = "系统订单号 " + erpOrderCode + " 增加发货信息记录失败！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "系统订单号 " + erpOrderCode + " 增加发货信息记录时，读取订单信息失败！";
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "系统订单号 " + erpOrderCode + " 增加发货信息记录时，程序出现异常！";
				Sys.SaveErrorLog(ex, "系统订单号 " + erpOrderCode + " 增加发货信息记录", userCode);
			}
			return resultInfo;
		}
	}
}
