using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 出库库位商品列表
	/// </summary>
	public class OutStockLocationProductsList : WarehouseLocationProducts {

		/// <summary>
		/// 库位编码
		/// </summary>
		public string LocationCode { get; set; }

		/// <summary>
		/// 出库数量
		/// </summary>
		public int OutNum { get; set; }
	}
}
