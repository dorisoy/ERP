using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 移位单商品实体类 前端交互使用
	/// </summary>
	public class MoveLocationItemWebInfo {
		/// <summary>
		/// 移位单号
		/// </summary>
		public string MoveLocationBillNo { get; set; }

		/// <summary>
		/// 移位单表主键ID
		/// </summary>
		public int MoveLocationID { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public int ProductsID { get; set; }

		/// <summary>
		/// 商品编码
		/// </summary>
		public string ProductsCode { get; set; }

		/// <summary>
		/// 商品名称
		/// </summary>
		public string ProductsName { get; set; }

		/// <summary>
		/// 商品货号
		/// </summary>
		public string ProductsNo { get; set; }

		/// <summary>
		/// 商品SkuID
		/// </summary>
		public int ProductsSkuID { get; set; }

		/// <summary>
		/// 商品SKU码
		/// </summary>
		public string ProductsSkuCode { get; set; }

		/// <summary>
		/// 商品销售属性
		/// </summary>
		public string ProductsSkuSaleprop { get; set; }

		/// <summary>
		/// 移入库位ID
		/// </summary>
		public int InLocationID { get; set; }

		/// <summary>
		/// 移入库位编码
		/// </summary>
		public string InLocationCode { get; set; }

		/// <summary>
		/// 移出库位ID
		/// </summary>
		public int[] OutLocationID { get; set; }

		/// <summary>
		/// 移出库位编码
		/// </summary>
		public string[] OutLocationCode { get; set; }

		/// <summary>
		/// 商品批次ID
		/// </summary>
		public int[] ProductsBatchID { get; set; }

		/// <summary>
		/// 商品批次号
		/// </summary>
		public string[] ProductsBatchCode { get; set; }

		/// <summary>
		/// 移出数量
		/// </summary>
		public int[] MoveNum { get; set; }
	}
}
