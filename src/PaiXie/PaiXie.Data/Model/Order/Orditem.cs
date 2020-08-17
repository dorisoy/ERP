using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 订单商品信息表
	/// </summary>
	[Serializable]
	public partial class Orditem {
		public Orditem() { }
        
        
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
	    /// 外部订单号
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

        
        private  int _OrdbaseID;
	    /// <summary>
	    /// 订单表主键ID
	    /// </summary>
		public  int OrdbaseID {
			set { _OrdbaseID = value; }
			get { return _OrdbaseID; }
		}

        
        private  string _OutboundBillNo;
	    /// <summary>
	    /// 出库单号
	    /// </summary>
		public  string OutboundBillNo {
			set { _OutboundBillNo = value; }
			get { return _OutboundBillNo; }
		}

        
        private  int _OutboundID;
	    /// <summary>
	    /// 出库单主键ID
	    /// </summary>
		public  int OutboundID {
			set { _OutboundID = value; }
			get { return _OutboundID; }
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

        
        private  string _ChildOrderCode;
	    /// <summary>
	    /// 子订单号
	    /// </summary>
		public  string ChildOrderCode {
			set { _ChildOrderCode = value; }
			get { return _ChildOrderCode; }
		}

        
        private  int _OrdouterItemID;
	    /// <summary>
	    /// OuterItem单商品表ID
	    /// </summary>
		public  int OrdouterItemID {
			set { _OrdouterItemID = value; }
			get { return _OrdouterItemID; }
		}

        
        private  int _BrandID;
	    /// <summary>
	    /// ProductsBrand表ID
	    /// </summary>
		public  int BrandID {
			set { _BrandID = value; }
			get { return _BrandID; }
		}

        
        private  string _BrandName;
	    /// <summary>
	    /// 品牌名称
	    /// </summary>
		public  string BrandName {
			set { _BrandName = value; }
			get { return _BrandName; }
		}

        
        private  int _CategoryID;
	    /// <summary>
	    /// Category表ID
	    /// </summary>
		public  int CategoryID {
			set { _CategoryID = value; }
			get { return _CategoryID; }
		}

        
        private  string _CategoryName;
	    /// <summary>
	    /// 分类名称
	    /// </summary>
		public  string CategoryName {
			set { _CategoryName = value; }
			get { return _CategoryName; }
		}

        
        private  string _MeasurementUnitID;
	    /// <summary>
	    /// 单位ID 字典表维护
	    /// </summary>
		public  string MeasurementUnitID {
			set { _MeasurementUnitID = value; }
			get { return _MeasurementUnitID; }
		}

        
        private  string _Unit;
	    /// <summary>
	    /// 单位
	    /// </summary>
		public  string Unit {
			set { _Unit = value; }
			get { return _Unit; }
		}

        
        private  int _ProductsID;
	    /// <summary>
	    /// Products表ID
	    /// </summary>
		public  int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
		}

        
        private  string _ProductsName;
	    /// <summary>
	    /// 商品名称
	    /// </summary>
		public  string ProductsName {
			set { _ProductsName = value; }
			get { return _ProductsName; }
		}

        
        private  string _ProductsCode;
	    /// <summary>
	    /// 商品编码
	    /// </summary>
		public  string ProductsCode {
			set { _ProductsCode = value; }
			get { return _ProductsCode; }
		}

        
        private  string _ProductsNo;
	    /// <summary>
	    /// 商品货号
	    /// </summary>
		public  string ProductsNo {
			set { _ProductsNo = value; }
			get { return _ProductsNo; }
		}

        
        private  int _ProductsNum;
	    /// <summary>
	    /// 商品数量
	    /// </summary>
		public  int ProductsNum {
			set { _ProductsNum = value; }
			get { return _ProductsNum; }
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
	    /// 商品SKU码
	    /// </summary>
		public  string ProductsSkuCode {
			set { _ProductsSkuCode = value; }
			get { return _ProductsSkuCode; }
		}

        
        private  string _ProductsSkuBarCode;
	    /// <summary>
	    /// 商品条码
	    /// </summary>
		public  string ProductsSkuBarCode {
			set { _ProductsSkuBarCode = value; }
			get { return _ProductsSkuBarCode; }
		}

        
        private  decimal _ProductsWeight;
	    /// <summary>
	    /// 商品重量
	    /// </summary>
		public  decimal ProductsWeight {
			set { _ProductsWeight = value; }
			get { return _ProductsWeight; }
		}

        
        private  string _ProductsSkuSaleprop;
	    /// <summary>
	    /// SKU的属性值。如：机身颜色:黑色;手机套餐:官方标配
	    /// </summary>
		public  string ProductsSkuSaleprop {
			set { _ProductsSkuSaleprop = value; }
			get { return _ProductsSkuSaleprop; }
		}

        
        private  decimal _SellingPrice;
	    /// <summary>
	    /// 商品销售价
	    /// </summary>
		public  decimal SellingPrice {
			set { _SellingPrice = value; }
			get { return _SellingPrice; }
		}

        
        private  decimal _ActualSellingPrice;
	    /// <summary>
	    /// 商品实际销售价 扣除优惠之后的价格
	    /// </summary>
		public  decimal ActualSellingPrice {
			set { _ActualSellingPrice = value; }
			get { return _ActualSellingPrice; }
		}

        
        private  decimal _CostPrice;
	    /// <summary>
	    /// 商品成本价
	    /// </summary>
		public  decimal CostPrice {
			set { _CostPrice = value; }
			get { return _CostPrice; }
		}

        
        private  decimal _DiscountAmount;
	    /// <summary>
	    /// 优惠金额
	    /// </summary>
		public  decimal DiscountAmount {
			set { _DiscountAmount = value; }
			get { return _DiscountAmount; }
		}

        
        private  DateTime _DeliveryDate;
	    /// <summary>
	    /// 发货时间
	    /// </summary>
		public  DateTime DeliveryDate {
			set { _DeliveryDate = value; }
			get { return _DeliveryDate; }
		}

        
        private  int _AddType;
	    /// <summary>
	    /// 添加商品类型 0：销售 1：赠品
	    /// </summary>
		public  int AddType {
			set { _AddType = value; }
			get { return _AddType; }
		}

        
        private  int _CodStatus;
	    /// <summary>
	    /// 货到付款状态 枚举
	    /// </summary>
		public  int CodStatus {
			set { _CodStatus = value; }
			get { return _CodStatus; }
		}

        
        private  decimal _TaxRate;
	    /// <summary>
	    /// 商品税率
	    /// </summary>
		public  decimal TaxRate {
			set { _TaxRate = value; }
			get { return _TaxRate; }
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

