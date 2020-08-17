using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 添加商品SKU实体类 前端交互使用
	/// </summary>
	public class OrdProductsSkuWebInfo {
		/// <summary>
		/// 订单ID
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// 系统订单号
		/// </summary>
		public string ErpOrderCode { get; set; }

		/// <summary>
		/// 店铺ID
		/// </summary>
		public int ShopID { get; set; }

		/// <summary>
		/// 添加商品类型
		/// </summary>
		public int AddType { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public int ProductsID { get; set; }

		/// <summary>
		/// 商品SkuID
		/// </summary>
		public int[] ProductsSkuID { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public int[] Num { get; set; }

		/// <summary>
		/// 是否外部订单 0：否 1：是
		/// </summary>
		public int IsOutOrder { get; set; }

		/// <summary>
		/// 外部订单商品表ID
		/// </summary>
		public int OrdouterItemID { get; set; }
	}
}
