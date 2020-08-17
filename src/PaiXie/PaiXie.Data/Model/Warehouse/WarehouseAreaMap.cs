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
	public partial class WarehouseAreaMap {
		public WarehouseAreaMap() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  int _WarehouseID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int WarehouseID {
			set { _WarehouseID = value; }
			get { return _WarehouseID; }
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

