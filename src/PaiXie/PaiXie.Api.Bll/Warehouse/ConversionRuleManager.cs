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
	public class ConversionRuleManager {

		#region 添加转换规则
		/// <summary>
		/// 添加转换规则
		/// </summary>
		/// <param name="WarehouseCode">仓库编码</param>
		/// <param name="WarehouseConversionRuleWebInfo">商品转换规则实体类</param>
		/// <returns></returns>
		public static BaseResult AddConversionRuleInfo(string warehouseCode, WarehouseConversionRuleWebInfo warehouseConversionRuleWebInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					DateTime createDate = DateTime.Now;
					WarehouseConversionRule warehouseConversionRule = new WarehouseConversionRule();
					warehouseConversionRule.WarehouseCode = warehouseCode;
					warehouseConversionRule.Name = warehouseConversionRuleWebInfo.Name;
					warehouseConversionRule.CreatePerson = FormsAuth.GetUserName(); ;
					warehouseConversionRule.CreateDate = createDate;
					int ruleID = WarehouseConversionRuleService.Add(warehouseConversionRule, context);
					if (ruleID == 0) {
						resultInfo.result = 0;
						resultInfo.message = "添加规则失败！";
					}
					else {
						for (int i = 0; i < warehouseConversionRuleWebInfo.ProductsSkuCode.Length; i++) {
							WarehouseProductsSkuInfo warehouseProductsSkuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(warehouseCode, warehouseConversionRuleWebInfo.ProductsSkuCode[i], context);
							if (warehouseProductsSkuInfo != null) {
								WarehouseConversionRuleItem warehouseConversionRuleItem = new WarehouseConversionRuleItem();
								warehouseConversionRuleItem.WarehouseCode = warehouseCode;
								warehouseConversionRuleItem.RuleName = warehouseConversionRuleWebInfo.Name;
								warehouseConversionRuleItem.RuleID = ruleID;
								warehouseConversionRuleItem.ProductsID = warehouseProductsSkuInfo.ProductsID;
								warehouseConversionRuleItem.ProductsSkuID = warehouseProductsSkuInfo.ID;
								warehouseConversionRuleItem.ProductsSkuCode = warehouseConversionRuleWebInfo.ProductsSkuCode[i];
								warehouseConversionRuleItem.Num = warehouseConversionRuleWebInfo.Num[i];
								warehouseConversionRuleItem.ConversionWay = warehouseConversionRuleWebInfo.ConversionWay[i];
								warehouseConversionRuleItem.CreatePerson = FormsAuth.GetUserName(); ;
								warehouseConversionRuleItem.CreateDate = createDate;

								int ruleItemId = WarehouseConversionRuleItemService.Add(warehouseConversionRuleItem, context);
								if (ruleItemId == 0) {
									resultInfo.result = 0;
									resultInfo.message = "添加转换的商品条码[" + warehouseConversionRuleItem.ProductsSkuCode + "]失败！";
									break;
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "添加转换的商品条码[" + warehouseConversionRuleWebInfo.ProductsSkuCode[i] + "]不存在！";
								break;
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
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "添加转换规则", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		#endregion

		#region 更新转换规则
		/// <summary>
		/// 更新转换规则
		/// </summary>
		/// <param name="WarehouseCode">仓库编码</param>
		/// <param name="warehouseConversionRuleInfo">商品转换规则实体类</param>
		/// <returns></returns>
		public static BaseResult UpdateConversionRuleInfo(string warehouseCode, WarehouseConversionRuleWebInfo warehouseConversionRuleWebInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					DateTime createDate = DateTime.Now;
					DateTime updateDate = createDate;
					WarehouseConversionRule warehouseConversionRule = WarehouseConversionRuleService.GetSingleWarehouseConversionRule(warehouseCode, warehouseConversionRuleWebInfo.RuleID, context);
					warehouseConversionRule.Name = warehouseConversionRuleWebInfo.Name;
					warehouseConversionRule.UpdatePerson = FormsAuth.GetUserName();
					warehouseConversionRule.UpdateDate = updateDate;
					int rowsAffected = WarehouseConversionRuleService.Update(warehouseConversionRule, context);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "更新规则失败！";
					}
					else {
						rowsAffected = WarehouseConversionRuleItemService.Delete(warehouseCode, new List<int> { warehouseConversionRule.ID }, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "删除转化商品SKU失败！";
						}
						else {
							for (int i = 0; i < warehouseConversionRuleWebInfo.ProductsSkuCode.Length; i++) {
								WarehouseProductsSkuInfo warehouseProductsSkuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(warehouseCode, warehouseConversionRuleWebInfo.ProductsSkuCode[i], context);
								if (warehouseProductsSkuInfo != null) {
									WarehouseConversionRuleItem warehouseConversionRuleItem = new WarehouseConversionRuleItem();
									warehouseConversionRuleItem.WarehouseCode = warehouseCode;
									warehouseConversionRuleItem.RuleName = warehouseConversionRuleWebInfo.Name;
									warehouseConversionRuleItem.RuleID = warehouseConversionRuleWebInfo.RuleID;
									warehouseConversionRuleItem.ProductsID = warehouseProductsSkuInfo.ProductsID;
									warehouseConversionRuleItem.ProductsSkuID = warehouseProductsSkuInfo.ID;
									warehouseConversionRuleItem.ProductsSkuCode = warehouseConversionRuleWebInfo.ProductsSkuCode[i];
									warehouseConversionRuleItem.Num = warehouseConversionRuleWebInfo.Num[i];
									warehouseConversionRuleItem.ConversionWay = warehouseConversionRuleWebInfo.ConversionWay[i];
									warehouseConversionRuleItem.CreatePerson = FormsAuth.GetUserName();
									warehouseConversionRuleItem.CreateDate = createDate;

									int ruleItemId = WarehouseConversionRuleItemService.Add(warehouseConversionRuleItem, context);
									if (ruleItemId == 0) {
										resultInfo.result = 0;
										resultInfo.message = "更新转化的商品条码[" + warehouseConversionRuleItem.ProductsSkuCode + "]失败！";
										break;
									}
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "更新转化的商品条码[" + warehouseConversionRuleWebInfo.ProductsSkuCode[i] + "]不存在！";
									break;
								}
							}
						}

						List<WarehouseConversionRuleItem> ruleItemList = WarehouseConversionRuleItemService.GetManyWarehouseConversionRuleItem(warehouseCode, warehouseConversionRuleWebInfo.RuleID, context);
						foreach (var ruleItem in ruleItemList) {
							var results = from p in warehouseConversionRuleWebInfo.ProductsSkuCode where p == ruleItem.ProductsSkuCode select p;
							if (results.Count() == 0) {
								rowsAffected = WarehouseConversionRuleItemService.Delete(warehouseCode, ruleItem.ID, context);
								if (rowsAffected == 0) {
									resultInfo.result = 0;
									resultInfo.message = "更新转化的商品条码[" + ruleItem.ProductsSkuCode + "]失败！";
									break;
								}
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
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "更新转换规则", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		#endregion

		#region 删除转换规则
		/// <summary>
		/// 删除转换规则
		/// </summary>
		/// <param name="WarehouseCode">仓库编码</param>
		/// <param name="ruleIDList">规则ID列表</param>
		/// <returns></returns>
		public static BaseResult DelConversionRuleInfo(string warehouseCode, List<int> ruleIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					int rowsAffected = WarehouseConversionRuleService.Delete(warehouseCode, ruleIDList, context);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "删除转换规则失败！";
					}
					else {
						rowsAffected = WarehouseConversionRuleItemService.Delete(warehouseCode, ruleIDList, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "删除转换规则SKU失败！";
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
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "删除转换规则", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		#endregion

		#region 确认商品转换
		/// <summary>
		/// 确认商品转换
		/// </summary>
		/// <param name="WarehouseCode">仓库编码</param>
		/// <param name="WarehouseConversionRuleWebInfo">商品转换规则实体类</param>
		/// <returns></returns>
		public static BaseResult SaveConversionInfo(string warehouseCode, WarehouseConversionRuleWebInfo warehouseConversionRuleWebInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					DateTime createDate = DateTime.Now;
					DateTime updateDate = createDate;
					string billNo = Sys.GetBillNo(BillType.ZH.ToString());
					WarehouseConversionLog warehouseConversionLog = new WarehouseConversionLog();
					warehouseConversionLog.WarehouseCode = warehouseCode;
					warehouseConversionLog.BillNo = billNo;
					warehouseConversionLog.CreateDate = createDate;
					warehouseConversionLog.CreatePerson = FormsAuth.GetUserName();
					int clID = WarehouseConversionLogService.Add(warehouseConversionLog, context);
					if (clID == 0) {
						resultInfo.result = 0;
						resultInfo.message = "添加商品转换记录失败！";
					}
					else {
						int rowsAffected = 0;
						decimal totalCostPrice = 0;
						Dictionary<int, decimal> costPriceList = new Dictionary<int, decimal>();
						WarehouseLocation warehouseLocation = WarehouseLocationService.GetSingleSubWarehouseLocation(FormsAuth.GetWarehouseCode(), (int)LocationType.中转区);
						for (int i = 0; i < warehouseConversionRuleWebInfo.ProductsSkuID.Length; i++) {
							decimal costPrice = 0;
							WarehouseProductsSkuInfo warehouseProductsSkuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(warehouseCode, warehouseConversionRuleWebInfo.ProductsSkuID[i], context);
							if (warehouseProductsSkuInfo != null) {
								int num = warehouseConversionRuleWebInfo.Num[i];
								int tempNum = 0;
								int ConversionWay = 0;
								if ((warehouseConversionRuleWebInfo.PermitTransformation == 0 && warehouseConversionRuleWebInfo.ConversionWay[i] == 0) || (warehouseConversionRuleWebInfo.PermitTransformation == 1 && warehouseConversionRuleWebInfo.ConversionWay[i] == 1)) {
									#region 转入数量

									ConversionWay = 1;

									#region 添加新批次

									WarehouseProductsBatch warehouseProductsBatch = new WarehouseProductsBatch();
									warehouseProductsBatch.WarehouseCode = warehouseCode;
									warehouseProductsBatch.ProductsID = warehouseProductsSkuInfo.ProductsID;
									warehouseProductsBatch.ProductsSkuID = warehouseProductsSkuInfo.ID;
									warehouseProductsBatch.BatchCode = billNo;
									warehouseProductsBatch.ShelfLife = WarehouseProductsService.GetSingleWarehouseProductsInfo(warehouseCode, warehouseProductsSkuInfo.ProductsID, context).ShelfLife;
									costPrice = getCostPrice(warehouseCode, warehouseConversionRuleWebInfo, warehouseConversionRuleWebInfo.ProductsSkuID[i], ref totalCostPrice, costPriceList, context);
									warehouseProductsBatch.CostPrice = costPrice;
									warehouseProductsBatch.KyNum = num;
									warehouseProductsBatch.ZkNum = num;
									warehouseProductsBatch.CreatePerson = FormsAuth.GetUserName();
									warehouseProductsBatch.CreateDate = createDate;
									int productsBatchID = WarehouseProductsBatchService.Add(warehouseProductsBatch, context);
									if (productsBatchID == 0) {
										resultInfo.result = 0;
										resultInfo.message = "添加商品条码[" + warehouseConversionRuleWebInfo.ProductsSkuCode[i] + "]批次记录失败！";
										break;
									}

									#endregion

									#region 添加库位商品信息

									WarehouseLocationProducts newLocationProducts = new WarehouseLocationProducts();
									newLocationProducts.WarehouseCode = warehouseCode;
									newLocationProducts.TopLocationID = warehouseLocation.ParentID;
									newLocationProducts.LocationID = warehouseLocation.ID;
									newLocationProducts.LocationTypeID = (int)LocationType.中转区;
									newLocationProducts.ProductsID = warehouseProductsSkuInfo.ProductsID;
									newLocationProducts.ProductsSkuID = warehouseProductsSkuInfo.ID;
									newLocationProducts.ProductsBatchID = productsBatchID;
									newLocationProducts.ProductsBatchCode = warehouseProductsBatch.BatchCode;
									newLocationProducts.ProductionDate = warehouseProductsBatch.ProductionDate;
									newLocationProducts.ShelfLife = warehouseProductsBatch.ShelfLife;
									newLocationProducts.KyNum = num;
									newLocationProducts.ZkNum = num;
									newLocationProducts.CreatePerson = FormsAuth.GetUserName();
									newLocationProducts.CreateDate = createDate;
									int locationProductsID = WarehouseLocationProductsService.Add(newLocationProducts, context);
									if (locationProductsID == 0) {
										resultInfo.result = 0;
										resultInfo.message = "添加商品条码[" + warehouseConversionRuleWebInfo.ProductsSkuCode[i] + "]库位商品失败！";
										break;
									}

									#endregion

									if (resultInfo.result == 1) {
										WarehouseConversionItemLog warehouseConversionItemLog = new WarehouseConversionItemLog();
										warehouseConversionItemLog.WarehouseCode = warehouseCode;
										warehouseConversionItemLog.BillNo = billNo;
										warehouseConversionItemLog.ClID = clID;
										warehouseConversionItemLog.ProductsID = newLocationProducts.ProductsID;
										warehouseConversionItemLog.ProductsID = newLocationProducts.ID;
										warehouseConversionItemLog.ProductsBatchID = newLocationProducts.ID;
										warehouseConversionItemLog.Num = num;
										warehouseConversionItemLog.ConversionWay = ConversionWay;
										warehouseConversionItemLog.CreatePerson = FormsAuth.GetUserName();
										warehouseConversionItemLog.CreateDate = createDate;
										int clItemId = WarehouseConversionItemLogService.Add(warehouseConversionItemLog, context);
										if (clItemId == 0) {
											resultInfo.result = 0;
											resultInfo.message = "添加商品条码[" + warehouseConversionRuleWebInfo.ProductsSkuCode[i] + "]转换记录失败！";
											break;
										}
										else {
											#region 添加出入库日志

											WarehouseOutInStockLog warehouseOutInStockLog = new WarehouseOutInStockLog();
											warehouseOutInStockLog.WarehouseCode = warehouseCode;
											warehouseOutInStockLog.BillType = (int)BillType.ZH;
											warehouseOutInStockLog.SourceID = clID;
											warehouseOutInStockLog.SourceNo = billNo;
											warehouseOutInStockLog.SourceItemID = clItemId;
											warehouseOutInStockLog.StockWay = ConversionWay;
											warehouseOutInStockLog.ProductsID = newLocationProducts.ProductsID;
											WarehouseProductsInfo warehouseProductsInfo = WarehouseProductsService.GetSingleWarehouseProductsInfo(warehouseCode, newLocationProducts.ProductsID);
											warehouseOutInStockLog.ProductsNo = warehouseProductsInfo.No;
											warehouseOutInStockLog.ProductsName = warehouseProductsInfo.Name;
											warehouseOutInStockLog.ProductsCode = warehouseProductsInfo.Code;
											warehouseOutInStockLog.ProductsSkuID = newLocationProducts.ProductsSkuID;
											warehouseOutInStockLog.ProductsSkuCode = warehouseProductsSkuInfo.Code;
											warehouseOutInStockLog.ProductsSkuSaleprop = warehouseProductsSkuInfo.Saleprop;
											warehouseOutInStockLog.LocationID = newLocationProducts.LocationID;
											warehouseOutInStockLog.ProductsBatchID = productsBatchID;
											warehouseOutInStockLog.ProductsBatchCode = warehouseProductsBatch.BatchCode;
											warehouseOutInStockLog.Num = num;
											warehouseOutInStockLog.CreatePerson = FormsAuth.GetUserName();
											warehouseOutInStockLog.CreateDate = createDate;
											int ID = WarehouseOutInStockLogService.Add(warehouseOutInStockLog, context);
											if (ID == 0) {
												resultInfo.result = 0;
												resultInfo.message = "添加商品条码[" + warehouseConversionRuleWebInfo.ProductsSkuCode[i] + "]出入库日志失败！";
												break;
											}

											#endregion
										}
									}

									#endregion
								}
								else {
									#region 转出数量

									ConversionWay = -1;
									List<WarehouseLocationProducts> warehouseLocationProductsList = WarehouseLocationProductsService.GetManyWarehouseLocationProducts(warehouseCode, warehouseProductsSkuInfo.ID, warehouseLocation.ID, context);
									foreach (var item in warehouseLocationProductsList) {
										int zcNum = 0;
										int kucNum = item.KyNum;
										if (kucNum == 0) continue;
										tempNum += kucNum;
										if (num > tempNum) {
											zcNum = kucNum;
											item.KyNum = 0;
										}
										else {
											if (tempNum == kucNum) {
												zcNum = num;
											}
											else {
												zcNum = kucNum - (tempNum - num);
											}

											item.KyNum = kucNum - zcNum;
										}
										item.ZkNum = item.KyNum + item.DjNum;
										item.UpdatePerson = FormsAuth.GetUserName();
										item.UpdateDate = updateDate;
										rowsAffected = WarehouseLocationProductsService.Update(item, context);
										if (rowsAffected == 0) {
											resultInfo.result = 0;
											resultInfo.message = "转换商品条码[" + warehouseConversionRuleWebInfo.ProductsSkuCode[i] + "]失败！";
											break;
										}
										else {
											#region 更新批次表库存信息

											WarehouseProductsBatch warehouseProductsBatch = WarehouseProductsBatchService.GetQuerySingleByID(item.ProductsBatchID, context);
											warehouseProductsBatch.KyNum = warehouseProductsBatch.KyNum - zcNum;
											warehouseProductsBatch.ZkNum = warehouseProductsBatch.KyNum + warehouseProductsBatch.DjNum;
											warehouseProductsBatch.UpdatePerson = FormsAuth.GetUserName();
											warehouseProductsBatch.UpdateDate = updateDate;
											costPrice = warehouseProductsBatch.CostPrice;
											rowsAffected = WarehouseProductsBatchService.Update(warehouseProductsBatch, context);

											if (rowsAffected == 0) {
												resultInfo.result = 0;
												resultInfo.message = "转换商品条码[" + warehouseConversionRuleWebInfo.ProductsSkuCode[i] + "]更新批次信息失败！";
												break;
											}

											#endregion
										}

										if (resultInfo.result == 1) {
											WarehouseConversionItemLog warehouseConversionItemLog = new WarehouseConversionItemLog();
											warehouseConversionItemLog.WarehouseCode = warehouseCode;
											warehouseConversionItemLog.BillNo = billNo;
											warehouseConversionItemLog.ClID = clID;
											warehouseConversionItemLog.ProductsID = item.ProductsID;
											warehouseConversionItemLog.ProductsID = item.ID;
											warehouseConversionItemLog.ProductsBatchID = item.ProductsBatchID;
											warehouseConversionItemLog.Num = zcNum;
											warehouseConversionItemLog.ConversionWay = ConversionWay;
											warehouseConversionItemLog.CreatePerson = FormsAuth.GetUserName();
											warehouseConversionItemLog.CreateDate = createDate;
											int clItemId = WarehouseConversionItemLogService.Add(warehouseConversionItemLog, context);
											if (clItemId == 0) {
												resultInfo.result = 0;
												resultInfo.message = "添加商品条码[" + warehouseConversionRuleWebInfo.ProductsSkuCode[i] + "]转换记录失败！";
												break;
											}
											else {
												#region 添加出入库日志

												WarehouseOutInStockLog warehouseOutInStockLog = new WarehouseOutInStockLog();
												warehouseOutInStockLog.WarehouseCode = warehouseCode;
												warehouseOutInStockLog.BillType = (int)BillType.ZH;
												warehouseOutInStockLog.SourceID = clID;
												warehouseOutInStockLog.SourceNo = billNo;
												warehouseOutInStockLog.SourceItemID = clItemId;
												warehouseOutInStockLog.StockWay = ConversionWay;
												warehouseOutInStockLog.ProductsID = item.ProductsID;
												WarehouseProductsInfo warehouseProductsInfo = WarehouseProductsService.GetSingleWarehouseProductsInfo(warehouseCode, item.ProductsID, context);
												warehouseOutInStockLog.ProductsNo = warehouseProductsInfo.No;
												warehouseOutInStockLog.ProductsName = warehouseProductsInfo.Name;
												warehouseOutInStockLog.ProductsCode = warehouseProductsInfo.Code;
												warehouseOutInStockLog.ProductsSkuID = item.ProductsSkuID;
												warehouseOutInStockLog.ProductsSkuCode = warehouseProductsSkuInfo.Code;
												warehouseOutInStockLog.ProductsSkuSaleprop = warehouseProductsSkuInfo.Saleprop;
												warehouseOutInStockLog.LocationID = item.LocationID;
												warehouseOutInStockLog.ProductsBatchID = item.ProductsBatchID;
												warehouseOutInStockLog.ProductsBatchCode = item.ProductsBatchCode;
												warehouseOutInStockLog.Num = zcNum;
												warehouseOutInStockLog.CreatePerson = FormsAuth.GetUserCode();
												warehouseOutInStockLog.CreateDate = createDate;
												int ID = WarehouseOutInStockLogService.Add(warehouseOutInStockLog, context);
												if (ID == 0) {
													resultInfo.result = 0;
													resultInfo.message = "添加商品条码[" + warehouseConversionRuleWebInfo.ProductsSkuCode[i] + "]出入库日志失败！";
													break;
												}

												#endregion
											}
										}
										if (num <= tempNum) break;
									}

									#endregion
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "转换的商品条码[" + warehouseConversionRuleWebInfo.ProductsSkuCode[i] + "]不存在！";
								break;
							}
							if (resultInfo.result == 0) break;
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
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "确认商品转换", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		/// <summary>
		/// 获取批次采购价
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="warehouseConversionRuleWebInfo"></param>
		/// <param name="productsSkuID"></param>
		/// <param name="totalCostPrice"></param>
		/// <param name="costPriceList"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		private static decimal getCostPrice(string warehouseCode, WarehouseConversionRuleWebInfo warehouseConversionRuleWebInfo, int productsSkuID, ref decimal totalCostPrice, Dictionary<int, decimal> costPriceList, IDbContext context) {
			decimal costPrice = 0;
			if (totalCostPrice == 0) {
				WarehouseLocation warehouseLocation = WarehouseLocationService.GetSingleSubWarehouseLocation(FormsAuth.GetWarehouseCode(), (int)LocationType.中转区);
				for (int i = 0; i < warehouseConversionRuleWebInfo.ProductsSkuID.Length; i++) {
					int num = warehouseConversionRuleWebInfo.Num[i];
					if ((warehouseConversionRuleWebInfo.PermitTransformation == 0 && warehouseConversionRuleWebInfo.ConversionWay[i] != 0) || (warehouseConversionRuleWebInfo.PermitTransformation == 1 && warehouseConversionRuleWebInfo.ConversionWay[i] != 1)) {
						int tempNum = 0;
						List<WarehouseLocationProducts> warehouseLocationProductsList = WarehouseLocationProductsService.GetManyWarehouseLocationProducts(warehouseCode, warehouseConversionRuleWebInfo.ProductsSkuID[i], warehouseLocation.ID, context);
						foreach (var item in warehouseLocationProductsList) {
							int kucNum = item.KyNum;
							if (kucNum > 0) {
								tempNum += kucNum;
								if (num > tempNum) {
									totalCostPrice += kucNum * WarehouseProductsBatchService.GetQuerySingleByID(item.ProductsBatchID, context).CostPrice;
								}
								else {
									if (tempNum == kucNum) {
										totalCostPrice += num * WarehouseProductsBatchService.GetQuerySingleByID(item.ProductsBatchID, context).CostPrice;
									}
									else {
										totalCostPrice += (kucNum - (tempNum - num)) * WarehouseProductsBatchService.GetQuerySingleByID(item.ProductsBatchID, context).CostPrice;
									}
									break;
								}
							}
						}
					}
					else {
						if (warehouseConversionRuleWebInfo.PermitTransformation == 1) {
							WarehouseProductsBatch batch = WarehouseProductsBatchService.GetLatestWarehouseProductsBatch(warehouseCode, warehouseConversionRuleWebInfo.ProductsSkuID[i], context);
							if (batch != null) {
								decimal tempCostPrice = batch.CostPrice * num;
								costPriceList.Add(warehouseConversionRuleWebInfo.ProductsSkuID[i], tempCostPrice);
							}
						}
					}
				}
			}

			if (warehouseConversionRuleWebInfo.PermitTransformation == 0) {
				costPrice = totalCostPrice / warehouseConversionRuleWebInfo.Num[0];
			}
			else {
				int ii = 1;
				decimal tempTotalCostPrice = 0;
				foreach (var key in costPriceList.Keys) {
					if (productsSkuID == key) {
						if (ii == costPriceList.Count) {
							costPrice = Math.Truncate((totalCostPrice - tempTotalCostPrice) / warehouseConversionRuleWebInfo.Num[ii] * 1000) / 1000;
						}
						else {
							costPrice = Math.Truncate(totalCostPrice * (costPriceList[key] / totalCostPrice) / warehouseConversionRuleWebInfo.Num[ii] * 1000) / 1000;
						}
					}
					if (costPrice > 0) break;
					tempTotalCostPrice += costPriceList[key];
					ii++;
				}
			}

			return costPrice;
		}

		#endregion
	}
}
