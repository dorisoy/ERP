using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 供应商审核
	/// </summary>
	public class SuppliersShList : WarehouseOutInStock {

		/// <summary>
		/// 单据状态
		/// </summary>
		public string  BillTypename { get; set; }
		/// <summary>
		/// 供应商名称
		/// </summary>
		public string Suppliersname { get; set; }

		/// <summary>
		/// 仓库名称
		/// </summary>
		public string Warehousename { get; set; }


		/// <summary>
		/// 数量
		/// </summary>
		public int totalnum { get; set; }

		/// <summary>
		/// 总金额
		/// </summary>
		public decimal totalPrice { get; set; }
	
		/// <summary>
		/// 核对状态
		/// </summary>
		public string shenheName { get; set; }

		/// <summary>
		/// 结算状态
		/// </summary>
		public string  SettlementName { get; set; }



		private decimal _SettlementAmount;
		/// <summary>
		/// 金额
		/// </summary>
		public decimal SettlementAmount {
			set { _SettlementAmount = value; }
			get { return _SettlementAmount; }
		}


		private string _TradingNumber;
		/// <summary>
		/// 交易号
		/// </summary>
		public string TradingNumber {
			set { _TradingNumber = value; }
			get { return _TradingNumber; }
		}


		private string _JsRemark;
		/// <summary>
		/// 结算备注
		/// </summary>
		public string JsRemark {
			set { _JsRemark = value; }
			get { return _JsRemark; }
		}


		private DateTime _SettlementTime;
		/// <summary>
		/// 结算时间
		/// </summary>
		public DateTime SettlementTime {
			set { _SettlementTime = value; }
			get { return _SettlementTime; }
		}

	}
}
