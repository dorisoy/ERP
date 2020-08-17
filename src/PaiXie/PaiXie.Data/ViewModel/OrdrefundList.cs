using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 售后列表
	/// </summary>
	public class OrdRefundList : Ordrefund {

		/// <summary>
		/// 仓库名称
		/// </summary>
		public string WarehouseName { get; set; }

		/// <summary>
		/// 店铺名称
		/// </summary>
		public string ShopName { get; set; }

		/// <summary>
		/// 售后类型名称
		/// </summary>
		public string RefundTypeName { get; set; }

		/// <summary>
		/// 责任方名称
		/// </summary>
		public string DutyName { get; set; }

		/// <summary>
		/// 状态名称
		/// </summary>
		public string StatusName { get; set; }

	}
}
