using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 发货单信息实体类
	/// </summary>
	public class DeliveryOrderInfo {

		/// <summary>
		/// 当前时间 yyyy-MM-dd HH:mm:ss
		/// </summary>
		public string CurrentTime { get; set; }

		/// <summary>
		/// 当前日期 yyyy-MM-dd
		/// </summary>
		public string CurrentDate { get; set; }

		/// <summary>
		/// 打印模版信息
		/// </summary>
		public WarehousePrintTemplate PrintTemplateInfo { get; set; }

		/// <summary>
		/// 出库单信息列表
		/// </summary>
		public List<WarehouseOutboundList> OutboundList { get; set; }
	}
}
