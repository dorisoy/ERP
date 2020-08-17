using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;

namespace PaiXie.Api.Bll {

	/// <summary>
	/// 商品分类管理
	/// </summary>
	public class CategoryManager {

		#region 删除分类并把对应商品的分类设置为未分类

		/// <summary>
		///删除分类并把对应商品的分类设置为未分类
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="idList">分类ID列表</param>
		/// <returns></returns>
		public static BaseResult Del(string userCode, List<int> idList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (var categoryID in idList) {
						Category category = CategoryService.GetSingleCategory(categoryID, context);
						bool tempFlag = CategoryService.Del(categoryID, context) > 0;
						if (tempFlag) {
							List<int> productsIDList = ProductsService.GetProductsIDListByCategoryID(categoryID, context);
							if (productsIDList.Count > 0) {
								tempFlag = ProductsService.UpdateProductsCategory(userCode, productsIDList, 0, context) > 0;
								if (!tempFlag) {
									resultInfo.result = 0;
									resultInfo.message = "分类名称：" + category.Name + " 清空对应商品的分类失败！";
									break;
								}
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "分类名称：" + category.Name + " 删除失败！";
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
				Sys.SaveErrorLog(ex, "删除商品分类", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存分类信息

		/// <summary>
		/// 保存分类信息
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="obj">分类信息实体类</param>
		/// <returns></returns>
		public static BaseResult Save(string userCode, Category obj) {
			BaseResult resultInfo = new BaseResult();
			try {
				if (obj.ID == 0) {
					obj.CreatePerson = userCode;
					obj.CreateDate = DateTime.Now;
					obj.ParentID = obj.ParentID == -1 ? 0 : obj.ParentID;
					bool tempFlag = CategoryService.Add(obj) > 0;
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "添加分类失败！";
					}
				}
				else {
					Category objCategory = CategoryService.GetSingleCategory(obj.ID);
					objCategory.Code = obj.Code;
					objCategory.ParentID = obj.ParentID == -1 ? 0 : obj.ParentID;
					objCategory.Name = obj.Name;
					objCategory.UpdatePerson = userCode;
					objCategory.UpdateDate = DateTime.Now;
					bool tempFlag = CategoryService.Update(objCategory) > 0;
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "修改分类失败！";
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存分类信息", userCode);
			}
			return resultInfo;
		}

		#endregion

	}
}
