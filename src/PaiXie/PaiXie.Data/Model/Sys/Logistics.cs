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
	public partial class Logistics {
		public Logistics() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _Name;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
		}

        
        private  string _WebUrl;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string WebUrl {
			set { _WebUrl = value; }
			get { return _WebUrl; }
		}

        
        private  int _IsSetArea;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int IsSetArea {
			set { _IsSetArea = value; }
			get { return _IsSetArea; }
		}

        
        private  string _Tags;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string Tags {
			set { _Tags = value; }
			get { return _Tags; }
		}

        
        private  string _KeyWords;
	    /// <summary>
	    /// 
	    /// </summary>
		public  string KeyWords {
			set { _KeyWords = value; }
			get { return _KeyWords; }
		}

        
        private  int _IsEnable;
	    /// <summary>
	    /// 
	    /// </summary>
		public  int IsEnable {
			set { _IsEnable = value; }
			get { return _IsEnable; }
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

		
	}
}

