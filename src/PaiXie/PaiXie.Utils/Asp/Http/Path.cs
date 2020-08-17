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
        #region 本地路径转换成URL相对路径
        //本地路径转换成URL相对路径
        public static string ServerPathToUrl(string ServerPath)
        {
            string tmpRootDir = HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string sUrl = ServerPath.Replace(tmpRootDir, ""); //转换成相对路径
            sUrl = "/" + sUrl.Replace(@"\", @"/");
            return sUrl;
        }
        #endregion

        #region 解析地址为客户端地址
        /// <summary>
        /// 把一个URL路径转换为绝对路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ResolveUrl(string url)
        {
            //空地址, 绝对地址, 不以~开头的地址
            if (string.IsNullOrEmpty(url) || url.Contains("://") || !url.StartsWith("~"))
            {
                return url;
            }

            int queryindex = url.IndexOf('?');
            if (queryindex != -1)
            {
                string queryString = url.Substring(queryindex);
                string baseUrl = url.Substring(0, queryindex);

                return string.Concat(VirtualPathUtility.ToAbsolute(baseUrl), queryString);
            }
            else
            {
                return VirtualPathUtility.ToAbsolute(url);
            }
        }
        #endregion

        #region 获取WEB应用程序的根目录
        /// <summary>
        /// 获取WEB应用程序的根目录(包含虚拟目录路径),以/结尾 
        /// </summary>
        public static string RootPath
        {
            get
            {
                string url = HttpContext.Current.Request.ApplicationPath;
                if (url.EndsWith("/"))
                {
                    return url;
                }
                return url + "/";
            }
        }

        /// <summary>
        /// 获取WEB应用程序的根目录全路径(包含虚拟目录路径),以/结尾 
        /// </summary>
        public static string RootFullPath
        {
            get
            {
                if (Port == 80)
                {
                    return string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, Host, RootPath);
                }
                else
                {
                    return string.Format("{0}://{1}:{2}{3}", HttpContext.Current.Request.Url.Scheme, Host, Port, RootPath);
                }
            }
        }
        #endregion

        #region 取得网站根目录的物理路径
        /// <summary>
        /// 取得网站根目录的物理路径
        /// </summary>
        public static string RootPhysicalPath
        {
            get
            {
                return System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
            }
        }
        #endregion

        #region 获取网站的端口
        /// <summary>
        /// 端口
        /// </summary>
        public static int Port
        {
            get
            {
                return HttpContext.Current.Request.Url.Port;
            }
        }
        #endregion
    }
}

