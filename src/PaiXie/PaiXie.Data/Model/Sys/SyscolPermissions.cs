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
	public partial class SyscolPermissions {
		public SyscolPermissions() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _ColumnFild;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ColumnFild {
			set { _ColumnFild = value; }
			get { return _ColumnFild; }
		}

        
        private  string _ColumnName;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ColumnName {
			set { _ColumnName = value; }
			get { return _ColumnName; }
		}

        
        private  string _ViewCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ViewCode {
			set { _ViewCode = value; }
			get { return _ViewCode; }
		}

        
        private  int _Seq;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int Seq {
			set { _Seq = value; }
			get { return _Seq; }
		}

		
	}
}

