using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 分配仓库 前端交互使用
	/// </summary>
	public class DistributionWarehouseWebInfo {
		/// <summary>
		/// 订单编号
		/// </summary>
		public string ErpOrderCode { get; set; }

		/// <summary>
		/// 订单ID
		/// </summary>
		public int OrdbaseID { get; set; }

		/// <summary>
		/// 订单明细ID
		/// </summary>
		public int[] OrditemID { get; set; }

		/// <summary>
		/// 商品SKUID
		/// </summary>
		public int[] ProductsSkuID { get; set; }

		/// <summary>
		/// 分配数量
		/// </summary>
		public int[] Num { get; set; }

		/// <summary>
		/// 仓库编号
		/// </summary>
		public string[] WarehouseCode { get; set; }
	}
}
