using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 仓库快递列表实体类
	/// </summary>
	public class WarehouseExpressList : WarehouseExpress {

		/// <summary>
		/// 物流公司名称
		/// </summary>
		public string LogisticsName { get; set; }

		/// <summary>
		/// 物流公司代码
		/// </summary>
		public string LogisticsCode { get; set; }

		/// <summary>
		/// 打印类型名称
		/// </summary>
		public string PrinterTypeName { get; set; }
	}
}