using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 系统错误日志表
	/// </summary>
	[Serializable]
	public partial class SyserrorLog {
		public SyserrorLog() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _ErrorTitle;
	    /// <summary>
	    /// 错误标题
	    /// </summary>
		public  string ErrorTitle {
			set { _ErrorTitle = value; }
			get { return _ErrorTitle; }
		}

        
        private  string _ErrorUrl;
	    /// <summary>
	    /// 错误地址
	    /// </summary>
		public  string ErrorUrl {
			set { _ErrorUrl = value; }
			get { return _ErrorUrl; }
		}

        
        private  string _FriendlyMessage;
	    /// <summary>
	    /// 错误友好信息
	    /// </summary>
		public  string FriendlyMessage {
			set { _FriendlyMessage = value; }
			get { return _FriendlyMessage; }
		}

		private string _TargetSite;
	    /// <summary>
	    /// 异常方法
	    /// </summary>
		public string TargetSite {
			set { _TargetSite = value; }
			get { return _TargetSite; }
		}
        
        private  string _StackTrace;
	    /// <summary>
	    /// 堆栈消息
	    /// </summary>
		public  string StackTrace {
			set { _StackTrace = value; }
			get { return _StackTrace; }
		}

		private string _Remark;
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark {
			set { _Remark = value; }
			get { return _Remark; }
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

