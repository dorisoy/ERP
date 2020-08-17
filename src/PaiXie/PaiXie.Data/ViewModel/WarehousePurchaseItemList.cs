using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 采购单商品列表
	/// </summary>
	public class WarehousePurchaseItemList : WarehousePurchaseItem {
		/// <summary>
		/// 当前可用库存
		/// </summary>
		public int KyNum { get; set; }

		/// <summary>
		/// 预计金额
		/// </summary>
		public decimal ExpectedAmount { get; set; }
	}
}
