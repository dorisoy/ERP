using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 手动下单添加商品SKU实体类
	/// </summary>
	public class OrdProductsSkuList {
		/// <summary>
		/// 商品SkuID
		/// </summary>
		public int ProductsSkuID { get; set; }

		/// <summary>
		/// 商品SKU编码
		/// </summary>
		public string ProductsSkuCode { get; set; }

		/// <summary>
		/// 商品SKU属性
		/// </summary>
		public string ProductsSkuSaleprop { get; set; }

		/// <summary>
		/// 销售价
		/// </summary>
		public decimal SellingPrice { get; set; }

		/// <summary>
		/// 已下单数量
		/// </summary>
		public int ProductsNum { get; set; }

		/// <summary>
		/// 可下单数量
		/// </summary>
		public int KyNum { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public int[] Num { get; set; }
	}
}
