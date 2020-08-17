using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Core.Enum;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Order.Controllers {
	public class AddOrderController : BaseController {
		//
		// GET: /Order/AddOrder/

		public ActionResult Index(string erpOrderCode = "") {
			AreaManager.Area area = new AreaManager.Area();
			Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode);
			if (ordbase == null) {
				ordbase = new Ordbase();
			}
			if (ordbase.DistrictID > 0) {
				area.ProvinceID = ordbase.ProvinceID;
				area.Province = ordbase.Province;
				area.CityID = ordbase.CityID;
				area.City = ordbase.City;
				area.CountyID = ordbase.DistrictID;
				area.County = ordbase.District;
			}
			ViewBag.Ordbase = ordbase;
			ViewBag.Area = area;

			return View();
		}

		/// <summary>
		/// 获取付款方式
		/// </summary>
		/// <returns></returns>
		public ActionResult GetPaymentMethod() {
			DataTable dtPlatformType = EnumManager<PaymentMethod>.GetDataTable(-1);
			List<CListItem> treeList = new List<CListItem>();
			for (int i = 0; i < dtPlatformType.Rows.Count; i++) {
				CListItem cListItem = new CListItem();
				cListItem.Text = dtPlatformType.Rows[i]["Name"].ToString();
				cListItem.Value = dtPlatformType.Rows[i]["Value"].ToString();
				treeList.Add(cListItem);
			}
			return JsonDate(treeList);
		}

		/// <summary>
		/// 获取物流列表
		/// </summary>
		/// <returns></returns>
		public ActionResult GetLogisticsJson() {
			List<CListItem> treeList = new List<CListItem>();
			CListItem cListItem = new CListItem();
			cListItem.Text = "请选择";
			cListItem.Value = "0";
			treeList.Add(cListItem);
			var shop = LogisticsService.GetManyLogistics().Select(b => new CListItem { Text = b.Name, Value = b.ID.ToString() }); ;
			if (shop.Count() > 0) {
				treeList.AddRange(shop);
			}
			return JsonDate(treeList);
		}

		/// <summary>
		/// 商品列表
		/// </summary>
		/// <returns></returns>
		public ActionResult ShowOrditem(string erpOrderCode) {
			List<Orditem> orditemList = OrditemService.GetManyOrditem(erpOrderCode);
			var result = new { total = orditemList.Count, rows = orditemList };
			return JsonDate(result);
		}

		/// <summary>
		/// 优惠信息
		/// </summary>
		/// <returns></returns>
		public ActionResult ShowDiscount(string erpOrderCode) {
			List<Orddiscount> discountList = OrddiscountService.GetManyOrddiscount(erpOrderCode);
			var result = new { total = discountList.Count, rows = discountList };
			return JsonDate(result);
		}

		/// <summary>
		/// 添加订单商品
		/// </summary>
		/// <returns></returns>
		public ActionResult AddProducts(string erpOrderCode) {
			Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode);
			if (ordbase == null) {
				ordbase = new Ordbase();
			}
			ViewBag.Ordbase = ordbase;
			return View();
		}

		/// <summary>
		/// 搜索商品信息
		/// </summary>
		/// <param name="productsCode"></param>
		/// <returns></returns>
		public ActionResult SearchProducts(string code) {
			BaseResult resultInfo = new BaseResult();
			int productsID = 0;
			string productsName = string.Empty;
			Data.Products products = ProductsService.GetSingleProducts(code);
			if (products == null) {
				resultInfo.result = 0;
				resultInfo.message = "未找到该商品！";
			}
			else {
				if (products.Status == (int)ProductsStatus.仓库中) {
					resultInfo.result = 0;
					resultInfo.message = "商品已下架！";
				}
				else {
					productsID = products.ID;
					productsName = products.Name;
				}
			}
			var result = new { result = resultInfo.result, message = resultInfo.message, productsID = productsID, productsName = productsName };
			return JsonDate(result);
		}

		/// <summary>
		/// 绑定商品信息
		/// </summary>
		/// <param name="productsID"></param>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult SearchProductsSku(int productsID, string erpOrderCode) {
			SelectBuilder data = new SelectBuilder();
			string whereSql = string.Format("ps.ProductsID={0}", productsID);
			data.Having = "";
			data.GroupBy = "ps.ID,ps.Saleprop,ps.Code,ps.SellingPrice";
			data.OrderBy = "ps.ID";
			data.From = @"productsSku ps LEFT JOIN ord_item oi ON ps.ID = oi.ProductsSkuID AND oi.ErpOrderCode = '" + erpOrderCode + "'";
			data.Select = "ps.ID AS ProductsSkuID,ps.Saleprop AS ProductsSkuSaleprop,ps.Code AS ProductsSkuCode,ps.SellingPrice,SUM(IFNULL(oi.ProductsNum, 0)) ProductsNum,0 AS KyNum";
			data.WhereSql = whereSql;
			int total = 0;
			List<OrdProductsSkuList> list = BaseService<OrdProductsSkuList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				item.KyNum = ProductsSkuService.GetKfhNumByProductsSkuID(item.ProductsSkuID, 1);
				if (item.KyNum < 0) item.KyNum = 0;
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
			BaseResult resultInfo = OrditemManager.AddItem(ordProductsSkuWebInfo);
			var result = new { result = resultInfo.result, message = resultInfo.message, id = ordProductsSkuWebInfo.ID, erpOrderCode = ordProductsSkuWebInfo.ErpOrderCode };
			return JsonDate(result);
		}

		/// <summary>
		/// 添加优惠信息
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="orddiscountID"></param>
		/// <returns></returns>
		public ActionResult AddDiscount(string erpOrderCode, int orddiscountID = 0) {
			Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode);
			List<Orditem> orditemList = OrditemService.GetManyOrditem(erpOrderCode);
			ViewBag.Ordbase = ordbase;
			ViewBag.OrditemList = orditemList;
			return View();
		}

		/// <summary>
		/// 保存订单优惠
		/// </summary>
		/// <param name="orddiscountWebInfo"></param>
		/// <returns></returns>
		public ActionResult SaveDiscount(OrddiscountWebInfo orddiscountWebInfo) {
			BaseResult resultInfo = OrditemManager.AddDiscount(orddiscountWebInfo);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 更新订单商品数量
		/// </summary>
		/// <param name="orditemID"></param>
		/// <param name="productsNum"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public ActionResult UpdateProductsNum(int orditemID, int productsNum, int type) {
			BaseResult resultInfo = OrditemManager.UpdateProductsNum(orditemID, productsNum, type);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 删除订单商品
		/// </summary>
		/// <param name="orditemID"></param>
		/// <returns></returns>
		public ActionResult DeleteItem(int orditemID) {
			BaseResult resultInfo = OrditemManager.DeleteItem(orditemID);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 删除优惠
		/// </summary>
		/// <param name="discountID"></param>
		/// <returns></returns>
		public ActionResult DeleteDiscount(int discountID) {
			BaseResult resultInfo = OrditemManager.DeleteDiscount(discountID);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 暂存订单
		/// </summary>
		/// <param name="orddiscountWebInfo"></param>
		/// <returns></returns>
		public ActionResult ScratchOrder(Ordbase ordbase) {
			BaseResult resultInfo = OrdbaseManager.ScratchOrder(ordbase, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
			var result = new { result = resultInfo.result, message = resultInfo.message, id = ordbase.ID, erpOrderCode = ordbase.ErpOrderCode };
			return JsonDate(result);
		}

		/// <summary>
		/// 生成订单
		/// </summary>
		/// <param name="orddiscountWebInfo"></param>
		/// <returns></returns>
		public ActionResult Generate(Ordbase ordbase) {
			Ordbase oldOrdbase = OrdbaseService.GetQuerySingleByErpOrderCode(ordbase.ErpOrderCode);
			oldOrdbase.BuyName = ordbase.BuyName;
			oldOrdbase.BuyMtel = ordbase.BuyMtel;
			oldOrdbase.BuyPostCode = ordbase.BuyPostCode;
			oldOrdbase.ProvinceID = ordbase.ProvinceID;
			oldOrdbase.Province = ordbase.Province;
			oldOrdbase.CityID = ordbase.CityID;
			oldOrdbase.City = ordbase.City;
			oldOrdbase.DistrictID = ordbase.DistrictID;
			oldOrdbase.District = ordbase.District;
			oldOrdbase.BuyAddressDetail = ordbase.BuyAddressDetail;
			oldOrdbase.BuyAddr = ordbase.Province + ordbase.City + ordbase.District + ordbase.BuyAddressDetail;
			oldOrdbase.PaymentMethod = ordbase.PaymentMethod;
			oldOrdbase.LogisticsID = ordbase.LogisticsID;
			oldOrdbase.Freight = ordbase.Freight;
			oldOrdbase.ShopID = ordbase.ShopID;
			oldOrdbase.OutOrderCode = ordbase.OutOrderCode;
			oldOrdbase.ExpectedDeliDate = ordbase.ExpectedDeliDate;
			oldOrdbase.DeliveryMethod = ordbase.DeliveryMethod;
			oldOrdbase.SinceSome = ordbase.SinceSome;
			oldOrdbase.InvoiceName = ordbase.InvoiceName;
			oldOrdbase.SellerRemark = ordbase.SellerRemark;
			oldOrdbase.BuyCodFee = ordbase.BuyCodFee;

			BaseResult resultInfo = OrdbaseManager.Generate(oldOrdbase, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
			var result = new { result = resultInfo.result, message = resultInfo.message, id = ordbase.ID, erpOrderCode = ordbase.ErpOrderCode };
			return JsonDate(result);
		}
	}
}
