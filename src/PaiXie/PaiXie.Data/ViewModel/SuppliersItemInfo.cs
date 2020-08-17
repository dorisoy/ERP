using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 供应商商品信息实体类
	/// </summary>
	public class SuppliersItemInfo : SuppliersItem {

		/// <summary>
		/// 供应商简称
		/// </summary>
		public string AliasName { get; set; }
	}
}
