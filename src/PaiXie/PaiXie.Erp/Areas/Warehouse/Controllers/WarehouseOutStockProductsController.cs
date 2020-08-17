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
	public class WarehouseOutStockProductsController : BaseController {

		#region Index

		public ActionResult Index(int outInStockID) {
			ViewBag.OutInStockID = outInStockID;
			ViewBag.ProductsNum = WarehouseOutInStockItemService.GetProductsNumByOutInStockID(outInStockID);
			ViewBag.WarehouseOutInStock = WarehouseOutInStockService.GetQuerySingleByID(outInStockID);
			return View();
		}

		#endregion

		#region 出库单商品列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "woii.ID DESC";
			data.From = @"warehouseOutInStockItem woii LEFT JOIN warehouseLocation wl ON woii.LocationID=wl.ID
			LEFT JOIN warehouseLocationProducts wlp ON woii.ProductsSkuID=wlp.ProductsSkuID AND woii.LocationID=wlp.LocationID AND woii.ProductsBatchID=wlp.ProductsBatchID";
			data.Select = @"woii.WarehouseCode, woii.ID,woii.ProductsID,woii.ProductsSkuID,woii.ProductsCode,woii.ProductsName,
			woii.ProductsSkuSaleprop,woii.ProductsSkuCode,IFNULL(wl.Code,'') AS LocationCode,IFNULL(woii.ProductsBatchCode,'') AS ProductsBatchCode,(wlp.KyNum-wlp.ZyNum) AS KyNum,woii.ProductsNum";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseOutStockItemList> list = BaseService<WarehouseOutStockItemList>.GetQueryManyForPage(data, out total);
			for (int z = 0; z < list.Count(); z++) {
				string Price=WarehouseProductsBatchService.Getobject(" SELECT CostPrice FROM   warehouseProductsBatch WHERE BatchCode='"+list[z].ProductsBatchCode+"'AND productsskuid="+list[z].ProductsSkuID+" AND WarehouseCode='"+list[z].WarehouseCode+"'");
				list[z].CostPrice = ZConvert.StrToDecimal(Price,0);
			}
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int outInStockID = ZConvert.StrToInt(Request["outInStockID"]);
			string whereSql = string.Format("woii.OutInStockID={0}", outInStockID);
			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and woii.ProductsName like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and woii.ProductsCode like '%{0}%'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and woii.ProductsNo like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and woii.ProductsSkuCode like '%{0}%'", keyWord);
						break;
					case "批次":
						whereSql += string.Format(" and woii.ProductsBatchCode like '%{0}%'", keyWord);
						break;
					case "库位编码":
						whereSql += string.Format(" and wl.Code like '%{0}%'", keyWord);
						break;
				}
			}
			return whereSql;
		}

		#endregion

		#region 添加商品

		/// <summary>
		/// 添加商品
		/// </summary>
		/// <param name="sourceID">来源ID(采购入库单ID)</param>
		/// <param name="outInStockID">出库单ID</param>
		/// <param name="outInStockBillNo">出库单号</param>
		/// <returns></returns>
		public ActionResult Add(int sourceID, int outInStockID, string outInStockBillNo) {
			ViewBag.SourceID = sourceID;
			ViewBag.OutInStockID = outInStockID;
			ViewBag.OutInStockBillNo = outInStockBillNo;
			return View();
		}

		#endregion

		#region 搜索商品SKU码搜索

		public ActionResult SearchProductsSku(int sourceID, string productsSkuCode) {
			productsSkuCode = productsSkuCode.Trim();
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
				bool cgrHasPro = true;
				if (sourceID > 0) {
					WarehouseOutInStockItem CgrWarehouseOutInStockItem = WarehouseOutInStockItemService.GetSingleWarehouseOutInStockItem(sourceID, warehouseProductsSkuInfo.ID);
					cgrHasPro = CgrWarehouseOutInStockItem != null;
				}
				if (!cgrHasPro) {
					resultInfo.result = 0;
					resultInfo.message = "商品SKU码不属于关联的采购入库单！";
				}
				else {
					name = warehouseProductsSkuInfo.ProductsName;
					no = warehouseProductsSkuInfo.ProductsNo;
					code = warehouseProductsSkuInfo.ProductsCode;
					id = warehouseProductsSkuInfo.ProductsID;
					productsSkuID = warehouseProductsSkuInfo.ID;
					saleprop = warehouseProductsSkuInfo.Saleprop;
				}
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
		/// <returns></returns>
		public ActionResult SearchLocationProducts(int productsSkuID, int topLocationID) {
			SelectBuilder data = new SelectBuilder();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string whereSql = string.Format("wlp.WarehouseCode='{0}' AND wlp.ProductsSkuID={1} AND wlp.KyNum-wlp.ZyNum>0", warehouseCode, productsSkuID);
			if (topLocationID > 0) {
				whereSql += " AND wlp.TopLocationID=" + topLocationID; ;
			}
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wl.Seq,wlp.LocationID ASC, wlp.ID DESC";
			data.From = @"warehouseLocationProducts wlp INNER JOIN warehouseLocation wl ON wlp.LocationID=wl.ID";
			data.Select = @"wl.Code AS LocationCode,wl.ID AS LocationID,wlp.ProductsBatchCode,wlp.ProductsBatchID,(wlp.KyNum-wlp.ZyNum) AS KyNum, 0 AS OutNum";
			data.WhereSql = whereSql;
			int total = 0;
			List<OutStockLocationProductsList> list = WarehouseOutInStockItemService.GetQueryManyForOutStockLocationProductsList(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 保存商品

		public ActionResult Save(OutStockLocationProductsWebInfo obj) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutInStockManager.AddOutStockProducts(userCode, warehouseCode, obj);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 导入商品

		/// <summary>
		/// 导入商品
		/// </summary>
		/// <param name="outInStockID">出库单主键ID</param>
		/// <returns></returns>
		public ActionResult Import(int outInStockID) {
			ViewBag.OutInStockID = outInStockID;
			return View();
		}

		private string[] arrAllowedFiles = new string[] { "xls", "xlsx" };
		public ActionResult ImportOutStockProducts(int outInStockID) {
			BaseResult resultInfo = new BaseResult();
			string userCode = FormsAuth.GetUserCode();
			WarehouseOutInStock outStock = WarehouseOutInStockService.GetQuerySingleByID(outInStockID);
			if (outStock != null) {
				if (outStock.Status == (int)WarehouseOutInStockStatus.未提交) {
					string fileUrl = string.Empty;
					if (Request.Files != null && Request.Files.Count > 0) {
						var file = Request.Files[0];
						string fileSuffix = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace(".", "");
						if (file.ContentLength <= 1024 * 1024 * 20) {
							if (arrAllowedFiles.Contains(fileSuffix.ToLower())) {
								fileUrl = FileUploader.Upload(file, "OutStock");
								DataTable dt = null;
								try {
									dt = ExcelHelp.importMin.Import(Server.MapPath(fileUrl), 1);
									dt = ZDataSet.removeEmpty(dt);
									int columnsCount = dt.Columns.Count;
									int rowsCount = dt.Rows.Count;
									if (columnsCount >= 7) {
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
												WarehouseProductsSkuInfo skuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(outStock.WarehouseCode, productsSkuCode, context);
												if (skuInfo == null) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行商品SKU码 " + productsSkuCode + " 不存在！";
													break;
												}
												#endregion

												#region *库位编码
												string lcode = dt.Rows[i]["*库位编码"].ToString();
												if (lcode == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行库位编码未填写！";
													break;
												}
												WarehouseLocation warehouseLocation = WarehouseLocationService.GetSingleWarehouseLocation(outStock.WarehouseCode, lcode, context);
												//排除掉库区
												if (warehouseLocation == null || warehouseLocation.ParentID == 0) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行库位编码不存在！";
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
												WarehouseProductsBatch warehouseProductsBatch = WarehouseProductsBatchService.GetSingleWarehouseProductsBatch(outStock.WarehouseCode, skuInfo.ID, batchCode, context);
												if (warehouseProductsBatch == null) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行SKU不存在该批次！";
													break;
												}

												#endregion

												#region *出库数量

												string num = dt.Rows[i]["*出库数量"].ToString();
												if (num == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行出库数量未填写！";
													break;
												}
												if (!(ZConvert.StrToInt(num) > 0 && ZConvert.StrToInt(num) <= 9999999)) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行出库数量填写不正确，必须是大于0且小于9999999的整数！";
													break;
												}
												#endregion

												#endregion

												#region 出库单商品
												OutStockLocationProductsWebInfo objWebInfo = new OutStockLocationProductsWebInfo();
												objWebInfo.LocationCode = new[] { lcode };
												objWebInfo.LocationID = new[] { warehouseLocation.ID};
												objWebInfo.OutInStockBillNo = outStock.BillNo;
												objWebInfo.OutInStockID = outStock.ID;
												objWebInfo.OutNum = new[]{ ZConvert.StrToInt(num)};
												objWebInfo.ProductsBatchCode = new[] { batchCode };
												objWebInfo.ProductsBatchID = new[] { warehouseProductsBatch.ID };
												objWebInfo.ProductsCode = skuInfo.ProductsCode;
												objWebInfo.ProductsID = skuInfo.ProductsID;
												objWebInfo.ProductsName = skuInfo.ProductsName;
												objWebInfo.ProductsNo = skuInfo.ProductsNo;
												objWebInfo.ProductsSkuCode = skuInfo.Code;
												objWebInfo.ProductsSkuID = skuInfo.ID;
												objWebInfo.ProductsSkuSaleprop = skuInfo.Saleprop;
												BaseResult currResultInfo = OutInStockManager.AddOutStockProducts(userCode, outStock.WarehouseCode, objWebInfo, context);
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
									PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "出库单 " + outStock.BillNo + " 导入商品", userCode);
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
					resultInfo.message = WarehouseOutInStockStatus.未提交.ToString() + "的出库单才可以导入！";
				}
			}
			else {
				resultInfo.result = 0;
				resultInfo.message = "出库单不存在或已被删除！";
			}

			//   构造成Json的格式传递
			var result = new { result = resultInfo.result, message = resultInfo.message };
			return JsonDate(result);
		}

		#endregion

		#region 删除商品

		public ActionResult Delete(int outInStockID, string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> outInStockItemIDList = new List<int>();
			outInStockItemIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = OutInStockManager.DelOutInStockItem(userCode, outInStockID, outInStockItemIDList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 获取出库数量汇总

		public ActionResult GetNum(int outInStockID) {
			int outStockNum = WarehouseOutInStockItemService.GetProductsNumByOutInStockID(outInStockID);
			var result = new { outStockNum = outStockNum };
			return JsonDate(result);
		}

		#endregion

		#region 修改出库单明细的出库数量

		public ActionResult UpdateOutStockNum(int outStockItemID, int outStockNum) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = OutInStockManager.UpdateOutStockNum(userCode, outStockItemID, outStockNum);
			return JsonDate(resultInfo);
		}

		#endregion
	}
}
