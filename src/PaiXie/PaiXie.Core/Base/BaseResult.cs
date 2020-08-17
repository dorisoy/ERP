using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Core {
	public class BaseResult {
		public BaseResult() {
			result = 1;
			message = "OK";
		}
		/// <summary>
		/// 操作结果代码 默认值1代表成功，其它都是失败
		/// </summary>
		public int result { get; set; }
		/// <summary>
		///结果消息 默认值 OK
		/// </summary>
		public string  message { get; set; }
	}
}
