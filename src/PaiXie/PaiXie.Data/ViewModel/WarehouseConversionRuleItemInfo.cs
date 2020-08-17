using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 商品转换实体类
	/// </summary>
	public class WarehouseConversionRuleItemInfo : WarehouseConversionRuleItem {
		/// <summary>
		/// 中转区数量
		/// </summary>
		public int KyNum { get; set; }
	}
}
