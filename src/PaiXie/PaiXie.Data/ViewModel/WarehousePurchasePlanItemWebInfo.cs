using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 采购计划单商品实体类 前端交互使用
	/// </summary>
	public class WarehousePurchasePlanItemWebInfo {
		/// <summary>
		/// 采购计划单号
		/// </summary>
		public string BillNo { get; set; }

		/// <summary>
		/// 采购计划单表主键ID
		/// </summary>
		public int PlanID { get; set; }

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
		/// 供应商ID
		/// </summary>
		public int[] SuppliersID { get; set; }

		/// <summary>
		/// 计划采购数量
		/// </summary>
		public int[] Num { get; set; }
	}
}
