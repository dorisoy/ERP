using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PaiXie.Utils
{
  public  class PlanLog
    {
        #region 日志
	  /// <summary>
	  /// 文件日志
	  /// </summary>
	  /// <param name="ex"></param>
        public static void WriteLog(string ex,string typestr)
        {
            try
            {
				string LogPath = ZConfig.GetConfigString("LogPath");
				if (string.IsNullOrEmpty(LogPath))	LogPath = @"C:\";
              //  string phyPath = HttpContext.Current.Server.MapPath("\\") +typestr +"/";
				string phyPath = LogPath + typestr + @"\";
				if (!Directory.Exists(phyPath))
                    Directory.CreateDirectory(phyPath);
                string t = System.DateTime.Now.ToString("yyyyMMddHH");
                string sPath = phyPath + "\\" + t;
                string filename = "error";
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
                using (StreamWriter SW = File.AppendText(sPath + "\\" + filename + ".txt"))
                {
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
