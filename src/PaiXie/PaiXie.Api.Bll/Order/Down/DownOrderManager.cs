using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Api.Bll {
	public class DownOrderManager {
		/// <summary>
		/// 开启一个任务
		/// </summary>
		/// <param name="downParam">下载订单参数对象</param>
		public static void StartTask(DownOrderParam downParam) {
			BaseResult resultInfo = new BaseResult();
			try {
				bool createNewTask = false;
				ShopTask shopTask = ShopTaskService.GetSingleShopTask(downParam.ShopID, (int)ShopTaskType.下载订单);
				if (shopTask == null || shopTask.TaskStatus == (int)ShopTaskStatus.已结束) {
					createNewTask = true;
				}
				else {
					DateTime now = DateTime.Now;
					DateTime LastUpdateDate = shopTask.UpdateDate;
					if ((now - LastUpdateDate).TotalMinutes > 5 || shopTask.TotalCount == 0) {
						int rowsAffected = ShopTaskService.UpdateStatus(shopTask.TaskID, (int)ShopTaskStatus.已结束);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "结束下载订单超时任务失败！";
						}
						else {
							createNewTask = true;
						}
					}
				}
				if (createNewTask) {
					shopTask = new ShopTask();
					shopTask.TaskID = Guid.NewGuid().ToString(); ;
					shopTask.ShopID = downParam.ShopID;
					shopTask.TaskType = (int)ShopTaskType.下载订单;
					shopTask.TaskStatus = (int)ShopTaskStatus.进行中;
					shopTask.CreatePerson = downParam.UserCode;
					shopTask.CreateDate = DateTime.Now;
					shopTask.UpdateDate = DateTime.Now;
					int id = ShopTaskService.Add(shopTask);
					if (id == 0) {
						resultInfo.result = 0;
						resultInfo.message = "创建下载订单任务失败！";
					}
					else {
						Shop shop = ShopService.GetSingleShop(shopTask.ShopID);
						DownOrder downOrder = DownOrder(shop.PlatformType);
						downParam.TaskID = shopTask.TaskID;
						downOrder.downParam = downParam;
						downOrder.Download();

						ShopAutogeneration shopAuto = ShopAutogenerationService.GetSingleShopAutogeneration(downParam.ShopID);
						shopAuto.DownCompletionTime = DateTime.Now;
						shopAuto.UpdateDate = DateTime.Now;
						shopAuto.UpdatePerson = downParam.UserCode;
						ShopAutogenerationService.Update(shopAuto);
					}
				}
				if (resultInfo.result == 0) {
					Sys.SaveErrorLog(new Exception(resultInfo.message), "下载订单任务[" + (downParam.IsAuto == 0 ? "手动" : "自动") + "]", downParam.UserCode);
				}
			}
			catch (Exception ex) {
				Sys.SaveErrorLog(ex, "下载订单任务[" + (downParam.IsAuto == 0 ? "手动" : "自动") + "]", downParam.UserCode);
			}
		}

		/// <summary>
		/// 根据平台下载订单
		/// </summary>
		/// <param name="platformType">平台类型枚举值</param>
		/// <returns></returns>
		public static DownOrder DownOrder(int platformType) {
			DownOrder downOrder = null;
			switch (platformType) {
				case (int)ThirdApi.微小店:
					downOrder = new DownWeiXiaoDian();
					break;
			}
			return downOrder;
		}
	}
}
