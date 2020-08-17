using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 出库单商品实体类 前端交互使用
	/// </summary>
	public class OutStockLocationProductsWebInfo {
		
		/// <summary>
		/// 出库单号
		/// </summary>
		public string OutInStockBillNo { get; set; }

		/// <summary>
		/// 出库单表主键ID
		/// </summary>
		public int OutInStockID { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public int ProductsID { get; set; }

		/// <summary>
		/// 商品编码
		/// </summary>
		public string ProductsCode { get; set; }

		/// <summary>
		/// 商品名称
		/// </summary>
		public string ProductsName { get; set; }

		/// <summary>
		/// 商品货号
		/// </summary>
		public string ProductsNo { get; set; }

		/// <summary>
		/// 商品SkuID
		/// </summary>
		public int ProductsSkuID { get; set; }

		/// <summary>
		/// 商品SKU码
		/// </summary>
		public string ProductsSkuCode { get; set; }

		/// <summary>
		/// 商品销售属性
		/// </summary>
		public string ProductsSkuSaleprop { get; set; }

		/// <summary>
		/// 库位ID
		/// </summary>
		public int[] LocationID { get; set; }

		/// <summary>
		/// 库位编码
		/// </summary>
		public string[] LocationCode { get; set; }

		/// <summary>
		/// 商品批次ID
		/// </summary>
		public int[] ProductsBatchID { get; set; }

		/// <summary>
		/// 商品批次号
		/// </summary>
		public string[] ProductsBatchCode { get; set; }

		/// <summary>
		/// 出库数量
		/// </summary>
		public int[] OutNum { get; set; }
	}
}
