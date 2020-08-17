using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PaiXie.Core {
	/// <summary>
	/// 业务单据类型
	/// </summary>

	public enum BillType {
		
		[Description("采购入库")]
		CGR = 10,
		[Description("采购退回")]
		CGC = 20,
		[Description("其它入库")]
		QTR = 30,
		[Description("其它出库")]
		QTC = 40,
		[Description("退货入库")]
		THR = 50,
		[Description("销售出库")]
		XSC = 60,
		[Description("盘点")]
		PD = 70,
		[Description("移位")]
		YW = 80,
		[Description("调拨入库")]
		DBR = 90,
		[Description("调拨出库")]
		DBC = 100,
		[Description("采购计划单")]
		JH = 110,
		[Description("采购单")]
		CG = 120,
		[Description("商品转换")]
		ZH = 130,
		[Description("售后单")]
		SH = 140,
		[Description("收款单")]
		SK = 150,
		[Description("退款单")]
		TK = 160
	}
}
