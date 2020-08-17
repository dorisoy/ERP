using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 外部订单商品信息表
	/// </summary>
	[Serializable]
	public partial class OrdouterItem {
		public OrdouterItem() { }
        
        
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
	    /// 外部订单号。和子订单号两个字段构成唯一值索引
	    /// </summary>
		public  string OutOrderCode {
			set { _OutOrderCode = value; }
			get { return _OutOrderCode; }
		}

        
        private  string _ErpOrderCode;
	    /// <summary>
	    /// 系统订单号
	    /// </summary>
		public  string ErpOrderCode {
			set { _ErpOrderCode = value; }
			get { return _ErpOrderCode; }
		}

        
        private  int _OrdouterID;
	    /// <summary>
	    /// 外部订单表ID
	    /// </summary>
		public  int OrdouterID {
			set { _OrdouterID = value; }
			get { return _OrdouterID; }
		}

        
        private  int _ShopID;
	    /// <summary>
	    /// 店铺ID
	    /// </summary>
		public  int ShopID {
			set { _ShopID = value; }
			get { return _ShopID; }
		}

        
        private  string _ChildOrderCode;
	    /// <summary>
	    /// 子订单号
	    /// </summary>
		public  string ChildOrderCode {
			set { _ChildOrderCode = value; }
			get { return _ChildOrderCode; }
		}

        
        private  string _ProductsName;
	    /// <summary>
	    /// 商品标题
	    /// </summary>
		public  string ProductsName {
			set { _ProductsName = value; }
			get { return _ProductsName; }
		}

        
        private  string _PicPath;
	    /// <summary>
	    /// 商品图片的绝对路径
	    /// </summary>
		public  string PicPath {
			set { _PicPath = value; }
			get { return _PicPath; }
		}

        
        private  string _ProductsCode;
	    /// <summary>
	    /// 商品编码
	    /// </summary>
		public  string ProductsCode {
			set { _ProductsCode = value; }
			get { return _ProductsCode; }
		}

        
        private  int _ProductsNum;
	    /// <summary>
	    /// 商品数量
	    /// </summary>
		public  int ProductsNum {
			set { _ProductsNum = value; }
			get { return _ProductsNum; }
		}

        
        private  string _ProductsSkuCode;
	    /// <summary>
	    /// 商品SKU码
	    /// </summary>
		public  string ProductsSkuCode {
			set { _ProductsSkuCode = value; }
			get { return _ProductsSkuCode; }
		}

        
        private  string _ProductsSkuSaleprop;
	    /// <summary>
	    /// SKU的属性值。如：机身颜色:黑色;手机套餐:官方标配
	    /// </summary>
		public  string ProductsSkuSaleprop {
			set { _ProductsSkuSaleprop = value; }
			get { return _ProductsSkuSaleprop; }
		}

        
        private  decimal _AdjustFee;
	    /// <summary>
	    /// 子订单手工调整金额
	    /// </summary>
		public  decimal AdjustFee {
			set { _AdjustFee = value; }
			get { return _AdjustFee; }
		}

        
        private  decimal _DiscountFee;
	    /// <summary>
	    /// 子订单级订单优惠金额
	    /// </summary>
		public  decimal DiscountFee {
			set { _DiscountFee = value; }
			get { return _DiscountFee; }
		}

        
        private  decimal _Payment;
	    /// <summary>
	    /// 子订单实付金额。精确到2位小数，单位:元。如:200.07，表示:200元7分。对于多子订单的交易，计算公式如下：payment = price * num + adjust_fee - discount_fee ；单子订单交易，payment与主订单的payment一致，对于退款成功的子订单，由于主订单的优惠分摊金额，会造成该字段可能不为0.00元。建议使用退款前的实付金额减去退款单中的实际退款金额计算。
	    /// </summary>
		public  decimal Payment {
			set { _Payment = value; }
			get { return _Payment; }
		}

        
        private  decimal _Price;
	    /// <summary>
	    /// 商品价格
	    /// </summary>
		public  decimal Price {
			set { _Price = value; }
			get { return _Price; }
		}

        
        private  int _IsProductAddFin;
	    /// <summary>
		/// 是否添加成功 0：否 1：是 2:标记删除
	    /// </summary>
		public  int IsProductAddFin {
			set { _IsProductAddFin = value; }
			get { return _IsProductAddFin; }
		}

        
        private  string _ProductAddMsg;
	    /// <summary>
	    /// 商品添加信息
	    /// </summary>
		public  string ProductAddMsg {
			set { _ProductAddMsg = value; }
			get { return _ProductAddMsg; }
		}


		private int _IsRefund;
		/// <summary>
		/// 是否申请退款 0：否 1：是
		/// </summary>
		public int IsRefund {
			set { _IsRefund = value; }
			get { return _IsRefund; }
		}


        private  string _RefundStatus;
	    /// <summary>
	    /// 退款状态
	    /// </summary>
		public  string RefundStatus {
			set { _RefundStatus = value; }
			get { return _RefundStatus; }
		}

        
        private  string _OrderStatus;
	    /// <summary>
	    /// 订单状态
	    /// </summary>
		public  string OrderStatus {
			set { _OrderStatus = value; }
			get { return _OrderStatus; }
		}

        
        private  string _SingleOutOrderCode;
	    /// <summary>
	    /// 外部订单号，没有合并订单或拆分订单时和OutOrderCode数据一样
	    /// </summary>
		public  string SingleOutOrderCode {
			set { _SingleOutOrderCode = value; }
			get { return _SingleOutOrderCode; }
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

