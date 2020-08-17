using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 库存更新 提示信息
	/// </summary>
	public class shopStockUpdateMsg {

		/// <summary>
		/// 是否成功 1  是
		/// </summary>
		public int result { get; set; }

		/// <summary>
		/// 商品数量
		/// </summary>
		public int pronum { get; set; }

		/// <summary>
		/// sku 数量
		/// </summary>
		public int skunum { get; set; }

		/// <summary>
		/// 成功数量 
		/// </summary>
		public int sucesssku { get; set; }

		/// <summary>
		/// 失败数量
		/// </summary>
		public int errorsku { get; set; }

		/// <summary>
		/// 更新时间
		/// </summary>
		public string updatetime { get; set; }

	}
}
