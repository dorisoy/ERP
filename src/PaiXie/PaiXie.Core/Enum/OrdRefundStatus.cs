using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Core {

	/// <summary>
	/// 售后状态
	/// </summary>
	public enum OrdRefundStatus {
		等待买家退货 = 10,
		等待卖家收货 = 20,
		收货异常 = 30,
		已完成 = 40,
		已取消 = 99
	}
}