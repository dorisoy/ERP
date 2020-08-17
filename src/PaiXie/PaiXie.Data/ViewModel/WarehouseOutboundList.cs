using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 出库单列表
	/// </summary>
	public class WarehouseOutboundList : WarehouseOutbound {

		/// <summary>
		/// 出库单状态名称
		/// </summary>
		public string StatusName { get; set; }

		/// <summary>
		/// 店铺名称
		/// </summary>
		public string ShopName { get; set; }

		/// <summary>
		/// 下单选择发货快递名称
		/// </summary>
		public string ExpressName { get; set; }

		/// <summary>
		/// 实际发货快递名称
		/// </summary>
		public string DeliveryExpressName { get; set; }

		/// <summary>
		/// $货到付款金额(大写)
		/// </summary>
		public string UncollectedeAmountChinese { get; set; }

		/// <summary>
		/// 商品明细
		/// </summary>
		public string ProList { get; set; }
	}
}
