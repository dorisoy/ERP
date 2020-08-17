using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 库位商品库存信息
	/// </summary>
	public class LocationProductsKucInfo {

		/// <summary>
		/// 商品编码
		/// </summary>
		public string ProductsCode { get; set; }

		/// <summary>
		/// 商品名称
		/// </summary>
		public string ProductsName { get; set; }

		/// <summary>
		/// 商品属性
		/// </summary>
		public string Saleprop { get; set; }

		/// <summary>
		/// 商品SKU码
		/// </summary>
		public string ProductsSkuCode { get; set; }

		/// <summary>
		/// 入库批次
		/// </summary>
		public string ProductsBatchCode { get; set; }

		/// <summary>
		/// 库位数量
		/// </summary>
		public int ZkNum { get; set; }

		/// <summary>
		/// 占用数量
		/// </summary>
		public int ZyNum { get; set; }

		/// <summary>
		/// 冻结数量
		/// </summary>
		public int DjNum { get; set; }
	}
}
