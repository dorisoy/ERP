using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	///  商品信息表
	/// </summary>
	[Serializable]
	public partial class Products {
		public Products() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _No;
	    /// <summary>
	    /// 商品货号：不能为空，自定义
	    /// </summary>
		public  string No {
			set { _No = value; }
			get { return _No; }
		}

        
        private  string _Code;
	    /// <summary>
	    /// 商品编码：不能为空，不能重复，自定义。一个商品编码对应一个商品。当商品没有规格时，商品编码做为商品SKU码
	    /// </summary>
		public  string Code {
			set { _Code = value; }
			get { return _Code; }
		}

        
        private  string _BarCode;
	    /// <summary>
	    /// 商品条码：可为空，不能重复，国家商品标准条码，也可自定义。对应商品。可扫描商品条码确认对应商品（查找、校验、出入库等）
	    /// </summary>
		public  string BarCode {
			set { _BarCode = value; }
			get { return _BarCode; }
		}

        
        private  int _BrandID;
	    /// <summary>
	    /// 商品品牌ID
	    /// </summary>
		public  int BrandID {
			set { _BrandID = value; }
			get { return _BrandID; }
		}

        
        private  int _CategoryID;
	    /// <summary>
	    /// 商品分类ID
	    /// </summary>
		public  int CategoryID {
			set { _CategoryID = value; }
			get { return _CategoryID; }
		}

        
        private  int _ShelfLife;
	    /// <summary>
	    /// 保质期（天） 
	    /// </summary>
		public  int ShelfLife {
			set { _ShelfLife = value; }
			get { return _ShelfLife; }
		}

        
        private  string _Name;
	    /// <summary>
	    /// 商品名称：不能为空
	    /// </summary>
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
		}

        
        private  int _SaleType;
	    /// <summary>
	    /// 商品类型：销售、物料，必选，当仅为物料时，不能上架销售  枚举类型。销售=1 物料=2  采用位运算例：http://www.cnblogs.com/zgqys1980/archive/2010/05/31/1748404.html
	    /// </summary>
		public  int SaleType {
			set { _SaleType = value; }
			get { return _SaleType; }
		}

        
        private  decimal _Weight;
	    /// <summary>
	    /// 商品重量
	    /// </summary>
		public  decimal Weight {
			set { _Weight = value; }
			get { return _Weight; }
		}

        
        private  decimal _CostPrice;
	    /// <summary>
	    /// 商品成本价
	    /// </summary>
		public  decimal CostPrice {
			set { _CostPrice = value; }
			get { return _CostPrice; }
		}

        
        private  decimal _SellingPrice;
	    /// <summary>
	    /// 商品销售价
	    /// </summary>
		public  decimal SellingPrice {
			set { _SellingPrice = value; }
			get { return _SellingPrice; }
		}

        
        private  decimal _TaxRate;
	    /// <summary>
	    /// 商品税率： 必填
	    /// </summary>
		public  decimal TaxRate {
			set { _TaxRate = value; }
			get { return _TaxRate; }
		}

        
        private  string _MeasurementUnitID;
	    /// <summary>
	    /// 单位ID 字典表维护
	    /// </summary>
		public  string MeasurementUnitID {
			set { _MeasurementUnitID = value; }
			get { return _MeasurementUnitID; }
		}

        
        private  string _SmallPic;
	    /// <summary>
	    /// 商品小图
	    /// </summary>
		public  string SmallPic {
			set { _SmallPic = value; }
			get { return _SmallPic; }
		}

        
        private  string _CreatePerson;
	    /// <summary>
	    /// 创建人（创建用户姓名）
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
	    /// 修改人（用户姓名）
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

        
        private  string _Remark;
	    /// <summary>
	    /// 备注
	    /// </summary>
		public  string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}

        
        private  int _Status;
	    /// <summary>
	    /// 销售中=1   仓库中=2 枚举类型
	    /// </summary>
		public  int Status {
			set { _Status = value; }
			get { return _Status; }
		}

		private int _IsDelete;
	    /// <summary>
	    /// 是否删除 0否 1是
	    /// </summary>
		public int IsDelete {
			set { _IsDelete = value; }
			get { return _IsDelete; }
		}
	}
}

