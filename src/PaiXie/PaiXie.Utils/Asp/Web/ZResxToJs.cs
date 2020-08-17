using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;
using System.IO;

namespace PaiXie.Utils
{
    public class ZResxToJs
    {
        public static void RenderJsResource(string RsPath,string JsPath)
        {
            //var RsPath = "~//App_GlobalResources//resource1.resx";
            //var JsPath = "~//common//js//abc.js";
            RsPath = HttpContext.Current.Server.MapPath(RsPath);
            JsPath = HttpContext.Current.Server.MapPath(JsPath);
            var script = new StringBuilder();
            using (var resourceReader = new System.Resources.ResXResourceReader(RsPath))
            {
                foreach (DictionaryEntry entry in resourceReader)
                {
                    var key = ZConvert.ToString(entry.Key).Replace('.', '_');
                    var value = ZConvert.ToString(entry.Value);
                    script.Append(",");
                    script.Append(key);
                    script.Append(":");
                    script.Append('"' + value + '"');
                    script.Append("\r\n");
                }
            }

            try
            {
                var str = "var lang = {\r\n " + script.ToString().Trim(',') + "}";
                ZFiles.DeleteFiles(JsPath);
                ZFiles.WriteStrToTxtFile(JsPath, str, FileMode.CreateNew);
            }
            catch
            {

            }
        }
    }
}
