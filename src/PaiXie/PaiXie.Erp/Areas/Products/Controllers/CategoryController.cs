using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Products
{
    public class CategoryController : BaseController
    {
		#region Index

		public ActionResult Index() {
			return View();
		}

		#endregion

		#region 分类列表

		/// <summary>
		/// 分类列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			EasyUITree EUItree = new EasyUITree();
			DataTable dt = CategoryService.GetDataTable("SELECT ID, Name AS TEXT, ParentID, 'open' AS  state , '0' AS attr FROM category ORDER BY Seq ASC,ID DESC");
			List<JsonTree> list = EUItree.initTree(dt);
			return JsonDate(list);
		}

		#endregion

		#region 下拉列表

		/// <summary>
		/// 下拉列表
		/// </summary>
		/// <returns></returns>
		public ActionResult JsonTree() {
			EasyUITree EUItree = new EasyUITree();
			DataTable dt = CategoryService.GetDataTable("SELECT ID, Name AS TEXT, ParentID, 'open' AS  state , '0' AS attr FROM category");
			DataRow newDr = dt.NewRow();
			newDr["ID"] = "-1";
			newDr["TEXT"] = "请选择";
			newDr["ParentID"] = "0";
			newDr["state"] = "open";
			newDr["attr"] = "0";
			dt.Rows.InsertAt(newDr, 0);
			List<JsonTree> list = EUItree.initTree(dt);
			return JsonDate(list);
		}

		#endregion

		#region 编辑、添加、添加子级 

		public ActionResult Edit(int id, int parentID) {
			Category objCategory = new Category();
			if (id > 0) {
				objCategory = CategoryService.GetSingleCategory(id);
			}
			if (parentID > 0) {
				objCategory.ParentID = parentID;
			}
			else {
				objCategory.ParentID = objCategory.ParentID == 0 ? -1 : objCategory.ParentID;
			}
			ViewBag.Category = objCategory;
			return View();
		}

		#endregion

		#region 保存

		[HttpPost]
		public ActionResult Save(Category obj) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = CategoryManager.Save(userCode, obj);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 删除

		public ActionResult Delete(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = CategoryManager.Del(userCode, idList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 检查分类名称唯一性(同一父级下唯一)

		/// <summary>
		/// 检查分类名称唯一性(同一父级下唯一)
		/// </summary>
		/// <param name="name">分类名称</param>
		/// <param name="id">编辑时需要排除自己的ID</param>
		/// <param name="parentID">父级别ID</param>
		/// <returns></returns>
		public ActionResult CheckName(string name, int id, int parentID) {
			if (parentID == -1) {
				parentID = 0;
			}
			BaseResult BaseResult = new BaseResult();
			if (id > 0) {
				if (CategoryService.GetCategoryID(name, parentID, id) > 0) {
					BaseResult.result = -1;
				}

			}
			else {
				if (CategoryService.GetCategoryID(name, parentID) > 0) {
					BaseResult.result = -1;
				}
			}
			return JsonDate(BaseResult);
		}
		#endregion
    }
}
