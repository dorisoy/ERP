using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data {
	[Serializable]
	public partial class Sysbillno {
		public Sysbillno() { }


		private int _ID;

		public int ID {
			set { _ID = value; }
			get { return _ID; }
		}


		private string _TypeNo;

		public string TypeNo {
			set { _TypeNo = value; }
			get { return _TypeNo; }
		}


		private string _BillNo;

		public string BillNo {
			set { _BillNo = value; }
			get { return _BillNo; }
		}


		private DateTime _CreateDate;

		public DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}
	}
}

