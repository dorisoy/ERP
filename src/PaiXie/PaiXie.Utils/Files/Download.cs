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
        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="FileFullPath">下载文件下载的完整路径及名称</param>
        public static void DownLoadFile(string FileFullPath)
        {
            if (!string.IsNullOrEmpty(FileFullPath) && FileExists(FileFullPath))
            {
                FileInfo fi = new FileInfo(FileFullPath);//文件信息
				string browser = HttpContext.Current.Request.UserAgent.ToUpper();
				string fileName = browser.Contains("FIREFOX") ? fi.Name : HttpUtility.UrlEncode(fi.Name); //对文件名编码
				fileName = fileName.Replace("+", "%20"); //解决空格被编码为"+"号的问题

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
				HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                HttpContext.Current.Response.AppendHeader("content-length", fi.Length.ToString()); //文件长度
                int chunkSize = 102400;//缓存区大小,可根据服务器性能及网络情况进行修改
                byte[] buffer = new byte[chunkSize]; //缓存区
                using (FileStream fs = fi.Open(FileMode.Open))  //打开一个文件流
                {
                    while (fs.Position >= 0 && HttpContext.Current.Response.IsClientConnected) //如果没到文件尾并且客户在线
                    {
                        int tmp = fs.Read(buffer, 0, chunkSize);//读取一块文件
                        if (tmp <= 0) break; //tmp=0说明文件已经读取完毕,则跳出循环
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, tmp);//向客户端传送一块文件
                        HttpContext.Current.Response.Flush();//保证缓存全部送出
                        Thread.Sleep(10);//主线程休息一下,以释放CPU
                    }
                }
            }
        }
        #endregion

        #region 下载大文件 支持续传、速度限制
        public static bool DownloadFile(HttpContext context, string filePath, long speed)
        {
            string fileName = Path.GetFileName(filePath);
            Stream myFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return DownloadFile(context, myFile, fileName,speed);
        }
        /// <summary>
        /// 下载文件，支持大文件、续传、速度限制。支持续传的响应头Accept-Ranges、ETag，请求头Range 。
        /// Accept-Ranges：响应头，向客户端指明，此进程支持可恢复下载.实现后台智能传输服务（BITS），值为：bytes；
        /// ETag：响应头，用于对客户端的初始（200）响应，以及来自客户端的恢复请求，
        /// 必须为每个文件提供一个唯一的ETag值（可由文件名和文件最后被修改的日期组成），这使客户端软件能够验证它们已经下载的字节块是否仍然是最新的。
        /// Range：续传的起始位置，即已经下载到客户端的字节数，值如：bytes=1474560- 。
        /// 另外：UrlEncode编码后会把文件名中的空格转换中+（+转换为%2b），但是浏览器是不能理解加号为空格的，所以在浏览器下载得到的文件，空格就变成了加号；
        /// 解决办法：UrlEncode 之后, 将 "+" 替换成 "%20"，因为浏览器将%20转换为空格
        /// </summary>
        /// <param name="httpContext">当前请求的HttpContext</param>
        /// <param name="filePath">下载文件的物理路径，含路径、文件名</param>
        /// <param name="speed">下载速度：每秒允许下载的字节数</param>
        /// <returns>true下载成功，false下载失败</returns>
        public static bool DownloadFile(HttpContext context, Stream myFile,string fileName, long speed)
        {
            bool ret = true;
            try
            {
                #region 定义局部变量
                long startBytes = 0;
                int packSize = 1024 * 10; //分块读取，每块10K bytes
               
                //FileStream myFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                long fileLength = myFile.Length;

                int sleep = (int)Math.Ceiling(1000.0 * packSize / speed);//毫秒数：读取下一数据块的时间间隔
                //string lastUpdateTiemStr = File.GetLastWriteTimeUtc(filePath).ToString("r");
                //string eTag = HttpUtility.UrlEncode(fileName, Encoding.UTF8) + lastUpdateTiemStr;//便于恢复下载时提取请求头;
                #endregion

                #region--验证：文件是否太大，是否是续传，且在上次被请求的日期之后是否被修改过--------------
                if (myFile.Length > Int32.MaxValue)
                {//-------文件太大了-------
                    context.Response.StatusCode = 413;//请求实体太大
                    return false;
                }

                //if (context.Request.Headers["If-Range"] != null)//对应响应头ETag：文件名+文件最后修改时间
                //{
                //    //----------上次被请求的日期之后被修改过--------------
                //    if (context.Request.Headers["If-Range"].Replace("\"", "") != eTag)
                //    {//文件修改过
                //        context.Response.StatusCode = 412;//预处理失败
                //        return false;
                //    }
                //}
                #endregion

                try
                {
                    #region -------添加重要响应头、解析请求头、相关验证-------------------
                    context.Response.Clear();
                    context.Response.Buffer = false;
                    context.Response.AddHeader("Content-MD5", GetHashMD5(myFile));//用于验证文件
                    context.Response.AddHeader("Accept-Ranges", "bytes");//重要：续传必须
                    //context.Response.AppendHeader("ETag", "\"" + eTag + "\"");//重要：续传必须
                    //context.Response.AppendHeader("Last-Modified", lastUpdateTiemStr);//把最后修改日期写入响应              
                    context.Response.ContentType = "application/octet-stream";//MIME类型：匹配任意文件类型
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).Replace("+", "%20"));
                    context.Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    context.Response.AddHeader("Connection", "Keep-Alive");
                    context.Response.ContentEncoding = Encoding.UTF8;
                    if (context.Request.Headers["Range"] != null)
                    {
                        //------如果是续传请求，则获取续传的起始位置，即已经下载到客户端的字节数------
                        context.Response.StatusCode = 206;//重要：续传必须，表示局部范围响应。初始下载时默认为200
                        string[] range = context.Request.Headers["Range"].Split(new char[] { '=', '-' });//"bytes=1474560-"
                        startBytes = Convert.ToInt64(range[1]);//已经下载的字节数，即本次下载的开始位置
                        if (startBytes < 0 || startBytes >= fileLength)
                        {
                            //无效的起始位置
                            return false;
                        }
                    }
                    if (startBytes > 0)
                    {
                        //------如果是续传请求，告诉客户端本次的开始字节数，总长度，以便客户端将续传数据追加到startBytes位置后----------
                        context.Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                    }
                    #endregion


                    #region -------向客户端发送数据块-------------------
                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Ceiling((fileLength - startBytes + 0.0) / packSize);//分块下载，剩余部分可分成的块数
                    for (int i = 0; i < maxCount && context.Response.IsClientConnected; i++)
                    {//客户端中断连接，则暂停
                        context.Response.BinaryWrite(br.ReadBytes(packSize));
                        context.Response.Flush();
                        if (sleep > 1) Thread.Sleep(sleep);
                    }
                    #endregion
                }
                catch
                {
                    ret = false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        #endregion

        #region 下载文件，用cookies记录下载状态，源自microsoft Ajax File Download
        //writefile
        public static void DownloadFile(HttpContext context, string fileName)
        {
            HttpResponse response = context.Response;
            HttpCookie cookie = context.Request.Cookies["downloadstatus"];
            if (cookie == null)
            {
                cookie = new HttpCookie("downloadstatus", String.Empty);
            }
            HttpCookie errorCookie = new HttpCookie("downloaderror", string.Empty);
            FileInfo TheFile = new FileInfo(fileName);
            if (TheFile.Exists)
            {
                response.Clear();

                cookie.Value = "success";

                response.SetCookie(cookie);
                response.Buffer = true;
                response.AddHeader("content-disposition", "attachment;filename=" + TheFile.Name);
                response.Charset = "";
                response.ContentType = PaiXie.Utils.ZFiles.GetContentType(TheFile.Name);
                //response.WriteFile(TheFile.FullName);      
                response.TransmitFile(TheFile.FullName); //WriteFile改良版
                response.Flush();
                response.End();
            }
            else
            {
                cookie.Value = "fail";
                errorCookie.Value = "File " + TheFile.Name + " doesn't exists on the server";
                response.SetCookie(cookie);
                response.SetCookie(errorCookie);
            }
        }
        #endregion
    }
}