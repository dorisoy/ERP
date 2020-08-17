#region using
using PaiXie.Data;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Service;
using PaiXie.Core;
using PaiXie.Api.Bll;
using System.IO;
using System.Data;
using PaiXie.Excel;

#endregion

namespace PaiXie.Erp.Areas.Products
{
    public class ProductsController : BaseController
    {
		#region Index

		/// <summary>
		/// Index
		/// </summary>
		/// <param name="status">商品状态 1销售中 2仓库中</param>
		/// <returns></returns>
		public ActionResult Index(int status = 1) {
			ViewBag.Status = status;
			return View();
		}

		#endregion

		#region 商品列表

		/// <summary>
		/// 商品列表 默认销售中
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {			
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "p.ID DESC";
			data.From = @"products p 
LEFT JOIN category c ON p.CategoryID=c.ID 
LEFT JOIN brand b ON p.BrandID=b.ID";
			data.Select = "p.ID,p.Code,p.SmallPic,p.SaleType,p.Name,c.Name AS CategoryName,b.name AS BrandName,p.SellingPrice,p.CostPrice,0 AS Num,p.CreateDate";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<ProductsList> list = BaseService<ProductsList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				item.Num = ProductsService.GetKfhNumByProductsID(item.ID);
			}
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		private string GetWhereSql() {
			int status = ZConvert.StrToInt(Request["status"], (int)ProductsStatus.销售中);
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int categoryID = ZConvert.StrToInt(Request["categoryID"]);
			int brandID = ZConvert.StrToInt(Request["brandID"]);
			string whereSql = "";
			//库存分配 
			if (!string.IsNullOrEmpty(Request["kc"])) {
				whereSql += " p.IsDelete=" + (int)IsEnable.否;//不区分状态
			}
			else {
				whereSql += string.Format("p.Status={0} AND p.IsDelete={1}", status, (int)IsEnable.否);
			}
			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and p.Name like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and p.Code like '%{0}%'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and p.No like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and p.ID in(Select ProductsID From productsSku Where Code like '%{0}%')", keyWord);
						break;
					case "商品条码":
						whereSql += string.Format(" and p.BarCode like '%{0}%'", keyWord);
						break;
				}
			}
			if (categoryID > 0) {
				whereSql += string.Format(" and p.CategoryID={0}", categoryID);
			}
			if (brandID > 0) {
				whereSql += string.Format(" and p.BrandID={0}", brandID);
			}
			return whereSql;
		}

		#endregion

		#region 商品编辑

		public ActionResult Edit(int id, int status) {
			ProductsInfo objProductsInfo = new ProductsInfo();
			if (id > 0) {
				objProductsInfo = ProductsManager.GetProductsInfo(id);
			}
			else {
				Data.Products products = new Data.Products();
				objProductsInfo.Products = products;
				objProductsInfo.ProductsSkuList = new List<ProductsSku>();
			}
			objProductsInfo.Products.CategoryID = objProductsInfo.Products.CategoryID == 0 ? -1 : objProductsInfo.Products.CategoryID;
			ViewBag.ProductsInfo = objProductsInfo;
			ViewBag.Status = status;
			return View();
		}

		#endregion

		#region 上传小图

		[HttpPost]
		public string Upload(FormCollection fc) {
			string newFileName = string.Empty;
			//判断Request中是否有接收Files文件
			if (Request.Files.Count != 0) {
				//HttpPostedFileBase类，提供对用户上载的单独文件的访问
				//获取到用户上传的文件
				HttpPostedFileBase file = Request.Files[0];

				//获取用户上传文件的后缀名
				string Extension = Path.GetExtension(file.FileName);

				//重新命名文件
				newFileName = Guid.NewGuid().ToString() + Extension;

				//利用file.SaveAs保存图片
				string name = Path.Combine(Server.MapPath("/Upload/Products/"), newFileName);
				file.SaveAs(name);
			}
			return "../../Upload/Products/" + newFileName;
		}

		#endregion

		#region 表格导入

		public ActionResult Import(int status) {
			ProductsImportErrorService.Delete();
			ViewBag.Status = status;
			return View();
		}

		private string[] arrAllowedFiles = new string[] { "xls", "xlsx" };
		public ActionResult ImportProducts(bool isUpdate = false) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = new BaseResult();
			int successCount = 0;
			int errorCount = 0;
			string fileUrl = string.Empty;
			if (Request.Files != null && Request.Files.Count > 0) {
				var file = Request.Files[0];
				string fileSuffix = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace(".", "");
				if (file.ContentLength <= 1024 * 1024 * 20) {
					if (arrAllowedFiles.Contains(fileSuffix.ToLower())) {
						fileUrl = FileUploader.Upload(file, "Products");

						DataTable dt = null;
						try {
							dt = ExcelHelp.importMin.Import(Server.MapPath(fileUrl), 1);
							dt = ZDataSet.removeEmpty(dt);
							int columnsCount = dt.Columns.Count;
							int rowsCount = dt.Rows.Count;
							if (columnsCount >= 14) {
								for (int i = 0; i < rowsCount; i++) {
									#region 获取表格内容并校验
									bool hasError = false;
									List<string> errorMessage = new List<string>();
									string proTitle = dt.Rows[i]["*商品名称"].ToString();
									if (proTitle == "") {
										hasError = true;
										errorMessage.Add("商品名称未填写！");
									}
									string proNo = dt.Rows[i]["*商品货号"].ToString();
									if (proNo == "") {
										hasError = true;
										errorMessage.Add("商品货号未填写！");
									}
									string proCode = dt.Rows[i]["*商品编码"].ToString();
									if (proCode == "") {
										hasError = true;
										errorMessage.Add("商品编码未填写！");
									}
									string proBarCode = dt.Rows[i]["商品条码"].ToString();
									string costPrice = dt.Rows[i]["*成本价"].ToString();
									if (costPrice == "") {
										hasError = true;
										errorMessage.Add("成本价未填写！");
									}
									decimal sellingPrice = ZConvert.StrToDecimal(dt.Rows[i]["销售价"]);
									string taxRate = dt.Rows[i]["*税率"].ToString();
									if (taxRate == "") {
										hasError = true;
										errorMessage.Add("税率未填写！");
									}
									decimal proWeight = ZConvert.StrToDecimal(dt.Rows[i]["重量"]);
									int shelfLife = ZConvert.StrToInt(dt.Rows[i]["保质期"]);
									string saleprop = dt.Rows[i]["*商品属性"].ToString();
									string skuCode = dt.Rows[i]["*商品SKU码"].ToString();
									string skuBarCode = dt.Rows[i]["商品SKU条码"].ToString();
									string skuCostPrice = dt.Rows[i]["*SKU成本价"].ToString();
									decimal skuSellingPrice = ZConvert.StrToDecimal(dt.Rows[i]["SKU销售价"]);
									if (saleprop != "" || skuCode != "" || skuCostPrice !="") {
										if (saleprop == "") {
											hasError = true;
											errorMessage.Add("商品属性未填写！");
										}
										if (skuCode == "") {
											hasError = true;
											errorMessage.Add("商品SKU码未填写！");
										}
										if (skuCostPrice == "") {
											hasError = true;
											errorMessage.Add("SKU成本价未填写！");
										}
									}
									else {
										//没有SKU就以商品编码做为商品SKU码
										skuCode = proCode;
										if (saleprop == "") saleprop = "无";
									}
									#endregion
									if (!hasError) {
										ProductsInfo productsInfo = new ProductsInfo();
										Data.Products products = new Data.Products();
										products.BarCode = proBarCode;
										products.Code = proCode;
										products.CostPrice = ZConvert.StrToDecimal(costPrice);
										products.SellingPrice = sellingPrice;
										products.Name = proTitle;
										products.No = proNo;
										products.SaleType = (int)SaleType.销售 + (int)SaleType.物料;
										products.Status = (int)ProductsStatus.仓库中;
										products.ShelfLife = shelfLife;
										try{
											products.TaxRate = ZConvert.StrToDecimal(taxRate.Split('%')[0]) / 100;
										}catch(Exception ex){
											PlanLog.WriteLog(ex.ToString(), LogType.General.ToString());
										}
										products.Weight = proWeight;
										productsInfo.Products = products;
										List<ProductsSku> productsSkuList = new List<ProductsSku>();
										ProductsSku productsSku = new ProductsSku();
										productsSku.ProductsCode = proCode;
										productsSku.BarCode = skuBarCode;
										productsSku.Code = skuCode;
										productsSku.CostPrice = ZConvert.StrToDecimal(skuCostPrice);
										productsSku.SellingPrice = skuSellingPrice;
										productsSku.Saleprop = saleprop;
										productsSkuList.Add(productsSku);
										productsInfo.ProductsSkuList = productsSkuList;
										string position = "Products/ProductsController/ImportProducts";
										string buttonName = "导入商品";
										string target = "商品列表";
										bool isImport = true;
										BaseResult currentResultInfo = ProductsManager.Save(userCode, position, target, buttonName, productsInfo, isUpdate, isImport);
										if (currentResultInfo.result == 1) {
											successCount++;
										}
										else {
											errorCount++;
											ProductsManager.SaveImportError(userCode, proCode, proTitle, saleprop, skuCode, currentResultInfo.message);
										}
									}
									else {
										errorCount++;
										ProductsManager.SaveImportError(userCode, proCode, proTitle, saleprop, skuCode, string.Join(",", errorMessage.ToArray()));
									}
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "导入的模版列数不正确！";
							}
						}
						catch(Exception ex) {
							resultInfo.result = 0;
							resultInfo.message = "导入的数据格式不正确！";
							PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "表格导入商品", userCode);
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "只能上传 .xls或.xlsx类型的文件！";
					}
				}else{
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

		/// <summary>
		/// 导入失败商品列表
		/// </summary>
		/// <returns></returns>
		public ActionResult ImportError() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = "productsImportError";
			data.Select = "ID, ProductsCode, ProductsTitle, Saleprop, ProductsSkuCode, ErrorMessage, CreateDate";
			data.WhereSql = "";
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<ProductsImportError> list = ProductsImportErrorService.GetQueryManyForPageList(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion
		
		#region 更换分类

		public ActionResult ChangeCategory(string ids) {
			string[] arrId = ids.Split(',');
			if (arrId.Length == 1) {
				Data.Products products = ProductsService.GetSingleProducts(ZConvert.StrToInt(ids));
				ViewBag.CategoryID = products.CategoryID == 0 ? -1 : products.CategoryID;
			}
			else {
				ViewBag.CategoryID = -1;
			}
			ViewBag.productsIDs = ids;
			ViewBag.productsCount = arrId.Length;
			return View();
		}

		#endregion

		#region 保存商品分类
		[HttpPost]
		public ActionResult SaveCategory(string ids, int categoryID) {
			categoryID = categoryID == -1 ? 0 : categoryID;
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = ProductsManager.ChangeCategory(userCode, idList, categoryID);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 更换品牌

		public ActionResult ChangeBrand(string ids) {
			string[] arrId = ids.Split(',');
			if (arrId.Length == 1) {
				Data.Products products = ProductsService.GetSingleProducts(ZConvert.StrToInt(ids));
				ViewBag.BrandID = products.BrandID;
			}
			else {
				ViewBag.BrandID = 0;
			}
			ViewBag.productsIDs = ids;
			ViewBag.productsCount = arrId.Length;
			return View();
		}

		#endregion

		#region 保存商品品牌
		[HttpPost]
		public ActionResult SaveBrand(string ids, int brandID) {
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = ProductsManager.ChangeBrand(userCode, idList, brandID);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 导出

		public ActionResult Export(string ids) {
			return View();
		}

		#endregion

		#region 上架

		public ActionResult OnSale(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = ProductsManager.OnSale(userCode, idList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 下架

		public ActionResult OffSale(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = ProductsManager.OffSale(userCode, idList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 保存

		[HttpPost]
		[ValidateInput(false)] 
		public ActionResult Save(ProductsWebInfo objWeb) {
			#region 将前端商品信息实体类赋值给后端所需要的实体
			Data.Products products = new Data.Products();
			products.ID = objWeb.ProID;
			products.BarCode = objWeb.ProBarCode;
			products.Code = objWeb.ProCode;
			products.CostPrice = objWeb.ProCostPrice;
			products.SellingPrice = objWeb.ProSellingPrice;
			products.Name = objWeb.Name;
			products.No = objWeb.No;
			products.MeasurementUnitID = objWeb.MeasurementUnitID;
			products.Remark = objWeb.Remark;
			products.ShelfLife = objWeb.ShelfLife;
			if (objWeb.UrlType == 1) {
				if (objWeb.PicUrl != "") {
					products.SmallPic = objWeb.PicUrl;
				}
			}
			else {
				if (objWeb.SmallPic != null) {
					products.SmallPic = string.Join(",", objWeb.SmallPic);
				}
			}
			products.Status = (int)ProductsStatus.仓库中;
			products.TaxRate = objWeb.TaxRate / 100;
			products.Weight = objWeb.ProWeight;
			products.BrandID = objWeb.BrandID;
			products.CategoryID = objWeb.CategoryID == -1 ? 0 : objWeb.CategoryID;
			int isSale = 0;
			try {
				isSale = ZConvert.StrToInt(objWeb.SaleType[0]);
			}
			catch { }
			int isMaterial = 0;
			try {
				isMaterial = ZConvert.StrToInt(objWeb.SaleType[1]);
			}
			catch { }
			products.SaleType = isSale + isMaterial;
			products.Weight = objWeb.ProWeight;
			ProductsInfo productsInfo = new ProductsInfo();
			productsInfo.Products = products;
			if (objWeb.ID != null) {
				for (int i = 0; i < objWeb.ID.Length; i++) {
					ProductsSku productsSku = new ProductsSku();
					productsSku.ID = ZConvert.StrToInt(objWeb.ID[i]);
					productsSku.Code = objWeb.Code[i];
					productsSku.ProductsCode = products.Code;
					productsSku.BarCode = objWeb.BarCode[i];
					productsSku.CostPrice = ZConvert.StrToDecimal(objWeb.CostPrice[i]);
					productsSku.Saleprop = objWeb.Saleprop[i];
					productsSku.SellingPrice = ZConvert.StrToDecimal(objWeb.SellingPrice[i]);
					//productsSku.Weight = ZConvert.StrToDecimal(objWeb.Weight[i]);

					productsInfo.ProductsSkuList.Add(productsSku);
				}
			}
			#endregion
			string userCode = FormsAuth.GetUserCode();
			string position = "Products/ProductsController/Save";
			string buttonName = "保存商品";
			string target = "商品列表";
			bool isUpdate = false;
			BaseResult resultInfo = ProductsManager.Save(userCode, position, target, buttonName, productsInfo, isUpdate);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 删除商品

		public ActionResult Delete(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = ProductsManager.Del(userCode, idList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 删除商品SKU

		public ActionResult DeleteProductsSku(int productsSkuID) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = ProductsManager.DeleteProductsSku(userCode, productsSkuID);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 删除商品图片

		public ActionResult DeleteImg(string smallPic){
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = ProductsManager.DeleteImg(userCode, smallPic);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 查看商品库存信息

		/// <summary>
		/// 商品信息
		/// </summary>
		/// <param name="id">商品表标识</param>
		/// <param name="status">商品状态 1销售中 2仓库中</param>
		/// <param name="kc"></param>
		/// <returns></returns>
		public ActionResult Show(int id, int status, string kc = "") {
			Data.Products products = ProductsService.GetSingleProducts(id);
			ViewBag.Products = products;
			if (!string.IsNullOrEmpty(kc)) {
				ViewBag.kc = "1"; //分配库存 返回标示
			}
			else {
				ViewBag.kc = "0";
			}
			ViewBag.Status = status;
			return View();
		}

		/// <summary>
		/// 商品SKU列表
		/// </summary>
		/// <param name="id">商品表标识</param>
		/// <returns></returns>
		public ActionResult ShowSkuList(int id) {
			List<ProductsSkuKucInfo> productsSkuKucInfoList = ProductsManager.GetProductsSkuKucInfo(id);
			return JsonDate(productsSkuKucInfoList);
		}

		/// <summary>
		/// 商品各仓库SKU列表
		/// </summary>
		/// <param name="id">商品表标识</param>
		/// <returns></returns>
		public ActionResult ShowWarehouseSkuList(int id) {
			List<WarehouseProductsSkuKucInfo> warehouseProductsSkuKucInfoList = ProductsManager.GetWarehouseProductsSkuKucInfo("", id);
			return JsonDate(warehouseProductsSkuKucInfoList);
		}

		#endregion

		#region 关联物料

		/// <summary>
		/// 商品信息
		/// </summary>
		/// <param name="id">商品表标识</param>
		/// <param name="status">商品状态 1销售中 2仓库中</param>
		/// <returns></returns>
		public ActionResult ShowProductsMaterialMap(int id, int status) {
			Data.Products products = ProductsService.GetSingleProducts(id);
			ViewBag.Products = products;
			ViewBag.Status = status;
			return View();
		}

		/// <summary>
		/// 获取指定商品ID物料关联信息 分组统计
		/// </summary>
		/// <param name="id">商品表标识</param>
		/// <returns></returns>
		public ActionResult ShowProductsMaterialMapGroupList(int id) {
			List<ProductsSkuKucInfo> productsSkuKucInfoList = ProductsManager.GetProductsMaterialMapInfo(id);
			return JsonDate(productsSkuKucInfoList);
		}

		/// <summary>
		/// 设置关联物料
		/// </summary>
		/// <param name="sourceProductsSkuID">来源SKUID</param>
		/// <returns></returns>
		public ActionResult SetProductsMaterialMap(int sourceProductsSkuID) {
			ViewBag.SourceProductsSkuID = sourceProductsSkuID;
			return View();
		}

		/// <summary>
		/// 获取指定SKUID的关联列表
		/// </summary>
		/// <param name="sourceProductsSkuID">商品SKU标识</param>
		/// <returns></returns>
		public ActionResult GetProductsSkuMaterialMapList(int sourceProductsSkuID) {
			List<ProductsSkuMaterialMapInfo> productsSkuMaterialMapInfoList = ProductsManager.GetProductsSkuMaterialMapInfo(sourceProductsSkuID);
			return JsonDate(productsSkuMaterialMapInfoList);
		}

		/// <summary>
		/// 添加关联物料记录
		/// </summary>
		/// <param name="entity">关联物料实体类</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult AddProductsMaterialMap(ProductsMaterialMap entity) {
			string userCode = FormsAuth.GetUserCode();
			string position = "Products/ProductsController/AddProductsMaterialMap";
			string buttonName = "添加关联物料记录";
			string target = "商品列表";
			BaseResult resultInfo = ProductsManager.AddProductsMaterialMap(userCode, position, target, buttonName, entity);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 验证商品SKU码
		/// </summary>
		/// <param name="sourceProductsSkuID">要关联物料的SKUID</param>
		/// <param name="productsSkuCode">物料商品SKU码</param>
		/// <returns></returns>
		public ActionResult CheckProductsSkuCode(int sourceProductsSkuID, string productsSkuCode) {
			BaseResult resultInfo = new BaseResult();
			ProductsSku productsSku = ProductsSkuService.GetSingleProductsSku(productsSkuCode);
			int productsSkuID = 0;
			if (productsSku != null) {
				productsSkuID = productsSku.ID;
				Data.Products products = ProductsService.GetSingleProducts(productsSku.ProductsID);
				if (products != null) {
					bool isMaterial = (products.SaleType & (int)SaleType.物料) > 0;
					if (isMaterial) {
						bool isExists = ProductsMaterialMapService.IsExists(sourceProductsSkuID, productsSkuID);
						if (isExists) {
							resultInfo.result = 0;
							resultInfo.message = "该物料已经关联！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "SKU码对应商品编码 " + products.Code + " 不是物料！";
					}
				}
				else {
					resultInfo.result = 0;
					resultInfo.message = "SKU码存在，但商品不存在！";
				}
			}
			else {
				resultInfo.result = 0;
				resultInfo.message = "SKU码不存在，请先创建！";
			}
			var result = new { result = resultInfo.result, message = resultInfo.message, fromProductsSkuID = productsSkuID };
			return JsonDate(result);
		}

		/// <summary>
		/// 删除指定物料关联记录
		/// </summary>
		/// <param name="productsMaterialMapID"></param>
		/// <returns></returns>
		public ActionResult DelProductsMaterialMap(int productsMaterialMapID) {
			string userCode = FormsAuth.GetUserCode();
			string position = "Products/ProductsController/DelProductsMaterialMap";
			string buttonName = "删除关联物料记录";
			string target = "商品列表";
			BaseResult resultInfo = ProductsManager.DelProductsMaterialMap(userCode, position, target, buttonName, productsMaterialMapID);
			return JsonDate(resultInfo);
		}

		#endregion
	}
}
