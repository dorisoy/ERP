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

namespace PaiXie.Erp.Areas.Order.Controllers {
	public class DetailsController : BaseController {
		//
		// GET: /Order/Details/

		public ActionResult Index(string erpOrderCode) {
			Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode);
			if (ordbase == null) {
				ordbase = new Ordbase();
			}
			List<Ordremark> ordremarkList = OrdremarkService.GetManyOrdremark(erpOrderCode);
			ViewBag.Ordbase = ordbase;
			ViewBag.OrdremarkList = ordremarkList;
			return View();
		}

		/// <summary>
		/// 添加备注
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult AddRemark(string erpOrderCode) {
			List<Ordremark> ordremarkList = OrdremarkService.GetManyOrdremark(erpOrderCode);
			ViewBag.ErpOrderCode = erpOrderCode;
			ViewBag.OrdremarkList = ordremarkList;
			return View();
		}

		/// <summary>
		/// 保存备注
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult SaveRemark(string erpOrderCode, string content) {
			BaseResult resultInfo = new BaseResult();
			Ordremark ordremark = new Ordremark();
			ordremark.ErpOrderCode = erpOrderCode;
			ordremark.Content = content;
			ordremark.UserCode = FormsAuth.GetUserCode();
			ordremark.UserName = FormsAuth.GetUserName();
			ordremark.CreateDate = DateTime.Now;
			int ordremarkID = OrdremarkService.Add(ordremark);
			if (ordremarkID == 0) {
				resultInfo.result = 0;
				resultInfo.message = "添加失败！";
			}
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 商品列表
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult OrdItem(string erpOrderCode) {
			Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode);
			List<Orditem> orditemList = new List<Orditem>();
			List<Orddiscount> discountList = new List<Orddiscount>();
			if (ordbase == null) {
				ordbase = new Ordbase();
			}
			else {
				orditemList = OrditemService.GetManyOrditem(erpOrderCode);
				discountList = OrddiscountService.GetManyOrddiscount(erpOrderCode);
			}

			ViewBag.Ordbase = ordbase;
			ViewBag.OrditemList = orditemList;
			ViewBag.DiscountList = discountList;
			return View();
		}

		/// <summary>
		/// 挂起订单
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult Hang(string erpOrderCode) {
			ViewBag.ErpOrderCode = erpOrderCode;
			return View();
		}

		/// <summary>
		/// 保存挂起
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult SaveHang(string erpOrderCode, string hangRemark) {
			BaseResult resultInfo = OrdbaseManager.HandOrder(erpOrderCode, hangRemark, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 取消挂起
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult CancelHang(string erpOrderCode) {
			BaseResult resultInfo = OrdbaseManager.CancelHang(erpOrderCode, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 添加收款
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		public ActionResult AddCollection(string erpOrderCode, string amount) {
			ViewBag.ErpOrderCode = erpOrderCode;
			ViewBag.Amount = amount;
			return View();
		}

		/// <summary>
		/// 保存收款
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult SaveCollection(OrdaccountsBill accountsBill) {
			BaseResult resultInfo = new BaseResult();
			accountsBill.BillNo = PaiXie.Api.Bll.Sys.GetBillNo(BillType.SK.ToString());
			accountsBill.BillType = (int)BillType.SK;
			accountsBill.BillWay = 1;
			accountsBill.AssociatedCode = accountsBill.ErpOrderCode;
			accountsBill.CreateDate = DateTime.Now;
			accountsBill.CreatePerson = FormsAuth.GetUserCode();
			resultInfo = OrdbaseManager.AddAccountsBill(accountsBill, FormsAuth.GetUserCode());
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 添加退款
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		public ActionResult AddRefund(string erpOrderCode, string amount) {
			ViewBag.ErpOrderCode = erpOrderCode;
			ViewBag.Amount = Math.Abs(ZConvert.StrToDecimal(amount));
			return View();
		}

		/// <summary>
		/// 保存退款
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult SaveRefund(OrdaccountsBill accountsBill) {
			BaseResult resultInfo = new BaseResult();
			accountsBill.BillNo = PaiXie.Api.Bll.Sys.GetBillNo(BillType.TK.ToString());
			accountsBill.BillType = (int)BillType.TK;
			accountsBill.BillWay = -1;
			accountsBill.AssociatedCode = accountsBill.ErpOrderCode;
			accountsBill.CreateDate = DateTime.Now;
			accountsBill.CreatePerson = FormsAuth.GetUserCode();
			resultInfo = OrdbaseManager.AddAccountsBill(accountsBill, FormsAuth.GetUserCode());
			return JsonDate(resultInfo);
		}

	    /// <summary>
		/// 收退款记录
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult AccountsBill(string erpOrderCode) {
			Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode);
			List<OrdaccountsBill> accountsBillList = new List<OrdaccountsBill>();
			if (ordbase == null) {
				ordbase = new Ordbase();
			}
			else {
				accountsBillList = OrdaccountsBillService.GetManyOrdaccountsBill(erpOrderCode);
			}

			ViewBag.Ordbase = ordbase;
			ViewBag.AccountsBillList = accountsBillList;
			return View();
		}

		/// <summary>
		/// 订单日志
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult Log(string erpOrderCode) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = "SELECT ol.*,w.Name AS WarehouseName FROM ord_log ol LEFT JOIN warehouse w ON ol.WarehouseCode=w.Code WHERE ol.ErpOrderCode = @0 ORDER BY ol.ID DESC";
			List<OrdlogList> logList = BaseService<OrdlogList>.GetQueryMany(sqlStr, null, objects);
			ViewBag.ErpOrderCode = erpOrderCode;
			ViewBag.LogList = logList;
			return View();
		}

		/// <summary>
		/// 发货记录
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult Outbound(string erpOrderCode) {
			Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode);
			List<WarehouseOutbound> outboundList = new List<WarehouseOutbound>();
			List<WarehouseOutboundPickItemWebInfo> outboundItemList = new List<WarehouseOutboundPickItemWebInfo>();
			if (ordbase != null) {
				outboundList = WarehouseOutboundService.GetWarehouseOutboundByErpOrderCode(erpOrderCode);
				outboundItemList = OrditemService.GetManyOutboundItem(erpOrderCode);
			}
			else {
				ordbase = new Ordbase();
			}
			ViewBag.Ordbase = ordbase;
			ViewBag.OutboundList = outboundList;
			ViewBag.OutboundItemList = outboundItemList;
			return View();
		}

		/// <summary>
		/// 售后记录
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult OrderRefund(string erpOrderCode) {
			Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode);
			List<OrdRefundList> ordRefundList = new List<OrdRefundList>();
			List<OrdRefundItemList> ordRefundItemList = new List<OrdRefundItemList>();
			if (ordbase != null) {
				ordRefundList = OrdrefundService.GetManyOrdrefund(erpOrderCode);
				ordRefundItemList = OrdrefundItemService.GetManyOrdRefundItemList(erpOrderCode);
				foreach (var refund in ordRefundList) {
					refund.StatusName = OrdrefundManager.GetStatusName(refund.Status);
				}
			}
			else {
				ordbase = new Ordbase();
			}
			ViewBag.Ordbase = ordbase;
			ViewBag.OrdRefundList = ordRefundList;
			ViewBag.OrdRefundItemList = ordRefundItemList;
			return View();
		}

		/// <summary>
		/// 取消订单
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult CancelOrder(string erpOrderCode) {
			BaseResult resultInfo = OrdbaseManager.Cancel(erpOrderCode, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
			return JsonDate(resultInfo);
		}
	}
}
