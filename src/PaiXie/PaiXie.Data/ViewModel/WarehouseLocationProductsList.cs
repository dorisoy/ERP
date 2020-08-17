using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 库位商品列表
	/// </summary>
	public class WarehouseLocationProductsList {

		/// <summary>
		/// 库位ID
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// 库位编码
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// 库位名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 商品名称
		/// </summary>
		public string ProductsName { get; set; }

		/// <summary>
		/// 商品编码
		/// </summary>
		public string ProductsCode { get; set; }

		/// <summary>
		/// 商品属性
		/// </summary>
		public string Saleprop { get; set; }

		/// <summary>
		/// 商品SKU码
		/// </summary>
		public string ProductsSkuCode { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public int ZkNum { get; set; }

		/// <summary>
		/// 批次
		/// </summary>
		public string ProductsBatchCode { get; set; }
	}
}
