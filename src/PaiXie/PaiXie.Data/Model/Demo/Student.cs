using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
	[Serializable]
	public partial class student {
		public student() { }
        
        
        	private  int _ID;
	
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        	private  string _SstuNmae;
	
		public  string SstuNmae {
			set { _SstuNmae = value; }
			get { return _SstuNmae; }
		}

        
        	private  string _Sex;
	
		public  string Sex {
			set { _Sex = value; }
			get { return _Sex; }
		}

        
        	private  int _ClassId;
	
		public  int ClassId {
			set { _ClassId = value; }
			get { return _ClassId; }
		}

        
        	private  DateTime _CreTime;
	
		public  DateTime CreTime {
			set { _CreTime = value; }
			get { return _CreTime; }
		}

        
        	private  string _IsTuanYuan;
	
		public  string IsTuanYuan {
			set { _IsTuanYuan = value; }
			get { return _IsTuanYuan; }
		}

        
        	private  decimal _Score;
	
		public  decimal Score {
			set { _Score = value; }
			get { return _Score; }
		}

        
        	private  string _Remark;
	
		public  string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}

		
	
        
    
	}
}

