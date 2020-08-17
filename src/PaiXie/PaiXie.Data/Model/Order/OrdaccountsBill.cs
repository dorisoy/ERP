using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 无注释
	/// </summary>
	[Serializable]
	public partial class OrdaccountsBill {
		public OrdaccountsBill() { }
        
        
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
	    /// 收付款单据号
	    /// </summary>
		public  string BillNo {
			set { _BillNo = value; }
			get { return _BillNo; }
		}

        
        private  int _BillType;
	    /// <summary>
	    /// 单据类型 枚举
	    /// </summary>
		public  int BillType {
			set { _BillType = value; }
			get { return _BillType; }
		}

        
        private  int _BillWay;
	    /// <summary>
	    /// 单据方向 -1：退款 1：收款 （用于计算）
	    /// </summary>
		public  int BillWay {
			set { _BillWay = value; }
			get { return _BillWay; }
		}

        
        private  string _ErpOrderCode;
	    /// <summary>
	    /// 系统订单号
	    /// </summary>
		public  string ErpOrderCode {
			set { _ErpOrderCode = value; }
			get { return _ErpOrderCode; }
		}


		private string _AssociatedCode;
		/// <summary>
		/// 关联单号 订单编号或售后单号
		/// </summary>
		public string AssociatedCode {
			set { _AssociatedCode = value; }
			get { return _AssociatedCode; }
		}

        
        private  decimal _Amount;
	    /// <summary>
	    /// 金额
	    /// </summary>
		public  decimal Amount {
			set { _Amount = value; }
			get { return _Amount; }
		}

        
        private  int _PaymentMethod;
	    /// <summary>
		/// 付款方式 0:在线支付 1：现金支付
	    /// </summary>
		public  int PaymentMethod {
			set { _PaymentMethod = value; }
			get { return _PaymentMethod; }
		}

        
        private  string _PaymentAccount;
	    /// <summary>
	    /// 付款账号
	    /// </summary>
		public  string PaymentAccount {
			set { _PaymentAccount = value; }
			get { return _PaymentAccount; }
		}

        
        private  string _ReceivableAccount;
	    /// <summary>
	    /// 收款账号
	    /// </summary>
		public  string ReceivableAccount {
			set { _ReceivableAccount = value; }
			get { return _ReceivableAccount; }
		}

        
        private  string _TradingNumber;
	    /// <summary>
	    /// 交易号
	    /// </summary>
		public  string TradingNumber {
			set { _TradingNumber = value; }
			get { return _TradingNumber; }
		}

        
        private  int _Status;
	    /// <summary>
		/// 审核状态 0：未付款 1：已付未审 2：已付已审 
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

        
        private  DateTime _PayDate;
	    /// <summary>
	    /// 付款时间
	    /// </summary>
		public  DateTime PayDate {
			set { _PayDate = value; }
			get { return _PayDate; }
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

	}
}

