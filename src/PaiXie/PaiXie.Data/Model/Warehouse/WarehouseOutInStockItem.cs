using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data {
	/// <summary>
	/// 出入库单明细表
	/// </summary>
	[Serializable]
	public partial class WarehouseOutInStockItem {
		public WarehouseOutInStockItem() { }


		private int _ID;
		/// <summary>
		/// 主键ID
		/// </summary>
		public int ID {
			set { _ID = value; }
			get { return _ID; }
		}


		private int _OutInStockID;
		/// <summary>
		/// 出入库单表标识
		/// </summary>
		public int OutInStockID {
			set { _OutInStockID = value; }
			get { return _OutInStockID; }
		}


		private string _OutInStockBillNo;
		/// <summary>
		/// 出入库单据编号 系统生成唯一
		/// </summary>
		public string OutInStockBillNo {
			set { _OutInStockBillNo = value; }
			get { return _OutInStockBillNo; }
		}


		private int _BillType;
		/// <summary>
		/// 单据类型 枚举 采购入库10,采购退货20,其它入库30,其它出库40,退货入库50,销售出库60,盘点70,移位80,调拨入库90,调拨出库100
		/// </summary>
		public int BillType {
			set { _BillType = value; }
			get { return _BillType; }
		}


		private int _SourceID;
		/// <summary>
		/// 来源单据标识
		/// </summary>
		public int SourceID {
			set { _SourceID = value; }
			get { return _SourceID; }
		}


		private string _SourceNo;
		/// <summary>
		/// 来源单据编号
		/// </summary>
		public string SourceNo {
			set { _SourceNo = value; }
			get { return _SourceNo; }
		}


		private int _StockWay;
		/// <summary>
		/// 出入库方向 -1出库 1入库
		/// </summary>
		public int StockWay {
			set { _StockWay = value; }
			get { return _StockWay; }
		}


		private string _WarehouseCode;
		/// <summary>
		/// 仓库编号
		/// </summary>
		public string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}


		private int _Status;
		/// <summary>
		/// 单据状态 枚举  状态: 未提交=1待审核=2已审核=3
		/// </summary>
		public int Status {
			set { _Status = value; }
			get { return _Status; }
		}


		private int _IsAuditPrice;
		/// <summary>
		/// 是否财审0否1是
		/// </summary>
		public int IsAuditPrice {
			set { _IsAuditPrice = value; }
			get { return _IsAuditPrice; }
		}


		private int _ProductsID;
		/// <summary>
		/// 商品表标识
		/// </summary>
		public int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
		}


		private string _ProductsCode;
		/// <summary>
		/// 商品编码
		/// </summary>
		public string ProductsCode {
			set { _ProductsCode = value; }
			get { return _ProductsCode; }
		}


		private string _ProductsName;
		/// <summary>
		/// 商品名称
		/// </summary>
		public string ProductsName {
			set { _ProductsName = value; }
			get { return _ProductsName; }
		}


		private string _ProductsNo;
		/// <summary>
		/// 商品货号
		/// </summary>
		public string ProductsNo {
			set { _ProductsNo = value; }
			get { return _ProductsNo; }
		}


		private int _ProductsSkuID;
		/// <summary>
		/// 商品Sku表标识
		/// </summary>
		public int ProductsSkuID {
			set { _ProductsSkuID = value; }
			get { return _ProductsSkuID; }
		}


		private string _ProductsSkuCode;
		/// <summary>
		/// Sku码
		/// </summary>
		public string ProductsSkuCode {
			set { _ProductsSkuCode = value; }
			get { return _ProductsSkuCode; }
		}


		private string _ProductsSkuSaleprop;
		/// <summary>
		/// 销售属性(颜色：红色 规格：S)
		/// </summary>
		public string ProductsSkuSaleprop {
			set { _ProductsSkuSaleprop = value; }
			get { return _ProductsSkuSaleprop; }
		}


		private int _ProductsNum;
		/// <summary>
		/// 商品数量
		/// </summary>
		public int ProductsNum {
			set { _ProductsNum = value; }
			get { return _ProductsNum; }
		}


		private int _LocationID;
		/// <summary>
		/// 库位表标识
		/// </summary>
		public int LocationID {
			set { _LocationID = value; }
			get { return _LocationID; }
		}


		private int _ProductsBatchID;
		/// <summary>
		/// 商品批次表标识
		/// </summary>
		public int ProductsBatchID {
			set { _ProductsBatchID = value; }
			get { return _ProductsBatchID; }
		}


		private string _ProductsBatchCode;
		/// <summary>
		/// 商品批次号
		/// </summary>
		public string ProductsBatchCode {
			set { _ProductsBatchCode = value; }
			get { return _ProductsBatchCode; }
		}


		private DateTime _ProductionDate;
		/// <summary>
		/// 生产日期
		/// </summary>
		public DateTime ProductionDate {
			set { _ProductionDate = value; }
			get { return _ProductionDate; }
		}


		private decimal _CostPrice;
		/// <summary>
		/// 成本价 
		/// </summary>
		public decimal CostPrice {
			set { _CostPrice = value; }
			get { return _CostPrice; }
		}


		private string _Remark;
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}


		private DateTime _ConfirmDate;
		/// <summary>
		/// 确认时间
		/// </summary>
		public DateTime ConfirmDate {
			set { _ConfirmDate = value; }
			get { return _ConfirmDate; }
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


		private int _SourceItemID;
		/// <summary>
		/// 来源单据明细标识
		/// </summary>
		public int SourceItemID {
			set { _SourceItemID = value; }
			get { return _SourceItemID; }
		}
	}
}

