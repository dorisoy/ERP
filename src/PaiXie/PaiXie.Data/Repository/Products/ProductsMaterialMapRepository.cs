using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
	public class ProductsMaterialMapRepository : BaseRepository<ProductsMaterialMap> {

		#region 构造函数
		private static ProductsMaterialMapRepository _instance;
		public static ProductsMaterialMapRepository GetInstance() {
			if (_instance == null) {
				_instance = new ProductsMaterialMapRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(ProductsMaterialMap entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int id = context.Insert<ProductsMaterialMap>("productsMaterialMap", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return id;
		}
		#endregion

		#region Update
		public int Update(ProductsMaterialMap entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<ProductsMaterialMap>("productsMaterialMap", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		#region 根据主键ID删除

		/// <summary>
		/// 根据主键ID删除
		/// </summary>
		/// <param name="productsMaterialMapID">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Del(int productsMaterialMapID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsMaterialMapID;
			string sqlStr = @"Delete FROM productsMaterialMap WHERE ID = @0";
			int count = Del(sqlStr, context, objects);
			return count;
		}

		#endregion

		#region 根据物料关联表标识获取物料关联信息

		/// <summary>
		/// 根据物料关联表标识获取物料关联信息
		/// </summary>
		/// <param name="productsMaterialMapID">物料关联表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public ProductsMaterialMap GetSingleProductsMaterialMap(int productsMaterialMapID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsMaterialMapID;
			string sqlStr = @"SELECT * FROM productsMaterialMap WHERE ID = @0";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 判断是否存在

		/// <summary>
		/// 判断是否存在
		/// </summary>
		/// <param name="sourceProductsSkuID">要关联物料的SKUID</param>
		/// <param name="fromProductsSkuID">物料SKUID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public bool IsExists(int sourceProductsSkuID, int fromProductsSkuID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = sourceProductsSkuID;
			objects[1] = fromProductsSkuID;
			string sqlStr = @"SELECT Count(0) FROM productsMaterialMap WHERE SourceProductsSkuID = @0 and FromProductsSkuID = @1";
			int count = GetCount(sqlStr, context, objects);
			return count > 0;
		}

		#endregion
	}
}





