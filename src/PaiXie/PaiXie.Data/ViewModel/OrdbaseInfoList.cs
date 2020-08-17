using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 订单信息
	/// </summary>
	public class OrdbaseInfoList : Ordbase {
		/// <summary>
		/// 店铺名称
		/// </summary>
		public string ShopName { get; set; }

		/// <summary>
		/// 发货物流
		/// </summary>
		public string LogisticsName { get; set; }

		/// <summary>
		/// 订单状态
		/// </summary>
		public string StrOrderStatus { get; set; }

		/// <summary>
		/// 是否有系统备注 0：否 1：有
		/// </summary>
		public int IsSysRemark { get; set; }
	}
}
