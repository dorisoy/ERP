using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// SKU库存情况实体类
	/// </summary>
	public class StockItemList : StockList {
		public StockItemList() { }

		/// <summary>
		/// SkuID
		/// </summary>
		public int ProductsSkuID { get; set; }

		/// <summary>
		/// 销售属性
		/// </summary>
		public string ProductsSkuSaleprop { get; set; }

		/// <summary>
		/// SKU码
		/// </summary>
		public string ProductsSkuCode { get; set; }
	}
}
