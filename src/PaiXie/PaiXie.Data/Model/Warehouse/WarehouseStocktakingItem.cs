using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 盘点记录商品
	/// </summary>
	[Serializable]
	public partial class WarehouseStocktakingItem {
		public WarehouseStocktakingItem() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  int _StocktakingID;
	    /// <summary>
	    /// warehouseStocktaking主键ID
	    /// </summary>
		public  int StocktakingID {
			set { _StocktakingID = value; }
			get { return _StocktakingID; }
		}

        
        private  string _BillNo;
	    /// <summary>
	    /// 盘点记录号
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

        
        private  int _TopLocationID;
	    /// <summary>
	    /// 库位所属库区ID
	    /// </summary>
		public  int TopLocationID {
			set { _TopLocationID = value; }
			get { return _TopLocationID; }
		}


		private int _SourceID;
		/// <summary>
		/// warehouselocationproducts表ID
		/// </summary>
		public int SourceID {
			set { _SourceID = value; }
			get { return _SourceID; }
		}


        private  int _LocationID;
	    /// <summary>
	    /// 库位ID(库位信息表主键)
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

        
        private  int _LocationTypeID;
	    /// <summary>
	    /// 库位类型ID（包括中转区=1、废品区=2、发货区=3、备用区=4 单选）
	    /// </summary>
		public  int LocationTypeID {
			set { _LocationTypeID = value; }
			get { return _LocationTypeID; }
		}


		private int _LocationProductsID;
		/// <summary>
		/// 库位ID(库位信息表主键)
		/// </summary>
		public int LocationProductsID {
			set { _LocationProductsID = value; }
			get { return _LocationProductsID; }
		}

        
        private  int _ProductsID;
	    /// <summary>
	    /// 商品表标识
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
	    /// 商品Sku表标识
	    /// </summary>
		public  int ProductsSkuID {
			set { _ProductsSkuID = value; }
			get { return _ProductsSkuID; }
		}

        
        private  string _ProductsSkuCode;
	    /// <summary>
	    /// Sku码
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
	    /// 商品批次表标识
	    /// </summary>
		public  int ProductsBatchID {
			set { _ProductsBatchID = value; }
			get { return _ProductsBatchID; }
		}

        
        private  string _ProductsBatchCode;
	    /// <summary>
	    /// 商品批次号
	    /// </summary>
		public  string ProductsBatchCode {
			set { _ProductsBatchCode = value; }
			get { return _ProductsBatchCode; }
		}

        
        private  DateTime _ProductionDate;
	    /// <summary>
	    /// 生产日期
	    /// </summary>
		public  DateTime ProductionDate {
			set { _ProductionDate = value; }
			get { return _ProductionDate; }
		}

        
        private  int _ShelfLife;
	    /// <summary>
	    /// 保质期（天）
	    /// </summary>
		public  int ShelfLife {
			set { _ShelfLife = value; }
			get { return _ShelfLife; }
		}

        
        private  int _KyNum;
	    /// <summary>
	    /// 可用数量
	    /// </summary>
		public  int KyNum {
			set { _KyNum = value; }
			get { return _KyNum; }
		}

        
        private  int _ZyNum;
	    /// <summary>
	    /// 占用数量
	    /// </summary>
		public  int ZyNum {
			set { _ZyNum = value; }
			get { return _ZyNum; }
		}

        
        private  int _DjNum;
	    /// <summary>
	    /// 冻结数量
	    /// </summary>
		public  int DjNum {
			set { _DjNum = value; }
			get { return _DjNum; }
		}

        
        private  int _SdNum;
	    /// <summary>
	    /// 手动冻结
	    /// </summary>
		public  int SdNum {
			set { _SdNum = value; }
			get { return _SdNum; }
		}

        
        private  int _ZkNum;
	    /// <summary>
	    /// 在库数量
	    /// </summary>
		public  int ZkNum {
			set { _ZkNum = value; }
			get { return _ZkNum; }
		}

        
        private  int _PdNum;
	    /// <summary>
	    /// 盘点数量
	    /// </summary>
		public  int PdNum {
			set { _PdNum = value; }
			get { return _PdNum; }
		}


		private int _IsImport;
		/// <summary>
		/// 是否导入盘点 0：否 1：是
		/// </summary>
		public int IsImport {
			set { _IsImport = value; }
			get { return _IsImport; }
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

		/// <summary>
		/// 成本价
		/// </summary>
		public decimal CostPrice {
			set;
			get;
		}
		
	}
}

