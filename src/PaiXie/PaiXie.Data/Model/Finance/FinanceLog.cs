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
	public partial class FinanceLog {
		public FinanceLog() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  int _FinanceType;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int FinanceType {
			set { _FinanceType = value; }
			get { return _FinanceType; }
		}

        
        private  string _FinancePerson;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string FinancePerson {
			set { _FinancePerson = value; }
			get { return _FinancePerson; }
		}

        
        private  DateTime _CreateDate;
	    /// <summary>
	    /// 
	    /// </summary>
		public  DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
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

	}
}

