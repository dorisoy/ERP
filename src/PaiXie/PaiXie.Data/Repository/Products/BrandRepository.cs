using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class BrandRepository : BaseRepository<Brand> {

		#region 构造函数
		private static BrandRepository _instance;
		public static BrandRepository GetInstance() {
			if (_instance == null) {
				_instance = new BrandRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(Brand entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<Brand>("brand", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region Update
		public int Update(Brand entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<Brand>("brand", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion
		
		#region 根据品牌名称获取品牌ID

		/// <summary>
		/// 根据品牌名称获取品牌ID
		/// </summary>
		/// <param name="brandName">品牌名称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetBrandID(string brandName, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = brandName;
			string sqlStr = "SELECT ID FROM brand WHERE Name=@0";
			Brand brand = GetQuerySingle(sqlStr, context, objects);
			int brandID = 0;
			if (brand != null) {
				brandID = brand.ID;
			}
			return brandID;
		}

		#endregion

		#region 根据品牌名称获取排除指定品牌ID之外的品牌ID(修改品牌时使用)

		/// <summary>
		/// 根据品牌名称获取排除指定品牌ID之外的品牌ID(修改品牌时使用)
		/// </summary>
		/// <param name="brandName">品牌名称</param>
		/// <param name="exceptBrandID">需要排除品牌ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetBrandID(string brandName, int exceptBrandID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = brandName;
			objects[1] = exceptBrandID;
			string sqlStr = "SELECT ID FROM brand WHERE Name=@0 and ID<>@1";
			Brand brand = GetQuerySingle(sqlStr, context, objects);
			int brandID = 0;
			if (brand != null) {
				brandID = brand.ID;
			}
			return brandID;
		}

		#endregion

		#region 删除品牌

		/// <summary>
		/// 删除品牌
		/// </summary>
		/// <param name="brandID">品牌ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Del(int brandID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = brandID;
			string sqlStr = "DELETE FROM brand Where ID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据品牌ID获取品牌信息

		/// <summary>
		/// 根据品牌ID获取品牌信息
		/// </summary>
		/// <param name="brandID">品牌ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public Brand GetSingleBrand(int brandID, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = brandID;
			string sqlStr = "SELECT * FROM brand Where ID=@0";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 根据仓库编码获取授权品牌信息

		/// <summary>
		/// 根据仓库编码获取授权品牌信息
		/// </summary>
		/// <param name="brandID">品牌ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<Brand> GetManyBrandByWarehouseCode(string warehouseCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = warehouseCode;
			string sqlStr = "SELECT b.ID,b.Name FROM brand b INNER JOIN warehouse w ON FIND_IN_SET(b.ID,  w.Librand) WHERE w.Code = @0";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 获取品牌信息

		/// <summary>
		/// 获取品牌信息
		/// </summary>
		
		public  List<Brand>  GetBrand() {
			
			string sqlStr = "SELECT  *   FROM  brand ORDER BY Seq";
			return GetQueryMany (sqlStr);
		}

		#endregion
	}
}





