using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace PaiXie.WinService {
	/// <summary>
	/// 辅助类
	/// </summary>
	public class common {
		#region 创建文件夹
		public static string crefod(string typestr) {
			string sPath = Application.StartupPath + "\\" + typestr + "\\" + System.DateTime.Now.ToString("yyyy-MM-dd");
			if (!Directory.Exists(sPath)) {
				Directory.CreateDirectory(sPath);
			}
			return sPath;
		}
		#endregion

		#region 日志
		/// <summary>
		/// 文件日志
		/// </summary>
		/// <param name="ex"></param>
		public static void WriteLog(string ex, string typestr) {
			try {
				string phyPath = common.crefod(typestr);
				string t = System.DateTime.Now.ToString("yyyyMMddHH");
				string sPath = phyPath + "\\" + t;
				string filename = "error";
				if (!Directory.Exists(sPath)) {
					Directory.CreateDirectory(sPath);
				}
				using (StreamWriter SW = File.AppendText(sPath + "\\" + filename + ".txt")) {
					SW.WriteLine(System.DateTime.Now.ToString());
					SW.WriteLine(ex);
					SW.WriteLine("------------------------------------------------------------------");
					SW.Close();
				}
			}
			catch { }

		}
		#endregion

	}
}