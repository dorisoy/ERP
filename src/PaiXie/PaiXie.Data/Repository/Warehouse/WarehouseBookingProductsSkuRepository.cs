using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using System.Data;
namespace PaiXie.Data {
	public class WarehouseBookingProductsSkuRepository : BaseRepository<WarehouseBookingProductsSku> {

		#region 构造函数
		private static WarehouseBookingProductsSkuRepository _instance;
		public static WarehouseBookingProductsSkuRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehouseBookingProductsSkuRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(WarehouseBookingProductsSku entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehouseBookingProductsSku>("warehouseBookingProductsSku", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region Update
		public int Update(WarehouseBookingProductsSku entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehouseBookingProductsSku>("warehouseBookingProductsSku", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		/// <summary>
		/// 取消预售商品
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public int Delete(string warehouseCode, int productsID, IDbContext context = null) {
			string sqlStr = @"DELETE FROM warehouseBookingProductsSku
                              WHERE WarehouseCode = @0 AND ProductsID = @1";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = productsID;
			return Del(sqlStr, context, objects);
		}

		/// <summary>
		/// 根据商品ID返回SKU预售列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public List<WarehouseBookingProductsSku> GetManyWarehouseBookingProductsSku(string warehouseCode, int productsID, IDbContext context = null) {
			string sqlStr = @"SELECT *
                              FROM warehouseBookingProductsSku
                              WHERE WarehouseCode = @0 AND ProductsID = @1";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = productsID;
			return GetQueryMany(sqlStr, context, objects);
		}

		/// <summary>
		/// 获取预售信息
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="productsSkuID">SkuId</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public WarehouseBookingProductsSku GetSingleWarehouseBookingProductsSku(string warehouseCode, int productsID, int productsSkuID, IDbContext context = null) {
			string sqlStr = @"SELECT *
                              FROM warehouseBookingProductsSku
                              WHERE WarehouseCode = @0 AND ProductsID = @1 AND ProductsSkuID = @2";
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = productsID;
			objects[2] = productsSkuID;
			return GetQuerySingle(sqlStr, context, objects);
		}

		/// <summary>
		/// 根据商品ID返回SKU预售列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<WarehouseBookingProductsSkuInfo> GetManyWarehouseBookingProductsSkuInfo(string warehouseCode, int productsID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = productsID;
			string sqlStr = @"SELECT ps.*,BookingNum,ZyNum,CdNum,CASE WHEN BookingModel=0 THEN BookingNum-ZyNum-CdNum ELSE BookingNum END KyNum FROM productsSku ps LEFT JOIN warehouseBookingProductsSku wbps ON wbps.ProductsSkuID = ps.ID AND wbps.WarehouseCode = @0 WHERE ps.ProductsID = @1";
			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QueryMany<WarehouseBookingProductsSkuInfo>();
		}

		/// <summary>
		/// 获取商品预售信息
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="productsID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public WarehouseBookingProductsList GetSingleWarehouseBookingProducts(string warehouseCode, int productsID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = productsID;
			string sqlStr = @"SELECT SUM(BookingNum)BookingNum,SUM(ZyNum) ZyNum,SUM(CdNum) CdNum,SUM(CASE WHEN BookingModel=0 THEN BookingNum-ZyNum-CdNum ELSE BookingNum END) KyNum FROM warehouseBookingProductsSku WHERE WarehouseCode = @0 AND ProductsID = @1";
			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QuerySingle<WarehouseBookingProductsList>();
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
		public virtual int DeductionZyNum(string userCode, string warehouseCode, int productsSkuID, int num, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = productsSkuID;
			objects[2] = num;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			string sqlStr = @"Update warehouseBookingProductsSku SET ZyNum=ZyNum-@2,UpdatePerson=@3,UpdateDate=@4 WHERE WarehouseCode=@0 AND ProductsSkuID=@1 AND ZyNum-@2>=0";
			return Update(sqlStr, context, objects);
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
		public virtual int IncreaseCdNum(string userCode, string warehouseCode, int productsSkuID, int num, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = productsSkuID;
			objects[2] = num;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			string sqlStr = @"Update warehouseBookingProductsSku SET CdNum=CdNum+@2,UpdatePerson=@3,UpdateDate=@4 WHERE WarehouseCode=@0 AND ProductsSkuID=@1";
			return Update(sqlStr, context, objects);
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
		public virtual int IncreaseZyNum(string userCode, string warehouseCode, int productsSkuID, int num, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = productsSkuID;
			objects[2] = num;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			string sqlStr = @"Update warehouseBookingProductsSku SET ZyNum = ZyNum + @2,UpdatePerson = @3,UpdateDate = @4 WHERE WarehouseCode = @0 AND ProductsSkuID = @1 AND Case BookingModel When 0 Then ZyNum + @2 <= BookingNum Else 1 = 1 END";
			return Update(sqlStr, context, objects);
		}

		#endregion
	}
}





