using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 采购单列表
	/// </summary>
	public class WarehousePurchaseList : WarehousePurchase {

		/// <summary>
		/// 仓库名称
		/// </summary>
		public string WarehouseName { get; set; }

		/// <summary>
		/// 供应商简称
		/// </summary>
		public string AliasName { get; set; }
	}
}
