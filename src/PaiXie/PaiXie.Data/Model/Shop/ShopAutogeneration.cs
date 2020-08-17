using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 自动生成设置
	/// </summary>
	[Serializable]
	public partial class ShopAutogeneration {
		public ShopAutogeneration() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  int _ShopID;
	    /// <summary>
	    /// 网店ID
	    /// </summary>
		public  int ShopID {
			set { _ShopID = value; }
			get { return _ShopID; }
		}

        
        private  int _IsAutoDown;
	    /// <summary>
	    /// 是否自动下载订单 0：否 1：是
	    /// </summary>
		public  int IsAutoDown {
			set { _IsAutoDown = value; }
			get { return _IsAutoDown; }
		}

        
        private  int _DownInterval;
	    /// <summary>
	    /// 下载间隔
	    /// </summary>
		public  int DownInterval {
			set { _DownInterval = value; }
			get { return _DownInterval; }
		}

        
        private  int _CreateInterval;
	    /// <summary>
	    /// 下载几分钟前的订单
	    /// </summary>
		public  int CreateInterval {
			set { _CreateInterval = value; }
			get { return _CreateInterval; }
		}

        
        private  int _IsAutogeneration;
	    /// <summary>
	    /// 是否自动生成订单 0：否 1：是
	    /// </summary>
		public  int IsAutogeneration {
			set { _IsAutogeneration = value; }
			get { return _IsAutogeneration; }
		}

        
        private  int _GenerateInterval;
	    /// <summary>
	    /// 生成间隔
	    /// </summary>
		public  int GenerateInterval {
			set { _GenerateInterval = value; }
			get { return _GenerateInterval; }
		}

        
        private  string _NotGenerated;
	    /// <summary>
	    /// 不自动生成情况 1：商品添加错误 2：未匹配发货物流 3：申请退款 4：货到付款 5：需要发票 6：有买家留言 7：有卖家备注
	    /// </summary>
		public  string NotGenerated {
			set { _NotGenerated = value; }
			get { return _NotGenerated; }
		}

        
        private  DateTime _AutoDownTaskTime;
	    /// <summary>
	    /// 自动下载时间
	    /// </summary>
		public  DateTime AutoDownTaskTime {
			set { _AutoDownTaskTime = value; }
			get { return _AutoDownTaskTime; }
		}

        
        private  DateTime _DownCompletionTime;
	    /// <summary>
	    /// 下载完成时间
	    /// </summary>
		public  DateTime DownCompletionTime {
			set { _DownCompletionTime = value; }
			get { return _DownCompletionTime; }
		}

        
        private  DateTime _AutogenerationTime;
	    /// <summary>
	    /// 自动生成时间
	    /// </summary>
		public  DateTime AutogenerationTime {
			set { _AutogenerationTime = value; }
			get { return _AutogenerationTime; }
		}

        
        private  DateTime _GenerateCompletionTime;
	    /// <summary>
	    /// 生成完成时间
	    /// </summary>
		public  DateTime GenerateCompletionTime {
			set { _GenerateCompletionTime = value; }
			get { return _GenerateCompletionTime; }
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

