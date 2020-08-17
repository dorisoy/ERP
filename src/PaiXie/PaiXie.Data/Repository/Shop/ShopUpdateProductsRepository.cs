using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class ShopUpdateProductsRepository : BaseRepository<ShopUpdateProducts> {

		#region 构造函数
		private static ShopUpdateProductsRepository _instance;
		public static ShopUpdateProductsRepository GetInstance() {
			if (_instance == null) {
				_instance = new ShopUpdateProductsRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(ShopUpdateProducts entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<ShopUpdateProducts>("ShopUpdateProducts", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region Update
		public int Update(ShopUpdateProducts entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<ShopUpdateProducts>("ShopUpdateProducts", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		#region 删除店铺下载商品
	/// <summary>
		/// 删除店铺下载商品
	/// </summary>
	/// <param name="ShopID"></param>
	/// <param name="context"></param>
	/// <returns></returns>
		public int Del(int ShopID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ShopID;
			string sqlStr = "DELETE FROM shopUpdateproducts Where ShopID=@0";
			return Del(sqlStr, context, objects);
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

		public List<ShopUpdateProducts> getshopProductslist(int shopID, int platformType, string OuterId, IDbContext context = null) {

			Object[] objects = new Object[3];
			objects[0] = shopID;
			objects[1] = platformType;
			objects[2] = OuterId;
			return GetQueryMany("SELECT  *  FROM  ShopUpdateProducts WHERE   PlatformType=@1 AND shopid=@0  and  OuterId=@2", context, objects);

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
		public List<ShopUpdateProducts> getshopProductslist(int shopID, int platformType, IDbContext context = null) {

			Object[] objects = new Object[2];
			objects[0] = shopID;
			objects[1] = platformType;

			return GetQueryMany("SELECT  *  FROM  ShopUpdateProducts WHERE   PlatformType=@1 AND shopid=@0", context, objects);


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
			return Getobject("SELECT  COUNT(0) FROM  shopUpdateproducts WHERE shopid=@0", context, objects);

		}

		#endregion
	}
}