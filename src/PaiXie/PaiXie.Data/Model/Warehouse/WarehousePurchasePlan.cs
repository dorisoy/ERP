using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 采购计划单
	/// </summary>
	[Serializable]
	public partial class WarehousePurchasePlan {
		public WarehousePurchasePlan() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _BillNo;
	    /// <summary>
	    /// 采购计划单号
	    /// </summary>
		public  string BillNo {
			set { _BillNo = value; }
			get { return _BillNo; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编号
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  string _Name;
	    /// <summary>
	    /// 采购计划单名称
	    /// </summary>
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
		}

        
        private  int _Status;
	    /// <summary>
	    /// 单据状态枚举0：未提交 10：已提交 20：已结束
	    /// </summary>
		public  int Status {
			set { _Status = value; }
			get { return _Status; }
		}

        
        private  int _Num;
	    /// <summary>
	    /// 计划采购数量
	    /// </summary>
		public  int Num {
			set { _Num = value; }
			get { return _Num; }
		}

        
        private  int _PurchasedNum;
	    /// <summary>
	    /// 已采购数量
	    /// </summary>
		public  int PurchasedNum {
			set { _PurchasedNum = value; }
			get { return _PurchasedNum; }
		}

        
        private  int _PurchaseOrderCount;
	    /// <summary>
	    /// 采购单数
	    /// </summary>
		public  int PurchaseOrderCount {
			set { _PurchaseOrderCount = value; }
			get { return _PurchaseOrderCount; }
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

