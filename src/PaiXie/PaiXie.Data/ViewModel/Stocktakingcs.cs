using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 盘点异常原因
	/// </summary>
	public class StocktakingcsAbnormal {
		/// <summary>
		/// 占用单据
		/// </summary>
		public string BillNo{get;set;}
		/// <summary>
		/// 单据类型
		/// </summary>
		public int BillType{get;set;}
		/// <summary>
		/// 单据类型名称
		/// </summary>
		public string BillTypeName { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public int Status{get;set;}
		/// <summary>
		/// 状态
		/// </summary>
		public string StatusName { get; set; }
		/// <summary>
		/// 占用数量
		/// </summary>
		public int Num { get; set; }
	}
}
