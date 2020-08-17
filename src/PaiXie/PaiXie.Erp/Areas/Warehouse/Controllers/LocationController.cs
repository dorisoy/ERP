using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Excel;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Warehouse {
	public class LocationController : BaseController {
		#region Index

		public ActionResult Index() {
			return View();
		}

		#endregion

		#region 库区列表

		/// <summary>
		/// 库区列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string whereSql = string.Format("wl.ParentID=0 and wl.WarehouseCode='{0}' and wl.TypeID in({1},{2})", warehouseCode, (int)LocationType.发货区, (int)LocationType.备用区);

			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wl.Seq ASC,wl.ID DESC";
			data.From = "warehouseLocation wl";
			data.Select = @"wl.ID,wl.WarehouseCode,wl.Name,CODE,TypeID,
			IFNULL((SELECT Count(0) FROM warehouseLocation WHERE ParentID=wl.ID),0) as LocationNum,
			IFNULL((SELECT SUM(ZkNum) FROM warehouseLocationProducts Where TopLocationID=wl.ID),0) as ProductsNum";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseLocationList> list = BaseService<WarehouseLocationList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				item.TypeName = WarehouseLocationService.GetTypeName(item.TypeID);
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 编辑、添加库区

		public ActionResult Edit(int id = 0) {
			WarehouseLocation objWarehouseLocation = new WarehouseLocation();
			if (id > 0) {
				objWarehouseLocation = WarehouseLocationService.GetQuerySingleByID(id);
			}
			ViewBag.WarehouseLocation = objWarehouseLocation;
			return View();
		}

		#endregion

		#region 删除库区

		public ActionResult Delete(string ids) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/LocationController/Delete";
			string buttonName = "删除库区";
			string target = "库区列表";
			BaseResult resultInfo = LocationManager.DelTopLocation(userCode, warehouseCode, position, target, buttonName, ids);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 保存库区

		[HttpPost]
		public ActionResult Save(WarehouseLocationInfo obj) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/LocationController/Save";
			string buttonName = "保存库区";
			string target = "库区列表";
			BaseResult resultInfo = LocationManager.Save(userCode, warehouseCode, position, target, buttonName, obj);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 检查库区或库位代码是否已经存在于同一个仓库

		/// <summary>
		/// 检查库区或库位代码是否已经存在于同一个仓库
		/// </summary>
		/// <param name="id">编辑时需要排除的id</param>
		/// <param name="code">代码</param>
		/// <returns></returns>
		public ActionResult CheckCode(int id, string code) {
			BaseResult resultInfo = new BaseResult();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			if (id > 0) {
				if (WarehouseLocationService.GetWarehouseLocationID(warehouseCode, code, id) > 0) {
					resultInfo.result = -1;
				}
			}
			else {
				if (WarehouseLocationService.GetWarehouseLocationID(warehouseCode, code) > 0) {
					resultInfo.result = -1;
				}
			}
			return JsonDate(resultInfo);
		}

		#endregion

		#region 编辑、添加库位

		public ActionResult EditChild(int parentID, int id = 0) {
			WarehouseLocation objWarehouseLocation = new WarehouseLocation();
			if (id > 0) {
				objWarehouseLocation = WarehouseLocationService.GetQuerySingleByID(id);
			}
			ViewBag.ParentID = parentID;
			ViewBag.WarehouseLocation = objWarehouseLocation;
			return View();
		}

		#endregion

		#region 删除库位

		public ActionResult DeleteChild(string ids) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/LocationController/DeleteChild";
			string buttonName = "删除库位";
			string target = "库区商品";
			BaseResult resultInfo = LocationManager.DeleteChild(userCode, warehouseCode, position, target, buttonName, ids);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 导入库位

		public ActionResult Import(int parentID) {
			ViewBag.ParentID = parentID;
			return View();
		}

		private string[] arrAllowedFiles = new string[] { "xls", "xlsx" };
		public ActionResult ImportLocation(int parentID) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = new BaseResult();
			int successCount = 0;
			int errorCount = 0;
			string fileUrl = string.Empty;
			if (Request.Files != null && Request.Files.Count > 0) {
				var file = Request.Files[0];
				string fileSuffix = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace(".", "");
				if (file.ContentLength <= 1024 * 1024 * 20) {
					if (arrAllowedFiles.Contains(fileSuffix.ToLower())) {
						fileUrl = FileUploader.Upload(file, "Location");

						DataTable dt = null;
						try {
							dt = ExcelHelp.importMin.Import(Server.MapPath(fileUrl), 1);
							dt = ZDataSet.removeEmpty(dt);
							int columnsCount = dt.Columns.Count;
							int rowsCount = dt.Rows.Count;
							if (columnsCount >= 2) {
								WarehouseLocation topLocation = WarehouseLocationService.GetQuerySingleByID(parentID);
								for (int i = 0; i < rowsCount; i++) {
									#region 获取表格内容并校验
									string locationName = dt.Rows[i]["*库位名称"].ToString();
									if (locationName == "") {
										errorCount++;
										continue;
									}
									string locationCode = dt.Rows[i]["*库位编码"].ToString();
									if (locationCode == "") {
										errorCount++;
										continue;
									}
									WarehouseLocation objWarehouseLocation = WarehouseLocationService.GetSingleWarehouseLocation(warehouseCode, locationCode);
									if (objWarehouseLocation == null) {
										objWarehouseLocation = new WarehouseLocation();
										objWarehouseLocation.TypeID = topLocation.TypeID;
										objWarehouseLocation.ParentID = parentID;
										objWarehouseLocation.Name = locationName;
										objWarehouseLocation.Code = locationCode;
									}
									else {
										errorCount++;
										continue;
									}
									#endregion
									string position = "Warehouse/LocationController/ImportLocation";
									string buttonName = "导入库位";
									string target = "库区管理";
									BaseResult currentResultInfo = LocationManager.Save(userCode, warehouseCode, position, target, buttonName, objWarehouseLocation);
									if (currentResultInfo.result == 1) {
										successCount++;
									}
									else {
										errorCount++;
									}
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "导入的模版列数不正确！";
							}
						}
						catch (Exception ex) {
							resultInfo.result = 0;
							resultInfo.message = "导入的数据格式不正确！";
							PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "表格导入商品", userCode);
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "只能上传 .xls或.xlsx类型的文件！";
					}
				}
				else {
					resultInfo.result = 0;
					resultInfo.message = "只能上传小于等于20M的文件！";
				}
			}
			else {
				resultInfo.result = 0;
				resultInfo.message = "请选择要上传的文件！";
			}
			//   构造成Json的格式传递
			var result = new { result = resultInfo.result, message = resultInfo.message, successCount = successCount, errorCount = errorCount };
			return JsonDate(result);
		}

		#endregion

		#region 保存库位

		[HttpPost]
		public ActionResult SaveChild(WarehouseLocation warehouseLocation) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string position = "Warehouse/LocationController/SaveChild";
			string buttonName = "保存库位";
			string target = "库区管理";
			BaseResult resultInfo = LocationManager.Save(userCode, warehouseCode, position, target, buttonName, warehouseLocation);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 查看库位商品

		public ActionResult Show(int id) {
			WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(id);
			ViewBag.WarehouseLocation = warehouseLocation;
			ViewBag.ProductsNum = WarehouseLocationProductsService.GetProductsNum(id);
			return View();
		}

		public ActionResult ShowSkuList(int id) {
			List<LocationProductsKucInfo> locationProductsKucInfo = LocationManager.GetLocationProductsKucInfo(id);
			return JsonDate(locationProductsKucInfo);
		}

		#endregion

		#region 库区列表

		public ActionResult GetDictTopLocation() {
			CListItem cListItem = new CListItem();
			cListItem.Text = "请选择库区";
			cListItem.Value = "0";
			List<CListItem> treeList = new List<CListItem>();
			treeList.Add(cListItem);
			string sqlStr = "SELECT ID AS VALUE, NAME AS TEXT FROM warehouseLocation WHERE ParentID=0 AND WarehouseCode='" + FormsAuth.GetWarehouseCode() + "' ORDER BY typeid desc";
			if (!string.IsNullOrEmpty(sqlStr)) {
				List<CListItem> objlist = Db.GetInstance().Context().Sql(sqlStr).QueryMany<CListItem>();
				treeList.AddRange(objlist);
			}
			return JsonDate(treeList);
		}

		#endregion

		#region 库区下的库位列表

		public ActionResult GetDictLocation(int topLocationID) {
			CListItem cListItem = new CListItem();
			cListItem.Text = "请选择库位";
			cListItem.Value = "0";
			List<CListItem> treeList = new List<CListItem>();
			treeList.Add(cListItem);
			string sqlStr = "SELECT ID AS VALUE, Code AS TEXT FROM warehouseLocation WHERE ParentID=" + topLocationID + " AND WarehouseCode='" + FormsAuth.GetWarehouseCode() + "' ORDER BY Seq,ID ASC";
			if (!string.IsNullOrEmpty(sqlStr)) {
				List<CListItem> objlist = Db.GetInstance().Context().Sql(sqlStr).QueryMany<CListItem>();
				treeList.AddRange(objlist);
			}
			return JsonDate(treeList);
		}

		#endregion
	}
}
