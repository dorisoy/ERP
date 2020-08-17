using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Core {
	/// <summary>
	/// 店铺任务类型
	/// </summary>
	public enum ShopTaskType {
		下载订单 = 1,
		下载商品 = 2,
		库存更新 = 3
	}
}