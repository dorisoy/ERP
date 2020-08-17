using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Core {
	/// <summary>
	/// 采购单状态
	/// </summary>
	public enum PurchaseStatus {
		未确认 = 0,
		已确认 = 10,
		已结束 = 20
	}
}
