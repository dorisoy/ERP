using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 拣货单信息实体类
	/// </summary>
	public class PickOrderInfo {


		/// <summary>
		/// 当前时间 yyyy-MM-dd HH:mm:ss
		/// </summary>
		public string CurrentTime { get; set; }

		/// <summary>
		/// 当前日期 yyyy-MM-dd
		/// </summary>
		public string CurrentDate { get; set; }

		/// <summary>
		/// 出库单ID数组
		/// </summary>
		public int[] outboundIDs { get; set; }

		/// <summary>
		/// 打印批次
		/// </summary>
		public string PrintBatchCode { get; set; }

		/// <summary>
		/// 商品数量
		/// </summary>
		public int ProductsNum { get; set; }

		/// <summary>
		/// 打印商品明细
		/// </summary>
		public string ProList { get; set; }

		/// <summary>
		/// 打印模版信息
		/// </summary>
		public WarehousePrintTemplate PrintTemplateInfo { get; set; }
	}
}
