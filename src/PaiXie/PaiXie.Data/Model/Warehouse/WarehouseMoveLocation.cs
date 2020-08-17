using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	///   移位单表
	/// </summary>
	[Serializable]
	public partial class WarehouseMoveLocation {
		public WarehouseMoveLocation() { }
        
        
        private  int _ID;
	    /// <summary>
	    ///  主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _BillNo;
	    /// <summary>
	    /// 移位单号 唯一
	    /// </summary>
		public  string BillNo {
			set { _BillNo = value; }
			get { return _BillNo; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编码
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  int _Status;
	    /// <summary>
	    /// 状态  0:未确认 10 已确认
	    /// </summary>
		public  int Status {
			set { _Status = value; }
			get { return _Status; }
		}

        
        private  string _Remark;
	    /// <summary>
	    /// 备注
	    /// </summary>
		public  string Remark {
			set { _Remark = value; }
			get { return _Remark; }
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

