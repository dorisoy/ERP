using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Core {

	/// <summary>
	/// 货到付款状态枚举
	/// </summary>
	public enum CodStatus {
		未收货 = 0,
		部分收货 = 1,
		已收货 = 2,
		已拒收 = 3
	}
}