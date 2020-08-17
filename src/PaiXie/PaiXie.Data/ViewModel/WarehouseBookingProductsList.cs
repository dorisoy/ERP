using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 预售商品实体类
	/// </summary>
	public class WarehouseBookingProductsList : WarehouseProductsInfo {
		/// <summary>
		/// 预售数量
		/// </summary>
		public int BookingNum { get; set; }

		/// <summary>
		/// 预售可用
		/// </summary>
		public int KyNum { get; set; }

		/// <summary>
		/// 预售占用
		/// </summary>
		public int ZyNum { get; set; }

		/// <summary>
		/// 预售冲抵
		/// </summary>
		public int CdNum { get; set; }
	}
}
