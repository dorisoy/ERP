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
using System.Security.Cryptography;

namespace PaiXie.Utils
{
    public partial class ZEncypt
    {
        #region MD5����
        /// <summary>
        /// MD5����,�����ã�using System.Security.Cryptography;
        /// </summary>
        /// <param name="str">ԭʼ�ַ���</param>
        /// <returns>MD5���</returns>
        public static string MD5(string str)
        {
            //΢��md5�����ο�return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
        #endregion
    }
}
