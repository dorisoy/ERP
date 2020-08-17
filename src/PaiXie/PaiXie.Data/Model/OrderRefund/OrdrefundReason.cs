using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 无注释
	/// </summary>
	[Serializable]
	public partial class OrdrefundReason {
		public OrdrefundReason() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _OrdrefundValue;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  string OrdrefundValue {
			set { _OrdrefundValue = value; }
			get { return _OrdrefundValue; }
		}

	}
}

