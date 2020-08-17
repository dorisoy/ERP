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
    public class SuppliersItemController : BaseController
    {
		#region Index

		public ActionResult Index(int suppliersID = 0) {
			Suppliers suppliers = SuppliersService.GetQuerySingleByID(suppliersID);
			ViewBag.Suppliers = suppliers;
			ViewBag.ProductsCount = SuppliersItemService.GetProductsCount(suppliersID);
			return View();
		}

		#endregion

		#region 供应商商品列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "suppi.ProductsID,suppi.ProductsCode,suppi.ProductsName";
			data.OrderBy = "suppi.ID DESC";
			data.From = @"suppliersItem suppi";
			data.Select = @"suppi.ProductsID,suppi.ProductsCode,suppi.ProductsName,suppi.ProductsNo,MIN(suppi.PurchasePrice) AS MinPurchasePrice,MAX(suppi.PurchasePrice) AS MaxPurchasePrice,MAX(suppi.ArrivalCycle) AS ArrivalCycle";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<SuppliersItemList> list = BaseService<SuppliersItemList>.GetQueryManyForPage(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);

		}

		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int suppliersID = ZConvert.StrToInt(Request["suppliersID"]);
			string whereSql = string.Format("suppi.SuppliersID={0}", suppliersID);

			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and suppi.ProductsName like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and suppi.ProductsCode like '%{0}%'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and suppi.ProductsNo like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and suppi.ProductsSkuCode like '%{0}%'", keyWord);
						break;
				}
			}
			return whereSql;
		}

		#endregion

		#region 供应商商品编辑

		public ActionResult Edit(int suppliersID, int productsID) {
			PaiXie.Data.Products products = new Data.Products();
			string arrivalCycle = string.Empty;
			if (productsID > 0) {
				products = ProductsService.GetSingleProducts(productsID);
				arrivalCycle = SuppliersItemService.GetArrivalCycle(suppliersID, productsID);
			}
			ViewBag.SuppliersID = suppliersID;
			ViewBag.Products = products;
			ViewBag.IsEdit = productsID > 0 ? 1 : 0;
			ViewBag.ArrivalCycle = arrivalCycle;
			return View();
		}

		#endregion

		#region 根据商品编码搜索商品信息

		public ActionResult SearchProducts(string code, int suppliersID) {
			code = code.Trim();
			BaseResult resultInfo = new BaseResult();
			string name = string.Empty;
			string unit = string.Empty;
			string no = string.Empty;
			decimal weight = 0;
			int shelfLife = 0;
			int id = 0;
			PaiXie.Data.Products products = ProductsService.GetSingleProducts(code);
			if (products == null) {
				resultInfo.result = 0;
				resultInfo.message = "商品不存在！";
			}
			else {
				if ((products.SaleType & (int)SaleType.物料) > 0) {
					name = products.Name;
					no = products.No;
					code = products.Code;
					id = products.ID;
					weight = products.Weight;
					shelfLife = products.ShelfLife;
					Syscode sysCode = SyscodeService.GetSyscodeByCode(products.MeasurementUnitID);
					if (sysCode != null) {
						unit = sysCode.Text;
					}
				}
				else {
					resultInfo.result = 0;
					resultInfo.message = "该商品不是物料不能直接采购！";
				}
			}
			var result = new { result = resultInfo.result, message = resultInfo.message, Name = name, Code = code, No = no, ID = id, Weight = weight, ShelfLife = shelfLife, Unit = unit };
			return JsonDate(result);
		}

		#endregion

		#region 根据商品ID搜索商品SKU

		public ActionResult SearchProductsSku(int productsID, int suppliersID) {
			SelectBuilder data = new SelectBuilder();
			string whereSql = string.Format("ps.ProductsID={0}", productsID);
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "ps.ID DESC";
			data.From = @"productsSku ps INNER JOIN products p ON ps.ProductsID=p.ID
			LEFT JOIN (SELECT ProductsSkuID,PurchasePrice,ArrivalCycle FROM suppliersItem WHERE SuppliersID=" + suppliersID + @") AS suppi ON ps.ID=suppi.ProductsSkuID";
			data.Select = @"ps.ID as ProductsSkuID,ps.Saleprop AS ProductsSkuSaleprop,ps.Code AS ProductsSkuCode,suppi.ArrivalCycle,
			CASE WHEN suppi.PurchasePrice>0 THEN suppi.PurchasePrice 
			WHEN ps.CostPrice>0 THEN ps.CostPrice 
			ELSE p.CostPrice END AS PurchasePrice";
			data.WhereSql = whereSql;
			int total = 0;
			List<SuppliersSkuList> list = SuppliersItemService.GetQueryManyForSkuList(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 保存

		public ActionResult Save(SuppliersItemWebInfo objWebInfo) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = SuppliersManager.AddSuppliersItem(userCode, objWebInfo);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 导入商品

		public ActionResult Import(int suppliersID) {
			ViewBag.SuppliersID = suppliersID;
			return View();
		}

		private string[] arrAllowedFiles = new string[] { "xls", "xlsx" };
		public ActionResult ImportSuppliersItem(int suppliersID) {
			BaseResult resultInfo = new BaseResult();
			Suppliers suppliers = SuppliersService.GetQuerySingleByID(suppliersID);
			if (suppliers != null) {
				string userCode = FormsAuth.GetUserCode();
				string fileUrl = string.Empty;
				if (Request.Files != null && Request.Files.Count > 0) {
					var file = Request.Files[0];
					string fileSuffix = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace(".", "");
					if (file.ContentLength <= 1024 * 1024 * 20) {
						if (arrAllowedFiles.Contains(fileSuffix.ToLower())) {
							fileUrl = FileUploader.Upload(file, "SuppliersItem");
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
											string purchasePrice = dt.Rows[i]["*采购价"].ToString();
											if (purchasePrice == "") {
												resultInfo.result = 0;
												resultInfo.message = "第" + rowNumber + "行采购价未填写！";
												break;
											}
											if (!(ZConvert.StrToDecimal(purchasePrice) > 0 && ZConvert.StrToInt(purchasePrice) <= 9999999.999)) {
												resultInfo.result = 0;
												resultInfo.message = "第" + rowNumber + "行采购价填写不正确，必须是大于0且小于9999999.999的数字！";
												break;
											}
											int arrivalCycle = ZConvert.StrToInt(dt.Rows[i]["到货周期(天)"]);

											ProductsSkuInfo skuInfo = ProductsSkuService.GetSingleProductsSkuInfo(productsSkuCode, context);
											if (skuInfo == null) {
												resultInfo.result = 0;
												resultInfo.message = "第" + rowNumber + "行商品SKU码 " + productsSkuCode + " 不存在！";
												break;
											}

											PaiXie.Data.Products products = ProductsService.GetSingleProducts(skuInfo.ProductsID, context);
											if ((products.SaleType & (int)SaleType.物料) <= 0) {
												resultInfo.result = 0;
												resultInfo.message = "第" + rowNumber + "行商品SKU码 " + productsSkuCode + " 不是物料不能直接采购！";
												break;
											}
											#endregion

											#region 不存在就添加，否则更新

											SuppliersItem entity = SuppliersItemService.GetSingleSuppliersItem(skuInfo.ID, suppliersID, context);
											if (entity == null) {
												entity = new SuppliersItem();
												entity.SuppliersID = suppliersID;
												entity.ProductsID = skuInfo.ProductsID;
												entity.ProductsName = skuInfo.ProductsName;
												entity.ProductsNo = skuInfo.ProductsNo;
												entity.ProductsCode = skuInfo.ProductsCode;
												entity.ProductsSkuID = skuInfo.ID;
												entity.ProductsSkuCode = productsSkuCode;
												entity.ProductsSkuSaleprop = skuInfo.Saleprop;
												entity.PurchasePrice = ZConvert.StrToDecimal(purchasePrice);
												entity.ArrivalCycle = arrivalCycle;
												entity.CreatePerson = userCode;
												entity.CreateDate = DateTime.Now;
												int suppliersItemID = 0;
												try {
													suppliersItemID = SuppliersItemService.Add(entity, context);
												}
												catch (Exception ex) {
													PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "供应商 " + suppliers.Name + " 导入商品", userCode);
												}
												if (suppliersItemID == 0) {
													resultInfo.result = 0;
													resultInfo.message = "第" + rowNumber + "行添加失败！";
													break;
												}
											}
											else {
												entity.PurchasePrice = ZConvert.StrToDecimal(purchasePrice);
												entity.ArrivalCycle = arrivalCycle;
												entity.UpdatePerson = userCode;
												entity.UpdateDate = DateTime.Now;
												try {
													int count = SuppliersItemService.Update(entity, context);
													if (count == 0) {
														resultInfo.result = 0;
														resultInfo.message = "第" + rowNumber + "行更新采购价和到货周期失败！";
														break;
													}
												}
												catch (Exception ex) {
													PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "供应商 " + suppliers.Name + " 导入商品", userCode);
												}
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
								PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "供应商 " + suppliers.Name + " 导入商品", userCode);
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
				resultInfo.message = "供应商不存在或已被删除！";
			}
			//   构造成Json的格式传递
			var result = new { result = resultInfo.result, message = resultInfo.message };
			return JsonDate(result);
		}

		#endregion

		#region 删除商品

		public ActionResult Delete(string productsIDs) {
			string userCode=FormsAuth.GetUserCode();
			List<int> productsIDList = new List<int>();
			productsIDList.AddRange(productsIDs.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = SuppliersManager.DeleteSuppliersItem(userCode, productsIDList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 获取商品数量

		public ActionResult GetProductsCount(int suppliersID) {
			int productsCount = SuppliersItemService.GetProductsCount(suppliersID);
			var result = new { productsCount = productsCount };
			return JsonDate(result);
		}

		#endregion
	}
}
