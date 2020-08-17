using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 移位商品列表
	/// </summary>
	public class WarehouseMoveLocationItemList : WarehouseMoveLocationItem {
		
		/// <summary>
		/// 移出库位
		/// </summary>
		public string OutLocationCode { get; set; }

		/// <summary>
		/// 移入库位
		/// </summary>
		public string InLocationCode { get; set; }
	}
}
