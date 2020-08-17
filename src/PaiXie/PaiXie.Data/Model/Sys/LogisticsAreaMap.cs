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
	public partial class LogisticsAreaMap {
		public LogisticsAreaMap() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  int _LogisticsID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int LogisticsID {
			set { _LogisticsID = value; }
			get { return _LogisticsID; }
		}

        
        private  int _AreaID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int AreaID {
			set { _AreaID = value; }
			get { return _AreaID; }
		}

		
	}
}

