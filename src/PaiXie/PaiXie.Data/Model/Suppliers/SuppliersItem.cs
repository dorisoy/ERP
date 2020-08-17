using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 供应商商品表
	/// </summary>
	[Serializable]
	public partial class SuppliersItem {
		public SuppliersItem() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 供应商商品表主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  int _SuppliersID;
	    /// <summary>
	    /// 供应商表标识
	    /// </summary>
		public  int SuppliersID {
			set { _SuppliersID = value; }
			get { return _SuppliersID; }
		}

        
        private  int _ProductsID;
	    /// <summary>
	    /// 商品表标识
	    /// </summary>
		public  int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
		}

        
        private  string _ProductsCode;
	    /// <summary>
	    /// 商品编码
	    /// </summary>
		public  string ProductsCode {
			set { _ProductsCode = value; }
			get { return _ProductsCode; }
		}

        
        private  string _ProductsName;
	    /// <summary>
	    /// 商品名称
	    /// </summary>
		public  string ProductsName {
			set { _ProductsName = value; }
			get { return _ProductsName; }
		}

        
        private  string _ProductsNo;
	    /// <summary>
	    /// 商品货号
	    /// </summary>
		public  string ProductsNo {
			set { _ProductsNo = value; }
			get { return _ProductsNo; }
		}

        
        private  int _ProductsSkuID;
	    /// <summary>
	    /// 商品SKU表标识
	    /// </summary>
		public  int ProductsSkuID {
			set { _ProductsSkuID = value; }
			get { return _ProductsSkuID; }
		}

        
        private  string _ProductsSkuCode;
	    /// <summary>
	    /// 商品SKU码
	    /// </summary>
		public  string ProductsSkuCode {
			set { _ProductsSkuCode = value; }
			get { return _ProductsSkuCode; }
		}

        
        private  string _ProductsSkuSaleprop;
	    /// <summary>
	    /// 商品销售属性
	    /// </summary>
		public  string ProductsSkuSaleprop {
			set { _ProductsSkuSaleprop = value; }
			get { return _ProductsSkuSaleprop; }
		}

        
        private  decimal _PurchasePrice;
	    /// <summary>
	    /// 采购价
	    /// </summary>
		public  decimal PurchasePrice {
			set { _PurchasePrice = value; }
			get { return _PurchasePrice; }
		}

        
        private  int _ArrivalCycle;
	    /// <summary>
	    /// 到货周期(天)
	    /// </summary>
		public  int ArrivalCycle {
			set { _ArrivalCycle = value; }
			get { return _ArrivalCycle; }
		}

		private int _IsDefault;
	    /// <summary>
	    /// 是否默认供应商 0:否 1:是
	    /// </summary>
		public int IsDefault {
			set { _IsDefault = value; }
			get { return _IsDefault; }
		}
        
        private  string _CreatePerson;
	    /// <summary>
	    /// 创建人
	    /// </summary>
		public  string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}

        
        private  DateTime _CreateDate;
	    /// <summary>
	    /// 创建时间
	    /// </summary>
		public  DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}

        
        private  string _UpdatePerson;
	    /// <summary>
	    ///  修改人
	    /// </summary>
		public  string UpdatePerson {
			set { _UpdatePerson = value; }
			get { return _UpdatePerson; }
		}

        
        private  DateTime _UpdateDate;
	    /// <summary>
	    /// 修改时间
	    /// </summary>
		public  DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}

		
	}
}

