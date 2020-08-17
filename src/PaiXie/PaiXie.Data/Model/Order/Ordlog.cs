using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 订单操作日志
	/// </summary>
	[Serializable]
	public partial class Ordlog {
		public Ordlog() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _ErpOrderCode;
	    /// <summary>
	    /// 系统订单号
	    /// </summary>
		public  string ErpOrderCode {
			set { _ErpOrderCode = value; }
			get { return _ErpOrderCode; }
		}

        
        private  string _OutOrderCode;
	    /// <summary>
	    /// 外部订单号
	    /// </summary>
		public  string OutOrderCode {
			set { _OutOrderCode = value; }
			get { return _OutOrderCode; }
		}

		private string _BillNo;
		/// <summary>
		/// 出库单号
		/// </summary>
		public string BillNo {
			set { _BillNo = value; }
			get { return _BillNo; }
		}

		private string _WarehouseCode;
		/// <summary>
		/// 仓库编号
		/// </summary>
		public string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}
        
        private  string _UserCode;
	    /// <summary>
	    /// 用户帐号
	    /// </summary>
		public  string UserCode {
			set { _UserCode = value; }
			get { return _UserCode; }
		}

        
        private  string _UserName;
	    /// <summary>
	    /// 用户名称
	    /// </summary>
		public  string UserName {
			set { _UserName = value; }
			get { return _UserName; }
		}

        
        private  string _UserIP;
	    /// <summary>
	    /// 用户IP
	    /// </summary>
		public  string UserIP {
			set { _UserIP = value; }
			get { return _UserIP; }
		}


		private string _Message;
	    /// <summary>
	    /// 操作内容
	    /// </summary>
		public string Message {
			set { _Message = value; }
			get { return _Message; }
		}

        
        private  DateTime _CreateDate;
	    /// <summary>
	    /// 创建时间
	    /// </summary>
		public  DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}

	}
}

