using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 商品-物料关联表 
	/// </summary>
	[Serializable]
	public partial class ProductsMaterialMap {
		public ProductsMaterialMap() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  int _SourceProductsSkuID;
	    /// <summary>
	    /// 商品sku标识 例子：盒苹果商品
	    /// </summary>
		public  int SourceProductsSkuID {
			set { _SourceProductsSkuID = value; }
			get { return _SourceProductsSkuID; }
		}

        
        private  int _FromProductsSkuID;
	    /// <summary>
	    /// 被引用的 商品sku标识  引用个苹果商品编号
	    /// </summary>
		public  int FromProductsSkuID {
			set { _FromProductsSkuID = value; }
			get { return _FromProductsSkuID; }
		}

        
        private  int _FromNum;
	    /// <summary>
	    /// 引用的数量 默认是1  引用数量是4 
	    /// </summary>
		public  int FromNum {
			set { _FromNum = value; }
			get { return _FromNum; }
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

		
	}
}

