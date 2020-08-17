using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 售后记录表
	/// </summary>
	[Serializable]
	public partial class Ordrefund {
		public Ordrefund() { }
        
        
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
	    /// 售后单编号 系统生成唯一
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

        
        private  int _OrderSource;
	    /// <summary>
	    /// 订单来源 枚举
	    /// </summary>
		public  int OrderSource {
			set { _OrderSource = value; }
			get { return _OrderSource; }
		}

        
        private  int _ShopID;
	    /// <summary>
	    /// 店铺ID
	    /// </summary>
		public  int ShopID {
			set { _ShopID = value; }
			get { return _ShopID; }
		}

        
        private  int _RefundType;
	    /// <summary>
	    /// 售后类型，默认0：退货退款，1：仅退款 枚举
	    /// </summary>
		public  int RefundType {
			set { _RefundType = value; }
			get { return _RefundType; }
		}

        
        private  int _Status;
	    /// <summary>
	    /// 售后单状态 等待买家退货 = 10,等待卖家收货 = 20,收货异常=30,已完成 = 40,已取消 = 99 枚举
	    /// </summary>
		public  int Status {
			set { _Status = value; }
			get { return _Status; }
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

        
        private  string _OutboundBillNo;
	    /// <summary>
	    /// 销售出库单号
	    /// </summary>
		public  string OutboundBillNo {
			set { _OutboundBillNo = value; }
			get { return _OutboundBillNo; }
		}

        
        private  string _BuyAddr;
	    /// <summary>
	    /// 买家地址
	    /// </summary>
		public  string BuyAddr {
			set { _BuyAddr = value; }
			get { return _BuyAddr; }
		}

        
        private  string _BuyName;
	    /// <summary>
	    /// 买家姓名
	    /// </summary>
		public  string BuyName {
			set { _BuyName = value; }
			get { return _BuyName; }
		}

        
        private  string _BuyMtel;
	    /// <summary>
	    /// 买家手机
	    /// </summary>
		public  string BuyMtel {
			set { _BuyMtel = value; }
			get { return _BuyMtel; }
		}

        
        private  string _BuyTel;
	    /// <summary>
	    /// 买家电话
	    /// </summary>
		public  string BuyTel {
			set { _BuyTel = value; }
			get { return _BuyTel; }
		}

        
        private  int _Duty;
	    /// <summary>
	    /// 售后责任方：比如说顾客或者厂家 用枚举
	    /// </summary>
		public  int Duty {
			set { _Duty = value; }
			get { return _Duty; }
		}

        
        private  string _DutyOther;
	    /// <summary>
	    /// 其他责任方 文本
	    /// </summary>
		public  string DutyOther {
			set { _DutyOther = value; }
			get { return _DutyOther; }
		}

        
        private  string _ReceivePerson;
	    /// <summary>
	    /// 售后收货人
	    /// </summary>
		public  string ReceivePerson {
			set { _ReceivePerson = value; }
			get { return _ReceivePerson; }
		}

        
        private  string _ReceiveTel;
	    /// <summary>
	    /// 售后收货人电话或手机
	    /// </summary>
		public  string ReceiveTel {
			set { _ReceiveTel = value; }
			get { return _ReceiveTel; }
		}

        
        private  string _ReceiveAddress;
	    /// <summary>
	    /// 售后收货人地址
	    /// </summary>
		public  string ReceiveAddress {
			set { _ReceiveAddress = value; }
			get { return _ReceiveAddress; }
		}

		private string _ReceivePostCode;
	    /// <summary>
	    /// 售后收货人邮编
	    /// </summary>
		public string ReceivePostCode {
			set { _ReceivePostCode = value; }
			get { return _ReceivePostCode; }
		}
        
        private  string _ExpressCompany;
	    /// <summary>
	    /// 商品寄回的快递公司
	    /// </summary>
		public  string ExpressCompany {
			set { _ExpressCompany = value; }
			get { return _ExpressCompany; }
		}

        
        private  string _WaybillNo;
	    /// <summary>
	    /// 商品寄回运单号
	    /// </summary>
		public  string WaybillNo {
			set { _WaybillNo = value; }
			get { return _WaybillNo; }
		}

        
        private  DateTime _SendBackDate;
	    /// <summary>
	    /// 商品寄回时间
	    /// </summary>
		public  DateTime SendBackDate {
			set { _SendBackDate = value; }
			get { return _SendBackDate; }
		}

        
        private  string _RefundBillNo;
	    /// <summary>
	    /// 退款单据号
	    /// </summary>
		public  string RefundBillNo {
			set { _RefundBillNo = value; }
			get { return _RefundBillNo; }
		}

        
        private  decimal _RefundAmount;
	    /// <summary>
	    /// 退金额(不包含运费)
	    /// </summary>
		public  decimal RefundAmount {
			set { _RefundAmount = value; }
			get { return _RefundAmount; }
		}

        
        private  decimal _RefundFreight;
	    /// <summary>
	    /// 退运费
	    /// </summary>
		public  decimal RefundFreight {
			set { _RefundFreight = value; }
			get { return _RefundFreight; }
		}

        
        private  decimal _ReturnFreight;
	    /// <summary>
	    /// 寄回运费
	    /// </summary>
		public  decimal ReturnFreight {
			set { _ReturnFreight = value; }
			get { return _ReturnFreight; }
		}

        
        private  string _Reason;
	    /// <summary>
	    /// 售后原因
	    /// </summary>
		public  string Reason {
			set { _Reason = value; }
			get { return _Reason; }
		}

        
        private  string _ReasonDetail;
	    /// <summary>
	    /// 售后详细原因
	    /// </summary>
		public  string ReasonDetail {
			set { _ReasonDetail = value; }
			get { return _ReasonDetail; }
		}

        
        private  string _ReceiveRemark;
	    /// <summary>
	    /// 收货备注
	    /// </summary>
		public  string ReceiveRemark {
			set { _ReceiveRemark = value; }
			get { return _ReceiveRemark; }
		}

        
        private  DateTime _ReceiveDate;
	    /// <summary>
	    /// 收货时间
	    /// </summary>
		public  DateTime ReceiveDate {
			set { _ReceiveDate = value; }
			get { return _ReceiveDate; }
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

