using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Utils {
	public class Base64 {
		/// <summary>
		///  编码
		/// </summary>
		/// <param name="OldStatus"></param>
		/// <param name="Action"></param>
		/// <param name="StatusVal"></param>
		/// <returns></returns>
		public static string stringtobase64(string Message) {
			try {


				byte[] bytes = Encoding.UTF8.GetBytes(Message);


				return Convert.ToBase64String(bytes);
			}
			catch (Exception ex) {
				//Fcity.Core.Logs.WriteLog(ex.ToString());
				return "";
			}


		}

		/// <summary>
		/// 解码
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string base64tostring(string Message) {
			try {


				byte[] outputb = Convert.FromBase64String(Message);

				//Fcity.Core.Logs.WriteLog(Encoding.UTF8.GetString(outputb));
				return Encoding.UTF8.GetString(outputb);
			}
			catch (Exception ex) {
				//Fcity.Core.Logs.WriteLog(ex.ToString());
				return "";
			}

		}

	}
}
