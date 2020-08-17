#region using
using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Utils;
using System.Data;
#endregion
namespace PaiXie.Api.Bll {
	/// <summary>
	/// 出入库单管理
	/// </summary>
	public class OutInStockManager {

		#region sku 搜索
		/// <summary>
		/// sku 搜索
		/// </summary>
		/// <param name="skucode"></param>
		/// <returns></returns>
		public static SkuSearchList SkuSearch(string skucode, string BillNo) {
			SkuSearchList resultInfo = new SkuSearchList();
			try {
				ProductsSku objProductsSku = ProductsSkuService.GetProductsSku(FormsAuth.GetWarehouseCode(),skucode);
					if (objProductsSku != null) {
				resultInfo.Name = ProductsService.GetSingleProducts(objProductsSku.ProductsID).Name;
				resultInfo.Attribute = objProductsSku.Saleprop;
				#region //价格 
				decimal price = 0;
				string temp = "";
				WarehouseOutInStock WarehouseOutInStock = WarehouseOutInStockService.GetModelByBillNo(BillNo);
					if (WarehouseOutInStock == null) {
					//供应商 价格最低的
						temp = WarehouseOutInStockService.PurchasePrice(skucode);
							if (string.IsNullOrEmpty(temp) || ZConvert.StrToDecimal(temp,0)==0) {
						if (string.IsNullOrEmpty(objProductsSku.CostPrice.ToString()) || objProductsSku.CostPrice == 0) {
							//直接取商品的
							temp = WarehouseOutInStockService.CostPrice(objProductsSku.ProductsID);
							
							price = ZConvert.StrToDecimal(temp, 0);
						}
						else {
							price = objProductsSku.CostPrice;
						}
					}
					else {
						price = ZConvert.StrToDecimal(temp,0);
					}
				}
				else {
					//入库单 关联的供应商
					temp = WarehouseOutInStockService.PurchasePrice(skucode, WarehouseOutInStock.SuppliersID);
					
					if (string.IsNullOrEmpty(temp) || ZConvert.StrToDecimal(temp, 0) == 0) {
						//供应商 价格最低的
						temp = WarehouseOutInStockService.PurchasePrice(skucode);
						
						 if (string.IsNullOrEmpty(temp) || ZConvert.StrToDecimal(temp, 0) == 0) {
							 if (string.IsNullOrEmpty(objProductsSku.CostPrice.ToString()) || objProductsSku.CostPrice == 0) {

								 //直接取商品的
								 temp = WarehouseOutInStockService.CostPrice(objProductsSku.ProductsID);
								  price = ZConvert.StrToDecimal(temp, 0);
							 }
							 else {
								 price = objProductsSku.CostPrice;
							 }
						 
						 }
						 else {
							 price = ZConvert.StrToDecimal(temp, 0);
						 }
					}
					else {
						price = ZConvert.StrToDecimal(temp, 0);
					}
				}
				#endregion
				resultInfo.PurchasePrice = price;
				resultInfo.result = 1;
			}
			else {
				resultInfo.result = 0;
				resultInfo.message = "该商品SKU码不存在";
			}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "SKU搜索失败！";
				Sys.SaveErrorLog(ex, "sku 搜索", FormsAuth.GetUserCode());
			}
			
			return resultInfo;
		}
		#endregion

		#region sku 库存 数量  添加入库单商品
		/// <summary>
		/// 
		/// </summary>
		/// <param name="skucode">sku 吗 </param>
		/// <param name="kqid">库位id</param>
		/// <param name="total">条数</param>
		/// <returns></returns>
		public static List<SKUStockNumList> SKUStockNum(string skucode, int kqid, out int total) {
			List<SKUStockNumList> resultInfo = new List<SKUStockNumList>();
			try {
				DataTable WarehouseLocationProductslist = WarehouseLocationProductsService.GetDataTable(kqid, skucode, FormsAuth.GetWarehouseCode());
				total = WarehouseLocationProductslist.Rows.Count;
			int i = 0;
			for (int z = 0; i < WarehouseLocationProductslist.Rows.Count; z++) {
				SKUStockNumList objSKUStockNumList = new SKUStockNumList();
				objSKUStockNumList.ID = i + 1;
				objSKUStockNumList.LibraryCode = WarehouseLocationService.GetQuerySingleByID(ZConvert.StrToInt(WarehouseLocationProductslist.Rows[z]["LocationID"])).Code;
				objSKUStockNumList.Inventory = ZConvert.StrToInt(WarehouseLocationProductslist.Rows[z]["ZkNum"]); 
				objSKUStockNumList.StorageNum = 0;
				i++;
				resultInfo.Add(objSKUStockNumList);
			}
			if (total == 0) {
				total = 1;
				SKUStockNumList objSKUStockNumList = new SKUStockNumList();
				objSKUStockNumList.ID = 1;
				WarehouseLocation obj= WarehouseLocationService.GetQuerySingleByID(kqid);
				if (obj != null) {
					if (obj.TypeID == 1 || obj.TypeID == 2) {
						WarehouseLocation o = WarehouseLocationService.GetQuerySingleByParentID(kqid);
						if (o != null) {
							objSKUStockNumList.LibraryCode = o.Code;
							objSKUStockNumList.IsEdit = 1;
						}

					}
					else {
						objSKUStockNumList.LibraryCode = "";
					}
				}
				else {
					objSKUStockNumList.LibraryCode = "";
				}
				
				objSKUStockNumList.Inventory = 0; 
				objSKUStockNumList.StorageNum = 0;
				i++;
				resultInfo.Add(objSKUStockNumList);
			}
				}
			catch (Exception ex) {
				total = 0;
				Sys.SaveErrorLog(ex, "sku 库存 数量  添加入库单商品", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}
		#endregion

		#region 入库单 商品保存
		/// <summary>
		/// 入库单 商品保存
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static BaseResult PutSKUStockSave(PutSKUStock obj) {
			BaseResult resultInfo = new BaseResult();
			try {
			//if (obj.ReservoirArea == 0) {
			//	resultInfo.result = 0;
			//	resultInfo.message = "请选择库区";
			//	return resultInfo;
			//}
			if (ZConvert.StrToDecimal(obj.PurchasePrice, 0) == 0) {
				resultInfo.result = 0;
				resultInfo.message = "请填写采购价";
				return resultInfo;
			}
			using (IDbContext context = Db.GetInstance().Context()) {
				context.UseTransaction(true);
				int num = 0; //入库数量
				for (int i = 0; i < obj.LibraryCode.Length; i++) {
					num += obj.StorageNum[i];
				}
				if (num == 0) {

					resultInfo.result = 0;
					resultInfo.message = "入库数量不能为0";
					
				}

				if (resultInfo.result != 0) { 
				for (int i = 0; i < obj.LibraryCode.Length; i++) {
					#region 入库单商品
					WarehouseOutInStockItem objWarehouseOutInStockItem = new WarehouseOutInStockItem();
					WarehouseOutInStock objWarehouseOutInStock = WarehouseOutInStockService.GetQuerySingleByBillNo(obj.BillNo, context);
					objWarehouseOutInStockItem.OutInStockID = objWarehouseOutInStock.ID;
					objWarehouseOutInStockItem.OutInStockBillNo = obj.BillNo;
					objWarehouseOutInStockItem.BillType = objWarehouseOutInStock.BillType;
					objWarehouseOutInStockItem.SourceID = objWarehouseOutInStock.SourceID;
					objWarehouseOutInStockItem.SourceNo = objWarehouseOutInStock.SourceNo;
					objWarehouseOutInStockItem.StockWay = (int)StockWay.入库;
					objWarehouseOutInStockItem.WarehouseCode = objWarehouseOutInStock.WarehouseCode;
					objWarehouseOutInStockItem.Status = objWarehouseOutInStock.Status;
					objWarehouseOutInStockItem.IsAuditPrice = objWarehouseOutInStock.IsAuditPrice;
					ProductsSku objProductsSku = ProductsSkuService.GetSingleProductsSku(obj.ffcode);
					Products objProducts = ProductsService.GetSingleProducts(objProductsSku.ProductsID);
					objWarehouseOutInStockItem.ProductsID = objProducts.ID;
					objWarehouseOutInStockItem.ProductsCode = objProducts.Code;
					objWarehouseOutInStockItem.ProductsName = objProducts.Name;
					objWarehouseOutInStockItem.ProductsNo = objProducts.No;
					objWarehouseOutInStockItem.ProductsSkuID = objProductsSku.ID;
					objWarehouseOutInStockItem.ProductsSkuCode = objProductsSku.Code;
					objWarehouseOutInStockItem.ProductsSkuSaleprop = objProductsSku.Saleprop;
					#region 校验
					if (obj.StorageNum[i] == 0) {

						//resultInfo.result = 0;
						//resultInfo.message = "入库数量不能为0";
						continue;
					}

					objWarehouseOutInStockItem.ProductsNum = obj.StorageNum[i];//
					if (string.IsNullOrEmpty(obj.LibraryCode[i])) {

						resultInfo.result = 0;
						resultInfo.message = "库位编码不能为空！";
						break;
					}
					WarehouseLocation WarehouseLocation = WarehouseLocationService.GetSingleWarehouseLocation(objWarehouseOutInStock.WarehouseCode, obj.LibraryCode[i], context);
					if (WarehouseLocation == null) {
						resultInfo.result = 0;
						resultInfo.message = "库位编码[" + obj.LibraryCode[i] + "]不存在！";
						break;
					} 
					#endregion
					objWarehouseOutInStockItem.LocationID = WarehouseLocation.ID;				
					objWarehouseOutInStockItem.ProductionDate = ZConvert.StrToDateTime(obj.ProductionDate, DateTime.Now);
					objWarehouseOutInStockItem.CostPrice = ZConvert.StrToDecimal(obj.PurchasePrice);
					objWarehouseOutInStockItem.CreateDate = System.DateTime.Now;
					objWarehouseOutInStockItem.CreatePerson = FormsAuth.GetUserCode();
					objWarehouseOutInStockItem.UpdateDate = System.DateTime.Now;
					objWarehouseOutInStockItem.UpdatePerson = FormsAuth.GetUserCode();
					int outInStockID = WarehouseOutInStockItemService.Add(objWarehouseOutInStockItem, context);
					if (outInStockID == 0) {
						resultInfo.result = 0;
						resultInfo.message = "添加失败！";
					}
					#endregion

				}
				}
				if (resultInfo.result == 0) {
					context.Rollback();				
				}
				else {
					context.Commit();
				}
				
			}
				
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "添加失败！";
				Sys.SaveErrorLog(ex, "入库单商品保存", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}
		#endregion

		#region 入库单  商品 列表  单行修改

		/// <summary>
		/// 入库单  商品 列表  单行修改
		/// </summary>
		/// <param name="userCode">操作帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outInStockItemID">入库单商品表主键ID</param>
		/// <param name="costPrice">采购价</param>
		/// <param name="productionDate">生产日期</param>
		/// <param name="locationcode">库位编码</param>
		/// <param name="productsNum">入库数量</param>
		/// <returns></returns>
		public static BaseResult rowsave(string userCode, string warehouseCode, int outInStockItemID, decimal costPrice, string productionDate, string locationcode, int productsNum) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					int OldNum = 0;
					#region 验证参数
					if (costPrice <= 0) {
						resultInfo.result = 0;
						resultInfo.message = "采购价必须大于0！";
						return resultInfo;
					}
					if (string.IsNullOrEmpty(productionDate)) {
						resultInfo.result = 0;
						resultInfo.message = "生产日期不能为空！";
						return resultInfo;
					}
					if (string.IsNullOrEmpty(locationcode)) {
						resultInfo.result = 0;
						resultInfo.message = "库位编码不能为空！";
						return resultInfo;
					}
					int locationID = WarehouseLocationService.GetWarehouseLocationID(warehouseCode, locationcode, context);
					if (locationID == 0) {
						resultInfo.result = 0;
						resultInfo.message = "库位编码[" + locationcode + "]不存在！";
						return resultInfo;
					}
					if (productsNum <= 0) {
						resultInfo.result = 0;
						resultInfo.message = "入库数量必须大于0！";
						return resultInfo;
					}
					#endregion
					WarehouseOutInStockItem objWarehouseOutInStockItem = WarehouseOutInStockItemService.GetQuerySingleByID(outInStockItemID, context);
					OldNum = objWarehouseOutInStockItem.ProductsNum;
					objWarehouseOutInStockItem.CostPrice = costPrice;
					objWarehouseOutInStockItem.ProductionDate = ZConvert.StrToDateTime(productionDate, DateTime.Now);
					objWarehouseOutInStockItem.LocationID = locationID;
					objWarehouseOutInStockItem.ProductsNum = productsNum;
					int count = WarehouseOutInStockItemService.Update(objWarehouseOutInStockItem, context);
					if (count > 0) {
						if (objWarehouseOutInStockItem.SourceItemID > 0) {
							count = WarehousePurchaseItemService.UpdateInStockNum(userCode, objWarehouseOutInStockItem.SourceItemID, productsNum - OldNum, context);
							if (count > 0) {
								WarehousePurchaseService.UpdateInStockNum(userCode, objWarehouseOutInStockItem.SourceID, context);
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "更新采购单已入库数量失败！";
							}
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "保存失败！";
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
				Sys.SaveErrorLog(ex, "入库单商品列表单行修改", userCode);
			}
			return resultInfo;
		}
		#endregion

		#region 入库单  商品 列表   修改   生产日期
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ids">id 列表字符串</param>
		/// <param name="scdate">生产日期</param>
		/// <returns></returns>
		public static BaseResult Updatescdate(string ids, string scdate) {
			BaseResult resultInfo = new BaseResult();
			try {
				int result = WarehouseOutInStockItemService.updateProductionDate(scdate,ids);
			  if (result ==0) {
				  resultInfo.result = 0;
				  resultInfo.message = "修改失败！";
			  }
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "修改失败！";
				Sys.SaveErrorLog(ex, "入库单商品列表修改生产日期", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}
		#endregion

		#region 入库单  商品 列表   修改   库位编码
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ids">id 列表字符串</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="Library">库位编码</param>
		/// <returns></returns>
		public static BaseResult UpdateLocation(string ids, string warehouseCode, string Library) {
			BaseResult resultInfo = new BaseResult();
			try {
				int Locationid = WarehouseLocationService.GetWarehouseLocationID(warehouseCode, Library);
				if (Locationid > 0) {
					int result = WarehouseOutInStockItemService.updateLocationID(Locationid, ids);
					if (result == 0) {
						resultInfo.result = 0;
						resultInfo.message = "修改失败！";
					}
				}
				else {
					resultInfo.result = 0;
					resultInfo.message = "库位编码[" + Library + "]不存在！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "入库单商品列表修改库位编码", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}
		#endregion

		#region 按照采购单商品生成入库单

		/// <summary>
		/// 按照采购单商品生成采购入库单
		/// </summary>
		/// <param name="userCode">操作帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="purchaseID">采购单ID</param>
		/// <param name="purchaseItemIDList">采购单商品ID列表</param>
		/// <returns></returns>
		public static BaseResult PurchaseItemStorage(string userCode, string warehouseCode, int purchaseID, List<int> purchaseItemIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					int outInStockId = 0;
					List<WarehousePurchaseItem> purchaseItemList = WarehousePurchaseItemService.GetWarehousePurchaseItemList(purchaseItemIDList, context);
					purchaseItemList = purchaseItemList.Where(p => p.Num > p.InStockNum).ToList();
					if (purchaseItemList.Count > 0) {
						string billNo = Sys.GetBillNo(BillType.CGR.ToString());
						DateTime createDate = DateTime.Now;
						WarehousePurchase purchase = WarehousePurchaseService.GetQuerySingleByID(purchaseID, context);
						#region 创建入库单

						WarehouseOutInStock warehouseOutInStock = new WarehouseOutInStock();
						warehouseOutInStock.BillNo = billNo;
						warehouseOutInStock.BillType = (int)BillType.CGR;
						warehouseOutInStock.WarehouseCode = FormsAuth.GetWarehouseCode();
						warehouseOutInStock.Status = (int)WarehouseOutInStockStatus.未提交;
						warehouseOutInStock.SourceID = purchase.ID;
						warehouseOutInStock.SourceNo = purchase.BillNo;
						warehouseOutInStock.SuppliersID = purchase.SuppliersID;
						warehouseOutInStock.CreatePerson = FormsAuth.GetUserCode();
						warehouseOutInStock.CreateDate = createDate;
						outInStockId = WarehouseOutInStockService.Add(warehouseOutInStock, context);
						if (outInStockId == 0) {
							resultInfo.result = 0;
							resultInfo.message = "添加采购单失败！";
						}

						#endregion
						if (resultInfo.result == 1) {
							foreach (var item in purchaseItemList) {
								#region 添加入库单商品

								WarehouseOutInStockItem warehouseOutInStockItem = new WarehouseOutInStockItem();
								warehouseOutInStockItem.OutInStockID = outInStockId;
								warehouseOutInStockItem.OutInStockBillNo = warehouseOutInStock.BillNo;
								warehouseOutInStockItem.BillType = warehouseOutInStock.BillType;
								warehouseOutInStockItem.SourceID = warehouseOutInStock.SourceID;
								warehouseOutInStockItem.SourceNo = warehouseOutInStock.SourceNo;
								warehouseOutInStockItem.SourceItemID = item.ID;
								warehouseOutInStockItem.StockWay = (int)StockWay.入库;
								warehouseOutInStockItem.WarehouseCode = warehouseCode;
								warehouseOutInStockItem.Status = warehouseOutInStock.Status;
								warehouseOutInStockItem.ProductsID = item.ProductsID;
								warehouseOutInStockItem.ProductsCode = item.ProductsCode;
								warehouseOutInStockItem.ProductsName = item.ProductsName;
								warehouseOutInStockItem.ProductsNo = item.ProductsNo;
								warehouseOutInStockItem.ProductsSkuID = item.ProductsSkuID;
								warehouseOutInStockItem.ProductsSkuCode = item.ProductsSkuCode;
								warehouseOutInStockItem.ProductsSkuSaleprop = item.ProductsSkuSaleprop;
								decimal costPrice = ProductsSkuService.GetCostPrice(item.ProductsSkuID, context);
								if (costPrice == 0) costPrice = ProductsService.GetCostPrice(item.ProductsID, context);
								warehouseOutInStockItem.CostPrice = costPrice;
								warehouseOutInStockItem.ProductionDate = ZConvert.StrToDateTime(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now);
								warehouseOutInStockItem.CreatePerson = userCode;
								warehouseOutInStockItem.CreateDate = createDate;
								int outInStockItemId = WarehouseOutInStockItemService.Add(warehouseOutInStockItem, context);
								if (outInStockItemId == 0) {
									resultInfo.result = 0;
									resultInfo.message = "添加入库单商品SKU码 " + item.ProductsSkuCode + " 失败！";
									break;
								}

								#endregion
							}

						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "没有可以生成入库单的商品！";
					}

					if (resultInfo.result == 1) {
				    	resultInfo=	UpdatePurchase(outInStockId, context);
						if (resultInfo.result == 1) {
							context.Commit();
						}
						else {
							context.Rollback();
						}

					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "采购单商品入库", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除出入库单

		/// <summary>
		/// 删除出入库单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="outInStockIDList">出入库单主键ID列表</param>
		/// <returns></returns>
		public static BaseResult DelOutInStock(string userCode, List<int> outInStockIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (int outInStockID in outInStockIDList) {
						WarehouseOutInStock warehouseOutInStock = WarehouseOutInStockService.GetQuerySingleByID(outInStockID, context);
						int rowsAffected = WarehouseOutInStockService.DelByID(outInStockID, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "删除失败，未提交状态才可以删除！";
							break;
						}
						else {
							//删除之前获取所有商品
							List<WarehouseOutInStockItem> outInStockItemList = WarehouseOutInStockItemService.GetWarehouseOutInStockItemList(outInStockID, context);
							if (outInStockItemList.Count > 0) {
								int count = WarehouseOutInStockItemService.DeleteByOutInStockID(outInStockID, context);
								if (count > 0) {
									bool needUpdateSource = false;
									bool isCk = false;
									switch (warehouseOutInStock.BillType) {
										case (int)BillType.CGR:
											// 查询出来源单据商品明细ID大于0的记录，更新采购单商品已入库数量
											outInStockItemList = outInStockItemList.Where(item => item.SourceItemID > 0).ToList();
											foreach (var outInStockItem in outInStockItemList) {
												count = WarehousePurchaseItemService.UpdateInStockNum(userCode, outInStockItem.SourceItemID, -outInStockItem.ProductsNum, context);
												if (count == 0) {
													resultInfo.result = 0;
													resultInfo.message = "删除失败，更新采购单商品SKU码 " + outInStockItem.ProductsSkuCode + " 的已入库数量失败！";
													needUpdateSource = false;
													break;
												}
												else {
													needUpdateSource = true;
												}
											}
											if (needUpdateSource) {
												//更新采购单已入库数量
												WarehousePurchaseService.UpdateInStockNum(userCode, warehouseOutInStock.SourceID, context);
												//更新采购单的入库单条数
												WarehousePurchaseService.UpdateInStockOrderCount(userCode, warehouseOutInStock.SourceID, context);
												//还原采购单状态
												WarehousePurchaseService.Restore(userCode, warehouseOutInStock.SourceID, context);

											}
											break;
										case (int)BillType.QTR:
											//nothing
											break;
										case (int)BillType.CGC:
											isCk = true;
											break;
										case (int)BillType.QTC:
											isCk = true;
											break;
									}
									if (resultInfo.result == 0) {
										//遍历商品操作失败
										break;
									}
									else {
										if (isCk) {
											#region 还原可用，解除冻结

											foreach (var outInStockItem in outInStockItemList) {
												count = WarehouseProductsBatchService.UpdateDjNumAndKyNum(userCode, outInStockItem.ProductsBatchID, -outInStockItem.ProductsNum, context);
												if (count > 0) {
													count = WarehouseLocationProductsService.UpdateDjNumAndKyNum(userCode, outInStockItem.ProductsSkuID, outInStockItem.LocationID, outInStockItem.ProductsBatchID, -outInStockItem.ProductsNum, context);
													if (count == 0) {
														string locationCode = string.Empty;
														WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(outInStockItem.LocationID, context);
														if (warehouseLocation != null) {
															locationCode = warehouseLocation.Code;
														}
														resultInfo.result = 0;
														resultInfo.message = "删除失败，商品SKU码 " + outInStockItem.ProductsSkuCode + " 商品批次 " + outInStockItem.ProductsBatchCode + " 库位编码 " + locationCode + " 解除冻结失败！";
														break;
													}
												}
												else {
													resultInfo.result = 0;
													resultInfo.message = "删除失败，商品SKU码 " + outInStockItem.ProductsSkuCode + " 商品批次 " + outInStockItem.ProductsBatchCode + " 解除冻结失败！";
													break;
												}
											}

											#endregion
										}
									}
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
				Sys.SaveErrorLog(ex, "删除出入库单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 提交入库单
		public static BaseResult tj(int id) {
			BaseResult resultInfo = new BaseResult();
			WarehouseOutInStock WarehouseOutInStock = WarehouseOutInStockService.GetQuerySingleByID(id);
		    if(WarehouseOutInStock.Status!=(int)WarehouseOutInStockStatus.未提交)
			{
				resultInfo.result = 0;
				resultInfo.message = "入库单已提交！";
				return resultInfo;
			}
			List<WarehouseOutInStockItem> WarehouseOutInStockItem = WarehouseOutInStockItemService.GetWarehouseOutInStockItemList(id);
		
			#region //校验
			if (WarehouseOutInStockItem.Count == 0) {
				resultInfo.result = 0;
				resultInfo.message = "入库单没有商品";
				return resultInfo;
			}
			foreach (var item in WarehouseOutInStockItem) {
				//库位编码
				if (item.LocationID < 1) {
					resultInfo.result = 0;
					resultInfo.message = "该SKU" + item.ProductsSkuCode + "码的库位编码为空";
					break;
				}
				//采购价
				if (item.CostPrice == 0) {
					resultInfo.result = 0;
					resultInfo.message = "该SKU" + item.ProductsSkuCode + "码的采购价为空";
					break;
				}
				//入库数量
				if (item.ProductsNum < 1) {
					resultInfo.result = 0;
					resultInfo.message = "该SKU" + item.ProductsSkuCode + "码的入库数量为空";
					break;
				}
			}
			if (resultInfo.result == 0)
				return resultInfo;
			#endregion
			try {
                 using (IDbContext context = Db.GetInstance().Context()) {
				context.UseTransaction(true);
				int result = WarehouseOutInStockService.updatestatus(id,context);
				if (result == 0) {
					resultInfo.result = 0;
					resultInfo.message = "入库单状态更新失败";
					
				}
					 for (int i = 0; i < WarehouseOutInStockItem.Count(); i++)
					 {
					ProductsSku objProductsSkupc = ProductsSkuService.GetSingleProductsSku(WarehouseOutInStockItem[i].ProductsSkuCode, context);
					Products objProductspc = ProductsService.GetSingleProducts(objProductsSkupc.ProductsID, context);
					#region 添加新批次
					string SourceNo = WarehouseOutInStock.SourceNo;
					string billNo = WarehouseOutInStock.BillNo;
					int productsBatchID = 0;				
                    //查找
					WarehouseProductsBatch warehouseProductsBatch = WarehouseProductsBatchService.GetSingleWarehouseProductsBatch(FormsAuth.GetWarehouseCode(), objProductsSkupc.ID, billNo, context);
					if (warehouseProductsBatch == null) {
						warehouseProductsBatch = new WarehouseProductsBatch();
						//添加
						warehouseProductsBatch.WarehouseCode = FormsAuth.GetWarehouseCode();
						warehouseProductsBatch.ProductsID = objProductspc.ID;
						warehouseProductsBatch.ProductsSkuID = objProductsSkupc.ID;
						warehouseProductsBatch.BatchCode = billNo;
						warehouseProductsBatch.ProductionDate = ZConvert.StrToDateTime(WarehouseOutInStockItem[i].ProductionDate, DateTime.Now);
						warehouseProductsBatch.ShelfLife = objProductspc.ShelfLife;
						warehouseProductsBatch.CostPrice = ZConvert.StrToDecimal(WarehouseOutInStockItem[i].CostPrice);
						warehouseProductsBatch.KyNum = WarehouseOutInStockItem[i].ProductsNum;
						warehouseProductsBatch.ZkNum = WarehouseOutInStockItem[i].ProductsNum;
						warehouseProductsBatch.CreatePerson = FormsAuth.GetUserCode();
						warehouseProductsBatch.CreateDate = DateTime.Now;
						productsBatchID = WarehouseProductsBatchService.Add(warehouseProductsBatch, context);
						if (productsBatchID == 0) {
							resultInfo.result = 0;
							resultInfo.message = "入库单添加批次失败";
							break;

						}
					}
					else {
						//修改
						warehouseProductsBatch.KyNum = warehouseProductsBatch.KyNum + WarehouseOutInStockItem[i].ProductsNum;
						warehouseProductsBatch.ZkNum = warehouseProductsBatch.KyNum + WarehouseOutInStockItem[i].ProductsNum;
						warehouseProductsBatch.UpdatePerson = FormsAuth.GetUserCode();
						warehouseProductsBatch.UpdateDate = DateTime.Now;
						productsBatchID = WarehouseProductsBatchService.Update(warehouseProductsBatch, context);
						if (productsBatchID == 0) {
							resultInfo.result = 0;
							resultInfo.message = "入库单修改批次失败";
							break;

						}
					}
					#endregion

					#region 更新批次号  确认状态
				result=	WarehouseOutInStockItemService.updateproductsBatch(productsBatchID, billNo, WarehouseOutInStockItem[i].ID,context);
			     		if (result == 0) {
						resultInfo.result = 0;
						resultInfo.message = "入库单更新批次号失败";
						break;

					}
					#endregion

					#region 添加库位商品信息
					WarehouseLocationProducts newLocationProducts = new WarehouseLocationProducts();
					newLocationProducts.WarehouseCode = FormsAuth.GetWarehouseCode();
					WarehouseLocation WarehouseLocation = WarehouseLocationService.GetQuerySingleByID(WarehouseOutInStockItem[i].LocationID);
					newLocationProducts.TopLocationID = WarehouseLocation.ParentID;
					newLocationProducts.LocationID = WarehouseLocation.ID;//
					newLocationProducts.LocationTypeID = WarehouseLocation.TypeID;//;
					newLocationProducts.ProductsID = WarehouseOutInStockItem[i].ProductsID;
					newLocationProducts.ProductsSkuID = WarehouseOutInStockItem[i].ProductsSkuID;
					newLocationProducts.ProductsBatchID = productsBatchID;
					newLocationProducts.ProductsBatchCode = warehouseProductsBatch.BatchCode;
					newLocationProducts.ProductionDate = warehouseProductsBatch.ProductionDate;
					newLocationProducts.ShelfLife = warehouseProductsBatch.ShelfLife;
					newLocationProducts.KyNum = WarehouseOutInStockItem[i].ProductsNum;
					newLocationProducts.ZkNum = WarehouseOutInStockItem[i].ProductsNum;
					newLocationProducts.CreatePerson = FormsAuth.GetUserCode();
					newLocationProducts.CreateDate = DateTime.Now;
					result=WarehouseLocationProductsService.Add(newLocationProducts, context);
					if (result == 0) {
						resultInfo.result = 0;
						resultInfo.message = "入库单添加库位商品失败";
						break;
					}
					#endregion

					#region 添加出入库日志
					int BillTypeid = WarehouseOutInStock.BillType;
					WarehouseOutInStockLog warehouseOutInStockLog = new WarehouseOutInStockLog();
					warehouseOutInStockLog.WarehouseCode = FormsAuth.GetWarehouseCode();
					warehouseOutInStockLog.BillType = BillTypeid;
					warehouseOutInStockLog.SourceID = WarehouseOutInStockItem[i].OutInStockID;
					warehouseOutInStockLog.SourceNo = WarehouseOutInStockItem[i].OutInStockBillNo;
					warehouseOutInStockLog.SourceItemID =    WarehouseOutInStockItem[i].ID;
					warehouseOutInStockLog.StockWay = WarehouseOutInStockItem[i].StockWay;
					warehouseOutInStockLog.ProductsID = newLocationProducts.ProductsID;
					warehouseOutInStockLog.ProductsNo = WarehouseOutInStockItem[i].ProductsNo;
					warehouseOutInStockLog.ProductsName = WarehouseOutInStockItem[i].ProductsName;
					warehouseOutInStockLog.ProductsCode = WarehouseOutInStockItem[i].ProductsCode;
					warehouseOutInStockLog.ProductsSkuID = newLocationProducts.ProductsSkuID;
					warehouseOutInStockLog.ProductsSkuCode = WarehouseOutInStockItem[i].ProductsSkuCode;
					warehouseOutInStockLog.ProductsSkuSaleprop = objProductsSkupc.Saleprop;
					warehouseOutInStockLog.LocationID = newLocationProducts.LocationID;
					warehouseOutInStockLog.ProductsBatchID = productsBatchID;
					warehouseOutInStockLog.ProductsBatchCode = warehouseProductsBatch.BatchCode;
					warehouseOutInStockLog.Num = WarehouseOutInStockItem[i].ProductsNum;
					warehouseOutInStockLog.Remark = BillTypeConvert.GetBillTypeName(BillTypeid);
					warehouseOutInStockLog.CreatePerson = FormsAuth.GetUserCode();
					warehouseOutInStockLog.CreateDate = DateTime.Now;
					 result=WarehouseOutInStockLogService.Add(warehouseOutInStockLog, context);
					if (result == 0) {
						resultInfo.result = 0;
						resultInfo.message = "入库单添加入库日志失败";
						break;
					}
					#endregion
				}
					 if (resultInfo.result == 0) {
						 context.Rollback();
					 }
					 else {
						 context.Commit();
					 }
				
			}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "提交入库单失败！";
				Sys.SaveErrorLog(ex, "提交入库单", FormsAuth.GetUserCode());
			}
			
			return resultInfo;
		}
		#endregion

		#region 更新采购单的相关数据
		/// <summary>
		/// 更新采购单的相关数据
		/// </summary>
		/// <param name="id">入库单 id </param>
		/// <returns></returns>
		public static BaseResult UpdatePurchase(int id, IDbContext context) {
			BaseResult resultInfo = new BaseResult();
			WarehouseOutInStock WarehouseOutInStock = WarehouseOutInStockService.GetQuerySingleByID(id, context);
			List<WarehouseOutInStockItem> WarehouseOutInStockItem = WarehouseOutInStockItemService.GetWarehouseOutInStockItemList(id,context); 
			
			try {
          	for (int i = 0; i < WarehouseOutInStockItem.Count(); i++) {
				if (WarehouseOutInStockItem[i].SourceItemID == 0) {
					continue;
				}
					ProductsSku objProductsSkupc = ProductsSkuService.GetSingleProductsSku(WarehouseOutInStockItem[i].ProductsSkuCode, context);
					Products objProductspc = ProductsService.GetSingleProducts(objProductsSkupc.ProductsID, context);
					#region 更新采购单 明细  入库量
					string tnum = WarehouseOutInStockItemService.ProductsNum(WarehouseOutInStockItem[i].SourceItemID, context);
		    		int result = WarehouseOutInStockItemService.updateInStockNum(ZConvert.StrToInt(tnum, 0), WarehouseOutInStockItem[i].SourceItemID,context); 
				
					if (result == 0) {
						resultInfo.result = 0;
						resultInfo.message = "更新采购单明细入库量失败！";
						break;
					}
					#endregion

					#region 提交
					WarehousePurchase objwarehousePurchase = WarehousePurchaseService.GetQuerySingleByID(WarehouseOutInStock.SourceID, context);
					List<WarehousePurchaseItem> objwarehousePurchaseItem = WarehousePurchaseItemService.GetQueryMany("SELECT *  FROM warehousePurchaseItem WHERE PurchaseID=" + WarehouseOutInStock.SourceID, context);
					if (objwarehousePurchase != null) {
						//						Num	int	采购数量
						int cgnum = objwarehousePurchase.Num;
						//InStockNum	int	已入库数量
						int rknum = 0;
						foreach (var item in objwarehousePurchaseItem) {
							rknum += item.InStockNum;
						}
						//InStockOrderCount	int	入库单条数
						int rkts = WarehouseOutInStockService.warehouseOutInStockCOUNT(objwarehousePurchase.ID, context);
						
						//Status	int	状态 0：未确认 10：已确认 20：已结束
						
						result = WarehousePurchaseService.UpdatewarehousePurchasekc(rkts, rknum, WarehouseOutInStock.SourceID, context);
						if (result == 0) {
							resultInfo.result = 0;
							resultInfo.message = "更新采购单失败！";
							break;
						}
					}

					#endregion
				}
		
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "更新采购单的相关数据失败！";
				Sys.SaveErrorLog(ex, "更新采购单的相关数据", FormsAuth.GetUserCode());
			}
			

			return resultInfo;
		}
		#endregion

		#region 保存出入库单

		/// <summary>
		/// 保存出入库单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="obj">出入库实体类</param>
		/// <returns></returns>
		public static BaseResult SaveOutInStock(string userCode, string warehouseCode, WarehouseOutInStock obj) {
			BaseResult resultInfo = new BaseResult();
			string typeName = BillTypeConvert.GetBillTypeName(obj.BillType);
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					obj.BillNo = PaiXie.Api.Bll.Sys.GetBillNo(((BillType)obj.BillType).ToString());
					obj.Status = (int)WarehouseOutInStockStatus.未提交;
					obj.IsAuditPrice = 0;
					obj.OutInDate = DateTime.Now;
					obj.IsDxYs = 0;
					obj.ExpressID = 0;
					//obj.MainName = userCode;
					obj.CreatePerson = userCode;
					obj.CreateDate = DateTime.Now;
					obj.WarehouseCode = warehouseCode;
					int OutInStockID = WarehouseOutInStockService.Add(obj, context);
					if (OutInStockID > 0) {
						if (obj.BillType == (int)BillType.CGR) {
							WarehousePurchase warehousePurchase = WarehousePurchaseService.GetQuerySingleByBillNo(obj.SourceNo, context);
							obj.SourceID = warehousePurchase.ID;//来源单据id
							obj.SuppliersID = warehousePurchase.SuppliersID;
							obj.ID = OutInStockID;
							WarehouseOutInStockService.Update(obj, context);
							//复制采购单商品到采购入库单
							if (!CopyPurchaseItemToInStock(userCode, warehouseCode, OutInStockID, obj, context)) {
								resultInfo.result = 0;
								resultInfo.message = "复制采购单商品到采购入库单失败！";
							}
							else {
								//更新采购单的入库单条数
								int count = WarehousePurchaseService.UpdateInStockOrderCount(userCode, obj.SourceID, context);
								if (count == 0) {
									resultInfo.result = 0;
									resultInfo.message = "更新采购单的入库单条数失败！";
								}
							}
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "保存" + typeName + "失败！";
					}
					if (resultInfo.result == 1) {
					
							context.Commit();
					
						
					}else{
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存" + typeName, userCode);
			}
			return resultInfo;
		}

		/// <summary>
		/// 复制采购单商品到采购入库单
		/// </summary>
		/// <param name="userCode"> 用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outInStockID">采购入库单主键ID</param>
		/// <param name="warehouseOutInStock">采购入库单实体类</param>
		/// <param name="context">数据库连接对象</param>
		private static bool CopyPurchaseItemToInStock(string userCode, string warehouseCode, int outInStockID, WarehouseOutInStock warehouseOutInStock, IDbContext context) {
			bool isSuccess = true;
			List<WarehousePurchaseItem> purchaseItemList = WarehousePurchaseItemService.GetWarehousePurchaseItemList(warehouseOutInStock.SourceID, context);
			purchaseItemList = purchaseItemList.Where(p => p.Num > p.InStockNum).ToList();
			if (purchaseItemList.Count > 0) {
				foreach (var purchaseItem in purchaseItemList) {
					#region 添加入库单商品

					WarehouseOutInStockItem warehouseOutInStockItem = new WarehouseOutInStockItem();
					warehouseOutInStockItem.WarehouseCode = warehouseCode;
					warehouseOutInStockItem.OutInStockID = outInStockID;
					warehouseOutInStockItem.OutInStockBillNo = warehouseOutInStock.BillNo;
					warehouseOutInStockItem.BillType = warehouseOutInStock.BillType;
					warehouseOutInStockItem.SourceID = warehouseOutInStock.SourceID;
					warehouseOutInStockItem.SourceNo = warehouseOutInStock.SourceNo;
					warehouseOutInStockItem.SourceItemID = purchaseItem.ID;
					warehouseOutInStockItem.StockWay = (int)StockWay.入库;
					warehouseOutInStockItem.Status = warehouseOutInStock.Status;
					warehouseOutInStockItem.ProductsID = purchaseItem.ProductsID;
					warehouseOutInStockItem.ProductsCode = purchaseItem.ProductsCode;
					warehouseOutInStockItem.ProductsName = purchaseItem.ProductsName;
					warehouseOutInStockItem.ProductsNo = purchaseItem.ProductsNo;
					warehouseOutInStockItem.ProductsSkuID = purchaseItem.ProductsSkuID;
					warehouseOutInStockItem.ProductsSkuCode = purchaseItem.ProductsSkuCode;
					warehouseOutInStockItem.ProductsSkuSaleprop = purchaseItem.ProductsSkuSaleprop;
					warehouseOutInStockItem.CreatePerson = userCode;
					warehouseOutInStockItem.CreateDate = DateTime.Now;

					WarehousePurchase objWarehousePurchase= WarehousePurchaseService.GetQuerySingleByID(purchaseItem.PurchaseID);
					if (objWarehousePurchase != null) {
						warehouseOutInStockItem.CostPrice = SuppliersManager.GetPurchasePrice(purchaseItem.ProductsSkuID, objWarehousePurchase.SuppliersID);
					}
				
					int outInStockItemID = WarehouseOutInStockItemService.Add(warehouseOutInStockItem, context);
					if (outInStockItemID == 0) {
						isSuccess = false;
						break;
					}

					#endregion
				}
			}
			else {
				isSuccess = false;
			}
			return isSuccess;
		}

		#endregion

		#region 保存出库单商品

		/// <summary>
		/// 保存出库单商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="objWebInfo">出库单商品实体类</param>
		/// <param name="context">数据库连接对象 外部传值则外部提交，否则内部自动创建并提交</param>
		/// <returns></returns>
		public static BaseResult AddOutStockProducts(string userCode, string warehouseCode, OutStockLocationProductsWebInfo objWebInfo, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				bool submitTran = false;
				if (context == null) {
					context = Db.GetInstance().Context();
					context.UseTransaction(true);
					submitTran = true;
				}
				WarehouseOutInStock outInStock = WarehouseOutInStockService.GetQuerySingleByID(objWebInfo.OutInStockID, context);
				if (outInStock.Status == (int)WarehouseOutInStockStatus.未提交) {
					int productsSkuID = objWebInfo.ProductsSkuID;
					string productsSkuCode = objWebInfo.ProductsSkuCode;
					string productsSkuSaleprop = objWebInfo.ProductsSkuSaleprop;
					for (int i = 0; i < objWebInfo.OutNum.Length; i++) {
						int outNum = objWebInfo.OutNum[i];
						if (outNum == 0) {
							continue;
						}
						int locationID = objWebInfo.LocationID[i];
						string locationCode = objWebInfo.LocationCode[i];
						int productsBatchID = objWebInfo.ProductsBatchID[i];
						string productsBatchCode = objWebInfo.ProductsBatchCode[i];
						WarehouseProductsSkuInfo productsSkuInfo = WarehouseProductsSkuService.GetSingleWarehouseProductsSkuInfo(warehouseCode, productsSkuID, context);
						if (productsSkuInfo != null) {
							bool cgrHasPro = false;
							if (outInStock.BillType == (int)BillType.CGC) {
								//采购退货单，只能添加关联采购入库单拥有的商品
								WarehouseOutInStockItem CgrWarehouseOutInStockItem = WarehouseOutInStockItemService.GetSingleWarehouseOutInStockItem(outInStock.SourceID, productsSkuID, productsBatchID, context);
								cgrHasPro = CgrWarehouseOutInStockItem != null;
								if (!cgrHasPro) {
									resultInfo.result = 0;
									resultInfo.message = "商品SKU码 " + productsSkuCode + " 商品批次 " + productsBatchCode + " 不属于关联的采购入库单！";
									break;
								}
							}
							WarehouseOutInStockItem outStockItem = WarehouseOutInStockItemService.GetSingleWarehouseOutInStockItem(objWebInfo.OutInStockID, productsSkuID, locationID, productsBatchID, context);
							if (outStockItem == null) {
								outStockItem = new WarehouseOutInStockItem();
								outStockItem.OutInStockBillNo = objWebInfo.OutInStockBillNo;
								outStockItem.OutInStockID = objWebInfo.OutInStockID;
								outStockItem.BillType = outInStock.BillType;
								outStockItem.StockWay = (int)StockWay.出库;
								outStockItem.WarehouseCode = warehouseCode;
								outStockItem.ProductsCode = objWebInfo.ProductsCode;
								outStockItem.ProductsID = objWebInfo.ProductsID;
								outStockItem.ProductsName = objWebInfo.ProductsName;
								outStockItem.ProductsNo = objWebInfo.ProductsNo;
								outStockItem.ProductsSkuCode = productsSkuCode;
								outStockItem.ProductsSkuID = productsSkuID;
								outStockItem.ProductsSkuSaleprop = productsSkuSaleprop;
								outStockItem.LocationID = locationID;
								outStockItem.ProductsBatchID = productsBatchID;
								outStockItem.ProductsBatchCode = productsBatchCode;
								outStockItem.ProductsNum = outNum;
								outStockItem.Status = (int)WarehouseOutInStockStatus.未提交;
								outStockItem.CreatePerson = userCode;
								outStockItem.CreateDate = DateTime.Now;
								int outStockItemID = WarehouseOutInStockItemService.Add(outStockItem, context);
								if (outStockItemID == 0) {
									resultInfo.result = 0;
									resultInfo.message = "商品SKU码 " + productsSkuCode + " 库位编码 " + locationCode + " 商品批次 " + productsBatchCode + " 添加失败！";
									break;
								}
								else {
									int count = WarehouseProductsBatchService.UpdateDjNumAndKyNum(userCode, productsBatchID, outNum, context);
									if (count > 0) {
										count = WarehouseLocationProductsService.UpdateDjNumAndKyNum(userCode, productsSkuID, locationID, productsBatchID, outNum, context);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "商品SKU码 " + productsSkuCode + " 库位编码 " + locationCode + " 商品批次 " + productsBatchCode + " 更新库位表冻结数量失败！";
											break;
										}
									}
									else {
										resultInfo.result = 0;
										resultInfo.message = "商品SKU码 " + productsSkuCode + " 库位编码 " + locationCode + " 商品批次 " + productsBatchCode + " 更新批次表冻结数量失败！";
										break;
									}
								}
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "商品SKU码 " + productsSkuCode + " 库位编码 " + locationCode + " 商品批次 " + productsBatchCode + " 已经添加过，不能重复添加！";
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
					resultInfo.message = "未提交状态才可以添加商品！";
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

		#region 删除出入库单商品

		/// <summary>
		/// 删除出入库单商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="outInStockID">出入库单主键ID</param>
		/// <param name="outInStockItemIDList">出入库单商品表主键ID列表</param>
		/// <returns></returns>
		public static BaseResult DelOutInStockItem(string userCode, int outInStockID, List<int> outInStockItemIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseOutInStock outInStock = WarehouseOutInStockService.GetQuerySingleByID(outInStockID, context);
					if (outInStock.Status == (int)WarehouseOutInStockStatus.未提交) {
						//删除之前查出采购单商品列表
						List<WarehouseOutInStockItem> outInStockItemList = WarehouseOutInStockItemService.GetWarehouseOutInStockItemList(outInStockItemIDList, context);
						if (outInStockItemList.Count > 0) {
							int rowsAffected = WarehouseOutInStockItemService.Delete(outInStockItemIDList, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "删除失败，" + WarehouseOutInStockStatus.未提交.ToString() + "状态才可以删除！";
							}
							else {
								int count = 0;
								bool isCk = false;
								switch (outInStock.BillType) {
									case (int)BillType.CGR:
										#region 更新采购单和商品的已入库数量

										bool needUpdateSource = false;
										foreach (var outInStockItem in outInStockItemList) {
											if (outInStockItem.SourceItemID > 0) {
												count = WarehousePurchaseItemService.UpdateInStockNum(userCode, outInStockItem.SourceItemID, -outInStockItem.ProductsNum, context);
												if (count == 0) {
													resultInfo.result = 0;
													resultInfo.message = "删除失败，更新采购单商品SKU码 " + outInStockItem.ProductsSkuCode + " 的已入库数量失败！";
													needUpdateSource = false;
													break;
												}
												else {
													needUpdateSource = true;
												}
											}
										}
										if (needUpdateSource) {
											//更新采购单已入库数量
											WarehousePurchaseService.UpdateInStockNum(userCode, outInStock.SourceID, context);
										}

										#endregion
										break;
									case (int)BillType.QTR:
										//nothing
										break;
									case (int)BillType.CGC:
										isCk = true;
										break;
									case (int)BillType.QTC:
										isCk = true;
										break;
								}
								if (isCk) {
									#region 还原可用，解除冻结

									foreach (var outInStockItem in outInStockItemList) {
										count = WarehouseProductsBatchService.UpdateDjNumAndKyNum(userCode, outInStockItem.ProductsBatchID, -outInStockItem.ProductsNum, context);
										if (count > 0) {
											count = WarehouseLocationProductsService.UpdateDjNumAndKyNum(userCode, outInStockItem.ProductsSkuID, outInStockItem.LocationID, outInStockItem.ProductsBatchID, -outInStockItem.ProductsNum, context);
											if (count == 0) {
												string locationCode = string.Empty;
												WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(outInStockItem.LocationID, context);
												if (warehouseLocation != null) {
													locationCode = warehouseLocation.Code;
												}
												resultInfo.result = 0;
												resultInfo.message = "删除失败，商品SKU码 " + outInStockItem.ProductsSkuCode + " 商品批次 " + outInStockItem.ProductsBatchCode + " 库位编码 " + locationCode + " 解除冻结失败！";
												break;
											}
										}
										else {
											resultInfo.result = 0;
											resultInfo.message = "删除失败，商品SKU码 " + outInStockItem.ProductsSkuCode + " 商品批次 " + outInStockItem.ProductsBatchCode + " 解除冻结失败！";
											break;
										}
									}

									#endregion
								}
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "删除失败，可能已经被删除！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = WarehouseOutInStockStatus.未提交.ToString() + "状态才可以删除！";
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
				Sys.SaveErrorLog(ex, "删除出入库单商品", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 修改出库单明细的出库数量

		/// <summary>
		/// 修改出库单明细的出库数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="outStockItemID">出库单明细主键ID</param>
		/// <param name="newOutStockNum">新出库数量</param>
		/// <returns></returns>
		public static BaseResult UpdateOutStockNum(string userCode, int outStockItemID, int newOutStockNum) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseOutInStockItem outStockItem = WarehouseOutInStockItemService.GetQuerySingleByID(outStockItemID, context);
					if (outStockItem != null) {
						//差异数量
						int diffNum = newOutStockNum - outStockItem.ProductsNum;
						int count = WarehouseOutInStockItemService.UpdateProductsNum(userCode, outStockItemID, diffNum, context);
						if (count > 0) {
							int kfhNum = WarehouseLocationProductsService.GetKfhNum(outStockItem.LocationID, outStockItem.ProductsSkuID, outStockItem.ProductsBatchID, context);
							if (kfhNum - (diffNum) >= 0) {
								count = WarehouseProductsBatchService.UpdateDjNumAndKyNum(userCode, outStockItem.ProductsBatchID, diffNum, context);
								if (count > 0) {
									count = WarehouseLocationProductsService.UpdateDjNumAndKyNum(userCode, outStockItem.ProductsSkuID, outStockItem.LocationID, outStockItem.ProductsBatchID, diffNum, context);
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
								resultInfo.message = "库存不足，当前可出库数量只有" + (kfhNum + outStockItem.ProductsNum) + "！";
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "更新出库单明细表失败，已提交或已删除！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "出库单明细已删除！";
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
				Sys.SaveErrorLog(ex, "修改出库单明细的出库数量", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 提交出库单

		/// <summary>
		/// 提交出库单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">出库单主键ID</param>
		/// <returns></returns>
		public static BaseResult SubmitOutStock(string userCode, int id) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					int oldStatus = (int)WarehouseOutInStockStatus.未提交;
					int newStatus = (int)WarehouseOutInStockStatus.待审核;
					int count = WarehouseOutInStockService.UpdateStatus(userCode, id, oldStatus, newStatus, context);
					if (count > 0) {
						List<WarehouseOutInStockItem> outStockItemList = WarehouseOutInStockItemService.GetWarehouseOutInStockItemList(id, context);
						outStockItemList = outStockItemList.Where(o => o.Status == (int)WarehouseOutInStockStatus.未提交).ToList();
						if (outStockItemList.Count > 0) {
							foreach (var item in outStockItemList) {
								count = WarehouseOutInStockItemService.UpdateStatus(userCode, item.ID, oldStatus, newStatus, context);
								if (count > 0) {
									#region 更新批次表库存

									count = WarehouseProductsBatchService.UpdateDjNumAndZkNum(userCode, item.ProductsBatchID, item.ProductsNum, context);
									if (count == 0) {
										resultInfo.result = 0;
										resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 批次号 " + item.ProductsBatchCode + " 更新批次表库存失败！";
										break;
									}

									#endregion

									#region 更新库位商品表库存

									count = WarehouseLocationProductsService.UpdateDjNumAndZkNum(userCode, item.ProductsSkuID, item.LocationID, item.ProductsBatchID, item.ProductsNum, context);
									if (count == 0) {
										string locationCode = string.Empty;
										WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(item.LocationID, context);
										if (warehouseLocation != null) {
											locationCode = warehouseLocation.Code;
										}
										resultInfo.result = 0;
										resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 库位编码 " + locationCode + " 批次号 " + item.ProductsBatchCode + " 更新库位商品表库存失败！";
										break;
									}

									#endregion

									#region 增加出库日志

									WarehouseOutInStockLog outStockLog = new WarehouseOutInStockLog();
									outStockLog.BillType = item.BillType;
									outStockLog.StockWay = item.StockWay;
									outStockLog.SourceID = item.OutInStockID;
									outStockLog.SourceNo = item.OutInStockBillNo;
									outStockLog.SourceItemID = item.ID;
									outStockLog.WarehouseCode = item.WarehouseCode;
									outStockLog.ProductsCode = item.ProductsCode;
									outStockLog.ProductsID = item.ProductsID;
									outStockLog.ProductsName = item.ProductsName;
									outStockLog.ProductsNo = item.ProductsNo;
									outStockLog.ProductsSkuCode = item.ProductsSkuCode;
									outStockLog.ProductsSkuID = item.ProductsSkuID;
									outStockLog.ProductsSkuSaleprop = item.ProductsSkuSaleprop;
									outStockLog.Num = item.ProductsNum;
									outStockLog.LocationID = item.LocationID;
									outStockLog.ProductsBatchID = item.ProductsBatchID;
									outStockLog.ProductsBatchCode = item.ProductsBatchCode;
									outStockLog.Remark = BillTypeConvert.GetBillTypeName(item.BillType);
									outStockLog.CreatePerson = userCode;
									outStockLog.CreateDate = DateTime.Now;
									int outStockLogID = WarehouseOutInStockLogService.Add(outStockLog, context);
									if (outStockLogID == 0) {
										resultInfo.result = 0;
										string locationCode = string.Empty;
										WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(item.LocationID, context);
										if (warehouseLocation != null) {
											locationCode = warehouseLocation.Code;
										}
										resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 库位编码 " + locationCode + " 批次 " + item.ProductsBatchCode + " 增加出库日志失败！";
										break;
									}

									#endregion
								}
								else {
									resultInfo.result = 0;
									string locationCode = string.Empty;
									WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(item.LocationID, context);
									if (warehouseLocation != null) {
										locationCode = warehouseLocation.Code;
									}
									resultInfo.message = "商品SKU码 " + item.ProductsSkuCode + " 库位编码 " + locationCode + " 批次 " + item.ProductsBatchCode + " 更新状态失败，可能已被提交或删除！";
									break;
								}
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "出库单没有可提交的商品！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "更新出库单状态失败，可能已被提交或删除！";
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
				Sys.SaveErrorLog(ex, "提交出库单", userCode);
			}
			return resultInfo;
		}

		#endregion
	}
}