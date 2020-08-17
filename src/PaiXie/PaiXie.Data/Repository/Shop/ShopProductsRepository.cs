using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class ShopProductsRepository : BaseRepository<ShopProducts> {

		#region 构造函数
		private static ShopProductsRepository _instance;
		public static ShopProductsRepository GetInstance() {
			if (_instance == null) {
				_instance = new ShopProductsRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(ShopProducts entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<ShopProducts>("shopProducts", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region Update
		public int Update(ShopProducts entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<ShopProducts>("shopProducts", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		#region 根据店铺ID和商品编码查询店铺商品信息
		/// <summary>
		/// 根据店铺ID和商品编码查询店铺商品信息
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="outerId">商品货号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public ShopProducts GetSingleShopProductsByOuterId(int shopID, string outerId, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = shopID;
			objects[1] = outerId;
			string sqlStr = @"SELECT * FROM shopProducts where ShopID=@0 and OuterId=@1";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 根据店铺ID和商品goodsId查询店铺商品信息
		/// <summary>
		/// 根据店铺ID和商品goodsId查询商品信息
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="goodsId">goodsId</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public ShopProducts GetSingleShopProductsByGoodsId(int shopID, int goodsId, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = shopID;
			objects[1] = goodsId;
			string sqlStr = @"SELECT * FROM shopProducts where ShopID=@0 and GoodsId=@1";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 根据店铺商品表标识查询店铺商品信息
		/// <summary>
		/// 根据店铺商品表标识查询店铺商品信息
		/// </summary>
		/// <param name="shopProductsID">店铺商品表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public ShopProducts GetSingleShopProducts(int shopProductsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = shopProductsID;
			string sqlStr = @"SELECT * FROM shopProducts where ID=@0";
			return GetQuerySingle(sqlStr, context, objects);
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
		public int UpdateProductsID(int shopProductsID, int productsID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = shopProductsID;
			objects[1] = productsID;
			string sqlStr = "UPDATE shopProducts set ProductsID=@1 Where ID=@0";
			return Update(sqlStr, context, objects);
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
		public int UpdateErrorMessage(int shopProductsID, string errorMessage, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = shopProductsID;
			objects[1] = errorMessage;
			string sqlStr = "UPDATE shopProducts set ErrorMessage=@1 Where ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 删除店铺商品
		/// <summary>
		/// 删除店铺商品
		/// </summary>
		/// <param name="shopProductsID">店铺商品表ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Del(int shopProductsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = shopProductsID;
			string sqlStr = "DELETE FROM shopProducts Where ID=@0 and ProductsID=0";
			return Del(sqlStr, context, objects);
		}
		#endregion

		#region 根据系统商品表标识，删除对应的网店下载商品

		/// <summary>
		/// 根据系统商品表标识，删除对应的网店下载商品
		/// </summary>
		/// <param name="productsID">系统商品表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int DelByProductsID(int productsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsID;
			string sqlStr = "DELETE FROM shopProducts Where ProductsID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据shopid 获取总条数

		 /// <summary>
		/// 根据shopid 获取总条数
	/// </summary>
	/// <param name="shopid"></param>
	/// <param name="context"></param>
	/// <returns></returns>
		public string shopProductscount(int shopid, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = shopid;
			return Getobject("SELECT  COUNT(0) FROM  shopProducts WHERE shopid=@0", context, objects);
          
		}

		#endregion
	
		#region 获取实体列表
		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="platformType"></param>
		/// <param name="OuterId"></param>
		/// <param name="context"></param>
		/// <returns></returns>

		public List<ShopProducts> getshopProductslist(int shopID, int platformType, string OuterId , IDbContext context = null) {

			Object[] objects = new Object[3];
			objects[0] = shopID;
			objects[1] = platformType;
			objects[2] = OuterId;
			return GetQueryMany("SELECT  *  FROM  shopProducts WHERE   PlatformType=@1 AND shopid=@0  and  OuterId=@2", context,objects);
			
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
		public List<ShopProducts> getshopProductslist(int shopID, int platformType , IDbContext context = null) {

			Object[] objects = new Object[2];
			objects[0] = shopID;
			objects[1] = platformType;

			return GetQueryMany("SELECT  *  FROM  shopProducts WHERE   PlatformType=@1 AND shopid=@0",context, objects);

			
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
		public int Del(int shopID, int platformType, IDbContext context = null) {

			Object[] objects = new Object[2];
			objects[0] = shopID;
			objects[1] = platformType;

			return Del("DELETE FROM   shopStockUpdate WHERE   PlatformType=@1 AND shopid=@0", context,objects);						
		}
		#endregion

		#region 删除 根据店铺id
	/// <summary>
	/// 
	/// </summary>
	/// <param name="shopID"></param>
	/// <param name="context"></param>
	/// <returns></returns>
		public int DelbyshopID(int shopID, IDbContext context = null) {

			Object[] objects = new Object[1];
			objects[0] = shopID;
			return Del("DELETE FROM   shopStockUpdate WHERE    shopid=@0", context, objects);

		}
		#endregion


		#region 删除 根据店铺id  商品编码
		/// <summary>
		/// 
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="ProductsCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public int DelshopStockUpdateSinge(int shopID, string ProductsCode, IDbContext context = null) {

			Object[] objects = new Object[2];
			objects[0] = shopID;
			objects[1] = ProductsCode;
			return Del("DELETE FROM   shopStockUpdate WHERE    shopid=@0 and  ProductsCode=@1", context, objects);

		}
		#endregion
	}
}