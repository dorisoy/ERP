using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 供应商表
	/// </summary>
	[Serializable]
	public partial class Suppliers {
		public Suppliers() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 供应商表主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _Name;
	    /// <summary>
	    /// 供应商名称
	    /// </summary>
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
		}

        
        private  string _AliasName;
	    /// <summary>
	    /// 简称
	    /// </summary>
		public  string AliasName {
			set { _AliasName = value; }
			get { return _AliasName; }
		}

        
        private  string _ContactPerson;
	    /// <summary>
	    /// 联系人
	    /// </summary>
		public  string ContactPerson {
			set { _ContactPerson = value; }
			get { return _ContactPerson; }
		}

        
        private  string _Tel;
	    /// <summary>
	    /// 供应商电话
	    /// </summary>
		public  string Tel {
			set { _Tel = value; }
			get { return _Tel; }
		}

        
        private  string _Fax;
	    /// <summary>
	    /// 传真
	    /// </summary>
		public  string Fax {
			set { _Fax = value; }
			get { return _Fax; }
		}

        
        private  string _Email;
	    /// <summary>
	    /// 邮件
	    /// </summary>
		public  string Email {
			set { _Email = value; }
			get { return _Email; }
		}

        
        private  string _CreatePerson;
	    /// <summary>
	    /// 创建人
	    /// </summary>
		public  string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}

		private int _IsEnable;
		/// <summary>
		/// 是否可用 0:否 1:是
		/// </summary>
		public int IsEnable {
			set { _IsEnable = value; }
			get { return _IsEnable; }
		}

        private  DateTime _CreateDate;
	    /// <summary>
	    /// 创建时间
	    /// </summary>
		public  DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}

        
        private  string _UpdatePerson;
	    /// <summary>
	    /// 修改人
	    /// </summary>
		public  string UpdatePerson {
			set { _UpdatePerson = value; }
			get { return _UpdatePerson; }
		}

        
        private  DateTime _UpdateDate;
	    /// <summary>
	    /// 修改时间
	    /// </summary>
		public  DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}

		
	}
}

