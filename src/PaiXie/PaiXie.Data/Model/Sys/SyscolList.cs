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
	public partial class SyscolList {
		public SyscolList() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _ViewName;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ViewName {
			set { _ViewName = value; }
			get { return _ViewName; }
		}

        
        private  string _ViewCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ViewCode {
			set { _ViewCode = value; }
			get { return _ViewCode; }
		}

		
	}
}

