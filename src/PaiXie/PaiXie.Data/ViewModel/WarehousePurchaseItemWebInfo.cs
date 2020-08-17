using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 采购单商品实体类 前端交互使用
	/// </summary>
	public class WarehousePurchaseItemWebInfo {
		/// <summary>
		/// 采购单号
		/// </summary>
		public string BillNo { get; set; }

		/// <summary>
		/// 采购单表主键ID
		/// </summary>
		public int PurchaseID { get; set; }

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
		public int[] ProductsSkuID { get; set; }

		/// <summary>
		/// 商品SKU码
		/// </summary>
		public string[] ProductsSkuCode { get; set; }

		/// <summary>
		/// 商品销售属性
		/// </summary>
		public string[] ProductsSkuSaleprop { get; set; }

		/// <summary>
		/// 采购数量
		/// </summary>
		public int[] Num { get; set; }
	}
}