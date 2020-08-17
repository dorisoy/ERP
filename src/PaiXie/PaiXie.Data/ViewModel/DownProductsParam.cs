using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	public class DownProductsParam {

		/// <summary>
		/// 任务Guid
		/// </summary>
		public string TaskID { get; set; }

		/// <summary>
		/// 店铺ID
		/// </summary>
		public int ShopID { get; set; }

		/// <summary>
		/// 操作用户
		/// </summary>
		public string UserCode { get; set; }

		/// <summary>
		/// 默认0全部 商品类型 1出售中(上架中)  2下架商品(除无货下架外)  3下架商品(缺货)  目前只有微小店才有区分2和3的两种下架，其它的下架都是2 
		/// </summary>
		public int ProductsStatus { get; set; }
	}
}
