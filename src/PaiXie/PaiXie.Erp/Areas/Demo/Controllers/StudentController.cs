using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System.IO;
using System.Data;
namespace PaiXie.Erp.Areas.Demo {

	public class StudentController : BaseController {

		public ActionResult JsonTreeTest() {
			
			EasyUITree EUItree = new EasyUITree();
			DataTable dt = TreeService.GetDataTable("SELECT 	ID ,Name  AS  TEXT , 	ParentID, 	state, Url AS attr FROM Tree");
			List<JsonTree> list = EUItree.initTree(dt);
		   return 	JsonDate(list);
		}


		public ActionResult select() {
		
			return View();
		}
		public ActionResult Uploadajax() {
		
			return View();
		}
		public ActionResult Uploadform() {

			return View();
		}
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Uploadform(FormCollection collection) {


			var c = Request.Files[0];
			if (c != null && c.ContentLength > 0) {
				try {
					int lastSlashIndex = c.FileName.LastIndexOf("\\");
					string fileName = c.FileName.Substring(lastSlashIndex + 1, c.FileName.Length - lastSlashIndex - 1);
					string phyPath = Request.MapPath("~/Upload/");
					if (!Directory.Exists(phyPath)) {
						Directory.CreateDirectory(phyPath);
					}
					c.SaveAs(phyPath + fileName);
					ViewBag.filename = fileName;
					ViewBag.Result = "上传成功";
				}
				catch {
					ViewBag.Result = "上传失败";
				}
			}
			else {
				ViewBag.Result = "未选择文件";
			}
			return View();
		}
		public ActionResult treegrid() {
		
			return View();
		}
		public ActionResult FooterDataGrid() {
		
			return View();
		}
		
			public ActionResult Getfootgrid() {
			string str = ZFiles.ReadFile(Request.MapPath("~/Content/menu/datagrid_data2.json"));
			
			return Content(str);
		}
			public ActionResult GetMergegrid() {
				string str = ZFiles.ReadFile(Request.MapPath("~/Content/menu/datagrid_data1.json"));

				return Content(str);
			}

		public ActionResult Gettreegrid() {
			string str = ZFiles.ReadFile(Request.MapPath("~/Content/menu/treegrid_data.json"));
			
			return Content(str);
		}
		public ActionResult EditDataGrid() {

			return View();
		}
		public ActionResult Tree() {
			return View();
		}
		public ActionResult Upload() {
			return View();
		}
		[HttpPost]
		public string Upload(FormCollection fc) {
			string newFileName = string.Empty;
			//判断Request中是否有接收Files文件
			if (Request.Files.Count != 0) {
				//  Thread.Sleep(1000);
				//HttpPostedFileBase类，提供对用户上载的单独文件的访问
				//获取到用户上传的文件
				HttpPostedFileBase file = Request.Files[0];

				//获取用户上传文件的后缀名
				string Extension = Path.GetExtension(file.FileName);

				//重新命名文件
				newFileName = Guid.NewGuid().ToString() + Extension;

				//利用file.SaveAs保存图片
				string name = Path.Combine(Server.MapPath("/Upload/"), newFileName);
				file.SaveAs(name);
			}
			//   Thread.Sleep(1000);
			return "/Upload/" + newFileName;
		}

	//	[AllowAnonymous]
		//[Authorize]
		[HttpPost]
		public ActionResult save(student obj) {

			if (obj.ID == 0) {
				studentService.Add(obj);
			}
			else {
				string strsql = string.Format("SELECT 	* FROM student WHERE id={0}", obj.ID);
				student objstudent = studentService.GetQuerySingle(strsql);
				objstudent.SstuNmae = obj.SstuNmae;
				studentService.Update(objstudent);
			}
			return Content("OK");
		}

		public ActionResult search() {
			//   Json格式的要求{total:22,rows:{}}
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql="";
			string SstuNmae = Request["SstuNmae"];
			if (!string.IsNullOrEmpty(SstuNmae))
				whereSql += string.Format("SstuNmae LIKE '%{0}%'", SstuNmae);
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = "student a LEFT JOIN classs b ON a.ClassId=b.ID ";
			data.Select = "a.*,b.ClassName";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<studentList> list = studentService.GetQueryManyForPageList(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}


		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection) {
			try {
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch {
				return View();
			}
		}

		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection) {
			try {
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch {
				return View();
			}
		}

		//
		// GET: /Demo/Receive/

		public ActionResult Index() {
			return View();
		}

		public ActionResult PopShow() {
			return View();
		}

		public ActionResult PopShowContent(string id="") {
			ViewBag.Param = id;
			return View();
		}
		//
		// GET: /Demo/Receive/Details/5

		public ActionResult Details(int id) {
			return View();
		}

		//
		// GET: /Demo/Receive/Create

		public ActionResult Create() {
			return View();
		}

		//
		// POST: /Demo/Receive/Create

		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			try {
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			}
			catch {
				return View();
			}
		}




		public ActionResult edit(string id) {
			student objstudent = new student();
			string strsql = string.Format("SELECT 	* FROM student WHERE id={0}", id);
			if (id != "0") objstudent = studentService.GetQuerySingle(strsql);
			ViewBag.student = objstudent;
			return View();
		}



		public ActionResult delete(string id) {
			string strsql = string.Format("delete  from student where ID={0}", id);
			testtableService.Del(strsql);
			return Content("OK");
		}



	}
}
