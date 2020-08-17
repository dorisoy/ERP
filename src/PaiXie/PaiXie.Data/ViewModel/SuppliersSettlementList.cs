using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 供应商结算提交
	/// </summary>
	public class SuppliersSettlementList{

		private decimal _SettlementAmount;
		/// <summary>
		/// 
		/// </summary>
		public decimal SettlementAmount {
			set { _SettlementAmount = value; }
			get { return _SettlementAmount; }
		}


		private string _TradingNumber;
		/// <summary>
		/// 
		/// </summary>
		public string TradingNumber {
			set { _TradingNumber = value; }
			get { return _TradingNumber; }
		}


		private string _Remark;
		/// <summary>
		/// 
		/// </summary>
		public string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}


		private string _ids;
		/// <summary>
		/// 
		/// </summary>
		public string ids {
			set { _ids = value; }
			get { return _ids; }
		}
	}
}
