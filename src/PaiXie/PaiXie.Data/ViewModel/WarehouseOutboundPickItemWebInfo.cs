using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 拣货明细实体类 出库单详情商品列表
	/// </summary>
	public class WarehouseOutboundPickItemWebInfo : WarehouseOutboundPickItem {

		/// <summary>
		/// 商品重量
		/// </summary>
		public decimal ProductsWeight { get; set; }

		/// <summary>
		/// 单位
		/// </summary>
		public string ProductsUnit { get; set; }

		 /// <summary>
	    /// 商品税率
	    /// </summary>
		public decimal TaxRate { get; set; }

		/// <summary>
		/// 优惠金额
		/// </summary>
		public decimal DiscountAmount { get; set; }

		/// <summary>
		/// 采购价
		/// </summary>
		public decimal CostPrice { get; set; }
	}
}
