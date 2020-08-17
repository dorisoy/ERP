using FluentData;
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

namespace PaiXie.Erp.Areas.Warehouse
{
    public class MoveLocationItemController : BaseController {
		
		#region Index

		public ActionResult Index(int moveLocationID)
        {
			WarehouseMoveLocation warehouseMoveLocation = WarehouseMoveLocationService.GetQuerySingleByID(moveLocationID);
			ViewBag.WarehouseMoveLocation = warehouseMoveLocation;
			ViewBag.MoveNum = WarehouseMoveLocationItemService.GetNumByMoveLocationID(moveLocationID);
            return View();
		}

		#endregion

		#region 移位商品列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wmli.ID desc";
			data.From = @"warehouseMoveLocationItem wmli";
			data.Select = @"wmli.ID,wmli.ProductsCode,wmli.ProductsName,wmli.ProductsSkuCode,wmli.ProductsSkuSaleprop,wmli.ProductsBatchCode,
			(SELECT CODE FROM warehouseLocation WHERE ID=wmli.OutLocationID) AS OutLocationCode,
			(SELECT CODE FROM warehouseLocation WHERE ID=wmli.InLocationID) AS InLocationCode,Num";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseMoveLocationItemList> list = BaseService<WarehouseMoveLocationItemList>.GetQueryManyForPage(data, out total, null, null);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}
		private string GetWhereSql() {
			int moveLocationID = ZConvert.StrToInt(Request["moveLocationID"]);
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string whereSql = string.Format("wmli.WarehouseCode = '{0}' AND wmli.MoveLocationID={1}", FormsAuth.GetWarehouseCode(), moveLocationID);
			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format("  AND wmli.ProductsName like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format("  AND wmli.ProductsCode like '%{0}%'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format("  AND wmli.ProductsNo like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format("  AND wmli.ProductsSkuCode like '%{0}%'", keyWord);
						break;
				}
			}
			return whereSql;
		}

		#endregion

		#region 添加商品

		public ActionResult Add(int moveLocationID, string moveLocationBillNo) {
			ViewBag.MoveLocationID = moveLocationID;
			ViewBag.MoveLocationBillNo = moveLocationBillNo;
			return View();
		}

		#endregion

		#region 搜索商品SKU码搜索

		public ActionResult SearchProductsSku(string productsSkuCode) {
			BaseResult resultInfo = new BaseResult();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string name = string.Empty;
			string no = string.Empty;
			string code = string.Empty;
			string saleprop = string.Empty;
			int id = 0;
			int productsSkuID = 0;
			WarehouseProductsSkuInfo warehouseProductsSkuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(warehouseCode, productsSkuCode);
			if (warehouseProductsSkuInfo == null) {
				resultInfo.result = 0;
				resultInfo.message = "商品SKU码不存在！";
			}
			else {
				name = warehouseProductsSkuInfo.ProductsName;
				no = warehouseProductsSkuInfo.ProductsNo;
				code = warehouseProductsSkuInfo.ProductsCode;
				id = warehouseProductsSkuInfo.ProductsID;
				productsSkuID = warehouseProductsSkuInfo.ID;
				saleprop = warehouseProductsSkuInfo.Saleprop;
			}
			var result = new { result = resultInfo.result, message = resultInfo.message, Name = name, Code = code, No = no, ID = id, ProductsSkuID = productsSkuID, Saleprop = saleprop };
			return JsonDate(result);
		}

		#endregion

		#region 搜索SKU的库位商品列表

		/// <summary>
		/// 搜索SKU的库位商品列表
		/// </summary>
		/// <param name="productsSkuID">商品SKUID/param>
		/// <param name="topLocationID">库区ID</param>
		/// <param name="locationID">库位ID</param>
		/// <returns></returns>
		public ActionResult SearchLocationProducts(int productsSkuID, int topLocationID, int locationID) {
			SelectBuilder data = new SelectBuilder();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string whereSql = string.Format("wlp.WarehouseCode='{0}' AND wlp.ProductsSkuID={1} AND wlp.KyNum-wlp.ZyNum>0", warehouseCode, productsSkuID);
			if (topLocationID > 0) {
				whereSql += " AND wlp.TopLocationID=" + topLocationID; ;
			}
			if (locationID > 0) {
				whereSql += " AND wlp.LocationID=" + locationID; ;
			}
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wl.Code Asc,wlp.ID DESC";
			data.From = @"warehouseLocationProducts wlp LEFT JOIN warehouseLocation wl on wlp.LocationID=wl.ID";
			data.Select = @"wl.Code as LocationCode, wlp.LocationID, wlp.ProductsBatchCode,wlp.ProductsBatchID,(wlp.KyNum-wlp.ZyNum) AS KyNum";
			data.WhereSql = whereSql;
			int total = 0;
			List<MoveLocationProductsList> list = BaseService<MoveLocationProductsList>.GetQueryManyForPage(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 保存商品

		public ActionResult Save(MoveLocationItemWebInfo obj) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = MoveLocationManager.AddMoveLocationItem(userCode, warehouseCode, obj);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 导入

		/// <summary>
		/// 导入商品
		/// </summary>
		/// <param name="moveLocationID">移位单主键ID</param>
		/// <returns></returns>
		public ActionResult Import(int moveLocationID) {
			ViewBag.MoveLocationID = moveLocationID;
			return View();
		}

		private string[] arrAllowedFiles = new string[] { "xls", "xlsx" };
		public ActionResult ImportMoveLocationItem(int moveLocationID) {
			BaseResult resultInfo = new BaseResult();
			string userCode = FormsAuth.GetUserCode();
			WarehouseMoveLocation moveLocation = WarehouseMoveLocationService.GetQuerySingleByID(moveLocationID);
			if (moveLocation != null) {
				if (moveLocation.Status == (int)MoveLocationStatus.未确认) {
					string fileUrl = string.Empty;
					if (Request.Files != null && Request.Files.Count > 0) {
						var file = Request.Files[0];
						string fileSuffix = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace(".", "");
						if (file.ContentLength <= 1024 * 1024 * 20) {
							if (arrAllowedFiles.Contains(fileSuffix.ToLower())) {
								fileUrl = FileUploader.Upload(file, "MoveLocationItem");
								DataTable dt = null;
								try {
									dt = ExcelHelp.importMin.Import(Server.MapPath(fileUrl), 1);
									dt = ZDataSet.removeEmpty(dt);
									int columnsCount = dt.Columns.Count;
									int rowsCount = dt.Rows.Count;
									if (columnsCount >= 6) {
										using (IDbContext context = Db.GetInstance().Context()) {
											context.UseTransaction(true);
											for (int i = 0; i < rowsCount; i++) {
												int rowNumber = i + 3;
												#region 获取表格内容并校验

												#region *商品SKU码

												string productsSkuCode = dt.Rows[i]["*商品SKU码"].ToString();
												if (productsSkuCode == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行商品SKU码未填写！";
													break;
												}
												WarehouseProductsSkuInfo skuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(moveLocation.WarehouseCode, productsSkuCode, context);
												if (skuInfo == null) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行商品SKU码 " + productsSkuCode + " 不存在！";
													break;
												}
												#endregion

												#region *批次

												string batchCode = dt.Rows[i]["*批次"].ToString();
												if (batchCode == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行批次未填写！";
													break;
												}
												WarehouseProductsBatch warehouseProductsBatch = WarehouseProductsBatchService.GetSingleWarehouseProductsBatch(moveLocation.WarehouseCode, skuInfo.ID, batchCode, context);
												if (warehouseProductsBatch == null) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行SKU不存在该批次！";
													break;
												}

												#endregion

												#region *移出库位编码
												string outLcode = dt.Rows[i]["*移出库位编码"].ToString();
												if (outLcode == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行移出库位编码未填写！";
													break;
												}
												WarehouseLocation outWarehouseLocation = WarehouseLocationService.GetSingleWarehouseLocation(moveLocation.WarehouseCode, outLcode, context);
												//排除掉库区
												if (outWarehouseLocation == null || outWarehouseLocation.ParentID == 0) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行移出库位编码不存在！";
													break;
												}
												#endregion

												#region *移入库位编码
												string inLcode = dt.Rows[i]["*移入库位编码"].ToString();
												if (inLcode == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行移入库位编码未填写！";
													break;
												}
												WarehouseLocation inWarehouseLocation = WarehouseLocationService.GetSingleWarehouseLocation(moveLocation.WarehouseCode, inLcode, context);
												//排除掉库区
												if (inWarehouseLocation == null || inWarehouseLocation.ParentID == 0) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行移入库位编码不存在！";
													break;
												}
												#endregion

												if (outLcode.Equals(inLcode)) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行移出库位编码和移入库位编码不能一样！";
													break;
												}

												#region *移位数量

												string num = dt.Rows[i]["*移位数量"].ToString();
												if (num == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行移位数量未填写！";
													break;
												}
												if (!(ZConvert.StrToInt(num) > 0 && ZConvert.StrToInt(num) <= 9999999)) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行移位数量填写不正确，必须是大于0且小于9999999的整数！";
													break;
												}
												#endregion

												#endregion

												#region 移位单商品

												MoveLocationItemWebInfo objWebInfo = new MoveLocationItemWebInfo();
												objWebInfo.OutLocationCode = new[] { outLcode };
												objWebInfo.OutLocationID = new[] { outWarehouseLocation.ID };
												objWebInfo.InLocationCode = inLcode;
												objWebInfo.InLocationID = inWarehouseLocation.ID;
												objWebInfo.MoveLocationBillNo = moveLocation.BillNo;
												objWebInfo.MoveLocationID = moveLocation.ID;
												objWebInfo.MoveNum = new[] { ZConvert.StrToInt(num) };
												objWebInfo.ProductsBatchCode = new[] { batchCode };
												objWebInfo.ProductsBatchID = new[] { warehouseProductsBatch.ID };
												objWebInfo.ProductsCode = skuInfo.ProductsCode;
												objWebInfo.ProductsID = skuInfo.ProductsID;
												objWebInfo.ProductsName = skuInfo.ProductsName;
												objWebInfo.ProductsNo = skuInfo.ProductsNo;
												objWebInfo.ProductsSkuCode = skuInfo.Code;
												objWebInfo.ProductsSkuID = skuInfo.ID;
												objWebInfo.ProductsSkuSaleprop = skuInfo.Saleprop;
												BaseResult currResultInfo = MoveLocationManager.AddMoveLocationItem(userCode, moveLocation.WarehouseCode, objWebInfo, context);
												if (currResultInfo.result == 0) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行添加失败，" + currResultInfo.message;
													break;
												}

												#endregion
											}
											if (resultInfo.result == 1) {
												context.Commit();
											}
											else {
												context.Rollback();
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
									PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "移位单 " + moveLocation.BillNo + " 导入商品", userCode);
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "只能导入 .xls或.xlsx类型的文件！";
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "只能导入小于等于20M的文件！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "请选择要导入的文件！";
					}
				}
				else {
					resultInfo.result = 0;
					resultInfo.message = MoveLocationStatus.未确认.ToString() + "的移位单才可以导入！";
				}
			}
			else {
				resultInfo.result = 0;
				resultInfo.message = "移位单不存在或已被删除！";
			}

			//   构造成Json的格式传递
			var result = new { result = resultInfo.result, message = resultInfo.message };
			return JsonDate(result);
		}

		#endregion

		#region 删除

		public ActionResult Delete(int moveLocationID, string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> moveLocationItemIDList = new List<int>();
			moveLocationItemIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = MoveLocationManager.DelMoveLocationItem(userCode, moveLocationID, moveLocationItemIDList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 获取移位数量汇总

		public ActionResult GetNum(int moveLocationID) {
			int moveNum = WarehouseMoveLocationItemService.GetNumByMoveLocationID(moveLocationID);
			var result = new { moveNum = moveNum };
			return JsonDate(result);
		}

		#endregion

		#region 修改移入库位编码和移位数量

		/// <summary>
		/// 修改移入库位编码和移位数量
		/// </summary>
		/// <param name="moveLocationItemID">移位单商品明细主键ID</param>
		/// <param name="inLocationCode">移入库位编码</param>
		/// <param name="moveNum">移位数量</param>
		/// <returns></returns>
		public ActionResult UpdateMoveLocationItem(int moveLocationItemID, string inLocationCode, int moveNum) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = MoveLocationManager.UpdateMoveLocationItem(userCode, moveLocationItemID, inLocationCode, moveNum);
			return JsonDate(resultInfo);
		}

		#endregion
	}
}
