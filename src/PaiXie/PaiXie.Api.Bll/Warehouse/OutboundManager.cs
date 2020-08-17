using PaiXie.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using PaiXie.Service;
using FluentData;
using PaiXie.Utils;
using System.Data;

namespace PaiXie.Api.Bll {

	/// <summary>
	/// 出库单管理类
	/// </summary>
	public class OutboundManager {

		#region 生成出库单

	    /// <summary>
		/// 生成出库单
		/// </summary>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <param name="distributionWarehouseWebInfo"></param>
		/// <returns></returns>
		public static BaseResult CreateOutbound(string userCode, string userName, DistributionWarehouseWebInfo distributionWarehouseWebInfo) { 
		BaseResult resultInfo = new BaseResult();
			Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(distributionWarehouseWebInfo.ErpOrderCode);
			if ((ordbase.OrderStatus != (int)OrdBaseStatus.待审核 && ordbase.OrderStatus != (int)OrdBaseStatus.发货中 && ordbase.OrderStatus != (int)OrdBaseStatus.部分发货)) {
				resultInfo.result = -1;
				resultInfo.message = "订单状态错误！";
				return resultInfo;
			}
			if (ordbase.IsHang == 1) {
				resultInfo.result = -1;
				resultInfo.message = "挂起状态不能生成出库单！";
				return resultInfo;
			}
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					List<Orditem> orditemList = new List<Orditem>();
					
					for (int i = 0; i < distributionWarehouseWebInfo.Num.Length; i++) {
						int num = distributionWarehouseWebInfo.Num[i];
						int orditemID = distributionWarehouseWebInfo.OrditemID[i];
						string warehouseCode = distributionWarehouseWebInfo.WarehouseCode[i];
						if (num > 0) {
							Orditem orditem = OrditemService.GetQuerySingleByID(orditemID, context);
							orditem.WarehouseCode = warehouseCode;
							orditem.ProductsNum = num;
							orditem.CreatePerson = userCode;
							orditem.CreateDate = DateTime.Now;
							orditem.UpdatePerson = userCode;
							orditem.UpdateDate = DateTime.Now;
							orditemList.Add(orditem);
						}
					}

					foreach (var orditemID in distributionWarehouseWebInfo.OrditemID.Distinct().ToArray()) {
						Orditem orditem = OrditemService.GetQuerySingleByID(orditemID, context);
						List<Orditem> filter = orditemList.Where(r => r.ID == orditemID).ToList();
						if (filter.Count > 0) {
							int totalNum = filter.Sum(r => r.ProductsNum);
							if (orditem.ProductsNum == totalNum) {
								for (int i = 0; i < filter.Count; i++) {
									if (i != 0) {
										filter[i].ID = 0;
									}
									else {
										filter[i].OutboundID = -1;
									}
								}
							}
							else {
								filter.ForEach(r => r.ID = 0);
								orditem.WarehouseCode = distributionWarehouseWebInfo.WarehouseCode[0];
								orditem.ProductsNum = orditem.ProductsNum - totalNum;
								orditemList.Add(orditem);
							}
						}
					}

					foreach (var warehouseCode in distributionWarehouseWebInfo.WarehouseCode.Distinct().ToArray()) {
						List<Orditem> filter = orditemList.Where(r => r.WarehouseCode == warehouseCode).ToList();
						if (filter.Count > 0) {
							resultInfo = CreateOutbound(userCode, userName, orditemList.Where(r => r.WarehouseCode == warehouseCode).ToList(), context);
						}
						if (resultInfo.result == 0) break;
					}

					ordbase = OrdbaseService.GetQuerySingleByID(ordbase.ID, context);
					if (resultInfo.result == 1) {
						int totalNum = OrditemService.GetManyOrditem(ordbase.ID, context).Sum(r => r.ProductsNum);
						if (ordbase.ProductsNum != totalNum) {
							resultInfo.result = 0;
							resultInfo.message = "分配数量异常，请刷新页面！";
						}
					}
					if (resultInfo.result == 1) {
						if (ordbase.OrderStatus == (int)OrdBaseStatus.待审核 || ordbase.IsReject == 1) {
							if (ordbase.OrderStatus == (int)OrdBaseStatus.待审核) {
								ordbase.OrderStatus = (int)OrdBaseStatus.发货中;
							}
							ordbase.IsReject = 0;
							ordbase.RejectRemark = "";
							ordbase.UpdateDate = DateTime.Now;
							ordbase.UpdatePerson = userCode;
							int rowsAffected = OrdbaseService.Update(ordbase, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "更新订单状态失败！";
							}
						}
					}

					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "生成出库单", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}

		/// <summary>
		/// 生成出库单
		/// </summary>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <param name="erpOrderCode"></param>
		/// <param name="warehouseCode"></param>
		/// <returns></returns>
		public static BaseResult CreateOutbound(string userCode, string userName, string erpOrderCode, string warehouseCode) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode);
					if ((ordbase.OrderStatus != (int)OrdBaseStatus.待审核 && ordbase.OrderStatus != (int)OrdBaseStatus.发货中 && ordbase.OrderStatus != (int)OrdBaseStatus.部分发货)) {
						resultInfo.result = -1;
						resultInfo.message = "订单状态错误！";
						return resultInfo;
					}
					if (ordbase.IsHang == 1) {
						resultInfo.result = -1;
						resultInfo.message = "挂起状态不能生成出库单！";
						return resultInfo;
					}
					List<Orditem> orditemList = OrditemService.GetManyOrditem(erpOrderCode, context).Where(r => r.OutboundID == 0).ToList();
					foreach (var item in orditemList) {
						item.WarehouseCode = warehouseCode;
						item.OutboundID = -1;
					}
					resultInfo = CreateOutbound(userCode, userName, orditemList, context);
					if (resultInfo.result == 1) {
					    ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode, context);
						if (ordbase.OrderStatus == (int)OrdBaseStatus.待审核 || ordbase.IsReject == 1) {
							if (ordbase.OrderStatus == (int)OrdBaseStatus.待审核) {
								ordbase.OrderStatus = (int)OrdBaseStatus.发货中;
							}
							ordbase.IsReject = 0;
							ordbase.RejectRemark = "";
							ordbase.UpdateDate = DateTime.Now;
							ordbase.UpdatePerson = userCode;
							int rowsAffected = OrdbaseService.Update(ordbase, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "更新订单状态失败！";
							}
						}
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "生成出库单", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}

		/// <summary>
		/// 生成出库单
		/// </summary>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <param name="orditemList"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static BaseResult CreateOutbound(string userCode, string userName, List<Orditem> orditemList, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				int ordbaseID = orditemList[0].OrdbaseID;
				string warehouseCode = orditemList[0].WarehouseCode;
				Ordbase ordbase = OrdbaseService.GetQuerySingleByID(ordbaseID, context);

				#region 添加出库单

				string billNo = Sys.GetBillNo(BillType.XSC.ToString());
				WarehouseOutbound outbound = new WarehouseOutbound();
				outbound.BillNo = billNo;
				outbound.BillType = (int)BillType.XSC;
				outbound.WarehouseCode = warehouseCode;
				outbound.ErpOrderCode = ordbase.ErpOrderCode;
				outbound.OutOrderCode = ordbase.OutOrderCode;
				outbound.ShopID = ordbase.ShopID;
				outbound.OrderSource = ordbase.OrderSource;
				outbound.OrderType = ordbase.OrderType;
				outbound.BuyAddr = ordbase.BuyAddr;
				outbound.BuyAddressDetail = ordbase.BuyAddressDetail;
				outbound.BuyMtel = ordbase.BuyMtel;
				outbound.BuyName = ordbase.BuyName;
				outbound.BuyNickName = ordbase.BuyNickName;
				outbound.BuyPostCode = ordbase.BuyPostCode;
				outbound.BuyTel = ordbase.BuyTel;
				outbound.BuyMessage = ordbase.BuyMessage;
				outbound.SellerRemark = ordbase.SellerRemark;
				outbound.ProvinceID = ordbase.ProvinceID;
				outbound.CityID = ordbase.CityID;
				outbound.DistrictID = ordbase.DistrictID;
				outbound.Province = ordbase.Province;
				outbound.City = ordbase.City;
				outbound.District = ordbase.District;
				outbound.Status = (int)WarehouseOutboundStatus.待拣货;
				outbound.PayDate = ordbase.PayDate;
				outbound.PaymentMethod = ordbase.PaymentMethod;
				outbound.IsCod = ordbase.IsCod;
				outbound.CodStatus = ordbase.CodStatus;
				outbound.BuyCodFee = ordbase.BuyCodFee;
				outbound.TradingNumber = ordbase.TradingNumber;
				outbound.PaymentAccount = ordbase.PaymentAccount;
				outbound.IsApplyRefund = ordbase.IsApplyRefund;
				outbound.ExpectedDeliDate = ordbase.ExpectedDeliDate;
				outbound.DeliveryMethod = ordbase.DeliveryMethod;
				outbound.SinceSome = ordbase.SinceSome;
				outbound.IsNeedInvoice = ordbase.IsNeedInvoice;
				outbound.InvoiceName = ordbase.InvoiceName;
				outbound.InvoiceInfo = ordbase.InvoiceInfo;
				List<WarehouseExpress> expressList = WarehouseExpressService.GetManyExpress(warehouseCode, ordbase.LogisticsID, context);
				if (expressList.Count > 0) outbound.ExpressID = expressList[0].ID;
				if (WarehouseOutboundService.GetWarehouseOutboundByBillNoOrErpOrderCode(warehouseCode, billNo, context).Count == 0) {
					outbound.Freight = ordbase.Freight;
				}
				outbound.LogisticsID = ordbase.LogisticsID;
				outbound.CreatePerson = userCode;
				outbound.CreateDate = DateTime.Now;
				int outboundID = WarehouseOutboundService.Add(outbound, context);
				outbound.ID = outboundID;
				if (outboundID == 0) {
					resultInfo.result = 0;
					resultInfo.message = "发货仓库[" + WarehouseService.GetwarehousebyCode(warehouseCode).Name + "]添加出库单失败！";
				}
				else {
					#region 订单操作日志

					string msg = string.Format("添加出库单[{0}]", outbound.BillNo);
					resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, msg, context, outbound.WarehouseCode, outbound.BillNo);

					#endregion
				}

				#endregion

				foreach (var item in orditemList) {
					#region 添加出库单商品

					if (item.ID > 0) {
						if (item.OutboundID == -1) {
							item.OutboundID = outbound.ID;
							item.OutboundBillNo = outbound.BillNo;
						}
						else {
							item.WarehouseCode = "";
						}
						int rowsAffected = OrditemService.Update(item, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "发货仓库[" + WarehouseService.GetwarehousebyCode(warehouseCode).Name + "]添加出库单商品SKU码[" + item.ProductsSkuCode + "]失败！";
						}
					}
					else {
						item.OutboundID = outbound.ID;
						item.OutboundBillNo = outbound.BillNo;
						int orditemID = OrditemService.Add(item, context);
						item.ID = orditemID;
						if (orditemID == 0) {
							resultInfo.result = 0;
							resultInfo.message = "发货仓库[" + WarehouseService.GetwarehousebyCode(warehouseCode).Name + "]添加出库单商品SKU码[" + item.ProductsSkuCode + "]失败！";
						}
					}

					if (resultInfo.result == 1 && item.OutboundID > 0) {
						List<WarehouseOutboundPickItem> outboundPickItemList = new List<WarehouseOutboundPickItem>();
						List<WarehouseLocationProducts> locationProductsList = WarehouseLocationProductsService.GetManyLocationProducts(item.WarehouseCode, item.ProductsSkuID, context);
						int num = 0;
						int productsNum = item.ProductsNum;
						foreach (var locationProducts in locationProductsList) {
							if (productsNum <= locationProducts.KyNum - locationProducts.ZyNum) {
								num = productsNum;
								productsNum = 0;
							}
							else {
								productsNum -= locationProducts.KyNum - locationProducts.ZyNum;
								num = locationProducts.KyNum - locationProducts.ZyNum;
							}
							int rowsAffected = WarehouseLocationProductsService.IncreaseZyNum(userCode, warehouseCode, item.ProductsSkuID, locationProducts.LocationID, locationProducts.ProductsBatchID, num, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "发货仓库[" + WarehouseService.GetwarehousebyCode(warehouseCode).Name + "]商品SKU码[" + item.ProductsSkuCode + "]增加库位占用失败！";
							}
							if (resultInfo.result == 1) {
								rowsAffected = WarehouseProductsBatchService.IncreaseZyNum(userCode, locationProducts.ProductsBatchID, num, context);
								if (rowsAffected == 0) {
									resultInfo.result = 0;
									resultInfo.message = "发货仓库[" + WarehouseService.GetwarehousebyCode(warehouseCode).Name + "]商品SKU码[" + item.ProductsSkuCode + "]增加批次占用失败！";
								}
							}
							if (resultInfo.result == 1) {
								WarehouseOutboundPickItem outboundPickItem = new WarehouseOutboundPickItem();
								outboundPickItem.ErpOrderCode = item.ErpOrderCode;
								outboundPickItem.OutboundID = outbound.ID;
								outboundPickItem.OutboundBillNo = outbound.BillNo;
								outboundPickItem.OrditemID = item.ID;
								outboundPickItem.WarehouseCode = warehouseCode;
								outboundPickItem.ProductsID = item.ProductsID;
								outboundPickItem.ProductsName = item.ProductsName;
								outboundPickItem.ProductsCode = item.ProductsCode;
								outboundPickItem.ProductsNo = item.ProductsNo;
								outboundPickItem.ProductsSkuID = item.ProductsSkuID;
								outboundPickItem.ProductsSkuCode = item.ProductsSkuCode;
								outboundPickItem.ProductsSkuSaleprop = item.ProductsSkuSaleprop;
								outboundPickItem.ProductsBatchID = locationProducts.ProductsBatchID;
								outboundPickItem.ProductsBatchCode = locationProducts.ProductsBatchCode;
								WarehouseLocation location = WarehouseLocationService.GetQuerySingleByID(locationProducts.LocationID, context);
								outboundPickItem.LocationID = location.ID;
								outboundPickItem.LocationCode = location.Code;
								outboundPickItem.LocationName = location.Name;
								outboundPickItem.Num = num;
								outboundPickItem.ActualSellingPrice = item.ActualSellingPrice;
								outboundPickItem.CreatePerson = userCode;
								outboundPickItem.CreateDate = DateTime.Now;
								outboundPickItemList.Add(outboundPickItem);
							}
							if (productsNum == 0 || resultInfo.result == 0) break;
						}
						if (resultInfo.result == 1 && productsNum != 0) {
							int rowsAffected = WarehouseBookingProductsSkuService.IncreaseZyNum(userCode, warehouseCode, item.ProductsSkuID, productsNum, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "发货仓库[" + WarehouseService.GetwarehousebyCode(warehouseCode).Name + "]商品SKU码[" + item.ProductsSkuCode + "]增加预售占用失败！";
							}
							else {
								WarehouseOutboundPickItem outboundPickItem = new WarehouseOutboundPickItem();
								outboundPickItem.ErpOrderCode = item.ErpOrderCode;
								outboundPickItem.OutboundID = outbound.ID;
								outboundPickItem.OutboundBillNo = outbound.BillNo;
								outboundPickItem.OrditemID = item.ID;
								outboundPickItem.WarehouseCode = warehouseCode;
								outboundPickItem.ProductsID = item.ProductsID;
								outboundPickItem.ProductsName = item.ProductsName;
								outboundPickItem.ProductsCode = item.ProductsCode;
								outboundPickItem.ProductsNo = item.ProductsNo;
								outboundPickItem.ProductsSkuID = item.ProductsSkuID;
								outboundPickItem.ProductsSkuCode = item.ProductsSkuCode;
								outboundPickItem.ProductsSkuSaleprop = item.ProductsSkuSaleprop;
								outboundPickItem.ProductsBatchID = 0;
								outboundPickItem.ProductsBatchCode = "";
								outboundPickItem.LocationID = 0;
								outboundPickItem.LocationCode = "";
								outboundPickItem.LocationName = "";
								outboundPickItem.Num = item.ProductsNum;
								outboundPickItem.CreatePerson = userCode;
								outboundPickItem.CreateDate = DateTime.Now;
								outboundPickItemList.Add(outboundPickItem);

								outbound.IsWaitPurchase = (int)IsEnable.是;
							}
						}
						if (resultInfo.result == 1) {
							foreach (var outboundPickItem in outboundPickItemList) {
								int outboundPickItemID = WarehouseOutboundPickItemService.Add(outboundPickItem, context);
								if (outboundPickItemID == 0) {
									resultInfo.result = 0;
									resultInfo.message = "发货仓库[" + WarehouseService.GetwarehousebyCode(warehouseCode).Name + "]商品SKU码[" + item.ProductsSkuCode + "]添加拣货明细失败！";
									break;
								}
							}
						}

						if (resultInfo.result == 1) {
							if (WarehouseOutboundService.UpdateProductsNumWeightAndAmount(userCode, outbound.ID, context) == 0) {
								resultInfo.result = 0;
								resultInfo.message = "更新出库单[" + outbound.BillNo + "]失败！";
							}

							if (resultInfo.result == 1 && outbound.IsWaitPurchase == 1) {
								if (WarehouseOutboundService.UpdateIsWaitPurchase(userCode, outbound.ID, (int)IsEnable.是, context) == 0) {
									resultInfo.result = 0;
									resultInfo.message = "更新出库单[" + outbound.BillNo + "]失败！";
								}
							}
						}
					}

					if (resultInfo.result == 1) {
						Ordoccupy ordoccupy = OrdoccupyService.GetSingleOrdoccupy(item.ID, context);
						if (ordoccupy != null) {
							if (item.ProductsNum == ordoccupy.Num) {
								int rowsAffected = OrdoccupyService.DelByID(ordoccupy.ID, context);
								if (rowsAffected == 0) {
									resultInfo.result = 0;
									resultInfo.message = "删除商品SKU码[" + item.ProductsSkuCode + "]订单占用失败！";
								}
							}
							else {
								ordoccupy.Num = item.ProductsNum;
								int rowsAffected = OrdoccupyService.Update(ordoccupy, context);
								if (rowsAffected == 0) {
									resultInfo.result = 0;
									resultInfo.message = "更新商品SKU码[" + item.ProductsSkuCode + "]订单占用失败！";
								}
							}
						}
					}

					#endregion
				}
			}
			catch (Exception ex) {
				throw new Exception(ex.Message + ex.StackTrace);;
			}
			return resultInfo;
		}

		#endregion

		#region 获取出库单状态名称

		/// <summary>
		/// 获取出库单状态名称
		/// </summary>
		/// <param name="status">出库单状态枚举值</param>
		/// <returns></returns>
		public static string GetStatusName(int status) {
			string statusName = string.Empty;
			switch (status) {
				case (int)WarehouseOutboundStatus.待拣货:
					statusName = WarehouseOutboundStatus.待拣货.ToString();
					break;
				case (int)WarehouseOutboundStatus.待打印:
					statusName = WarehouseOutboundStatus.待打印.ToString();
					break;
				case (int)WarehouseOutboundStatus.待发货:
					statusName = WarehouseOutboundStatus.待发货.ToString();
					break;
				case (int)WarehouseOutboundStatus.已发货:
					statusName = WarehouseOutboundStatus.已发货.ToString();
					break;
				case (int)WarehouseOutboundStatus.已取消:
					statusName = WarehouseOutboundStatus.已取消.ToString();
					break;
			}
			return statusName;
		}

		#endregion

		#region 获取安排打印出库单的快递个数

		/// <summary>
		/// 获取安排打印出库单的快递个数
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="ids">出库单主键ID，多个以半角逗号隔开</param>
		/// <returns></returns>
		public static int GetExpressCount(string userCode, string ids) {
			int expressCount = 0;
			try {
				expressCount = WarehouseOutboundService.GetExpressCount(ids);
			}
			catch (Exception ex) {
				Sys.SaveErrorLog(ex, "获取安排打印出库单的快递个数", userCode);
			}
			return expressCount;
		}

		#endregion

		#region 驳回出库单

		/// <summary>
		/// 驳回出库单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="idList">出库单ID列表</param>
		/// <param name="rejectRemark">驳回备注</param>
		/// <returns></returns>
		public static BaseResult Reject(string userCode, string userName, string warehouseCode, List<int> idList, string rejectRemark) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					List<WarehouseOutbound> outboundList = WarehouseOutboundService.GetWarehouseOutboundList(warehouseCode, idList, context);
					foreach (var outbound in outboundList) {
						if (outbound.Status == (int)WarehouseOutboundStatus.待拣货) {
							int count = WarehouseOutboundService.DelByID(warehouseCode, outbound.ID, (int)WarehouseOutboundStatus.待拣货, context);
							if (count > 0) {
								#region 遍历拣货明细 扣减占用
								List<WarehouseOutboundPickItem> pickItemList = WarehouseOutboundPickItemService.GetQueryManyByOutboundID(outbound.ID, context);
								if (pickItemList.Count() == 0) {
									resultInfo.result = 0;
									resultInfo.message = "出库单 " + outbound.BillNo + " 没有拣货明细！";
									break;
								}
								foreach (var pickItem in pickItemList) {
									if (pickItem.LocationID > 0) {
										count = WarehouseProductsBatchService.DeductionZyNum(userCode, pickItem.ProductsBatchID, pickItem.Num, context);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "出库单 " + outbound.BillNo + " 扣减批次占用失败！";
											break;
										}
										count = WarehouseLocationProductsService.DeductionZyNum(userCode, pickItem.WarehouseCode, pickItem.ProductsSkuID, pickItem.LocationID, pickItem.ProductsBatchID, pickItem.Num, context);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "出库单 " + outbound.BillNo + " 扣减库位占用失败！";
											break;
										}
									}
									else {
										count = WarehouseBookingProductsSkuService.DeductionZyNum(userCode, pickItem.WarehouseCode, pickItem.ProductsSkuID, pickItem.Num, context);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "出库单 " + outbound.BillNo + " 扣减预售占用失败！";
											break;
										}
									}
								}
								#endregion
								if (resultInfo.result == 1) {
									WarehouseOutboundPickItemService.DelByOutboundID(warehouseCode, outbound.ID, context);
									//获取未生成出库单的订单明细(驳回出库单明细的时候，如果订单里有相同类型明细，进行合并)
									List<Orditem> ordItemListNotOutbound = OrditemService.GetOrdItemListNotOutbound(outbound.ErpOrderCode, context);
									#region 遍历出库单明细，创建或更新下单占用
									List<Orditem> ordItemList = OrditemService.GetQueryManyByOutboundID(outbound.ID, context);
									if (ordItemList.Count() == 0) {
										resultInfo.result = 0;
										resultInfo.message = "出库单 " + outbound.BillNo + " 没有商品！";
										break;
									}
									foreach (var ordItem in ordItemList) {
										List<Orditem> repeatOrdItemList = ordItemListNotOutbound.Where(o => o.ProductsSkuID == ordItem.ProductsSkuID && o.AddType == ordItem.AddType).ToList();
										if (repeatOrdItemList.Count() > 0) {
											#region 更新订单明细数量和下单占用数量，删除驳回的出库单明细
											int sameSkuOrdItemID = repeatOrdItemList[0].ID;
											count = OrditemService.UpdateProductsNum(userCode, sameSkuOrdItemID, ordItem.ProductsNum, context);
											if (count == 0) {
												resultInfo.result = 0;
												resultInfo.message = "出库单 " + ordItem.OutboundBillNo + " SKU码 " + ordItem.ProductsSkuCode + " 更新订单明细数量失败！";
												break;
											}
											count = OrdoccupyService.UpdateNum(userCode, sameSkuOrdItemID, ordItem.ProductsNum, context);
											if (count == 0) {
												resultInfo.result = 0;
												resultInfo.message = "出库单 " + ordItem.OutboundBillNo + " SKU码 " + ordItem.ProductsSkuCode + " 更新订单占用失败！";
												break;
											}
											count = OrditemService.DelByID(ordItem.ID, context);
											if (count == 0) {
												resultInfo.result = 0;
												resultInfo.message = "出库单 " + ordItem.OutboundBillNo + " SKU码 " + ordItem.ProductsSkuCode + " 删除出库单明细失败！";
												break;
											}
											#endregion
										}
										else {
											#region 清空出库单明细的仓库编码、出库单号、出库单ID，创建订单占用
											count = OrditemService.ClearOutboundInfo(userCode, ordItem.ID);
											if (count == 0) {
												resultInfo.result = 0;
												resultInfo.message = "出库单 " + ordItem.OutboundBillNo + " SKU码 " + ordItem.ProductsSkuCode + " 清空出库单信息失败！";
												break;
											}
											Ordoccupy objOrdccupy = new Ordoccupy();
											objOrdccupy.ErpOrderCode = ordItem.ErpOrderCode;
											objOrdccupy.Num = ordItem.ProductsNum;
											objOrdccupy.OrditemID = ordItem.ID;
											objOrdccupy.ProductsID = ordItem.ProductsID;
											objOrdccupy.ProductsSkuID = ordItem.ProductsSkuID;
											objOrdccupy.Remark = "驳回出库单[" + ordItem.OutboundBillNo + "]创建下单占用";
											objOrdccupy.CreatePerson = userCode;
											objOrdccupy.CreateDate = DateTime.Now;
											int ordoccupyID = OrdoccupyService.Add(objOrdccupy, context);
											if (ordoccupyID == 0) {
												resultInfo.result = 0;
												resultInfo.message = "出库单 " + ordItem.OutboundBillNo + " SKU码 " + ordItem.ProductsSkuCode + " 创建下单占用失败！";
												break;
											}
											#endregion
										}
									}
									#endregion
									if (resultInfo.result == 1) {
										#region 标记订单为驳回，并记录驳回备注
										count = OrdbaseService.UpdateRejectStatus(outbound.ErpOrderCode, rejectRemark, context);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "出库单 " + outbound.BillNo + "修改订单为已驳回失败！";
											break;
										}
										#endregion
									}
									if (resultInfo.result == 1) {
										#region 保存订单操作日志
										resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "驳回出库单[" + outbound.BillNo + "]备注[" + rejectRemark + "]", context, warehouseCode, outbound.BillNo);
										if (resultInfo.result != 1) {
											break;
										}
										#endregion
									}
									else {
										//遍历出库单明细，创建或更新下单占用出错
										break;
									}
								}
								else {
									//遍历拣货明细扣减占用出错
									break;
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "出库单 " + outbound.BillNo + " 驳回失败，请检查出库单状态！";
								break;
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "出库单 " + outbound.BillNo + " 驳回失败，出库单状态已经不是" + WarehouseOutboundStatus.待拣货.ToString() + "！";
							break;
						}
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "驳回出库单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存打印批次，并将出库单状态改为待打印

		/// <summary>
		/// 保存打印批次，并将出库单状态改为待打印
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="idList">出库单ID列表</param>
		/// <param name="expressID">下单选择快递ID</param>
		/// <param name="deliveryExpressID">实际发货快递ID，传0表示使用下单选择快递发货</param>
		/// <returns></returns>
		public static BaseResult SavePrintBatch(string userCode, string userName, string warehouseCode, List<int> idList, int deliveryExpressID) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					string printBatchCode = NewKey.datetime();
					List<WarehouseOutbound> outboundList = WarehouseOutboundService.GetWarehouseOutboundList(warehouseCode, idList, context);
					foreach (var outbound in outboundList) {
						WarehouseOutboundPrintBatch outboundPrintBatch = new WarehouseOutboundPrintBatch();
						outboundPrintBatch.Code = printBatchCode;
						outboundPrintBatch.OutboundID = outbound.ID;
						outboundPrintBatch.CreatePerson = userCode;
						outboundPrintBatch.CreateDate = DateTime.Now;
						int outboundPrintBatchID = WarehouseOutboundPrintBatchService.Add(outboundPrintBatch, context);
						if (outboundPrintBatchID > 0) {
							int count = WarehouseOutboundService.ArrangePrint(userCode, warehouseCode, outbound.ID, printBatchCode, deliveryExpressID, context);
							if (count > 0) {
								#region 保存订单操作日志
								resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "出库单[" + outbound.BillNo + "]安排打印", context, warehouseCode, outbound.BillNo);
								if (resultInfo.result != 1) {
									break;
								}
								#endregion
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "出库单 " + outbound.BillNo + " 更新状态为" + WarehouseOutboundStatus.待打印.ToString() + "失败，请检查是否挂起或申请退款！";
								break;
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "出库单 " + outbound.BillNo + " 保存打印批次失败！";
							break;
						}
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存打印批次，并将出库单状态改为待打印", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 挂起、取消挂起

		public static BaseResult Hang(string userCode, string userName, string warehouseCode, int type, int id, string hangRemark) {
			BaseResult resultInfo = new BaseResult();
			int isHang = 0;
			string operaTxt = string.Empty;
			string logStr = string.Empty;
			if (type == 0) {
				operaTxt = "标记挂起";
				isHang = 1;
				logStr = "，挂起备注：" + hangRemark;
			}
			else {
				operaTxt = "取消挂起";
			}
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseOutbound outbound = WarehouseOutboundService.GetQuerySingleByID(id, context);
					int count = WarehouseOutboundService.UpdateIsHang(userCode, warehouseCode, id, isHang, hangRemark, context);
					if (count > 0) {
						#region 保存订单操作日志
						resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "出库单[" + outbound.BillNo + "]" + operaTxt + logStr, context, warehouseCode, outbound.BillNo);
						#endregion
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = operaTxt + "失败！";
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				Sys.SaveErrorLog(ex, operaTxt, userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 待打印返回待拣货

		/// <summary>
		/// 待打印返回待拣货
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="idList">出库单ID列表</param>
		/// <returns></returns>
		public static BaseResult ReturnWaitPick(string userCode, string userName, string warehouseCode, List<int> idList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					List<WarehouseOutbound> outboundList = WarehouseOutboundService.GetWarehouseOutboundList(warehouseCode, idList, context);
					foreach (var outbound in outboundList) {
						if (outbound.Status == (int)WarehouseOutboundStatus.待打印) {
							int count = WarehouseOutboundService.ReturnWaitPick(userCode, warehouseCode, outbound.ID, context);
							if (count > 0) {
								//删除打印批次
								count = WarehouseOutboundPrintBatchService.DelByOutboundID(outbound.ID, context);
								if (count > 0) {
									#region 保存订单操作日志
									resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "出库单[" + outbound.BillNo + "]返回待拣货", context, warehouseCode, outbound.BillNo);
									if (resultInfo.result != 1) {
										break;
									}
									#endregion
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "出库单 " + outbound.BillNo + " 删除打印批次失败！";
									break;
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "出库单 " + outbound.BillNo + " 返回失败，请检查出库单状态！";
								break;
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "出库单 " + outbound.BillNo + " 返回失败，出库单状态已经不是" + WarehouseOutboundStatus.待打印.ToString() + "！";
							break;
						}
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "待打印返回待拣货", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 待发货返回待打印

		/// <summary>
		/// 待发货返回待打印
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="idList">出库单ID列表</param>
		/// <returns></returns>
		public static BaseResult ReturnWaitPrint(string userCode, string userName, string warehouseCode, List<int> idList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					List<WarehouseOutbound> outboundList = WarehouseOutboundService.GetWarehouseOutboundList(warehouseCode, idList, context);
					foreach (var outbound in outboundList) {
						if (outbound.Status == (int)WarehouseOutboundStatus.待发货) {
							int count = WarehouseOutboundService.ReturnWaitPrint(userCode, warehouseCode, outbound.ID, context);
							if (count > 0) {
								#region 保存订单操作日志
								resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "出库单[" + outbound.BillNo + "]返回待打印", context, warehouseCode, outbound.BillNo);
								if (resultInfo.result != 1) {
									break;
								}
								#endregion
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "出库单 " + outbound.BillNo + " 返回失败，请检查出库单状态！";
								break;
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "出库单 " + outbound.BillNo + " 返回失败，出库单状态已经不是" + WarehouseOutboundStatus.待发货.ToString() + "！";
							break;
						}
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "待发货返回待打印", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 根据出库单状态获取申请退款出库单笔数

		/// <summary>
		/// 根据出库单状态获取申请退款出库单笔数
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="status">出库单状态 枚举</param>
		/// <param name="printBatchCode">打印批次 如不需要可传""</param>
		/// <param name="isWaitPurchase">是否待采出库单 0否 1是</param>
		/// <returns></returns>
		public static int GetApplyRefundCount(string userCode, string warehouseCode, int status, string printBatchCode, int isWaitPurchase) {
			int applyRefundOrderCount = 0;
			try {
				applyRefundOrderCount = WarehouseOutboundService.GetApplyRefundCount(warehouseCode, status, printBatchCode, isWaitPurchase);
			}
			catch (Exception ex) {
				Sys.SaveErrorLog(ex, "根据出库单状态获取申请退款出库单笔数", userCode);
			}
			return applyRefundOrderCount;
		}

		#endregion

		#region 根据打印模版类型获取打印批次的已打印数量

		/// <summary>
		/// 根据打印模版类型获取打印批次的已打印数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="status">出库单状态枚举值</param>
		/// <param name="printBatchCode">打印批次</param>
		/// <param name="printTemplateType">打印模版类型 0发货单 1拣货单 2快递单</param>
		/// <returns></returns>
		public static int GetPrintCount(string userCode, string warehouseCode, int status, string printBatchCode, int printTemplateType) {
			int printCount = 0;
			try {
				printCount = WarehouseOutboundService.GetPrintCount(warehouseCode, status, printBatchCode, printTemplateType);
			}
			catch (Exception ex) {
				Sys.SaveErrorLog(ex, "根据打印模版类型获取打印批次的已打印数量", userCode);
			}
			return printCount;
		}

		#endregion

		#region 根据待采出库单生成采购计划单

		/// <summary>
		/// 根据待采出库单生成采购计划单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="idList">出库单ID列表</param>
		/// <returns></returns>
		public static BaseResult Generation(string userCode, string userName, string warehouseCode, List<int> idList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					//获取出库单的预售拣货明细，排除申请退款的出库单
					List<WarehouseOutboundPickItem> pickItemList = WarehouseOutboundPickItemService.GetBookingPickItemList(warehouseCode, idList, context);
					if (pickItemList.Count > 0) {
						string billNo = Sys.GetBillNo(BillType.JH.ToString());
						WarehousePurchasePlan purchasePlan = new WarehousePurchasePlan();
						purchasePlan.WarehouseCode = pickItemList[0].WarehouseCode;
						purchasePlan.BillNo = billNo;
						purchasePlan.Name = DateTime.Now.ToString("yyyy-MM-dd") + "已售待采商品";
						purchasePlan.Status = (int)PurchasePlanStatus.未提交;
						purchasePlan.CreatePerson = userCode;
						purchasePlan.CreateDate = DateTime.Now;
						int purchasePlanID = WarehousePurchasePlanService.Add(purchasePlan, context);
						if (purchasePlanID > 0) {
							foreach (var item in pickItemList) {
								WarehousePurchasePlanItem purchasePlanItem = new WarehousePurchasePlanItem();
								purchasePlanItem.WarehouseCode = item.WarehouseCode;
								purchasePlanItem.BillNo = billNo;
								purchasePlanItem.Num = item.Num;
								purchasePlanItem.PlanID = purchasePlanID;
								purchasePlanItem.ProductsCode = item.ProductsCode;
								purchasePlanItem.ProductsID = item.ProductsID;
								purchasePlanItem.ProductsName = item.ProductsName;
								purchasePlanItem.ProductsNo = item.ProductsNo;
								purchasePlanItem.ProductsSkuCode = item.ProductsSkuCode;
								purchasePlanItem.ProductsSkuID = item.ProductsSkuID;
								purchasePlanItem.ProductsSkuSaleprop = item.ProductsSkuSaleprop;
								purchasePlanItem.SuppliersID = SuppliersItemService.GetDefaultSuppliersID(item.ProductsSkuID, context);
								purchasePlanItem.CreatePerson = userCode;
								purchasePlanItem.CreateDate = DateTime.Now;
								int purchasePlanItemID = WarehousePurchasePlanItemService.Add(purchasePlanItem, context);
								if (purchasePlanItemID == 0) {
									resultInfo.result = 0;
									resultInfo.message = "出库单 " + item.OutboundBillNo + " 商品SKU码 " + item.ProductsSkuCode + " 添加采购计划商品失败！";
									break;
								}
							}
							if (resultInfo.result == 1) {
								//更新采购计划单的计划数量
								int count = WarehousePurchasePlanService.UpdateNum(userCode, purchasePlanID, context);
								if (count > 0) {
									List<WarehouseOutbound> outboundList = WarehouseOutboundService.GetWarehouseOutboundList(warehouseCode, idList, context);
									foreach (var outbound in outboundList) {
										count = WarehouseOutboundService.UpdateIsPurchasePlan(userCode, outbound.ID);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "出库单[" + outbound.BillNo + "]更新为已生成采购计划单失败！";
											break;
										}
										#region 保存订单操作日志
										resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "出库单[" + outbound.BillNo + "]生成采购计划单[" + billNo + "]", context, warehouseCode, outbound.BillNo);
										if (resultInfo.result != 1) {
											break;
										}
										#endregion
									}
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "更新采购计划单的计划数量失败！";
								}
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "添加采购计划单失败！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "没有可以生成采购计划单的出库单！";
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "根据待采出库单生成采购计划单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 拆分出库单

		/// <summary>
		/// 拆分出库单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">操作用户名</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="id">出库单ID</param>
		/// <returns></returns>
		public static BaseResult SplitOutbound(string userCode, string userName, string warehouseCode, int id) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseOutbound outbound = WarehouseOutboundService.GetQuerySingleByID(id, context);
					if (outbound.Status == (int)WarehouseOutboundStatus.待拣货 && outbound.IsWaitPurchase == 1 && outbound.IsCod == 0) {
						List<WarehouseOutboundPickItem> pickItemList = WarehouseOutboundPickItemService.GetQueryManyByOutboundID(id, context);
						//过滤出有库位ID和批次ID的拣货明细
						pickItemList = pickItemList.Where(p => p.LocationID > 0 && p.ProductsBatchID > 0).ToList();
						if (pickItemList.Count > 0) {
							#region 添加新出库单

							string billNo = Sys.GetBillNo(BillType.XSC.ToString());
							WarehouseOutbound newOutbound = new WarehouseOutbound();
							newOutbound.WarehouseCode = outbound.WarehouseCode;
							newOutbound.BillNo = billNo;
							newOutbound.BillType = (int)BillType.XSC;
							newOutbound.BuyAddr = outbound.BuyAddr;
							newOutbound.Province = outbound.Province;
							newOutbound.ProvinceID = outbound.ProvinceID;
							newOutbound.City = outbound.City;
							newOutbound.CityID = outbound.CityID;
							newOutbound.District = outbound.District;
							newOutbound.DistrictID = outbound.DistrictID;
							newOutbound.BuyMessage = outbound.BuyMessage;
							newOutbound.BuyMtel = outbound.BuyMtel;
							newOutbound.BuyName = outbound.BuyName;
							newOutbound.BuyNickName = outbound.BuyNickName;
							newOutbound.BuyPostCode = outbound.BuyPostCode;
							newOutbound.BuyTel = outbound.BuyTel;
							newOutbound.ErpOrderCode = outbound.ErpOrderCode;
							newOutbound.ExpectedDeliDate = outbound.ExpectedDeliDate;
							newOutbound.ExpressID = outbound.ExpressID;
							newOutbound.IsApplyRefund = outbound.IsApplyRefund;
							newOutbound.IsNeedInvoice = outbound.IsNeedInvoice;
							newOutbound.IsWaitPurchase = 0;
							newOutbound.LogisticsID = outbound.LogisticsID;
							newOutbound.OrderSource = outbound.OrderSource;
							newOutbound.OrderType = 0;
							newOutbound.OutOrderCode = outbound.OutOrderCode;
							newOutbound.ParentBillNo = outbound.BillNo;
							newOutbound.PayDate = outbound.PayDate;
							newOutbound.PaymentAccount = outbound.PaymentAccount;
							newOutbound.PaymentMethod = outbound.PaymentMethod;
							newOutbound.SellerRemark = outbound.SellerRemark;
							newOutbound.ShopID = outbound.ShopID;
							newOutbound.Status = (int)WarehouseOutboundStatus.待拣货;
							newOutbound.TradingNumber = outbound.TradingNumber;
							newOutbound.CreatePerson = userCode;
							newOutbound.CreateDate = DateTime.Now;
							int newOutboundID = WarehouseOutboundService.Add(newOutbound, context);

							#endregion
							if (newOutboundID > 0) {
								var ordItemList = from p in pickItemList
												  group p by p.OrditemID into g
												  select new {
													  g.Key,
													  ProductsNum = g.Sum(p => p.Num)
												  };
								int count = 0;
								foreach (var item in ordItemList) {
									int oldOrdItemID = item.Key;
									Orditem orditem = OrditemService.GetQuerySingleByID(oldOrdItemID, context);
									if (orditem.ProductsNum == item.ProductsNum) {
										orditem.OutboundBillNo = billNo;
										orditem.OutboundID = newOutboundID;
										orditem.UpdatePerson = userCode;
										orditem.UpdateDate = DateTime.Now;
										count = OrditemService.Update(orditem, context);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "商品SKU码 " + orditem.ProductsSkuCode + " 更新出库单明细所属出库单失败！";
											break;
										}
									}
									else {
										count = OrditemService.UpdateProductsNum(userCode, oldOrdItemID, -item.ProductsNum, context);
										if (count > 0) {
											#region 添加新出库单商品
											Orditem newOrditem = new Orditem();
											newOrditem.ErpOrderCode = orditem.ErpOrderCode;
											newOrditem.OrdbaseID = orditem.OrdbaseID;
											newOrditem.OutboundBillNo = billNo;
											newOrditem.OutboundID = newOutboundID;
											newOrditem.WarehouseCode = orditem.WarehouseCode;
											newOrditem.ShopID = orditem.ShopID;
											newOrditem.OrdouterItemID = orditem.OrdouterItemID;
											newOrditem.OutOrderCode = orditem.OutOrderCode;
											newOrditem.ChildOrderCode = orditem.ChildOrderCode;
											newOrditem.BrandID = orditem.BrandID;
											newOrditem.BrandName = orditem.BrandName;
											newOrditem.CategoryID = orditem.CategoryID;
											newOrditem.CategoryName = orditem.CategoryName;
											newOrditem.ProductsCode = orditem.ProductsCode;
											newOrditem.ProductsID = orditem.ProductsID;
											newOrditem.ProductsName = orditem.ProductsName;
											newOrditem.ProductsNo = orditem.ProductsNo;
											newOrditem.ProductsNum = item.ProductsNum;
											newOrditem.ProductsSkuCode = orditem.ProductsSkuCode;
											newOrditem.ProductsSkuID = orditem.ProductsSkuID;
											newOrditem.ProductsSkuSaleprop = orditem.ProductsSkuSaleprop;
											newOrditem.ProductsWeight = orditem.ProductsWeight;
											newOrditem.TaxRate = orditem.TaxRate;
											newOrditem.SellingPrice = orditem.SellingPrice;
											newOrditem.CostPrice = orditem.CostPrice;
											newOrditem.DiscountAmount = orditem.DiscountAmount;
											newOrditem.AddType = orditem.AddType;
											newOrditem.CreatePerson = userCode;
											newOrditem.CreateDate = DateTime.Now;
											int newOrditemID = OrditemService.Add(newOrditem, context);
											if (newOrditemID > 0) {
												count = WarehouseOutboundPickItemService.UpdateOutboundInfo(userCode, oldOrdItemID, billNo, newOutboundID, newOrditemID, context);
												if (count == 0) {
													resultInfo.result = 0;
													resultInfo.message = "商品SKU码 " + orditem.ProductsSkuCode + "更新出库单拣货明细所属出库单失败！";
													break;
												}
											}
											else {
												resultInfo.result = 0;
												resultInfo.message = "商品SKU码 " + orditem.ProductsSkuCode + " 添加新出库单明细失败！";
												break;
											}
											#endregion
										}
										else {
											resultInfo.result = 0;
											resultInfo.message = "商品SKU码 " + orditem.ProductsSkuCode + " 更新出库单明细数量失败！";
											break;
										}
									}
								}
								if (resultInfo.result == 1) {
									//更新新旧两笔出库单的数量、重量、金额数据
									WarehouseOutboundService.UpdateProductsNumWeightAndAmount(userCode, id, context);
									WarehouseOutboundService.UpdateProductsNumWeightAndAmount(userCode, newOutboundID, context);
									#region 保存订单操作日志
									resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "出库单[" + outbound.BillNo + "]拆分出新出库单[" + billNo + "]", context, warehouseCode, outbound.BillNo);
									#endregion
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "创建新出库单失败！";
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "没有可以拆分的拣货明细！";
						}

					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "非货到付款的待采出库单才可以拆分！";
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "拆分出库单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 根据打印模版类型设置打印时间
		/// <summary>
		/// 根据打印模版类型设置打印时间
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="idList">出库单ID列表</param>
		/// <param name="printTemplateType">打印模版类型 0发货单 1拣货单 2快递单</param>
		/// <returns></returns>
		public static BaseResult SetPrintDate(string userCode, string userName, string warehouseCode, List<int> idList, int printTemplateType) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					List<WarehouseOutbound> outboundList = WarehouseOutboundService.GetWarehouseOutboundList(warehouseCode, idList, context);
					if (outboundList != null) {
						foreach (var outbound in outboundList) {
							int count = 0;
							string templateName = string.Empty;
							switch (printTemplateType) {
								case (int)PrintTemplateType.发货单:
									templateName = PrintTemplateType.发货单.ToString();
									count = WarehouseOutboundService.SetDeliveryPrintDate(userCode, warehouseCode, outbound.ID, context);
									break;
								case (int)PrintTemplateType.拣货单:
									templateName = PrintTemplateType.拣货单.ToString();
									count = WarehouseOutboundService.SetPickPrintDate(userCode, warehouseCode, outbound.ID, context);
									break;
								case (int)PrintTemplateType.快递单:
									templateName = PrintTemplateType.快递单.ToString();
									count = WarehouseOutboundService.SetExpressPrintDate(userCode, warehouseCode, outbound.ID, context);
									break;
							}
							if (count > 0) {
								#region 保存订单操作日志
								resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "出库单[" + outbound.BillNo + "]打印" + templateName, context, warehouseCode, outbound.BillNo);
								if (resultInfo.result != 1) {
									break;
								}
								#endregion
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "出库单 " + outbound.BillNo + " 设置" + templateName + "打印时间失败！";
								break;
							}
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "出库单ID " + string.Join(",", idList.ToArray()) + " 不存在！";
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "根据打印模版类型设置打印时间", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 打印完毕 单笔出库单使用ID，批量使用批次

		/// <summary>
		/// 打印完毕 单笔出库单使用ID，批量使用批次
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="printBatchCode">打印批次</param>
		/// <returns></returns>
		public static BaseResult PrintFinish(string userCode, string userName, string warehouseCode, int outboundID, string printBatchCode) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					List<WarehouseOutbound> outboundList = new List<WarehouseOutbound>();
					if (outboundID == 0) {
						if (printBatchCode != "" && printBatchCode != "0") {
							outboundList = WarehouseOutboundService.GetWarehouseOutboundList(warehouseCode, printBatchCode, (int)WarehouseOutboundStatus.待打印, context);

						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "请选择一个批次！";
						}
					}
					else {
						WarehouseOutbound outbound = WarehouseOutboundService.GetQuerySingleByID(outboundID, context);
						outboundList.Add(outbound);
					}
					if (outboundList.Count > 0) {
						foreach (var outbound in outboundList) {
							int count = WarehouseOutboundService.UpdateStatus(userCode, warehouseCode, outbound.ID, (int)WarehouseOutboundStatus.待打印, (int)WarehouseOutboundStatus.待发货, null, context);
							if (count > 0) {
								#region 保存订单操作日志
								resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "出库单[" + outbound.BillNo + "]打印完毕", context, warehouseCode, outbound.BillNo);
								if (resultInfo.result != 1) {
									break;
								}
								#endregion
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "申请退款的出库单不能进行此操作！";
								break;
							}
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = outboundID > 0 ? "出库单不存在！" : "打印批次 " + printBatchCode + " 没有待打印出库单！";
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "打印完毕", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 更换发货快递

		/// <summary>
		/// 更换发货快递
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="printBatchCode">打印批次</param>
		/// <param name="deliveryExpressID">发货快递ID</param>
		/// <returns></returns>
		public static BaseResult ChangeDeliveryExpress(string userCode, string userName, string warehouseCode, string printBatchCode, int deliveryExpressID) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					List<WarehouseOutbound> outboundList = new List<WarehouseOutbound>();
					if (printBatchCode != "" && printBatchCode != "0") {
						outboundList = WarehouseOutboundService.GetWarehouseOutboundList(warehouseCode, printBatchCode, (int)WarehouseOutboundStatus.待打印, context);
						if (outboundList.Count > 0) {
							foreach (var outbound in outboundList) {
								int count = WarehouseOutboundService.UpdateDeliveryExpressID(userCode, warehouseCode, outbound.ID, deliveryExpressID, context);
								if (count > 0) {
									#region 保存订单操作日志
									string expressName = string.Empty;
									WarehouseExpress warehouseExpress = WarehouseExpressService.GetQuerySingleByID(deliveryExpressID, context);
									if (warehouseExpress != null) expressName = warehouseExpress.Name;
									resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "出库单[" + outbound.BillNo + "]更换发货快递为[" + expressName + "]", context, warehouseCode, outbound.BillNo);
									if (resultInfo.result != 1) {
										break;
									}
									#endregion
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "出库单[" + outbound.BillNo + "]更换发货快递失败！";
									break;
								}
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "打印批次 " + printBatchCode + " 没有待打印出库单！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "请选择一个批次！";
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "更换发货快递", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存运单号

		/// <summary>
		/// 保存运单号
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="idList">出库单ID列表</param>
		/// <param name="waybillNoList">运单号列表</param>
		/// <param name="deliveryExpressID">发货快递ID</param>
		/// <returns></returns>
		public static BaseResult SaveWaybillNo(string userCode, string userName, string warehouseCode, List<int> idList, List<string> waybillNoList, int deliveryExpressID) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					for (int i = 0; i < idList.Count(); i++) {
						int id = idList[i];
						string waybillNo = waybillNoList[i];
						WarehouseOutbound outbound = WarehouseOutboundService.GetQuerySingleByID(id, context);
						if (outbound != null) {
							if (outbound.DeliveryExpressID == deliveryExpressID) {
								int count = WarehouseOutboundService.UpdateWaybillNo(userCode, warehouseCode, id, waybillNo, context);
								if (count > 0) {
									resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "出库单[" + outbound.BillNo + "] 保存运单号为[" + waybillNo + "]", context, warehouseCode, outbound.BillNo);
									if (resultInfo.result != 1) {
										break;
									}
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "出库单[" + outbound.BillNo + "]保存运单号失败！";
									break;
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "出库单[" + outbound.BillNo + "]发货快递已改变，不能保存该运单号！";
								break;
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "出库单ID[" + id + "]不存在！";
							break;
						}
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存运单号", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 获取打印快递单商品明细

		/// <summary>
		/// 获取打印快递单商品明细
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="fieldList">字段列表</param>
		/// <param name="id">出库单ID</param>
		/// <returns></returns>
		public static string GetPrintExpressPro(string warehouseCode, List<string> fieldList, int id) {
			StringBuilder proInfo = new StringBuilder();
			if (fieldList.Count > 0) {
				StringBuilder sb = new StringBuilder();
				List<Orditem> ordItemList = OrditemService.GetQueryManyByOutboundID(id);
				foreach (var ordItem in ordItemList) {
					if (fieldList.Contains("ProductsCode")) {
						sb.Append(ordItem.ProductsCode + "，");
					}
					if (fieldList.Contains("ProductsName")) {
						sb.Append(ordItem.ProductsName + "，");
					}
					if (fieldList.Contains("ProductsSkuSaleprop")) {
						sb.Append(ordItem.ProductsSkuSaleprop + "，");
					}
					if (fieldList.Contains("ProductsSkuCode")) {
						sb.Append(ordItem.ProductsSkuCode + "，");
					}
					if (fieldList.Contains("SellingPrice")) {
						sb.Append(ordItem.SellingPrice.ToString() + "，");
					}
					if (fieldList.Contains("ProductsNum")) {
						sb.Append("*" + ordItem.ProductsNum + "，");
					}
					if (fieldList.Contains("LocationCode")) {
						DataTable locationDt = WarehouseOutboundPickItemService.GetLocationInfoByOrdItemID(warehouseCode, ordItem.ID);
						foreach (DataRow dr in locationDt.Rows) {
							string currentLocationCode = ZConvert.ToString(dr["LocationCode"]);
							if (currentLocationCode == "") currentLocationCode = "YS";
							sb.Append(currentLocationCode + "*" + ZConvert.StrToInt(dr["Num"]) + "，");
						}
					}
					proInfo.AppendLine(sb.ToString().TrimEnd('，'));
				}
				proInfo.Append("[全]");
			}
			return proInfo.ToString();
		}

		#endregion

		#region 获取打印商品明细字段宽度百分比

		/// <summary>
		/// 获取打印商品明细字段宽度百分比
		/// </summary>
		/// <param name="fieldList">字段列表</param>
		/// <param name="fieldWidthList">字段宽度百分比列表</param>
		/// <param name="field">字段名称</param>
		/// <returns></returns>
		private static string getPrintFieldWidth(List<string> fieldList, List<string> fieldWidthList, string field) {
			string width = "20%";
			int index = fieldList.IndexOf(field);
			if (index < fieldWidthList.Count) {
				width = fieldWidthList[index];
			}
			return width;
		}

		#endregion

		#region 获取打印发货单商品明细

		/// <summary>
		/// 获取打印发货单商品明细
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="fieldList">字段列表</param>
		/// <param name="fieldWidthList">字段宽度百分比列表</param>
		/// <param name="id">出库单ID</param>
		/// <returns></returns>
		public static string GetPrintDeliveryPro(string warehouseCode, List<string> fieldList, List<string> fieldWidthList, int id) {
			StringBuilder proInfo = new StringBuilder();
			if (fieldList.Count > 0) {
				proInfo.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"printTable\" >");
				#region 标题行
				proInfo.Append("<thead><tr>");
				if (fieldList.Contains("ProductsCode")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "ProductsCode") + "\"><b>商品编码</b></td>");
				}
				if (fieldList.Contains("ProductsName")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "ProductsName") + "\"><b>商品名称</b></td>");
				}
				if (fieldList.Contains("ProductsSkuSaleprop")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "ProductsSkuSaleprop") + "\"><b>商品属性</b></td>");
				}
				if (fieldList.Contains("ProductsSkuCode")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "ProductsSkuCode") + "\"><b>商品SKU码</b></td>");
				}
				if (fieldList.Contains("SellingPrice")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "SellingPrice") + "\"><b>单价</b></td>");
				}
				if (fieldList.Contains("ProductsNum")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "ProductsNum") + "\"><b>数量</b></td>");
				}
				if (fieldList.Contains("LocationCode")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "LocationCode") + "\"><b>库位编码</b></td>");
				}
				proInfo.Append("</tr></thead>");
				#endregion
				#region 数据行
				List<Orditem> ordItemList = OrditemService.GetQueryManyByOutboundID(id);
				foreach (var ordItem in ordItemList) {
					proInfo.Append("<tr>");
					if (fieldList.Contains("ProductsCode")) {
						proInfo.Append("<td>" + ordItem.ProductsCode + "</td>");
					}
					if (fieldList.Contains("ProductsName")) {
						proInfo.Append("<td>" + ordItem.ProductsName + "</td>");
					}
					if (fieldList.Contains("ProductsSkuSaleprop")) {
						proInfo.Append("<td>" + ordItem.ProductsSkuSaleprop + "</td>");
					}
					if (fieldList.Contains("ProductsSkuCode")) {
						proInfo.Append("<td>" + ordItem.ProductsSkuCode + "</td>");
					}
					if (fieldList.Contains("SellingPrice")) {
						proInfo.Append("<td>" + ordItem.SellingPrice + "</td>");
					}
					if (fieldList.Contains("ProductsNum")) {
						proInfo.Append("<td>" + ordItem.ProductsNum + "</td>");
					}
					if (fieldList.Contains("LocationCode")) {
						StringBuilder locationInfo = new StringBuilder();
						DataTable locationDt = WarehouseOutboundPickItemService.GetLocationInfoByOrdItemID(warehouseCode, ordItem.ID);
						foreach (DataRow dr in locationDt.Rows) {
							string currentLocationCode = ZConvert.ToString(dr["LocationCode"]);
							if (currentLocationCode == "") currentLocationCode = "YS";
							locationInfo.AppendLine(currentLocationCode + "*" + ZConvert.StrToInt(dr["Num"]));
						}
						proInfo.Append("<td>" + locationInfo.ToString() + "</td>");
					}
					proInfo.Append("</tr>");
				}
				#endregion
				proInfo.Append("</table>");
			}
			return proInfo.ToString();
		}

		#endregion

		#region 获取打印拣货单商品明细

		/// <summary>
		/// 获取打印拣货单商品明细
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="fieldList">字段列表</param>
		/// <param name="fieldWidthList">字段宽度百分比列表</param>
		/// <param name="id">出库单ID列表</param>
		/// <returns></returns>
		public static string GetPrintPickPro(string warehouseCode, List<string> fieldList, List<string> fieldWidthList, List<int> idList) {
			StringBuilder proInfo = new StringBuilder();
			if (fieldList.Count > 0) {
				proInfo.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"printTable\" >");
				#region 标题行
				proInfo.Append("<thead><tr>");
				if (fieldList.Contains("ProductsCode")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "ProductsCode") + "\"><b>商品编码</b></td>");
				}
				if (fieldList.Contains("ProductsName")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "ProductsName") + "\"><b>商品名称</b></td>");
				}
				if (fieldList.Contains("ProductsSkuSaleprop")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "ProductsSkuSaleprop") + "\"><b>商品属性</b></td>");
				}
				if (fieldList.Contains("ProductsSkuCode")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "ProductsSkuCode") + "\"><b>商品SKU码</b></td>");
				}
				if (fieldList.Contains("ProductsNum")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "ProductsNum") + "\"><b>数量</b></td>");
				}
				if (fieldList.Contains("LocationCode")) {
					proInfo.Append("<td width=\"" + getPrintFieldWidth(fieldList, fieldWidthList, "LocationCode") + "\"><b>库位编码</b></td>");
				}
				proInfo.Append("</tr></thead>");
				#endregion
				#region 数据行
				DataTable pickItemDt = WarehouseOutboundPickItemService.GetLocationInfoByOutboundIDList(warehouseCode, idList);
				foreach (DataRow dr in pickItemDt.Rows) {
					proInfo.Append("<tr>");
					if (fieldList.Contains("ProductsCode")) {
						proInfo.Append("<td>" + ZConvert.ToString(dr["ProductsCode"]) + "</td>");
					}
					if (fieldList.Contains("ProductsName")) {
						proInfo.Append("<td>" + ZConvert.ToString(dr["ProductsName"]) + "</td>");
					}
					if (fieldList.Contains("ProductsSkuSaleprop")) {
						proInfo.Append("<td>" + ZConvert.ToString(dr["ProductsSkuSaleprop"]) + "</td>");
					}
					if (fieldList.Contains("ProductsSkuCode")) {
						proInfo.Append("<td>" + ZConvert.ToString(dr["ProductsSkuCode"]) + "</td>");
					}
					if (fieldList.Contains("ProductsNum")) {
						proInfo.Append("<td>" + ZConvert.ToString(dr["ProductsNum"]) + "</td>");
					}
					if (fieldList.Contains("LocationCode")) {
						proInfo.Append("<td>" + ZConvert.ToString(dr["LocationCode"]) + "</td>");
					}
					proInfo.Append("</tr>");
				}
				#endregion
				proInfo.Append("</table>");
			}
			return proInfo.ToString();
		}

		#endregion

		#region 更新为已校验

		/// <summary>
		/// 更新为已校验
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="billNo">出库单号</param>
		/// <returns></returns>
		public static BaseResult SetScanCheck(string userCode, string userName, string warehouseCode, string billNo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseOutbound outbound = WarehouseOutboundService.GetQuerySingleByBillNo(warehouseCode, billNo, context);
					if (outbound != null) {
						int count = WarehouseOutboundService.UpdateScanCheck(userCode, warehouseCode, billNo, context);
						if (count > 0) {
							#region 保存订单操作日志
							resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "出库单[" + outbound.BillNo + "]更新为已校验", context, warehouseCode, outbound.BillNo);
							#endregion
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "更新为已校验失败 ，可能已经校验过！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "出库单[" + billNo + "]不存在！";
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "更新为已校验", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 更新实际包裹重量 如果是发货之后再称重，会更新参考运费

		/// <summary>
		/// 更新实际包裹重量 如果是发货之后再称重，会更新参考运费
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="billNo">出库单号</param>
		/// <param name="totalWeight">实际包裹重量</param>
		/// <returns></returns>
		public static BaseResult SetTotalWeight(string userCode, string userName, string warehouseCode, string billNo, decimal totalWeight) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseOutbound outbound = WarehouseOutboundService.GetQuerySingleByBillNo(warehouseCode, billNo, context);
					if (outbound != null) {
						//实际包裹重量不等于商品重量才校验误差
						if (totalWeight != outbound.ProductsWeight) {
							#region 判断是否要校验误差
							WarehouseConfig warehouseConfig = WarehouseConfigService.GetQuerySingleByWarehouseCode(warehouseCode, context);
							if (warehouseConfig != null && warehouseConfig.IsOpenWeightWarn == 1) {
								if (totalWeight - outbound.ProductsWeight > warehouseConfig.DeviationWeight || outbound.ProductsWeight - totalWeight > warehouseConfig.DeviationWeight) {
									resultInfo.result = 0;
									resultInfo.message = "称重误差超过系统设置的 " + warehouseConfig.DeviationWeight + " G，请检查！";
								}
							}
							#endregion
						}
						if (resultInfo.result == 1) {
							int count = WarehouseOutboundService.UpdateTotalWeight(userCode, warehouseCode, billNo, totalWeight, context);
							if (count > 0) {
								string message = "出库单[" + outbound.BillNo + "]更新实际包裹重量为：" + totalWeight + " G";
								//发货之后再称重，需要更新参考运费(仓库付给快递公司)
								if (outbound.Status == (int)WarehouseOutboundStatus.已发货) {
									decimal expressPrice = ExpressManager.GetExpressPrice(outbound.DeliveryExpressID, totalWeight, outbound.ProvinceID, outbound.CityID, context);
									count = WarehouseOutboundService.UpdateExpressFreight(userCode, warehouseCode, outbound.ID, expressPrice, context);
									if (count > 0) {
										message += "，参考运费为：" + expressPrice + " 元";
									}
									else {
										resultInfo.result = 0;
										resultInfo.message = "更新参考运费失败！";
									}
								}
								if (resultInfo.result == 1) {
									#region 保存订单操作日志
									resultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, message, context, warehouseCode, outbound.BillNo);
									#endregion
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "更新实际包裹重量失败！";
							}
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "出库单[" + billNo + "]不存在！";
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "更新实际包裹重量", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 发货

		/// <summary>
		/// 发货
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="idList">出库单ID列表</param>
		/// <param name="successCount">发货成功单数</param>
		/// <param name="errorCount">发货失败单数</param>
		/// <param name="deliveryMode">发货方式 默认0：确认发货 1：扫描发货</param>
		/// <returns></returns>
		public static BaseResult Delivery(string userCode, string userName, string warehouseCode, List<int> idList, out int successCount, out int errorCount, int deliveryMode = 0) {
			BaseResult resultInfo = new BaseResult();
			successCount = 0;
			errorCount = 0;
			string deliveryText = deliveryMode == 0 ? "确认发货" : "扫描发货";
			StringBuilder sb = new StringBuilder();
			foreach (var outboundID in idList) {
				int count = 0;
				BaseResult currResultInfo = new BaseResult();
				try {
					using (IDbContext context = Db.GetInstance().Context()) {
						context.UseTransaction(true);
						WarehouseOutbound outbound = WarehouseOutboundService.GetQuerySingleByID(outboundID, context);
						if (outbound != null) {
							if (outbound.Status == (int)WarehouseOutboundStatus.待发货) {
								DateTime deliveryDate = DateTime.Now;
								#region 检查是否可发货

								if (currResultInfo.result == 1) {
									#region 检查是否挂起
									if (outbound.IsHang == 1) {
										currResultInfo.result = 0;
										currResultInfo.message = "出库单 " + outbound.BillNo + " 已挂起！";
									}
									#endregion
								}
								if (currResultInfo.result == 1) {
									#region 检查是否待采出库单
									if (outbound.IsWaitPurchase == 1) {
										currResultInfo.result = 0;
										currResultInfo.message = "出库单 " + outbound.BillNo + " 是待采出库单！";
									}
									#endregion
								}
								if (currResultInfo.result == 1) {
									#region 检查是否申请退款
									if (outbound.IsApplyRefund == 1) {
										currResultInfo.result = 0;
										currResultInfo.message = "出库单 " + outbound.BillNo + " 已申请退款！";
									}
									#endregion
								}
								//获取仓库配置信息
								WarehouseConfig warehouseConfig = WarehouseConfigService.GetQuerySingleByWarehouseCode(warehouseCode, context);
								if (warehouseConfig == null) warehouseConfig = new WarehouseConfig();
								if (currResultInfo.result == 1) {
									#region 根据配置检查是否校验
									if (warehouseConfig.IsScanDelivery == 1 && outbound.IsScanCheck == 0) {
										currResultInfo.result = 0;
										currResultInfo.message = "出库单 " + outbound.BillNo + " 未校验！";
									}
									#endregion
								}
								if (currResultInfo.result == 1) {
									#region 根据配置检查是否称重
									if (warehouseConfig.IsWeightDelivery == 1 && outbound.TotalWeight == 0) {
										currResultInfo.result = 0;
										currResultInfo.message = "出库单 " + outbound.BillNo + " 未称重！";
									}
									#endregion
								}

								#endregion

								if (currResultInfo.result == 1) {
									#region 遍历出库单拣货明细更新库存，并检查是否有商品正在盘点
									List<WarehouseOutboundPickItem> outboundPickItemList = WarehouseOutboundPickItemService.GetQueryManyByOutboundID(outbound.ID, context);
									foreach (var outboundPickItem in outboundPickItemList) {
										//检查是否正在盘点
										bool isExists = WarehouseStocktakingItemService.IsExists(outboundPickItem.ProductsSkuID, outboundPickItem.ProductsBatchID, outboundPickItem.LocationID, context);
										if (!isExists) {
											//更新批次表库存
											count = WarehouseProductsBatchService.UpdateZyNumAndKyNumAndZkNum(userCode, outboundPickItem.ProductsBatchID, outboundPickItem.Num, context);
											if (count > 0) {
												//更新库位商品库存
												count = WarehouseLocationProductsService.UpdateZyNumAndKyNumAndZkNum(userCode, outboundPickItem.ProductsSkuID, outboundPickItem.LocationID, outboundPickItem.ProductsBatchID, outboundPickItem.Num, context);
												if (count > 0) {
													#region 增加发货日志

													WarehouseOutInStockLog outStockLog = new WarehouseOutInStockLog();
													outStockLog.BillType = (int)BillType.XSC;
													outStockLog.StockWay = (int)StockWay.出库;
													outStockLog.SourceID = outboundPickItem.OutboundID;
													outStockLog.SourceNo = outboundPickItem.OutboundBillNo;
													outStockLog.SourceItemID = outboundPickItem.ID;
													outStockLog.WarehouseCode = outboundPickItem.WarehouseCode;
													outStockLog.ProductsCode = outboundPickItem.ProductsCode;
													outStockLog.ProductsID = outboundPickItem.ProductsID;
													outStockLog.ProductsName = outboundPickItem.ProductsName;
													outStockLog.ProductsNo = outboundPickItem.ProductsNo;
													outStockLog.ProductsSkuCode = outboundPickItem.ProductsSkuCode;
													outStockLog.ProductsSkuID = outboundPickItem.ProductsSkuID;
													outStockLog.ProductsSkuSaleprop = outboundPickItem.ProductsSkuSaleprop;
													outStockLog.Num = outboundPickItem.Num;
													outStockLog.LocationID = outboundPickItem.LocationID;
													outStockLog.ProductsBatchID = outboundPickItem.ProductsBatchID;
													outStockLog.ProductsBatchCode = outboundPickItem.ProductsBatchCode;
													outStockLog.Remark = BillTypeConvert.GetBillTypeName(outStockLog.BillType);
													outStockLog.CreatePerson = userCode;
													outStockLog.CreateDate = DateTime.Now;
													int outStockLogID = WarehouseOutInStockLogService.Add(outStockLog, context);
													if (outStockLogID == 0) {
														currResultInfo.result = 0;
														currResultInfo.message = "商品SKU码 " + outboundPickItem.ProductsSkuCode + " 商品批次 " + outboundPickItem.ProductsBatchCode + " 库位编码 " + outboundPickItem.LocationCode + " 增加发货日志失败！";
														break;
													}

													#endregion
												}
												else {
													currResultInfo.result = 0;
													currResultInfo.message = "商品SKU码 " + outboundPickItem.ProductsSkuCode + " 商品批次 " + outboundPickItem.ProductsBatchCode + " 库位编码 " + outboundPickItem.LocationCode + " 更新库位商品表库存失败！";
													break;
												}
											}
											else {
												currResultInfo.result = 0;
												currResultInfo.message = "商品SKU码 " + outboundPickItem.ProductsSkuCode + " 商品批次 " + outboundPickItem.ProductsBatchCode + " 库位编码 " + outboundPickItem.LocationCode + " 更新批次表库存失败！";
												break;
											}
										}
										else {
											currResultInfo.result = 0;
											currResultInfo.message = "商品SKU码 " + outboundPickItem.ProductsSkuCode + " 商品批次 " + outboundPickItem.ProductsBatchCode + " 库位编码 " + outboundPickItem.LocationCode + " 正在盘点！";
											break;
										}
									}
									#endregion
								}
								if (currResultInfo.result == 1) {
									#region 出库单状态由待发货改为已发货，并更新发货时间
									count = WarehouseOutboundService.UpdateStatus(userCode, warehouseCode, outboundID, (int)WarehouseOutboundStatus.待发货, (int)WarehouseOutboundStatus.已发货, deliveryDate, context);
									if (count > 0) {
										count = OrditemService.UpdateDeliveryDate(userCode, warehouseCode, outbound.ID, outbound.DeliveryDate, context);
										if (count == 0) {
											currResultInfo.result = 0;
											currResultInfo.message = "出库单 " + outbound.BillNo + " 更新商品发货时间失败！";
										}
									}
									else {
										currResultInfo.result = 0;
										currResultInfo.message = "出库单 " + outbound.BillNo + " 更新状态为已发货失败，请检查是否已申请退款！";
									}
									#endregion
								}
								if (currResultInfo.result == 1) {
									//代码顺序不能改变，必须先更新出库单状态为已发货，再来更新订单状态
									#region 更新订单状态、发货时间、物流信息
									Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(outbound.ErpOrderCode, context);
									bool IsExistsNoDelivery = WarehouseOutboundService.IsExistsNoDelivery(outbound.ErpOrderCode, context);
									int orderStatus = (int)OrdBaseStatus.已发货;
									if (IsExistsNoDelivery) {
										orderStatus = (int)OrdBaseStatus.部分发货;
									}
									if (ordbase.OrderStatus == (int)OrdBaseStatus.发货中) {
										//第一次发货，更新订单状态 、发货时间、物流信息
										count = OrdbaseService.UpdateOrderStatus(userCode, outbound.ErpOrderCode, (int)OrdBaseStatus.发货中, orderStatus, context, deliveryDate, outbound.DeliveryExpressID, outbound.WaybillNo);
										if (count == 0) {
											currResultInfo.result = 0;
											currResultInfo.message = "出库单 " + outbound.BillNo + " 更新订单状态和物流信息失败！";
										}
									}
									else {
										//如果订单状态为部分发货，且已经没有未发货的出库单，则把订单状态更新为已发货
										if (ordbase.OrderStatus == (int)OrdBaseStatus.部分发货 && !IsExistsNoDelivery) {
											count = OrdbaseService.UpdateOrderStatus(userCode, outbound.ErpOrderCode, (int)OrdBaseStatus.部分发货, (int)OrdBaseStatus.已发货, context);
											if (count == 0) {
												currResultInfo.result = 0;
												currResultInfo.message = "出库单 " + outbound.BillNo + " 更新订单状态失败！";
											}
										}
									}
									#endregion
								}
								if (currResultInfo.result == 1) {
									#region 更新参考运费(仓库付给快递公司)
									decimal totalWeight = outbound.TotalWeight > 0 ? outbound.TotalWeight : outbound.ProductsWeight;
									decimal expressPrice = ExpressManager.GetExpressPrice(outbound.DeliveryExpressID, totalWeight, outbound.ProvinceID, outbound.CityID, context);
									count = WarehouseOutboundService.UpdateExpressFreight(userCode, warehouseCode, outboundID, expressPrice, context);
									if (count > 0) {
										deliveryText += "，参考运费为：" + expressPrice;
									}
									else {
										currResultInfo.result = 0;
										currResultInfo.message = "更新参考运费失败！";
									}
									#endregion
								}
								if (currResultInfo.result == 1) {
									#region 增加到发货信息表
									currResultInfo = SendShopManager.Add(userCode, outbound.ErpOrderCode, context);
									#endregion
								}
								if (currResultInfo.result == 1) {
									#region 保存订单操作日志
									currResultInfo = OrdlogManager.Save(userCode, userName, outbound.ErpOrderCode, outbound.OutOrderCode, "出库单[" + outbound.BillNo + "]" + deliveryText, context, warehouseCode, outbound.BillNo);
									#endregion
								}
							}
							else {
								currResultInfo.result = 0;
								currResultInfo.message = "出库单 " + outbound.BillNo + " 状态为" + ((WarehouseOutboundStatus)outbound.Status).ToString() + "！";
							}
						}
						else {
							currResultInfo.result = 0;
							currResultInfo.message = "出库单 " + outbound.BillNo + " 不存在！";
						}
						if (currResultInfo.result == 1) {
							context.Commit();
							successCount++;
						}
						else {
							context.Rollback();
							errorCount++;
							sb.Append(currResultInfo.message);
						}
					}
				}
				catch (Exception ex) {
					currResultInfo.result = 0;
					currResultInfo.message = "程序出现异常！";
					errorCount++;
					sb.Append(currResultInfo.message);
					Sys.SaveErrorLog(ex, deliveryText, userCode);
				}
			}
			if (errorCount > 0) {
				resultInfo.result = 0;
				resultInfo.message = sb.ToString();
			}
			return resultInfo;
		}

		#endregion

		#region 预售出库单查询库存

		/// <summary>
		/// 预售出库单查询库存
		/// </summary>
		/// <param name="userCode">操作帐号</param>
		/// <param name="id">出库单ID</param>
		/// <returns></returns>
		public static BaseResult SearchStock(string userCode, int id) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					List<Orditem> ordItemList = OrditemService.GetQueryManyByOutboundID(id, context);
					if (ordItemList.Count() > 0) {
						string warehouseCode = ordItemList[0].WarehouseCode;
						foreach (var ordItem in ordItemList) {
							resultInfo = WarehouseProductsManager.BookingZyToLocationZy(userCode, warehouseCode, ordItem.ProductsSkuID, context, ordItem.ID);
							if (resultInfo.result != 1) {
								break;
							}
						}
						if (resultInfo.result == 1) {
							#region 检查出库单是否还有预售拣货明细，如果没有则将出库单标记为普通出库单
							int count = WarehouseOutboundPickItemService.GetBookingPickItemCount(id, context);
							if (count == 0) {
								WarehouseOutboundService.UpdateIsWaitPurchase(userCode, id, (int)IsEnable.否, context);
								resultInfo.message = "查询成功！";
							}
							else {
								resultInfo.message = "查询成功，还有部分商品库位库存不足！";
							}
							#endregion
							context.Commit();
						}
						else {
							context.Rollback();
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "出库单没有商品！";
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "预售出库单查询库存", userCode);
			}
			return resultInfo;
		}


		#endregion
	}
}
