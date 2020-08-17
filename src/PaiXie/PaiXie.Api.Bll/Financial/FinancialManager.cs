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
	public class FinancialManager {

		#region 入库单 出库单 审核
		public static BaseResult OutInStockshenhe(int id = 0) {
			BaseResult resultInfo = new BaseResult();
			WarehouseOutInStock WarehouseOutInStock = WarehouseOutInStockService.GetQuerySingleByID(id);
			#region //校验
			if (WarehouseOutInStock==null) {
				resultInfo.result = 0;
				resultInfo.message = "该单据不存在！";
				return resultInfo;
			}
			if (WarehouseOutInStock.Status == (int)WarehouseOutInStockStatus.未提交) {
				resultInfo.result = 0;
				resultInfo.message = "该单据未提交！";
				return resultInfo;
			}
			else if (WarehouseOutInStock.Status == (int)WarehouseOutInStockStatus.已审核) {
					resultInfo.result = 0;
					resultInfo.message = "该单据已审核！";
					return resultInfo;
				}
			#endregion
			try {
					using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseOutInStock.Status = (int)WarehouseOutInStockStatus.已审核;
					WarehouseOutInStock.IsAuditPrice = 1;
					WarehouseOutInStock.CountName = FormsAuth.GetUserCode();
					WarehouseOutInStock.UpdateDate = DateTime.Now;
					int result = WarehouseOutInStockService.Update(WarehouseOutInStock);
					if (result == 0) {
						resultInfo.result = 0;
						resultInfo.message = "该单据财务审核失败";
					}
					result = WarehouseOutInStockItemService.UpdatecwshenheStatus((int)WarehouseOutInStockStatus.已审核, (int)WarehouseOutInStockStatus.待审核, id, context);
						if (result == 0) {
						resultInfo.result = 0;
						resultInfo.message = "该单据明细财务审核失败";
					}

						//日志
						FinanceLog FinanceLog = new FinanceLog();
						FinanceLog.FinanceType = (int)FinanceType.出库入库单审核;
						FinanceLog.FinancePerson = FormsAuth.GetUserCode();
						FinanceLog.CreateDate = DateTime.Now;
						FinanceLog.SourceID = WarehouseOutInStock.ID;
						FinanceLog.SourceNO = WarehouseOutInStock.BillNo;
						result = FinanceLogService.Add(FinanceLog, context);
						if (result == 0) {
							resultInfo.result = 0;
							resultInfo.message = "审核日志添加失败";
						}

						//批次信息  更新 (入库单) OutInStockID
						List<WarehouseOutInStockItem> list= WarehouseOutInStockItemService.GetWarehouseOutInStockItemList(id,context);
						foreach (var item in list) {
							if (item.BillType == (int)BillType.CGR || item.BillType == (int)BillType.QTR) {
								WarehouseProductsBatch WarehouseProductsBatch=WarehouseProductsBatchService.GetSingleWarehouseProductsBatch(item.WarehouseCode, item.ProductsSkuID, item.ProductsBatchCode, context);
								if (WarehouseProductsBatch == null) {
									continue;
								}
								WarehouseProductsBatch.CostPrice = item.CostPrice;
						    	result=	WarehouseProductsBatchService.Update(WarehouseProductsBatch,context);
								if (result == 0) {
									resultInfo.result = 0;
									resultInfo.message = "更新批次信息失败";
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
				resultInfo.message = "单据财务审核失败！";
				Sys.SaveErrorLog(ex, "单据财务审核", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}
		#endregion

		#region 入库单审核 商品价格修改

		public static BaseResult UpdatePrice(string userCode, Storagelist obj) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					if (obj.CostPrice == 0) {
						resultInfo.result = 0;
						resultInfo.message = "商品价格不能为空";
					}
					WarehouseOutInStockItem objWarehouseOutInStockItem = WarehouseOutInStockItemService.GetQuerySingleByID(obj.ID, context);				
					objWarehouseOutInStockItem.CostPrice = obj.CostPrice;
					int count = WarehouseOutInStockItemService.Update(objWarehouseOutInStockItem, context);
				    if (count==0) {
						resultInfo.result = 0;
						resultInfo.message = "更新商品价格失败！";
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
				Sys.SaveErrorLog(ex, "入库单审核 商品价格修改", userCode);
			}
			return resultInfo;
		}
		#endregion

		#region 盘点 审核
		public static BaseResult PDshenhe(int id = 0) {
			BaseResult resultInfo = new BaseResult();
			WarehouseStocktaking WarehouseStocktaking = WarehouseStocktakingService.GetQuerySingleByID(id);
			#region //校验
			if (WarehouseStocktaking == null) {
				resultInfo.result = 0;
				resultInfo.message = "该单据不存在！";
				return resultInfo;
			}
			if (WarehouseStocktaking.Status == (int)StocktakingStatus.未确认) {
				resultInfo.result = 0;
				resultInfo.message = "该单据未确认！";
				return resultInfo;
			}
			else if (WarehouseStocktaking.Status == (int)StocktakingStatus.已审核) {
				resultInfo.result = 0;
				resultInfo.message = "该单据已审核！";
				return resultInfo;
			}
			#endregion
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseStocktaking.Status = (int)StocktakingStatus.已审核;
					int result = WarehouseStocktakingService.Update(WarehouseStocktaking);
					if (result == 0) {
						resultInfo.result = 0;
						resultInfo.message = "该单据财务审核失败";
					}
					//日志
					FinanceLog FinanceLog = new FinanceLog();
					FinanceLog.FinanceType = (int)FinanceType.盘点审核;
					FinanceLog.FinancePerson = FormsAuth.GetUserCode();
					FinanceLog.CreateDate = DateTime.Now;
					FinanceLog.SourceID = WarehouseStocktaking.ID;
					FinanceLog.SourceNO = WarehouseStocktaking.BillNo;
					result=FinanceLogService.Add(FinanceLog, context);
					if (result == 0) {
						resultInfo.result = 0;
						resultInfo.message = "审核日志添加失败";
					}
					//批次信息  更新  StocktakingID
					List<WarehouseStocktakingItem> list = WarehouseStocktakingItemService.GetQuerySingleByStocktakingID(id, context);
					foreach (var item in list) {					
							WarehouseProductsBatch WarehouseProductsBatch = WarehouseProductsBatchService.GetSingleWarehouseProductsBatch(item.WarehouseCode, item.ProductsSkuID, item.ProductsBatchCode, context);
							if (WarehouseProductsBatch == null) {
								continue;
							}
						    WarehouseProductsBatch.CostPrice = item.CostPrice;
							result = WarehouseProductsBatchService.Update(WarehouseProductsBatch, context);
							if (result == 0) {
								resultInfo.result = 0;
								resultInfo.message = "更新批次信息失败";
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
				resultInfo.result = 0;
				resultInfo.message = "单据财务审核失败！";
				Sys.SaveErrorLog(ex, "盘点单据财务审核", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}
		#endregion

		#region 盘点 审核 商品价格修改

		public static BaseResult UpdatePdPrice(string userCode, WarehouseStocktakingItemList obj) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					if (obj.CostPrice == 0) {
						resultInfo.result = 0;
						resultInfo.message = "商品价格不能为空";
					}


					WarehouseStocktakingItem WarehouseStocktakingItem = WarehouseStocktakingItemService.GetQuerySingleByID(obj.ID, context);
					WarehouseStocktakingItem.CostPrice = obj.CostPrice;
					int count = WarehouseStocktakingItemService.Update(WarehouseStocktakingItem, context);
					if (count == 0) {
						resultInfo.result = 0;
						resultInfo.message = "更新商品价格失败！";
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
				Sys.SaveErrorLog(ex, "盘点 审核 商品价格修改", userCode);
			}
			return resultInfo;
		}
		#endregion

		#region 供应商结算检查

		public static BaseResult SuppliersCheck(string ids) {
			BaseResult resultInfo = new BaseResult();
			try {
				 List<WarehouseOutInStock>  list=
				WarehouseOutInStockService.Getlistbyids(ids);
			   	int sid=-1;
				 foreach (var item in list) {
					 if (sid!=-1) {
						 if (sid != item.SuppliersID) {
							 resultInfo.result = -1;
							 resultInfo.message = "请先选择供应商！";
							 break;
						 }						
					 }
					 sid = item.SuppliersID;
				 }
				 if (resultInfo.result==1) { 
				 foreach (var item in list) {
					 if (item.Status != (int)WarehouseOutInStockStatus.已审核) {
						 resultInfo.result = -1;
						 resultInfo.message = "未核对的记录不能进行结算！";
						 break;
					 }					
				 }
				 }


				 if (resultInfo.result == 1) {
					 foreach (var item in list) {
						 if (item.Settlement == 1) {
							 resultInfo.result = -1;
							 resultInfo.message = "已结算的记录不能重复结算！";
							 break;
						 }
					 }
				 }


			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "供应商结算检查", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}
		#endregion

       #region  添加  供应商 结算
		public static BaseResult SuppliersSettlement(SuppliersSettlementList obj) {
			BaseResult resultInfo = new BaseResult();
			//验证 
			resultInfo = SuppliersCheck(obj.ids);
			if (resultInfo.result == -1) {
				return resultInfo;
			}
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					List<WarehouseOutInStock> list =
							WarehouseOutInStockService.Getlistbyids(obj.ids, context);
					foreach(var item in list)
					{
						WarehouseOutInStock WarehouseOutInStock = WarehouseOutInStockService.GetQuerySingleByID(item.ID,context);
						WarehouseOutInStock.Settlement = 1;
						int result = WarehouseOutInStockService.Update(WarehouseOutInStock);
						if (result == 0) {
							resultInfo.result = -1;
							resultInfo.message = "结算失败";
							break;
						}
						SuppliersSettlement SuppliersSettlement = new SuppliersSettlement();
						SuppliersSettlement.SourceID = item.ID;
						SuppliersSettlement.SourceNO = item.BillNo;
						SuppliersSettlement.SettlementAmount = obj.SettlementAmount;
						SuppliersSettlement.TradingNumber = obj.TradingNumber;
						SuppliersSettlement.Remark = obj.Remark;
						SuppliersSettlement.SettlementTime = DateTime.Now;
						SuppliersSettlement.SettlementPerson = FormsAuth.GetUserCode();
						SuppliersSettlement.SuppliersID = item.SuppliersID;
					   result = SuppliersSettlementService.Add(SuppliersSettlement, context);
						if (result == 0) {
							resultInfo.result = -1;
							resultInfo.message = "结算失败";
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
				resultInfo.result = 0;
				resultInfo.message = "程序异常！";
				Sys.SaveErrorLog(ex, "添加供应商结算", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}
		#endregion
		
	}
}
