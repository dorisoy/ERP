using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 采购计划单商品列表
	/// </summary>
	public class WarehousePurchasePlanItemList : WarehousePurchasePlanItem {

		/// <summary>
		/// 供应商简称
		/// </summary>
		public string AliasName { get; set; }

		/// <summary>
		/// 当前可用库存
		/// </summary>
		public int KyNum { get; set; }
	}
}
