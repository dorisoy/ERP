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
	public partial class SysmenuButtonMap {
		public SysmenuButtonMap() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _ButtonCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ButtonCode {
			set { _ButtonCode = value; }
			get { return _ButtonCode; }
		}

        
        private  string _MenuCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string MenuCode {
			set { _MenuCode = value; }
			get { return _MenuCode; }
		}

		
	}
}

