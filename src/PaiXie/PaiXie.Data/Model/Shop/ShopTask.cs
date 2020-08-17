using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 店铺任务表
	/// </summary>
	[Serializable]
	public partial class ShopTask {
		public ShopTask() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _TaskID;
	    /// <summary>
	    /// 任务标识ID guid
	    /// </summary>
		public  string TaskID {
			set { _TaskID = value; }
			get { return _TaskID; }
		}

        
        private  int _TaskType;
	    /// <summary>
	    /// 任务类型 枚举
	    /// </summary>
		public  int TaskType {
			set { _TaskType = value; }
			get { return _TaskType; }
		}

        
        private  int _ShopID;
	    /// <summary>
	    /// 店铺ID
	    /// </summary>
		public  int ShopID {
			set { _ShopID = value; }
			get { return _ShopID; }
		}

        
        private  int _TotalCount;
	    /// <summary>
	    /// 任务总条数
	    /// </summary>
		public  int TotalCount {
			set { _TotalCount = value; }
			get { return _TotalCount; }
		}

        
        private  int _FinshCount;
	    /// <summary>
	    /// 任务完成数
	    /// </summary>
		public  int FinshCount {
			set { _FinshCount = value; }
			get { return _FinshCount; }
		}

        
        private  int _TaskStatus;
	    /// <summary>
	    /// 1：进行中 2：已结束
	    /// </summary>
		public  int TaskStatus {
			set { _TaskStatus = value; }
			get { return _TaskStatus; }
		}

		private string _Message;
	    /// <summary>
	    /// 任务消息
	    /// </summary>
		public string Message {
			set { _Message = value; }
			get { return _Message; }
		}
        
        private  string _RequestMessage;
	    /// <summary>
	    /// 请求报文
	    /// </summary>
		public  string RequestMessage {
			set { _RequestMessage = value; }
			get { return _RequestMessage; }
		}

        
        private  string _ResponseMessage;
	    /// <summary>
	    /// 响应报文
	    /// </summary>
		public  string ResponseMessage {
			set { _ResponseMessage = value; }
			get { return _ResponseMessage; }
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

        
        private  DateTime _UpdateDate;
	    /// <summary>
	    /// 更新时间
	    /// </summary>
		public  DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}

		
	}
}

