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
        #region 返回指定的服务器变量信息
        /// <summary>
        /// 返回指定的服务器变量信息,如果当前请求或要查找的变量名不存在,返回 null.
        /// </summary>
        /// <param name="name">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServerValue(string name)
        {
            return HttpContext.Current.Request.ServerVariables[name];
        }
        #endregion

        #region 得到当前完整主机头
        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string Host
        {
            get
            {
                return HttpContext.Current.Request.Url.Host;
            }
        }
        #endregion

        #region 获取服务器IP
        /// <summary>
        /// 获取服务器IP
        /// </summary>
        public static string LocalIP
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"].ToString();
            }
        }
        #endregion

        #region 获取服务器操作系统
        /// <summary>
        /// 获取服务器操作系统
        /// </summary>
        public static string OS
        {
            get
            {
                return Environment.OSVersion.Platform.ToString();
            }
        }
        #endregion

        #region 获取服务器域名
        /// <summary>
        /// 获取服务器域名
        /// </summary>
        public static string ServerDomain
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            }
        }
        #endregion
    }
}

