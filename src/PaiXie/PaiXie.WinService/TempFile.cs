using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;

namespace PaiXie.WinService {
	public class TempFile {

		//间隔5分钟
		public int autoDeleteIntervalMinutes = 1000 * 60 * ZConvert.StrToInt(ConfigurationManager.AppSettings["AutoDeleteIntervalMinutes"]);
		//临时文件目录
		public string tempFileDirs = ZConvert.ToString(ConfigurationManager.AppSettings["TempFileDirs"]);
		#region  删除临时文件

		public void AutoDeleteTempFile() {
			while (true) {
				if (DateTime.Now.Hour >= 2 && DateTime.Now.Hour <= 3) {
					string[] arrTempFileDir = tempFileDirs.Split('|');
					foreach (var tempFileDir in arrTempFileDir) {
						try {
							DirectoryInfo dirinfo = new DirectoryInfo(tempFileDir);
							FileInfo[] Files = dirinfo.GetFiles();
							foreach (var file in Files) {
								if (file.CreationTime < DateTime.Now.AddDays(-1)) {
									ZFiles.DeleteFiles(file.FullName);
								}
							}
						}
						catch {}
					}
					Thread.Sleep(1000 * 3600 * 23);
				}
				else {
					Thread.Sleep(autoDeleteIntervalMinutes);
				}
			}
		}

		#endregion
	}
}
