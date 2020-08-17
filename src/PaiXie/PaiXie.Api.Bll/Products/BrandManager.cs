using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;

namespace PaiXie.Api.Bll {

	/// <summary>
	/// 商品品牌管理
	/// </summary>
	public class BrandManager {

		#region 删除品牌并把对应商品的品牌设置为无品牌
		/// <summary>
		///删除品牌并把对应商品的品牌设置为无品牌
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="idList">品牌ID列表</param>
		/// <returns></returns>
		public static BaseResult Del(string userCode, List<int> idList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (var brandID in idList) {
						Brand brand = BrandService.GetSingleBrand(brandID, context);
						bool tempFlag = BrandService.Del(brandID, context) > 0;
						if (tempFlag) {
							List<int> productsIDList = ProductsService.GetProductsIDListByBrandID(brandID, context);
							if (productsIDList.Count > 0) {
								tempFlag = ProductsService.UpdateProductsBrand(userCode, productsIDList, 0, context) > 0;
								if (!tempFlag) {
									resultInfo.result = 0;
									resultInfo.message = "品牌名称：" + brand.Name + " 清空对应商品的品牌失败！";
									break;
								}
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "品牌名称：" + brand.Name + " 删除失败！";
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
				Sys.SaveErrorLog(ex, "删除商品品牌", userCode);
			}
			return resultInfo;
		}
		#endregion

		#region 保存品牌信息
		/// <summary>
		/// 保存品牌信息
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="obj">品牌信息实体类</param>
		/// <returns></returns>
		public static BaseResult Save(string userCode, Brand obj) {
			BaseResult resultInfo = new BaseResult();
			try {
				if (obj.ID == 0) {
					obj.CreatePerson = userCode;
					obj.CreateDate = DateTime.Now;
					bool tempFlag = BrandService.Add(obj) > 0;
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "添加品牌失败！";
					}
				}
				else {
					Brand objBrand = BrandService.GetSingleBrand(obj.ID);
					objBrand.Code = obj.Code;
					objBrand.Name = obj.Name;
					objBrand.Remark = obj.Remark;
					objBrand.UpdatePerson = userCode;
					objBrand.UpdateDate = DateTime.Now;
					bool tempFlag = BrandService.Update(objBrand) > 0;
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "修改品牌失败！";
					}

				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存品牌信息", userCode);
			}
			return resultInfo;
		}
		#endregion
	}
}
