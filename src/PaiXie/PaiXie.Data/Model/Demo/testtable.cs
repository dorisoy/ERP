using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// testtable:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class testtable {
		public testtable() { }
		#region Model
		private long _id;
		private string _name;
		private DateTime? _cretime;
		/// <summary>
		/// auto_increment
		/// </summary>
		public long ID {
			set { _id = value; }
			get { return _id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string name {
			set { _name = value; }
			get { return _name; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? creTime {
			set { _cretime = value; }
			get { return _cretime; }
		}
		#endregion Model

	}
}
