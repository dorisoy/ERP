using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace PaiXie.Utils
{
	public partial class ZFiles {
		#region 返回文件是否存在
		/// <summary>
		/// 返回文件是否存在
		/// </summary>
		/// <param name="filename">文件名</param>
		/// <returns>是否存在</returns>
		public static bool FileExists(string filename) {
			return System.IO.File.Exists(filename);
		}
		#endregion

		#region 获取文件最后修改时间
		/// <summary>
		/// 获取文件最后修改时间
		/// </summary>
		/// <param name="FileUrl">文件真实路径</param>
		/// <returns></returns>
		public DateTime GetFileWriteTime(string FileUrl) {
			return File.GetLastWriteTime(FileUrl);
		}
		#endregion

		#region 返回指定文件的扩展名
		/// <summary>
		/// 返回指定路径的文件的扩展名
		/// </summary>
		/// <param name="PathFileName">完整路径的文件</param>
		/// <returns></returns>
		public string GetFileExtension(string PathFileName) {
			return Path.GetExtension(PathFileName);
		}
		#endregion

		#region 判断是否是隐藏文件
		/// <summary>
		/// 判断是否是隐藏文件
		/// </summary>
		/// <param name="path">文件路径</param>
		/// <returns></returns>
		public bool IsHiddenFile(string path) {
			FileAttributes MyAttributes = File.GetAttributes(path);
			string MyFileType = MyAttributes.ToString();
			if (MyFileType.LastIndexOf("Hidden") != -1) //是否隐藏文件
            {
				return true;
			}
			else
				return false;
		}
		#endregion

		#region 获取物理路径
		/// <summary>
		/// 获取物理路径
		/// </summary>
		/// <param name="strPath">相对路径</param>
		/// <returns></returns>
		public static new string MapPath(string strPath) {
			if (HttpContext.Current != null) {
				return HttpContext.Current.Server.MapPath(strPath);
			}
			else {
				//非web程序引用 
				strPath = strPath.TrimStart('~');
				strPath = strPath.Replace("/", "\\");
				strPath = strPath.Replace("\\", "\\");
				if (strPath.StartsWith("\\")) {
					strPath = strPath.TrimStart('\\');
				}
				return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
			}
		}
		#endregion
	}
}
