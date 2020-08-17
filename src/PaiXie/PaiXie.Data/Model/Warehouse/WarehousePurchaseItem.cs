using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 采购单商品表
	/// </summary>
	[Serializable]
	public partial class WarehousePurchaseItem {
		public WarehousePurchaseItem() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 采购单商品主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _BillNo;
	    /// <summary>
	    /// 采购单号
	    /// </summary>
		public  string BillNo {
			set { _BillNo = value; }
			get { return _BillNo; }
		}

        
        private  int _PurchaseID;
	    /// <summary>
	    /// 采购单主键ID
	    /// </summary>
		public  int PurchaseID {
			set { _PurchaseID = value; }
			get { return _PurchaseID; }
		}

        
        private  int _PlanID;
	    /// <summary>
	    /// 采购计划单主键ID
	    /// </summary>
		public  int PlanID {
			set { _PlanID = value; }
			get { return _PlanID; }
		}

        
        private  string _PlanBillNo;
	    /// <summary>
	    /// 采购计划单号
	    /// </summary>
		public  string PlanBillNo {
			set { _PlanBillNo = value; }
			get { return _PlanBillNo; }
		}

        
        private  int _PlanItemID;
	    /// <summary>
	    /// 采购计划单商品主键ID
	    /// </summary>
		public  int PlanItemID {
			set { _PlanItemID = value; }
			get { return _PlanItemID; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编码
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  string _ProductsCode;
	    /// <summary>
	    /// 商品编码
	    /// </summary>
		public  string ProductsCode {
			set { _ProductsCode = value; }
			get { return _ProductsCode; }
		}

        
        private  int _ProductsID;
	    /// <summary>
	    /// 商品ID
	    /// </summary>
		public  int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
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
	    /// 商品SKU主键ID
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
	    /// 商品属性
	    /// </summary>
		public  string ProductsSkuSaleprop {
			set { _ProductsSkuSaleprop = value; }
			get { return _ProductsSkuSaleprop; }
		}

        
        private  int _Num;
	    /// <summary>
	    /// 采购数量
	    /// </summary>
		public  int Num {
			set { _Num = value; }
			get { return _Num; }
		}

		private int _InStockNum;
	    /// <summary>
	    /// 已入库数量
	    /// </summary>
		public int InStockNum {
			set { _InStockNum = value; }
			get { return _InStockNum; }
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
	    /// 修改人
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

