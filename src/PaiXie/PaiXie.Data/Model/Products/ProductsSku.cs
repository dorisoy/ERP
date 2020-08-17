using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 商品sku码信息表 
	/// </summary>
	[Serializable]
	public partial class ProductsSku {
		public ProductsSku() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _Code;
	    /// <summary>
	    /// 商品SKU码：不能为空，不能重复，自定义。对应最小库存单位，可扫描商品SKU码确认对应商品（查找、校验、出入库等）
	    /// </summary>
		public  string Code {
			set { _Code = value; }
			get { return _Code; }
		}

        
        private  string _Saleprop;
	    /// <summary>
	    /// 商品销售属性：一个商品可能会因为不同的销售属性，分为多个SKU。字符串形式保存，建议每一个“属性:属性值”为一对，每对属性之间使用半角分号“;”分隔。       例  颜色:黑色;规格:M;套餐:A级套餐   。 一个商品可以对应多个销售属性，每个销售属性对应一个商品SKU码。文本类型用户自己录入
	    /// </summary>
		public  string Saleprop {
			set { _Saleprop = value; }
			get { return _Saleprop; }
		}

        
        private  string _BarCode;
	    /// <summary>
	    /// 商品sku条码：可为空，不能重复，国家商品标准条码，也可自定义。对应最终单品。可扫描商品条码确认对应商品（查找、校验、出入库等）
	    /// </summary>
		public  string BarCode {
			set { _BarCode = value; }
			get { return _BarCode; }
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
	    /// 商品编码 关联商品表
	    /// </summary>
		public  string ProductsCode {
			set { _ProductsCode = value; }
			get { return _ProductsCode; }
		}

        
        private  decimal _Weight;
	    /// <summary>
	    /// 商品sku重量
	    /// </summary>
		public  decimal Weight {
			set { _Weight = value; }
			get { return _Weight; }
		}

        
        private  decimal _CostPrice;
	    /// <summary>
	    /// 商品成本价？ 添加的时候 默认加载商品对应的价格
	    /// </summary>
		public  decimal CostPrice {
			set { _CostPrice = value; }
			get { return _CostPrice; }
		}

        
        private  decimal _SellingPrice;
	    /// <summary>
	    /// 商品销售价？添加的时候 默认加载商品对应的价格
	    /// </summary>
		public  decimal SellingPrice {
			set { _SellingPrice = value; }
			get { return _SellingPrice; }
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

