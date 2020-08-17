using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 商品库存信息实体类
	/// </summary>
	public class ProductsInventory {
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
	}
}
