using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 无注释
	/// </summary>
	[Serializable]
	public partial class WarehouseInventoryWarn {
		public WarehouseInventoryWarn() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编号
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  int _WarnType;
	    /// <summary>
	    /// 预警类型 0：整仓 1：单个商品 2：单个SKU
	    /// </summary>
		public  int WarnType {
			set { _WarnType = value; }
			get { return _WarnType; }
		}

        
        private  int _ProductsID;
	    /// <summary>
		/// Products表主键ID
	    /// </summary>
		public  int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
		}

        
        private  int _ProductsSkuID;
	    /// <summary>
		/// ProductsSku表主键ID
	    /// </summary>
		public  int ProductsSkuID {
			set { _ProductsSkuID = value; }
			get { return _ProductsSkuID; }
		}

        
        private  int _ProductsWarn;
	    /// <summary>
	    /// 商品库存预警数量
	    /// </summary>
		public  int ProductsWarn {
			set { _ProductsWarn = value; }
			get { return _ProductsWarn; }
		}

        
        private  int _ProductsSkuWarn;
	    /// <summary>
	    /// 商品SKU库存预警数量
	    /// </summary>
		public  int ProductsSkuWarn {
			set { _ProductsSkuWarn = value; }
			get { return _ProductsSkuWarn; }
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

