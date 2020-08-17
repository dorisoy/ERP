using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 移位商品SKU列表
	/// </summary>
	public class MoveLocationProductsList : WarehouseLocationProducts {

		/// <summary>
		/// 库位编码
		/// </summary>
		public string LocationCode { get; set; }
	}
}
