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

namespace PaiXie.Erp.Areas.Warehouse {
	public class ConversionRuleController : BaseController {
		//
		// GET: /Warehouse/ConversionRule/

		public ActionResult Index() {
			WarehouseLocation warehouseLocation = WarehouseLocationService.GetSingleSubWarehouseLocation(FormsAuth.GetWarehouseCode(), (int)LocationType.中转区);
			ViewBag.WarehouseLocation = warehouseLocation;
			ViewBag.ProductsNum = WarehouseLocationProductsService.GetProductsNum(warehouseLocation.ParentID);
			return View();
		}

		/// <summary>
		/// 商品列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			string gridID = ZConvert.ToString(Request["gridID"]);
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int categoryID = ZConvert.StrToInt(Request["categoryID"]);
			int brandID = ZConvert.StrToInt(Request["brandID"]);
			int locationID = ZConvert.StrToInt(Request["locationID"]); 
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));

			string whereSql = " w.WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "'";

			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and p.Name like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and p.Code = '{0}'", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and p.No = '{0}'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and ps.Code = '{0}'", keyWord);
						break;
				}
			}
			if (categoryID > 0) {
				whereSql += string.Format(" and p.CategoryID = {0}", categoryID);
			}
			if (brandID > 0) {
				whereSql += string.Format(" and p.BrandID = {0}", brandID);
			}

			SelectBuilder data = new SelectBuilder();
			if (gridID == "grid") {
				whereSql += " and LocationID = " + locationID;
				data.Having = "";
				data.GroupBy = "p.Code,p.Name,ps.ID,ps.Saleprop,ps.Code";
				data.OrderBy = "";
				data.From = "warehouseLocationProducts w inner join productsSku ps on w.ProductsSkuID = ps.ID inner join products p on ps.ProductsID = p.ID";
				data.Select = "p.Code AS ProductsCode,p.Name,ps.ID,ps.Saleprop,ps.Code AS ProductsSkuCode,SUM(w.ZkNum) AS ZkNum";
				data.WhereSql = whereSql;
				data.PagingCurrentPage = pageIndex;
				data.PagingItemsPerPage = pageSize;
				int total = 0;
				List<WarehouseLocationProductsList> list = BaseService<WarehouseLocationProductsList>.GetQueryManyForPage(data, out total);
				var result = new { total = total, rows = list };
				return JsonDate(result);
			}
			else {
				data.Having = "";
				data.GroupBy = "w.RuleID";
				data.OrderBy = "";
				data.From = "warehouseConversionRuleItem w INNER JOIN productsSku ps ON w.ProductsSkuID = ps.ID INNER JOIN products p ON ps.ProductsID = p.ID";
				data.Select = "w.RuleID AS ID,w.RuleName AS Name";
				data.WhereSql = whereSql;
				data.PagingCurrentPage = pageIndex;
				data.PagingItemsPerPage = pageSize;
				int total = 0;
				List<WarehouseConversionRule> list = WarehouseConversionRuleService.GetQueryManyForPage(data, out total);
				var result = new { total = total, rows = list };
				return JsonDate(result);
			}

		}

		/// <summary>
		/// 添加转换规则
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Edit(int id = 0) {
			WarehouseConversionRule rule = new WarehouseConversionRule();
			List<WarehouseConversionRuleItem> leftRuleItemList = new List<WarehouseConversionRuleItem>();
			List<WarehouseConversionRuleItem> rightRuleItemList = new List<WarehouseConversionRuleItem>();
			if (id > 0) {
				rule = WarehouseConversionRuleService.GetSingleWarehouseConversionRule(FormsAuth.GetWarehouseCode(), id);
				List<WarehouseConversionRuleItem> RuleItemList = WarehouseConversionRuleItemService.GetManyWarehouseConversionRuleItem(FormsAuth.GetWarehouseCode(), id);
				leftRuleItemList = (from r in RuleItemList where r.ConversionWay == 0 select r).ToList<WarehouseConversionRuleItem>();
				rightRuleItemList = (from r in RuleItemList where r.ConversionWay == 1 select r).ToList<WarehouseConversionRuleItem>();
			}

			ViewBag.Rule = rule;
			ViewBag.LeftRuleItemList = leftRuleItemList;
			ViewBag.RightRuleItemList = rightRuleItemList;
			return View();
		}

		/// <summary>
		/// 添加转换商品
		/// </summary>
		/// <param name="location">商品添加的位置</param>
		/// <returns></returns>
		public ActionResult AddProducts(string location) {
			ViewBag.Location = location;
			return View();
		}

		/// <summary>
		/// 添加转换商品到页面列表
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="Num"></param>
		/// <returns></returns>
		public ActionResult SaveProducts(string Code, int Num) {
			BaseResult resultInfo = new BaseResult();
			WarehouseProductsSkuInfo warehouseProductsSkuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(FormsAuth.GetWarehouseCode(), Code);
			if (warehouseProductsSkuInfo == null) {
				resultInfo.result = 0;
				resultInfo.message = "SKU码不存在，请先创建！";
			}
			else {
				resultInfo.message = "添加成功！";
			}
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 添加转换规则（编辑）
		/// </summary>
		/// <param name="warehouseConversionRuleWebInfo"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Save(WarehouseConversionRuleWebInfo warehouseConversionRuleWebInfo) {
			BaseResult resultInfo = new BaseResult();
			if (warehouseConversionRuleWebInfo.RuleID == 0) {
				resultInfo = ConversionRuleManager.AddConversionRuleInfo(FormsAuth.GetWarehouseCode(), warehouseConversionRuleWebInfo);
				if (resultInfo.result == 1) {
					resultInfo.message = "添加成功！";
				}
			}
			else {
				resultInfo = ConversionRuleManager.UpdateConversionRuleInfo(FormsAuth.GetWarehouseCode(), warehouseConversionRuleWebInfo);
				if (resultInfo.result == 1) {
					resultInfo.message = "更新成功！";
				}
			}

			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public ActionResult Delete(string ids) {
			List<int> ruleIDList = new List<int>();
			ruleIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = ConversionRuleManager.DelConversionRuleInfo(FormsAuth.GetWarehouseCode(), ruleIDList);
			if (resultInfo.result == 1) {
				resultInfo.message = "删除成功！";
			}
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 获取商品转换规则列表，多个需要用户选择
		/// </summary>
		/// <param name="productsSkuID"></param>
		/// <returns></returns>
		public ActionResult getConversionRule(int productsSkuID) {
			List<WarehouseConversionRule> warehouseConversionRuleList = WarehouseConversionRuleService.GetManyWarehouseConversionRule(FormsAuth.GetWarehouseCode(), productsSkuID);
			return JsonDate(warehouseConversionRuleList);
		}

		/// <summary>
		/// 显示商品转换规则列表
		/// </summary>
		/// <param name="productsSkuID"></param>
		/// <returns></returns>
		public ActionResult SelectRule(int productsSkuID) {
			List<WarehouseConversionRule> warehouseConversionRuleList = WarehouseConversionRuleService.GetManyWarehouseConversionRule(FormsAuth.GetWarehouseCode(), productsSkuID);
			ViewBag.WarehouseConversionRuleList = warehouseConversionRuleList;
			return View();
		}

		/// <summary>
		/// 商品转换
		/// </summary>
		/// <param name="ruleID"></param>
		/// <returns></returns>
		public ActionResult Conversion(int ruleID) {
			List<WarehouseConversionRuleItemInfo> leftRuleItemList = new List<WarehouseConversionRuleItemInfo>();
			List<WarehouseConversionRuleItemInfo> rightRuleItemList = new List<WarehouseConversionRuleItemInfo>();
			if (ruleID > 0) {
				List<WarehouseConversionRuleItemInfo> RuleItemList = WarehouseConversionRuleItemService.GetManyWarehouseConversionRuleItemInfo(FormsAuth.GetWarehouseCode(), ruleID);
				leftRuleItemList = (from r in RuleItemList where r.ConversionWay == 0 select r).ToList<WarehouseConversionRuleItemInfo>();
				rightRuleItemList = (from r in RuleItemList where r.ConversionWay == 1 select r).ToList<WarehouseConversionRuleItemInfo>();
			}
			ViewBag.LeftRuleItemList = leftRuleItemList;
			ViewBag.RightRuleItemList = rightRuleItemList;
			return View();
		}

		/// <summary>
		/// 确认转换
		/// </summary>
		/// <param name="warehouseConversionRuleWebInfo"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveConversion(Data.WarehouseConversionRuleWebInfo warehouseConversionRuleWebInfo) {
			BaseResult resultInfo = ConversionRuleManager.SaveConversionInfo(FormsAuth.GetWarehouseCode(), warehouseConversionRuleWebInfo);
			if (resultInfo.result == 1) {
				resultInfo.message = "转换成功！";
			}
			return JsonDate(resultInfo);
		}
        
		/// <summary>
		/// 查看商品数量
		/// </summary>
		/// <param name="LocationID"></param>
		/// <param name="productsSkuID"></param>
		/// <returns></returns>
		public ActionResult Show(int locationID,int productsSkuID) {
			WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(locationID);
			ViewBag.WarehouseLocation = warehouseLocation;
			ViewBag.ProductsNum = WarehouseLocationProductsService.GetProductsNum(locationID);
			ViewBag.ProductsSkuID = productsSkuID;
			return View();
		}

		public ActionResult ShowSkuList(int locationID, int productsSkuID) {
			List<LocationProductsKucInfo> locationProductsKucInfo = WarehouseLocationProductsService.GetManyLocationProductsKucInfo(FormsAuth.GetWarehouseCode(), productsSkuID, locationID);
			return JsonDate(locationProductsKucInfo);
		}
	}
}
