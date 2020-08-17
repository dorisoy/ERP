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
        private const string PATH_SPLIT_CHAR = "\\";

        #region 工具方法：ASP.NET上传文件的方法
        /// <summary>
        /// 工具方法：上传文件的方法
        /// </summary>
        /// <param name="myFileUpload">上传控件的ID</param>
        /// <param name="allowExtensions">允许上传的扩展文件名类型,如：string[] allowExtensions = { ".doc", ".xls", ".ppt", ".jpg", ".gif" };</param>
        /// <param name="maxLength">允许上传的最大大小，以M为单位</param>
        /// <param name="savePath">保存文件的目录，注意是绝对路径,如：Server.MapPath("~/upload/");</param>
        /// <param name="saveName">保存的文件名，如果是""则以原文件名保存</param>
        public static string FilesUpload(FileUpload myFileUpload, string[] allowExtensions, int maxLength, string savePath, string saveName)
        {
            // 文件格式是否允许上传
            bool fileAllow = false;
            //检查是否有文件案
            if (myFileUpload.HasFile)
            {
                // 检查文件大小, ContentLength获取的是字节，转成M的时候要除以2次1024
                if (myFileUpload.PostedFile.ContentLength / 1024 / 1024 >= maxLength)
                {
                    return String.Format("只能上传小于{0}M的文件！",maxLength);
                }
                //取得上传文件之扩展文件名，并转换成小写字母
                string fileExtension = System.IO.Path.GetExtension(myFileUpload.FileName).ToLower();
                string tmp = "";   // 存储允许上传的文件后缀名
                //检查扩展文件名是否符合限定类型
                for (int i = 0; i < allowExtensions.Length; i++)
                {
                    tmp += i == allowExtensions.Length - 1 ? allowExtensions[i] : allowExtensions[i] + ",";
                    if (fileExtension == allowExtensions[i])
                    {
                        fileAllow = true;
                    }
                }
                if (allowExtensions.Length == 0) { fileAllow = true; }
                if (fileAllow)
                {
                    try
                    {
                        DirectoryInfo di = new DirectoryInfo(Path.GetFullPath(savePath));
                        if (!di.Exists)
                        {
                            di.Create();
                        }

                        string path = savePath + (saveName == "" ? myFileUpload.FileName : saveName);
                        //存储文件到文件夹
                        myFileUpload.SaveAs(path);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    return "文件格式不符，可以上传的文件格式为：" + tmp;
                }
            }
            else
            {
                return "请选择要上传的文件！";
            }
            return "";
        }
        #endregion
    }
}
