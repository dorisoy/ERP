using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Configuration;
using System.Threading;

namespace PaiXie.WinService {
	public class SysLocationProducts {

		//间隔5分钟
		public int autoDeleteIntervalMinutes = 1000 * 60 * ZConvert.StrToInt(ConfigurationManager.AppSettings["AutoDeleteIntervalMinutes"]);

		#region  删除在库数量为0的库位商品记录

		public void AutoDeleteLocationProducts() {
			while (true) {
				if (DateTime.Now.Hour >= 2 && DateTime.Now.Hour <= 3) {
					//测试
					common.WriteLog("删除数量为0的库位商品记录", LogType.General.ToString());
					WarehouseLocationProductsService.DeleteZeroRecord();
					Thread.Sleep(1000 * 3600 * 23);
				}
				else {
					Thread.Sleep(autoDeleteIntervalMinutes);
				}
			}
		}

		#endregion
	}
}
