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
	public partial class Sysuser {
		public Sysuser() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _Code;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Code {
			set { _Code = value; }
			get { return _Code; }
		}

        
        private  int _Seq;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int Seq {
			set { _Seq = value; }
			get { return _Seq; }
		}

        
        private  string _Name;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
		}

        
        private  string _Description;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Description {
			set { _Description = value; }
			get { return _Description; }
		}

        
        private  string _Password;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Password {
			set { _Password = value; }
			get { return _Password; }
		}

        
        private  string _RoleName;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string RoleName {
			set { _RoleName = value; }
			get { return _RoleName; }
		}

        
        private  string _OrganizeName;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string OrganizeName {
			set { _OrganizeName = value; }
			get { return _OrganizeName; }
		}

        
        private  string _ConfigJSON;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ConfigJSON {
			set { _ConfigJSON = value; }
			get { return _ConfigJSON; }
		}

        
        private  int _IsEnable;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int IsEnable {
			set { _IsEnable = value; }
			get { return _IsEnable; }
		}

        
        private  int _LoginCount;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int LoginCount {
			set { _LoginCount = value; }
			get { return _LoginCount; }
		}

        
        private  DateTime _LastLoginDate;
	    /// <summary>
	    /// 
	    /// </summary>
		public  DateTime LastLoginDate {
			set { _LastLoginDate = value; }
			get { return _LastLoginDate; }
		}

        
        private  string _CreatePerson;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}

        
        private  DateTime _CreateDate;
	    /// <summary>
	    /// 
	    /// </summary>
		public  DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}

        
        private  string _UpdatePerson;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string UpdatePerson {
			set { _UpdatePerson = value; }
			get { return _UpdatePerson; }
		}

        
        private  DateTime _UpdateDate;
	    /// <summary>
	    /// 
	    /// </summary>
		public  DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
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

		/// <summary>
		/// 是否超级用户  后端管理员 和  仓库管理员 1  是  0  否  
		/// </summary>
		public int  IsSupper {
			set;
			get;
		}


		


		
	}
}

