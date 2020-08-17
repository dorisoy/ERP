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
	public partial class Sysmenu {
		public Sysmenu() { }
        
        
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

        
        private  string _ParentCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ParentCode {
			set { _ParentCode = value; }
			get { return _ParentCode; }
		}

        
        private  string _Name;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
		}

        
        private  string _URL;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string URL {
			set { _URL = value; }
			get { return _URL; }
		}

        
        private  string _IconClass;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string IconClass {
			set { _IconClass = value; }
			get { return _IconClass; }
		}

        
        private  string _IconURL;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string IconURL {
			set { _IconURL = value; }
			get { return _IconURL; }
		}

        
        private  int _Seq;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int Seq {
			set { _Seq = value; }
			get { return _Seq; }
		}

        
        private  string _Description;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Description {
			set { _Description = value; }
			get { return _Description; }
		}

        
        private  int _IsVisible;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int IsVisible {
			set { _IsVisible = value; }
			get { return _IsVisible; }
		}

        
        private  int _IsEnable;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int IsEnable {
			set { _IsEnable = value; }
			get { return _IsEnable; }
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

		
	}
}

