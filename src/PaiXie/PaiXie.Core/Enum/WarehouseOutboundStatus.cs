using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Core {

	/// <summary>
	/// 销售出库单状态枚举
	/// </summary>
	public enum WarehouseOutboundStatus {
		待拣货 = 0,
		待打印 = 10,
		待发货 = 20,
		已发货 = 30,
		已取消 = 99
	}
}
