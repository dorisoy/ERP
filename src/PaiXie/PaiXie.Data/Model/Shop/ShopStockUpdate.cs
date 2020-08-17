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
	public partial class ShopStockUpdate {
		public ShopStockUpdate() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  DateTime _UpdateTime;
	    /// <summary>
	    /// 
	    /// </summary>
		public  DateTime UpdateTime {
			set { _UpdateTime = value; }
			get { return _UpdateTime; }
		}

        
        private  int _UpdateStatus;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int UpdateStatus {
			set { _UpdateStatus = value; }
			get { return _UpdateStatus; }
		}

        
        private  string _ErrorMsg;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ErrorMsg {
			set { _ErrorMsg = value; }
			get { return _ErrorMsg; }
		}

        
        private  int _ShopID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ShopID {
			set { _ShopID = value; }
			get { return _ShopID; }
		}


		private int _PlatformType;
	    /// <summary>
	    /// 
	    /// </summary>
		  public int PlatformType {
			set { _PlatformType = value; }
			get { return _PlatformType; }
		}

		  private int _SkuNum;
	    /// <summary>
	    /// 
	    /// </summary>
			public int SkuNum {
			  set { _SkuNum = value; }
			  get { return _SkuNum; }
		}
        

		


        private   string  _ProductsCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ProductsCode {
			set { _ProductsCode = value; }
			get { return _ProductsCode; }
		}

		
	}
}

