using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 入库单列表
	/// </summary>
	public class WarehouseOutInStockList : WarehouseOutInStock {
		/// <summary>
		/// 出入库类型名称
		/// </summary>
		public string BillTypeName { get; set; }
		/// <summary>
		/// 出入库数量
		/// </summary>
		public int OutInStockNum { get; set; }

		/// <summary>
		/// 状态名称
		/// </summary>
		public string StatusName { get; set; }
	}
}
