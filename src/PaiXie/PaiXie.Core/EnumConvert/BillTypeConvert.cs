using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Core {
	public static class BillTypeConvert {
		#region 获取单据类型名称

		/// <summary>
		/// 获取单据类型名称
		/// </summary>
		/// <param name="billType">单据类型枚举值</param>
		/// <returns></returns>
		public static string GetBillTypeName(int billType) {
			string typeName = string.Empty;
			switch (billType) {
				case (int)BillType.CGR:
					typeName = "采购入库";
					break;
				case (int)BillType.QTR:
					typeName = "其它入库";
					break;
				case (int)BillType.CGC:
					typeName = "采购退回";
					break;
				case (int)BillType.QTC:
					typeName = "其它出库";
					break;
				case (int)BillType.XSC:
					typeName = "销售出库";
					break;
				case (int)BillType.THR:
					typeName = "退货入库";
					break;
				case (int)BillType.PD:
					typeName = "盘点";
					break;
				case (int)BillType.YW:
					typeName = "移位";
					break;
				case (int)BillType.DBR:
					typeName = "调拨入库";
					break;
				case (int)BillType.DBC:
					typeName = "调拨出库";
					break;
				case (int)BillType.CG:
					typeName = "采购单";
					break;
				case (int)BillType.JH:
					typeName = "采购计划单 ";
					break;
				case (int)BillType.ZH:
					typeName = "商品转换";
					break;
			}
			return typeName;
		}

		#endregion
	}
}
