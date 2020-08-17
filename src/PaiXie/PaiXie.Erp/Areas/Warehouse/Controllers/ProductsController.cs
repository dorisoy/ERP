using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Warehouse
{
	public class ProductsController : BaseController
    {
		//
		// GET: /Warehouse/Products/

		public ActionResult Index() {
			return View();
		}

		/// <summary>
		/// 商品列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();

			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = "products p left join warehouseProducts wp on p.ID = wp.ProductsID and WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "'";
			data.Select = "p.ID,p.Code,p.SmallPic,p.Name,p.BrandID,p.CategoryID,p.SellingPrice,p.CostPrice,p.Status";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<ProductsList> list = BaseService<ProductsList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				Brand brand = BrandService.GetSingleBrand(item.BrandID);
				Category category = CategoryService.GetSingleCategory(item.CategoryID);
				if (brand != null) {
					item.BrandName = brand.Name;
				}
				if (category != null) {
					item.CategoryName = category.Name;
				}
			}
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
			int categoryID = ZConvert.StrToInt(Request["categoryID"]);
			int brandID = ZConvert.StrToInt(Request["brandID"]);
			int status = ZConvert.StrToInt(Request["status"]);

			string whereSql = string.Format(" wp.ID is null and p.IsDelete=" + (int)IsEnable.否 + " and exists (select 1 from warehouse w where w.Code = '" + FormsAuth.GetWarehouseCode() + "' AND find_in_set(p.BrandID,  w.Librand)) ");

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
						whereSql += string.Format(" and p.ID in (select ProductsID from productsSku where Code like '%{0}%')", keyWord);
						break;
					case "商品条码":
						whereSql += string.Format(" and p.BarCode like '%{0}%'", keyWord);
						break;
				}
			}
			if (categoryID > 0) {
				whereSql += string.Format(" and p.CategoryID = {0}", categoryID);
			}
			if (brandID > 0) {
				whereSql += string.Format(" and p.BrandID = {0}", brandID);
			}
			if (status > 0) {
				whereSql += string.Format(" and p.Status = {0}", status);
			}

			return whereSql;
		}

		/// <summary>
		/// 导入仓库
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public ActionResult ImportWarehouse(string ids) {
			List<int> productsIDList = new List<int>();
			if (ids == "") {
				SelectBuilder data = new SelectBuilder();
				data.Having = "";
				data.GroupBy = "";
				data.OrderBy = "";
				data.From = "products p left join warehouseProducts wp on p.ID = wp.ProductsID and WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "'";
				data.Select = "p.ID";
				data.WhereSql = GetWhereSql(); ;
				data.PagingCurrentPage = 0;
				data.PagingItemsPerPage = 0;
				int total = 0;
				productsIDList = ProductsService.GetProductsIDListForPage(data, out total);
			}
			else {
				productsIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			}

			BaseResult resultInfo = WarehouseProductsManager.AddProductsInfo(FormsAuth.GetWarehouseCode(), productsIDList);
			if (resultInfo.result == 1) {
				resultInfo.message = "导入成功！";
			}
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 获取仓库授权品牌
		/// </summary>
		/// <returns></returns>
		public ActionResult GetWarehouseBrandJson() {
			CListItem cListItem = new CListItem();
			cListItem.Text = "请选择";
			cListItem.Value = "0";
			List<CListItem> treeList = new List<CListItem>();
			treeList.Add(cListItem);
			var brand = BrandService.GetManyBrandByWarehouseCode(FormsAuth.GetWarehouseCode()).Select(b => new CListItem { Text = b.Name, Value = b.ID.ToString()});
			if (brand.Count() > 0) {
				treeList.AddRange(brand);
			}
			return JsonDate(treeList);
		}

		/// <summary>
		/// 商品信息
		/// </summary>
		/// <param name="id">商品表标识</param>
		/// <returns></returns>
		public ActionResult Show(int id) {
			Data.Products products = ProductsService.GetSingleProducts(id);
			string unit = "";  
			Syscode sysCode = SyscodeService.GetSyscodeByCode(products.MeasurementUnitID);
			if (sysCode != null) {
				unit = sysCode.Text;
			}
			ViewBag.Products = products;
			ViewBag.Unit = unit;
			return View();
		}

		/// <summary>
		/// 商品SKU信息
		/// </summary>
		/// <param name="id">商品表标识</param>
		/// <returns></returns>
		public ActionResult ShowSkuList(int id) {
			List<ProductsSkuMaterialMapInfo> productsSkuMaterialMapList = ProductsService.GetManyProductsSkuMaterialMapInfo(id);
			return JsonDate(productsSkuMaterialMapList);
		}
	}
}
