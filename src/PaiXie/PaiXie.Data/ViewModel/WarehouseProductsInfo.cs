using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 仓库商品列表
	/// </summary>
	public class WarehouseProductsInfo : Products{
		/// <summary>
		/// 是否添加预售 0：否 1：是
		/// </summary>
		public int IsBooking { get; set; }

		/// <summary>
		/// 预售商品的库存扣减模式：0：扣减 1：不扣减
		/// </summary>
		public int BookingModel { get; set; }
	}
}
