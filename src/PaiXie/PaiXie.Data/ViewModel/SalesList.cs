using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 销售明细
	/// </summary>
	public class SalesList {
		/// <summary>
		/// ID
		/// </summary>
		public string ID { get; set; }

		/// <summary>
		/// 单据类型 1：销售 -1：售后
		/// </summary>
		public int BillType { get; set; }

		/// <summary>
		/// 关联单号
		/// </summary>
		public string BillNo { get; set; }

		/// <summary>
		/// 发货日期
		/// </summary>
		public string BillDate { get; set; }

		/// <summary>
		/// 商品编码
		/// </summary>
		public string ProductsCode { get; set; }

		/// <summary>
		/// 商品名称
		/// </summary>
		public string ProductsName { get; set; }

		/// <summary>
		/// 分类
		/// </summary>
		public string CategoryName { get; set; }

		/// <summary>
		/// 品牌
		/// </summary>
		public string BrandName { get; set; }

		/// <summary>
		/// 商品属性
		/// </summary>
		public string ProductsSkuSaleprop { get; set; }

		/// <summary>
		/// 商品SKU码
		/// </summary>
		public string ProductsSkuCode { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public int ProductsNum { get; set; }

        /// <summary>
        /// 销售额
        /// </summary>
		public decimal ProductsAmount { get; set; }

		/// <summary>
		/// 税率
		/// </summary>
		public decimal TaxRate { get; set; }

		/// <summary>
		/// 批次
		/// </summary>
		public string ProductsBatchCode { get; set; }

		/// <summary>
		/// 采购价
		/// </summary>
		public decimal CostPrice { get; set; }
	}
}
