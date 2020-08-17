#region using
using Newtonsoft.Json;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using FluentData;
using Newtonsoft.Json.Converters;
using System.Linq;
#endregion
namespace PaiXie.Api.Bll {

	/// <summary>
	/// 店铺商品管理类
	/// </summary>
	public class ShopProductsManager {

		#region 根据店铺ID和商品状态下载商品

		/// <summary>
		/// 根据店铺ID和商品状态下载商品
		/// </summary>
		/// <param name="param">下载商品参数实体</param>
		public static void DownLoad(DownProductsParam param) {
			BaseResult resultInfo = new BaseResult();
			int shopID = param.ShopID;
			int productsStatus = param.ProductsStatus;
			string taskID = param.TaskID;
			string userCode = param.UserCode;
			try {
				ShopTask currentShopTask = new ShopTask();
				currentShopTask.TaskID = taskID;
				currentShopTask.ShopID = shopID;
				currentShopTask.TaskType = (int)ShopTaskType.下载商品;
				currentShopTask.TaskStatus = (int)ShopTaskStatus.进行中;
				currentShopTask.CreatePerson = userCode;
				currentShopTask.CreateDate = DateTime.Now;
				bool tempFlag = ShopTaskService.Add(currentShopTask) > 0;
				if (tempFlag) {
					Shop shopInfo = ShopService.GetSingleShop(shopID);
					if (shopInfo != null) {
						int platformType = shopInfo.PlatformType;
						string url = string.Empty;
						switch (platformType) {
							case (int)ThirdApi.微小店:
								url = ZConfig.GetConfigString("WeiXiaoDian_Url"); ;
								resultInfo = DownWeiXiaoDian(taskID, userCode, shopID, platformType, url, shopInfo.AppKey, shopInfo.AppSecret, shopInfo.AppSession, productsStatus);
								break;
							default:
								resultInfo.result = 0;
								resultInfo.message = "平台类型枚举值[" + platformType + "]不存在！";
								break;
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "店铺ID[" + shopID + "]不存在！";
					}
				}
				else {
					resultInfo.result = 0;
					resultInfo.message = "创建下载商品任务失败！";
				}
				if (resultInfo.result == 0) {
					Sys.SaveErrorLog(new Exception(), resultInfo.message, userCode);
				}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "下载商品", userCode);
			}
		}
		#endregion

		#region 下载微小店商品

		/// <summary>
		/// 下载微小店商品
		/// </summary>
		/// <param name="taskID">任务ID</param>
		/// <param name="userCode">操作人</param>
		/// <param name="shopID">店铺表标识</param>
		/// <param name="platformType">平台枚举值</param>
		/// <param name="url">拍鞋网或微小店接口地址</param>
		/// <param name="AppKey">AppKey</param>
		/// <param name="AppSecret">AppSecret</param>
		/// <param name="AppSession">AppSession</param>
		/// <param name="status">1出售中(上架中)  2下架商品(除无货下架外)  3下架商品(缺货)</param>
		private static BaseResult DownWeiXiaoDian(string taskID, string userCode, int shopID, int platformType, string url, string AppKey, string AppSecret, string AppSession, int status) {
			BaseResult resultInfo = new BaseResult();
			string method = "GetProList";
			int pageNo = 1;
			int pageSize = ZConfig.GetConfigInt("PaiXie_PageSize");
			bool hasNext = false;
			do {
				IDictionary<string, string> dic = new Dictionary<string, string>();
				dic.Add("api_key", AppKey);
				dic.Add("api_secret", AppSecret);
				dic.Add("api_signkey", AppSession);
				dic.Add("method", method);
				dic.Add("page_no", pageNo.ToString());
				dic.Add("page_size", pageSize.ToString());
				if (status > 0) {
					dic.Add("status", status.ToString());
				}
				string rspStr = PXinterface.GetPost(url, dic);
				Root root = JsonConvert.DeserializeObject<Root>(rspStr);
				//更新任务总条数 请求报文 响应报文
				string reqStr = "url:" + url + " param:" + JsonConvert.SerializeObject(dic, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
				int totalCount = 0;
				try {
					totalCount = root.Data.Pro_Total;
				}
				catch { }
				ShopTaskService.UpdateTotalCount(taskID, totalCount, reqStr, rspStr);
				if (root.msg.ToLower() == "success" && root.Data != null) {
					if (root.Data.ProductList != null) {
						List<ProductList> productList = root.Data.ProductList;
						foreach (var item in productList) {
							try {
								//查询goods_id是否已经存在
								ShopProducts shopProducts = ShopProductsService.GetSingleShopProductsByGoodsId(shopID, ZConvert.StrToInt(item.goods_id));
								if (shopProducts == null) {
									shopProducts = new ShopProducts();
									shopProducts.CreateDate = DateTime.Now;
									shopProducts.CreatePerson = userCode;
									shopProducts.CateId = ZConvert.StrToInt(item.CateId);
									shopProducts.CustomCateId = item.CustomCateId;
									shopProducts.GoodsId = ZConvert.StrToInt(item.goods_id);
									shopProducts.OuterId = item.goods_id;
									shopProducts.ImgUrl = item.Img_Url;
									shopProducts.Num = ZConvert.StrToInt(item.Num);
									shopProducts.PlatformType = platformType;
									shopProducts.Price = ZConvert.StrToDecimal(item.Price);
									if (item.ProductKucList != null) {
										shopProducts.ProductKucList = JsonConvert.SerializeObject(item.ProductKucList);
									}
									else {
										shopProducts.ProductKucList = "[{\"Outer_id\": \"" + item.outer_id + "\",\"Properties_alias\": \"\",\"Sku_id\": \"0\",\"Quantity\": \"" + ZConvert.StrToInt(item.Num) + "\",\"Price\": \"" + ZConvert.StrToDecimal(item.Price) + "\"}]";
									}
									shopProducts.ProNo = item.Pro_No;
									shopProducts.ProTitle = item.Pro_Title;
									shopProducts.ShopID = shopID;
									shopProducts.ProductsStatus = status;
									ShopProductsService.Add(shopProducts);
								}
								else {
									Sys.SaveErrorLog(new Exception(), "下载微小店商品时，发现goods_id[" + item.outer_id + "]已经存在", userCode);
								}
							}
							catch (Exception ex) {
								Sys.SaveErrorLog(ex, "保存微小店商品编码[" + item.outer_id + "]", userCode);
							}
							//更新进度
							ShopTaskService.UpdateFinshCount(taskID);
						}
						hasNext = root.Data.Pro_Total > pageNo * pageSize;
						if (hasNext) {
							pageNo++;
						}
					}
				}
			} while (hasNext);
			return resultInfo;
		}

		#endregion

		#region 导入店铺商品到系统

		/// <summary>
		/// 导入店铺商品到系统
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="shopProductsIDs">店铺商品表标识，多个以半角逗号分隔</param>
		/// <returns></returns>
		public static BaseResult Import(string userCode, string shopProductsIDs) {
			BaseResult resultInfo = new BaseResult();
			int successCount = 0;
			int errorCount = 0;
			try {
				string[] arrId = shopProductsIDs.Split(',');
				#region 遍历商品进行导入，一个商品一个事务
				foreach (var id in arrId) {
					BaseResult currentResultInfo = new BaseResult();
					try {
						using (IDbContext context = Db.GetInstance().Context()) {
							context.UseTransaction(true);
							ShopProducts shopProducts = ShopProductsService.GetSingleShopProducts(ZConvert.StrToInt(id), context);
							if (shopProducts.ProductsID == 0) {
								Products products = new Products();
								products.Name = shopProducts.ProTitle;
								products.No = shopProducts.ProNo;
								products.Code = shopProducts.OuterId;
								products.SellingPrice = shopProducts.Price;
								products.Status = (int)ProductsStatus.仓库中;
								products.SaleType = (int)SaleType.销售 + (int)SaleType.物料;
								products.SmallPic = shopProducts.ImgUrl;
								products.CreateDate = DateTime.Now;
								products.CreatePerson = userCode;
								int productsID = ProductsService.Add(products, context);
								if (productsID > 0) {
									#region 根据不同平台处理商品SKU
									switch (shopProducts.PlatformType) {
										case (int)ThirdApi.微小店:
											List<ProductKucList> productSkuList = JsonConvert.DeserializeObject(shopProducts.ProductKucList, typeof(List<ProductKucList>)) as List<ProductKucList>;
											foreach (var skuItem in productSkuList) {
												ProductsSku productsSku = new ProductsSku();
												productsSku.Code = skuItem.Outer_id;
												if (skuItem.Outer_id != "") {
													productsSku.ProductsID = productsID;
													string Saleprop = string.Empty;
													try {
														//720:1020:颜色分类:深蓝色;50:995:尺码:S
														string[] arrProp = skuItem.Properties_alias.Split(';');
														foreach (var prop in arrProp) {
															string[] arrInfo = prop.Split(':');
															Saleprop += arrInfo[3] + "，";
														}
														Saleprop = Saleprop.TrimEnd('，');
													}
													catch (Exception ex) {
														Saleprop = skuItem.Properties_alias;
														Sys.SaveErrorLog(ex, "导入店铺商品到系统，拆分销售属性", userCode);
													}
													productsSku.Saleprop = Saleprop != "" ? Saleprop : "无";
													productsSku.ProductsCode = products.Code;
													productsSku.SellingPrice = ZConvert.StrToDecimal(skuItem.Price) == 0 ? products.SellingPrice : ZConvert.StrToDecimal(skuItem.Price);
													productsSku.CreateDate = DateTime.Now;
													productsSku.CreatePerson = userCode;
													int productsSkuID = ProductsSkuService.Add(productsSku, context);
													if (productsSkuID <= 0) {
														currentResultInfo.result = 0;
														currentResultInfo.message = "商品SKU码[" + productsSku.Code + "]添加失败！";
														break;
													}
												}
												else {
													currentResultInfo.result = 0;
													currentResultInfo.message = "有SKU码未设置！";
													break;
												}
											}
											break;
									}
									#endregion
								}
								else {
									currentResultInfo.result = 0;
									currentResultInfo.message = "商品编码[" + products.Code + "]添加失败！";
								}
								if (currentResultInfo.result == 1) {
									ShopProductsService.UpdateProductsID(ZConvert.StrToInt(id), productsID, context);
									successCount++;
									context.Commit();
								}
								else {
									errorCount++;
									context.Rollback();
								}
							}
							else {
								//已经导入的商品，直接跳过
								successCount++;
							}
						}
					}
					catch (Exception ex) {
						currentResultInfo.result = 0;
						currentResultInfo.message = ex.Message.Contains("uni_productsSku_Code") ? "SKU码设置重复！" : "程序出现异常！";
						errorCount++;
						Sys.SaveErrorLog(ex, "导入店铺商品到系统", userCode);
					}
					if (currentResultInfo.result == 0) {
						ShopProductsService.UpdateErrorMessage(ZConvert.StrToInt(id), currentResultInfo.message);
					}
				}
				#endregion

				if (errorCount > 0) {
					resultInfo.result = 0;
					resultInfo.message = successCount + " 条导入成功，" + errorCount + " 条导入失败！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "导入店铺商品到系统", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除商品

		/// <summary>
		/// 删除店铺商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="idList">店铺商品表ID列表</param>
		/// <returns></returns>
		public static BaseResult Del(string userCode, List<int> idList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (var shopProductsID in idList) {
						ShopProducts shopProducts = ShopProductsService.GetSingleShopProducts(shopProductsID, context);
						bool tempFlag = ShopProductsService.Del(shopProductsID) > 0;
						if (!tempFlag) {
							resultInfo.result = 0;
							resultInfo.message = "商品编码：" + shopProducts.OuterId + " 删除失败，可能已经被删除！";
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
				Sys.SaveErrorLog(ex, "删除店铺商品", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 根据店铺ID更新库存	
		public static void ShopStockUpdate(int shopid, string OuterId="") {
			BaseResult resultInfo = new BaseResult();
			int shopID = shopid;
				Shop shopInfo = ShopService.GetSingleShop(shopID);
				if (shopInfo != null) {
					int platformType = shopInfo.PlatformType;
					string url = string.Empty;
					switch (platformType) {
						case (int)ThirdApi.微小店:
							url = ZConfig.GetConfigString("WeiXiaoDian_Url"); ;
							resultInfo = ShopStockUpdatewxd(shopID, platformType, url, shopInfo.AppKey, shopInfo.AppSecret, shopInfo.AppSession, OuterId);
							break;
						default:
							resultInfo.result = 0;
							resultInfo.message = "平台类型枚举值[" + platformType + "]不存在！";
							break;
					}
				}
				else {
					resultInfo.result = 0;
					resultInfo.message = "店铺ID[" + shopID + "]不存在！";
				}
			}
		/// <summary>
		/// 接口是否实现
		/// </summary>
		/// <param name="shopid"></param>
		/// <returns></returns>
		public static BaseResult IsShopStockUpdate(int shopid) 
		{
			BaseResult resultInfo = new BaseResult();
			Shop shopInfo = ShopService.GetSingleShop(shopid);
			if (shopInfo != null) {
				int platformType = shopInfo.PlatformType;
			#region MyRegion
			switch (platformType) {
					case (int)ThirdApi.微小店:
						break;
					default:
						resultInfo.result = 0;
						resultInfo.message = "该平台接口还未实现！";
						break;
				} 
	#endregion
			}

			return resultInfo;
}

	
		#endregion

		#region 微小店 库存更新

		private static BaseResult ShopStockUpdatewxd(int shopID, int platformType, string url, string AppKey, string AppSecret, string AppSession, string OuterId="") {
			BaseResult resultInfo = new BaseResult();
			try {
				string errmsg = "";
				string method = "GetUpdateQty";
				List<ShopUpdateProducts> ShopProductslist = null;
				if (OuterId != "")
					ShopProductslist = ShopUpdateProductsService.getshopProductslist(shopID, platformType, OuterId);
				else
					ShopProductslist = ShopUpdateProductsService.getshopProductslist(shopID, platformType);
				//删除旧的数据
			//	ShopProductsService.Del(shopID, platformType);

				foreach (var objShopProducts in ShopProductslist) {
					errmsg = ""; //清空消息
					
					List<ProductKucList> productSkuList = JsonConvert.DeserializeObject(objShopProducts.ProductKucList, typeof(List<ProductKucList>)) as List<ProductKucList>;

					PlanLog.WriteLog(objShopProducts.ProductKucList, "wxd");
					//商品outer_id，多个值以英文半角逗号隔开
					string sku_id = "";
					//SKU值分别对应的库存数
					string quantity = "";
					int skunum = 0;
				 //   List<ProductsSkuKucInfo> ProductsSkuKucInfoList= 	ProductsManager.GetProductsSkuKucInfo(objShopProducts.ProductsID);

					#region sku 组装
					foreach (var objproductSkuList in productSkuList) {
						int flag = 0; //sku 是否为空表示
					//	if (!errmsg.Contains("SKU码为空")) {
							if (string.IsNullOrEmpty(objproductSkuList.Outer_id)) {
								errmsg += "SKU码为空<br/>";
								flag = 1;
							}
						//}

						
						sku_id += objproductSkuList.Outer_id + ",";




						

						#region 根据分配 获取库存
						int kc = 0;
						//
					//	ProductsSkuKucInfo objProductsSkuKucInfo = ProductsSkuKucInfoList.Where(p => p.ProductsSkuCode == objproductSkuList.Outer_id).FirstOrDefault();

					

						int ProductsID = ProductsService.GetProductsID(OuterId);
						int sku_ID = 0;
						ProductsSku objProductsSku = ProductsSkuService.GetSingleProductsSku(objproductSkuList.Outer_id);
						if (objProductsSku != null) {
							sku_ID = objProductsSku.ID;
						}
						else {
							if(flag!=1)
							errmsg += "系统未找到SKU码" + objproductSkuList.Outer_id + "<br/>";
							
						}
						int kfhNum = ProductsSkuService.GetKfhNumByProductsSkuID(sku_ID, 1);
					//	PlanLog.WriteLog("可用库存" + kfhNum.ToString(), "wxd");


						#region 库存
						if (kfhNum != 0) {
							#region MyRegion

						//	PlanLog.WriteLog("商品id" + ProductsID.ToString() + "skuid" + sku_ID.ToString(), "wxd");
							//独占的

							List<ShopAllocation> ShopAllocationList = ShopAllocationService.GetQuerySingleByProductsSkuID(sku_ID);
							int _DxNum = ShopAllocationList.Sum(p => p.SaleInventory);

						//	PlanLog.WriteLog("独占的数量" + _DxNum.ToString(), "wxd");



							ShopComancation objShopComancation = ShopComancationService.GetQuerySingle(shopID, ProductsID);
							//PlanLog.WriteLog("个性的", "wxd");
							#endregion

							if (objShopComancation != null) {
								#region //个性的
								double a = ZConvert.StrToDouble((kfhNum - _DxNum)) * (ZConvert.StrToDouble(objShopComancation.Ranges) * 0.01);
							//	PlanLog.WriteLog("个性的" + a.ToString(), "wxd");
								if (0 < a && a < 1) {
									kc = ZConvert.StrToInt(objShopComancation.Remark);
								}
								else {
									kc = ZConvert.StrToInt(a);
								}
								#endregion
							}

							else {
								#region //公用
							//	PlanLog.WriteLog("公用", "wxd");
								ShopComancation objShopComancation2 = ShopComancationService.GetQuerySingle(shopID);
								if (objShopComancation2 != null) {
									double a = ZConvert.StrToDouble((kfhNum - _DxNum)) * (ZConvert.StrToDouble(objShopComancation2.Ranges) * 0.01);
								//	PlanLog.WriteLog("公用" + a.ToString(), "wxd");
									if (0 < a && a < 1) {
										kc = ZConvert.StrToInt(objShopComancation2.Remark);
									}
									else {
										kc = ZConvert.StrToInt(a);
									}
								}
								else {
									kc = kfhNum - _DxNum;
								}
								#endregion
							}




							#region //独享的
							ShopAllocation objShopAllocation = ShopAllocationService.GetQuerySingle(shopID, sku_ID);
							if (objShopAllocation != null) {
								if (objShopAllocation.IsSalePub == 1) {
									kc += objShopAllocation.SaleInventory;
								}
								else {
									kc = objShopAllocation.SaleInventory;
								}
							}
							#endregion
						} 
						#endregion
						
						//测试
					//	kc = 1000;
							quantity += kc + ",";
					
					
						#endregion



						skunum += 1;
					} 
					#endregion
					IDictionary<string, string> dic = new Dictionary<string, string>();
					dic.Add("api_key", AppKey);
					dic.Add("api_secret", AppSecret);
					dic.Add("api_signkey", AppSession);
					dic.Add("method", method);
					dic.Add("sku_id", sku_id.Substring(0, sku_id.Length - 1).ToString());
					dic.Add("quantity", quantity.Substring(0, quantity.Length - 1).ToString());

					string rspStr = PXinterface.GetPost(url, dic);
					// 请求报文 响应报文
					string reqStr = "url:" + url + " param:" + JsonConvert.SerializeObject(dic, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
					PlanLog.WriteLog(reqStr, "wxd");
					PlanLog.WriteLog(rspStr,"wxd");
					wxdrequest root = new wxdrequest();
					try {
						 root = JsonConvert.DeserializeObject<wxdrequest>(rspStr);
					}
					catch {
					//	root.code = -99;
					}
				
				   
					ShopStockUpdate objShopStockUpdate = new Data.ShopStockUpdate();
					objShopStockUpdate.ShopID = shopID;
					objShopStockUpdate.ProductsCode = objShopProducts.OuterId;
					objShopStockUpdate.UpdateTime = System.DateTime.Now;
						if(root.code==0 && string.IsNullOrEmpty(errmsg) )
						{
							objShopStockUpdate.UpdateStatus = 1; 
					        objShopStockUpdate.ErrorMsg = "更新成功";
						}
						else
						{
							objShopStockUpdate.UpdateStatus = 0;
							objShopStockUpdate.ErrorMsg = root.msg + "(" + root.code + ")<br/>" + errmsg;
						}
					
					objShopStockUpdate.PlatformType = platformType;
					objShopStockUpdate.SkuNum = skunum;
					ShopStockUpdateService.Add(objShopStockUpdate);


				}

			}

			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "微小店 库存更新", FormsAuth.GetUserCode());
			}

			return resultInfo;

		
		}
		
		#endregion	

		#region 根据店铺ID和商品状态下载商品  库存更新

		/// <summary>
		/// 根据店铺ID和商品状态下载商品  库存更新
		/// </summary>
		/// <param name="param">下载商品参数实体</param>
		public static void DownLoadUpdate(DownProductsParam param) {
			BaseResult resultInfo = new BaseResult();
			int shopID = param.ShopID;
			int productsStatus = param.ProductsStatus;
			string taskID = param.TaskID;
			string userCode = param.UserCode;
			try {
					Shop shopInfo = ShopService.GetSingleShop(shopID);
					if (shopInfo != null) {
						int platformType = shopInfo.PlatformType;
						string url = string.Empty;
						switch (platformType) {
							case (int)ThirdApi.微小店:
								url = ZConfig.GetConfigString("WeiXiaoDian_Url"); ;
								resultInfo = DownWeiXiaoDianUpdate(taskID, userCode, shopID, platformType, url, shopInfo.AppKey, shopInfo.AppSecret, shopInfo.AppSession, productsStatus);
								break;
							default:
								resultInfo.result = 0;
								resultInfo.message = "平台类型枚举值[" + platformType + "]不存在！";
								break;
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "店铺ID[" + shopID + "]不存在！";
					}
			
				if (resultInfo.result == 0) {
					Sys.SaveErrorLog(new Exception(), resultInfo.message, userCode);
				}
			}
			catch (Exception ex) {
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "下载商品库存更新", userCode);
			}
		}
		#endregion

		#region 下载微小店商品 库存更新

		/// <summary>
		/// 下载微小店商品 库存更新
		/// </summary>
		/// <param name="taskID">任务ID</param>
		/// <param name="userCode">操作人</param>
		/// <param name="shopID">店铺表标识</param>
		/// <param name="platformType">平台枚举值</param>
		/// <param name="url">拍鞋网或微小店接口地址</param>
		/// <param name="AppKey">AppKey</param>
		/// <param name="AppSecret">AppSecret</param>
		/// <param name="AppSession">AppSession</param>
		/// <param name="status">1出售中(上架中)  2下架商品(除无货下架外)  3下架商品(缺货)</param>
		private static BaseResult DownWeiXiaoDianUpdate(string taskID, string userCode, int shopID, int platformType, string url, string AppKey, string AppSecret, string AppSession, int status) {
			BaseResult resultInfo = new BaseResult();
			string method = "GetProList";
			int pageNo = 1;
			int pageSize = ZConfig.GetConfigInt("PaiXie_PageSize");
			bool hasNext = false;
			ShopUpdateProductsService.Del(shopID);
			do {
				IDictionary<string, string> dic = new Dictionary<string, string>();
				dic.Add("api_key", AppKey);
				dic.Add("api_secret", AppSecret);
				dic.Add("api_signkey", AppSession);
				dic.Add("method", method);
				dic.Add("page_no", pageNo.ToString());
				dic.Add("page_size", pageSize.ToString());
				if (status > 0) {
					dic.Add("status", status.ToString());
				}
				string rspStr = PXinterface.GetPost(url, dic);
				Root root = JsonConvert.DeserializeObject<Root>(rspStr);
				//更新任务总条数 请求报文 响应报文
				string reqStr = "url:" + url + " param:" + JsonConvert.SerializeObject(dic, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
				int totalCount = 0;
				try {
					totalCount = root.Data.Pro_Total;
				}
				catch { }
				if (root.msg.ToLower() == "success" && root.Data != null) {
					if (root.Data.ProductList != null) {
						List<ProductList> productList = root.Data.ProductList;
						foreach (var item in productList) {
							try {
								//查询goods_id是否已经存在
								ShopUpdateProducts shopProducts =new ShopUpdateProducts();
								shopProducts = new ShopUpdateProducts();
									shopProducts.CreateDate = DateTime.Now;
									shopProducts.CreatePerson = userCode;
									shopProducts.CateId = ZConvert.StrToInt(item.CateId);
									shopProducts.CustomCateId = item.CustomCateId;
									shopProducts.GoodsId = ZConvert.StrToInt(item.goods_id);
									shopProducts.OuterId = item.goods_id;
									shopProducts.ImgUrl = item.Img_Url;
									shopProducts.Num = ZConvert.StrToInt(item.Num);
									shopProducts.PlatformType = platformType;
									shopProducts.Price = ZConvert.StrToDecimal(item.Price);
									if (item.ProductKucList != null) {
										shopProducts.ProductKucList = JsonConvert.SerializeObject(item.ProductKucList);
									}
									else {
										shopProducts.ProductKucList = "[{\"Outer_id\": \"" + item.outer_id + "\",\"Properties_alias\": \"\",\"Sku_id\": \"0\",\"Quantity\": \"" + ZConvert.StrToInt(item.Num) + "\",\"Price\": \"" + ZConvert.StrToDecimal(item.Price) + "\"}]";
									}
									shopProducts.ProNo = item.Pro_No;
									shopProducts.ProTitle = item.Pro_Title;
									shopProducts.ShopID = shopID;
									shopProducts.ProductsStatus = status;
									ShopUpdateProductsService.Add(shopProducts);
							}
							catch (Exception ex) {
								Sys.SaveErrorLog(ex, "保存微小店商品编码[" + item.outer_id + "]", userCode);
							}
						}
						hasNext = root.Data.Pro_Total > pageNo * pageSize;
						if (hasNext) {
							pageNo++;
						}
					}
				}
			} while (hasNext);
			return resultInfo;
		}

		#endregion
	}
}