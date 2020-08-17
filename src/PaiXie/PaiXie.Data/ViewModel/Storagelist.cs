using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	public class Storagelist :WarehouseOutInStockItem {
		/// <summary>
		/// 采购数量
		/// </summary>
		public int PurchaseNum { get; set; }
		/// <summary>
		/// 已经入库数量  其他库位的入库数量
		/// </summary>
		public int AlreadyNum { get; set; }
		/// <summary>
		/// 库位编码
		/// </summary>
		public string  Locationcode { get; set; }
	}
}
