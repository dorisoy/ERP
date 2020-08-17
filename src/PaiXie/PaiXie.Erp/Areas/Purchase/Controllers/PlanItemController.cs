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
using FluentData;
using System.IO;
namespace PaiXie.Erp.Areas.Purchase
{
    public class PlanItemController : BaseController
    {
		#region Index

		public ActionResult Index(string warehouseName, int planID) {
			WarehousePurchasePlan warehousePurchasePlan = WarehousePurchasePlanService.GetSingleWarehousePurchasePlan(planID);
			ViewBag.WarehousePurchasePlan = warehousePurchasePlan;
			ViewBag.WarehouseName = warehouseName;
			return View();
		}

		#endregion

		#region 采购计划单商品列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wppi.ID DESC";
			data.From = @"warehousePurchasePlanItem wppi 
LEFT JOIN suppliers sup ON wppi.SuppliersID=sup.ID";
			data.Select = "wppi.ID,wppi.ProductsCode,wppi.ProductsName,wppi.ProductsSkuID,wppi.ProductsSkuSaleprop,wppi.ProductsSkuCode,0 AS KyNum,wppi.Num,wppi.PurchasedNum,sup.AliasName";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehousePurchasePlanItemList> list = BaseService<WarehousePurchasePlanItemList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				item.KyNum = ProductsSkuService.GetKfhNumByProductsSkuID(item.ProductsSkuID);
			}
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);

		}

		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int noSupplier = ZConvert.StrToInt(Request["noSupplier"]);
			int planID = ZConvert.StrToInt(Request["planID"]);
			string whereSql = string.Format("wppi.planID={0}", planID);
			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and wppi.ProductsName like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and wppi.ProductsCode like '%{0}%'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and wppi.ProductsNo like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and wppi.ProductsSkuCode like '%{0}%'", keyWord);
						break;
				}
			}
			if (noSupplier == 1) {
				whereSql += string.Format(" and wppi.SuppliersID=0");
			}
			return whereSql;
		}

		#endregion

		#region 添加商品

		public ActionResult Add(string warehouseCode, string billNo, int planID) {
			ViewBag.WarehouseCode = warehouseCode;
			ViewBag.BillNo = billNo;
			ViewBag.PlanID = planID;
			return View();
		}
		
		#endregion

		#region 根据商品编码搜索商品信息

		public ActionResult SearchProducts(string warehouseCode, string code) {
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
				//检查商品是否关联供应商
				List<SuppliersItemInfo> suppliersItemInfoList = SuppliersManager.GetSuppliersByProductsID(warehouseProductsInfo.ID);
				if (suppliersItemInfoList.Count > 0) {
					name = warehouseProductsInfo.Name;
					no = warehouseProductsInfo.No;
					code = warehouseProductsInfo.Code;
					id = warehouseProductsInfo.ID;
				}
				else {
					resultInfo.result = 0;
					resultInfo.message = "请先给商品关联供应商！";
				}
			}
			var result = new { result = resultInfo.result, message = resultInfo.message, Name = name, Code = code, No = no, ID = id };
			return JsonDate(result);
		}

		#endregion

		#region 根据商品ID搜索商品SKU

		public ActionResult SearchProductsSku(string warehouseCode, int productsID) {
			SelectBuilder data = new SelectBuilder();
			string whereSql = string.Format("wps.WarehouseCode='{0}' AND wps.ProductsID={1}", warehouseCode, productsID);
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "ps.ID,wps.ID DESC";
			data.From = @"warehouseProductsSku wps
  LEFT JOIN productsSku ps ON wps.ProductsSkuID=ps.ID";
			data.Select = "wps.ProductsSkuID,ps.Saleprop AS ProductsSkuSaleprop,ps.Code AS ProductsSkuCode,0 AS KyNum";
			data.WhereSql = whereSql;
			int total = 0;
			List<WarehousePurchasePlanSkuList> list = WarehousePurchasePlanItemService.GetQueryManyForSkuList(data, out total);
			foreach (var item in list) {
				item.KyNum = ProductsSkuService.GetKfhNumByProductsSkuID(item.ProductsSkuID);
				List<SuppliersItemInfo> suppliersItemInfoList = SuppliersManager.GetSuppliersByProductsSkuID(item.ProductsSkuID);
				item.SuppliersItemInfoList.AddRange(suppliersItemInfoList);
			}
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 保存商品

		public ActionResult Save(string warehouseCode, WarehousePurchasePlanItemWebInfo obj) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = PurchaseManager.AddPlanItem(userCode, warehouseCode, obj);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 导入商品

		public ActionResult Import(int planID, string billNo, string warehouseCode) {
			ViewBag.PlanID = planID;
			ViewBag.BillNo = billNo;
			ViewBag.WarehouseCode = warehouseCode;
			return View();
		}

		private string[] arrAllowedFiles = new string[] { "xls", "xlsx" };
		public ActionResult ImportPlanItem(int planID, string billNo, string warehouseCode) {
			BaseResult resultInfo = new BaseResult();
			WarehousePurchasePlan purchasePlan = WarehousePurchasePlanService.GetSingleWarehousePurchasePlan(planID);
			if (purchasePlan != null) {
				if (purchasePlan.Status == (int)PurchasePlanStatus.已提交 && purchasePlan.PurchaseOrderCount == 0) {
					string userCode = FormsAuth.GetUserCode();
					string fileUrl = string.Empty;
					if (Request.Files != null && Request.Files.Count > 0) {
						var file = Request.Files[0];
						string fileSuffix = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace(".", "");
						if (file.ContentLength <= 1024 * 1024 * 20) {
							if (arrAllowedFiles.Contains(fileSuffix.ToLower())) {
								fileUrl = FileUploader.Upload(file, "PurchasePlanItem");

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
												string num = dt.Rows[i]["*计划采购数量"].ToString();
												if (num == "") {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行计划采购数量未填写！";
													break;
												}
												if (!(ZConvert.StrToInt(num) > 0 && ZConvert.StrToInt(num) <= 9999999)) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行计划采购数量填写不正确，必须是大于0且小于9999999的整数！";
													break;
												}

												WarehouseProductsSkuInfo skuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(warehouseCode, productsSkuCode, context);
												if (skuInfo == null) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行商品SKU码 " + productsSkuCode + " 不存在！";
													break;
												}
												int suppliersID = 0;
												string suppliersName = dt.Rows[i]["供应商名称"].ToString();
												if (suppliersName.Trim() == "") {
													suppliersID = SuppliersItemService.GetDefaultSuppliersID(skuInfo.ID, context);
												}
												else {
													//优先读取简称，读不到再读取供应商名称
													Suppliers suppliers = SuppliersService.GetQuerySingleByAliasName(suppliersName);
													if (suppliers == null) {
														suppliers = SuppliersService.GetQuerySingleByName(suppliersName);
														if (suppliers == null) {
															resultInfo.result = 0;
															resultInfo.message = "第" + rowNumber + "行供应商名称 " + suppliersName + " 不存在！";
															break;
														}
														else {
															suppliersID = suppliers.ID;
														}
													}
													else {
														suppliersID = suppliers.ID;
													}
													//查询SKU和供应商是否关联
													SuppliersItem suppliersItem = SuppliersItemService.GetSingleSuppliersItem(skuInfo.ID, suppliersID, context);
													if (suppliersItem == null) {
														resultInfo.result = 0;
														resultInfo.message = "第" + rowNumber + "行商品SKU码 " + productsSkuCode + "与供应商 " + suppliersName + " 未关联！";
														break;
													}
												}

												#endregion

												#region 不存在就添加，否则更新计划数量和供应商
												WarehousePurchasePlanItem entity = WarehousePurchasePlanItemService.GetSingleWarehousePurchasePlanItem(planID, skuInfo.ID, context);
												if (entity == null) {
													entity = new WarehousePurchasePlanItem();
													entity.WarehouseCode = warehouseCode;
													entity.PlanID = planID;
													entity.BillNo = billNo;
													entity.ProductsID = skuInfo.ProductsID;
													entity.ProductsName = skuInfo.ProductsName;
													entity.ProductsNo = skuInfo.ProductsNo;
													entity.ProductsCode = skuInfo.ProductsCode;
													entity.ProductsSkuID = skuInfo.ID;
													entity.ProductsSkuCode = productsSkuCode;
													entity.ProductsSkuSaleprop = skuInfo.Saleprop;
													entity.Num = ZConvert.StrToInt(num);
													entity.SuppliersID = suppliersID;
													entity.CreatePerson = userCode;
													entity.CreateDate = DateTime.Now;
													int planItemID = 0;
													try {
														planItemID = WarehousePurchasePlanItemService.Add(entity, context);
													}
													catch (Exception ex) {
														PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "计划单 " + billNo + " 导入商品", userCode);
													}
													if (planItemID == 0) {
														resultInfo.result = 0;
														resultInfo.message = "第" + rowNumber + "行添加失败！";
														break;
													}
												}
												else {

													entity.Num = ZConvert.StrToInt(num);
													entity.SuppliersID = suppliersID;
													entity.UpdatePerson = userCode;
													entity.UpdateDate = DateTime.Now;
													int count = 0;
													try {
														count = WarehousePurchasePlanItemService.Update(entity, context);
													}
													catch (Exception ex) {
														PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "计划单 " + billNo + " 导入商品", userCode);
													}
													if (count == 0) {
														resultInfo.result = 0;
														resultInfo.message = "第" + rowNumber + "行更新计划数量失败！";
														break;
													}
												}

												#endregion
											}
											if (resultInfo.result == 1) {
												int count = WarehousePurchasePlanService.UpdateNum(userCode, planID, context);
												if (count == 0) {
													resultInfo.result = 0;
													resultInfo.message = "更新采购计划单 " + billNo + " 的计划数量失败！";
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
									PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "计划单 " + billNo + " 导入商品", userCode);
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
					resultInfo.message = "未采购的计划单才可以导入！";
				}
			}
			else {
				resultInfo.result = 0;
				resultInfo.message = "计划单不存在或已被删除！";
			}
			//   构造成Json的格式传递
			var result = new { result = resultInfo.result, message = resultInfo.message};
			return JsonDate(result);
		}

		#endregion

		#region 按照采购计划单商品生成采购单

		public ActionResult Generation(string ids) {
			List<int> idList = new List<int>();
			if (ids == "") {
				//如果没有选择商品，则默认当前查询条件的所有商品
				string whereSql = " WHERE " + GetWhereSql();
				string sqlStr = "SELECT wppi.ID FROM warehousePurchasePlanItem wppi" + whereSql;
				DataTable dt = ShopProductsService.GetDataTable(sqlStr);
				foreach (DataRow dr in dt.Rows) {
					idList.Add(ZConvert.StrToInt(dr["ID"]));
				}
			}
			else {
				idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			}
			string userCode = FormsAuth.GetUserCode();
			int purchaseOrderCount = 0;
			BaseResult resultInfo = PurchaseManager.Generation(userCode, idList, out purchaseOrderCount);
			var result = new { result = resultInfo.result, message = resultInfo.message, purchaseOrderCount = purchaseOrderCount };
			return JsonDate(result);
		}

		#endregion

		#region 删除商品

		public ActionResult Delete(int planID, string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> planItemIDList = new List<int>();
			planItemIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = PurchaseManager.DelPlanItem(userCode, (int)ProjectType.管理端, planID, planItemIDList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 设置供应商

		public ActionResult EditPlanItemSupplier(int planItemID, int productsSkuID) {
			ViewBag.ProductsSkuID = productsSkuID;
			ViewBag.PlanItemID = planItemID;
			return View();
		}

		#endregion

		#region 根据商品SKUID搜索供应商列表

		public ActionResult SearchSuppliers(int productsSkuID) {
			List<SuppliersItemInfo> suppliersItemInfoList = SuppliersManager.GetSuppliersByProductsSkuID(productsSkuID);
			return JsonDate(suppliersItemInfoList);
		}

		#endregion

		#region 保存供应商

		public ActionResult UpdateSuppliersID(int planItemID, int suppliersID, int productsSkuID, int isDefault) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = PurchaseManager.UpdateSuppliersID(userCode, planItemID, suppliersID, productsSkuID, isDefault);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 获取计划采购数量

		public ActionResult GetNum(int planID) {
			int num = 0;
			WarehousePurchasePlan warehousePurchasePlan = WarehousePurchasePlanService.GetSingleWarehousePurchasePlan(planID);
			if (warehousePurchasePlan != null) {
				num = warehousePurchasePlan.Num;
			}
			var result = new { num = num };
			return JsonDate(result);
		}

		#endregion
	}
}
