using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
	[Serializable]
	public partial class studentList :student {
		public studentList() { }
		private string _ClassName;

			public string ClassName {
			set { _ClassName = value; }
			get { return _ClassName; }
		}

		
	
        
    
	}
}

