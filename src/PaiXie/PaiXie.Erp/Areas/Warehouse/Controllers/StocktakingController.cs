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
	public class StocktakingController : BaseController
    {
        //
        // GET: /Warehouse/Stocktaking/

        public ActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// 盘点记录列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = "WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "'";
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "ID DESC";
			data.From = @"warehouseStocktaking";
			data.Select = "ID,BillNo,LocationID,LocationName,Remark,Status,ConfirmDate,CreatePerson,CreateDate";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseStocktaking> list = WarehouseStocktakingService.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		/// <summary>
		/// 添加盘点记录
		/// </summary>
		/// <returns></returns>
		public ActionResult Add() {
			List<WarehouseLocation> warehouseLocationList = WarehouseLocationService.GetManyWarehouseLocation(FormsAuth.GetWarehouseCode());
			ViewBag.WarehouseLocationList = warehouseLocationList;
			return View();
		}

		/// <summary>
		/// 保存盘点记录
		/// </summary>
		/// <param name="warehouseStocktakingWebInfo"></param>
		/// <returns></returns>
		public ActionResult Save(WarehouseStocktakingWebInfo warehouseStocktakingWebInfo) {
			BaseResult resultInfo = StocktakingManager.AddStocktaking(FormsAuth.GetWarehouseCode(), warehouseStocktakingWebInfo);
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 删除盘点记录
		/// </summary>
		/// <returns></returns>
		public ActionResult Delete(int id) {
			BaseResult resultInfo = StocktakingManager.DelStocktaking(id);
			if (resultInfo.result == 1) {
				resultInfo.message = "删除成功！";
			}
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 确认盘点记录
		/// </summary>
		/// <returns></returns>
		public ActionResult Submit(int id) {
			BaseResult resultInfo = StocktakingManager.SubmitStocktaking(id);
			if (resultInfo.result == 1) {
				resultInfo.message = "确认成功！";
			}
			return JsonDate(resultInfo);
		}
    }
}
