using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 自动生成设置实体类 前端交互使用
	/// </summary>
	public class ShopAutogenerationWebInfo {
		/// <summary>
		/// 店铺ID
		/// </summary>
		public int ShopID { get; set; }

		/// <summary>
		/// 是否自动下载订单 0：否 1：是
		/// </summary>
		public int IsAutoDown { get; set; }

		/// <summary>
		/// 下载间隔
		/// </summary>
		public int DownInterval { get; set; }

		/// <summary>
		/// 下载几分钟前的订单
		/// </summary>
		public int CreateInterval { get; set; }

		/// <summary>
		/// 是否自动生成订单 0：否 1：是
		/// </summary>
		public int IsAutogeneration { get; set; }

		/// <summary>
		/// 生成间隔
		/// </summary>
		public int GenerateInterval { get; set; }

		/// <summary>
		/// 不自动生成情况 1：商品添加错误 2：未匹配发货物流 3：申请退款 4：货到付款 5：需要发票 6：有买家留言 7：有卖家备注
		/// </summary>
		public int[] NotGenerated { get; set; }
	}
}
