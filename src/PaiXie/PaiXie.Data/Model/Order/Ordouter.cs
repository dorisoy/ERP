using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data {
	/// <summary>
	/// 外部订单信息表
	/// </summary>
	[Serializable]
	public partial class Ordouter {
		public Ordouter() { }


		private int _ID;
		/// <summary>
		/// 无注释
		/// </summary>
		public int ID {
			set { _ID = value; }
			get { return _ID; }
		}


		private string _OutOrderCode;
		/// <summary>
		/// 外部订单号。和订单来源两个字段构成唯一值索引
		/// </summary>
		public string OutOrderCode {
			set { _OutOrderCode = value; }
			get { return _OutOrderCode; }
		}


		private string _ErpOrderCode;
		/// <summary>
		/// 系统订单号
		/// </summary>
		public string ErpOrderCode {
			set { _ErpOrderCode = value; }
			get { return _ErpOrderCode; }
		}


		private int _OrderSource;
		/// <summary>
		/// 订单来源，枚举
		/// </summary>
		public int OrderSource {
			set { _OrderSource = value; }
			get { return _OrderSource; }
		}


		private int _ShopID;
		/// <summary>
		/// 店铺ID
		/// </summary>
		public int ShopID {
			set { _ShopID = value; }
			get { return _ShopID; }
		}


		private string _BuyNickName;
		/// <summary>
		/// 买家昵称
		/// </summary>
		public string BuyNickName {
			set { _BuyNickName = value; }
			get { return _BuyNickName; }
		}


		private string _BuyName;
		/// <summary>
		/// 买家姓名
		/// </summary>
		public string BuyName {
			set { _BuyName = value; }
			get { return _BuyName; }
		}


		private string _BuyTel;
		/// <summary>
		/// 买家电话
		/// </summary>
		public string BuyTel {
			set { _BuyTel = value; }
			get { return _BuyTel; }
		}


		private string _BuyMtel;
		/// <summary>
		/// 买家手机
		/// </summary>
		public string BuyMtel {
			set { _BuyMtel = value; }
			get { return _BuyMtel; }
		}


		private string _BuyAddr;
		/// <summary>
		/// 买家地址
		/// </summary>
		public string BuyAddr {
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

		private string _BuyPostCode;
		/// <summary>
		/// 买家邮编
		/// </summary>
		public string BuyPostCode {
			set { _BuyPostCode = value; }
			get { return _BuyPostCode; }
		}


		private string _BuyMessage;
		/// <summary>
		/// 买家留言
		/// </summary>
		public string BuyMessage {
			set { _BuyMessage = value; }
			get { return _BuyMessage; }
		}


		private string _SellerNickName;
		/// <summary>
		/// 卖家昵称
		/// </summary>
		public string SellerNickName {
			set { _SellerNickName = value; }
			get { return _SellerNickName; }
		}


		private string _SellerRemark;
		/// <summary>
		/// 卖家备注
		/// </summary>
		public string SellerRemark {
			set { _SellerRemark = value; }
			get { return _SellerRemark; }
		}


		private string _BuyProvince;
		/// <summary>
		/// 收货人的所在省份
		/// </summary>
		public string BuyProvince {
			set { _BuyProvince = value; }
			get { return _BuyProvince; }
		}


		private string _BuyCity;
		/// <summary>
		/// 收货人的所在城市
		/// </summary>
		public string BuyCity {
			set { _BuyCity = value; }
			get { return _BuyCity; }
		}


		private string _BuyDistrict;
		/// <summary>
		/// 收货人的所在地区
		/// </summary>
		public string BuyDistrict {
			set { _BuyDistrict = value; }
			get { return _BuyDistrict; }
		}


		private DateTime _Created;
		/// <summary>
		/// 平台交易创建时间
		/// </summary>
		public DateTime Created {
			set { _Created = value; }
			get { return _Created; }
		}


		private DateTime _Modified;
		/// <summary>
		/// 平台交易修改时间
		/// </summary>
		public DateTime Modified {
			set { _Modified = value; }
			get { return _Modified; }
		}


		private DateTime _PayDate;
		/// <summary>
		/// 付款时间。格式:yyyy-MM-dd HH:mm:ss
		/// </summary>
		public DateTime PayDate {
			set { _PayDate = value; }
			get { return _PayDate; }
		}


		private int _ShippingType;
		/// <summary>
		/// 创建交易时的物流方式 枚举值
		/// </summary>
		public int ShippingType {
			set { _ShippingType = value; }
			get { return _ShippingType; }
		}


		private string _TradeType;
		/// <summary>
		/// 交易类型列表，同时查询多种交易类型可用逗号分隔。默认同时查询guarantee_trade, auto_delivery, ec, cod的4种交易类型的数据 可选值 fixed(一口价) auction(拍卖) guarantee_trade(一口价、拍卖) auto_delivery(自动发货) independent_simple_trade(旺店入门版交易) independent_shop_trade(旺店标准版交易) ec(直冲) cod(货到付款) fenxiao(分销) game_equi
		/// </summary>
		public string TradeType {
			set { _TradeType = value; }
			get { return _TradeType; }
		}


		private int _IsCod;
		/// <summary>
		/// 是否货到付款 0否 1是
		/// </summary>
		public int IsCod {
			set { _IsCod = value; }
			get { return _IsCod; }
		}


		private int _ProductsNum;
		/// <summary>
		/// 商品数量
		/// </summary>
		public int ProductsNum {
			set { _ProductsNum = value; }
			get { return _ProductsNum; }
		}


		private decimal _ProductsAmount;
		/// <summary>
		/// 商品金额
		/// </summary>
		public decimal ProductsAmount {
			set { _ProductsAmount = value; }
			get { return _ProductsAmount; }
		}


		private decimal _OrderDiscount;
		/// <summary>
		/// 订单优惠
		/// </summary>
		public decimal OrderDiscount {
			set { _OrderDiscount = value; }
			get { return _OrderDiscount; }
		}


		private decimal _ReceivableAmount;
		/// <summary>
		/// 应收金额
		/// </summary>
		public decimal ReceivableAmount {
			set { _ReceivableAmount = value; }
			get { return _ReceivableAmount; }
		}


		private decimal _UncollectedeAmount;
		/// <summary>
		/// 未收金额
		/// </summary>
		public decimal UncollectedeAmount {
			set { _UncollectedeAmount = value; }
			get { return _UncollectedeAmount; }
		}


		private decimal _RealAmount;
		/// <summary>
		/// 实收金额
		/// </summary>
		public decimal RealAmount {
			set { _RealAmount = value; }
			get { return _RealAmount; }
		}


		private decimal _Freight;
		/// <summary>
		/// 邮费
		/// </summary>
		public decimal Freight {
			set { _Freight = value; }
			get { return _Freight; }
		}


		private decimal _PlatformFee;
		/// <summary>
		/// 平台费
		/// </summary>
		public decimal PlatformFee {
			set { _PlatformFee = value; }
			get { return _PlatformFee; }
		}


		private decimal _BuyCodFee;
		/// <summary>
		/// 买家货到付款服务费
		/// </summary>
		public decimal BuyCodFee {
			set { _BuyCodFee = value; }
			get { return _BuyCodFee; }
		}


		private string _BuyAccount;
		/// <summary>
		/// 买家付款账号
		/// </summary>
		public string BuyAccount {
			set { _BuyAccount = value; }
			get { return _BuyAccount; }
		}


		private string _SellerAccount;
		/// <summary>
		/// 卖家收款账号
		/// </summary>
		public string SellerAccount {
			set { _SellerAccount = value; }
			get { return _SellerAccount; }
		}


		private int _IsNeedInvoice;
		/// <summary>
		/// 是否有发票信息
		/// </summary>
		public int IsNeedInvoice {
			set { _IsNeedInvoice = value; }
			get { return _IsNeedInvoice; }
		}


		private string _InvoiceInfo;
		/// <summary>
		/// 发票信息
		/// </summary>
		public string InvoiceInfo {
			set { _InvoiceInfo = value; }
			get { return _InvoiceInfo; }
		}


		private string _InvoiceName;
		/// <summary>
		/// 发票抬头
		/// </summary>
		public string InvoiceName {
			set { _InvoiceName = value; }
			get { return _InvoiceName; }
		}


		private int _IsHang;
		/// <summary>
		/// 是否挂起 0：否 1：是
		/// </summary>
		public int IsHang {
			set { _IsHang = value; }
			get { return _IsHang; }
		}


		private string _HangRemark;
		/// <summary>
		/// 挂起备注
		/// </summary>
		public string HangRemark {
			set { _HangRemark = value; }
			get { return _HangRemark; }
		}


		private int _IsFromTbFxpt;
		/// <summary>
		/// 是否来自淘宝分销平台 0:否 1：是
		/// </summary>
		public int IsFromTbFxpt {
			set { _IsFromTbFxpt = value; }
			get { return _IsFromTbFxpt; }
		}


		private string _OrderStatus;
		/// <summary>
		/// 平台订单状态
		/// </summary>
		public string OrderStatus {
			set { _OrderStatus = value; }
			get { return _OrderStatus; }
		}


		private int _IsDownFin;
		/// <summary>
		/// 是否下载完成
		/// </summary>
		public int IsDownFin {
			set { _IsDownFin = value; }
			get { return _IsDownFin; }
		}


		private int _IsMergeOrder;
		/// <summary>
		/// 是否合并的订单 0：否 1：是
		/// </summary>
		public int IsMergeOrder {
			set { _IsMergeOrder = value; }
			get { return _IsMergeOrder; }
		}


		private string _MergeMasterOrder;
		/// <summary>
		/// 合并订单的主外部单号
		/// </summary>
		public string MergeMasterOrder {
			set { _MergeMasterOrder = value; }
			get { return _MergeMasterOrder; }
		}


		private int _IsSplitOrder;
		/// <summary>
		/// 是否拆分订单 0：否 1：是
		/// </summary>
		public int IsSplitOrder {
			set { _IsSplitOrder = value; }
			get { return _IsSplitOrder; }
		}


		private string _SplitMasterOrder;
		/// <summary>
		/// 拆分订单的主外部单号
		/// </summary>
		public string SplitMasterOrder {
			set { _SplitMasterOrder = value; }
			get { return _SplitMasterOrder; }
		}


		private string _SingleOutOrderCode;
		/// <summary>
		/// 外部订单号，没有合并订单或拆分订单时和OutOrderCode数据一样
		/// </summary>
		public string SingleOutOrderCode {
			set { _SingleOutOrderCode = value; }
			get { return _SingleOutOrderCode; }
		}


		private int _CanSplitMerge;
		/// <summary>
		/// 可拆可合=0；不可拆不可合=1；不可拆可合并=2；可拆不可合=3；
		/// </summary>
		public int CanSplitMerge {
			set { _CanSplitMerge = value; }
			get { return _CanSplitMerge; }
		}


		private int _GenerateState;
		/// <summary>
		/// 订单生成状态 0：未生成 1：已生成
		/// </summary>
		public int GenerateState {
			set { _GenerateState = value; }
			get { return _GenerateState; }
		}


		private string _CreatePerson;
		/// <summary>
		/// 创建人
		/// </summary>
		public string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}


		private DateTime _CreateDate;
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}


		private string _UpdatePerson;
		/// <summary>
		/// 修改人
		/// </summary>
		public string UpdatePerson {
			set { _UpdatePerson = value; }
			get { return _UpdatePerson; }
		}


		private DateTime _UpdateDate;
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}

	}
}

