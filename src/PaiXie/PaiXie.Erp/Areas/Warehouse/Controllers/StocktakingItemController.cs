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

namespace PaiXie.Erp.Areas.Warehouse {
	public class StocktakingItemController : BaseController {
		//
		// GET: /Warehouse/StocktakingItem/

		public ActionResult Index(int stocktakingID) {
			WarehouseStocktaking warehouseStocktaking = WarehouseStocktakingService.GetQuerySingleByID(stocktakingID);
			ViewBag.WarehouseStocktaking = warehouseStocktaking;
			return View();
		}

		/// <summary>
		/// 盘点记录列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "s.ID DESC";
			data.From = @"warehouseStocktakingItem s LEFT JOIN warehouselocationproducts p on s.SourceID = p.ID";
			data.Select = "s.ID,s.LocationCode,s.ProductsCode,s.ProductsName,s.ProductsSkuSaleprop,s.ProductsSkuCode,s.ProductsBatchCode,s.IsImport,s.PdNum,p.ZkNum,p.ZyNum,p.DjNum";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseStocktakingItem> list = BaseService<WarehouseStocktakingItem>.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		/// <summary>
		/// 获取搜索条件
		/// </summary>
		/// <returns></returns>
		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int stocktakingID = ZConvert.StrToInt(Request["stocktakingID"]);
			int isProfitAndLoss = ZConvert.StrToInt(Request["isProfitAndLoss"]);
			int isAbnormal = ZConvert.StrToInt(Request["isAbnormal"]);

			string whereSql = "s.StocktakingID = " + stocktakingID;

			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and s.ProductsName like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and s.ProductsCode like '%{0}%'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and s.ProductsNo like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and s.ProductsSkuCode like '%{0}%'", keyWord);
						break;
				}
			}
			if (isProfitAndLoss == 1) {
				whereSql += " and (s.IsImport = 1 and s.PdNum - p.ZkNum <> 0 and PdNum >= p.ZyNum + p.DjNum)";
			}
			if (isAbnormal == 1) {
				whereSql += " and (s.IsImport = 1 and s.PdNum < p.ZyNum + p.DjNum)";
			}
			return whereSql;
		}

		/// <summary>
		/// 获取盈亏数量
		/// </summary>
		/// <returns></returns>
		public int GetProfitAndLossCount() {
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = @"warehouseStocktakingItem s INNER JOIN warehouselocationproducts p on s.SourceID = p.ID";
			data.Select = "s.ID";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = 0;
			data.PagingItemsPerPage = 0;
			int total = 0;
			List<WarehouseStocktakingItem> list = BaseService<WarehouseStocktakingItem>.GetQueryManyForPage(data, out total);
			return total;
		}

		/// <summary>
		/// 获取异常数量
		/// </summary>
		/// <returns></returns>
		public int GetAbnormalCount() {
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = @"warehouseStocktakingItem s INNER JOIN warehouselocationproducts p on s.SourceID = p.ID";
			data.Select = "s.ID";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = 0;
			data.PagingItemsPerPage = 0;
			int total = 0;
			List<WarehouseStocktakingItem> list = BaseService<WarehouseStocktakingItem>.GetQueryManyForPage(data, out total);
			return total;
		}

		/// <summary>
		/// 查看异常原因
		/// </summary>
		/// <param name="stocktakingItemID"></param>
		/// <returns></returns>
		public ActionResult showAbnormalInfo(int stocktakingItemID) {
			WarehouseStocktakingItem stocktakingItem = WarehouseStocktakingItemService.GetSingleUnconfirmed(stocktakingItemID);
			if (stocktakingItem == null) {
				stocktakingItem = new WarehouseStocktakingItem();
			}
			ViewBag.StocktakingItem = stocktakingItem;
			return View();
		}

		/// <summary>
		/// 查询异常原因
		/// </summary>
		/// <param name="productsSkuID"></param>
		/// <returns></returns>
		public ActionResult SearchAbnormalInfo(int locationID, int productsSkuID) {
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = @"(
                          SELECT w.BillNo,1 AS BillType,w.Status,i.Num FROM warehouseoutbound w INNER JOIN warehouseOutboundPickItem i ON w.ID = i.OutboundID WHERE w.WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "' AND Status < " + (int)WarehouseOutboundStatus.已发货 + " AND i.ProductsSkuID = " + productsSkuID + " AND i.LocationID = "+locationID+@"
						  UNION ALL
						  SELECT w.BillNo,2 AS BillType,w.Status,i.ProductsNum AS Num FROM warehouseOutInStock w INNER JOIN warehouseOutInStockItem i ON w.ID = i.OutInStockID WHERE w.WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "' AND w.Status = " + (int)WarehouseOutInStockStatus.未提交 + " AND i.ProductsSkuID = " + productsSkuID + " AND i.LocationID = " + locationID + @"
						  UNION ALL
						  SELECT w.BillNo,3 AS BillType,w.Status,i.Num FROM warehouseMoveLocation w INNER JOIN warehouseMoveLocationItem i ON w.ID = i.MoveLocationID WHERE w.WarehouseCode ='" + FormsAuth.GetWarehouseCode() + "' AND w.Status = " + (int)MoveLocationStatus.未确认 + " AND i.ProductsSkuID = " + productsSkuID + " AND i.OutLocationID = " + locationID + @"
                          ) AS T";
			data.Select = "t.BillNo,t.BillType,t.Status,t.Num";
			data.WhereSql = " IFNULL(BillNo,'') <> ''";
			data.PagingCurrentPage = 0;
			data.PagingItemsPerPage = 0;
			int total = 0;
			List<StocktakingcsAbnormal> list = BaseService<StocktakingcsAbnormal>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				switch (item.BillType) {
					case 1:
						item.BillTypeName = "销售出库单";
						item.StatusName = Enum.GetName(typeof(WarehouseOutboundStatus), item.Status);
						break;
					case 2:
						item.BillTypeName = "出库单";
						item.StatusName = Enum.GetName(typeof(WarehouseOutInStockStatus), item.Status);
						break;
					case 3:
						item.BillTypeName = "移位单";
						item.StatusName = Enum.GetName(typeof(MoveLocationStatus), item.Status);
						break;
				}
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#region 导出盘点表格

		/// <summary>
		/// 初始化参数
		/// </summary>
		public string Export() {
			string fileName, fileMapPath, downTaskId;
			downTaskId = NewKey.guid();
			fileName = "盘点明细表格(" + NewKey.datetime() + ")";
			fileMapPath = System.Web.HttpContext.Current.Server.MapPath("../../Down/" + fileName + "");

			string strSql = GetWhereSql();
			string json = "";
			IDictionary<string, string> dicts = new Dictionary<string, string>();
			dicts.Add("fileName", fileName);
			dicts.Add("fileMapPath", fileMapPath);
			dicts.Add("downTaskId", downTaskId);
			dicts.Add("filter", strSql);
			dicts.Add("reportName", "盘点明细表格");

			Common.RunAsyn(obj => { ExportTask((IDictionary<string, string>)obj); }, dicts);
			json = Newtonsoft.Json.JsonConvert.SerializeObject(dicts.Where(dic => { return dic.Key != "filter" && dic.Key != "reportName"; }).ToDictionary(data => data.Key, data => data.Value));
			return json;
		}

		protected void ExportTask(IDictionary<string, string> dicts) {
			string fileName = dicts["fileName"];
			string fileMapPath = dicts["fileMapPath"];
			string downTaskId = dicts["downTaskId"];
			string filter = dicts["filter"];
			string reportName = dicts["reportName"];

			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					string format = "LocationCode|*库位编码;ProductsName|商品名称;ProductsNo|商品货号;ProductsCode|商品编码;ProductsSkuSaleprop|属性;ProductsSkuCode|*商品SKU码;ProductsBatchCode|*批次;CostPrice|成本单价;ZkNum|系统库存;PdNum|*盘点库存";
					SelectBuilder data = new SelectBuilder();
					data.Having = "";
					data.GroupBy = "";
					data.OrderBy = "s.ID DESC";
					data.From = @"warehouseStocktakingItem s INNER JOIN warehouselocationproducts p on s.SourceID = p.ID";
					data.Select = "s.LocationCode,s.ProductsName,s.ProductsNo,s.ProductsCode,s.ProductsSkuSaleprop,s.ProductsSkuCode,s.ProductsBatchCode,0 CostPrice,p.ZkNum,s.PdNum,s.ProductsBatchID";
					data.WhereSql = filter;
					data.PagingCurrentPage = 0;
					data.PagingItemsPerPage = 0;
					DataTable exportTable = WarehouseStocktakingItemService.GetDataTableForPage(data, context);
					foreach (DataRow dr in exportTable.Rows) {
						dr["CostPrice"] = WarehouseProductsBatchService.GetQuerySingleByID(ZConvert.StrToInt(dr["ProductsBatchID"]), context).CostPrice;
						dr["PdNum"] = "0";
						PaiXie.Utils.Export.add_Down_Task_Progress(downTaskId);
					}

					if (exportTable.Rows.Count > 60000) {
						fileName += ".csv";
						fileMapPath += ".csv";
					}
					else {
						fileMapPath += ".xls";
						fileName += ".xls";
					}

					Excel.ExcelHelp.exportMin.GenerateXlsFormat(format, fileMapPath, exportTable, reportName); GC.Collect();
				}
			}
			catch (Exception ex) {
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "导出盘点表格", FormsAuth.GetUserCode());
			}
		}

		#endregion

		#region 导入盘点结果

		public ActionResult Import(int stocktakingID) {
			ViewBag.StocktakingID = stocktakingID;
			return View();
		}

		private string[] arrAllowedFiles = new string[] { "xls", "xlsx", "csv" };
		public ActionResult ImportStocktakingItem(int stocktakingID) {
			BaseResult resultInfo = new BaseResult();
			WarehouseStocktaking stocktaking = WarehouseStocktakingService.GetQuerySingleByID(stocktakingID);
			if (stocktaking != null) {
				if (stocktaking.Status == (int)StocktakingStatus.未确认) {
					string fileUrl = string.Empty;
					if (Request.Files != null && Request.Files.Count > 0) {
						var file = Request.Files[0];
						string fileSuffix = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace(".", "");
						if (file.ContentLength <= 1024 * 1024 * 20) {
							if (arrAllowedFiles.Contains(fileSuffix.ToLower())) {
								fileUrl = FileUploader.Upload(file, "StocktakingItem");
								DataTable dt = null;
								try {
									dt = ExcelHelp.importMin.Import(Server.MapPath(fileUrl), 0);
									dt = ZDataSet.removeEmpty(dt);
									int columnsCount = dt.Columns.Count;
									int rowsCount = dt.Rows.Count;
									if (columnsCount >= 10) {
										using (IDbContext context = Db.GetInstance().Context()) {
											context.UseTransaction(true);
											for (int i = 0; i < rowsCount; i++) {
												int rowNumber = i;

												#region 获取表格内容并校验

												string locationCode = dt.Rows[i]["*库位编码"].ToString();
												if (locationCode == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行库位编码未填写！";
													break;
												}

												string productsSkuCode = dt.Rows[i]["*商品SKU码"].ToString();
												if (productsSkuCode == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行商品SKU码未填写！";
													break;
												}
												if (productsSkuCode.Length > 50) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行商品SKU码长度超过了50位！";
													break;
												}

												string productsBatchCode = dt.Rows[i]["*批次"].ToString();
												if (productsBatchCode == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行批次未填写！";
													break;
												}

												string pdNum = dt.Rows[i]["*盘点库存"].ToString();
												if (pdNum == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行盘点库存未填写！";
													break;
												}
												if (!(ZConvert.StrToInt(pdNum) >= 0 && ZConvert.StrToInt(pdNum) <= 9999999)) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行盘点库存填写不正确，必须是大于或等于0且小于9999999的整数！";
													break;
												}

												string kcNum = dt.Rows[i]["系统库存"].ToString();

												#endregion

												#region 更新盘点库存或添加盘点商品

												WarehouseStocktakingItem stocktakingItem = WarehouseStocktakingItemService.GetSingleWarehouseStocktakingItem(stocktakingID, locationCode, productsBatchCode, productsSkuCode, context);
												if (stocktakingItem != null) {
													#region 更新盘点库存

													stocktakingItem.PdNum = ZConvert.StrToInt(pdNum);
													stocktakingItem.IsImport = 1;
													stocktakingItem.UpdatePerson = FormsAuth.GetUserName();
													stocktakingItem.UpdateDate = DateTime.Now;
													int rowsAffected = WarehouseStocktakingItemService.Update(stocktakingItem, context);
													if (rowsAffected == 0) {
														resultInfo.result = 0;
														resultInfo.message = "第" + rowNumber + "行更新盘点库存失败！";
														break;
													}

													#endregion
												}
												else {
													#region 添加盘点商品

													WarehouseProductsSkuInfo warehouseProductsSkuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(FormsAuth.GetWarehouseCode(),productsSkuCode,context);
													if (warehouseProductsSkuInfo == null) {
														resultInfo.result = 0;
														resultInfo.message = "第" + rowNumber + "行商品SKU码不存在！";
														break;
													}

													WarehouseLocation warehouseLocation = WarehouseLocationService.GetSingleWarehouseLocation(FormsAuth.GetWarehouseCode(), locationCode, context);
													if (warehouseLocation == null) {
														resultInfo.result = 0;
														resultInfo.message = "第" + rowNumber + "行库位编码不存在！";
														break;
													}
													else {
														if (!System.Text.RegularExpressions.Regex.IsMatch(stocktaking.LocationID, "\\b" + warehouseLocation.ParentID + "\\b")) {
															resultInfo.result = 0;
															resultInfo.message = "第" + rowNumber + "行库位编码不在本次盘点库区里！";
															break;
														}
													}

													stocktakingItem = new WarehouseStocktakingItem();
													stocktakingItem.StocktakingID = stocktaking.ID;
													stocktakingItem.BillNo = stocktaking.BillNo;
													stocktakingItem.WarehouseCode = FormsAuth.GetWarehouseCode();
													stocktakingItem.TopLocationID = warehouseLocation.ParentID;
													stocktakingItem.LocationID = warehouseLocation.ID;
													stocktakingItem.LocationCode = warehouseLocation.Code;
													stocktakingItem.LocationName = warehouseLocation.Name;
													stocktakingItem.LocationTypeID = warehouseLocation.TypeID;
													stocktakingItem.ProductsID = warehouseProductsSkuInfo.ProductsID;
													stocktakingItem.ProductsCode = warehouseProductsSkuInfo.ProductsCode;
													stocktakingItem.ProductsName = warehouseProductsSkuInfo.ProductsName;
													stocktakingItem.ProductsNo = warehouseProductsSkuInfo.ProductsNo;
													stocktakingItem.ProductsSkuID = warehouseProductsSkuInfo.ID;
													stocktakingItem.ProductsSkuCode = warehouseProductsSkuInfo.Code;
													stocktakingItem.ProductsSkuSaleprop = warehouseProductsSkuInfo.Saleprop;
													stocktakingItem.ProductsBatchCode = productsBatchCode;
													stocktakingItem.ZkNum = 0;
													stocktakingItem.KyNum = 0;
													stocktakingItem.PdNum = ZConvert.StrToInt(pdNum);
													stocktakingItem.IsImport = 1;
													stocktakingItem.CreatePerson = FormsAuth.GetUserName();
													stocktakingItem.CreateDate = DateTime.Now;
													stocktakingItem.UpdatePerson = FormsAuth.GetUserName();
													stocktakingItem.UpdateDate = DateTime.Now;
													int stocktakingItemID = WarehouseStocktakingItemService.Add(stocktakingItem, context);
													if (stocktakingItemID == 0) {
														resultInfo.result = 0;
														resultInfo.message = "第" + rowNumber + "行添加盘点商品失败！";
														break;
													}

													#endregion
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
								}
								catch (Exception ex) {
									resultInfo.result = 0;
									resultInfo.message = "导入的数据格式不正确！";
									PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "盘点记录" + stocktaking.BillNo + " 导入盘点结果", FormsAuth.GetUserCode());
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "只能导入 .xls.xlsx或者.csv类型的文件！";
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
					resultInfo.message = "未确认的盘点记录才可以导入！";
				}
			}
			else {
				resultInfo.result = 0;
				resultInfo.message = "盘点记录不存在或已被删除！";
			}

			var result = new { result = resultInfo.result, message = resultInfo.message };
			return JsonDate(result);
		}

		#endregion

	}
}
