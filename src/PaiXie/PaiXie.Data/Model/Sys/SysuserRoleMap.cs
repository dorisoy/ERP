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
	public partial class SysuserRoleMap {
		public SysuserRoleMap() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _UserCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string UserCode {
			set { _UserCode = value; }
			get { return _UserCode; }
		}

        
        private  string _RoleCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string RoleCode {
			set { _RoleCode = value; }
			get { return _RoleCode; }
		}

		
	}
}

