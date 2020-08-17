using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	public class WarehouseMoveLocationList : WarehouseMoveLocation {

		/// <summary>
		/// 移位数量
		/// </summary>
		public int Num { get; set; }

		/// <summary>
		/// 创建人名称
		/// </summary>
		public string CreUserName { get; set; }

		/// <summary>
		/// 状态名称
		/// </summary>
		public string StatusName { get; set; }
	}
}
