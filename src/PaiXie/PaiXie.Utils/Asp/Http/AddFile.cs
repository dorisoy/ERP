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
        #region 向客户端发送添加js文件
        /// <summary>
        /// 要添加的js文件的路径
        /// </summary>
        /// <param name="page">要添加js文件的页面</param>
        /// <param name="src">要添加的js文件的路径</param>
        public static void AddJsFile(System.Web.UI.Page page, string src)
        {
            HtmlGenericControl hc = new HtmlGenericControl("script");
            hc.Attributes.Add("type", "text/javascript");
            if (src.StartsWith("~"))
            {
                hc.Attributes.Add("src", page.ResolveClientUrl(src));
            }
            else
            {
                hc.Attributes.Add("src", src);
            }
            page.Header.Controls.Add(hc);
        }

        public static void RegisterJsToPage(System.Web.UI.Page page, string src)
        {
            if (!page.ClientScript.IsClientScriptBlockRegistered(src))
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), src, String.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", src));
        }

        public static void RegisterJsToPage(System.Web.UI.Page page, string key, string script)
        {
            if (!page.ClientScript.IsClientScriptBlockRegistered(key))
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), key, script);
        }

        /// <summary>
        /// 要添加的js文件的路径
        /// </summary>
        /// <param name="page">要添加js文件的页面</param>
        /// <param name="src">要添加的js文件的路径</param>
        /// <param name="index">要添加的js文件在网页中的位置</param>
        public static void AddJsFile(System.Web.UI.Page page, string src, int index)
        {
            HtmlGenericControl hc = new HtmlGenericControl("script");
            hc.Attributes.Add("type", "text/javascript");
            if (src.StartsWith("~"))
            {
                hc.Attributes.Add("src", page.ResolveClientUrl(src));
            }
            else
            {
                hc.Attributes.Add("src", src);
            }
            page.Header.Controls.AddAt(index, hc);
        }
        #endregion

        #region 向客户端添加CSS文件
        /// <summary>
        /// 向客户端添加CSS文件
        /// </summary>
        /// <param name="page">要添加CSS文件的页面</param>
        /// <param name="href">要添加的CSS文件的路径</param>
        public static void AddCssFile(System.Web.UI.Page page, string href)
        {
            HtmlGenericControl hc = new HtmlGenericControl("link");
            hc.Attributes.Add("rel", "stylesheet");
            hc.Attributes.Add("type", "text/css");
            if (href.StartsWith("~"))
            {
                hc.Attributes.Add("href", page.ResolveClientUrl(href));
            }
            else
            {
                hc.Attributes.Add("href", href);
            }
            page.Header.Controls.Add(hc);
        }
        /// <summary>
        /// 向客户端添加CSS文件
        /// </summary>
        /// <param name="page">要添加CSS文件的页面</param>
        /// <param name="href">要添加的CSS文件的路径</param>
        /// <param name="index">要添加的CSS文件在网页中的位置</param>
        public static void AddCssFile(System.Web.UI.Page page, string href, int index)
        {
            HtmlGenericControl hc = new HtmlGenericControl("link");
            hc.Attributes.Add("rel", "stylesheet");
            hc.Attributes.Add("type", "text/css");
            if (href.StartsWith("~"))
            {
                hc.Attributes.Add("href", page.ResolveClientUrl(href));
            }
            else
            {
                hc.Attributes.Add("href", href);
            }
            page.Header.Controls.AddAt(index, hc);
        }

        #endregion

        #region 向客户端发送文件
        /// <summary>
        /// 向客户端发送文件(在.ashx文件中使用)
        /// </summary>
        /// <param name="context">上下文信息</param>
        /// <param name="fi">文件在服务器上信息</param>
        public static void ResponseFile(HttpContext context, System.IO.FileInfo fi)
        {
            ResponseFile(context, fi.Name, fi);
        }

        /// <summary>
        /// 向客户端发送文件(在.ashx文件中使用)
        /// </summary>
        /// <param name="context">上下文信息</param>
        /// <param name="fileName">文件在客户端保存的名称</param>
        /// <param name="fi">文件在服务器上信息</param>
        public static void ResponseFile(HttpContext context, string fileName, System.IO.FileInfo fi)
        {
            context.Response.Clear();
            context.Response.Buffer = false;
            context.Response.ContentType = GetContentType(fi.Extension);
            if (fi != null && fi.Exists)
            {
                if (context.Request.Browser.Browser.ToLower().Contains("ie")) //IE使用编码
                {
                    context.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(context.Response.ContentEncoding.GetBytes(fileName)));
                }
                else //其它浏览器不使用编码
                {
                    context.Response.AppendHeader("Content-Disposition", string.Format("attachment;filename=\"{0}\"", fileName));
                }
                context.Response.AddHeader("Connection", "Keep-Alive");
                context.Response.AddHeader("Accept-Ranges", "bytes");
                context.Response.Buffer = false;


                context.Response.Cache.SetLastModified(fi.LastWriteTime);
                context.Response.Cache.SetETag(fi.LastWriteTime.Ticks.ToString());


                //ETags和If-None-Match是一种常用的判断资源是否改变的方法。
                //类似于Last-Modified和HTTP-IF-MODIFIED-SINCE。
                //所不同的是Last-Modified和HTTP-IF-MODIFIED-SINCE只判断资源的最后修改时间，而ETags和If-None-Match可以是资源任何的任何属性，如资源的MD5等。

                if (context.Request.Headers["If-None-Match"] != null)
                {
                    if (fi.LastWriteTime.Ticks.ToString() == context.Request.Headers["If-None-Match"])
                    {
                        context.Response.StatusCode = 304;
                        context.Response.StatusDescription = "Not Modified";
                        context.Response.End();
                        return;
                    }
                }

                //Last-Modified 与If-Modified-Since 都是用于记录页面最后修改时间的 HTTP 头信息，
                //Last-Modified 是由服务器往客户端发送的 HTTP 头，而 If-Modified-Since 则是由客户端往服务器发送的头
                if (context.Request.Headers["If-Modified-Since"] != null)
                {
                    DateTime fromhttptime;
                    DateTime.TryParse(context.Request.Headers["If-Modified-Since"], out fromhttptime);
                    if (fi.LastWriteTime.ToString() == fromhttptime.ToString())
                    {
                        context.Response.StatusCode = 304;
                        context.Response.StatusDescription = "Not Modified";
                        return;
                    }
                }

                using (FileStream fs = fi.OpenRead())
                {
                    BinaryReader br = new BinaryReader(fs);
                    try
                    {
                        int bufferlength = 5120;
                        int currentlength = 0;
                        byte[] buffer = new byte[bufferlength];
                        context.Response.AddHeader("Content-Length", fs.Length.ToString());
                        if (context.Response.IsClientConnected)
                        {
                            while (currentlength + bufferlength < fs.Length)
                            {
                                currentlength += br.Read(buffer, 0, buffer.Length);
                                context.Response.BinaryWrite(buffer);
                            }
                            if (fs.Length - currentlength > 0)
                            {
                                buffer = new byte[fs.Length - currentlength];
                                br.Read(buffer, 0, buffer.Length);
                                context.Response.BinaryWrite(buffer);
                            }
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        context.Response.OutputStream.Close();
                        br.Close();
                        fs.Close();
                    }
                    context.Response.Close();
                }
            }
            else
            {
                context.Response.StatusCode = 404;
            }
        }

        #endregion
    }
}

