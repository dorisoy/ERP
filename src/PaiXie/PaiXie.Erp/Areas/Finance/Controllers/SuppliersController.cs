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
namespace PaiXie.Erp.Areas.Finance
{	 
	public class SuppliersController : BaseController
    {
		#region 供应商结算
		//
		// GET: /Finance/Suppliers/Index

		public ActionResult Index() {
			return View();
		} 
		#endregion

		#region 添加 供应商结算
		//
		// GET: /Finance/Suppliers/Edit
		public ActionResult Edit(string ids) {
			ViewBag.ids = ids;
			List<WarehouseOutInStock> list =
		    WarehouseOutInStockService.Getlistbyids(ids);
			decimal totalPrices = 0;
			for (int i = 0; i < list.Count(); i++) {			
				//金额
				if (list[i].BillType == (int)BillType.CGR) {
					//入库
					totalPrices+= ZConvert.StrToDecimal(WarehouseOutInStockItemService.GetSumPrice(list[i].BillNo), 0);
				}
				else {
					//出库
					decimal totalPrice = 0;
					List<WarehouseOutInStockItem> WarehouseOutInStockItemlist = WarehouseOutInStockItemService.GetWarehouseOutInStockItemList(list[i].ID);
					for (int z = 0; z < WarehouseOutInStockItemlist.Count(); z++) {
						WarehouseProductsBatch objWarehouseProductsBatch = WarehouseProductsBatchService.GetSingleWarehouseProductsBatch(WarehouseOutInStockItemlist[z].WarehouseCode, ZConvert.StrToInt(WarehouseOutInStockItemlist[z].ProductsSkuID, 0), WarehouseOutInStockItemlist[z].ProductsBatchCode);
						decimal Price = objWarehouseProductsBatch != null ? objWarehouseProductsBatch.CostPrice : 0;
						totalPrice += Price * WarehouseOutInStockItemlist[z].ProductsNum;
					}
					totalPrices+= totalPrice;
				}		
			}
			ViewBag.totalPrices = totalPrices;		
			return View();
		}
		#endregion

		#region 供应商结算列表
		//   /Finance/Suppliers/search
		/// <summary>
		/// 供应商结算
		/// </summary>
		/// <returns></returns>
		public ActionResult search() {
			//   Json格式的要求{total:22,rows:{}}
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = getwhereSql();	
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = " id desc";
			data.From = @"warehouseOutInStock";
			data.Select = "	*";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<SuppliersShList> list = BaseService<SuppliersShList>.GetQueryManyForPage(data, out total, null, null);
			for (int i = 0; i < list.Count(); i++) {
				//单据类型
				list[i].BillTypename = ((BillType)list[i].BillType).ToString() == "CGR"?"采购入库":"采购退回"; 
			//供应商名称
				Suppliers Suppliers= SuppliersService.GetQuerySingleByID(list[i].SuppliersID);
				list[i].Suppliersname = Suppliers != null ? Suppliers.Name : "";
			 //仓库名称
				PaiXie.Data.Warehouse Warehouse=  WarehouseService.GetwarehousebyCode(list[i].WarehouseCode);
				list[i].Warehousename = Warehouse != null ? Warehouse.Name : "";
				//数量
			   string tnum=	WarehouseOutInStockItemService.GetSumProductsNum(list[i].BillNo);
			   list[i].totalnum = ZConvert.StrToInt(tnum,0);
				//金额
				if (list[i].BillType == (int)BillType.CGR) {
					//入库
					list[i].totalPrice = ZConvert.StrToDecimal( WarehouseOutInStockItemService.GetSumPrice(list[i].BillNo),0);
				}
				else {
					//出库
					decimal totalPrice = 0;
					List<WarehouseOutInStockItem> WarehouseOutInStockItemlist = WarehouseOutInStockItemService.GetWarehouseOutInStockItemList(list[i].ID);
					for (int z = 0; z < WarehouseOutInStockItemlist.Count(); z++) {
						WarehouseProductsBatch objWarehouseProductsBatch = WarehouseProductsBatchService.GetSingleWarehouseProductsBatch(WarehouseOutInStockItemlist[z].WarehouseCode, ZConvert.StrToInt(WarehouseOutInStockItemlist[z].ProductsSkuID, 0), WarehouseOutInStockItemlist[z].ProductsBatchCode);
						decimal Price = objWarehouseProductsBatch != null ? objWarehouseProductsBatch.CostPrice : 0;
						totalPrice += Price * WarehouseOutInStockItemlist[z].ProductsNum;
					}
					list[i].totalPrice = totalPrice;
				}
				//核对状态
				list[i].shenheName = ((WarehouseOutInStockStatus)list[i].Status).ToString();
				//结算状态
				list[i].SettlementName = list[i].Settlement == 0 ? "待结算" : "已结算";

				SuppliersSettlement SuppliersSettlement=SuppliersSettlementService.GetQuerySingleBySourceID(list[i].ID);
				if (SuppliersSettlement != null) {
					//金额
					list[i].SettlementAmount = SuppliersSettlement.SettlementAmount;
					//交易号
					list[i].TradingNumber = SuppliersSettlement.TradingNumber;
					//结算备注
					list[i].JsRemark = SuppliersSettlement.Remark;
					//结算时间
					list[i].SettlementTime = SuppliersSettlement.SettlementTime;

				}

			}
			
			
			
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}
		private string getwhereSql() {
			string whereSql = "   BillType IN (" + (int)BillType.CGR + "," + (int)BillType.CGC+ ") ";
			whereSql += " and    Status IN (" + (int)WarehouseOutInStockStatus.待审核 + "," + (int)WarehouseOutInStockStatus.已审核 + ") ";

			if (Request["suppliersID"] != "0" && !string.IsNullOrEmpty(Request["suppliersID"])) {
				whereSql += " and    SuppliersID =" + Request["suppliersID"];
		
			}
			if (Request["billtype"] != "0" && !string.IsNullOrEmpty(Request["billtype"])) {
				whereSql += " and    BillType =" + Request["billtype"];

			}

			if (Request["Status"] != "0" && !string.IsNullOrEmpty(Request["Status"])) {
				whereSql += " and    Status =" + Request["Status"];

			}


			if (Request["Settlement"] != "-1" && !string.IsNullOrEmpty(Request["Settlement"])) {
				whereSql += " and    Settlement =" + Request["Settlement"];

			}

			return whereSql;
		}

		#endregion

		#region  供应商结算检查
		//
		// GET: /Finance/Suppliers/SuppliersCheck

		public ActionResult SuppliersCheck(string ids) {
			BaseResult BaseResult = new BaseResult();
			BaseResult = FinancialManager.SuppliersCheck(ids);
			return JsonDate(BaseResult);
		}
		#endregion

		#region 添加  供应商结算
		// GET: /Finance/Suppliers/SuppliersSettlement
		public ActionResult SuppliersSettlement(SuppliersSettlementList obj) {
			BaseResult BaseResult = new BaseResult();
			BaseResult = FinancialManager.SuppliersSettlement(obj);			
			return JsonDate(BaseResult);
		}
		#endregion
		
			
    }
}