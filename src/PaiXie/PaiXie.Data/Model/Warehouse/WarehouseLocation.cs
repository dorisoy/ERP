using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 仓库库位表
	/// </summary>
	[Serializable]
	public partial class WarehouseLocation {
		public WarehouseLocation() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编号
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  string _StructName;
	    /// <summary>
	    /// 层级名称（例如：区、位）
	    /// </summary>
		public  string StructName {
			set { _StructName = value; }
			get { return _StructName; }
		}

        
        private  string _StructCode;
	    /// <summary>
	    /// 层级代码(库位编码的组成部分)
	    /// </summary>
		public  string StructCode {
			set { _StructCode = value; }
			get { return _StructCode; }
		}

        
        private  string _Code;
	    /// <summary>
	    /// 库位编码(由父级别结构代码+本身结构代码生成 )
	    /// </summary>
		public  string Code {
			set { _Code = value; }
			get { return _Code; }
		}

        
        private  string _Name;
	    /// <summary>
	    /// 库位名称
	    /// </summary>
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
		}

        
        private  int _TypeID;
	    /// <summary>
	    /// 库区类型id（包括中转区=1、废品区=2、发货区=3、备用区=4 单选）
	    /// </summary>
		public  int TypeID {
			set { _TypeID = value; }
			get { return _TypeID; }
		}

        
        private  int _ParentID;
	    /// <summary>
	    /// 父级ID
	    /// </summary>
		public  int ParentID {
			set { _ParentID = value; }
			get { return _ParentID; }
		}

        
        private  int _IsEnable;
	    /// <summary>
	    /// 是否可用（１是０否）
	    /// </summary>
		public  int IsEnable {
			set { _IsEnable = value; }
			get { return _IsEnable; }
		}

        
        private  int _Seq;
	    /// <summary>
	    /// 排序ID
	    /// </summary>
		public  int Seq {
			set { _Seq = value; }
			get { return _Seq; }
		}

        
        private  string _CreatePerson;
	    /// <summary>
	    /// 创建人
	    /// </summary>
		public  string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
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

