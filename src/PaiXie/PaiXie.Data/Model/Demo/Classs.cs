using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
	[Serializable]
	public partial class classs {
		public classs() { }
        
        
        	private  int _ID;
	
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        	private  string _ClassName;
	
		public  string ClassName {
			set { _ClassName = value; }
			get { return _ClassName; }
		}

		
	
        
    
	}
}

