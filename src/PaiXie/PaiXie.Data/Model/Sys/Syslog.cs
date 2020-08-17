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
	public partial class Syslog {
		public Syslog() { }
        
        
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

        
        private  string _Position;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Position {
			set { _Position = value; }
			get { return _Position; }
		}

        
        private  string _Target;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Target {
			set { _Target = value; }
			get { return _Target; }
		}

        
        private  int _Type;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int Type {
			set { _Type = value; }
			get { return _Type; }
		}

        
        private  string _OldMessage;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string OldMessage {
			set { _OldMessage = value; }
			get { return _OldMessage; }
		}

        
        private  DateTime _Date;
	    /// <summary>
	    /// 
	    /// </summary>
		public  DateTime Date {
			set { _Date = value; }
			get { return _Date; }
		}

        
        private  string _ButtonName;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ButtonName {
			set { _ButtonName = value; }
			get { return _ButtonName; }
		}

        
        private  string _Message;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Message {
			set { _Message = value; }
			get { return _Message; }
		}

        
        private  string _Part1;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Part1 {
			set { _Part1 = value; }
			get { return _Part1; }
		}

        
        private  string _Part2;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Part2 {
			set { _Part2 = value; }
			get { return _Part2; }
		}

        
        private  string _Part3;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Part3 {
			set { _Part3 = value; }
			get { return _Part3; }
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

