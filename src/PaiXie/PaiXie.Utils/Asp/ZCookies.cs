using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace PaiXie.Utils
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——COOKIES操作类
    /// <para>　-------------------------------------------------------------------</para>
    /// <para>　WriteCookie：创建COOKIE对象并赋Value值或值集合 [+4重载]</para>
    /// <para>　GetCookie：读取Cookie某个对象的Value值，返回Value值，如果对象本就不存在，则返回null</para>
    /// <para>　DelCookie：删除COOKIE对象</para>
    /// </summary>
    public class ZCookies
    {

        #region 创建COOKIE对象并赋Value值
        /// <summary>
        /// 创建COOKIE对象并赋Value值
        /// </summary>
        /// <param name="CookiesName">COOKIE对象名</param>
        /// <param name="IExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>  
        /// <param name="CookiesValue">COOKIE对象Value值</param>
        public static void WriteCookies(string CookiesName, int IExpires, string CookiesValue)
        {
            HttpCookie objCookie = new HttpCookie(CookiesName.Trim());
            objCookie.Value = ZEncypt.DESEncrypt(CookiesValue.Trim());    //加密存储
            if (IExpires > 0)
            {
                if (IExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddMinutes(IExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// 创建COOKIE对象并赋Value值
        /// </summary>
        /// <param name="CookiesName">COOKIE对象名</param>
        /// <param name="IExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>  
        /// <param name="CookiesValue">COOKIE对象Value值</param>
        /// <param name="CookiesDomain">作用域</param>
        public static void WriteCookies(string CookiesName, int IExpires, string CookiesValue, string CookiesDomain)
        {
            HttpCookie objCookie = new HttpCookie(CookiesName.Trim());
            objCookie.Value = ZEncypt.DESEncrypt(CookiesValue.Trim());    //加密存储
            objCookie.Domain = CookiesDomain.Trim();
            if (IExpires > 0)
            {
                if (IExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddMinutes(IExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>   
        /// 创建COOKIE对象并赋多个KEY键值   
        /// 设键/值如下：   
        /// NameValueCollection myCol = new NameValueCollection();   
        /// myCol.Add("red", "rojo");   
        /// myCol.Add("green", "verde");   
        /// myCol.Add("blue", "azul");   
        /// myCol.Add("red", "rouge");   结果“red:rojo,rouge；green:verde；blue:azul”   
        /// </summary>   
        /// <param name="CookiesName">COOKIE对象名</param>   
        /// <param name="IExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>   
        /// <param name="CookiesKeyValueCollection">键/值对集合</param> 
        public static void WriteCookies(string CookiesName, int IExpires, NameValueCollection CookiesKeyValueCollection)
        {
            HttpCookie objCookie = new HttpCookie(CookiesName.Trim());
            foreach (String key in CookiesKeyValueCollection.AllKeys)
            {
                objCookie[key] = ZEncypt.DESEncrypt(CookiesKeyValueCollection[key].Trim());
            }
            if (IExpires > 0)
            {
                if (IExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds(IExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>   
        /// 创建COOKIE对象并赋多个KEY键值   
        /// 设键/值如下：   
        /// NameValueCollection myCol = new NameValueCollection();   
        /// myCol.Add("red", "rojo");   
        /// myCol.Add("green", "verde");   
        /// myCol.Add("blue", "azul");   
        /// myCol.Add("red", "rouge");   结果“red:rojo,rouge；green:verde；blue:azul”   
        /// </summary>   
        /// <param name="CookiesName">COOKIE对象名</param>   
        /// <param name="IExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>   
        /// <param name="CookiesKeyValueCollection">键/值对集合</param> 
        /// <param name="CookiesDomain">作用域</param>
        public static void WriteCookies(string CookiesName, int IExpires, NameValueCollection CookiesKeyValueCollection, string CookiesDomain)
        {
            HttpCookie objCookie = new HttpCookie(CookiesName.Trim());
            foreach (String key in CookiesKeyValueCollection.AllKeys)
            {
                objCookie[key] = ZEncypt.DESEncrypt(CookiesKeyValueCollection[key].Trim());
            }
            objCookie.Domain = CookiesDomain.Trim();
            if (IExpires > 0)
            {
                if (IExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds(IExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        #endregion

        #region 读取Cookie某个对象的Value值，返回Value值，如果对象本就不存在，则返回null
        /// <summary>
        /// 读取Cookie某个对象的Value值，返回Value值，如果对象本就不存在，则返回null
        /// </summary>
        /// <param name="CookiesName">Cookie对象名称</param>
        /// <returns>返回对象的Value值，返回Value值，如果对象本就不存在，则返回null</returns>
        public static string GetCookies(string CookiesName)
        {
            if (HttpContext.Current.Request.Cookies[CookiesName] == null)
            {
                return null;
            }
            else
            {
				string result="";
				try {
					result=ZEncypt.DESDecrypt(HttpContext.Current.Request.Cookies[CookiesName].Value);
				}
				catch {
					//Thread.Sleep(2000);
					result=ZEncypt.DESDecrypt(HttpContext.Current.Request.Cookies[CookiesName].Value);
			
				}
				return result;
            }
        }

        /// <summary>
        /// 读取Cookie某个对象的Value值，返回Value值，如果对象本就不存在，则返回null
        /// </summary>
        /// <param name="CookiesName">Cookie对象名称</param>
        /// <param name="KeyName">键值</param>
        /// <returns>返回对象的Value值，返回Value值，如果对象本就不存在，则返回null</returns>
        public static string GetCookies(string CookiesName, string KeyName)
        {
            if (HttpContext.Current.Request.Cookies[CookiesName] == null)
            {
                return null;
            }
            else
            {
                string strObjValue = ZEncypt.DESDecrypt(HttpContext.Current.Request.Cookies[CookiesName].Value);
                string strKeyName2 = KeyName + "=";
                if (strObjValue.IndexOf(strKeyName2) == -1)
                {
                    return null;
                }
                else
                {
                    return ZEncypt.DESDecrypt(HttpContext.Current.Request.Cookies[CookiesName][KeyName]);
                }
            }
        }
        #endregion

        #region 删除COOKIE对象
        /// <summary>
        /// 删除COOKIE对象
        /// </summary>
        /// <param name="CookiesName">Cookie对象名称</param>
        public static void DelCookie(string CookiesName)
        {
            HttpCookie objCookie = new HttpCookie(CookiesName.Trim());
            objCookie.Expires = DateTime.Now.AddYears(-5);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        #endregion
    }
}
