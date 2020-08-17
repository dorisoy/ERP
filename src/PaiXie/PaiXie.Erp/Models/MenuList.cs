using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaiXie.Erp {
	
	public class menusItems {
		/// <summary>
		/// 
		/// </summary>
		public string menuid { get; set; }

		/// <summary>
		/// 添加列表
		/// </summary>
		public string menuname { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string icon { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string url { get; set; }

	}



	public class menusItem {
		/// <summary>
		/// 
		/// </summary>
		public string menuid { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string icon { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string menuname { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public List<menusItems> menus { get; set; }

	}



	public class MenuList {
		/// <summary>
		/// 
		/// </summary>
		public List<menusItem> menus { get; set; }

	}

}