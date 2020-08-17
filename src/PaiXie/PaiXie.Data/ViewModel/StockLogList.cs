using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data.ViewModel {
	/// <summary>
	/// 库存日志实体类
	/// </summary>
	public class WarehouseOutInStockLogList : WarehouseOutInStockLog {
		public WarehouseOutInStockLogList() { }

		/// <summary>
		/// 单据类型名称
		/// </summary>
		public string BillTypeName { get; set; }

		/// <summary>
		/// 出入库方向名称
		/// </summary>
		public string StockWayName { get; set; }

		/// <summary>
		/// 库位编码
		/// </summary>
		public string LocationCode { get; set; }
	}
}
