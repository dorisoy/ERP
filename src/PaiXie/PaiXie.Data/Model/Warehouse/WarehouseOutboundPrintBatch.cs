using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 出库单打印批次表
	/// </summary>
	[Serializable]
	public partial class WarehouseOutboundPrintBatch {
		public WarehouseOutboundPrintBatch() { }


		private int _ID;
		/// <summary>
		/// 主键ID
		/// </summary>
		public int ID {
			set { _ID = value; }
			get { return _ID; }
		}


		private string _Code;
		/// <summary>
		/// 打印批次
		/// </summary>
		public string Code {
			set { _Code = value; }
			get { return _Code; }
		}


		private int _OutboundID;
		/// <summary>
		/// 出库单主键ID
		/// </summary>
		public int OutboundID {
			set { _OutboundID = value; }
			get { return _OutboundID; }
		}

		private string _CreatePerson;
		/// <summary>
		/// 创建人
		/// </summary>
		public string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}


		private DateTime _CreateDate;
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}
	}
}

