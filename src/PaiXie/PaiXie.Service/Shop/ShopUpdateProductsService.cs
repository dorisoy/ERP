using PaiXie.Data;
using System.Collections.Generic;
using FluentData;
namespace PaiXie.Service {
	public class ShopUpdateProductsService : BaseService<ShopUpdateProducts> {

		public static int Update(ShopUpdateProducts entity) {
			return ShopUpdateProductsRepository.GetInstance().Update(entity);
		}



		public static int Add(ShopUpdateProducts entity) {
			return ShopUpdateProductsRepository.GetInstance().Add(entity);
		}

		#region 删除店铺下载商品
		/// <summary>
		/// 删除店铺下载商品
		/// </summary>
		/// <param name="ShopID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int Del(int ShopID, IDbContext context = null) {
			return ShopUpdateProductsRepository.GetInstance().Del(ShopID,context);
		}
		#endregion

		#region 获取实体列表

		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="shopID">店铺id </param>
		/// <param name="platformType">平台</param>
		/// <param name="OuterId">外部单号</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<ShopUpdateProducts> getshopProductslist(int shopID, int platformType, string OuterId, IDbContext context = null) {
			return ShopUpdateProductsRepository.GetInstance().getshopProductslist(shopID, platformType, OuterId, context);
		}

		#endregion

		#region 获取实体列表

		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="platformType"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<ShopUpdateProducts> getshopProductslist(int shopID, int platformType, IDbContext context = null) {
			return ShopUpdateProductsRepository.GetInstance().getshopProductslist(shopID, platformType, context);
		}

		#endregion

		#region 根据shopid 获取总条数

		/// <summary>
		/// 根据shopid 获取总条数
		/// </summary>
		/// <param name="shopid"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static string shopProductscount(int shopid, IDbContext context = null) {
			return ShopUpdateProductsRepository.GetInstance().shopProductscount(shopid, context);
		}

		#endregion
	}
}