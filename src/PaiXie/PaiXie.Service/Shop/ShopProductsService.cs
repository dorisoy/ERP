using PaiXie.Data;
using System.Collections.Generic;
using FluentData;
namespace PaiXie.Service {
	public class ShopProductsService : BaseService<ShopProducts> {

		public static int Update(ShopProducts entity) {
			return ShopProductsRepository.GetInstance().Update(entity);
		}



		public static int Add(ShopProducts entity) {
			return ShopProductsRepository.GetInstance().Add(entity);
		}
		#region 根据店铺ID和商品编码查询店铺商品信息
		/// <summary>
		/// 根据店铺ID和商品货号查询商品信息
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="outerId">平台商品编码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ShopProducts GetSingleShopProductsByOuterId(int shopID, string outerId, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().GetSingleShopProductsByOuterId(shopID, outerId, context);
		}

		#endregion

		#region 根据店铺ID和goodsId查询店铺商品信息
		/// <summary>
		/// 根据店铺ID和goodsId查询商品信息
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="goodsId">goodsId</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ShopProducts GetSingleShopProductsByGoodsId(int shopID, int goodsId, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().GetSingleShopProductsByGoodsId(shopID, goodsId, context);
		}

		#endregion

		#region 根据店铺商品表标识查询店铺商品信息
		/// <summary>
		/// 根据店铺商品表标识查询店铺商品信息
		/// </summary>
		/// <param name="shopProductsID">店铺商品表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ShopProducts GetSingleShopProducts(int shopProductsID, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().GetSingleShopProducts(shopProductsID, context);
		}

		#endregion

		#region 导入成功之后更新商品ID

		/// <summary>
		/// 导入成功之后更新商品ID
		/// </summary>
		/// <param name="shopProductsID">店铺商品表标识</param>
		/// <param name="productsID">系统商品表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateProductsID(int shopProductsID, int productsID, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().UpdateProductsID(shopProductsID, productsID, context);
		}

		#endregion

		#region 导入失败之后更新错误提示消息

		/// <summary>
		/// 导入失败之后更新错误提示消息
		/// </summary>
		/// <param name="shopProductsID">店铺商品表标识</param>
		/// <param name="errorMessage">错误提示消息</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateErrorMessage(int shopProductsID, string errorMessage, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().UpdateErrorMessage(shopProductsID, errorMessage, context);
		}

		#endregion

		#region 根据系统商品表标识，删除对应的网店下载商品

		/// <summary>
		/// 根据系统商品表标识，删除对应的网店下载商品
		/// </summary>
		/// <param name="productsID">系统商品表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DelByProductsID(int productsID, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().DelByProductsID(productsID, context);
		}

		#endregion

		#region 删除店铺商品

		/// <summary>
		/// 删除店铺商品
		/// </summary>
		/// <param name="shopProductsID">要删除的店铺商品表ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Del(int shopProductsID, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().Del(shopProductsID, context);
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
			return ShopProductsRepository.GetInstance().shopProductscount(shopid, context);
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
		public static List<ShopProducts> getshopProductslist(int shopID, int platformType, string OuterId , IDbContext context = null) {
			return ShopProductsRepository.GetInstance().getshopProductslist(shopID, platformType,  OuterId , context);
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
		public static List<ShopProducts> getshopProductslist(int shopID, int platformType, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().getshopProductslist(shopID, platformType, context);
		}

		#endregion

		#region 删除

	/// <summary>
		/// 删除
	/// </summary>
	/// <param name="shopID"></param>
	/// <param name="platformType"></param>
	/// <param name="context"></param>
	/// <returns></returns>
		public static int Del(int shopID, int platformType, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().Del(shopID, platformType, context);
		}

		#endregion

		#region 删除  根据shopid
		/// <summary>
		///   根据shopid
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int DelbyshopID(int shopID, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().DelbyshopID(shopID, context);
		}
		#endregion


		#region 删除 根据店铺id  商品编码
		/// <summary>
		/// 删除 根据店铺id  商品编码
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="ProductsCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static  int DelshopStockUpdateSinge(int shopID, string ProductsCode, IDbContext context = null) {

			return ShopProductsRepository.GetInstance().DelshopStockUpdateSinge(shopID,ProductsCode, context);
		
		}
		#endregion


		
	}
}