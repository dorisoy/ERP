using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 导入失败商品记录表
	/// </summary>
	[Serializable]
	public partial class ProductsImportError {
		public ProductsImportError() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _ProductsCode;
	    /// <summary>
	    /// 商品编码
	    /// </summary>
		public  string ProductsCode {
			set { _ProductsCode = value; }
			get { return _ProductsCode; }
		}

        
        private  string _ProductsTitle;
	    /// <summary>
	    /// 商品名称
	    /// </summary>
		public  string ProductsTitle {
			set { _ProductsTitle = value; }
			get { return _ProductsTitle; }
		}

        
        private  string _Saleprop;
	    /// <summary>
	    /// 商品属性
	    /// </summary>
		public  string Saleprop {
			set { _Saleprop = value; }
			get { return _Saleprop; }
		}

        
        private  string _ProductsSkuCode;
	    /// <summary>
	    /// 商品SKU码
	    /// </summary>
		public  string ProductsSkuCode {
			set { _ProductsSkuCode = value; }
			get { return _ProductsSkuCode; }
		}

        
        private  string _ErrorMessage;
	    /// <summary>
	    /// 失败原因
	    /// </summary>
		public  string ErrorMessage {
			set { _ErrorMessage = value; }
			get { return _ErrorMessage; }
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
	    /// 创建日期
	    /// </summary>
		public  DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}

		
	}
}

