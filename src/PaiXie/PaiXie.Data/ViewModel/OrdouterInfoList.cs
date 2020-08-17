using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 下载订单信息
	/// </summary>
	public class OrdouterInfoList : Ordouter {
		/// <summary>
		/// 店铺名称
		/// </summary>
		public string ShopName { get; set; }

		/// <summary>
		/// 订单商品是否添加完成
		/// </summary>
		public int IsProductAddFin { get; set; }

		/// <summary>
		/// 物流ID
		/// </summary>
		public int LogisticsID { get; set; }

		/// <summary>
		/// 物流名称
		/// </summary>
		public string LogisticsName { get; set; }
	}
}
