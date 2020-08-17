using System;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.IO;
using System.IO.Compression;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Generic;

namespace PaiXie.Utils
{
    /// <summary>
    /// WEB请求上下文信息工具类
    /// </summary>
    public partial class ZHttp
    {
        #region 获取当前站点的Application实例
        /// <summary>
        /// 获取当前站点的Application实例
        /// </summary>
        public static System.Web.HttpApplicationState Application
        {
            get
            {
                return HttpContext.Current.Application;
            }
        }
        #endregion

        #region 取得页面返回的信息
        public static string Get_url_data(string url)
        {
            url = RootFullPath + url.Trim('/');
            string Url_Data = "";
            try
            {
                System.Net.WebRequest Request = System.Net.WebRequest.Create(url);
                System.Net.WebResponse Response = Request.GetResponse();
                System.IO.Stream resStream = Response.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(resStream, System.Text.Encoding.Default);
                Url_Data = sr.ReadToEnd();
                resStream.Close();
                sr.Close();
            }
            catch { }
            return Url_Data.Trim();
        }
        #endregion
    }
}

