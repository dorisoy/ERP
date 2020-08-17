using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 仓库出库单商品分页列表
	/// </summary>
	public class WarehouseOutStockItemList : WarehouseOutInStockItem {

		/// <summary>
		/// 库位编码
		/// </summary>
		public string LocationCode { get; set; }

		/// <summary>
		/// 当前可用库存
		/// </summary>
		public int KyNum { get; set; }
	}
}
