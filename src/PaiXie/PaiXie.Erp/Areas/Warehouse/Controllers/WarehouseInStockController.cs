#region using
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Api.Bll;
using PaiXie.Utils;
using FluentData;
using System.Data;
using PaiXie.Excel;
#endregion
namespace PaiXie.Erp.Areas.Warehouse {
	public class WarehouseInStockController : BaseController {

		#region index
		//
		// GET: /Warehouse/WarehouseInStock/
		public ActionResult index() {
			return View();
		}
		#endregion

		#region  入库单商品
		//
		// GET: /Warehouse/WarehouseInStock/
		public ActionResult InStockProducts(string BillNo, int outInStockID) {
			ViewBag.BillNo = BillNo;
			ViewBag.OutInStockID = outInStockID;
			ViewBag.SourceNo = WarehouseOutInStockService.GetModelByBillNo(BillNo).SourceNo;
			ViewBag.ProductsNum = WarehouseOutInStockItemService.GetProductsNumByOutInStockID(outInStockID);
			int  status = WarehouseOutInStockService.GetQuerySingleByID(outInStockID).Status;
			if (status != (int)WarehouseOutInStockStatus.未提交)
				ViewBag.del = "1";
			return View();
		}
		//  /Warehouse/WarehouseInStock/getProductsNum?BillNo=11
		public ContentResult getProductsNum(string  BillNo="") {
			string ProductsNum = WarehouseOutInStockItemService.GetSumProductsNum(BillNo);
				return Content(ZConvert.StrToInt(ProductsNum, 0).ToString()) ;

		}
		#endregion

		#region  添加商品
		//
		// GET: /Warehouse/WarehouseInStock/AddPro

		public ActionResult AddPro(string BillNo) {
			ViewBag.BillNo = BillNo;
			return View();
		}
		#endregion

		#region  sku搜索
		///Warehouse/WarehouseInStock/SkuSearch?skucode=
		public ActionResult SkuSearch(string Code = "", string BillNo="") {
			SkuSearchList result = new SkuSearchList();
			result = OutInStockManager.SkuSearch(Code.Trim(), BillNo.Trim());
			return JsonDate(result);
		}

		#endregion

		#region 添加入库单
		/// <summary>
		/// 添加入库单
		/// </summary>
		/// <returns></returns>
		public ActionResult AddInStock() {
			return View();
		}
		#endregion

		#region 保存入库单
		[HttpPost]
		public ActionResult SaveInStock(WarehouseOutInStock obj) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutInStockManager.SaveOutInStock(userCode, warehouseCode, obj);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 入库单列表
		public ActionResult search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "id desc";
			data.From = @"warehouseOutInStock  a";
			data.Select = "	a.*,'' AS BillTypeName,0 AS OutInStockNum,'' AS StatusName";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseOutInStockList> list = BaseService<WarehouseOutInStockList>.GetQueryManyForPage(data, out total, null, null);
			//   构造成Json的格式传递
			for (int i = 0; i < list.Count(); i++) {
				//入库单类型
				list[i].BillTypeName = BillTypeConvert.GetBillTypeName(list[i].BillType);
				//入库数量  
				int  cc = WarehouseOutInStockItemService.GetProductsNumByOutInStockID(list[i].ID);
					list[i].OutInStockNum =cc;
				//状态名称
				list[i].StatusName = ((WarehouseOutInStockStatus)list[i].Status).ToString();
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string state = Request["state"];
			string whereSql = "a.WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "' and a.BillType IN (" + (int)BillType.CGR + "," + (int)BillType.QTR + ")";
			if (keyWord != "") {
				switch (keyWordType) {
					case "入库单号":
						whereSql += string.Format(" AND a.BillNo like '%{0}%'", keyWord);
						break;
					case "入库单备注":
						whereSql += string.Format(" AND a.Remark like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format("  AND a.ID IN (SELECT OutInStockID FROM warehouseOutInStockItem WHERE ProductsCode like '%{0}%')", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format("  AND a.ID IN (SELECT OutInStockID FROM warehouseOutInStockItem WHERE ProductsSkuCode like '%{0}%')", keyWord);
						break;
				}
			}
			if (!string.IsNullOrEmpty(state)) {
				whereSql += " AND a.STATUS IN (" + state.Substring(0, state.Length - 1) + ")";
			}
			return whereSql;
		}

		#endregion

		#region sku 库存 数量
		// /Warehouse/WarehouseInStock/SKUStockNum
		public ActionResult SKUStockNum(string skucode, int kqid) {

			int total = 0;
			List<SKUStockNumList> list = OutInStockManager.SKUStockNum(skucode, kqid, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 入库单  保存商品
		// // /Warehouse/WarehouseInStock/PutSKUStockSave

		public ActionResult PutSKUStockSave(PutSKUStock obj) {
			BaseResult BaseResult = new BaseResult();
			try {
				BaseResult = OutInStockManager.PutSKUStockSave(obj);
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
			}
			return JsonDate(BaseResult);
		}
		#endregion

		#region 入库单  商品列表

		public ActionResult InStockProsearch(string BillNo) {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = " OutInStockBillNo='" + BillNo + "' ";
			whereSql += GetWhereSqlstr();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = @"warehouseOutInStockItem  a  LEFT JOIN warehouseLocation b ON a.LocationID=b.ID";
			data.Select = "	a.*,0 AS PurchaseNum,0 AS AlreadyNum,'' as Locationcode ";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<Storagelist> list = BaseService<Storagelist>.GetQueryManyForPage(data, out total);
			for (int i = 0; i < list.Count(); i++) {
				//采购数量
				WarehousePurchaseItem WarehousePurchaseItem=WarehousePurchaseItemService.GetQuerySingleByID(list[i].SourceItemID);
				int num = WarehousePurchaseItem!=null?WarehousePurchaseItem.Num:0;
					list[i].PurchaseNum = num;
				//已经入库数量  其他库位的入库数量
					string AlreadyNum = WarehouseOutInStockItemService.GetOtherProductsNum(list[i].ProductsSkuID, list[i].OutInStockID, list[i].LocationID);
					list[i].AlreadyNum = ZConvert.StrToInt(AlreadyNum, 0);
				//库位编码
					WarehouseLocation Locationcode = WarehouseLocationService.GetQuerySingleByID(list[i].LocationID);
			list[i].Locationcode = Locationcode!=null?Locationcode.Code:"";
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}
		private string GetWhereSqlstr() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string whereSql = "";
			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and a.ProductsName like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and a.ProductsCode like '%{0}%'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and a.ProductsNo like '%{0}%'", keyWord);
						break;			
					case "商品SKU码":
						whereSql += string.Format(" and a.ProductsSkuCode like '%{0}%'", keyWord);
						break;				
					case "库位编码":
						whereSql += string.Format(" and b.Code like '%{0}%'", keyWord);
						break;
				}
			}
			return whereSql;
		}

		#endregion

		#region 入库单 商品 单条修改

		public ActionResult rowsave(int id, decimal costPrice, string productionDate, string locationcode, int productsNum) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutInStockManager.rowsave(userCode, warehouseCode, id, costPrice, productionDate, locationcode, productsNum);
			return JsonDate(resultInfo);
		}
		#endregion

		#region 入库单 商品 修改生产日期
		// // /Warehouse/WarehouseInStock/PutSKUStockSave

		public ActionResult Updatescdate(string ids, string scdate) {
			BaseResult BaseResult = new BaseResult();
			BaseResult = OutInStockManager.Updatescdate(ids, scdate);
			return JsonDate(BaseResult);
		}
		#endregion

		#region 入库单 商品 修改库位编码
		// // /Warehouse/WarehouseInStock/PutSKUStockSave
		public ActionResult UpdateLocation(string ids, string Library) {
			BaseResult BaseResult = new BaseResult();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult = OutInStockManager.UpdateLocation(ids, warehouseCode, Library);
			return JsonDate(BaseResult);
		}
		#endregion

		#region 检查采购单号是否存在且状态不为未确认和已结束
		/// <summary>
		/// 检查采购单号是否存在且状态不为未确认和已结束
		/// </summary>
		/// <param name="billNo">采购单号</param>
		/// <returns></returns>
		public ActionResult CheckSourceNo(string billNo) {
			BaseResult resultInfo = new BaseResult();
			WarehousePurchase obj = WarehousePurchaseService.GetQuerySingleByBillNo(billNo);
			if (obj == null) {
				resultInfo.result = 0;
				resultInfo.message = "采购单号不存在！";
			}
			else {
				if (obj.Status == (int)PurchaseStatus.未确认) {
					resultInfo.result = 0;
					resultInfo.message = "采购单号未确认！";
				}
				else if (obj.Status == (int)PurchaseStatus.已结束) {
					resultInfo.result = 0;
					resultInfo.message = "采购单号已结束！";
				}
			}
			return JsonDate(resultInfo);
		}
		#endregion

		#region 删除入库单
		/// <summary>
		/// 删除入库单
		/// </summary>
		/// <param name="ids">入库单主键ID  多个以半角逗号隔开</param>
		/// <returns></returns>
		public ActionResult DelInStock(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = OutInStockManager.DelOutInStock(userCode, idList);
			return JsonDate(resultInfo);
		}
		#endregion

		#region 入库单 提交

		public ActionResult tj(int id) {
			BaseResult BaseResult = new BaseResult();
			BaseResult = OutInStockManager.tj(id);
			return JsonDate(BaseResult);
		}
		#endregion

		#region 删除入库单 商品

		public ActionResult DelwarehouseOutInStockItem(int outInStockID, string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = OutInStockManager.DelOutInStockItem(userCode, outInStockID, idList);
			return JsonDate(resultInfo);
		}
		#endregion

		#region 导入商品

		public ActionResult Import(int outInStockID) {
			ViewBag.OutInStockID = outInStockID;
			return View();
		}

		private string[] arrAllowedFiles = new string[] { "xls", "xlsx" };
		public ActionResult ImportInStock(int outInStockID) {
			BaseResult resultInfo = new BaseResult();
			string userCode = FormsAuth.GetUserCode();
			WarehouseOutInStock inStock = WarehouseOutInStockService.GetQuerySingleByID(outInStockID);
			if (inStock != null) {
				if (inStock.Status == (int)WarehouseOutInStockStatus.未提交) {
					string fileUrl = string.Empty;
					if (Request.Files != null && Request.Files.Count > 0) {
						var file = Request.Files[0];
						string fileSuffix = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace(".", "");
						if (file.ContentLength <= 1024 * 1024 * 20) {
							if (arrAllowedFiles.Contains(fileSuffix.ToLower())) {
								fileUrl = FileUploader.Upload(file, "instock");
								DataTable dt = null;
								try {
									dt = ExcelHelp.importMin.Import(Server.MapPath(fileUrl), 1);
									dt = ZDataSet.removeEmpty(dt);
									int columnsCount = dt.Columns.Count;
									int rowsCount = dt.Rows.Count;
									if (columnsCount >= 8) {
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
												if (productsSkuCode.Length > 50) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行商品SKU码长度超过了50位！";
													break;
												}
												WarehouseProductsSkuInfo skuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(FormsAuth.GetWarehouseCode(), productsSkuCode, context);
												if (skuInfo == null) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行商品SKU码 " + productsSkuCode + " 不存在！";
													break;
												}
												#endregion

												#region *采购价
												string price = dt.Rows[i]["*采购价"].ToString();
												if (price == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行采购价未填写！";
													break;
												}
												if (ZConvert.StrToDecimal(price, 0) == 0) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行采购价填写不正确，必须大于0";
													break;
												}
												#endregion

												#region *入库数量

												string num = dt.Rows[i]["*入库数量"].ToString();
												if (num == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行入库数量未填写！";
													break;
												}
												if (!(ZConvert.StrToInt(num) > 0 && ZConvert.StrToInt(num) <= 9999999)) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行入库数量填写不正确，必须是大于0且小于9999999的整数！";
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
												WarehouseLocation WarehouseLocation = WarehouseLocationService.GetSingleWarehouseLocation(inStock.WarehouseCode,lcode,context);
												//排除掉库区
												if (WarehouseLocation == null || WarehouseLocation.ParentID == 0) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行库位编码不存在";
													break;
												}
												#endregion


												#endregion

												WarehouseOutInStockItem WarehouseOutInStockItem = WarehouseOutInStockItemService.GetWarehouseOutInStockItem(productsSkuCode, WarehouseLocation.ID, inStock.ID, context);
												
												if (WarehouseOutInStockItem == null) {
                                                    #region 入库单商品 不存在 添加
												WarehouseOutInStockItem objWarehouseOutInStockItem = new WarehouseOutInStockItem();
												objWarehouseOutInStockItem.OutInStockID = inStock.ID;
												objWarehouseOutInStockItem.OutInStockBillNo = inStock.BillNo;
												objWarehouseOutInStockItem.BillType = inStock.BillType;
												objWarehouseOutInStockItem.SourceID = inStock.SourceID;
												objWarehouseOutInStockItem.SourceNo = inStock.SourceNo;
												objWarehouseOutInStockItem.StockWay = (int)StockWay.入库;
												objWarehouseOutInStockItem.WarehouseCode = inStock.WarehouseCode;
												objWarehouseOutInStockItem.Status = (int)WarehouseOutInStockStatus.未提交;
												objWarehouseOutInStockItem.ProductsID = skuInfo.ProductsID;
												objWarehouseOutInStockItem.ProductsCode = skuInfo.ProductsCode;
												objWarehouseOutInStockItem.ProductsName = skuInfo.ProductsName;
												objWarehouseOutInStockItem.ProductsNo = skuInfo.ProductsNo;
												objWarehouseOutInStockItem.ProductsSkuID = skuInfo.ID;
												objWarehouseOutInStockItem.ProductsSkuCode = skuInfo.Code;
												objWarehouseOutInStockItem.ProductsSkuSaleprop = skuInfo.Saleprop;
												objWarehouseOutInStockItem.ProductsNum = ZConvert.StrToInt(num, 0);
												objWarehouseOutInStockItem.LocationID = WarehouseLocation.ID;
												objWarehouseOutInStockItem.ProductionDate = ZConvert.StrToDateTime(dt.Rows[i]["生产日期"].ToString(), DateTime.Now);
												objWarehouseOutInStockItem.CostPrice = ZConvert.StrToDecimal(price);
												objWarehouseOutInStockItem.CreateDate = DateTime.Now;
												objWarehouseOutInStockItem.CreatePerson = userCode;
												int outid = WarehouseOutInStockItemService.Add(objWarehouseOutInStockItem, context);
												if (outid == 0) {
													resultInfo.result = 0;
													resultInfo.message = "导入商品失败！";
													break;
												}
												#endregion
												}
												else {
													#region 入库单商品 存在 更新
													WarehouseOutInStockItem.OutInStockID = inStock.ID;
													WarehouseOutInStockItem.OutInStockBillNo = inStock.BillNo;
													WarehouseOutInStockItem.BillType = inStock.BillType;
													WarehouseOutInStockItem.SourceID = inStock.SourceID;
													WarehouseOutInStockItem.SourceNo = inStock.SourceNo;
													WarehouseOutInStockItem.StockWay = (int)StockWay.入库;
													WarehouseOutInStockItem.WarehouseCode = inStock.WarehouseCode;
													WarehouseOutInStockItem.Status = (int)WarehouseOutInStockStatus.未提交;
													WarehouseOutInStockItem.ProductsID = skuInfo.ProductsID;
													WarehouseOutInStockItem.ProductsCode = skuInfo.ProductsCode;
													WarehouseOutInStockItem.ProductsName = skuInfo.ProductsName;
													WarehouseOutInStockItem.ProductsNo = skuInfo.ProductsNo;
													WarehouseOutInStockItem.ProductsSkuID = skuInfo.ID;
													WarehouseOutInStockItem.ProductsSkuCode = skuInfo.Code;
													WarehouseOutInStockItem.ProductsSkuSaleprop = skuInfo.Saleprop;
													WarehouseOutInStockItem.ProductsNum = ZConvert.StrToInt(num, 0);
													WarehouseOutInStockItem.LocationID = WarehouseLocation.ID;
													WarehouseOutInStockItem.ProductionDate = ZConvert.StrToDateTime(dt.Rows[i]["生产日期"].ToString(), DateTime.Now);
													WarehouseOutInStockItem.CostPrice = ZConvert.StrToDecimal(price);
													WarehouseOutInStockItem.CreateDate = DateTime.Now;
													WarehouseOutInStockItem.CreatePerson = userCode;
													int outid = WarehouseOutInStockItemService.Update(WarehouseOutInStockItem, context);
													if (outid == 0) {
														resultInfo.result = 0;
														resultInfo.message = "导入商品失败！";
														break;
													}
													#endregion
												}
												
											}
											if (resultInfo.result == 0) {
												context.Rollback();
											}
											else {
												resultInfo = OutInStockManager.UpdatePurchase(outInStockID, context);
												if (resultInfo.result > 0) {
													context.Commit();
												}
												else {
													context.Rollback();
												}

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
									PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "入库单 " + inStock.BillNo + " 导入商品", userCode);
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
					resultInfo.message = "未提交的入库单才可以导入！";
				}
			}
			else {
				resultInfo.result = 0;
				resultInfo.message = "入库单不存在或已被删除！";
			}
			//   构造成Json的格式传递
			var result = new { result = resultInfo.result, message = resultInfo.message };
			return JsonDate(result);
		}

		#endregion

	}
}