using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Api.Bll {

	/// <summary>
	/// 售后管理类
	/// </summary>
	public class OrdrefundManager {

		#region 获取售后单状态名称

		/// <summary>
		/// 获取售后单状态名称
		/// </summary>
		/// <param name="status">售后单状态枚举值</param>
		/// <returns></returns>
		public static string GetStatusName(int status) {
			string statusName = string.Empty;
			switch (status) {
				case (int)OrdRefundStatus.等待买家退货:
					statusName = OrdRefundStatus.等待买家退货.ToString();
					break;
				case (int)OrdRefundStatus.等待卖家收货:
					statusName = OrdRefundStatus.等待卖家收货.ToString();
					break;
				case (int)OrdRefundStatus.收货异常:
					statusName = OrdRefundStatus.收货异常.ToString();
					break;
				case (int)OrdRefundStatus.已完成:
					statusName = OrdRefundStatus.已完成.ToString();
					break;
				case (int)OrdRefundStatus.已取消:
					statusName = OrdRefundStatus.已取消.ToString();
					break;
			}
			return statusName;
		}

		#endregion

		#region 获取售后单责任方名称

		/// <summary>
		/// 获取售后单责任方名称
		/// </summary>
		/// <param name="duty">售后单责任方枚举值</param>
		/// <param name="dutyOther">其他责任方</param>
		/// <returns></returns>
		public static string GetDutyName(int duty, string dutyOther) {
			string dutyName = string.Empty;
			switch (duty) {
				case (int)OrdRefundDuty.买家:
					dutyName = OrdRefundDuty.买家.ToString();
					break;
				case (int)OrdRefundDuty.商家:
					dutyName = OrdRefundDuty.商家.ToString();
					break;
				case (int)OrdRefundDuty.物流:
					dutyName = OrdRefundDuty.物流.ToString();
					break;
				case (int)OrdRefundDuty.其他:
					dutyName = dutyOther;
					break;
			}
			return dutyName;
		}

		#endregion

		#region 获取售后类型名称

		/// <summary>
		/// 获取售后类型名称
		/// </summary>
		/// <param name="refundType">售后类型枚举值</param>
		/// <returns></returns>
		public static string GetRefundTypeName(int refundType) {
			string refundTypeName = string.Empty;
			switch (refundType) {
				case (int)OrdRefundType.退货退款:
					refundTypeName = OrdRefundType.退货退款.ToString();
					break;
				case (int)OrdRefundType.仅退款:
					refundTypeName = OrdRefundType.仅退款.ToString();
					break;
			}
			return refundTypeName;
		}

		#endregion

		#region 添加售后

		/// <summary>
		/// 添加售后
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="userName">用户名称</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="objWebInfo">售后实体类 前端交互</param>
		/// <returns></returns>
		public static BaseResult Save(string userCode, string userName, string warehouseCode, OrdRefundWebInfo objWebInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					#region 添加售后记录

					string billNo = Sys.GetBillNo(BillType.SH.ToString());
					Ordrefund ordRefund = new Ordrefund();
					ordRefund.BillNo = billNo;
					ordRefund.WarehouseCode = warehouseCode;
					ordRefund.RefundType = objWebInfo.RefundType;
					ordRefund.ShopID = objWebInfo.ShopID;
					ordRefund.OrderSource = objWebInfo.OrderSource;
					ordRefund.ErpOrderCode = objWebInfo.ErpOrderCode;
					ordRefund.OutOrderCode = objWebInfo.OutOrderCode;
					ordRefund.OutboundBillNo = objWebInfo.OutboundBillNo;
					ordRefund.BuyAddr = objWebInfo.BuyAddr;
					ordRefund.BuyMtel = objWebInfo.BuyMtel;
					ordRefund.BuyName = objWebInfo.BuyName;
					ordRefund.BuyTel = objWebInfo.BuyTel;
					ordRefund.CreateDate = DateTime.Now;
					ordRefund.CreatePerson = userCode;
					ordRefund.Duty = objWebInfo.Duty;
					ordRefund.DutyOther = objWebInfo.DutyOther;
					ordRefund.RefundAmount = objWebInfo.RefundAmount;
					if (objWebInfo.RefundType != (int)OrdRefundType.仅退款) {
						ordRefund.ReceivePerson = objWebInfo.ReceivePerson;
						ordRefund.ReceiveTel = objWebInfo.ReceiveTel;
						ordRefund.ReceiveAddress = objWebInfo.ReceiveAddress;
						ordRefund.ReceivePostCode = objWebInfo.ReceivePostCode;
						ordRefund.ExpressCompany = objWebInfo.ExpressCompany;
						ordRefund.WaybillNo = objWebInfo.WaybillNo;
						ordRefund.RefundFreight = objWebInfo.RefundFreight;
						ordRefund.ReturnFreight = ZConvert.StrToDecimal(objWebInfo.ReturnFreight);
						ordRefund.Status = (ZConvert.ToString(objWebInfo.WaybillNo) == "" || objWebInfo.ExpressCompany == "" || objWebInfo.ReturnFreight == "") ? (int)OrdRefundStatus.等待买家退货 : (int)OrdRefundStatus.等待卖家收货;
						if (ordRefund.Status == (int)OrdRefundStatus.等待卖家收货) {
							ordRefund.SendBackDate = DateTime.Now;
						}
					}
					else {
						ordRefund.Status = (int)OrdRefundStatus.已完成;
					}
					ordRefund.Reason = objWebInfo.Reason;
					ordRefund.ReasonDetail = objWebInfo.ReasonDetail;
					int ordRefundID = OrdrefundService.Add(ordRefund, context);

					#endregion
					if (ordRefundID > 0) {
						if (objWebInfo.RefundType != (int)OrdRefundType.仅退款) {
							#region 遍历添加售后明细

							int[] arrOrdItemID = objWebInfo.OrdItemID;
							string[] arrProductsCode = objWebInfo.ProductsCode;
							int[] arrProductsID = objWebInfo.ProductsID;
							string[] arrProductsName = objWebInfo.ProductsName;
							string[] arrProductsNo = objWebInfo.ProductsNo;
							int[] arrProductsNum = objWebInfo.ProductsNum;
							string[] arrProductsSkuCode = objWebInfo.ProductsSkuCode;
							int[] arrProductsSkuID = objWebInfo.ProductsSkuID;
							string[] arrProductsSkuSaleprop = objWebInfo.ProductsSkuSaleprop;
							int[] arrProductsBatchID = objWebInfo.ProductsBatchID;
							string[] arrProductsBatchCode = objWebInfo.ProductsBatchCode;
							int[] arrRefundNum = objWebInfo.RefundNum;
							decimal[] arrActualSellingPrice = objWebInfo.ActualSellingPrice;
							int totalRefundNum = 0;
							for (int i = 0; i < arrRefundNum.Length; i++) {
								if (arrRefundNum[i] <= 0) continue;
								totalRefundNum += arrRefundNum[i];
								OrdrefundItem ordRefundItem = new OrdrefundItem();
								ordRefundItem.WarehouseCode = warehouseCode;
								ordRefundItem.OrdRefundID = ordRefundID;
								ordRefundItem.BillNo = billNo;
								ordRefundItem.ErpOrderCode = objWebInfo.ErpOrderCode;
								ordRefundItem.OutOrderCode = objWebInfo.OutOrderCode;
								ordRefundItem.OutboundBillNo = objWebInfo.OutboundBillNo;
								ordRefundItem.ShopID = objWebInfo.ShopID;
								ordRefundItem.OrdItemID = arrOrdItemID[i];
								ordRefundItem.ProductsCode = arrProductsCode[i];
								ordRefundItem.ProductsID = arrProductsID[i];
								ordRefundItem.ProductsName = arrProductsName[i];
								ordRefundItem.ProductsNo = arrProductsNo[i];
								ordRefundItem.ProductsNum = arrProductsNum[i];
								ordRefundItem.ProductsSkuCode = arrProductsSkuCode[i];
								ordRefundItem.ProductsSkuID = arrProductsSkuID[i];
								ordRefundItem.ProductsSkuSaleprop = arrProductsSkuSaleprop[i];
								ordRefundItem.ProductsBatchID = arrProductsBatchID[i];
								ordRefundItem.ProductsBatchCode = arrProductsBatchCode[i];
								ordRefundItem.RefundNum = arrRefundNum[i];
								ordRefundItem.ActualSellingPrice = arrActualSellingPrice[i];
								ordRefundItem.CreateDate = DateTime.Now;
								ordRefundItem.CreatePerson = userCode;
								int hasRefundNum = OrdrefundItemService.GetHasRefundNum(ordRefundItem.OrdItemID, context);
								if (ordRefundItem.RefundNum <= ordRefundItem.ProductsNum - hasRefundNum) {
									int ordRefundItemID = OrdrefundItemService.Add(ordRefundItem, context);
									if (ordRefundItemID == 0) {
										resultInfo.result = 0;
										resultInfo.message = "SKU码 " + ordRefundItem.ProductsSkuCode + " 添加售后明细失败！";
										break;
									}
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "SKU码 " + ordRefundItem.ProductsSkuCode + " 超过可售后数量！";
									break;
								}
							}
							if (totalRefundNum == 0) {
								resultInfo.result = 0;
								resultInfo.message = "请输入售后数量！";
							}

							#endregion
						}
						else {
							#region 生成退款单
							OrdaccountsBill ordaccountsBill = new OrdaccountsBill();
							ordaccountsBill.BillNo = Sys.GetBillNo(BillType.TK.ToString());
							ordaccountsBill.BillType = (int)BillType.TK;
							ordaccountsBill.BillWay = -1;
							ordaccountsBill.ErpOrderCode = objWebInfo.ErpOrderCode;
							ordaccountsBill.AssociatedCode = billNo;
							ordaccountsBill.Amount = objWebInfo.RefundAmount + objWebInfo.RefundFreight;
							ordaccountsBill.Remark = "添加仅退款售后时生成退款单";
							ordaccountsBill.CreatePerson = userCode;
							ordaccountsBill.CreateDate = DateTime.Now;
							int ordaccountsBillID = OrdaccountsBillService.Add(ordaccountsBill, context);
							if (ordaccountsBillID == 0) {
								resultInfo.result = 0;
								resultInfo.message = "生成退款单失败";
							}
							#endregion
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "添加售后失败！";
					}
					if (resultInfo.result == 1) {
						#region 保存订单操作日志
						resultInfo = OrdlogManager.Save(userCode, userName, ordRefund.ErpOrderCode, ordRefund.OutOrderCode, "出库单[" + ordRefund.OutboundBillNo + "]添加售后单[" + ordRefund.BillNo + "]", context, warehouseCode, ordRefund.OutboundBillNo);
						#endregion
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
				Sys.SaveErrorLog(ex, userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 设置售后物流信息

		/// <summary>
		/// 设置售后物流信息
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="ordRefundID">售后表主键ID</param>
		/// <param name="expressCompany">物流公司</param>
		/// <param name="waybillNo">运单号</param>
		/// <param name="returnFreight">寄回运费</param>
		/// <returns></returns>
		public static BaseResult Savelogistics(string userCode, int ordRefundID, string expressCompany, string waybillNo, decimal returnFreight) {
			BaseResult resultInfo = new BaseResult();
			try {
				int count = OrdrefundService.Updatelogistics(userCode, ordRefundID, expressCompany, waybillNo, returnFreight);
				if (count == 0) {
					resultInfo.result = 0;
					resultInfo.message = "设置售后物流信息失败，请检查状态是否为" + OrdRefundStatus.等待买家退货.ToString() + "！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "设置售后物流信息", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 确认收货

		/// <summary>
		/// 确认收货
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="objReceiveInfo">确认收货实体类</param>
		/// <returns></returns>
		public static BaseResult ConfirmReceive(string userCode, OrdRefundReceiveInfo objReceiveInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					Ordrefund ordRefund = OrdrefundService.GetQuerySingleByID(objReceiveInfo.OrdRefundID, context);
					if (ordRefund.Status == (int)OrdRefundStatus.等待卖家收货 || ordRefund.Status == (int)OrdRefundStatus.收货异常) {
						int count = 0;
						switch (objReceiveInfo.Status) {
							case (int)OrdRefundStatus.已完成:
								count = OrdrefundService.ReceiveNormal(userCode, objReceiveInfo.OrdRefundID, objReceiveInfo.ReceiveRemark, objReceiveInfo.Duty, objReceiveInfo.DutyOther, objReceiveInfo.RefundAmount, objReceiveInfo.RefundFreight, objReceiveInfo.ReturnFreight, context);
								break;
							case (int)OrdRefundStatus.收货异常:
								count = OrdrefundService.ReceiveException(userCode, objReceiveInfo.OrdRefundID, objReceiveInfo.ReceiveRemark, context);
								break;
						}
						if (count > 0) {
							if (objReceiveInfo.Status == (int)OrdRefundStatus.已完成) {
								#region 生成退款单
								OrdaccountsBill ordaccountsBill = new OrdaccountsBill();
								ordaccountsBill.BillNo = Sys.GetBillNo(BillType.TK.ToString());
								ordaccountsBill.BillType = (int)BillType.TK;
								ordaccountsBill.BillWay = -1;
								ordaccountsBill.ErpOrderCode = objReceiveInfo.ErpOrderCode;
								ordaccountsBill.AssociatedCode = ordRefund.BillNo;
								if (objReceiveInfo.Duty == (int)OrdRefundDuty.买家) {
									ordaccountsBill.Amount = objReceiveInfo.RefundAmount + objReceiveInfo.RefundFreight;
								}
								else {
									ordaccountsBill.Amount = objReceiveInfo.RefundAmount + objReceiveInfo.RefundFreight + objReceiveInfo.ReturnFreight;
								}
								ordaccountsBill.Remark = "售后确认收货时生成退款单";
								ordaccountsBill.CreatePerson = userCode;
								ordaccountsBill.CreateDate = DateTime.Now;
								int ordaccountsBillID = OrdaccountsBillService.Add(ordaccountsBill, context);
								if (ordaccountsBillID == 0) {
									resultInfo.result = 0;
									resultInfo.message = "生成退款单失败";
								}
								#endregion

								if (resultInfo.result == 1) {
									#region 创建退货入库单，并更新库存到中转仓
									string inStockBillNo = Sys.GetBillNo(BillType.THR.ToString());
									WarehouseOutInStock objWarehouseOutInStock = new WarehouseOutInStock();
									objWarehouseOutInStock.WarehouseCode = ordRefund.WarehouseCode;
									objWarehouseOutInStock.SourceID = ordRefund.ID;
									objWarehouseOutInStock.SourceNo = ordRefund.BillNo;
									objWarehouseOutInStock.BillNo = inStockBillNo;
									objWarehouseOutInStock.BillType = (int)BillType.THR;
									objWarehouseOutInStock.CountName = userCode;
									objWarehouseOutInStock.MainName = userCode;
									objWarehouseOutInStock.OutInDate = DateTime.Now;
									objWarehouseOutInStock.CreatePerson = userCode;
									objWarehouseOutInStock.CreateDate = DateTime.Now;
									objWarehouseOutInStock.Remark = "售后确认收货";
									objWarehouseOutInStock.IsAuditPrice = (int)IsEnable.是;
									objWarehouseOutInStock.IsDxYs = (int)IsEnable.否;
									objWarehouseOutInStock.Settlement = (int)IsEnable.是;
									objWarehouseOutInStock.Status = (int)WarehouseOutInStockStatus.已审核;
									objWarehouseOutInStock.ConfirmDate = DateTime.Now;
									int inStockID = WarehouseOutInStockService.Add(objWarehouseOutInStock, context);
									if (inStockID > 0) {
										WarehouseLocation warehouseLocation = WarehouseLocationService.GetSingleWarehouseLocation(ordRefund.WarehouseCode, (int)LocationType.中转区, context);
										if (warehouseLocation != null) {
											for (int i = 0; i < objReceiveInfo.OrdRefundItemID.Length; i++) {
												int ordRefundItemID = objReceiveInfo.OrdRefundItemID[i];
												string receiveNum = objReceiveInfo.ReceiveNum[i];
												string productsSkuCode = objReceiveInfo.ProductsSkuCode[i];
												int productsID = objReceiveInfo.ProductsID[i];
												string productsName = objReceiveInfo.ProductsName[i];
												string productsCode = objReceiveInfo.ProductsCode[i];
												string productsNo = objReceiveInfo.ProductsNo[i];
												string productsSkuSaleprop = objReceiveInfo.ProductsSkuSaleprop[i];
												int productsSkuID = objReceiveInfo.ProductsSkuID[i];
												int productsBatchID = objReceiveInfo.ProductsBatchID[i];
												string productsBatchCode = objReceiveInfo.ProductsBatchCode[i];
												if (receiveNum == "") {
													resultInfo.result = 0;
													resultInfo.message = "商品SKU码[" + productsSkuCode + "]批次号[" + productsBatchCode + "]收货数量未输入！";
													break;
												}
												OrdrefundItem ordRefundItem = OrdrefundItemService.GetQuerySingleByID(ordRefundItemID, context);
												if (ZConvert.StrToInt(receiveNum) > ordRefundItem.RefundNum) {
													resultInfo.result = 0;
													resultInfo.message = "商品SKU码[" + productsSkuCode + "]批次号[" + productsBatchCode + "]收货数量超过了售后数量！";
													break;
												}
												resultInfo = UpdateKucInfo(userCode, warehouseLocation.WarehouseCode, inStockID, inStockBillNo, ordRefund.ID, ordRefund.BillNo, ordRefundItemID, productsID, productsName, productsCode, productsNo, productsSkuSaleprop, productsSkuID, productsSkuCode, warehouseLocation.ID, productsBatchID, productsBatchCode, ZConvert.StrToInt(receiveNum), context);
												if (resultInfo.result == 0) {
													break;
												}
											}
										}
										else {
											resultInfo.result = 0;
											resultInfo.message = "仓库编码[" + ordRefund.WarehouseCode + "]没有中转仓！";
										}
									}
									else {
										resultInfo.result = 0;
										resultInfo.message = "创建退货入库单失败！";
									}
									#endregion
								}
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "更新售后单失败，请检查售后单状态！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "状态为" + GetStatusName(ordRefund.Status) + "的售后单，不能进行此操作！";
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
				Sys.SaveErrorLog(ex, "确认收货", userCode);
			}
			return resultInfo;
		}

		#region 创建退货入库明细、更新中转仓库存，增加库存日志

		/// <summary>
		/// 创建退货入库明细、更新中转仓库存，增加库存日志
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="inStockID">退货入库单主键ID</param>
		/// <param name="inStockBillNo">退货入库单号</param>
		/// <param name="sourceID">售后单主键ID</param>
		/// <param name="sourceNo">售后单号</param>
		/// <param name="sourceItemID">售后单商品明细主键ID</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="productsName"></param>
		/// <param name="productsCode"></param>
		/// <param name="productsNo"></param>
		/// <param name="productsSkuSaleprop"></param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="productsSkuCode">商品SKU码</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">批次ID</param>
		/// <param name="productsBatchCode">批次号</param>
		/// <param name="receiveNum">收货数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		private static BaseResult UpdateKucInfo(string userCode, string warehouseCode, int inStockID, string inStockBillNo, int sourceID, string sourceNo, int sourceItemID, int productsID, string productsName, string productsCode, string productsNo, string productsSkuSaleprop, int productsSkuID, string productsSkuCode, int locationID, int productsBatchID, string productsBatchCode, int receiveNum, IDbContext context) {
			BaseResult resultInfo = new BaseResult();
			int count = 0;
			int inStockItemID = 0;
			#region 更新批次表库存
			WarehouseProductsBatch warehouseProductsBatch = WarehouseProductsBatchService.GetQuerySingleByID(productsBatchID, context);
			if (warehouseProductsBatch != null) {
				warehouseProductsBatch.KyNum += receiveNum;
				warehouseProductsBatch.ZkNum += receiveNum;
				count = WarehouseProductsBatchService.Update(warehouseProductsBatch, context);
				if (count == 0) {
					resultInfo.result = 0;
					resultInfo.message = "商品SKU码[" + productsSkuCode + "]批次号[" + productsBatchCode + "]更新批次库存失败！";
				}
			}
			else {
				resultInfo.result = 0;
				resultInfo.message = "商品SKU码[" + productsSkuCode + "]批次号[" + productsBatchCode + "]不存在！";
			}
			#endregion
			if (resultInfo.result == 1) {
				#region 更新库位商品库存
				count = WarehouseLocationProductsService.UpdateKyNumAndZkNum(userCode, productsSkuID, locationID, productsBatchID, receiveNum, context);
				if (count == 0) {
					WarehouseLocationProducts warehouseLocationProducts = new WarehouseLocationProducts();
					warehouseLocationProducts.KyNum = receiveNum;
					warehouseLocationProducts.LocationID = locationID;
					warehouseLocationProducts.LocationTypeID = (int)LocationType.中转区;
					warehouseLocationProducts.ProductionDate = warehouseProductsBatch.ProductionDate;
					warehouseLocationProducts.ProductsBatchCode = productsBatchCode;
					warehouseLocationProducts.ProductsBatchID = productsBatchID;
					warehouseLocationProducts.ProductsID = productsID;
					warehouseLocationProducts.ProductsSkuID = productsSkuID;
					warehouseLocationProducts.ShelfLife = warehouseProductsBatch.ShelfLife;
					warehouseLocationProducts.TopLocationID = locationID;
					warehouseLocationProducts.WarehouseCode = warehouseCode;
					warehouseLocationProducts.ZkNum = receiveNum;
					warehouseLocationProducts.CreatePerson = userCode;
					warehouseLocationProducts.CreateDate = DateTime.Now;
					int warehouseLocationProductsID = WarehouseLocationProductsService.Add(warehouseLocationProducts, context);
					if (warehouseLocationProductsID == 0) {
						resultInfo.result = 0;
						resultInfo.message = "商品SKU码[" + productsSkuCode + "]批次号[" + productsBatchCode + "]更新中转仓库存失败！";
					}
				}
				#endregion
			}
			if (resultInfo.result == 1) {
				#region 创建退货入库明细
				WarehouseOutInStockItem inStockItem = new WarehouseOutInStockItem();
				inStockItem.WarehouseCode = warehouseCode;
				inStockItem.BillType = (int)BillType.THR;
				inStockItem.StockWay = (int)StockWay.入库;
				inStockItem.OutInStockID = inStockID;
				inStockItem.OutInStockBillNo = inStockBillNo;
				inStockItem.SourceID = sourceID;
				inStockItem.SourceNo = sourceNo;
				inStockItem.SourceItemID = sourceItemID;
				inStockItem.ProductsID = productsID;
				inStockItem.ProductsCode = productsCode;
				inStockItem.ProductsName = productsName;
				inStockItem.ProductsNo = productsNo;
				inStockItem.ProductsSkuID = productsSkuID;
				inStockItem.ProductsSkuCode = productsSkuCode;
				inStockItem.ProductsSkuSaleprop = productsSkuSaleprop;
				inStockItem.LocationID = locationID;
				inStockItem.ProductsBatchID = productsBatchID;
				inStockItem.ProductsBatchCode = productsBatchCode;
				inStockItem.ProductionDate = warehouseProductsBatch.ProductionDate;
				inStockItem.ProductsNum = receiveNum;
				inStockItem.CostPrice = warehouseProductsBatch.CostPrice;
				inStockItem.Remark = "售后确认收货";
				inStockItem.CreatePerson = userCode;
				inStockItem.CreateDate = DateTime.Now;
				inStockItem.Status = (int)WarehouseOutInStockStatus.已审核;
				inStockItem.IsAuditPrice = (int)IsEnable.是;
				inStockItem.ConfirmDate = DateTime.Now;
				inStockItemID = WarehouseOutInStockItemService.Add(inStockItem, context);
				if (inStockItemID == 0) {
					resultInfo.result = 0;
					resultInfo.message = "商品SKU码[" + productsSkuCode + "]批次号[" + productsBatchCode + "]创建退货入库明细失败！";
				}
				#endregion
			}
			if (resultInfo.result == 1) {
				#region 增加库存日志
				WarehouseOutInStockLog warehouseOutInStockLog = new WarehouseOutInStockLog();
				warehouseOutInStockLog.BillType = (int)BillType.THR;
				warehouseOutInStockLog.SourceID = sourceID;
				warehouseOutInStockLog.SourceNo = sourceNo;
				warehouseOutInStockLog.SourceItemID = inStockItemID;
				warehouseOutInStockLog.StockWay = (int)StockWay.入库;
				warehouseOutInStockLog.WarehouseCode = warehouseCode;
				warehouseOutInStockLog.ProductsID = productsID;
				warehouseOutInStockLog.ProductsName = productsName;
				warehouseOutInStockLog.ProductsCode = productsCode;
				warehouseOutInStockLog.ProductsNo = productsNo;				
				warehouseOutInStockLog.ProductsSkuSaleprop = productsSkuSaleprop;
				warehouseOutInStockLog.ProductsSkuID = productsSkuID;
				warehouseOutInStockLog.ProductsSkuCode = productsSkuCode;
				warehouseOutInStockLog.ProductsBatchID = productsBatchID;
				warehouseOutInStockLog.ProductsBatchCode = productsBatchCode;
				warehouseOutInStockLog.LocationID = locationID;
				warehouseOutInStockLog.Num = receiveNum;
				warehouseOutInStockLog.Remark = "退货入库";
				warehouseOutInStockLog.CreatePerson = userCode;
				warehouseOutInStockLog.CreateDate = DateTime.Now;
				int warehouseOutInStockLogID = WarehouseOutInStockLogService.Add(warehouseOutInStockLog, context);
				if (warehouseOutInStockLogID == 0) {
					resultInfo.result = 0;
					resultInfo.message = "商品SKU码[" + productsSkuCode + "]批次号[" + productsBatchCode + "]增加库存日志失败！";
				}
				#endregion
			}
			return resultInfo;
		}

		#endregion

		#endregion

		#region 取消售后

		/// <summary>
		/// 取消售后
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="billNo">售后单号</param>
		/// <returns></returns>
		public static BaseResult Cancel(string userCode, string warehouseCode, string billNo) {
			BaseResult resultInfo = new BaseResult();
			try {
				Ordrefund ordRefund = OrdrefundService.GetQuerySingleByBillNo(warehouseCode, billNo);
				if (ordRefund != null) {
					if (ordRefund.Status != (int)OrdRefundStatus.已取消 && ordRefund.Status != (int)OrdRefundStatus.已完成) {
						int count = OrdrefundService.Cancel(userCode, warehouseCode, billNo);
						if (count == 0) {
							resultInfo.result = 0;
							resultInfo.message = "售后单[" + billNo + "]取消失败，请检查是否已取消或已完成！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "售后单[" + billNo + "]状态为" + GetStatusName(ordRefund.Status) + "，不能取消！";
					}
				}
				else {
					resultInfo.result = 0;
					resultInfo.message = "售后单[" + billNo + "]不存在！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "取消售后单[" + billNo + "]", userCode);
			}
			return resultInfo;
		}

		#endregion
	}
}
