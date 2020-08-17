using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 预售商品SKU实体类 前端交互使用（例如添加和编辑商品页面）
	/// </summary>
	public class WarehouseBookingProductsWebInfo {
		/// <summary>
		/// 商品ID
		/// </summary>
		public int ProductsID { get; set; }

		/// <summary>
		/// 折扣模式
		/// </summary>
		public int BookingModel { get; set; }

		/// <summary>
		/// 商品SkuID
		/// </summary>
		public string[] ProductsSkuID { get; set; }

		/// <summary>
		/// 预售数量
		/// </summary>
		public string[] BookingNum { get; set; }
	}
}
