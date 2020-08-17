using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 商品SKU库存信息实体类
	/// </summary>
	public class ProductsSkuKucInfo {
		/// <summary>
		/// 商品SKU表标识
		/// </summary>
		public int ProductsSkuID { get; set; }
		/// <summary>
		/// 销售属性
		/// </summary>
		public string Saleprop { get; set; }
		/// <summary>
		/// SKU码
		/// </summary>
		public string ProductsSkuCode { get; set; }
		/// <summary>
		/// 关联物料数量
		/// </summary>
		public int ProductsMaterialMapCount { get; set; }
		/// <summary>
		/// 库存
		/// </summary>
		public int TotalNum { get; set; }
		/// <summary>
		/// 可用库存
		/// </summary>
		public int KyNum { get; set; }
		/// <summary>
		/// 占用库存
		/// </summary>
		public int ZyNum { get; set; }
		/// <summary>
		/// 冻结库存
		/// </summary>
		public int DjNum { get; set; }
		/// <summary>
		/// 预售可用
		/// </summary>
		public int YsNum { get; set; }
		/// <summary>
		/// 预售占用
		/// </summary>
		public int YsZyNum { get; set; }
		/// <summary>
		/// 备用库存
		/// </summary>
		public int ByNum { get; set; }
	}
}
