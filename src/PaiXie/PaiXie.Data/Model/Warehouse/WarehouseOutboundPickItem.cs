using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 出库单拣货表
	/// </summary>
	[Serializable]
	public partial class WarehouseOutboundPickItem {
		public WarehouseOutboundPickItem() { }
        
        
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

        
        private  int _OutboundID;
	    /// <summary>
	    /// 出库单主键ID
	    /// </summary>
		public  int OutboundID {
			set { _OutboundID = value; }
			get { return _OutboundID; }
		}

        
        private  string _OutboundBillNo;
	    /// <summary>
	    /// 出库单号
	    /// </summary>
		public  string OutboundBillNo {
			set { _OutboundBillNo = value; }
			get { return _OutboundBillNo; }
		}

        
        private  int _OrditemID;
	    /// <summary>
	    /// 订单商品表主键ID
	    /// </summary>
		public int OrditemID {
			set { _OrditemID = value; }
			get { return _OrditemID; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编码
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  int _ProductsID;
	    /// <summary>
	    /// 商品ID
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

        
        private  int _ProductsSkuID;
	    /// <summary>
	    /// 商品SKUID
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

        
        private  string _ProductsSkuSaleprop;
	    /// <summary>
	    /// 商品属性
	    /// </summary>
		public  string ProductsSkuSaleprop {
			set { _ProductsSkuSaleprop = value; }
			get { return _ProductsSkuSaleprop; }
		}

        
        private  int _ProductsBatchID;
	    /// <summary>
	    /// 批次ID 为0表示是预售
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

        
        private  int _LocationID;
	    /// <summary>
	    /// 库位ID 为0表示是预售
	    /// </summary>
		public  int LocationID {
			set { _LocationID = value; }
			get { return _LocationID; }
		}

        
        private  string _LocationCode;
	    /// <summary>
	    /// 库位编码
	    /// </summary>
		public  string LocationCode {
			set { _LocationCode = value; }
			get { return _LocationCode; }
		}

        
        private  string _LocationName;
	    /// <summary>
	    /// 库位名称
	    /// </summary>
		public  string LocationName {
			set { _LocationName = value; }
			get { return _LocationName; }
		}

        
        private  int _Num;
	    /// <summary>
	    /// 拣货数量
	    /// </summary>
		public  int Num {
			set { _Num = value; }
			get { return _Num; }
		}

		private decimal _ActualSellingPrice;
		/// <summary>
		/// 实际销售价
		/// </summary>
		public decimal ActualSellingPrice {
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

