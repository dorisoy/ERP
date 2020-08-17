using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace PaiXie.Api.Bll {
	public class AutogenerationOrderManager {
		/// <summary>
		/// 自动生成
		/// </summary>
		public static void AutogenerationTask() {
			try {
				List<ShopAutogeneration> shopAutoList = ShopAutogenerationService.GetManyShopAutogeneration().OrderBy(r => r.AutogenerationTime).ToList(); ;
				foreach (var shopAuto in shopAutoList) {
					shopAuto.AutogenerationTime = DateTime.Now;
					int rowsAffected = ShopAutogenerationService.Update(shopAuto);
					if (rowsAffected == 1) {
						if (shopAuto.GenerateCompletionTime > DateTime.Now.AddYears(-10) && shopAuto.GenerateCompletionTime > DateTime.Now.AddMinutes(-shopAuto.GenerateInterval)) {
							continue;
						}
						WaitCallback callBack = new WaitCallback(Autogeneration);
						Common.RunAsyn(callBack, shopAuto);
					}
				}
			}
			catch (Exception ex) {
				Sys.SaveErrorLog(ex, "自动下载订单", FormsAuth.GetUserCode());
			}
		}

		/// <summary>
		/// 异步执行生成任务
		/// </summary>
		/// <param name="state"></param>
		public static void Autogeneration(object state) {
			ShopAutogeneration shopAuto = (ShopAutogeneration)state;
			List<Ordouter> ordouterList = OrdouterService.GetManyOrdouterByTop(shopAuto.ShopID, 500);
			foreach (var ordouter in ordouterList) {
				if (Regex.IsMatch(shopAuto.NotGenerated, "\\b4\\b")) {
					if (ordouter.IsCod == 1) continue;
				}
				if (Regex.IsMatch(shopAuto.NotGenerated, "\\b5\\b")) {
					if (ordouter.IsNeedInvoice == 1) continue;
				}
				if (Regex.IsMatch(shopAuto.NotGenerated, "\\b6\\b")) {
					if (string.IsNullOrWhiteSpace(ordouter.BuyMessage)) continue;
				}
				if (Regex.IsMatch(shopAuto.NotGenerated, "\\b7\\b")) {
					if (string.IsNullOrWhiteSpace(ordouter.SellerRemark)) continue;
				}

				OrdbaseManager.Generate(ordouter.ID, "系统", "", true);
			}
		}
	}
}
