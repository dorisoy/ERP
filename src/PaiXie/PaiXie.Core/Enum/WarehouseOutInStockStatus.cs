using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Core {
	/// <summary>
	/// 出入库单 状态
	/// </summary>
	public enum WarehouseOutInStockStatus {
		未提交 = 1,
		待审核 = 2,
		已审核=3
	}
}
