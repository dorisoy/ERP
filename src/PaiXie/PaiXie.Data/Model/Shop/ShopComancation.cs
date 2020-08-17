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
	public partial class ShopComancation {
		public ShopComancation() { }
		private int _ProductsID;
		/// <summary>
		/// 
		/// </summary>
		public int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
		}
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  int _ShopID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ShopID {
			set { _ShopID = value; }
			get { return _ShopID; }
		}

        
        private  string _Ranges;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Ranges {
			set { _Ranges = value; }
			get { return _Ranges; }
		}

        
        private  string _Remark;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}

		
	}
}

