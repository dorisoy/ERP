using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 采购单表
	/// </summary>
	[Serializable]
	public partial class WarehousePurchase {
		public WarehousePurchase() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 采购单主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _BillNo;
	    /// <summary>
	    /// 采购单号 唯一
	    /// </summary>
		public  string BillNo {
			set { _BillNo = value; }
			get { return _BillNo; }
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

        
        private  int _SuppliersID;
	    /// <summary>
	    /// 供应商ID
	    /// </summary>
		public  int SuppliersID {
			set { _SuppliersID = value; }
			get { return _SuppliersID; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编码
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  int _Num;
	    /// <summary>
	    /// 采购数量
	    /// </summary>
		public  int Num {
			set { _Num = value; }
			get { return _Num; }
		}

        
        private  int _InStockNum;
	    /// <summary>
	    /// 已入库数量
	    /// </summary>
		public  int InStockNum {
			set { _InStockNum = value; }
			get { return _InStockNum; }
		}

        
        private  int _InStockOrderCount;
	    /// <summary>
	    /// 入库单条数
	    /// </summary>
		public  int InStockOrderCount {
			set { _InStockOrderCount = value; }
			get { return _InStockOrderCount; }
		}

        
        private  int _Status;
	    /// <summary>
	    /// 状态 0：未确认 10：已确认 20：已结束
	    /// </summary>
		public  int Status {
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

