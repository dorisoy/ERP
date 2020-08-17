using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 商品SKU信息
	/// </summary>
	public class ProductsSkuInfo : ProductsSku {
		
		/// <summary>
		/// 商品名称
		/// </summary>
		public string ProductsName { get; set; }
		
		/// <summary>
		/// 商品货号
		/// </summary>
		public string ProductsNo { get; set; }
	}
}
