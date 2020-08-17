using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 快递公司运费信息类 前端交互
	/// </summary>
	public class WarehouseExpressPriceWebInfo {

		/// <summary>
		/// 快递公司ID
		/// </summary>
		public int ExpressID { get; set; }

		/// <summary>
		/// 快递计费表主键ID列表
		/// </summary>
		public List<int> ID { get; set; }

		/// <summary>
		/// 地区名称列表
		/// </summary>
		public List<string> SysAreaNames { get; set; }

		/// <summary>
		/// 地区ID列表
		/// </summary>
		public List<string> SysAreaIDs { get; set; }

		/// <summary>
		/// 首重列表
		/// </summary>
		public List<decimal> FirstWeight { get; set; }

		/// <summary>
		/// 首价列表
		/// </summary>
		public List<decimal> FirstPrice { get; set; }

		/// <summary>
		/// 续重列表
		/// </summary>
		public List<decimal> ContinueWeight { get; set; }

		/// <summary>
		/// 续价列表
		/// </summary>
		public List<decimal> ContinuePrice { get; set; }
	}
}
