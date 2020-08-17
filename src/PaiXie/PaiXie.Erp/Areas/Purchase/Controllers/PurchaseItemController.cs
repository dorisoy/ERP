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

namespace PaiXie.Erp.Areas.Purchase
{
    public class PurchaseItemController : BaseController
    {
		#region Index

		public ActionResult Index(string aliasName, int purchaseID) {
			WarehousePurchase warehousePurchase = WarehousePurchaseService.GetQuerySingleByID(purchaseID);
			ViewBag.AliasName = aliasName;
			ViewBag.WarehousePurchase = warehousePurchase;
			ViewBag.ExpectedAmount = PurchaseManager.GetExpectedAmount(purchaseID, warehousePurchase.SuppliersID);
			return View();
		}

		#endregion

		#region 采购单商品列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			int suppliersID=ZConvert.StrToInt(Request["suppliersID"]);
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wpi.ID DESC";
			data.From = @"warehousePurchaseItem wpi";
			data.Select = "wpi.ID,wpi.ProductsCode,wpi.ProductsName,wpi.ProductsSkuID,wpi.ProductsSkuSaleprop,wpi.ProductsSkuCode,0 AS KyNum,wpi.Num,wpi.InStockNum,0 AS ExpectedAmount";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehousePurchaseItemList> list = BaseService<WarehousePurchaseItemList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				item.KyNum = ProductsSkuService.GetKfhNumByProductsSkuID(item.ProductsSkuID);
				decimal price = SuppliersManager.GetPurchasePrice(item.ProductsSkuID, suppliersID);
				item.ExpectedAmount = item.Num * price;
			}
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);

		}

		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int purchaseID = ZConvert.StrToInt(Request["purchaseID"]);
			string whereSql = string.Format("wpi.purchaseID={0}", purchaseID);
			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and wpi.ProductsName like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and wpi.ProductsCode like '%{0}%'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and wpi.ProductsNo like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and wpi.ProductsSkuCode like '%{0}%'", keyWord);
						break;
				}
			}
			return whereSql;
		}

		#endregion

		#region 添加商品

		public ActionResult Add(string warehouseCode, string billNo, int purchaseID, int suppliersID) {
			ViewBag.WarehouseCode = warehouseCode;
			ViewBag.BillNo = billNo;
			ViewBag.PurchaseID = purchaseID;
			ViewBag.SuppliersID = suppliersID;
			return View();
		}

		#endregion

		#region 根据商品编码搜索商品信息

		public ActionResult SearchProducts(string warehouseCode, string code, int suppliersID) {
			code = code.Trim();
			BaseResult resultInfo = new BaseResult();
			string name = string.Empty;
			string no = string.Empty;
			int id = 0;
			WarehouseProductsInfo warehouseProductsInfo = WarehouseProductsService.GetSingleWarehouseProductsInfo(warehouseCode, code);
			if (warehouseProductsInfo == null) {
				resultInfo.result = 0;
				resultInfo.message = "对应仓库没有此商品！";
			}
			else {
				//检查商品是否关联当前供应商
				List<SuppliersItemInfo> suppliersItemInfoList = SuppliersManager.GetSuppliersByProductsID(warehouseProductsInfo.ID);
				if (suppliersItemInfoList.Count > 0) {
					int count = suppliersItemInfoList.Where(x => x.SuppliersID == suppliersID).Count();
					if (count > 0) {
						name = warehouseProductsInfo.Name;
						no = warehouseProductsInfo.No;
						code = warehouseProductsInfo.Code;
						id = warehouseProductsInfo.ID;
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "商品未关联当前采购单的供应商！";
					}
				}
				else {
					resultInfo.result = 0;
					resultInfo.message = "商品未关联供应商！";
				}
			}
			var result = new { result = resultInfo.result, message = resultInfo.message, Name = name, Code = code, No = no, ID = id };
			return JsonDate(result);
		}

		#endregion

		#region 根据商品ID搜索商品SKU

		public ActionResult SearchProductsSku(string warehouseCode, int productsID, int suppliersID) {
			SelectBuilder data = new SelectBuilder();
			string whereSql = string.Format("wps.WarehouseCode='{0}' AND wps.ProductsID={1}", warehouseCode, productsID);
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "ps.ID,wps.ID DESC";
			data.From = @"warehouseProductsSku wps
  LEFT JOIN productsSku ps ON wps.ProductsSkuID=ps.ID";
			data.Select = @"wps.ProductsSkuID,ps.Saleprop AS ProductsSkuSaleprop,ps.Code AS ProductsSkuCode,0 AS KyNum,
(SELECT PurchasePrice FROM suppliersItem WHERE ID=" + suppliersID + @" AND ProductsSkuID=wps.ProductsSkuID) AS PurchasePrice";
			data.WhereSql = whereSql;
			int total = 0;
			List<WarehousePurchaseSkuList> list = WarehousePurchaseItemService.GetQueryManyForSkuList(data, out total);
			foreach (var item in list) {
				item.KyNum = ProductsSkuService.GetKfhNumByProductsSkuID(item.ProductsSkuID);
			}
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 保存商品

		public ActionResult Save(string warehouseCode, WarehousePurchaseItemWebInfo obj) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = PurchaseManager.AddPurchaseItem(userCode, warehouseCode, obj);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 导入商品

		public ActionResult Import(int purchaseID, string billNo, string warehouseCode) {
			ViewBag.PurchaseID = purchaseID;
			ViewBag.BillNo = billNo;
			ViewBag.WarehouseCode = warehouseCode;
			return View();
		}

		private string[] arrAllowedFiles = new string[] { "xls", "xlsx" };
		public ActionResult ImportPurchaseItem(int purchaseID, string billNo, string warehouseCode) {
			BaseResult resultInfo = new BaseResult();
			WarehousePurchase purchase = WarehousePurchaseService.GetQuerySingleByID(purchaseID);
			if (purchase != null) {
				if (purchase.Status == (int)PurchaseStatus.未确认) {
					string userCode = FormsAuth.GetUserCode();
					string fileUrl = string.Empty;
					if (Request.Files != null && Request.Files.Count > 0) {
						var file = Request.Files[0];
						string fileSuffix = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace(".", "");
						if (file.ContentLength <= 1024 * 1024 * 20) {
							if (arrAllowedFiles.Contains(fileSuffix.ToLower())) {
								fileUrl = FileUploader.Upload(file, "PurchasePlanItem");
								bool needUpdatePlan = false;
								DataTable dt = null;
								try {
									dt = ExcelHelp.importMin.Import(Server.MapPath(fileUrl), 1);
									dt = ZDataSet.removeEmpty(dt);
									int columnsCount = dt.Columns.Count;
									int rowsCount = dt.Rows.Count;
									if (columnsCount >= 5) {
										using (IDbContext context = Db.GetInstance().Context()) {
											context.UseTransaction(true);
											for (int i = 0; i < rowsCount; i++) {
												int rowNumber = i + 3;
												#region 获取表格内容并校验
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
												string num = dt.Rows[i]["*采购数量"].ToString();
												if (num == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行采购数量未填写！";
													break;
												}
												if (!(ZConvert.StrToInt(num) > 0 && ZConvert.StrToInt(num) <= 9999999)) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行采购数量填写不正确，必须是大于0且小于9999999的整数！";
													break;
												}

												WarehouseProductsSkuInfo skuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(warehouseCode, productsSkuCode, context);
												if (skuInfo == null) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行商品SKU码 " + productsSkuCode + " 不存在！";
													break;
												}
												//查询SKU和供应商是否关联
												SuppliersItem suppliersItem = SuppliersItemService.GetSingleSuppliersItem(skuInfo.ID, purchase.SuppliersID, context);
												if (suppliersItem == null) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行商品SKU码 " + productsSkuCode + "与采购单供应商未关联！";
													break;
												}

												#endregion

												#region 不存在就添加，否则更新

												WarehousePurchaseItem entity = WarehousePurchaseItemService.GetSingleWarehousePurchaseItem(purchaseID, skuInfo.ID, context);
												if (entity == null) {
													entity = new WarehousePurchaseItem();
													entity.WarehouseCode = warehouseCode;
													entity.PurchaseID = purchaseID;
													entity.BillNo = billNo;
													entity.ProductsID = skuInfo.ProductsID;
													entity.ProductsName = skuInfo.ProductsName;
													entity.ProductsNo = skuInfo.ProductsNo;
													entity.ProductsCode = skuInfo.ProductsCode;
													entity.ProductsSkuID = skuInfo.ID;
													entity.ProductsSkuCode = productsSkuCode;
													entity.ProductsSkuSaleprop = skuInfo.Saleprop;
													entity.Num = ZConvert.StrToInt(num);
													entity.CreatePerson = userCode;
													entity.CreateDate = DateTime.Now;
													int purchaseItemID = 0;
													try {
														purchaseItemID = WarehousePurchaseItemService.Add(entity, context);
													}
													catch (Exception ex) {
														PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "采购单 " + billNo + " 导入商品", userCode);
													}
													if (purchaseItemID == 0) {
														resultInfo.result = 0;
														resultInfo.message = "第" + rowNumber + "行添加失败！";
														break;
													}
												}
												else {
													int diffNum = ZConvert.StrToInt(num) - entity.Num;
													entity.Num = ZConvert.StrToInt(num);
													entity.UpdatePerson = userCode;
													entity.UpdateDate = DateTime.Now;
													int count = 0;
													try {
														count = WarehousePurchaseItemService.Update(entity, context);
													}
													catch (Exception ex) {
														PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "采购单 " + billNo + " 导入商品", userCode);
													}
													if (count > 0) {
														if (entity.PlanItemID > 0 && diffNum != 0) {
															needUpdatePlan = true;
															//更新采购计划商品的已采购数量
															count = WarehousePurchasePlanItemService.UpdatePurchasedNum(userCode, entity.PlanItemID, diffNum, context);
															if (count == 0) {
																resultInfo.result = 0;
																resultInfo.message = "第" + rowNumber + "行更新计划商品的采购数量失败！";
																break;
															}
														}
													}
													else {
														resultInfo.result = 0;
														resultInfo.message = "第" + rowNumber + "行更新采购数量失败！";
														break;
													}
												}

												#endregion
											}
											if (resultInfo.result == 1) {
												int count = WarehousePurchaseService.UpdateNum(userCode, purchaseID, context);
												if (count > 0) {
													if (needUpdatePlan) {
														//更新采购计划单的已采购数量
														count = WarehousePurchasePlanService.UpdatePurchasedNum(userCode, purchase.PlanID, context);
														if (count == 0) {
															resultInfo.result = 0;
															resultInfo.message = "更新采购计划单 " + purchase.PlanBillNo + " 的已采购数量失败！";
														}
													}
												}
												else {
													resultInfo.result = 0;
													resultInfo.message = "更新采购单 " + billNo + " 的采购数量失败！";
												}
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
									PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "采购单 " + billNo + " 导入商品", userCode);
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
					resultInfo.message = "未确认的采购单才可以导入！";
				}
			}
			else {
				resultInfo.result = 0;
				resultInfo.message = "采购单不存在或已被删除！";
			}
			//   构造成Json的格式传递
			var result = new { result = resultInfo.result, message = resultInfo.message };
			return JsonDate(result);
		}		

		#endregion

		#region 删除商品

		public ActionResult Delete(int purchaseID, string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> purchaseItemIDList = new List<int>();
			purchaseItemIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = PurchaseManager.DelPurchaseItem(userCode, purchaseItemIDList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 获取采购数量、预计金额、入库数量

		public ActionResult GetNum(int purchaseID) {
			int num = 0;
			int inStockNum = 0;
			decimal expectedAmount = 0;
			WarehousePurchase warehousePurchase = WarehousePurchaseService.GetQuerySingleByID(purchaseID);
			if (warehousePurchase != null) {
				num = warehousePurchase.Num;
				inStockNum = warehousePurchase.InStockNum;
				expectedAmount = PurchaseManager.GetExpectedAmount(purchaseID, warehousePurchase.SuppliersID);
			}
			var result = new { num = num, inStockNum = inStockNum, expectedAmount = expectedAmount };
			return JsonDate(result);
		}

		#endregion

		#region 修改采购数量

		public ActionResult UpdateNum(int purchaseID, int purchaseItemID, int num, int productsSkuID, int suppliersID) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = PurchaseManager.UpdateNum(userCode, purchaseID, purchaseItemID, num);
			decimal price = SuppliersManager.GetPurchasePrice(productsSkuID, suppliersID);
			var result = new { result = resultInfo.result, message = resultInfo.message, price = price };
			return JsonDate(result);
		}

		#endregion
	}
}
