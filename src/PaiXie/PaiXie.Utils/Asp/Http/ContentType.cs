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
        #region 获得ContentType
        private static string GetContentType(string ext)
        {
            string contentType = "application/octet-stream";

            #region 获取Content-Type
            switch (ext)
            {

                case ".asa":
                    {
                        contentType = "text/asa";
                        break;
                    }
                case ".asf":
                    {
                        contentType = "video/x-ms-asf";
                        break;
                    }
                case ".asp":
                    {
                        contentType = "text/asp";
                        break;
                    }
                case ".au":
                    {
                        contentType = "audio/basic";
                        break;
                    }
                case ".avi":
                    {
                        contentType = "video/avi";
                        break;
                    }
                case ".bmp":
                    {
                        contentType = "application/x-bmp";
                        break;
                    }
                case ".css":
                    {
                        contentType = "text/css";
                        break;
                    }
                case ".dll":
                    {
                        contentType = "application/x-msdownload";
                        break;
                    }
                case ".doc":
                    {
                        contentType = "application/msword";
                        break;
                    }
                case ".dot":
                    {
                        contentType = "application/msword";
                        break;
                    }
                case ".exe":
                    {
                        contentType = "application/x-msdownload";
                        break;
                    }
                case ".hta":
                    {
                        contentType = "application/hta";
                        break;
                    }
                case ".htc":
                    {
                        contentType = "text/x-component";
                        break;
                    }
                case ".htm":
                case ".html":
                case ".jsp":
                case ".xhtml":
                    {
                        contentType = "text/html";
                        break;
                    }
                case ".ico":
                    {
                        contentType = "image/x-icon";
                        break;
                    }
                case ".img":
                    {
                        contentType = "application/x-img";
                        break;
                    }
                case ".java":
                    {
                        contentType = "java/*";
                        break;
                    }
                case ".jpeg":
                case ".jpg":
                    {
                        contentType = "image/jpeg";
                        break;
                    }
                case ".js":
                    {
                        contentType = "application/x-javascript";
                        break;
                    }
                case ".mid":
                case ".midi":
                    {
                        contentType = "audio/mid";
                        break;
                    }
                case ".mp3":
                    {
                        contentType = "audio/mp3";
                        break;
                    }
                case ".mp4":
                    {
                        contentType = "video/mpeg4";
                        break;
                    }
                case ".mpg":
                case ".mpeg":
                    {
                        contentType = "video/mpg";
                        break;
                    }
                case ".png":
                    {
                        contentType = "image/png";
                        break;
                    }
                case ".pot":
                case ".ppa":
                case ".pps":
                case ".ppt":
                    {
                        contentType = "application/vnd.ms-powerpoint";
                        break;
                    }
                case ".ra":
                    {
                        contentType = "audio/vnd.rn-realaudio";
                        break;
                    }
                case ".ram":
                    {
                        contentType = "audio/x-pn-realaudio";
                        break;
                    }
                case ".rm":
                    {
                        contentType = "application/vnd.rn-realmedia";
                        break;
                    }
                case ".rmvb":
                    {
                        contentType = "application/vnd.rn-realmedia-vbr";
                        break;
                    }
                case ".torrent":
                    {
                        contentType = "application/x-bittorrent";
                        break;
                    }
                case ".wma":
                    {
                        contentType = "audio/x-ms-wma";
                        break;
                    }
                case ".wml":
                    {
                        contentType = "text/vnd.wap.wml";
                        break;
                    }
                case ".wmv":
                    {
                        contentType = "video/x-ms-wmv";
                        break;
                    }
                case ".xls":
                    {
                        contentType = "application/vnd.ms-excel";
                        break;
                    }
                case ".dtd":
                case ".biz":
                case ".xml":
                case ".xq":
                case ".xql":
                case ".xquery":
                case ".xsd":
                case ".xsl":
                case ".xslt":
                    {
                        contentType = "text/xml";
                        break;
                    }
            }
            #endregion
            return contentType;
        }
        #endregion
    }
}

