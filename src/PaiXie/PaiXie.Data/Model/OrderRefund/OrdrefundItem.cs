using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 售后记录商品表
	/// </summary>
	[Serializable]
	public partial class OrdrefundItem {
		public OrdrefundItem() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  int _OrdRefundID;
	    /// <summary>
	    /// 售后表主键ID
	    /// </summary>
		public  int OrdRefundID {
			set { _OrdRefundID = value; }
			get { return _OrdRefundID; }
		}

        
        private  string _BillNo;
	    /// <summary>
	    /// 售后单号
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

        
        private  int _ShopID;
	    /// <summary>
	    /// 店铺ID
	    /// </summary>
		public  int ShopID {
			set { _ShopID = value; }
			get { return _ShopID; }
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

        
        private  int _OrdItemID;
	    /// <summary>
	    /// Ord_Item订单商品表ID
	    /// </summary>
		public  int OrdItemID {
			set { _OrdItemID = value; }
			get { return _OrdItemID; }
		}

        
        private  int _ProductsID;
	    /// <summary>
	    /// Products表ID
	    /// </summary>
		public  int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
		}

        
        private  string _ProductsCode;
	    /// <summary>
	    /// 商品编码
	    /// </summary>
		public  string ProductsCode {
			set { _ProductsCode = value; }
			get { return _ProductsCode; }
		}

        
        private  string _ProductsName;
	    /// <summary>
	    /// 商品名称
	    /// </summary>
		public  string ProductsName {
			set { _ProductsName = value; }
			get { return _ProductsName; }
		}

        
        private  string _ProductsNo;
	    /// <summary>
	    /// 商品货号
	    /// </summary>
		public  string ProductsNo {
			set { _ProductsNo = value; }
			get { return _ProductsNo; }
		}

        
        private  int _ProductsSkuID;
	    /// <summary>
	    /// 商品SKU表ID
	    /// </summary>
		public  int ProductsSkuID {
			set { _ProductsSkuID = value; }
			get { return _ProductsSkuID; }
		}

        
        private  string _ProductsSkuCode;
	    /// <summary>
	    /// SKU码
	    /// </summary>
		public  string ProductsSkuCode {
			set { _ProductsSkuCode = value; }
			get { return _ProductsSkuCode; }
		}

        
        private  string _ProductsSkuSaleprop;
	    /// <summary>
	    /// 销售属性(颜色：红色 规格：S)
	    /// </summary>
		public  string ProductsSkuSaleprop {
			set { _ProductsSkuSaleprop = value; }
			get { return _ProductsSkuSaleprop; }
		}

        
        private  int _ProductsBatchID;
	    /// <summary>
	    /// 批次ID
	    /// </summary>
		public  int ProductsBatchID {
			set { _ProductsBatchID = value; }
			get { return _ProductsBatchID; }
		}

        
        private  string _ProductsBatchCode;
	    /// <summary>
	    /// 批次号
	    /// </summary>
		public  string ProductsBatchCode {
			set { _ProductsBatchCode = value; }
			get { return _ProductsBatchCode; }
		}

        
        private  int _ProductsNum;
	    /// <summary>
	    /// 商品销售数量
	    /// </summary>
		public  int ProductsNum {
			set { _ProductsNum = value; }
			get { return _ProductsNum; }
		}

        
        private  int _RefundNum;
	    /// <summary>
	    /// 商品售后数量
	    /// </summary>
		public  int RefundNum {
			set { _RefundNum = value; }
			get { return _RefundNum; }
		}

        
        private  decimal _ActualSellingPrice;
	    /// <summary>
	    /// 商品实际销售价 扣除优惠之后的价格
	    /// </summary>
		public  decimal ActualSellingPrice {
			set { _ActualSellingPrice = value; }
			get { return _ActualSellingPrice; }
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

