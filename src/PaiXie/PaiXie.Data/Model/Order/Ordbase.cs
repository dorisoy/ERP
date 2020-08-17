using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 订单基础信息表
	/// </summary>
	[Serializable]
	public partial class Ordbase {
		public Ordbase() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _OutOrderCode;
	    /// <summary>
	    /// 外部订单号。系统导入时的订单号，ERP内部手动添加时如果没有输入则为空
	    /// </summary>
		public  string OutOrderCode {
			set { _OutOrderCode = value; }
			get { return _OutOrderCode; }
		}

        
        private  string _ErpOrderCode;
	    /// <summary>
	    /// 系统订单号。ERP自动生成的订单号，不能重复，不能为空。订单唯一标识
	    /// </summary>
		public  string ErpOrderCode {
			set { _ErpOrderCode = value; }
			get { return _ErpOrderCode; }
		}

        
        private  int _CreateType;
	    /// <summary>
	    /// 创建类型 0：API下载；1：手动；2：导入
	    /// </summary>
		public  int CreateType {
			set { _CreateType = value; }
			get { return _CreateType; }
		}

        
        private  int _OrdouterID;
	    /// <summary>
	    /// 外部订单表ID
	    /// </summary>
		public  int OrdouterID {
			set { _OrdouterID = value; }
			get { return _OrdouterID; }
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

        
        private  int _OrderType;
	    /// <summary>
	    /// 订单类型 0：自发，1：代发
	    /// </summary>
		public  int OrderType {
			set { _OrderType = value; }
			get { return _OrderType; }
		}

        
        private  string _BuyNickName;
	    /// <summary>
	    /// 买家昵称
	    /// </summary>
		public  string BuyNickName {
			set { _BuyNickName = value; }
			get { return _BuyNickName; }
		}

        
        private  string _BuyName;
	    /// <summary>
	    /// 买家姓名
	    /// </summary>
		public  string BuyName {
			set { _BuyName = value; }
			get { return _BuyName; }
		}

        
        private  string _BuyTel;
	    /// <summary>
	    /// 买家电话
	    /// </summary>
		public  string BuyTel {
			set { _BuyTel = value; }
			get { return _BuyTel; }
		}

        
        private  string _BuyMtel;
	    /// <summary>
	    /// 买家手机
	    /// </summary>
		public  string BuyMtel {
			set { _BuyMtel = value; }
			get { return _BuyMtel; }
		}

        
        private  string _BuyAddr;
	    /// <summary>
	    /// 买家地址
	    /// </summary>
		public  string BuyAddr {
			set { _BuyAddr = value; }
			get { return _BuyAddr; }
		}


		private string _BuyAddressDetail;
	    /// <summary>
		/// 买家详细地址（不包含省市区）
	    /// </summary>
		public string BuyAddressDetail {
			set { _BuyAddressDetail = value; }
			get { return _BuyAddressDetail; }
		}

        
        private  string _BuyPostCode;
	    /// <summary>
	    /// 买家邮编
	    /// </summary>
		public  string BuyPostCode {
			set { _BuyPostCode = value; }
			get { return _BuyPostCode; }
		}

        
        private  string _BuyMessage;
	    /// <summary>
	    /// 买家留言
	    /// </summary>
		public  string BuyMessage {
			set { _BuyMessage = value; }
			get { return _BuyMessage; }
		}

        
        private  string _SellerRemark;
	    /// <summary>
	    /// 卖家备注
	    /// </summary>
		public  string SellerRemark {
			set { _SellerRemark = value; }
			get { return _SellerRemark; }
		}

        
        private  int _OrderStatus;
	    /// <summary>
	    /// 未生成 = 0,待审核 = 10,拣货中 = 20,部分发货 = 30,已发货 = 40,已取消 = 99
	    /// </summary>
		public  int OrderStatus {
			set { _OrderStatus = value; }
			get { return _OrderStatus; }
		}

        
        private  int _ProductsNum;
	    /// <summary>
	    /// 商品数量
	    /// </summary>
		public  int ProductsNum {
			set { _ProductsNum = value; }
			get { return _ProductsNum; }
		}

        
        private  decimal _ProductsAmount;
	    /// <summary>
	    /// 商品金额
	    /// </summary>
		public  decimal ProductsAmount {
			set { _ProductsAmount = value; }
			get { return _ProductsAmount; }
		}

        
        private  decimal _OrderDiscount;
	    /// <summary>
	    /// 商品优惠
	    /// </summary>
		public  decimal OrderDiscount {
			set { _OrderDiscount = value; }
			get { return _OrderDiscount; }
		}

        
        private  decimal _ReceivableAmount;
	    /// <summary>
	    /// 应收金额(商品金额-商品优惠+运费)
	    /// </summary>
		public  decimal ReceivableAmount {
			set { _ReceivableAmount = value; }
			get { return _ReceivableAmount; }
		}

        
        private  decimal _UncollectedeAmount;
	    /// <summary>
	    /// 未收金额
	    /// </summary>
		public  decimal UncollectedeAmount {
			set { _UncollectedeAmount = value; }
			get { return _UncollectedeAmount; }
		}

        
        private  decimal _RealAmount;
	    /// <summary>
	    /// 实收金额
	    /// </summary>
		public  decimal RealAmount {
			set { _RealAmount = value; }
			get { return _RealAmount; }
		}


		private decimal _RefundAmount;
		/// <summary>
		/// 应退金额：退款记录的退款金额之和（排除售后退款）
		/// </summary>
		public decimal RefundAmount {
			set { _RefundAmount = value; }
			get { return _RefundAmount; }
		}

        
        private  decimal _Freight;
	    /// <summary>
	    /// 运费
	    /// </summary>
		public  decimal Freight {
			set { _Freight = value; }
			get { return _Freight; }
		}

        
        private  decimal _TaxRevenue;
	    /// <summary>
	    /// 税金
	    /// </summary>
		public  decimal TaxRevenue {
			set { _TaxRevenue = value; }
			get { return _TaxRevenue; }
		}

        
        private  int _LogisticsID;
	    /// <summary>
	    /// 物流公司ID
	    /// </summary>
		public  int LogisticsID {
			set { _LogisticsID = value; }
			get { return _LogisticsID; }
		}

        
        private  int _ExpressID;
	    /// <summary>
	    /// 快递公司ID
	    /// </summary>
		public  int ExpressID {
			set { _ExpressID = value; }
			get { return _ExpressID; }
		}

        
        private  int _DeliveryExpressID;
	    /// <summary>
	    /// 发货快递公司ID
	    /// </summary>
		public  int DeliveryExpressID {
			set { _DeliveryExpressID = value; }
			get { return _DeliveryExpressID; }
		}

        
        private  string _WaybillNo;
	    /// <summary>
	    /// 运单号
	    /// </summary>
		public  string WaybillNo {
			set { _WaybillNo = value; }
			get { return _WaybillNo; }
		}

        
        private  DateTime _ExpectedDeliDate;
	    /// <summary>
	    /// 期望配送时间
	    /// </summary>
		public  DateTime ExpectedDeliDate {
			set { _ExpectedDeliDate = value; }
			get { return _ExpectedDeliDate; }
		}

        
        private  int _DeliveryMethod;
	    /// <summary>
	    /// 配送方式 0：送货上门 1：客户自提
	    /// </summary>
		public  int DeliveryMethod {
			set { _DeliveryMethod = value; }
			get { return _DeliveryMethod; }
		}

        
        private  string _SinceSome;
	    /// <summary>
	    /// 自提点
	    /// </summary>
		public  string SinceSome {
			set { _SinceSome = value; }
			get { return _SinceSome; }
		}

        
        private  int _PaymentMethod;
	    /// <summary>
	    /// 付款方式 枚举类型
	    /// </summary>
		public  int PaymentMethod {
			set { _PaymentMethod = value; }
			get { return _PaymentMethod; }
		}

        
        private  int _IsCod;
	    /// <summary>
	    /// 是否货到付款 0否 1是
	    /// </summary>
		public  int IsCod {
			set { _IsCod = value; }
			get { return _IsCod; }
		}

        
        private  int _CodStatus;
	    /// <summary>
	    /// 货到付款状态 枚举
	    /// </summary>
		public  int CodStatus {
			set { _CodStatus = value; }
			get { return _CodStatus; }
		}

        
        private  int _IsNeedInvoice;
	    /// <summary>
	    /// 是否有发票 0：否 1：是
	    /// </summary>
		public  int IsNeedInvoice {
			set { _IsNeedInvoice = value; }
			get { return _IsNeedInvoice; }
		}

        
        private  string _InvoiceName;
	    /// <summary>
	    /// 发票抬头
	    /// </summary>
		public  string InvoiceName {
			set { _InvoiceName = value; }
			get { return _InvoiceName; }
		}

        
        private  string _InvoiceInfo;
	    /// <summary>
	    /// 发票信息
	    /// </summary>
		public  string InvoiceInfo {
			set { _InvoiceInfo = value; }
			get { return _InvoiceInfo; }
		}

        
        private  DateTime _PayDate;
	    /// <summary>
	    /// 付款时间
	    /// </summary>
		public  DateTime PayDate {
			set { _PayDate = value; }
			get { return _PayDate; }
		}

        
        private  DateTime _Created;
	    /// <summary>
	    /// 下单时间
	    /// </summary>
		public  DateTime Created {
			set { _Created = value; }
			get { return _Created; }
		}

        
        private  string _TradingNumber;
	    /// <summary>
	    /// 交易号
	    /// </summary>
		public  string TradingNumber {
			set { _TradingNumber = value; }
			get { return _TradingNumber; }
		}

        
        private  string _PaymentAccount;
	    /// <summary>
	    /// 付款账号
	    /// </summary>
		public  string PaymentAccount {
			set { _PaymentAccount = value; }
			get { return _PaymentAccount; }
		}

        
        private  decimal _BuyCodFee;
	    /// <summary>
	    /// 买家货到付款服务费
	    /// </summary>
		public  decimal BuyCodFee {
			set { _BuyCodFee = value; }
			get { return _BuyCodFee; }
		}

        
        private  DateTime _GenerateOrderDate;
	    /// <summary>
	    /// 订单生成时间
	    /// </summary>
		public  DateTime GenerateOrderDate {
			set { _GenerateOrderDate = value; }
			get { return _GenerateOrderDate; }
		}

        
        private  string _OrderProcess;
	    /// <summary>
	    /// 订单操作过程
	    /// </summary>
		public  string OrderProcess {
			set { _OrderProcess = value; }
			get { return _OrderProcess; }
		}

        
        private  int _CancelPort;
	    /// <summary>
	    /// 取消订单端口，如仓库端或者管理端 0:管理端 1：仓库端
	    /// </summary>
		public  int CancelPort {
			set { _CancelPort = value; }
			get { return _CancelPort; }
		}

        
        private  string _CancelRemark;
	    /// <summary>
	    /// 订单取消备注
	    /// </summary>
		public  string CancelRemark {
			set { _CancelRemark = value; }
			get { return _CancelRemark; }
		}

        
        private  DateTime _CancelDate;
	    /// <summary>
	    /// 订单取消时间
	    /// </summary>
		public  DateTime CancelDate {
			set { _CancelDate = value; }
			get { return _CancelDate; }
		}

        
        private  int _IsSplitOrder;
	    /// <summary>
	    /// 是否拆分的订单 0：否 1：是
	    /// </summary>
		public  int IsSplitOrder {
			set { _IsSplitOrder = value; }
			get { return _IsSplitOrder; }
		}

        
        private  string _SplitMasterOrder;
	    /// <summary>
	    /// 拆分订单的主ERP单号
	    /// </summary>
		public  string SplitMasterOrder {
			set { _SplitMasterOrder = value; }
			get { return _SplitMasterOrder; }
		}

        
        private  int _ProvinceID;
	    /// <summary>
	    /// 收货人的所在省份ID
	    /// </summary>
		public  int ProvinceID {
			set { _ProvinceID = value; }
			get { return _ProvinceID; }
		}

        
        private  int _CityID;
	    /// <summary>
	    /// 收货人的所在城市ID
	    /// </summary>
		public  int CityID {
			set { _CityID = value; }
			get { return _CityID; }
		}

        
        private  int _DistrictID;
	    /// <summary>
	    /// 收货人的所在地区ID
	    /// </summary>
		public  int DistrictID {
			set { _DistrictID = value; }
			get { return _DistrictID; }
		}

        
        private  string _Province;
	    /// <summary>
	    /// 收货人的所在省份
	    /// </summary>
		public  string Province {
			set { _Province = value; }
			get { return _Province; }
		}

        
        private  string _City;
	    /// <summary>
	    /// 收货人的所在城市
	    /// </summary>
		public  string City {
			set { _City = value; }
			get { return _City; }
		}

        
        private  string _District;
	    /// <summary>
	    /// 收货人的所在地区
	    /// </summary>
		public  string District {
			set { _District = value; }
			get { return _District; }
		}

        
        private  DateTime _DeliveryDate;
	    /// <summary>
	    /// 发货时间：以第一次发货为准
	    /// </summary>
		public  DateTime DeliveryDate {
			set { _DeliveryDate = value; }
			get { return _DeliveryDate; }
		}

        
        private  int _IsApplyRefund;
	    /// <summary>
	    /// 是否申请退款 0否 1是
	    /// </summary>
		public  int IsApplyRefund {
			set { _IsApplyRefund = value; }
			get { return _IsApplyRefund; }
		}

        
        private  int _IsHang;
	    /// <summary>
	    /// 是否挂起 0否 1是
	    /// </summary>
		public  int IsHang {
			set { _IsHang = value; }
			get { return _IsHang; }
		}

        
        private  string _HangRemark;
	    /// <summary>
	    /// 挂起备注
	    /// </summary>
		public  string HangRemark {
			set { _HangRemark = value; }
			get { return _HangRemark; }
		}

		private int _IsReject;
		/// <summary>
		/// 是否驳回 0否 1是
		/// </summary>
		public int IsReject {
			set { _IsReject = value; }
			get { return _IsReject; }
		}


		private string _RejectRemark;
		/// <summary>
		/// 驳回备注
		/// </summary>
		public string RejectRemark {
			set { _RejectRemark = value; }
			get { return _RejectRemark; }
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

