using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 省市区街道表 
	/// </summary>
	[Serializable]
	public partial class Sysarea {
		public Sysarea() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _Name;
	    /// <summary>
	    /// 地区名称
	    /// </summary>
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
		}

        
        private  int _ParentID;
	    /// <summary>
	    /// 父级ID 顶级为0
	    /// </summary>
		public  int ParentID {
			set { _ParentID = value; }
			get { return _ParentID; }
		}

        
        private  string _PostCode;
	    /// <summary>
	    /// 邮政编码
	    /// </summary>
		public  string PostCode {
			set { _PostCode = value; }
			get { return _PostCode; }
		}

        
        private  string _LargeArea;
	    /// <summary>
	    /// 大区名称 例如：华东、华北
	    /// </summary>
		public  string LargeArea {
			set { _LargeArea = value; }
			get { return _LargeArea; }
		}

        
        private  string _AliasName;
	    /// <summary>
	    /// 地区简称
	    /// </summary>
		public  string AliasName {
			set { _AliasName = value; }
			get { return _AliasName; }
		}

        
        private  string _AreaKeys;
	    /// <summary>
	    /// 地区关键字
	    /// </summary>
		public  string AreaKeys {
			set { _AreaKeys = value; }
			get { return _AreaKeys; }
		}

        
        private  int _Seq;
	    /// <summary>
	    /// 排序字段
	    /// </summary>
		public  int Seq {
			set { _Seq = value; }
			get { return _Seq; }
		}

	}
}

