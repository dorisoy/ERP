using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 仓库信息表
	/// </summary>
	[Serializable]
	public partial class Warehouse {
		public Warehouse() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _Code;
	    /// <summary>
	    /// 仓库编号 唯一值
	    /// </summary>
		public  string Code {
			set { _Code = value; }
			get { return _Code; }
		}

        
        private  string _Name;
	    /// <summary>
	    /// 仓库名称
	    /// </summary>
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
		}

        
        private  int _IsEnable;
	    /// <summary>
	    /// 是否可用 0：否 1：是
	    /// </summary>
		public  int IsEnable {
			set { _IsEnable = value; }
			get { return _IsEnable; }
		}

        
        private  string _CreatePerson;
	    /// <summary>
	    /// 创建人（创建用户姓名）
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
	    /// 修改人（用户姓名）
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

        
        private  string _Remark;
	    /// <summary>
	    /// 备注
	    /// </summary>
		public  string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}

        
        private  string _Address;
	    /// <summary>
	    /// 仓库地址
	    /// </summary>
		public  string Address {
			set { _Address = value; }
			get { return _Address; }
		}

        
        private  string _Longitude;
	    /// <summary>
	    /// 经度
	    /// </summary>
		public  string Longitude {
			set { _Longitude = value; }
			get { return _Longitude; }
		}

        
        private  string _Latitude;
	    /// <summary>
	    /// 纬度
	    /// </summary>
		public  string Latitude {
			set { _Latitude = value; }
			get { return _Latitude; }
		}

        
        private  string _Librand;
	    /// <summary>
	    /// 授权品牌 字典表   多选  “,”隔开
	    /// </summary>
		public  string Librand {
			set { _Librand = value; }
			get { return _Librand; }
		}

		private int _Seq;
		/// <summary>
		/// 排序ID
		/// </summary>
		public int Seq {
			set { _Seq = value; }
			get { return _Seq; }
		}
	}
}

