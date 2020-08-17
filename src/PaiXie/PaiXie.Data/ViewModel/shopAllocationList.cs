using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 商品 公共库存分配 提交视图
	/// </summary>
	public class shopAllocationList {

		//商品id
		public int ProductsID { set; get; }
		//店铺id
		public int ShopID { set; get; }
		//sku  id  
		public int[] ProductsSkuID { set; get; }
		//独享数量
		public int[] dxsl { set; get; }
		//是否 使用公用库存
		public int Isggkc { set; get; }

		
		
	}
	/// <summary>
	/// 独享库存  按店铺
	/// </summary>
	public class shopAllocationByShop {
		/// <summary>
		/// 店铺id
		/// </summary>
		public int ShopID { set; get; }
		/// <summary>
		/// 店铺名称
		/// </summary>
		public string ShopName { set; get; }
		/// <summary>
		/// 是否使用 公用的库存
		/// </summary>
		public string IsSalePub { set; get; }
		/// <summary>
		/// 独享数量
		/// </summary>
		public string xsnum { set; get; }
		

		
	}
}
