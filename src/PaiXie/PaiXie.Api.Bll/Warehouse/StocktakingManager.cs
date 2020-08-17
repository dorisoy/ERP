using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PaiXie.Api.Bll {
	public class StocktakingManager {

		#region 添加盘点记录
		/// <summary>
		/// 添加盘点记录
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="warehouseStocktakingWebInfo">盘点记录实体</param>
		/// <returns></returns>
		public static BaseResult AddStocktaking(string warehouseCode, WarehouseStocktakingWebInfo warehouseStocktakingWebInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					int cnt = WarehouseStocktakingService.GetUnconfirmedCount(warehouseCode, context);
					if (cnt > 0) {
						resultInfo.result = 0;
						resultInfo.message = "有盘点记录未确认！";
					}
					else {
						string billNo = Sys.GetBillNo(BillType.PD.ToString());
						DateTime createDate = DateTime.Now;
						WarehouseStocktaking stocktaking = new WarehouseStocktaking();
						stocktaking.BillNo = billNo;
						stocktaking.WarehouseCode = warehouseCode;
						stocktaking.LocationID = string.Join(",", warehouseStocktakingWebInfo.LocationID);
						stocktaking.LocationName = string.Join(",", warehouseStocktakingWebInfo.LocationName);
						stocktaking.Remark = warehouseStocktakingWebInfo.Remark;
						stocktaking.CreatePerson = FormsAuth.GetUserCode();
						stocktaking.CreateDate = createDate;
						int stocktakingID = WarehouseStocktakingService.Add(stocktaking, context);
						if (stocktakingID == 0) {
							resultInfo.result = 0;
							resultInfo.message = "添加盘点记录失败！";
						}
						else {
							for (int i = 0; i < warehouseStocktakingWebInfo.LocationID.Length; i++) {
								int locationID = warehouseStocktakingWebInfo.LocationID[i];
								DataTable dt = WarehouseLocationService.GetTopLocationProducts(locationID, context);
								foreach (DataRow item in dt.Rows) {
									WarehouseStocktakingItem stocktakingItem = new WarehouseStocktakingItem();
									stocktakingItem.WarehouseCode = warehouseCode;
									stocktakingItem.StocktakingID = stocktakingID;
									stocktakingItem.BillNo = billNo;
									stocktakingItem.TopLocationID = locationID;
									stocktakingItem.SourceID = ZConvert.StrToInt(item["LocationProductsID"]);
									stocktakingItem.LocationID = ZConvert.StrToInt(item["LocationID"]);
									stocktakingItem.LocationCode = item["LocationCode"].ToString();
									stocktakingItem.LocationName = item["LocationName"].ToString();
									stocktakingItem.LocationTypeID = ZConvert.StrToInt(item["LocationTypeID"]);
									stocktakingItem.LocationProductsID = ZConvert.StrToInt(item["LocationProductsID"].ToString());
									stocktakingItem.ProductsID = ZConvert.StrToInt(item["ProductsID"]);
									stocktakingItem.ProductsCode = item["ProductsCode"].ToString();
									stocktakingItem.ProductsName = item["ProductsName"].ToString();
									stocktakingItem.ProductsNo = item["ProductsNo"].ToString();
									stocktakingItem.ProductsSkuID = ZConvert.StrToInt(item["ProductsSkuID"]);
									stocktakingItem.ProductsSkuCode = item["ProductsSkuCode"].ToString();
									stocktakingItem.ProductsSkuSaleprop = item["ProductsSkuSaleprop"].ToString();
									stocktakingItem.ProductsBatchID = ZConvert.StrToInt(item["ProductsBatchID"]);
									stocktakingItem.ProductsBatchCode = item["ProductsBatchCode"].ToString();
									if (item["ProductionDate"].ToString() != "") stocktakingItem.ProductionDate = ZConvert.StrToDateTime(item["ProductionDate"], DateTime.Now);
									stocktakingItem.ShelfLife = ZConvert.StrToInt(item["ShelfLife"]);
									stocktakingItem.KyNum = ZConvert.StrToInt(item["KyNum"]);
									stocktakingItem.ZyNum = ZConvert.StrToInt(item["ZyNum"]);
									stocktakingItem.DjNum = ZConvert.StrToInt(item["DjNum"]);
									stocktakingItem.SdNum = ZConvert.StrToInt(item["SdNum"]);
									stocktakingItem.ZkNum = ZConvert.StrToInt(item["ZkNum"]);
									stocktakingItem.KyNum = ZConvert.StrToInt(item["KyNum"]);
									stocktakingItem.CreatePerson = FormsAuth.GetUserCode();
									stocktakingItem.CreateDate = createDate;
									WarehouseProductsBatch WarehouseProductsBatch = WarehouseProductsBatchService.GetSingleWarehouseProductsBatch(warehouseCode, ZConvert.StrToInt(item["ProductsSkuID"]), item["ProductsBatchCode"].ToString());
									if (WarehouseProductsBatch != null) {
										stocktakingItem.CostPrice = WarehouseProductsBatch.CostPrice;
									}
									int stocktakingItemId = WarehouseStocktakingItemService.Add(stocktakingItem, context);
									if (stocktakingID == 0) {
										resultInfo.result = 0;
										resultInfo.message = "添加盘点记录商品失败！";
										break;
									}
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
				Sys.SaveErrorLog(ex, "添加盘点记录", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		#endregion

		#region 删除盘点记录
		/// <summary>
		/// 删除商品信息
		/// </summary>
		/// <param name="stocktakingID">盘点记录ID</param>
		/// <returns></returns>
		public static BaseResult DelStocktaking(int stocktakingID) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseStocktaking stocktaking = WarehouseStocktakingService.GetQuerySingleByID(stocktakingID, context);
					if (stocktaking != null && stocktaking.Status != (int)StocktakingStatus.未确认) {
						resultInfo.result = 0;
						resultInfo.message = "当前盘点记录状态不能删除！";
					}
					else {
						int rowsAffected = WarehouseStocktakingService.DelByID(stocktakingID, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "删除盘点记录失败！";
						}
						else {
							WarehouseStocktakingItemService.Delete(stocktakingID, context);
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
				Sys.SaveErrorLog(ex, "删除盘点记录", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		#endregion

		#region 确认盘点记录
		/// <summary>
		/// 确认商品信息
		/// </summary>
		/// <param name="stocktakingID">盘点记录ID</param>
		/// <returns></returns>
		public static BaseResult SubmitStocktaking(int stocktakingID) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					DateTime createDate = DateTime.Now;
					DateTime updateDate = createDate;
					int count = WarehouseStocktakingItemService.GetNotImportCount(stocktakingID,context);
					if (count == 0) {
						WarehouseStocktaking stocktaking = WarehouseStocktakingService.GetQuerySingleByID(stocktakingID);
						if (stocktaking.Status != (int)StocktakingStatus.待审核) {
							stocktaking.Status = (int)StocktakingStatus.待审核;
							stocktaking.UpdateDate = updateDate;
							stocktaking.ConfirmDate = updateDate;
							stocktaking.UpdatePerson = FormsAuth.GetUserCode();
							int rowsAffected = WarehouseStocktakingService.Update(stocktaking, context);
							if (rowsAffected > 0) {
								decimal costPrice = 0;
								int stockWay = 0;
								List<WarehouseStocktakingItem> StocktakingItemList = WarehouseStocktakingItemService.GetManyWarehouseStocktakingItem(stocktakingID);
								foreach (var item in StocktakingItemList) {
									if (item.PdNum < item.ZkNum - item.KyNum) {
										resultInfo.result = 0;
										resultInfo.message = "商品条码[" + item.ProductsCode + "]盘点异常,需要重新导入盘点数量或取消占用单据。";
										break;
									}

									int profitAndLossNum = item.PdNum - item.ZkNum;
									if (profitAndLossNum > 0) {
										#region 盘盈

										stockWay = (int)StockWay.入库;

										#region 添加新批次

										WarehouseProductsBatch warehouseProductsBatch = new WarehouseProductsBatch();
										warehouseProductsBatch.WarehouseCode = FormsAuth.GetWarehouseCode();
										warehouseProductsBatch.ProductsID = item.ProductsID;
										warehouseProductsBatch.ProductsSkuID = item.ProductsSkuID;
										warehouseProductsBatch.BatchCode = stocktaking.BillNo;
										warehouseProductsBatch.ShelfLife = item.ShelfLife;
										if (item.ProductsBatchID > 0)
											costPrice = WarehouseProductsBatchService.GetQuerySingleByID(item.ProductsBatchID, context).CostPrice;
										else
											costPrice = 0;
										warehouseProductsBatch.CostPrice = costPrice;
										warehouseProductsBatch.KyNum = profitAndLossNum;
										warehouseProductsBatch.ZkNum = profitAndLossNum;
										warehouseProductsBatch.CreatePerson = FormsAuth.GetUserCode();
										warehouseProductsBatch.CreateDate = createDate;
										int productsBatchID = WarehouseProductsBatchService.Add(warehouseProductsBatch, context);
										if (productsBatchID == 0) {
											resultInfo.result = 0;
											resultInfo.message = "盘点商品条码[" + item.ProductsCode + "]添加批次记录失败！";
											break;
										}

										#endregion

										#region 添加库位商品信息

										WarehouseLocationProducts newLocationProducts = new WarehouseLocationProducts();
										newLocationProducts.WarehouseCode = FormsAuth.GetWarehouseCode(); ;
										newLocationProducts.TopLocationID = item.TopLocationID;
										newLocationProducts.LocationID = item.LocationID;
										newLocationProducts.LocationTypeID = item.LocationTypeID;
										newLocationProducts.ProductsID = item.ProductsID;
										newLocationProducts.ProductsSkuID = item.ProductsSkuID;
										newLocationProducts.ProductsBatchID = productsBatchID;
										newLocationProducts.ProductsBatchCode = warehouseProductsBatch.BatchCode;
										newLocationProducts.ProductionDate = warehouseProductsBatch.ProductionDate;
										newLocationProducts.ShelfLife = warehouseProductsBatch.ShelfLife;
										newLocationProducts.KyNum = profitAndLossNum;
										newLocationProducts.ZkNum = profitAndLossNum;
										newLocationProducts.CreatePerson = FormsAuth.GetUserCode();
										newLocationProducts.CreateDate = createDate;
										int locationProductsID = WarehouseLocationProductsService.Add(newLocationProducts, context);
										if (locationProductsID == 0) {
											resultInfo.result = 0;
											resultInfo.message = "盘点商品条码[" + item.ProductsCode + "]添加库位商品失败！";
											break;
										}

										#endregion

										#endregion
									}
									else {
										#region 盘亏

										stockWay = (int)StockWay.出库;
										WarehouseLocationProducts locationProducts = WarehouseLocationProductsService.GetQuerySingleByID(item.LocationProductsID, context);
										locationProducts.KyNum = locationProducts.KyNum - System.Math.Abs(profitAndLossNum);
										locationProducts.ZkNum = locationProducts.KyNum + locationProducts.DjNum;
										locationProducts.UpdatePerson = FormsAuth.GetUserCode();
										locationProducts.UpdateDate = updateDate;
										rowsAffected = WarehouseLocationProductsService.Update(locationProducts, context);
										if (rowsAffected == 0) {
											resultInfo.result = 0;
											resultInfo.message = "盘点商品条码[" + item.ProductsCode + "]确认失败！";
											break;
										}

										#region 更新批次表库存信息

										WarehouseProductsBatch warehouseProductsBatch = WarehouseProductsBatchService.GetQuerySingleByID(item.ProductsBatchID, context);
										warehouseProductsBatch.KyNum = warehouseProductsBatch.KyNum - System.Math.Abs(profitAndLossNum);
										warehouseProductsBatch.ZkNum = warehouseProductsBatch.KyNum + warehouseProductsBatch.DjNum;
										warehouseProductsBatch.UpdatePerson = FormsAuth.GetUserCode();
										warehouseProductsBatch.UpdateDate = updateDate;
										rowsAffected = WarehouseProductsBatchService.Update(warehouseProductsBatch, context);
										costPrice = warehouseProductsBatch.CostPrice;
										if (rowsAffected == 0) {
											resultInfo.result = 0;
											resultInfo.message = "盘点商品条码[" + item.ProductsCode + "]更新批次信息失败！";
											break;
										}

										#endregion

										#endregion
									}

									if (resultInfo.result == 1) {
										#region 添加出入库日志

										WarehouseOutInStockLog warehouseOutInStockLog = new WarehouseOutInStockLog();
										warehouseOutInStockLog.WarehouseCode = FormsAuth.GetWarehouseCode();
										warehouseOutInStockLog.BillType = (int)BillType.PD;
										warehouseOutInStockLog.SourceID = stocktaking.ID;
										warehouseOutInStockLog.SourceNo = stocktaking.BillNo;
										warehouseOutInStockLog.SourceItemID = item.ID;
										warehouseOutInStockLog.StockWay = stockWay;
										warehouseOutInStockLog.ProductsID = item.ProductsID;
										warehouseOutInStockLog.ProductsNo = item.ProductsNo;
										warehouseOutInStockLog.ProductsName = item.ProductsName;
										warehouseOutInStockLog.ProductsCode = item.ProductsCode;
										warehouseOutInStockLog.ProductsSkuID = item.ProductsSkuID;
										warehouseOutInStockLog.ProductsSkuCode = item.ProductsSkuCode;
										warehouseOutInStockLog.ProductsSkuSaleprop = item.ProductsSkuSaleprop;
										warehouseOutInStockLog.LocationID = item.LocationID;
										warehouseOutInStockLog.ProductsBatchID = item.ProductsBatchID;
										warehouseOutInStockLog.ProductsBatchCode = item.ProductsBatchCode;
										warehouseOutInStockLog.Num = System.Math.Abs(profitAndLossNum);
										warehouseOutInStockLog.CreatePerson = FormsAuth.GetUserCode();
										warehouseOutInStockLog.CreateDate = createDate;
										int ID = WarehouseOutInStockLogService.Add(warehouseOutInStockLog, context);
										if (ID == 0) {
											resultInfo.result = 0;
											resultInfo.message = "盘点商品条码[" + item.ProductsCode + "]添加出入库日志失败！";
											break;
										}

										#endregion
									}
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "确认盘点记录失败！";
							}
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "有商品未盘点不能提交盘点记录！";
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
				Sys.SaveErrorLog(ex, "确认盘点记录", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		#endregion
	}
}
