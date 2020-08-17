using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 盘点记录
	/// </summary>
	[Serializable]
	public partial class WarehouseStocktaking {
		public WarehouseStocktaking() { }
        
        
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
	    /// 盘点记录号
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

        
        private  string _LocationID;
	    /// <summary>
	    /// 盘点库区ID
	    /// </summary>
		public  string LocationID {
			set { _LocationID = value; }
			get { return _LocationID; }
		}

        
        private  string _LocationName;
	    /// <summary>
	    /// 盘点库区名称
	    /// </summary>
		public  string LocationName {
			set { _LocationName = value; }
			get { return _LocationName; }
		}

        
        private  string _Remark;
	    /// <summary>
	    /// 盘点备注
	    /// </summary>
		public  string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}

        
        private  int _Status;
	    /// <summary>
	    /// 单据状态枚举0：未确认 10：待审核 20：已确认
	    /// </summary>
		public  int Status {
			set { _Status = value; }
			get { return _Status; }
		}

        
        private  DateTime? _ConfirmDate;
	    /// <summary>
	    /// 确认时间
	    /// </summary>
		public  DateTime? ConfirmDate {
			set { _ConfirmDate = value; }
			get { return _ConfirmDate; }
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

