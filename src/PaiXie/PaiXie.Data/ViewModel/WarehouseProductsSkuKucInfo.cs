using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 仓库商品SKU库存信息实体类
	/// </summary>
	public class WarehouseProductsSkuKucInfo {
		/// <summary>
		/// 仓库编号
		/// </summary>
		public string WarehouseCode { get; set; }
		/// <summary>
		/// 仓库名称
		/// </summary>
		public string WarehouseName { get; set; }
		/// <summary>
		/// 商品状态 枚举 销售中 = 1,仓库中 = 2,
		/// </summary>
		public int ProductsStatus { get; set; }
		/// <summary>
		/// 销售属性
		/// </summary>
		public string Saleprop { get; set; }
		/// <summary>
		/// SKU码
		/// </summary>
		public string ProductsSkuCode { get; set; }
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
