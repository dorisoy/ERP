using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 供应商商品列表
	/// </summary>
	public class SuppliersItemList {

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
		/// 货号
		/// </summary>
		public string ProductsNo { get; set; }

		/// <summary>
		/// 最低采购价
		/// </summary>
		public decimal MinPurchasePrice { get; set; }
		
		/// <summary>
		/// 最高采购价
		/// </summary>
		public decimal MaxPurchasePrice { get; set; }

		/// <summary>
		/// 到货周期(天)
		/// </summary>
		public int ArrivalCycle { get; set; }
	}
}
