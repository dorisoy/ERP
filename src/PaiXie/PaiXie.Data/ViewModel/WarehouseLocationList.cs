using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	public class WarehouseLocationList : WarehouseLocation {
		/// <summary>
		/// 库位数量
		/// </summary>
		public int LocationNum { get; set; }

		/// <summary>
		/// 商品数量
		/// </summary>
		public int ProductsNum { get; set; }


		/// <summary>
		/// 库区类型名称
		/// </summary>
		public string TypeName { get; set; }
	}
}
