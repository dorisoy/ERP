using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 店铺商品列表
	/// </summary>
	public class ShopProductsList : ShopUpdateProducts {
		private string _ShopName;
		/// <summary>
		/// 店铺名称
		/// </summary>
		public string ShopName {
			set { _ShopName = value; }
			get { return _ShopName; }
		}
	}



	/// <summary>
	/// 店铺更新库存商品列表
	/// </summary>
	public class ShopProductsupdateList : ShopProductsList {
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpdateTime { set; get; }
		/// <summary>
		/// 更新状态  1 更新成功   0  更新失败
		/// </summary>
		public string   UpdateStatus { set; get; }
		/// <summary>
		/// 错误消息
		/// </summary>
		public string ErrorMsg { set; get; }
		/// <summary>
		/// 店铺id
		/// </summary>
		public int ShopID { set; get; }

		/// <summary>
		/// 平台类型 枚举
		/// </summary>
		public int PlatformType { set; get; }

	}
}
