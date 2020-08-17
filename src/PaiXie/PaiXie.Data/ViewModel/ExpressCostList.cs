using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	public class ExpressCostList {

		/// <summary>
		/// 出库单ID
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// 出库单号
		/// </summary>
		public string BillNo { get; set; }

		/// <summary>
		/// 订单号
		/// </summary>
		public string ErpOrderCode { get; set; }

		/// <summary>
		/// 仓库
		/// </summary>
		public string WarehouseName { get; set; }

		/// <summary>
		/// 快递公司
		/// </summary>
		public string ExpressName { get; set; }

		/// <summary>
		/// 运单号
		/// </summary>
		public string WaybillNo { get; set; }

		/// <summary>
		/// 运费
		/// </summary>
		public decimal ExpressFreight { get; set; }

		/// <summary>
		/// 手续费
		/// </summary>
		public decimal BuyCodFee { get; set; }

		/// <summary>
		/// 发货时间
		/// </summary>
		public DateTime DeliveryDate { get; set; }
	}
}
