using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
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
    public class WarehouseProductsController : BaseController
    {
        //
        // GET: /Warehouse/WarehouseProducts/

        public ActionResult Index()
        {
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
			data.From = "products p inner join warehouseProducts wp on p.ID = wp.ProductsID";
			data.Select = "p.ID,p.Code,p.SmallPic,p.Name,p.BrandID,p.CategoryID,p.SellingPrice,p.CostPrice,p.Status";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseProductsList> list = BaseService<WarehouseProductsList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				Brand brand = BrandService.GetSingleBrand(item.BrandID);
				Category category = CategoryService.GetSingleCategory(item.CategoryID);
				if (brand != null) {
					item.BrandName = brand.Name;
				}
				if (category != null) {
					item.CategoryName = category.Name;
				}

				List<WarehouseProductsSkuKucInfo> skuKucInfoList = ProductsManager.GetWarehouseProductsSkuKucInfo(FormsAuth.GetWarehouseCode(), item.ID);
				foreach (var skuKucInfo in skuKucInfoList) {
					item.Num += skuKucInfo.TotalNum;
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

			string whereSql = " wp.WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "' and wp.ProductsStatus = " + status + " and p.IsDelete=" + (int)IsEnable.否;

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

			return whereSql;
		}

        /// <summary>
		/// 上架
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
		public ActionResult OnSale(string ids) {
			List<int> productsIDList = new List<int>();
			productsIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = WarehouseProductsManager.UpdateProductsStatus(FormsAuth.GetWarehouseCode(), productsIDList, (int)ProductsStatus.销售中);
			if (resultInfo.result == 1) {
				resultInfo.message = "上架成功！";
			}
			return JsonDate(resultInfo);
		}

        /// <summary>
		/// 下架
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
		public ActionResult OffSale(string ids) {
			List<int> productsIDList = new List<int>();
			productsIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = WarehouseProductsManager.UpdateProductsStatus(FormsAuth.GetWarehouseCode(), productsIDList, (int)ProductsStatus.仓库中);
			if (resultInfo.result == 1) {
				resultInfo.message = "下架成功！";
			}
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public ActionResult Delete(string ids) {
			List<int> productsIDList = new List<int>();
			productsIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = WarehouseProductsManager.DelProductsInfo(FormsAuth.GetWarehouseCode(), productsIDList);
			if (resultInfo.result == 1) {
				resultInfo.message = "删除成功！";
			}
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 查看商品
		/// </summary>
		/// <param name="id">商品ID</param>
		/// <returns></returns>
		public ActionResult ShowKuc(int id) {
			WarehouseProductsInfo warehouseProductsInfo = WarehouseProductsService.GetSingleWarehouseProductsInfo(FormsAuth.GetWarehouseCode(), id);
			ViewBag.WarehouseProductsInfo = warehouseProductsInfo;
			return View();
		}

		/// <summary>
		/// 商品SKU列表
		/// </summary>
		/// <param name="id">商品表标识</param>
		/// <returns></returns>
		public ActionResult ShowSkuKuc(int id) {
			List<WarehouseProductsSkuKucInfo> warehouseProductsSkuKucInfoList = ProductsManager.GetWarehouseProductsSkuKucInfo(FormsAuth.GetWarehouseCode(), id);
			return JsonDate(warehouseProductsSkuKucInfoList);
		}
    }
}
