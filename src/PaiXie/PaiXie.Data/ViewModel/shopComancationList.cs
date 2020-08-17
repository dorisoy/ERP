using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 公共库存分配 提交视图
	/// </summary>
	public class shopComancationList {

		//店铺名称
		public string[] ShopName { set; get; }
		//店铺id
		public int[] ShopID { set; get; }
		//比例
		public string[] Ranges { set; get; }
		//备注信息
		public string Remark { set; get; }
		//商品id
		public int ProductsID { set; get; }

		
		
	}
}
