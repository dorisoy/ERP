using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class SysloginHistory {
		public SysloginHistory() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _UserCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string UserCode {
			set { _UserCode = value; }
			get { return _UserCode; }
		}

        
        private  string _UserName;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string UserName {
			set { _UserName = value; }
			get { return _UserName; }
		}

        
        private  string _HostName;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string HostName {
			set { _HostName = value; }
			get { return _HostName; }
		}

        
        private  string _HostIP;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string HostIP {
			set { _HostIP = value; }
			get { return _HostIP; }
		}

        
        private  string _LoginCity;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string LoginCity {
			set { _LoginCity = value; }
			get { return _LoginCity; }
		}

        
        private  DateTime _LoginDate;
	    /// <summary>
	    /// 
	    /// </summary>
		public  DateTime LoginDate {
			set { _LoginDate = value; }
			get { return _LoginDate; }
		}

        
        private  int _ModeType;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ModeType {
			set { _ModeType = value; }
			get { return _ModeType; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

		
	}
}

