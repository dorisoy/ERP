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
	public partial class Ordremark {
		public Ordremark() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
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

        
        private  string _Content;
	    /// <summary>
	    /// 备注内容
	    /// </summary>
		public  string Content {
			set { _Content = value; }
			get { return _Content; }
		}

        
        private  string _UserCode;
	    /// <summary>
	    /// 创建人编号
	    /// </summary>
		public  string UserCode {
			set { _UserCode = value; }
			get { return _UserCode; }
		}

        
        private  string _UserName;
	    /// <summary>
	    /// 创建人名称
	    /// </summary>
		public  string UserName {
			set { _UserName = value; }
			get { return _UserName; }
		}

        
        private  DateTime _CreateDate;
	    /// <summary>
	    /// 创建时间
	    /// </summary>
		public  DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}

	}
}

