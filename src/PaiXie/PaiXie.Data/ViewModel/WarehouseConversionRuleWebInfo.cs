using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 商品转换规则实体类 前端交互使用
	/// </summary>
	public class WarehouseConversionRuleWebInfo{
		/// <summary>
		/// 规则ID
		/// </summary>
		public int RuleID { get; set; }
		
		/// <summary>
		/// 规则名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 商品SkuID
		/// </summary>
		public int[] ProductsSkuID { get; set; }

		/// <summary>
		/// 商品SKU码
		/// </summary>
		public string[] ProductsSkuCode { get; set; }

		/// <summary>
		/// 最少转换数量
		/// </summary>
		public int[] Num { get; set; }

		/// <summary>
		/// 转换方向，左右可以互转 0：左边商品 1：右边商品
		/// </summary>
		public int[] ConversionWay { get; set; }

		/// <summary>
		/// 转换商品时的转换方向 0:左边商品转右边商品 1：右边商品转左边商品
		/// </summary>
		public int PermitTransformation { get; set; }
	}
}
