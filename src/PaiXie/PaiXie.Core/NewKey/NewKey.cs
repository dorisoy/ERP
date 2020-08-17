using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Core {
	
	public class NewKey {
	/// <summary>
	///  单据号 用于  订单号
	/// </summary>
	/// <returns></returns>
		public static string datetime() {
			return DateTime.Now.ToString("yyMMddHHmmssffff");
		}
		/// <summary>
		/// 唯一值
		/// </summary>
		/// <returns></returns>
		public static string guid() {
			return Guid.NewGuid().ToString().Replace("-", "");
		}
	}
}