using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Core {
	/// <summary>
	/// 盘点单状态
	/// </summary>
	public enum StocktakingStatus {
		未确认 = 0,
		待审核 = 10,
		已审核 = 20
	}
}
