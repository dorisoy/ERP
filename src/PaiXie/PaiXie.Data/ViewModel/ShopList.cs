using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	public class ShopList : Shop {

		public string cname { set; get; }
		public string uname { set; get; }
		/// <summary>
		/// 店铺类型code
		/// </summary>
		public string part1 { set; get; }
		/// <summary>
		/// 平台类型 code
		/// </summary>
		public string part2 { set; get; }

		
	}
	
}
