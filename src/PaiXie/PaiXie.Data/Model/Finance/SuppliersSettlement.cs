using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class SuppliersSettlement {
		public SuppliersSettlement() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

		private int _SuppliersID;
	    /// <summary>
	    /// 
	    /// </summary>
		  public int SuppliersID {
			set { _SuppliersID = value; }
			get { return _SuppliersID; }
		}

		


        
        private  int _SourceID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int SourceID {
			set { _SourceID = value; }
			get { return _SourceID; }
		}

        
        private  string _SourceNO;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string SourceNO {
			set { _SourceNO = value; }
			get { return _SourceNO; }
		}

        
        private  decimal _SettlementAmount;
	    /// <summary>
	    /// 
	    /// </summary>
		public  decimal SettlementAmount {
			set { _SettlementAmount = value; }
			get { return _SettlementAmount; }
		}

        
        private  string _TradingNumber;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string TradingNumber {
			set { _TradingNumber = value; }
			get { return _TradingNumber; }
		}

        
        private  string _Remark;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}

        
        private  DateTime _SettlementTime;
	    /// <summary>
	    /// 
	    /// </summary>
		public  DateTime SettlementTime {
			set { _SettlementTime = value; }
			get { return _SettlementTime; }
		}

        
        private  string _SettlementPerson;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string SettlementPerson {
			set { _SettlementPerson = value; }
			get { return _SettlementPerson; }
		}

	}
}

