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
	public partial class Syscode {
		public Syscode() { }
        
        
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

        
        private  string _Text;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Text {
			set { _Text = value; }
			get { return _Text; }
		}

        
        private  string _ParentCode;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string ParentCode {
			set { _ParentCode = value; }
			get { return _ParentCode; }
		}

        
        private  int _Seq;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int Seq {
			set { _Seq = value; }
			get { return _Seq; }
		}

        
        private  int _IsEnable;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int IsEnable {
			set { _IsEnable = value; }
			get { return _IsEnable; }
		}

        
        private  int _IsDefault;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int IsDefault {
			set { _IsDefault = value; }
			get { return _IsDefault; }
		}

        
        private  string _Description;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Description {
			set { _Description = value; }
			get { return _Description; }
		}

        
        private  string _CodeTypeName;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string CodeTypeName {
			set { _CodeTypeName = value; }
			get { return _CodeTypeName; }
		}

        
        private  string _CodeType;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string CodeType {
			set { _CodeType = value; }
			get { return _CodeType; }
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

		
	}
}

