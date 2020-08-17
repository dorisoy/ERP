using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaiXie.Erp {
	/// <summary>
	///下拉框实体
	/// </summary>
	public class CListItem {
		public string Text {
			set;
			get;
		}
		public string Value {
			set;
			get;
		}
		public int IsChecked { get; set; }
	}
}