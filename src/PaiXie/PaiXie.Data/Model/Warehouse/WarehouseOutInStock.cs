using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 出入库单表
	/// </summary>
	[Serializable]
	public partial class WarehouseOutInStock {
		public WarehouseOutInStock() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _BillNo;
	    /// <summary>
	    /// 出入库单据编号 系统生成唯一
	    /// </summary>
		public  string BillNo {
			set { _BillNo = value; }
			get { return _BillNo; }
		}

        
        private  int _BillType;
	    /// <summary>
	    /// 单据类型 枚举 采购入库10,采购退货20,其它入库30,其它出库40,退货入库50,销售出库60,盘点70,移位80,调拨入库90,调拨出库100
	    /// </summary>
		public  int BillType {
			set { _BillType = value; }
			get { return _BillType; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编号
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  int _Status;
	    /// <summary>
	    /// 单据状态 枚举  状态: 未提交=1待审核=2已审核=3
	    /// </summary>
		public  int Status {
			set { _Status = value; }
			get { return _Status; }
		}

        
        private  int _IsAuditPrice;
	    /// <summary>
	    /// 是否财审 0 否 1 是
	    /// </summary>
		public  int IsAuditPrice {
			set { _IsAuditPrice = value; }
			get { return _IsAuditPrice; }
		}

        
        private  int _SourceID;
	    /// <summary>
	    /// 来源单据标识
	    /// </summary>
		public  int SourceID {
			set { _SourceID = value; }
			get { return _SourceID; }
		}

        
        private  string _SourceNo;
	    /// <summary>
	    /// 来源单据编号
	    /// </summary>
		public  string SourceNo {
			set { _SourceNo = value; }
			get { return _SourceNo; }
		}

        
        private  int _SuppliersID;
	    /// <summary>
	    /// 供应商表标识
	    /// </summary>
		public  int SuppliersID {
			set { _SuppliersID = value; }
			get { return _SuppliersID; }
		}

        
        private  string _MainName;
	    /// <summary>
	    /// 负责人
	    /// </summary>
		public  string MainName {
			set { _MainName = value; }
			get { return _MainName; }
		}

        
        private  string _CountName;
	    /// <summary>
	    /// 清点人
	    /// </summary>
		public  string CountName {
			set { _CountName = value; }
			get { return _CountName; }
		}

        
        private  int _ExpressID;
	    /// <summary>
	    /// 快递公司ID
	    /// </summary>
		public  int ExpressID {
			set { _ExpressID = value; }
			get { return _ExpressID; }
		}

        
        private  string _Remark;
	    /// <summary>
	    /// 备注
	    /// </summary>
		public  string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}

        
        private  DateTime _OutInDate;
	    /// <summary>
	    /// 出入日期
	    /// </summary>
		public  DateTime OutInDate {
			set { _OutInDate = value; }
			get { return _OutInDate; }
		}

        
        private  DateTime _ConfirmDate;
	    /// <summary>
	    /// 确认时间
	    /// </summary>
		public  DateTime ConfirmDate {
			set { _ConfirmDate = value; }
			get { return _ConfirmDate; }
		}

        
        private  int _IsDxYs;
	    /// <summary>
	    /// 入库是否抵消预售 0否 1是
	    /// </summary>
		public  int IsDxYs {
			set { _IsDxYs = value; }
			get { return _IsDxYs; }
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
		/// <summary>
		/// 供应商结算状态  0  未结算 1  已结算
		/// </summary>
		public int Settlement {set;	get;}
		


	}
}

