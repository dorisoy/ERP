using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Core.Enum;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Order.Controllers {
	public class DownloadController : BaseController {

		//
		// GET: /Order/Download/
		public ActionResult Index() {
			ViewBag.StartDate = DateTime.Now.AddDays(-6).AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss");
			ViewBag.EndDate = DateTime.Now.AddMinutes(-30);
			return View();
		}

		/// <summary>
		/// 订单列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "ID DESC";
			data.From = "ord_outer";
			data.Select = "ID,ShopID,OutOrderCode,ErpOrderCode,Created,ProductsAmount,Freight,BuyName,BuyMtel,BuyTel,BuyAddr,ShippingType,BuyProvince,BuyCity,BuyDistrict,BuyMessage,SellerRemark";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<OrdouterInfoList> list = BaseService<OrdouterInfoList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				item.IsProductAddFin = OrdouterItemService.getIsProductAddFin(item.ID);
				Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(item.ErpOrderCode);
				if (ordbase.LogisticsID == 0) {
					SearchLogistics(item);
				}
				else {
					item.LogisticsID = ordbase.LogisticsID;
					item.LogisticsName = LogisticsService.GetLogistics(ordbase.LogisticsID).Name;
				}
				if (!string.IsNullOrEmpty(ordbase.BuyName)) {
					item.BuyName = ordbase.BuyName;
				}
				if (!string.IsNullOrEmpty(ordbase.BuyMtel)) {
					item.BuyMtel = ordbase.BuyMtel;
				}
				if (!string.IsNullOrEmpty(ordbase.BuyTel)) {
					item.BuyTel = ordbase.BuyTel;
				}
				if (!string.IsNullOrEmpty(ordbase.BuyAddr)) {
					item.BuyAddr = ordbase.BuyAddr;
				}
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		/// <summary>
		/// 获取查询条件
		/// </summary>
		/// <returns></returns>
		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int shopID = ZConvert.StrToInt(Request["shopID"]);
			int isCod = ZConvert.StrToInt(Request["isCod"]);
			int isNeedInvoice = ZConvert.StrToInt(Request["isNeedInvoice"]);
			int isRemark = ZConvert.StrToInt(Request["isRemark"]);

			string whereSql = string.Format("GenerateState = 0");

			if (keyWord != "") {
				switch (keyWordType) {
					case "外部订单号":
						whereSql += string.Format(" and OutOrderCode like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and EXISTS(SELECT 1 FROM ord_item Where OutOrderCode = ord_outer.OutOrderCode and ProductsCode like '%{0}%')", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and EXISTS(SELECT 1 FROM ord_item Where OutOrderCode = ord_outer.OutOrderCode and ProductsNo like '%{0}%')", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and EXISTS(SELECT 1 FROM ord_item Where OutOrderCode = ord_outer.OutOrderCode and ProductsSkuCode like '%{0}%')", keyWord);
						break;
				}
			}

			if (shopID > 0) {
				whereSql += string.Format(" and ShopID = {0}", shopID);
			}
			if (isCod > 0) {
				whereSql += string.Format(" and IsCod = {0}", isCod);
			}
			if (isNeedInvoice > 0) {
				whereSql += string.Format(" and IsNeedInvoice = {0}", isNeedInvoice);
			}
			if (isRemark > 0) {
				whereSql += string.Format(" and (BuyMessage <> '' or SellerRemark <> '')");
			}

			return whereSql;
		}

		/// <summary>
		/// 匹配物流
		/// </summary>
		private void SearchLogistics(OrdouterInfoList item) {
			List<ShopExpressSet> expressSetList = ShopExpressSetService.GetManyShopExpressSet(item.ShopID);
			foreach (var expressSet in expressSetList) {
				if (item.ShippingType == expressSet.ShippingType) {
					item.LogisticsID = expressSet.LogisticsID;
					item.LogisticsName = LogisticsService.GetLogistics(expressSet.LogisticsID).Name;
					break;
				}
			}
		}

		/// <summary>
		/// 获取线上店铺
		/// </summary>
		/// <returns></returns>
		public ActionResult GetOnlineShopJson() {
			List<CListItem> treeList = new List<CListItem>();
			var shop = ShopService.shoplist().Select(b => new CListItem { Text = b.Name, Value = b.ID.ToString() }); ;
			if (shop.Count() > 0) {
				treeList.AddRange(shop);
			}
			else {
				CListItem cListItem = new CListItem();
				cListItem.Text = "请选择";
				cListItem.Value = "0";
				treeList.Add(cListItem);
			}
			return JsonDate(treeList);
		}

		/// <summary>
		/// 自动生成设置
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="shopName"></param>
		/// <returns></returns>
		public ActionResult Autogeneration(int shopID, string shopName) {
			ShopAutogeneration autogeneration = ShopAutogenerationService.GetSingleShopAutogeneration(shopID);
			if (autogeneration == null) {
				autogeneration = new ShopAutogeneration();
				autogeneration.ShopID = shopID;
				autogeneration.DownInterval = 10;
				autogeneration.CreateInterval = 10;
				autogeneration.GenerateInterval = 10;
			}
			ViewBag.ShopName = shopName;
			ViewBag.Autogeneration = autogeneration;
			return View();
		}

		/// <summary>
		/// 保存自动生成设置
		/// </summary>
		/// <param name="autogenerationWebInfo"></param>
		/// <returns></returns>
		public ActionResult SaveAutogeneration(ShopAutogenerationWebInfo autogenerationWebInfo) {
			BaseResult resultInfo = OrdbaseManager.SaveAutogeneration(autogenerationWebInfo);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 匹配快递设置
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="shopName"></param>
		/// <returns></returns>
		public ActionResult ExpressSet(int shopID, string shopName) {
			ViewBag.ShopID = shopID;
			ViewBag.ShopName = shopName;
			ViewBag.OrdShippingType =
			ViewBag.LogisticsList = LogisticsService.GetManyLogistics();
			ViewBag.ExpressSetList = ShopExpressSetService.GetManyShopExpressSet(shopID);
			return View();
		}

		/// <summary>
		/// 保存匹配快递设置
		/// </summary>
		/// <param name="expressSetWebInfo"></param>
		/// <returns></returns>
		public ActionResult SaveExpressSet(ShopExpressSetWebInfo expressSetWebInfo) {
			BaseResult resultInfo = OrdbaseManager.SaveExpressSet(expressSetWebInfo);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 更改发货物流
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="logisticsID"></param>
		/// <returns></returns>
		public ActionResult ChangeExpress(string erpOrderCode, int logisticsID) {
			ViewBag.ErpOrderCode = erpOrderCode;
			ViewBag.LogisticsID = logisticsID;
			ViewBag.LogisticsList = LogisticsService.GetManyLogistics();
			return View();
		}

		/// <summary>
		/// 保存选择发货物流
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="shopName"></param>
		/// <returns></returns>
		public ActionResult SaveExpress(string erpOrderCode, int logisticsID) {
			BaseResult resultInfo = new BaseResult();
			int rowsAffected = OrdbaseService.UpdateLogisticsID(erpOrderCode, logisticsID);
			if (rowsAffected == 0) {
				resultInfo.result = 0;
				resultInfo.message = "确认失败！";
			}

			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 商品订单
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="ordouterID"></param>
		/// <param name="outOrderCode"></param>
		/// <returns></returns>
		public ActionResult OrderProducts(int shopID, int ordouterID, string outOrderCode) {
			ViewBag.ShopID = shopID;
			ViewBag.OrdouterID = ordouterID;
			ViewBag.OutOrderCode = outOrderCode;
			return View();
		}

		/// <summary>
		/// 显示外部订单商品 
		/// </summary>
		/// <param name="ordouterID"></param>
		/// <returns></returns>
		public ActionResult ShowOrdOuterItem(int ordouterID) {
			List<OrdouterItem> ordouterItemList = OrdouterItemService.GetManyOrdouterItem(ordouterID);
			var result = new { total = ordouterItemList.Count, rows = ordouterItemList };
			return JsonDate(result);
		}

		/// <summary>
		/// 显示系统订单商品
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="outOrderCode"></param>
		/// <returns></returns>
		public ActionResult ShowOrdItem(int shopID, string outOrderCode) {
			List<Orditem> orditemList = OrditemService.GetManyOrditem(shopID, outOrderCode).OrderBy(o => o.OrdouterItemID).ToList();
			var result = new { total = orditemList.Count, rows = orditemList };
			return JsonDate(result);
		}

		/// <summary>
		/// 添加商品
		/// </summary>
		/// <param name="ordouterItemID"></param>
		/// <returns></returns>
		public ActionResult AddOrdItem(int ordouterItemID) {
			OrdouterItem ordouterItem = OrdouterItemService.GetQuerySingleByID(ordouterItemID);
			if (ordouterItem == null) ordouterItem = new OrdouterItem();
			ViewBag.OrdouterItem = ordouterItem;
			return View();
		}

		/// <summary>
		/// 搜索商品信息
		/// </summary>
		/// <param name="productsCode"></param>
		/// <param name="productsSkuCode"></param>
		/// <returns></returns>
		public ActionResult SearchProducts(string productsCode, string productsSkuCode) {
			BaseResult resultInfo = new BaseResult();
			int productsID = 0;
			string productsName = string.Empty;
			Data.Products products = null;
			if (!string.IsNullOrWhiteSpace(productsCode)) {
				products = ProductsService.GetSingleProducts(productsCode);
			}
			else {
				if (!string.IsNullOrWhiteSpace(productsSkuCode)) {
					ProductsSku productsSku = ProductsSkuService.GetSingleProductsSku(productsSkuCode);
					if (productsSku != null) {
						products = ProductsService.GetSingleProducts(productsSku.ProductsID);
					}
				}
			}

			if (products == null) {
				resultInfo.result = 0;
				resultInfo.message = "未找到该商品！";
			}
			else {
				productsID = products.ID;
				productsName = products.Name;
			}
			var result = new { result = resultInfo.result, message = resultInfo.message, productsID = productsID, productsName = productsName };
			return JsonDate(result);
		}

		/// <summary>
		/// 绑定商品信息
		/// </summary>
		/// <param name="productsID"></param>
		/// <param name="productsSkuCode"></param>
		/// <returns></returns>
		public ActionResult SearchProductsSku(int productsID, string productsSkuCode) {
			SelectBuilder data = new SelectBuilder();
			string whereSql = "";
			if (string.IsNullOrWhiteSpace(productsSkuCode)) {
				whereSql = string.Format("ProductsID={0}", productsID);
			}
			else {
				whereSql = string.Format("Code='{0}'", productsSkuCode);
			}
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = @"productsSku";
			data.Select = "ID AS ProductsSkuID,Saleprop AS ProductsSkuSaleprop,Code AS ProductsSkuCode,0 AS KyNum";
			data.WhereSql = whereSql;
			int total = 0;
			List<OrdProductsSkuList> list = BaseService<OrdProductsSkuList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				item.KyNum = ProductsSkuService.GetKfhNumByProductsSkuID(item.ProductsSkuID);
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		/// <summary>
		/// 保存订单商品
		/// </summary>
		/// <param name="ordProductsSkuWebInfo"></param>
		/// <returns></returns>
		public ActionResult SaveProducts(OrdProductsSkuWebInfo ordProductsSkuWebInfo) {
			ordProductsSkuWebInfo.IsOutOrder = 1;
			BaseResult resultInfo = OrditemManager.AddItem(ordProductsSkuWebInfo);
			var result = new { result = resultInfo.result, message = resultInfo.message, erpOrderCode = ordProductsSkuWebInfo.ErpOrderCode };
			return JsonDate(result);
		}

		/// <summary>
		/// 收货人信息
		/// </summary>
		/// <param name="ordouterID"></param>
		/// <returns></returns>
		public ActionResult ReceiverInfo(int ordouterID) {
			Ordouter ordouter = OrdouterService.GetQuerySingleByID(ordouterID);
			AreaManager.Area area = new AreaManager.Area();
			Ordbase ordbase = new Ordbase();
			if (ordouter == null) {
				ordouter = new Ordouter();
			}
			else {
				ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(ordouter.ErpOrderCode);
				if (ordbase.DistrictID == 0) {
					ordbase.BuyName = ordouter.BuyName;
					ordbase.BuyTel = ordouter.BuyTel;
					ordbase.BuyMtel = ordouter.BuyMtel;
					ordbase.Province = ordouter.BuyProvince;
					ordbase.City = ordouter.BuyCity;
					ordbase.District = ordouter.BuyDistrict;
					ordbase.BuyAddressDetail = ordouter.BuyAddressDetail;
					ordbase.BuyAddr = ordouter.BuyAddr;
					ordbase.BuyPostCode = ordouter.BuyPostCode;

					string address = "";
					if (ordouter.BuyProvince == "" && ordouter.BuyCity == "" && ordouter.BuyDistrict == "") {
						address = ordouter.BuyAddr;
					}
					else {
						address = ordouter.BuyProvince + ordouter.BuyCity + ordouter.BuyDistrict + ordouter.BuyAddressDetail;
					}
					area = AreaManager.GetAreaInfo(address);
				}
				else {
					area.ProvinceID = ordbase.ProvinceID;
					area.Province = ordbase.Province;
					area.CityID = ordbase.CityID;
					area.City = ordbase.City;
					area.CountyID = ordbase.DistrictID;
					area.County = ordbase.District;
				}
			}
			ViewBag.Ordbase = ordbase;
			ViewBag.Area = area;

			return View();
		}

		/// <summary>
		/// 保存收货人信息
		/// </summary>
		/// <param name="ordbaseWebInfo"></param>
		/// <returns></returns>
		public ActionResult SaveReceiverInfo(Ordbase ordbaseWebInfo) {
			BaseResult resultInfo = new BaseResult();
			Ordbase ordbase = OrdbaseService.GetQuerySingleByID(ordbaseWebInfo.ID);
			if (ordbase == null) {
				resultInfo.result = 0;
				resultInfo.message = "订单不存在！";
			}
			else {
				ordbase.BuyName = ordbaseWebInfo.BuyName;
				ordbase.BuyTel = ordbaseWebInfo.BuyTel;
				ordbase.BuyMtel = ordbaseWebInfo.BuyMtel;
				ordbase.ProvinceID = ordbaseWebInfo.ProvinceID;
				ordbase.Province = ordbaseWebInfo.Province;
				ordbase.CityID = ordbaseWebInfo.CityID;
				ordbase.City = ordbaseWebInfo.City;
				ordbase.DistrictID = ordbaseWebInfo.DistrictID;
				ordbase.District = ordbaseWebInfo.District;
				ordbase.BuyAddressDetail = ordbaseWebInfo.BuyAddressDetail;
				ordbase.BuyAddr = ordbase.Province + ordbase.City + ordbase.District + ordbase.BuyAddressDetail;
				ordbase.BuyPostCode = ordbaseWebInfo.BuyPostCode;
				ordbase.UpdateDate = DateTime.Now;
				ordbase.UpdatePerson = FormsAuth.GetUserCode();
				int rowsAffected = OrdbaseService.Update(ordbase);
				if (rowsAffected == 0) {
					resultInfo.result = 0;
					resultInfo.message = "保存收货人信息失败！";
				}
			}
			var result = new { result = resultInfo.result, message = resultInfo.message };
			return JsonDate(result);
		}

		/// <summary>
		/// 删除订单商品
		/// </summary>
		/// <returns></returns>
		public ActionResult DelItem(int id) {
			BaseResult resultInfo = OrditemManager.DeleteItem(id, false);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 删除外部商品
		/// </summary>
		/// <returns></returns>
		public ActionResult DelOuterItem(int id) {
			BaseResult resultInfo = new BaseResult();
			OrdouterItem ordouterItem = OrdouterItemService.GetQuerySingleByID(id);
			if (ordouterItem != null) {
				ordouterItem.IsProductAddFin = 2;
				int rowsAffected = OrdouterItemService.Update(ordouterItem);
				if (rowsAffected == 0) {
					resultInfo.result = 0;
					resultInfo.message = "删除失败！";
				}
			}
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 删除订单
		/// </summary>
		/// <returns></returns>
		public ActionResult Delete(int id) {
			BaseResult resultInfo = OrdouterManager.Delte(id);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 批量删除
		/// </summary>
		/// <param name="ordouterID"></param>
		/// <returns></returns>
		public ActionResult BatchDelete(string ids) {
			BaseResult resultInfo = new BaseResult();
			List<int> ordouterIDList = new List<int>();
			ordouterIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			int successCount = 0;
			foreach (var ordouterID in ordouterIDList) {
				resultInfo = OrdouterManager.Delte(ordouterID);
				if (resultInfo.result == 1) {
					successCount++;
				}
			}
			if (successCount == 0) {
				resultInfo.result = 0;
				resultInfo.message = "删除失败！";
			}
			else {
				resultInfo.result = 1;
				resultInfo.message = "成功删除" + successCount + "单！";
			}
			var result = new { result = resultInfo.result, message = resultInfo.message };
			return JsonDate(result);
		}

		/// <summary>
		/// 下载任务提示信息
		/// </summary>
		/// <param name="shopid"></param>
		/// <returns></returns>
		public ActionResult ShowTaskMsg(int shopid) {
			ShopTask shopTask = ShopTaskService.GetSingleShopTask(shopid, (int)ShopTaskType.下载订单);
			if (shopTask == null) {
				shopTask = new ShopTask();
			}
			return JsonDate(shopTask);
		}

		#region 下载订单

		/// <summary>
		/// 下载订单
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="dateType"></param>
		/// <param name="StartDate"></param>
		/// <param name="EndDate"></param>
		public void DownOrder(int shopID, int dateType, string StartDate, string EndDate) {
			if (shopID > 0) {
				DownOrderParam downParam = new DownOrderParam();
				downParam.ShopID = shopID;
				downParam.StartDate = ZConvert.StrToDateTime(StartDate, DateTime.Now);
				downParam.EndDate = ZConvert.StrToDateTime(EndDate, DateTime.Now);
				downParam.IsAuto = 0;
				downParam.PageNo = 1;
				downParam.DateType = dateType;
				downParam.UserCode = FormsAuth.GetUserCode();
				WaitCallback callBack = new WaitCallback(theadFunc);
				Common.RunAsyn(callBack, downParam);
			}
		}

		/// <summary>
		/// 异步执行下载任务
		/// </summary>
		/// <param name="state"></param>
		public static void theadFunc(object state) {
			DownOrderManager.StartTask((DownOrderParam)state);
		}

		/// <summary>
		/// 下载进度
		/// </summary>
		/// <param name="shopID"></param>
		/// <returns></returns>
		public ActionResult DownloadProgress(int shopID) {
			ShopTask shopTask = ShopTaskService.GetSingleShopTask(shopID, (int)ShopTaskType.下载订单);
			if (shopTask == null) {
				shopTask = new ShopTask();
			}
			else {
				if (shopTask.TaskStatus == (int)ShopTaskStatus.已结束) {
					shopTask.FinshCount = shopTask.TotalCount;
				}
			}
			var data = new { TotalCount = shopTask.TotalCount, FinshCount = shopTask.FinshCount };
			return JsonDate(data);
		}

		#endregion

		/// <summary>
		/// 生成订单
		/// </summary>
		/// <param name="ordouterID"></param>
		/// <returns></returns>
		public ActionResult Generate(int id) {
			BaseResult resultInfo = OrdbaseManager.Generate(id, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 批量生成
		/// </summary>
		/// <param name="ordouterID"></param>
		/// <returns></returns>
		public ActionResult BatchGenerate(string ids) {
			BaseResult resultInfo = new BaseResult();
			List<int> ordouterIDList = new List<int>();
			ordouterIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			int successCount = 0;
			foreach (var ordouterID in ordouterIDList) {
				resultInfo = OrdbaseManager.Generate(ordouterID, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
				if (resultInfo.result == 1) {
					successCount++;
				}
			}
			if (successCount == 0) {
				resultInfo.result = 0;
				resultInfo.message = "生成失败！";
			}
			else {
				resultInfo.result = 1;
				resultInfo.message = "成功生成" + successCount + "单！";
			}
			var result = new { result = resultInfo.result, message = resultInfo.message };
			return JsonDate(result);
		}

		/// <summary>
		/// 申请退款的商品列表
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult GetRefundItemList(int id) {
			List<OrdouterItem> list = OrdouterItemService.GetManyOrdouterItem(id);
			list = list.Where(r => r.IsProductAddFin == -2).ToList();
			return JsonDate(list);
		}
	}
}
