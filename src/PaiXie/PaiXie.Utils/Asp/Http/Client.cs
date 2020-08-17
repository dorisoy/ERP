using System;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.IO;
using System.IO.Compression;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Generic;
using System.Net;

namespace PaiXie.Utils
{
    /// <summary>
    /// WEB请求上下文信息工具类
    /// </summary>
    public partial class ZHttp
    {
        #region 可以获取客户端上次请求的url的有关信息
        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string ReferrerUrl
        {
            get
            {
                return HttpContext.Current.Request.UrlReferrer == null ? string.Empty : HttpContext.Current.Request.UrlReferrer.OriginalString;
            }
        }
        #endregion

        #region 得到用户IP地址
        /// <summary>
        /// 得到用户IP地址
        /// </summary>
        /// <returns>返回用户IP地址,如果获取不到返回 0.0.0.0 </returns>
        /// 
        public static string ClientIP
        {
            get
            {
                var context = HttpContext.Current;
                string result = context.Request.UserHostAddress;
                if (string.IsNullOrEmpty(result))
                {
                    result = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];//获取包括使用了代理服务器的地址列表。
                }
                if (string.IsNullOrEmpty(result))
                {
                    result = context.Request.ServerVariables["REMOTE_ADDR"];//最后一个代理服务器地址。
                }
                if (string.IsNullOrEmpty(result))
                {
                    result = context.Request.UserHostAddress;
                }
                return result;
            }
        }
        #endregion

        public static bool IsLanIP(string IP)
        {
            var result = false;
            var ips = new List<string>() { "10.", "192.", "172.", "127.", "::1", "localhost" };
            ips.ForEach(x=>{
                if (IP.StartsWith(x)) result = true;
            });
            return result;
        }

        public static string ClientHostName 
        {
            get 
            {
                var context = HttpContext.Current;
                string IP = context.Request.UserHostAddress;
                string Name = context.Request.UserHostName;

                if (Name == IP || Name == "127.0.0.1" || Name == "::1")
                {
                    Name = context.Request.ServerVariables["REMOTE_HOST"];
                }
                if (Name == IP || Name == "127.0.0.1" || Name == "::1")
                {
                    try { Name = Dns.GetHostEntry(IP).HostName; }
                    catch { }
                }

                return Name;
            }
        }

        #region 判断是否来自搜索引擎链接
        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsFromSearchEngines
        {
            get
            {
                string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
                if (HttpContext.Current.Request.UrlReferrer != null)
                {
                    string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
                    for (int i = 0; i < SearchEngine.Length; i++)
                    {
                        if (tmpReferrer.Contains(SearchEngine[i]))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        #endregion

        #region 取得客户端的操作系统
        public static string GetPlatformName(HttpRequestBase request)
        {
            string userAgent = request.UserAgent;

            if (string.IsNullOrEmpty(userAgent))
                return "未知类型";

            else if (userAgent.IndexOf("Windows NT 6.2") != -1)
                return "Windows 8";

            else if (userAgent.IndexOf("Windows NT 6.1") != -1)
                return "Windows 7";

            else if (userAgent.IndexOf("Windows NT 6") != -1)
                return "Windows Vista";

            else if (userAgent.IndexOf("Windows NT 5.1") != -1)
                return "Windows XP";

            else if (userAgent.IndexOf("Windows NT 5.2") != -1)
                return "Windows Server 2003";

            else if (userAgent.IndexOf("Windows NT 5") != -1)
                return "Windows 2000";

            else if (userAgent.IndexOf("iPhone") != -1)
                return "iPhone";

            else if (userAgent.IndexOf("(iPad;") != -1)
                return "iPad";

            else if (userAgent.IndexOf("Android") != -1)
                return "Android";

            else if (userAgent.IndexOf("9x") != -1)
                return "Windows ME";

            else if (userAgent.IndexOf("98") != -1)
                return "Windows 98";

            else if (userAgent.IndexOf("95") != -1)
                return "Windows 95";

            else if (userAgent.IndexOf("NT 4") != -1)
                return "Windows NT 4";

            if (request.Browser != null && !string.IsNullOrEmpty(request.Browser.Platform))
                return request.Browser.Platform.Replace("WinCE", "Windows CE");
            else
                return "未知类型";
        }
        #endregion
    }
}

