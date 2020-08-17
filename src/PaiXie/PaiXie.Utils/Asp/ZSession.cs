﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace PaiXie.Utils
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——Session操作类
    /// <para>　----------------------------------------------------------</para>
    /// <para>　AddSession：添加Session,有效期为默认</para>
    /// <para>　AddSession：添加Session，并调整有效期为分钟或几年</para>
    /// <para>　GetSession：读取某个Session对象值</para>
    /// <para>　DelSession：删除某个Session对象</para>
    /// </summary>
    public class ZSession
    {

        #region 添加Session,有效期为默认
        /// <summary>
        /// 添加Session,有效期为默认
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValue">Session值</param>
        public static void Add(string strSessionName, object objValue)
        {
            HttpContext.Current.Session[strSessionName] = objValue;
        }
        #endregion

        #region 添加Session，并调整有效期为分钟或几年
        /// <summary>
        /// 添加Session，并调整有效期为分钟或几年
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValue">Session值</param>
        /// <param name="iExpires">分钟数：大于０则以分钟数为有效期，等于０则以后面的年为有效期</param>
        /// <param name="iYear">年数：当分钟数为０时按年数为有效期，当分钟数大于０时此参数随意设置</param>
        public static void Set(string strSessionName, object objValue, int iExpires, int iYear)
        {
            HttpContext.Current.Session[strSessionName] = objValue;
            if (iExpires > 0)
            {
                HttpContext.Current.Session.Timeout = iExpires;
            }
            else if(iYear>0)
            {
                HttpContext.Current.Session.Timeout = 60 * 24 * 365 * iYear;
            }
        }
        #endregion

        #region 读取某个Session对象值
        /// <summary>
        /// 读取某个Session对象值
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static object Get(string strSessionName)
        {
            return HttpContext.Current.Session[strSessionName];
        }
        #endregion

        #region 删除某个Session对象
        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        public static void Remove(string strSessionName)
        {
            HttpContext.Current.Session.Remove(strSessionName);
        }
        #endregion
    }
}
