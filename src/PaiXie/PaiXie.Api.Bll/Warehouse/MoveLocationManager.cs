using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Api.Bll {

	/// <summary>
	/// 移位管理类
	/// </summary>
	public class MoveLocationManager {

		#region 添加移位单

		/// <summary>
		/// 添加移位单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="remark">备注</param>
		public static BaseResult AddMoveLocation(string userCode,string warehouseCode, string remark) {
			BaseResult resultInfo = new BaseResult();
			try {
				WarehouseMoveLocation warehouseMoveLocation = new WarehouseMoveLocation();
				warehouseMoveLocation.BillNo = Sys.GetBillNo(BillType.YW.ToString());
				warehouseMoveLocation.WarehouseCode = warehouseCode;
				warehouseMoveLocation.Status = (int)MoveLocationStatus.未确认;
				warehouseMoveLocation.Remark = remark;
				warehouseMoveLocation.CreatePerson = userCode;
				warehouseMoveLocation.CreateDate = DateTime.Now;
				int id = WarehouseMoveLocationService.Add(warehouseMoveLocation);
				if (id == 0) {
					resultInfo.result = 0;
					resultInfo.message = "添加移位单失败！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "添加移位单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除移位单

		/// <summary>
		/// 删除移位单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="idList">移位单主键ID列表</param>
		/// <returns></returns>
		public static BaseResult DelMoveLocation(string userCode, List<int> idList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (int moveLocationID in idList) {
						WarehouseMoveLocation warehouseMoveLocation = WarehouseMoveLocationService.GetQuerySingleByID(moveLocationID, context);
						int rowsAffected = WarehouseMoveLocationService.DelByID(moveLocationID, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "删除失败，未确认状态才可以删除！";
							break;
						}
						else {
							//删除之前获取所有商品
							List<WarehouseMoveLocationItem> moveLocationItemList = WarehouseMoveLocationItemService.GetWarehouseMoveLocationItemList(moveLocationID, context);
							if (moveLocationItemList.Count > 0) {
								int count = WarehouseMoveLocationItemService.DeleteByMoveLocationID(moveLocationID, context);
								if (count > 0) {
									#region 还原可用，解除冻结

									foreach (var moveLocationItem in moveLocationItemList) {
										count = WarehouseProductsBatchService.UpdateDjNumAndKyNum(userCode, moveLocationItem.ProductsBatchID, -moveLocationItem.Num, context);
										if (count > 0) {
											count = WarehouseLocationProductsService.UpdateDjNumAndKyNum(userCode, moveLocationItem.ProductsSkuID, moveLocationItem.OutLocationID, moveLocationItem.ProductsBatchID, -moveLocationItem.Num, context);
											if (count == 0) {
												string locationCode = string.Empty;
												WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(moveLocationItem.OutLocationID, context);
												if (warehouseLocation != null) {
													locationCode = warehouseLocation.Code;
												}
												resultInfo.result = 0;
												resultInfo.message = "删除失败，商品SKU码 " + moveLocationItem.ProductsSkuCode + " 商品批次 " + moveLocationItem.ProductsBatchCode + " 库位编码 " + locationCode + " 解除冻结失败！";
												break;
											}
										}
										else {
											resultInfo.result = 0;
											resultInfo.message = "删除失败，商品SKU码 " + moveLocationItem.ProductsSkuCode + " 商品批次 " + moveLocationItem.ProductsBatchCode + " 解除冻结失败！";
											break;
										}
									}

									#endregion
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "删除商品失败！";
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
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "删除移位单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存移位单商品

		/// <summary>
		/// 保存移位单商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="objWebInfo">移位单商品实体类</param>
		/// <param name="context">数据库连接对象 外部传值则外部提交，否则内部自动创建并提交</param>
		/// <returns></returns>
		public static BaseResult AddMoveLocationItem(string userCode, string warehouseCode, MoveLocationItemWebInfo objWebInfo, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				bool submitTran = false;
				if (context == null) {
					context = Db.GetInstance().Context();
					context.UseTransaction(true);
					submitTran = true;
				}
				WarehouseMoveLocation moveLocation = WarehouseMoveLocationService.GetQuerySingleByID(objWebInfo.MoveLocationID);
				if (moveLocation.Status == (int)MoveLocationStatus.未确认) {
					int productsSkuID = objWebInfo.ProductsSkuID;
					string productsSkuCode = objWebInfo.ProductsSkuCode;
					string productsSkuSaleprop = objWebInfo.ProductsSkuSaleprop;
					string inLocationCode = objWebInfo.InLocationCode;
					int inLocationID = objWebInfo.InLocationID;
					for (int i = 0; i < objWebInfo.MoveNum.Length; i++) {
						int moveNum = objWebInfo.MoveNum[i];
						if (moveNum == 0) {
							continue;
						}
						int outLocationID = objWebInfo.OutLocationID[i];
						string outLocationCode = objWebInfo.OutLocationCode[i];
						int productsBatchID = objWebInfo.ProductsBatchID[i];
						string productsBatchCode = objWebInfo.ProductsBatchCode[i];
						if (objWebInfo.InLocationID == outLocationID) {
							resultInfo.result = 0;
							resultInfo.message = "商品SKU码 " + productsSkuCode + " 商品批次 " + productsBatchCode + " 移出库位编码和移入库位编码不能一样！";
							break;
						}
						WarehouseProductsSkuInfo productsSkuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(warehouseCode, productsSkuID, context);
						if (productsSkuInfo != null) {
							WarehouseMoveLocationItem moveLocationItem = WarehouseMoveLocationItemService.GetSingleWarehouseMoveLocationItem(objWebInfo.MoveLocationID, productsSkuID, outLocationID, inLocationID, productsBatchID, context);
							if (moveLocationItem == null) {
								moveLocationItem = new WarehouseMoveLocationItem();
								moveLocationItem.MoveLocationBillNo = objWebInfo.MoveLocationBillNo;
								moveLocationItem.MoveLocationID = objWebInfo.MoveLocationID;
								moveLocationItem.WarehouseCode = warehouseCode;
								moveLocationItem.ProductsCode = objWebInfo.ProductsCode;
								moveLocationItem.ProductsID = objWebInfo.ProductsID;
								moveLocationItem.ProductsName = objWebInfo.ProductsName;
								moveLocationItem.ProductsNo = objWebInfo.ProductsNo;
								moveLocationItem.ProductsSkuCode = productsSkuCode;
								moveLocationItem.ProductsSkuID = productsSkuID;
								moveLocationItem.ProductsSkuSaleprop = productsSkuSaleprop;
								moveLocationItem.OutLocationID = outLocationID;
								moveLocationItem.InLocationID = inLocationID;
								moveLocationItem.ProductsBatchID = productsBatchID;
								moveLocationItem.ProductsBatchCode = productsBatchCode;
								moveLocationItem.Num = moveNum;
								moveLocationItem.Status = (int)MoveLocationStatus.未确认;
								moveLocationItem.CreatePerson = userCode;
								moveLocationItem.CreateDate = DateTime.Now;
								int outStockItemID = WarehouseMoveLocationItemService.Add(moveLocationItem, context);
								if (outStockItemID == 0) {
									resultInfo.result = 0;
									resultInfo.message = "商品SKU码 " + productsSkuCode + " 移出库位编码 " + outLocationCode + " 移入库位编码 " + inLocationCode + " 商品批次 " + productsBatchCode + " 添加失败！";
									break;
								}
								else {
									int count = WarehouseProductsBatchService.UpdateDjNumAndKyNum(userCode, productsBatchID, moveNum, context);
									if (count > 0) {
										count = WarehouseLocationProductsService.UpdateDjNumAndKyNum(userCode, productsSkuID, outLocationID, productsBatchID, moveNum, context);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "商品SKU码 " + productsSkuCode + " 移出库位编码 " + outLocationCode + " 移入库位编码 " + inLocationCode + " 商品批次 " + productsBatchCode + " 更新库位表冻结数量失败！";
											break;
										}
									}
									else {
										resultInfo.result = 0;
										resultInfo.message = "商品SKU码 " + productsSkuCode + " 移出库位编码 " + outLocationCode + " 移入库位编码 " + inLocationCode + " 商品批次 " + productsBatchCode + " 更新批次表冻结数量失败！";
										break;
									}
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "商品SKU码 " + productsSkuCode + " 移出库位编码 " + outLocationCode + " 移入库位编码 " + inLocationCode + " 商品批次 " + productsBatchCode + " 已经添加过，不能重复添加！";
								break;
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "商品SKU码 " + productsSkuCode + " 不存在或不属于此仓库！";
							break;
						}
					}
				}
				else {
					resultInfo.result = 0;
					resultInfo.message = "未确认状态才可以添加商品！";
				}
				if (submitTran) {
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
				Sys.SaveErrorLog(ex, "保存出库单商品", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除移位单商品

		/// <summary>
		/// 删除移位单商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="moveLocationID">移位单主键ID</param>
		/// <param name="moveLocationItemIDList">移位单商品表主键ID列表</param>
		/// <returns></returns>
		public static BaseResult DelMoveLocationItem(string userCode, int moveLocationID, List<int> moveLocationItemIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseMoveLocation moveLocation = WarehouseMoveLocationService.GetQuerySingleByID(moveLocationID, context);
					if (moveLocation.Status == (int)MoveLocationStatus.未确认) {
						//删除之前查出采购单商品列表
						List<WarehouseMoveLocationItem> moveLocationItemList = WarehouseMoveLocationItemService.GetWarehouseMoveLocationItemList(moveLocationItemIDList, context);
						if (moveLocationItemList.Count > 0) {
							int rowsAffected = WarehouseMoveLocationItemService.Delete(moveLocationItemIDList, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "删除失败，" + MoveLocationStatus.未确认.ToString() + "状态才可以删除！";
							}
							else {
								#region 还原可用，解除冻结

								int count = 0;
								foreach (var moveLocationItem in moveLocationItemList) {
									count = WarehouseProductsBatchService.UpdateDjNumAndKyNum(userCode, moveLocationItem.ProductsBatchID, -moveLocationItem.Num, context);
									if (count > 0) {
										count = WarehouseLocationProductsService.UpdateDjNumAndKyNum(userCode, moveLocationItem.ProductsSkuID, moveLocationItem.OutLocationID, moveLocationItem.ProductsBatchID, -moveLocationItem.Num, context);
										if (count == 0) {
											string locationCode = string.Empty;
											WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(moveLocationItem.OutLocationID, context);
											if (warehouseLocation != null) {
												locationCode = warehouseLocation.Code;
											}
											resultInfo.result = 0;
											resultInfo.message = "删除失败，商品SKU码 " + moveLocationItem.ProductsSkuCode + " 商品批次 " + moveLocationItem.ProductsBatchCode + " 库位编码 " + locationCode + " 解除冻结失败！";
											break;
										}
									}
									else {
										resultInfo.result = 0;
										resultInfo.message = "删除失败，商品SKU码 " + moveLocationItem.ProductsSkuCode + " 商品批次 " + moveLocationItem.ProductsBatchCode + " 解除冻结失败！";
										break;
									}
								}

								#endregion
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "删除失败，可能已经被删除！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "删除失败，" + MoveLocationStatus.未确认.ToString() + "状态才可以删除！";
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
				Sys.SaveErrorLog(ex, "删除移位单商品", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 修改移入库位编码和移位数量
		
		/// <summary>
		/// 修改移入库位编码和移位数量
		/// </summary>
		/// <param name="moveLocationItemID">移位单商品明细主键ID</param>
		/// <param name="inLocationCode">移入库位编码</param>
		/// <param name="moveNum">移位数量</param>
		public static BaseResult UpdateMoveLocationItem(string userCode, int moveLocationItemID, string inLocationCode, int moveNum) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseMoveLocationItem moveLocationItem = WarehouseMoveLocationItemService.GetQuerySingleByID(moveLocationItemID, context);
					if (moveLocationItem != null) {
						WarehouseLocation inWarehouseLocation = WarehouseLocationService.GetSingleWarehouseLocation(moveLocationItem.WarehouseCode, inLocationCode, context);
						if (inWarehouseLocation != null) {
							//差异数量
							int diffNum = moveNum - moveLocationItem.Num;
							int count = WarehouseMoveLocationItemService.UpdateMoveLocationItem(userCode, moveLocationItemID, inWarehouseLocation.ID, diffNum, context);
							if (count > 0) {
								int kfhNum = WarehouseLocationProductsService.GetKfhNum(moveLocationItem.OutLocationID, moveLocationItem.ProductsSkuID, moveLocationItem.ProductsBatchID, context);
								if (kfhNum - (diffNum) >= 0) {
									count = WarehouseProductsBatchService.UpdateDjNumAndKyNum(userCode, moveLocationItem.ProductsBatchID, diffNum, context);
									if (count > 0) {
										count = WarehouseLocationProductsService.UpdateDjNumAndKyNum(userCode, moveLocationItem.ProductsSkuID, moveLocationItem.OutLocationID, moveLocationItem.ProductsBatchID, diffNum, context);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "更新库位商品表失败！";
										}
									}
									else {
										resultInfo.result = 0;
										resultInfo.message = "更新商品批次表失败！";
									}
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "库存不足，当前可移位数量只有" + (kfhNum + moveLocationItem.Num) + "！";
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "更新移位单明细表失败，已确认或已删除！";
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "移入库位编码 " + inLocationCode + " 不存在！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "移位单明细已删除！";
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
				Sys.SaveErrorLog(ex, "修改移入库位编码和移位数量", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 确认移位单

		/// <summary>
		/// 确认移位单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">移位单主键ID</param>
		public static BaseResult Confirm(string userCode, int id) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					int oldStatus = (int)MoveLocationStatus.未确认;
					int newStatus = (int)MoveLocationStatus.已确认;
					int count = WarehouseMoveLocationService.UpdateStatus(userCode, id, oldStatus, newStatus, context);
					if (count > 0) {
						List<WarehouseMoveLocationItem> moveLocationItemList = WarehouseMoveLocationItemService.GetWarehouseMoveLocationItemList(id, context);
						moveLocationItemList = moveLocationItemList.Where(o => o.Status == (int)MoveLocationStatus.未确认).ToList();
						if (moveLocationItemList.Count > 0) {
							foreach (var item in moveLocationItemList) {
								#region 更新移位明细状态

								count = WarehouseMoveLocationItemService.UpdateStatus(userCode, item.ID, oldStatus, newStatus, context);
								if (count == 0) {
									string outLocationCode = string.Empty;
									string inLocationCode = string.Empty;
									WarehouseLocation outWarehouseLocation = WarehouseLocationService.GetQuerySingleByID(item.OutLocationID);
									if (outWarehouseLocation != null) {
										outLocationCode = outWarehouseLocation.Code;
									}
									WarehouseLocation inWarehouseLocation = WarehouseLocationService.GetQuerySingleByID(item.InLocationID);
									if (inWarehouseLocation != null) {
										inLocationCode = inWarehouseLocation.Code;
									}
									resultInfo.result = 0;
									resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 移出库位编码 " + outLocationCode + " 移入库位编码 " + inLocationCode + " 批次 " + item.ProductsBatchCode + " 更新状态失败，可能已被确认或删除！";
									break;
								}

								#endregion
								//扣减移出库位库存
								count = WarehouseLocationProductsService.UpdateDjNumAndZkNum(userCode, item.ProductsSkuID, item.OutLocationID, item.ProductsBatchID, item.Num, context);
								if (count > 0) {
									#region 移出库位增加日志
									WarehouseOutInStockLog outStockLog = new WarehouseOutInStockLog();
									outStockLog.SourceID = item.MoveLocationID;
									outStockLog.SourceNo = item.MoveLocationBillNo;
									outStockLog.SourceItemID = item.ID;
									outStockLog.WarehouseCode = item.WarehouseCode;
									outStockLog.BillType = (int)BillType.YW;
									outStockLog.StockWay = (int)StockWay.出库;
									outStockLog.ProductsID = item.ProductsID;
									outStockLog.ProductsCode = item.ProductsCode;
									outStockLog.ProductsName = item.ProductsName;
									outStockLog.ProductsNo = item.ProductsNo;
									outStockLog.ProductsSkuCode = item.ProductsSkuCode;
									outStockLog.ProductsSkuID = item.ProductsSkuID;
									outStockLog.ProductsSkuSaleprop = item.ProductsSkuSaleprop;
									outStockLog.ProductsBatchCode = item.ProductsBatchCode;
									outStockLog.ProductsBatchID = item.ProductsBatchID;									
									outStockLog.LocationID = item.OutLocationID;
									outStockLog.Num = item.Num;
									outStockLog.Remark = "移位出库";
									outStockLog.CreatePerson = userCode;
									outStockLog.CreateDate = DateTime.Now;
									int outStockLogID = WarehouseOutInStockLogService.Add(outStockLog, context);
									if (outStockLogID == 0) {
										string outLocationCode = string.Empty;
										WarehouseLocation outWarehouseLocation = WarehouseLocationService.GetQuerySingleByID(item.OutLocationID, context);
										if (outWarehouseLocation != null) {
											outLocationCode = outWarehouseLocation.Code;
										}
										resultInfo.result = 0;
										resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 批次 " + item.ProductsBatchCode + " 移出库位编码 " + outLocationCode + "  增加移位出库日志失败！";
										break;
									}
									#endregion
									#region 增加移入库位库存 存在则更新

									WarehouseLocationProducts warehouseLocationProducts = WarehouseLocationProductsService.GetSingleWarehouseLocationProducts(item.InLocationID, item.ProductsSkuID, item.ProductsBatchID, context);
									if (warehouseLocationProducts == null) {
										WarehouseLocation inWarehouseLocation = WarehouseLocationService.GetQuerySingleByID(item.InLocationID,context);
										warehouseLocationProducts = new WarehouseLocationProducts();
										warehouseLocationProducts.WarehouseCode = item.WarehouseCode;
										warehouseLocationProducts.LocationID = item.InLocationID;
										warehouseLocationProducts.LocationTypeID = inWarehouseLocation.TypeID;
										warehouseLocationProducts.TopLocationID = inWarehouseLocation.ParentID;
										warehouseLocationProducts.ProductsID = item.ProductsID;
										warehouseLocationProducts.ShelfLife = 0;
										warehouseLocationProducts.ProductionDate = DateTime.Now;
										warehouseLocationProducts.ProductsSkuID = item.ProductsSkuID;
										warehouseLocationProducts.ProductsBatchID = item.ProductsBatchID;
										warehouseLocationProducts.ProductsBatchCode = item.ProductsBatchCode;
										warehouseLocationProducts.KyNum = item.Num;
										warehouseLocationProducts.ZkNum = item.Num;
										warehouseLocationProducts.CreatePerson = userCode;
										warehouseLocationProducts.CreateDate = DateTime.Now;
										int warehouseLocationProductsID = WarehouseLocationProductsService.Add(warehouseLocationProducts, context);
										if (warehouseLocationProductsID == 0) {
											resultInfo.result = 0;
											resultInfo.message = "添加移入库位商品记录失败！";
											break;
										}
									}
									else {
										count = WarehouseLocationProductsService.UpdateKyNumAndZkNum(userCode, item.ProductsSkuID, item.InLocationID, item.ProductsBatchID, item.Num, context);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "更新移入库位商品库存失败！";
											break;
										}
									}

									#endregion
									#region 移入库位增加日志
									WarehouseOutInStockLog inStockLog = new WarehouseOutInStockLog();
									inStockLog.SourceID = item.MoveLocationID;
									inStockLog.SourceNo = item.MoveLocationBillNo;
									inStockLog.SourceItemID = item.ID;
									inStockLog.WarehouseCode = item.WarehouseCode;
									inStockLog.BillType = (int)BillType.YW;
									inStockLog.StockWay = (int)StockWay.入库;
									inStockLog.ProductsID = item.ProductsID;
									inStockLog.ProductsCode = item.ProductsCode;
									inStockLog.ProductsName = item.ProductsName;
									inStockLog.ProductsNo = item.ProductsNo;
									inStockLog.ProductsSkuCode = item.ProductsSkuCode;
									inStockLog.ProductsSkuID = item.ProductsSkuID;
									inStockLog.ProductsSkuSaleprop = item.ProductsSkuSaleprop;
									inStockLog.ProductsBatchCode = item.ProductsBatchCode;
									inStockLog.ProductsBatchID = item.ProductsBatchID;
									inStockLog.LocationID = item.InLocationID;
									inStockLog.Num = item.Num;
									inStockLog.Remark = "移位入库";
									inStockLog.CreatePerson = userCode;
									inStockLog.CreateDate = DateTime.Now;
									int inStockLogID = WarehouseOutInStockLogService.Add(inStockLog, context);
									if (inStockLogID == 0) {
										#region 读取移入库位编码

										string inLocationCode = string.Empty;
										WarehouseLocation inWarehouseLocation = WarehouseLocationService.GetQuerySingleByID(item.InLocationID, context);
										if (inWarehouseLocation != null) {
											inLocationCode = inWarehouseLocation.Code;
										}

										#endregion
										resultInfo.result = 0;
										resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 批次 " + item.ProductsBatchCode + " 移入库位编码 " + inLocationCode + "  增加移位入库日志失败！";
										break;
									}
									#endregion
								}
								else {
									#region 读取移出库位编码

									string outLocationCode = string.Empty;
									WarehouseLocation outWarehouseLocation = WarehouseLocationService.GetQuerySingleByID(item.OutLocationID, context);
									if (outWarehouseLocation != null) {
										outLocationCode = outWarehouseLocation.Code;
									}

									#endregion
									resultInfo.result = 0;
									resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 批次 " + item.ProductsBatchCode + " 移出库位编码 " + outLocationCode + " 扣减库位库存失败！";
									break;
								}
								//移位不会影响批次库存，所以只要解除批次冻结就好
								count = WarehouseProductsBatchService.UpdateDjNumAndKyNum(userCode, item.ProductsBatchID, -item.Num, context);
								if (count == 0) {
									#region 读取移出库位编码

									string outLocationCode = string.Empty;
									WarehouseLocation outWarehouseLocation = WarehouseLocationService.GetQuerySingleByID(item.OutLocationID, context);
									if (outWarehouseLocation != null) {
										outLocationCode = outWarehouseLocation.Code;
									}

									#endregion
									resultInfo.result = 0;
									resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 批次 " + item.ProductsBatchCode + " 移出库位编码 " + outLocationCode + " 更新批次库存失败！";
									break;
								}
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "移位单没有可确认的商品！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "更新移位单状态失败，可能已被确认或删除！";
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
				Sys.SaveErrorLog(ex, "确认移位单", userCode);
			}
			return resultInfo;
		}

		#endregion
	}
}
