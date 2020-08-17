using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Utils;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Data;

namespace PaiXie.Erp.Areas.Warehouse {
	public class BookingProductsSkuController : BaseController {
		//
		// GET: /Warehouse/BookingProductsSku/

		public ActionResult Index() {
			return View();
		}

		/// <summary>
		/// 预售商品列表
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
			data.From = "warehouseProducts wp INNER JOIN products p ON wp.ProductsID = p.ID";
			data.Select = "p.ID,p.Code,p.SmallPic,p.Name,wp.BookingModel";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseBookingProductsList> list = WarehouseBookingProductsSkuService.GetQueryManyForPageList(data, out total);
			foreach (var item in list) {
				WarehouseBookingProductsList bookingProducts = WarehouseBookingProductsSkuService.GetSingleWarehouseBookingProducts(FormsAuth.GetWarehouseCode(), item.ID);
				if (bookingProducts != null) {
					item.BookingNum = bookingProducts.BookingNum;
					item.KyNum = bookingProducts.KyNum;
					item.ZyNum = bookingProducts.ZyNum;
					item.CdNum = bookingProducts.CdNum;
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
			int bookingModel = ZConvert.StrToInt(Request["bookingModel"], -1);

			string whereSql = " wp.WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "' and wp.IsBooking = 1";

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
			if (bookingModel > -1) {
				whereSql += string.Format(" and wp.BookingModel = {0}", bookingModel);
			}

			return whereSql;
		}

		/// <summary>
		/// 添加预售
		/// </summary>
		/// <param name="productsCode"></param>
		/// <returns></returns>
		public ActionResult Add(string productsCode) {
			WarehouseProductsInfo warehouseProductsInfo = new WarehouseProductsInfo();
			if (!string.IsNullOrEmpty(productsCode)) {
				warehouseProductsInfo = WarehouseProductsService.GetSingleWarehouseProductsInfo(FormsAuth.GetWarehouseCode(), productsCode);
			}
			ViewBag.WarehouseProductsInfo = warehouseProductsInfo;
			return View();
		}

		/// <summary>
		/// 添加预售商品信息
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult AddSave(Data.WarehouseBookingProductsWebInfo warehouseBookingProductsInfo) {
			BaseResult resultInfo = CheackProducts("", warehouseBookingProductsInfo.ProductsID);
			if (resultInfo.result == 1) {
				IDictionary<int, int> skuList = new Dictionary<int, int>();
				for (int i = 0; i < warehouseBookingProductsInfo.BookingNum.Length; i++) {
					skuList.Add(ZConvert.StrToInt(warehouseBookingProductsInfo.ProductsSkuID[i]), ZConvert.StrToInt(warehouseBookingProductsInfo.BookingNum[i]));
				}
				resultInfo = WarehouseProductsManager.AddBookingProductsSku(FormsAuth.GetWarehouseCode(), warehouseBookingProductsInfo.BookingModel, warehouseBookingProductsInfo.ProductsID, skuList);
				if (resultInfo.result == 1) {
					resultInfo.message = "添加预售成功！";
				}
			}
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 检查商品编号
		/// </summary>
		/// <param name="productsCode"></param>
		/// <returns></returns>
		public ActionResult CheackProductsCode(string productsCode) {
			BaseResult resultInfo = CheackProducts(productsCode, 0);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 检查商品是否合法
		/// </summary>
		/// <param name="productsCode"></param>
		/// <param name="productsID"></param>
		/// <returns></returns>
		private static BaseResult CheackProducts(string productsCode, int productsID) {
			BaseResult resultInfo = new BaseResult();
			WarehouseProductsInfo warehouseProductsInfo = new WarehouseProductsInfo();
			if (productsID > 0) {
				warehouseProductsInfo = WarehouseProductsService.GetSingleWarehouseProductsInfo(FormsAuth.GetWarehouseCode(), productsID);
			}
			else {
				warehouseProductsInfo = WarehouseProductsService.GetSingleWarehouseProductsInfo(FormsAuth.GetWarehouseCode(), productsCode);
			}
			if (warehouseProductsInfo == null) {
				resultInfo.result = 0;
				resultInfo.message = "没有找到商品！";
			}
			else {
				if (warehouseProductsInfo.IsBooking == 1) {
					resultInfo.result = 0;
					resultInfo.message = "商品已经添加！";
				}
			}
			return resultInfo;
		}

		/// <summary>
		/// 商品SKU预售数量
		/// </summary>
		/// <param name="productsID"></param>
		/// <returns></returns>
		public ActionResult BookingNum(int productsID) {
			List<WarehouseBookingProductsSkuInfo> warehouseBookingProductsSkuList = WarehouseBookingProductsSkuService.GetManyWarehouseBookingProductsSkuInfo(FormsAuth.GetWarehouseCode(), productsID);
			if (warehouseBookingProductsSkuList.Count > 0) {
				int totalBookingNum = 0;
				foreach (var warehouseBookingProductsSku in warehouseBookingProductsSkuList) {
					totalBookingNum += warehouseBookingProductsSku.BookingNum;
				}
				warehouseBookingProductsSkuList.Add(new WarehouseBookingProductsSkuInfo { Code = "汇总", BookingNum = totalBookingNum });
			}
			return JsonDate(warehouseBookingProductsSkuList);
		}

		/// <summary>
		/// 查看（修改预售数量）
		/// </summary>
		/// <param name="productsCode">商品编码</param>
		/// <returns></returns>
		public ActionResult Edit(string productsCode) {
			WarehouseProductsInfo warehouseProductsInfo = new WarehouseProductsInfo();
			if (!string.IsNullOrEmpty(productsCode)) {
				warehouseProductsInfo = WarehouseProductsService.GetSingleWarehouseProductsInfo(FormsAuth.GetWarehouseCode(), productsCode);
			}
			ViewBag.WarehouseProductsInfo = warehouseProductsInfo;
			return View();
		}

		/// <summary>
		/// 修改预售数量
		/// </summary>
		/// <param name="warehouseBookingProductsInfo"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult EditSave(Data.WarehouseBookingProductsWebInfo warehouseBookingProductsInfo) {
			IDictionary<int, int> skuList = new Dictionary<int, int>();
			for (int i = 0; i < warehouseBookingProductsInfo.BookingNum.Length; i++) {
				skuList.Add(ZConvert.StrToInt(warehouseBookingProductsInfo.ProductsSkuID[i]), ZConvert.StrToInt(warehouseBookingProductsInfo.BookingNum[i]));
			}
			BaseResult resultInfo = WarehouseProductsManager.UpdateBookingNum(FormsAuth.GetWarehouseCode(), warehouseBookingProductsInfo.ProductsID, skuList);
			if (resultInfo.result == 1) {
				resultInfo.message = "修改成功！";
			}
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 取消预售
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public ActionResult Delete(string ids) {
			List<int> productsIDList = new List<int>();
			productsIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = WarehouseProductsManager.CancelBookingProducts(FormsAuth.GetWarehouseCode(), productsIDList);
			if (resultInfo.result == 1) {
				resultInfo.message = "删除成功！";
			}
			return JsonDate(resultInfo);
		}
	}
}