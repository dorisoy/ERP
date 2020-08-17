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
	public partial class SysroleMenuMap {
		public SysroleMenuMap() { }
        
        
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

