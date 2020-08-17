using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
 	public class WarehouseBookingProductsSkuService  : BaseService<WarehouseBookingProductsSku> {

		public static int Update(WarehouseBookingProductsSku entity, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().Update(entity, context);
		}

		public static int Add(WarehouseBookingProductsSku entity, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// 取消预售商品
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static int Delete(string warehouseCode, int productsID, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().Delete(warehouseCode, productsID, context);
		}

		/// <summary>
		/// 根据商品ID返回SKU预售列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static List<WarehouseBookingProductsSku> GetManyWarehouseBookingProductsSku(string warehouseCode, int productsID, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().GetManyWarehouseBookingProductsSku(warehouseCode, productsID, context);
		}

		/// <summary>
		/// 获取预售信息
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="productsSkuID">SkuId</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static WarehouseBookingProductsSku GetSingleWarehouseBookingProductsSku(string warehouseCode, int productsID, int productsSkuID, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().GetSingleWarehouseBookingProductsSku(warehouseCode, productsID, productsSkuID, context);
		}

		/// <summary>
		/// 获取预售商品分页列表
		/// </summary>
		/// <param name="data">sql语句实体类</param>
		/// <param name="count">总条数</param>
		/// <returns></returns>
		public static List<WarehouseBookingProductsList> GetQueryManyForPageList(SelectBuilder data, out int count) {
			BaseRepository<WarehouseBookingProductsList> obj = new BaseRepository<WarehouseBookingProductsList>();
			return obj.GetQueryManyForPage(data, out  count);
		}

		/// <summary>
		/// 根据商品ID返回SKU预售列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehouseBookingProductsSkuInfo> GetManyWarehouseBookingProductsSkuInfo(string warehouseCode, int productsID, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().GetManyWarehouseBookingProductsSkuInfo(warehouseCode, productsID, context);
		}

		/// <summary>
		/// 获取商品预售信息
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="productsID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static WarehouseBookingProductsList GetSingleWarehouseBookingProducts(string warehouseCode, int productsID, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().GetSingleWarehouseBookingProducts(warehouseCode, productsID, context);
		}

		#region 扣减预售占用

		/// <summary>
		/// 扣减预售占用
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="num">占用数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DeductionZyNum(string userCode, string warehouseCode, int productsSkuID, int num, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().DeductionZyNum(userCode, warehouseCode, productsSkuID, num, context);
		}

		#endregion

		#region 增加冲抵数量

		/// <summary>
		/// 增加冲抵数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="num">冲抵数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int IncreaseCdNum(string userCode, string warehouseCode, int productsSkuID, int num, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().IncreaseCdNum(userCode, warehouseCode, productsSkuID, num, context);
		}

		#endregion

		#region 增加预售占用

		/// <summary>
		/// 增加预售占用
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="num">占用数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int IncreaseZyNum(string userCode, string warehouseCode, int productsSkuID, int num, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().IncreaseZyNum(userCode, warehouseCode, productsSkuID, num, context);
		}

		#endregion
	}
}





