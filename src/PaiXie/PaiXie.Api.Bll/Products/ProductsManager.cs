using FluentData;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
namespace PaiXie.Api.Bll {

	/// <summary>
	/// 商品管理
	/// </summary>
	public class ProductsManager {

		#region 保存商品和Sku 添加、修改、导入 商品
		/// <summary>
		/// 保存商品和Sku 添加、修改、导入 商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="productsInfo">商品信息实体</param>
		/// <param name="isUpdate">已经存在 是否更新</param>
		/// <param name="isImport">是否导入</param>
		/// <returns></returns>
		public static BaseResult Save(string userCode, string position, string target, string buttonName, ProductsInfo productsInfo, bool isUpdate, bool isImport = false) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					string oldProductsMessage = string.Empty;
					string newProductsMessage = string.Empty;
					Products product = productsInfo.Products;
					int productsID = product.ID;//如果是手动修改商品，会带ID过来
					if (productsID == 0) {
						productsID = ProductsService.GetProductsID(product.Code, context);
					}
					else {
						isUpdate = true;
					}
					#region 添加或更新商品信息
					if (productsID == 0) {
						product.CreatePerson = userCode;
						product.CreateDate = DateTime.Now;
						productsID = ProductsService.Add(product, context);
						if (productsID == 0) {
							resultInfo.result = 0;
							resultInfo.message = "商品编码[" + product.Code + "]保存失败！";
							return resultInfo;
						}
						else {
							newProductsMessage = JsonConvert.SerializeObject(product, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
							Sys.WriteSyslog(position, target, buttonName, string.Empty, newProductsMessage, (int)SyslogList.商品, string.Empty, string.Empty, string.Empty, (int)ProjectType.管理端, "0", context);
						}
					}
					else {
						product.UpdatePerson = userCode;
						product.UpdateDate = DateTime.Now;
						if (product.ID > 0) {
							#region 手动修改的，要验证商品编码是否存在（排除当前商品ID）
							int otherProductsID = ProductsService.GetProductsID(product.Code, product.ID, context);
							if (otherProductsID > 0) {
								resultInfo.result = 0;
								resultInfo.message = "商品编码[" + product.Code + "]已经存在，商品ID为[" + otherProductsID + "]";
							}
							else {
								int count = ProductsService.Update(product, out oldProductsMessage, out newProductsMessage, context);
								if (count == 0) {
									resultInfo.result = 0;
									resultInfo.message = "商品编码[" + product.Code + "]更新失败！";
									return resultInfo;
								}
								else {
									#region 删除商品图片
									Products oldProduct = JsonConvert.DeserializeObject<Products>(oldProductsMessage);
									if (!string.IsNullOrEmpty(oldProduct.SmallPic)) {
										string[] arrOldSmallPic = oldProduct.SmallPic.Split(',');
										foreach (var oldSmallPic in arrOldSmallPic) {
											if (!(',' + ZConvert.ToString(product.SmallPic) + ',').Contains(',' + oldSmallPic + ',')) {
												DeleteImg(userCode, oldSmallPic);
											}
										}
									}
									#endregion
									Sys.WriteSyslog(position, target, buttonName, oldProductsMessage, newProductsMessage, (int)SyslogList.商品, string.Empty, string.Empty, string.Empty, (int)ProjectType.管理端, "0", context);
								}
							}
							#endregion
						}
						else {
							#region 添加或导入时发现商品编码已经存在
							if (isUpdate) {
								product.ID = productsID;
								int count = ProductsService.Update(product, out oldProductsMessage, out newProductsMessage, context);
								if (count == 0) {
									resultInfo.result = 0;
									resultInfo.message = "商品编码[" + product.Code + "]更新失败！";
									return resultInfo;
								}
								else {
									Sys.WriteSyslog(position, target, buttonName, oldProductsMessage, newProductsMessage, (int)SyslogList.商品, string.Empty, string.Empty, string.Empty, (int)ProjectType.管理端, "0", context);
								}
							}
							else {
								if (!isImport) {
									resultInfo.result = 0;
									resultInfo.message = "商品编码[" + product.Code + "]已经存在，商品ID为[" + productsID + "]";
									return resultInfo;
								}
							}
							#endregion
						}
					}
					#endregion
					if (productsInfo.ProductsSkuList.Count == 0) {
						#region 没有Sku记录，默认添加一条，以商品编码做为SKU码
						ProductsSku productsSku = new ProductsSku();
						productsSku.ProductsCode = product.Code;
						productsSku.Code = product.Code;
						productsSku.SellingPrice = product.SellingPrice;
						productsSku.CostPrice = product.CostPrice;
						productsSku.Saleprop = "无";
						productsInfo.ProductsSkuList.Add(productsSku);
						#endregion
					}
					List<ProductsSku> ProductsSkuList = productsInfo.ProductsSkuList;
					#region 遍历添加或更新商品Sku信息
					foreach (var skuItem in ProductsSkuList) {
						string oldSkuMessage = string.Empty;
						string newSkuMessage = string.Empty;
						skuItem.ProductsID = productsID;
						if (skuItem.SellingPrice == 0) skuItem.SellingPrice = product.SellingPrice;
						if (skuItem.CostPrice == 0) skuItem.CostPrice = product.CostPrice;
						int productsSkuID = skuItem.ID;
						ProductsSku productsSku = new ProductsSku();
						if (productsSkuID == 0) {
							productsSku = ProductsSkuService.GetSingleProductsSku(skuItem.Code, context);
							if (productsSku != null) {
								productsSkuID = productsSku.ID;
							}
						}
						if (productsSkuID == 0) {
							skuItem.CreateDate = DateTime.Now;
							skuItem.CreatePerson = userCode;
							productsSkuID = ProductsSkuService.Add(skuItem, context);
							if (productsSkuID == 0) {
								resultInfo.result = 0;
								resultInfo.message = "商品编码[" + product.Code + "]添加SKU码[" + skuItem.Code + "]失败！";
								break;
							}
							else {
								//获取拥有当前商品的仓库列表，遍历增加SKU
								List<WarehouseProducts> warehouseProductsList = WarehouseProductsService.GetWarehouseProductsList(productsID, context);
								foreach (var warehouseProducts in warehouseProductsList) {
									WarehouseProductsSku warehouseProductsSku = new WarehouseProductsSku();
									warehouseProductsSku.WarehouseCode = warehouseProducts.WarehouseCode;
									warehouseProductsSku.ProductsID = productsID;
									warehouseProductsSku.ProductsSkuID = productsSkuID;
									warehouseProductsSku.CreatePerson = userCode;
									warehouseProductsSku.CreateDate = DateTime.Now;
									int warehouseProductsSkuID = WarehouseProductsSkuService.Add(warehouseProductsSku, context);
									if (warehouseProductsSkuID == 0) {
										resultInfo.result = 0;
										resultInfo.message = "商品编码[" + product.Code + "]添加SKU码[" + skuItem.Code + "]到仓库失败！";
										break;
									}
								}
								if (resultInfo.result == 1) {
									newSkuMessage = JsonConvert.SerializeObject(skuItem, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
									Sys.WriteSyslog(position, target, buttonName, string.Empty, newSkuMessage, (int)SyslogList.商品, string.Empty, string.Empty, string.Empty, (int)ProjectType.管理端, "0", context);
								}
							}
						}
						else {
							skuItem.UpdatePerson = userCode;
							skuItem.UpdateDate = DateTime.Now;
							if (skuItem.ID > 0) {
								#region 手动修改的，要验证SKU码是否存在（排除当前SkuID）
								ProductsSku otherProductsSku = ProductsSkuService.GetSingleProductsSku(skuItem.Code, skuItem.ID, context);
								if (otherProductsSku != null) {
									resultInfo.result = 0;
									resultInfo.message = "SKU码[" + skuItem.Code + "]已经存在于商品编码[" + otherProductsSku.ProductsCode + "]中！";
								}
								else {
									int count = ProductsSkuService.Update(skuItem, out oldSkuMessage, out newSkuMessage, context);
									if (count == 0) {
										resultInfo.result = 0;
										resultInfo.message = "商品编码[" + product.Code + "]修改SKU码[" + skuItem.Code + "]失败！";
										return resultInfo;
									}
									else {
										Sys.WriteSyslog(position, target, buttonName, oldSkuMessage, newSkuMessage, (int)SyslogList.商品, string.Empty, string.Empty, string.Empty, (int)ProjectType.管理端, "0", context);
									}
								}
								#endregion
							}
							else {
								#region 导入时发现SKU码已经存在
								if (productsID == productsSku.ProductsID) {
									if (isUpdate) {
										skuItem.ID = productsSkuID;
										//同一个商品内才更新
										int count = ProductsSkuService.Update(skuItem, out oldSkuMessage, out newSkuMessage, context);
										if (count == 0) {
											resultInfo.result = 0;
											resultInfo.message = "商品编码[" + product.Code + "]修改SKU码[" + skuItem.Code + "]失败！";
											break;
										}
										else {
											Sys.WriteSyslog(position, target, buttonName, oldSkuMessage, newSkuMessage, (int)SyslogList.商品, string.Empty, string.Empty, string.Empty, (int)ProjectType.管理端, "0", context);
										}
									}
									else {
										//没勾选更新不处理
									}
								}
								else {
									resultInfo.result = 0;
									resultInfo.message = "SKU码[" + skuItem.Code + "]已经存在于商品编码[" + productsSku.ProductsCode + "]中！";
									break;
								}
								#endregion
							}
						}
					}
					#endregion
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
				Sys.SaveErrorLog(ex, "保存商品和Sku", userCode);
			}
			return resultInfo;
		}


		#endregion

		#region 获取指定商品信息(编辑商品时使用)
		/// <summary>
		/// 获取指定商品信息(编辑商品时使用)
		/// </summary>
		/// <param name="productsID">商品表标识</param>
		/// <returns></returns>
		public static ProductsInfo GetProductsInfo(int productsID) {
			ProductsInfo productsInfo = new ProductsInfo();
			productsInfo.Products = ProductsService.GetSingleProducts(productsID);
			productsInfo.ProductsSkuList = ProductsSkuService.GetManyProductsSku(productsID);
			return productsInfo;
		}
		#endregion

		#region 删除商品

		/// <summary>
		/// 删除商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="idList">商品ID列表</param>
		/// <returns></returns>
		public static BaseResult Del(string userCode, List<int> idList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (var productsID in idList) {
						Products products = ProductsService.GetSingleProducts(productsID, context);
						int totalNum = ProductsService.GetTotalNum(productsID, "", context);
						bool canDel = totalNum == 0;
						if (canDel) {
							bool tempFlag = ProductsService.Del(productsID, context) > 0;
							if (tempFlag) {
								ProductsSkuService.DelByProductsID(productsID, context);
								WarehouseProductsService.DeleteByProductsID(productsID, context);
								WarehouseProductsSkuService.DeleteByProductsID(productsID, context);	
								ShopProductsService.DelByProductsID(productsID, context);
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = "商品编码 " + products.Code + " 删除失败，可能已经被删除！";
								break;
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "商品编码 " + products.Code + " 删除失败，有库存或占用的商品不能删除！";
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
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "删除商品", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除商品SKU

		/// <summary>
		/// 删除商品SKU
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsSkuID">商品SKU表标识</param>
		/// <returns></returns>
		public static BaseResult DeleteProductsSku(string userCode, int productsSkuID) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					int totalNum = ProductsSkuService.GetTotalNum(productsSkuID, context);
					bool canDel = totalNum == 0;
					if (canDel) {
						bool tempFlag = ProductsSkuService.Del(productsSkuID, context) > 0;
						if (tempFlag) {
							WarehouseProductsSkuService.DeleteByProductsSkuID(productsSkuID, context);
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "删除商品SKU失败！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "有库存或占用的SKU不能删除！";
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
				Sys.SaveErrorLog(ex, "删除商品SKU", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除商品图片

		/// <summary>
		/// 删除商品图片
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="imgPath">图片相对地址</param>
		/// <returns></returns>
		public static BaseResult DeleteImg(string userCode, string imgPath) {
			BaseResult resultInfo = new BaseResult();
			try {
				string fileDir = ZFiles.MapPath(imgPath);
				ZFiles.DeleteFiles(fileDir);
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "删除商品图片[" + imgPath + "]", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 更换分类保存

		/// <summary>
		/// 更换分类保存
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="idList">商品ID列表</param>
		/// <param name="categoryID">分类ID</param>
		/// <returns></returns>
		public static BaseResult ChangeCategory(string userCode, List<int> idList, int categoryID) {
			BaseResult resultInfo = new BaseResult();
			try {
				bool tempFlag = ProductsService.UpdateProductsCategory(userCode, idList, categoryID) > 0;
				if (!tempFlag) {
					resultInfo.result = 0;
					resultInfo.message = "保存失败！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "更换分类保存", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 更换品牌保存

		/// <summary>
		/// 更换品牌保存
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="idList">商品ID列表</param>
		/// <param name="brandID">品牌ID</param>
		/// <returns></returns>
		public static BaseResult ChangeBrand(string userCode, List<int> idList, int brandID) {
			BaseResult resultInfo = new BaseResult();
			try {
				bool tempFlag = ProductsService.UpdateProductsBrand(userCode, idList, brandID) > 0;
				if (!tempFlag) {
					resultInfo.result = 0;
					resultInfo.message = "保存失败！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "更换品牌保存", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 上架

		/// <summary>
		/// 上架商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="idList">商品ID列表</param>
		/// <returns></returns>
		public static BaseResult OnSale(string userCode, List<int> idList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (var productsID in idList) {
						bool tempFlag = ProductsService.UpdateProductsStatus(userCode, productsID, (int)ProductsStatus.销售中, context) > 0;
						if (!tempFlag) {
							resultInfo.result = 0;
							resultInfo.message = "上架失败！";
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
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "管理端上架商品", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 下架

		/// <summary>
		/// 下架商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="idList">商品ID列表</param>
		/// <returns></returns>
		public static BaseResult OffSale(string userCode, List<int> idList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (var productsID in idList) {
						bool tempFlag = ProductsService.UpdateProductsStatus(userCode, productsID, (int)ProductsStatus.仓库中, context) > 0;
						if (tempFlag) {
							List<int> productsIDList = new List<int>();
							productsIDList.Add(productsID);
							//下架所有仓库商品
							WarehouseProductsService.UpdateProductsStatus(string.Empty, productsIDList, (int)ProductsStatus.仓库中, context);
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "下架失败！";
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
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "管理端下架商品", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存导入商品错误记录

		/// <summary>
		/// 保存导入商品错误记录
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsCode">商品编码</param>
		/// <param name="productsTitle">商品名称</param>
		/// <param name="saleprop">商品属性</param>
		/// <param name="productsSkuCode">商品SKU码</param>
		/// <param name="errorMessage">错误消息</param>
		/// <returns></returns>
		public static BaseResult SaveImportError(string userCode, string productsCode, string productsTitle, string saleprop, string productsSkuCode, string errorMessage) {
			BaseResult resultInfo = new BaseResult();
			try {
				ProductsImportError entity = new ProductsImportError();
				entity.ProductsCode = productsCode;
				entity.ProductsTitle = productsTitle;
				entity.Saleprop = saleprop;
				entity.ProductsSkuCode = productsSkuCode;
				entity.ErrorMessage = errorMessage;
				entity.CreatePerson = userCode;
				entity.CreateDate = DateTime.Now;
				bool tempFlag = ProductsImportErrorService.Add(entity) > 0;
				if (!tempFlag) {
					resultInfo.result = 0;
					resultInfo.message = "保存导入商品错误记录失败！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存导入商品错误记录", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 查看商品库存信息

		/// <summary>
		/// 查看商品库存信息
		/// </summary>
		/// <param name="productsID">商品表标识</param>
		/// <returns></returns>
		public static List<ProductsSkuKucInfo> GetProductsSkuKucInfo(int productsID) {
			List<ProductsSkuKucInfo> skuInfoList = new List<ProductsSkuKucInfo>(); ;
			DataTable dt = ProductsService.GetProductsSkuKucInfo(productsID);
			foreach (DataRow dr in dt.Rows) {
				ProductsSkuKucInfo skuInfo = new ProductsSkuKucInfo();
				skuInfo.ProductsSkuID = ZConvert.StrToInt(dr["ProductsSkuID"]);
				skuInfo.Saleprop = dr["Saleprop"].ToString();
				skuInfo.ProductsSkuCode = dr["ProductsSkuCode"].ToString();
				skuInfo.YsNum = ZConvert.StrToInt(dr["YsNum"]);
				skuInfo.YsZyNum = ZConvert.StrToInt(dr["YsZyNum"]);
				skuInfo.KyNum = ZConvert.StrToInt(dr["KyNum"]) - ZConvert.StrToInt(dr["ZyNum"]) - ZConvert.StrToInt(dr["OrderZyNum"]) + skuInfo.YsNum;
				skuInfo.ZyNum = ZConvert.StrToInt(dr["ZyNum"]) + ZConvert.StrToInt(dr["OrderZyNum"]) + skuInfo.YsZyNum;
				skuInfo.TotalNum = skuInfo.KyNum + skuInfo.ZyNum - ZConvert.StrToInt(dr["BkjYsZyNum"]);
				skuInfo.DjNum = ZConvert.StrToInt(dr["DjNum"]);
				skuInfo.ByNum = ZConvert.StrToInt(dr["ByNum"]);
				skuInfoList.Add(skuInfo);
			}
			return skuInfoList;
		}

		#endregion

		#region 查看各仓库商品库存信息

		/// <summary>
		/// 查看各仓库商品库存信息
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品表标识</param>
		/// <returns></returns>
		public static List<WarehouseProductsSkuKucInfo> GetWarehouseProductsSkuKucInfo(string warehouseCode, int productsID) {
			List<WarehouseProductsSkuKucInfo> warehouseSkuInfoList = new  List<WarehouseProductsSkuKucInfo>();
			DataTable dt = ProductsService.GetWarehouseProductsSkuKucInfo(warehouseCode, productsID);
			foreach (DataRow dr in dt.Rows) {
				WarehouseProductsSkuKucInfo warehouseSkuInfo = new WarehouseProductsSkuKucInfo();
				warehouseSkuInfo.WarehouseCode = dr["WarehouseCode"].ToString();
				warehouseSkuInfo.WarehouseName = dr["WarehouseName"].ToString();
				warehouseSkuInfo.ProductsStatus = ZConvert.StrToInt(dr["ProductsStatus"]);
				warehouseSkuInfo.Saleprop = dr["Saleprop"].ToString();
				warehouseSkuInfo.ProductsSkuCode = dr["ProductsSkuCode"].ToString();
				warehouseSkuInfo.YsNum = ZConvert.StrToInt(dr["YsNum"]);
				warehouseSkuInfo.YsZyNum = ZConvert.StrToInt(dr["YsZyNum"]);
				warehouseSkuInfo.KyNum = ZConvert.StrToInt(dr["KyNum"]) - ZConvert.StrToInt(dr["ZyNum"]) - ZConvert.StrToInt(dr["OrderZyNum"]) + warehouseSkuInfo.YsNum;
				warehouseSkuInfo.ZyNum = ZConvert.StrToInt(dr["ZyNum"]) + ZConvert.StrToInt(dr["OrderZyNum"]) + warehouseSkuInfo.YsZyNum;
				warehouseSkuInfo.TotalNum = warehouseSkuInfo.KyNum + warehouseSkuInfo.ZyNum - ZConvert.StrToInt(dr["BkjYsZyNum"]);
				warehouseSkuInfo.DjNum = ZConvert.StrToInt(dr["DjNum"]);
				warehouseSkuInfo.ByNum = ZConvert.StrToInt(dr["ByNum"]);
				warehouseSkuInfoList.Add(warehouseSkuInfo);
			}
			return warehouseSkuInfoList;
		}

		#endregion

		#region 获取指定商品ID物料关联信息 分组统计

		/// <summary>
		/// 查询商品物料关联信息 分组统计
		/// </summary>
		/// <param name="productsID">商品表标识</param>
		/// <returns></returns>
		public static List<ProductsSkuKucInfo> GetProductsMaterialMapInfo(int productsID) {
			List<ProductsSkuKucInfo> skuInfoList = new List<ProductsSkuKucInfo>(); ;
			DataTable dt = ProductsService.GetProductsMaterialMapInfo(productsID);
			foreach (DataRow dr in dt.Rows) {
				ProductsSkuKucInfo skuInfo = new ProductsSkuKucInfo();
				skuInfo.ProductsSkuID = ZConvert.StrToInt(dr["ProductsSkuID"]);
				skuInfo.Saleprop = dr["Saleprop"].ToString();
				skuInfo.ProductsSkuCode = dr["ProductsSkuCode"].ToString();
				skuInfo.ProductsMaterialMapCount = ZConvert.StrToInt(dr["ProductsMaterialMapCount"]);
				skuInfoList.Add(skuInfo);
			}
			return skuInfoList;
		}

		#endregion

		#region 获取指定商品SKUID物料关联信息

		/// <summary>
		/// 获取指定商品SKUID物料关联信息
		/// </summary>
		/// <param name="sourceProductsSkuID">商品SKU标识</param>
		/// <returns></returns>
		public static List<ProductsSkuMaterialMapInfo> GetProductsSkuMaterialMapInfo(int sourceProductsSkuID)
		{
			List<ProductsSkuMaterialMapInfo> SkuMaterialMapInfoList = new List<ProductsSkuMaterialMapInfo>();
			DataTable dt = ProductsService.GetProductsSkuMaterialMapInfo(sourceProductsSkuID);
			foreach (DataRow dr in dt.Rows) {
				ProductsSkuMaterialMapInfo SkuMaterialMapInfo = new ProductsSkuMaterialMapInfo();
				SkuMaterialMapInfo.ID = ZConvert.StrToInt(dr["ID"]);
				SkuMaterialMapInfo.ProductsName = dr["ProductsName"].ToString();
				SkuMaterialMapInfo.ProductsCode = dr["ProductsCode"].ToString();
				SkuMaterialMapInfo.SmallPic = dr["SmallPic"].ToString();
				SkuMaterialMapInfo.ProductsSkuCode = dr["ProductsSkuCode"].ToString();
				SkuMaterialMapInfo.Saleprop = dr["Saleprop"].ToString();
				SkuMaterialMapInfo.FromNum = ZConvert.StrToInt(dr["FromNum"]);
				SkuMaterialMapInfoList.Add(SkuMaterialMapInfo);
			}
			return SkuMaterialMapInfoList;
		}

		#endregion

		#region 添加物料关联记录

		/// <summary>
		/// 添加物料关联记录
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="productsMaterialMap">物料关联实体</param>
		/// <returns></returns>
		public static BaseResult AddProductsMaterialMap(string userCode, string position, string target, string buttonName, ProductsMaterialMap productsMaterialMap) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					productsMaterialMap.CreatePerson = userCode;
					productsMaterialMap.CreateDate = DateTime.Now;
					bool tempFlag = ProductsMaterialMapService.Add(productsMaterialMap, context) > 0;
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "添加物料关联记录失败！";
					}
					else {
						string message = JsonConvert.SerializeObject(productsMaterialMap, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
						Sys.WriteSyslog(position, target, buttonName, string.Empty, message, (int)SyslogList.商品, string.Empty, string.Empty, string.Empty, (int)ProjectType.管理端, "0", context);
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
				Sys.SaveErrorLog(ex, "添加物料关联记录", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除指定物料关联记录

		/// <summary>
		/// 删除指定物料关联记录
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="productsMaterialMapID">物料关联记录表标识</param>
		/// <returns></returns>
		public static BaseResult DelProductsMaterialMap(string userCode, string position, string target, string buttonName, int productsMaterialMapID) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					ProductsMaterialMap productsMaterialMap = ProductsMaterialMapService.GetSingleProductsMaterialMap(productsMaterialMapID, context);
					bool tempFlag = ProductsMaterialMapService.Del(productsMaterialMapID, context) > 0;
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "删除物料关联记录失败！";
					}
					else {
						string message = JsonConvert.SerializeObject(productsMaterialMap, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
						Sys.WriteSyslog(position, target, buttonName, message, string.Empty, (int)SyslogList.商品, string.Empty, string.Empty, string.Empty, (int)ProjectType.管理端, "0", context);
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
				Sys.SaveErrorLog(ex, "删除物料关联记录", userCode);
			}
			return resultInfo;
		}

		#endregion
	}
}
