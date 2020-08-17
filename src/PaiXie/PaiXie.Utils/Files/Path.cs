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
    public partial class ZFiles
    {
        #region 获取应用程序当前可执行文件的路径
        /// <summary>
        /// 获取应用程序当前可执行文件的路径
        /// </summary>
        /// <returns></returns>
        public static string GetAppCurrentDirectory()
        {
            return Application.StartupPath;
        }
        #endregion
    }
}
