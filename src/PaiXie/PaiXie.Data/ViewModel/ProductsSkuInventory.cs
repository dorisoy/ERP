using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 商品SKU库存信息实体类
	/// </summary>
	public class ProductsSkuInventory {
		/// <summary>
		/// 仓库编号
		/// </summary>
		public string WarehouseCode { get; set; }

		/// <summary>
		/// 仓库名称
		/// </summary>
		public string WarehouseName { get; set; }

		/// <summary>
		/// 可用库存
		/// </summary>
		public int KyNum { get; set; }
		
		/// <summary>
		/// 占用库存
		/// </summary>
		public int ZyNum { get; set; }
		
		/// <summary>
		/// 订单占用
		/// </summary>
		public int OrdZyNum { get; set; }
		
		/// <summary>
		/// 预售可用
		/// </summary>
		public int BookingKyNum { get; set; }

		/// <summary>
		/// 冻结数量
		/// </summary>
		public int DjNum { get; set; }
	}
}
