using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Api.Bll {
	public class OrdouterManager {

		#region 删除外部订单

		/// <summary>
		/// 删除外部订单
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static BaseResult Delte(int id) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					Ordouter ordouter = OrdouterService.GetQuerySingleByID(id, context);
					Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(ordouter.ErpOrderCode, context);
					resultInfo = OrdbaseManager.Delte(ordbase.ID, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
					
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "删除外部订单", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}

		/// <summary>
		/// 删除外部订单
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static BaseResult Delte(string erpOrderCode, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				Ordouter ordouter = OrdouterService.GetQuerySingleByErpOrderCode(erpOrderCode, context);
				if (ordouter != null) {
					int rowsAffected = OrdouterService.DelByID(ordouter.ID, context);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "删除订单失败！";
					}
					else {
						#region 订单操作日志

						string msg = string.Format("删除外部订单");
						resultInfo = OrdlogManager.Save(FormsAuth.GetUserCode(), FormsAuth.GetUserName(), ordouter.ErpOrderCode, ordouter.OutOrderCode, msg, context);

						#endregion
					}

					if (resultInfo.result == 1) {
						List<OrdouterItem> itemList = OrdouterItemService.GetManyOrdouterItem(ordouter.ID, context);
						foreach (var item in itemList) {
							rowsAffected = OrdouterItemService.DelByID(item.ID, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "删除外部订单商品失败！";
								break;
							}
							else {
								#region 订单操作日志

								string msg = string.Format("删除外部订单商品（SKU码：{0}）", item.ProductsSkuCode);
								resultInfo = OrdlogManager.Save(FormsAuth.GetUserCode(), FormsAuth.GetUserName(), item.ErpOrderCode, item.OutOrderCode, msg, context);

								#endregion
							}
						}
					}
				}
			}
			catch (Exception ex) {
				throw new Exception(ex.Message + ex.StackTrace);
			}
			return resultInfo;
		}

		#endregion
	}
}
