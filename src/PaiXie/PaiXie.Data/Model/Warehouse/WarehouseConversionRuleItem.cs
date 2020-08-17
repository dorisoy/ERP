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
	public partial class WarehouseConversionRuleItem {
		public WarehouseConversionRuleItem() { }
        
        
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

        
        private  string _RuleName;
	    /// <summary>
	    /// 规则名称
	    /// </summary>
		public  string RuleName {
			set { _RuleName = value; }
			get { return _RuleName; }
		}

        
        private  int _RuleID;
	    /// <summary>
	    /// warehouseConversionRule表主键ID
	    /// </summary>
		public  int RuleID {
			set { _RuleID = value; }
			get { return _RuleID; }
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

        
        private  string _ProductsSkuCode;
	    /// <summary>
	    /// 商品SKU码
	    /// </summary>
		public  string ProductsSkuCode {
			set { _ProductsSkuCode = value; }
			get { return _ProductsSkuCode; }
		}

        
        private  int _Num;
	    /// <summary>
	    /// 转换数量
	    /// </summary>
		public  int Num {
			set { _Num = value; }
			get { return _Num; }
		}

        
        private  int _ConversionWay;
	    /// <summary>
	    /// 转换方向，左右可以互转 0：左边商品 1：右边商品
	    /// </summary>
		public  int ConversionWay {
			set { _ConversionWay = value; }
			get { return _ConversionWay; }
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

