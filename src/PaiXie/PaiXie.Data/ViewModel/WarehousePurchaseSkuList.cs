using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 采购单商品SKU实体 添加商品时搜索SKU列表
	/// </summary>
	public class WarehousePurchaseSkuList {
		/// <summary>
		/// 商品SKU表主键ID
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
		/// 当前库存
		/// </summary>
		public int KyNum { get; set; }
		
		/// <summary>
		/// SKU采购价
		/// </summary>
		public decimal PurchasePrice { get; set; }
	}
}
