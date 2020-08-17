using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 批次商品详情实体类
	/// </summary>
	public class BatchItemList{
		public BatchItemList() { }

		/// <summary>
		/// 品牌ID
		/// </summary>
		public int BrandID { get; set; }

		/// <summary>
		/// 品牌名称
		/// </summary>
		public string BrandName { get; set; }

		/// <summary>
		/// 分类ID
		/// </summary>
		public int CategoryID { get; set; }

		/// <summary>
		/// 分类名称
		/// </summary>
		public string CategoryName { get; set; }

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
		/// 商品SkuID
		/// </summary>
		public int ProductsSkuID { get; set; }

		/// <summary>
		/// 商品SKU码
		/// </summary>
		public string ProductsSkuCode { get; set; }

		/// <summary>
		/// 商品税率
		/// </summary>
		public decimal TaxRate { get; set; }

		/// <summary>
		/// 商品属性
		/// </summary>
		public string ProductsSkuSaleprop { get; set; }

		/// <summary>
		/// 批次号
		/// </summary>
		public string BatchCode { get; set; }

		/// <summary>
		/// 成本价
		/// </summary>
		public decimal CostPrice { get; set; }

		/// <summary>
		/// 出库数量
		/// </summary>
		public int OutboundNum { get; set; }

		/// <summary>
		/// 入库数量
		/// </summary>
		public int StorageNum { get; set; }

		/// <summary>
		/// 销售数量
		/// </summary>
		public int SalesVolumes { get; set; }

		/// <summary>
		/// 调整数量
		/// </summary>
		public int AdjustQuantity { get; set; }

		/// <summary>
		/// 转出数量
		/// </summary>
		public int RollOutQuantity { get; set; }

		/// <summary>
		/// 转入数量
		/// </summary>
		public int QuantityOfTransfer { get; set; }

        /// <summary>
		/// 当前库存
		/// </summary>
		public int Inventory { get; set; }
	}
}
