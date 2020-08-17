using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Warehouse
{
	public class AreaStructController : BaseController
    {
		#region Index

		public ActionResult Index() {
			return View();
		}

		#endregion

		#region 库区结构列表

		/// <summary>
		/// 库区结构列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			EasyUITree EUItree = new EasyUITree();
			Object[] objects = new Object[1];
			objects[0] = FormsAuth.GetWarehouseCode();
			DataTable dt = CategoryService.GetDataTable("SELECT was.ID, was.Name AS TEXT, was.ParentID, 'open' AS  state , (SELECT COUNT(0) FROM warehouseAreaStruct WHERE ParentID=was.ID) AS attr FROM warehouseAreaStruct was Where was.WarehouseCode=@0 ORDER BY was.Seq ASC,was.ID DESC", null, objects);
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
			Object[] objects = new Object[1];
			objects[0] = FormsAuth.GetWarehouseCode();
			DataTable dt = CategoryService.GetDataTable("SELECT ID, Name AS TEXT, ParentID, 'open' AS  state , '0' AS attr FROM warehouseAreaStruct Where WarehouseCode=@0", null, objects);
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

		/// <summary>
		/// 顶级结构下拉列表
		/// </summary>
		/// <returns></returns>
		public ActionResult JsonTop() {
			Object[] objects = new Object[1];
			objects[0] = FormsAuth.GetWarehouseCode();
			DataTable dt = CategoryService.GetDataTable("SELECT  ID AS Value ,Name AS  Text FROM warehouseAreaStruct Where ParentID=0 and WarehouseCode=@0 ORDER BY Seq ASC,ID DESC", null, objects);
			DataRow newDr = dt.NewRow();
			newDr["Value"] = "0";
			newDr["Text"] = "请选择";
			dt.Rows.InsertAt(newDr, 0);
			return JsonDate(dt);
		}

		#endregion

		#region 编辑、添加、添加子级

		public ActionResult Edit(int id, int parentID) {
			WarehouseAreaStruct objWarehouseAreaStruct = new WarehouseAreaStruct();
			if (id > 0) {
				objWarehouseAreaStruct = WarehouseAreaStructService.GetSingleWarehouseAreaStruct(id);
			}
			if (parentID > 0) {
				objWarehouseAreaStruct.ParentID = parentID;
			}
			else {
				objWarehouseAreaStruct.ParentID = objWarehouseAreaStruct.ParentID == 0 ? -1 : objWarehouseAreaStruct.ParentID;
			}
			ViewBag.WarehouseAreaStruct = objWarehouseAreaStruct;
			return View();
		}

		#endregion

		#region 保存

		[HttpPost]
		public ActionResult Save(WarehouseAreaStruct obj) {
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = AreaStructManager.Save(warehouseCode, userCode, obj);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 删除

		public ActionResult Delete(string ids) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = AreaStructManager.Del(userCode, ids);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 检查父级结构下面是否已经有子级结构，如果是顶级结构就检查名称是否存在

		/// <summary>
		/// 检查父级结构下面是否已经有子级结构，如果是顶级结构就检查名称是否存在
		/// </summary>
		/// <param name="id">编辑时排除自身ID</param>
		/// <param name="name">当前结构名称</param>
		/// <param name="parentID">父级结构ID</param>
		/// <returns></returns>
		public ActionResult CheckName(int id, string name, int parentID) {
			BaseResult resultInfo = new BaseResult();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			if (parentID <= 0) {
				parentID = 0;
				if (id > 0) {
					if (WarehouseAreaStructService.GetWarehouseAreaStructID(warehouseCode, name, parentID, id) > 0) {
						resultInfo.result = -1;
						resultInfo.message = "结构名称已经存在！";
					}
				}
				else {
					if (WarehouseAreaStructService.GetWarehouseAreaStructID(warehouseCode, name, parentID) > 0) {
						resultInfo.result = -1;
						resultInfo.message = "结构名称已经存在！";
					}
				}
			}
			else {

				if (id == 0) {
					List<int> idList = WarehouseAreaStructService.GetChildWarehouseAreaStructID(parentID);
					if (idList.Count > 0) {
						resultInfo.result = -1;
						resultInfo.message = "该结构下已经有子级！";
					}
				}
				else {
					//非顶级结构永远只有一个子级，所以编辑时不用校验
				}

			}
			return JsonDate(resultInfo);
		}

		#endregion

		#region 根据父级结构ID递归获取所有子结构 排序从上到下

		/// <summary>
		/// 根据父级结构ID递归获取所有子结构 排序从上到下
		/// </summary>
		/// <param name="parentID">父级结构ID</param>
		/// <returns></returns>
		public ActionResult GetChildWarehouseAreaStruct(int parentID) {
			List<WarehouseAreaStruct> warehouseAreaStructList = new List<WarehouseAreaStruct>();
			WarehouseAreaStructService.GetChildWarehouseAreaStruct(parentID, ref warehouseAreaStructList);
			return JsonDate(warehouseAreaStructList);
		}

		#endregion
	}
}
