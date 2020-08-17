using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 匹配快递设置实体类 前端交互使用
	/// </summary>
	public class ShopExpressSetWebInfo {
		/// <summary>
		/// 店铺ID
		/// </summary>
		public int ShopID { get; set; }

		/// <summary>
		/// 物流方式
		/// </summary>
		public int[] ShippingType { get; set; }

		/// <summary>
		/// 物流公司ID
		/// </summary>
		public int[] LogisticsID { get; set; }
	}
}
