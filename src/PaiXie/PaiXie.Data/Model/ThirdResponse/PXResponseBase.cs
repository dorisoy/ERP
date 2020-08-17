using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 接口返回的参数  公用参数
	/// </summary>
 public 	class PXResponseBase {

	
		public int status { get; set; }
		public int code { get; set; }
		public string msg { get; set; }
	}
}
