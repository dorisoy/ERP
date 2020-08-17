using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PaiXie.Api.Bll {
	public class AutoDownOrderManager {
		/// <summary>
		/// 自动下载订单
		/// </summary>
		public static void AutoDownOrderTask() {
			try {
				List<ShopAutogeneration> shopAutoList = ShopAutogenerationService.GetManyShopAutogeneration().OrderBy(r => r.AutoDownTaskTime).ToList();
				foreach (var shopAuto in shopAutoList) {
					shopAuto.AutoDownTaskTime = DateTime.Now;
					int rowsAffected = ShopAutogenerationService.Update(shopAuto);
					if (rowsAffected == 1) {
						if (shopAuto.DownCompletionTime > DateTime.Now.AddYears(-10) && shopAuto.DownCompletionTime > DateTime.Now.AddMinutes(-shopAuto.DownInterval)) {
							continue;
						}
						Shop shop = ShopService.GetSingleShop(shopAuto.ShopID);
						DateTime now = DateTime.Now;
						DownOrderParam downParam = new DownOrderParam();
						downParam.ShopID = shop.ID;
						downParam.StartDate = now.AddDays(-6).AddMinutes(-shopAuto.CreateInterval);
						downParam.EndDate = now.AddMinutes(-shopAuto.CreateInterval);
						downParam.PageNo = 1;
						downParam.IsAuto = 1;
						downParam.DateType = 1;
						downParam.UserCode = "系统";
						WaitCallback callBack = new WaitCallback(theadFunc);
						Common.RunAsyn(callBack, downParam);
					}
				}
			}
			catch (Exception ex) {
				Sys.SaveErrorLog(ex, "自动下载订单", FormsAuth.GetUserCode());
			}
		}
        
		/// <summary>
		/// 异步执行下载任务
		/// </summary>
		/// <param name="state"></param>
		public static void theadFunc(object state) {
			DownOrderManager.StartTask((DownOrderParam)state);
		}
	}
}
