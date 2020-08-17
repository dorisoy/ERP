using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Core {

	/// <summary>
	/// 订单状态枚举
	/// </summary>
	public enum OrdBaseStatus {
		未生成 = 0,
		待审核 = 10,
		发货中 = 20,
		部分发货 = 30,
		已发货 = 40,
		已取消 = 99
	}
}
