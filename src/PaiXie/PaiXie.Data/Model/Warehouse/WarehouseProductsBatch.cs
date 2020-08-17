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
	public partial class WarehouseProductsBatch {
		public WarehouseProductsBatch() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  int _ProductsID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
		}

        
        private  int _ProductsSkuID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ProductsSkuID {
			set { _ProductsSkuID = value; }
			get { return _ProductsSkuID; }
		}

        
        private  string _BatchCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string BatchCode {
			set { _BatchCode = value; }
			get { return _BatchCode; }
		}

        
        private  DateTime _ProductionDate;
	    /// <summary>
	    /// 
	    /// </summary>
		public  DateTime ProductionDate {
			set { _ProductionDate = value; }
			get { return _ProductionDate; }
		}

        
        private  int _ShelfLife;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ShelfLife {
			set { _ShelfLife = value; }
			get { return _ShelfLife; }
		}

        
        private  decimal _CostPrice;
	    /// <summary>
	    /// 
	    /// </summary>
		public  decimal CostPrice {
			set { _CostPrice = value; }
			get { return _CostPrice; }
		}

        
        private  int _KyNum;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int KyNum {
			set { _KyNum = value; }
			get { return _KyNum; }
		}

        
        private  int _ZyNum;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ZyNum {
			set { _ZyNum = value; }
			get { return _ZyNum; }
		}

        
        private  int _DjNum;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int DjNum {
			set { _DjNum = value; }
			get { return _DjNum; }
		}

        
        private  int _SdNum;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int SdNum {
			set { _SdNum = value; }
			get { return _SdNum; }
		}

        
        private  int _ZkNum;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ZkNum {
			set { _ZkNum = value; }
			get { return _ZkNum; }
		}

        
        private  string _CreatePerson;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}

        
        private  DateTime _CreateDate;
	    /// <summary>
	    /// 
	    /// </summary>
		public  DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}

        
        private  string _UpdatePerson;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string UpdatePerson {
			set { _UpdatePerson = value; }
			get { return _UpdatePerson; }
		}

        
        private  DateTime _UpdateDate;
	    /// <summary>
	    /// 
	    /// </summary>
		public  DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}

		
	}
}

