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
	public partial class Sysbutton {
		public Sysbutton() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}


		private string _Url;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Url {
			set { _Url = value; }
			get { return _Url; }
		}
		private string _Code;
		/// <summary>
		/// 
		/// </summary>
		public string Code {
			set { _Code = value; }
			get { return _Code; }
		}
        
        private  string _Name;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
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

        
        private  string _Icon;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Icon {
			set { _Icon = value; }
			get { return _Icon; }
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

        
        private  string _UpdatePeson;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string UpdatePeson {
			set { _UpdatePeson = value; }
			get { return _UpdatePeson; }
		}

        
        private  DateTime _UpdateDate;
	    /// <summary>
	    /// 
	    /// </summary>
		public  DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}

        
        private  string _MenuCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string MenuCode {
			set { _MenuCode = value; }
			get { return _MenuCode; }
		}

		
	}
}

