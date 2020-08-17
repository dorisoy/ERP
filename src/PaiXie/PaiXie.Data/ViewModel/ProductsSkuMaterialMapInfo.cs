using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 商品SKU关联物料实体
	/// </summary>
	public class ProductsSkuMaterialMapInfo : ProductsMaterialMap {
		/// <summary>
		/// 商品名称
		/// </summary>
		public string ProductsName { get; set; }
		/// <summary>
		/// 商品编码
		/// </summary>
		public string ProductsCode { get; set; }
		/// <summary>
		/// 商品小图地址
		/// </summary>
		public string SmallPic { get; set; }
		/// <summary>
		/// 商品属性
		/// </summary>
		public string Saleprop { get; set; }
		/// <summary>
		/// 商品SKU码
		/// </summary>
		public string ProductsSkuCode { get; set; }
		/// <summary>
		/// 商品SKU条码
		/// </summary>
		public string ProductsSkuBarCode { get; set; }
		/// <summary>
		/// 商品SKU销售价
		/// </summary>
		public decimal ProductsSkuSellingPrice { get; set; }
		/// <summary>
		/// 商品SKU成本价
		/// </summary>
		public decimal ProductsSkuCostPrice { get; set; }
		/// <summary>
		/// 被引用的商品SKU码
		/// </summary>
		public string FromProductsSkuCode { get; set; }
	}
}
