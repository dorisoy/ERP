using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 快递按重计费表
	/// </summary>
	[Serializable]
	public partial class WarehouseExpressPrice {
		public WarehouseExpressPrice() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编码
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  int _ExpressID;
	    /// <summary>
	    /// 快递公司ID
	    /// </summary>
		public  int ExpressID {
			set { _ExpressID = value; }
			get { return _ExpressID; }
		}
        
        private  string _SysAreaNames;
	    /// <summary>
	    /// 系统区域 多个以半角逗号隔开
	    /// </summary>
		public  string SysAreaNames {
			set { _SysAreaNames = value; }
			get { return _SysAreaNames; }
		}

        
        private  string _SysAreaIDs;
	    /// <summary>
	    /// 系统区域ID 多个以半角逗号隔开
	    /// </summary>
		public  string SysAreaIDs {
			set { _SysAreaIDs = value; }
			get { return _SysAreaIDs; }
		}

        
        private  decimal _FirstWeight;
	    /// <summary>
	    /// 首重
	    /// </summary>
		public  decimal FirstWeight {
			set { _FirstWeight = value; }
			get { return _FirstWeight; }
		}

        
        private  decimal _ContinueWeight;
	    /// <summary>
	    /// 续重
	    /// </summary>
		public  decimal ContinueWeight {
			set { _ContinueWeight = value; }
			get { return _ContinueWeight; }
		}

        
        private  decimal _FirstPrice;
	    /// <summary>
	    /// 首费
	    /// </summary>
		public  decimal FirstPrice {
			set { _FirstPrice = value; }
			get { return _FirstPrice; }
		}

        
        private  decimal _ContinuePrice;
	    /// <summary>
	    /// 续费
	    /// </summary>
		public  decimal ContinuePrice {
			set { _ContinuePrice = value; }
			get { return _ContinuePrice; }
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

