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
	public partial class SysroleMenuButtonMap {
		public SysroleMenuButtonMap() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _RoleCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string RoleCode {
			set { _RoleCode = value; }
			get { return _RoleCode; }
		}

  
        
        private  string _ButtonCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ButtonCode {
			set { _ButtonCode = value; }
			get { return _ButtonCode; }
		}

		
	}
}

