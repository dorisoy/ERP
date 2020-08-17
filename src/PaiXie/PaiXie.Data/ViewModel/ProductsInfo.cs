using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 商品信息实体类
	/// </summary>
	public class ProductsInfo {
		/// <summary>
		/// 商品信息
		/// </summary>
		public Products Products { get; set; }
		/// <summary>
		/// Sku列表
		/// </summary>
		public List<ProductsSku> ProductsSkuList = new List<ProductsSku>();
	}
}
