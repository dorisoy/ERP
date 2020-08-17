using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
	[Serializable]
	public partial class Tree {
		public Tree() { }
        
        
        	private  int _ID;
	
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        	private  string _Name;
	
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
		}

        
        	private  int _ParentID;
	
		public  int ParentID {
			set { _ParentID = value; }
			get { return _ParentID; }
		}

        
        	private  string _State;
	
		public  string State {
			set { _State = value; }
			get { return _State; }
		}

        
        	private  string _Url;
	
		public  string Url {
			set { _Url = value; }
			get { return _Url; }
		}

		
	
        
    
	}
}

