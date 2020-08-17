using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PaiXie.Erp {
	public class FileUploader {
		#region Upload

		public static string Upload(HttpPostedFileBase file, string type) {
			string directory = HttpContext.Current.Server.MapPath("\\") + "upload/" + type + "/" + DateTime.Now.ToString("yyMM") + "/";
			string urlbase = @"/upload/" + type + @"/" + DateTime.Now.ToString("yyMM") + @"/";
			string fileSuffix = file.FileName.Substring(file.FileName.LastIndexOf("."));

			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") +
					   new Random(DateTime.Now.Millisecond).Next(99999).ToString("00000");
			var filePath = string.Format("{0}{1}{2}", directory, fileName, fileSuffix);
			string url = urlbase + fileName + fileSuffix;

			if (File.Exists(filePath))
				File.Delete(filePath);
			file.SaveAs(filePath);

			return url;
		}
		
		#endregion
	}
}