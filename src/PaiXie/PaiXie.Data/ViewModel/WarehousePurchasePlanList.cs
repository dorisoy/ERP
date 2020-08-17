using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 采购计划单列表
	/// </summary>
	public class WarehousePurchasePlanList : WarehousePurchasePlan {

		/// <summary>
		/// 仓库名称
		/// </summary>
		public string WarehouseName { get; set; }
	}
}
