using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 售后商品列表
	/// </summary>
	public class OrdRefundItemList : OrdrefundItem {
		/// <summary>
		/// 单位
		/// </summary>
		public string Unit { get; set; }

		/// <summary>
		/// 重量
		/// </summary>
		public decimal ProductsWeight { get; set; }
	}
}
