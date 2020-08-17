using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 快递单信息实体类
	/// </summary>
	public class ExpressOrderInfo {

		/// <summary>
		/// 物流代码
		/// </summary>
		public string LogisticsCode { get; set; }

		/// <summary>
		/// 当前时间 yyyy-MM-dd HH:mm:ss
		/// </summary>
		public string CurrentTime { get; set; }

		/// <summary>
		/// 当前日期 yyyy-MM-dd
		/// </summary>
		public string CurrentDate { get; set; }

		/// <summary>
		/// 快递信息
		/// </summary>
		public WarehouseExpress ExpressInfo { get; set; }

		/// <summary>
		/// 寄件方信息
		/// </summary>
		public WarehouseConfig SendInfo { get; set; }		

		/// <summary>
		/// 出库单信息列表
		/// </summary>
		public List<WarehouseOutboundList> OutboundList { get; set; }
	}
}
