using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 移位商品表
	/// </summary>
	[Serializable]
	public partial class WarehouseMoveLocationItem {
		public WarehouseMoveLocationItem() { }
        
        
        private  int _ID;
	    /// <summary>
	    ///  主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _MoveLocationBillNo;
	    /// <summary>
	    /// 移位单号
	    /// </summary>
		public  string MoveLocationBillNo {
			set { _MoveLocationBillNo = value; }
			get { return _MoveLocationBillNo; }
		}

        
        private  int _MoveLocationID;
	    /// <summary>
	    /// 移位单表主键ID
	    /// </summary>
		public  int MoveLocationID {
			set { _MoveLocationID = value; }
			get { return _MoveLocationID; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编码
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  int _ProductsID;
	    /// <summary>
	    /// 商品ID
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
	    /// 商品SKUID
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

        
        private  int _ProductsBatchID;
	    /// <summary>
	    /// 批次表ID
	    /// </summary>
		public  int ProductsBatchID {
			set { _ProductsBatchID = value; }
			get { return _ProductsBatchID; }
		}

        
        private  string _ProductsBatchCode;
	    /// <summary>
	    /// 批次号
	    /// </summary>
		public  string ProductsBatchCode {
			set { _ProductsBatchCode = value; }
			get { return _ProductsBatchCode; }
		}

        
        private  int _OutLocationID;
	    /// <summary>
	    /// 移出库位ID
	    /// </summary>
		public  int OutLocationID {
			set { _OutLocationID = value; }
			get { return _OutLocationID; }
		}

        
        private  int _InLocationID;
	    /// <summary>
	    /// 移入库位ID
	    /// </summary>
		public  int InLocationID {
			set { _InLocationID = value; }
			get { return _InLocationID; }
		}

        
        private  int _Num;
	    /// <summary>
	    /// 移位数量
	    /// </summary>
		public  int Num {
			set { _Num = value; }
			get { return _Num; }
		}

		private int _Status;
		/// <summary>
		/// 状态  0:未确认 10 已确认
		/// </summary>
		public int Status {
			set { _Status = value; }
			get { return _Status; }
		}
        
        private  string _CreatePerson;
	    /// <summary>
	    /// 创建人
	    /// </summary>
		public  string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}

		private DateTime _ConfirmDate;
	    /// <summary>
	    /// 确认时间
	    /// </summary>
		public DateTime ConfirmDate {
			set { _ConfirmDate = value; }
			get { return _ConfirmDate; }
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
