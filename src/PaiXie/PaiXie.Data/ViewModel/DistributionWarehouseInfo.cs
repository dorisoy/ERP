using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 分配仓库
	/// </summary>
	public class DistributionWarehouseInfo : Orditem {
		/// <summary>
		/// 未分配数量
		/// </summary>
		public int WfpNum { get; set; }

		/// <summary>
		/// 查货状态 0:有货 1:缺货 2:驳回
		/// </summary>
		public int CheckStatus { get; set; }

		/// <summary>
		/// 发货仓库
		/// </summary>
		public List<ProductsSkuInventory> warehouseProductsSkuInventoryList { get; set; }
	}
}
