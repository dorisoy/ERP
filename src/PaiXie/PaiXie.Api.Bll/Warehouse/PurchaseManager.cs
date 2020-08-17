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

	/// <summary>
	/// 采购管理类
	/// </summary>
	public class PurchaseManager {

		#region 添加采购计划单

		/// <summary>
		/// 添加采购计划单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="name">计划单名称</param>
		/// <param name="projectType">1:管理端 2:仓库端 使用枚举</param>
		/// <returns></returns>
		public static BaseResult AddPlan(string userCode, string warehouseCode, string name, int projectType) {
			BaseResult resultInfo = new BaseResult();
			try {
				WarehousePurchasePlan warehousePurchasePlan = new WarehousePurchasePlan();
				warehousePurchasePlan.BillNo = Sys.GetBillNo(BillType.JH.ToString());
				warehousePurchasePlan.Name = name;
				warehousePurchasePlan.WarehouseCode = warehouseCode;
				warehousePurchasePlan.CreatePerson = userCode;
				warehousePurchasePlan.CreateDate = DateTime.Now;
				if (projectType == (int)ProjectType.管理端) {
					warehousePurchasePlan.Status = (int)PurchasePlanStatus.已提交;
				}
				int id = WarehousePurchasePlanService.Add(warehousePurchasePlan);
				if (id == 0) {
					resultInfo.result = 0;
					resultInfo.message = "添加采购计划单失败！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "添加采购计划单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除采购计划单

		/// <summary>
		/// 删除采购计划单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="projectType">1:管理端 2:仓库端 使用枚举</param>
		/// <param name="planIDList">采购计划单ID列表</param>
		/// <returns></returns>
		public static BaseResult DelPlan(string userCode, int projectType, List<int> planIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (int planID in planIDList) {
						int rowsAffected = WarehousePurchasePlanService.Delete(projectType, planID, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							if (projectType == (int)ProjectType.管理端) {
								resultInfo.message = "删除失败，只能删除未采购的计划单！";
							}
							else {
								resultInfo.message = "删除失败，只能删除未提交的计划单！";
							}
							break;
						}
						else {
							WarehousePurchasePlanItemService.DeleteByPlanID(planID, context);
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
				Sys.SaveErrorLog(ex, "删除采购计划单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 结束采购计划单

		/// <summary>
		/// 结束采购计划单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="planIDList">采购计划单ID列表</param>
		/// <returns></returns>
		public static BaseResult EndPlan(string userCode, List<int> planIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (int planID in planIDList) {
						int rowsAffected = WarehousePurchasePlanService.End(userCode, planID, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "结束失败，只能结束已采购的计划单！";
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
				Sys.SaveErrorLog(ex, "结束采购计划单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 添加计划采购商品

		/// <summary>
		/// 添加计划采购商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="objWebInfo">采购计划单商品实体类</param>
		/// <returns></returns>
		public static BaseResult AddPlanItem(string userCode, string warehouseCode, WarehousePurchasePlanItemWebInfo objWebInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					for (int i = 0; i < objWebInfo.ProductsSkuID.Length; i++) {
						int num = objWebInfo.Num[i];
						if (num == 0) {
							continue;
						}
						int productsSkuID = objWebInfo.ProductsSkuID[i];
						string productsSkuCode = objWebInfo.ProductsSkuCode[i];
						string productsSkuSaleprop = objWebInfo.ProductsSkuSaleprop[i];
						int suppliersID = 0;
						if (objWebInfo.SuppliersID != null) suppliersID = objWebInfo.SuppliersID[i];
						WarehouseProductsSkuInfo productsSkuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(warehouseCode, productsSkuID, context);
						if (productsSkuInfo != null) {
							WarehousePurchasePlanItem planItem = WarehousePurchasePlanItemService.GetSingleWarehousePurchasePlanItem(objWebInfo.PlanID, productsSkuID, context);
							if (planItem == null) {
								planItem = new WarehousePurchasePlanItem();
								planItem.WarehouseCode = warehouseCode;
								planItem.BillNo = objWebInfo.BillNo;
								planItem.PlanID = objWebInfo.PlanID;
								planItem.ProductsID = objWebInfo.ProductsID;
								planItem.ProductsName = objWebInfo.ProductsName;
								planItem.ProductsNo = objWebInfo.ProductsNo;
								planItem.ProductsCode = objWebInfo.ProductsCode;
								planItem.ProductsSkuID = productsSkuID;
								planItem.ProductsSkuCode = productsSkuCode;
								planItem.ProductsSkuSaleprop = productsSkuSaleprop;
								planItem.SuppliersID = suppliersID;
								planItem.Num = num;
								planItem.CreatePerson = userCode;
								planItem.CreateDate = DateTime.Now;
								int planItemId = WarehousePurchasePlanItemService.Add(planItem, context);
								if (planItemId == 0) {
									resultInfo.result = 0;
									resultInfo.message = "商品SKU码 " + productsSkuCode + " 添加失败！";
									break;
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "商品SKU码 " + productsSkuCode + " 已经添加过，不能重复添加！";
								break;
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "商品SKU码 " + productsSkuCode + " 不存在！";
							break;
						}
					}
					if (resultInfo.result == 1) {
						int count = WarehousePurchasePlanService.UpdateNum(userCode, objWebInfo.PlanID, context);
						if (count == 0) {
							resultInfo.result = 0;
							resultInfo.message = "更新采购计划单 " + objWebInfo.BillNo + " 的计划数量失败！";
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
				Sys.SaveErrorLog(ex, "添加计划采购商品", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除计划采购商品

		/// <summary>
		/// 删除采购计划单商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="projectType">1:管理端 2:仓库端 使用枚举</param>
		/// <param name="planID">采购计划单主键ID</param>
		/// <param name="planItemIDList">采购计划单商品表主键ID列表</param>
		/// <returns></returns>
		public static BaseResult DelPlanItem(string userCode, int projectType, int planID, List<int> planItemIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					int rowsAffected = WarehousePurchasePlanItemService.Delete(projectType, planItemIDList, context);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "删除采购计划单商品失败！";
					}
					else {
						int count = WarehousePurchasePlanService.UpdateNum(userCode, planID, context);
						if (count == 0) {
							resultInfo.result = 0;
							resultInfo.message = "更新采购计划单的计划数量失败！";
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
				Sys.SaveErrorLog(ex, "删除采购计划单商品", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 按照计划采购商品生成采购单

		/// <summary>
		/// 按照采购计划单商品生成采购单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="planItemIDList">采购计划单商品表主键ID列表</param>
		/// <param name="purchaseOrderCount">返回成功生成的采购单条数</param>
		/// <returns></returns>
		public static BaseResult Generation(string userCode, List<int> planItemIDList, out int purchaseOrderCount) {
			purchaseOrderCount = 0;
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					//获取计划数量大于已采购数量的明细列表，相同供应商排序在一起
					List<WarehousePurchasePlanItem> PlanItemList = WarehousePurchasePlanItemService.GetWarehousePurchasePlanItemList(planItemIDList);
					if (PlanItemList.Count > 0) {
						int index = 0;
						string billNo = string.Empty;
						int purchaseID = 0;
						int count = 0;
						foreach (var item in PlanItemList) {
							if (item.SuppliersID > 0) {
								if (index == 0) {
									Suppliers suppliers = SuppliersService.GetQuerySingleByID(item.SuppliersID, context);
									billNo = Sys.GetBillNo(BillType.CG.ToString());
									#region 创建采购单

									WarehousePurchase entity = new WarehousePurchase();
									entity.BillNo = billNo;
									entity.PlanBillNo = item.BillNo;
									entity.PlanID = item.PlanID;
									entity.SuppliersID = item.SuppliersID;
									entity.WarehouseCode = item.WarehouseCode;
									entity.Num = item.Num;
									entity.CreatePerson = userCode;
									entity.CreateDate = DateTime.Now;
									purchaseID = WarehousePurchaseService.Add(entity, context);
									if (purchaseID == 0) {
										resultInfo.result = 0;
										resultInfo.message = "供应商 " + suppliers.AliasName + " 添加采购单失败！";
										break;
									}
									else {
										purchaseOrderCount++;
									}

									#endregion
								}
								else if (item.SuppliersID != PlanItemList[index - 1].SuppliersID) {
									Suppliers suppliers = SuppliersService.GetQuerySingleByID(item.SuppliersID, context);
									billNo = Sys.GetBillNo(BillType.CG.ToString());
									#region 创建采购单

									WarehousePurchase entity = new WarehousePurchase();
									entity.BillNo = billNo;
									entity.PlanBillNo = item.BillNo;
									entity.PlanID = item.PlanID;
									entity.SuppliersID = item.SuppliersID;
									entity.WarehouseCode = item.WarehouseCode;
									entity.Num = item.Num;
									entity.CreatePerson = userCode;
									entity.CreateDate = DateTime.Now;
									purchaseID = WarehousePurchaseService.Add(entity, context);
									if (purchaseID == 0) {
										resultInfo.result = 0;
										resultInfo.message = "供应商 " + suppliers.AliasName + " 添加采购单失败！";
										break;
									}
									else {
										purchaseOrderCount++;
									}

									#endregion
								}

								#region 添加采购单商品

								WarehousePurchaseItem entitiy = new WarehousePurchaseItem();
								entitiy.BillNo = billNo;
								entitiy.PurchaseID = purchaseID;
								entitiy.PlanBillNo = item.BillNo;
								entitiy.PlanID = item.PlanID;
								entitiy.WarehouseCode = item.WarehouseCode;
								entitiy.PlanItemID = item.ID;
								entitiy.ProductsCode = item.ProductsCode;
								entitiy.ProductsID = item.ProductsID;
								entitiy.ProductsName = item.ProductsName;
								entitiy.ProductsNo = item.ProductsNo;
								entitiy.ProductsSkuCode = item.ProductsSkuCode;
								entitiy.ProductsSkuID = item.ProductsSkuID;
								entitiy.ProductsSkuSaleprop = item.ProductsSkuSaleprop;
								entitiy.Num = item.Num;
								entitiy.CreatePerson = userCode;
								entitiy.CreateDate = DateTime.Now;
								int purchaseItemID = WarehousePurchaseItemService.Add(entitiy, context);
								if (purchaseItemID == 0) {
									resultInfo.result = 0;
									resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 添加失败！";
									break;
								}
								#endregion

								//更新采购计划单商品已采购数量
								count = WarehousePurchasePlanItemService.UpdatePurchasedNum(userCode, item.ID, item.Num, context);
								if (count == 0) {
									resultInfo.result = 0;
									resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 更新已采购数量失败！";
									break;
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 未选择供应商，无法生成采购单！";
								break;
							}
							index++;
						}
						count=WarehousePurchaseService.UpdateNum(userCode, purchaseID, context);
						if (count > 0) {
							count = WarehousePurchasePlanService.UpdatePurchasedNum(userCode, PlanItemList[0].PlanID, context);
							if (count > 0) {
								count = WarehousePurchasePlanService.UpdatePurchaseOrderCount(userCode, PlanItemList[0].PlanID, context);
								if (count > 0) {
									count = WarehousePurchasePlanItemService.GetNotFinCount(PlanItemList[0].PlanID, context);
									if (count == 0) {
										//如果所有商品都已采购完成，自动结束采购计划单
										count = WarehousePurchasePlanService.End(userCode, PlanItemList[0].PlanID, context);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "所有商品都已采购完成，结束采购计划单失败！";
										}
									}
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "更新采购计划单的采购单条数失败！";
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "更新采购计划单的已采购数量失败！";
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "更新采购单的采购数量失败！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "没有可以生成采购单的商品！";
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						purchaseOrderCount = 0;
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "按照采购计划单商品生成采购单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 设置计划采购商品供应商

		/// <summary>
		/// 设置计划采购商品供应商
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="planItemID">计划采购商品表主键ID</param>
		/// <param name="suppliersID">供应商ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="isDefault">是否设为默认 0:否 1:是</param>
		/// <returns></returns>
		public static BaseResult UpdateSuppliersID(string userCode, int planItemID, int suppliersID, int productsSkuID, int isDefault) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					int count = WarehousePurchasePlanItemService.UpdateSuppliersID(userCode, planItemID, suppliersID, context);
					if (count > 0) {
						if (isDefault > 0) {
							//设置当前供应商为SKU默认供应商，并清除之前的默认供应商
							count = SuppliersItemService.UpdateIsDefault(productsSkuID, suppliersID, context);
							if (count == 0) {
								resultInfo.result = 0;
								resultInfo.message = "设置默认供应商失败！";
							}
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "商品已采购，设置供应商失败！";
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
				Sys.SaveErrorLog(ex, "设置计划采购商品供应商", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 添加采购单

		/// <summary>
		/// 添加采购单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="suppliersID">供应商ID</param>
		/// <param name="planID">采购计划单主键ID</param>
		/// <param name="planBillNo">采购计划单号</param>
		/// <returns></returns>
		public static BaseResult AddPurchase(string userCode, string warehouseCode, int suppliersID) {
			BaseResult resultInfo = new BaseResult();
			try {
				WarehousePurchase entity = new WarehousePurchase();
				entity.BillNo = Sys.GetBillNo(BillType.CG.ToString());
				entity.SuppliersID = suppliersID;
				entity.WarehouseCode = warehouseCode;
				entity.CreatePerson = userCode;
				entity.CreateDate = DateTime.Now;
				int purchaseID = WarehousePurchaseService.Add(entity);
				if (purchaseID == 0) {
					resultInfo.result = 0;
					resultInfo.message = "添加采购单失败！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "添加采购单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 添加采购单商品

		/// <summary>
		/// 添加采购单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="objWebInfo">采购单商品实体类</param>
		/// <returns></returns>
		public static BaseResult AddPurchaseItem(string userCode, string warehouseCode, WarehousePurchaseItemWebInfo objWebInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					for (int i = 0; i < objWebInfo.ProductsSkuID.Length; i++) {
						int num = objWebInfo.Num[i];
						if (num == 0) {
							continue;
						}
						int productsSkuID = objWebInfo.ProductsSkuID[i];
						string productsSkuCode = objWebInfo.ProductsSkuCode[i];
						string productsSkuSaleprop = objWebInfo.ProductsSkuSaleprop[i];
						WarehouseProductsSkuInfo productsSkuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(warehouseCode, productsSkuID, context);
						if (productsSkuInfo != null) {
							WarehousePurchaseItem purchaseItem = WarehousePurchaseItemService.GetSingleWarehousePurchaseItem(objWebInfo.PurchaseID, productsSkuID, context);
							if (purchaseItem == null) {
								purchaseItem = new WarehousePurchaseItem();
								purchaseItem.BillNo = objWebInfo.BillNo;
								purchaseItem.PurchaseID = objWebInfo.PurchaseID;
								purchaseItem.WarehouseCode = warehouseCode;
								purchaseItem.ProductsCode = objWebInfo.ProductsCode;
								purchaseItem.ProductsID = objWebInfo.ProductsID;
								purchaseItem.ProductsName = objWebInfo.ProductsName;
								purchaseItem.ProductsNo = objWebInfo.ProductsNo;
								purchaseItem.ProductsSkuCode = productsSkuCode;
								purchaseItem.ProductsSkuID = productsSkuID;
								purchaseItem.ProductsSkuSaleprop = productsSkuSaleprop;
								purchaseItem.Num = num;
								purchaseItem.CreatePerson = userCode;
								purchaseItem.CreateDate = DateTime.Now;
								int purchaseItemID = WarehousePurchaseItemService.Add(purchaseItem, context);
								if (purchaseItemID == 0) {
									resultInfo.result = 0;
									resultInfo.message = "商品SKU码 " + productsSkuCode + " 添加失败！";
									break;
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "商品SKU码 " + productsSkuCode + " 已经添加过，不能重复添加！";
								break;
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "商品SKU码 " + productsSkuCode + " 不存在！";
							break;
						}
					}
					if (resultInfo.result == 1) {
						int count = WarehousePurchaseService.UpdateNum(userCode, objWebInfo.PurchaseID, context);
						if (count == 0) {
							resultInfo.result = 0;
							resultInfo.message = "更新采购单 " + objWebInfo.BillNo + " 的采购数量失败！";
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
				Sys.SaveErrorLog(ex, "添加采购单商品", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除采购单

		/// <summary>
		/// 删除采购单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseIDList">采购单ID列表</param>
		/// <returns></returns>
		public static BaseResult DelPurchase(string userCode, List<int> purchaseIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (int purchaseID in purchaseIDList) {
						WarehousePurchase warehousePurchase = WarehousePurchaseService.GetQuerySingleByID(purchaseID, context);
						int rowsAffected = WarehousePurchaseService.DelByID(purchaseID, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "删除失败，只能删除没有创建入库单的采购单！";
							break;
						}
						else {
							//删除之前获取采购单所有商品
							List<WarehousePurchaseItem> purchaseItemList = WarehousePurchaseItemService.GetWarehousePurchaseItemList(purchaseID, context);
							if (purchaseItemList.Count > 0) {
								int count = WarehousePurchaseItemService.DeleteByPurchaseID(purchaseID, context);
								if (count > 0) {
									bool needUpdatePlan = false;
									// 遍历更新计划单商品已采购数量
									foreach (var purchaseItem in purchaseItemList) {
										if (purchaseItem.PlanItemID > 0) {
											count = WarehousePurchasePlanItemService.UpdatePurchasedNum(userCode, purchaseItem.PlanItemID, -purchaseItem.Num, context);
											if (count == 0) {
												resultInfo.result = 0;
												resultInfo.message = "删除失败，更新计划单商品SKU码 " + purchaseItem.ProductsSkuCode + " 的已采购数量失败！";
												needUpdatePlan = false;
												break;
											}
											else {
												needUpdatePlan = true;
											}
										}
									}
									if (resultInfo.result == 1) {
										if (needUpdatePlan) {
											//更新计划单已采购数量
											WarehousePurchasePlanService.UpdatePurchasedNum(userCode, warehousePurchase.PlanID, context);
											//更新计划单的采购单条数
											WarehousePurchasePlanService.UpdatePurchaseOrderCount(userCode, warehousePurchase.PlanID, context);
											//还原计划单状态
											WarehousePurchasePlanService.Restore(userCode, warehousePurchase.PlanID, context);

										}
									}
									else {
										//遍历更新计划单商品已采购数量失败
										break;
									}
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "删除采购单商品失败！";
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
				Sys.SaveErrorLog(ex, "删除采购单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 确认采购单

		/// <summary>
		/// 确认采购单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseIDList">采购单ID列表</param>
		/// <returns></returns>
		public static BaseResult ConfirmPurchase(string userCode, List<int> purchaseIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (int purchaseID in purchaseIDList) {
						int rowsAffected = WarehousePurchaseService.Confirm(purchaseID, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "确认失败，只能确认未确认且采购数量大于0的采购单！";
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
				Sys.SaveErrorLog(ex, "确认采购单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region  结束采购单

		/// <summary>
		/// 结束采购单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseIDList">采购单ID列表</param>
		/// <returns></returns>
		public static BaseResult EndPurchase(string userCode, List<int> purchaseIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (int purchaseID in purchaseIDList) {
						int rowsAffected = WarehousePurchaseService.End(purchaseID, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "结束失败，只能结束已确认并且有创建入库单的采购单！";
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
				Sys.SaveErrorLog(ex, "结束采购单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 重新采购

		/// <summary>
		/// 重新采购
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseID">采购单ID</param>
		/// <returns></returns>
		public static BaseResult RePurchase(string userCode, int purchaseID) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					#region 根据采购单重新生成一笔新的采购单
					string newBillNo = Sys.GetBillNo(BillType.CG.ToString());
					WarehousePurchase warehousePurchase = WarehousePurchaseService.GetQuerySingleByID(purchaseID, context);
					WarehousePurchase newWarehousePurchase = new WarehousePurchase();
					newWarehousePurchase.BillNo = newBillNo;
					newWarehousePurchase.WarehouseCode = warehousePurchase.WarehouseCode;
					newWarehousePurchase.SuppliersID = warehousePurchase.SuppliersID;
					newWarehousePurchase.Num = warehousePurchase.Num;
					newWarehousePurchase.CreatePerson = userCode;
					newWarehousePurchase.CreateDate = DateTime.Now;
					int newPurchaseID = WarehousePurchaseService.Add(newWarehousePurchase, context);
					#endregion
					if (newPurchaseID > 0) {
						List<WarehousePurchaseItem> warehousePurchaseItemList = WarehousePurchaseItemService.GetWarehousePurchaseItemList(purchaseID, context);
						#region 遍历采购单明细，插入新的采购单明细

						foreach (var item in warehousePurchaseItemList) {
							WarehousePurchaseItem newWarehousePurchaseItem = new WarehousePurchaseItem();
							newWarehousePurchaseItem.BillNo = newBillNo;
							newWarehousePurchaseItem.PurchaseID = newPurchaseID;
							newWarehousePurchaseItem.WarehouseCode = item.WarehouseCode;
							newWarehousePurchaseItem.ProductsCode = item.ProductsCode;
							newWarehousePurchaseItem.ProductsID = item.ProductsID;
							newWarehousePurchaseItem.ProductsName = item.ProductsName;
							newWarehousePurchaseItem.ProductsNo = item.ProductsNo;
							newWarehousePurchaseItem.ProductsSkuCode = item.ProductsSkuCode;
							newWarehousePurchaseItem.ProductsSkuID = item.ProductsSkuID;
							newWarehousePurchaseItem.ProductsSkuSaleprop = item.ProductsSkuSaleprop;
							newWarehousePurchaseItem.Num = item.Num;
							newWarehousePurchaseItem.CreatePerson = userCode;
							newWarehousePurchaseItem.CreateDate = DateTime.Now;
							int newPurchaseItemID = WarehousePurchaseItemService.Add(newWarehousePurchaseItem, context);
							if (newPurchaseItemID == 0) {
								resultInfo.result = 0;
								resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 插入采购单明细失败！";
								break;
							}
						}

						#endregion
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "重新生成采购单失败！";
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
				Sys.SaveErrorLog(ex, "重新采购", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除采购单商品

		/// <summary>
		/// 删除采购单商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseID">采购单ID</param>
		/// <param name="purchaseItemIDList">采购单商品表主键ID列表</param>
		/// <returns></returns>
		public static BaseResult DelPurchaseItem(string userCode, List<int> purchaseItemIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					//删除之前查出采购单商品列表
					List<WarehousePurchaseItem> purchaseItemList = WarehousePurchaseItemService.GetWarehousePurchaseItemList(purchaseItemIDList, context);
					if (purchaseItemList.Count > 0) {
						int rowsAffected = WarehousePurchaseItemService.Delete(purchaseItemIDList, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "删除失败，未入库的采购单才可以删除商品！";
						}
						else {
							int count = WarehousePurchaseService.UpdateNum(userCode, purchaseItemList[0].PurchaseID, context);
							if (count == 0) {
								resultInfo.result = 0;
								resultInfo.message = "更新采购单的采购数量失败！";
							}
							else {
								bool needUpdatePlan = false;
								//遍历采购单商品列表，更新计划单商品的已采购数量
								foreach (var purchaseItem in purchaseItemList) {
									if (purchaseItem.PlanItemID > 0) {
										count = WarehousePurchasePlanItemService.UpdatePurchasedNum(userCode, purchaseItem.PlanItemID, -purchaseItem.Num, context);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "删除失败，更新计划单商品SKU码 " + purchaseItem.ProductsSkuCode + " 的已采购数量失败！";
											needUpdatePlan = false;
											break;
										}
										else {
											needUpdatePlan = true;
										}
									}
								}
								if (resultInfo.result == 1) {
									if (needUpdatePlan) {
										//更新计划单已采购数量
										WarehousePurchasePlanService.UpdatePurchasedNum(userCode, purchaseItemList[0].PlanID, context);
										//还原计划单状态
										WarehousePurchasePlanService.Restore(userCode, purchaseItemList[0].PlanID, context);
									}
								}
							}
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "删除失败，可能已经被删除！";
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
				Sys.SaveErrorLog(ex, "删除采购单商品", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 获取采购单预计金额

		/// <summary>
		/// 获取采购单预计金额
		/// </summary>
		/// <param name="purchaseID">采购单ID</param>
		/// <param name="suppliersID">供应商ID</param>
		/// <returns></returns>
		public static decimal GetExpectedAmount(int purchaseID,int suppliersID) {
			decimal expectedAmount = 0;
			List<WarehousePurchaseItem> purchaseItemList = WarehousePurchaseItemService.GetWarehousePurchaseItemList(purchaseID);
			foreach (var item in purchaseItemList) {
				decimal price = SuppliersManager.GetPurchasePrice(item.ProductsSkuID, suppliersID);
				expectedAmount += item.Num * price;
			}
			return expectedAmount;
		}

		#endregion

		#region 修改采购单商品的采购数量

		/// <summary>
		/// 修改采购单商品的采购数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseID">采购单表主键ID</param>
		/// <param name="purchaseItemID">采购单商品表主键ID</param>
		/// <param name="newNum">要更新数量</param>
		/// <returns></returns>
		public static BaseResult UpdateNum(string userCode, int purchaseID, int purchaseItemID, int newNum) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					int rowsAffected = WarehousePurchaseItemService.UpdateNum(userCode, purchaseItemID, newNum, context);
					if (rowsAffected > 0) {
						rowsAffected = WarehousePurchaseService.UpdateNum(userCode, purchaseID, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "更新采购单的采购数量失败！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "更新采购商品的采购数量失败！";
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
				Sys.SaveErrorLog(ex, "修改采购单商品的采购数量", userCode);
			}
			return resultInfo;
		}

		#endregion
	}
}
