#region using
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Utils;
using System.Data;
using PaiXie.Core;
#endregion
namespace PaiXie.Erp.Controllers {
	public class ExportController : BaseController {
		#region Index
		//
		// GET: /Export/

		public ActionResult Index() {
			return View();
		}
		#endregion



		#region 导出文件
		[AllowAnonymous]
		/// <summary>
		/// 导出
		/// </summary>
		/// <returns></returns>
		/// 
		public FileResult ExportFile() {
			//创建Excel文件的对象
			NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
			//添加一个sheet
			NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
			//给sheet1添加第一行的头部标题
			NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
			row1.CreateCell(0).SetCellValue("电脑号");
			row1.CreateCell(1).SetCellValue("姓名");
			//将数据逐步写入sheet1各个行
			for (int i = 0; i < 2; i++) {
				NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
				rowtemp.CreateCell(0).SetCellValue("001");
				rowtemp.CreateCell(1).SetCellValue("测试");
			}
			// 写入到客户端 
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			book.Write(ms);
			ms.Seek(0, SeekOrigin.Begin);
			return File(ms, "application/vnd.ms-excel", "test导出.xls");
		}

		#endregion

		#region 上传 图片

		private string[] _allowedPictureFiles2 = new string[] { "png", "jpg" };
		public ActionResult CatalogPicture() {
			BaseResult resultInfo = new BaseResult();
			bool result = false;
			string fileUrl = string.Empty;
			if (Request.Files != null && Request.Files.Count > 0) {
				var file = Request.Files[0];
				string fileSuffix = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace(".", "");
				if (file.ContentLength <= 1024 * 1024 * 10) {
					if (_allowedPictureFiles2.Contains(fileSuffix.ToLower())) {
						fileUrl = FileUploader.Upload(file, "Catalog");
					}
				}


				resultInfo.message = fileUrl;
			}


			return JsonDate(resultInfo);
		}

		#endregion

		#region 导出报表

		/// <summary>
		/// 检查文件是否存在
		/// </summary>
		public string CheckFileExist() {
			int result = 0;
			string fileMapPath = Request["fileMapPath"];
			string downTaskId = Request["downTaskId"];
			if (!string.IsNullOrEmpty(fileMapPath) && !string.IsNullOrEmpty(downTaskId)) {
				bool isfile = false;
				if (System.IO.File.Exists(fileMapPath)) {
					isfile = true;
				}
				else if (System.IO.File.Exists(fileMapPath + ".csv")) {
					isfile = true;
				}
				else if (System.IO.File.Exists(fileMapPath + ".xls")) {
					isfile = true;
				}
				if (isfile) {
					Export.clear_Down_Task_Progress(downTaskId);
					result = -1;
				}
				else {
					result = Export.get_Down_Task_Progress(downTaskId);
				}
			}

			return result.ToString();
		}

		/// <summary>
		/// 下载导出文件
		/// </summary>
		public void Download() {
			string fileName = ZConvert.ToString(Request["fileName"]).Trim();
			if (fileName == "") return;
			string filePath = Server.MapPath("~/Down/" + fileName);
			if (!System.IO.File.Exists(filePath)) {
				int lastIndex = filePath.LastIndexOf("\\");
				var dataFileName = filePath.Substring(lastIndex + 1, filePath.Length - lastIndex - 1);

				string[] array = dataFileName.Trim().Split('.');
				if (array.Length < 2) {
					if (System.IO.File.Exists(filePath + ".csv")) {
						filePath += ".csv";
					}
					else if (System.IO.File.Exists(filePath + ".xls")) {
						filePath += ".xls";
					}
				}
			}

			FileDownload(filePath, Path.GetFileName(filePath));
		}


		/// <summary>
		/// 下载文件
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <param name="fileName">文件名称</param>
		/// <returns></returns>
		public static bool FileDownload(string filePath, string fileName) {
			bool re = true;

			for (int i = 0; i < 24; i++) {
				re = FileSystemObject.FileExists(filePath);

				if (re) {
					break;
				}
				else {
					System.Threading.Thread.Sleep(1 * 1000);
				}
			}

			if (!re) {
				return re;
			}

			FileInfo info = new FileInfo(filePath);
			try {
				ZFiles.DownLoadFile(filePath);
			}
			catch {
				return false;
			}

			return true;
		}

		#endregion
	}
}