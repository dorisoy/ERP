using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 订单优惠实体类 前端交互使用
	/// </summary>
	public class OrddiscountWebInfo {
		/// <summary>
		/// 系统订单号
		/// </summary>
		public string ErpOrderCode { get; set; }

		/// <summary>
		/// 订单表主键ID
		/// </summary>
		public int OrdbaseID { get; set; }

		/// <summary>
		/// 优惠备注
		/// </summary>
		public string Remark { get; set; }
        
		/// <summary>
		/// 优惠类型 0：直减金额 1：订单包邮
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// 优惠金额
		/// </summary>
		public decimal Amount { get; set; }
        
		/// <summary>
		/// 关联商品SKUID
		/// </summary>
		public int[] ProductsSkuID { get; set; }

		/// <summary>
		/// 关联商品SKU码
		/// </summary>
		public string LibProductsSkuCode { get; set; }

	}
}
