using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class ShopExpressSetRepository:BaseRepository<ShopExpressSet> {

        #region 构造函数
     
	    private static ShopExpressSetRepository _instance;
	    public static ShopExpressSetRepository GetInstance() {
            if (_instance == null) {
                _instance = new ShopExpressSetRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(ShopExpressSet entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<ShopExpressSet>("shopExpressSet", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(ShopExpressSet entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<ShopExpressSet>("shopExpressSet", entity)
                    .AutoMap(x => x.ID)
        		    .Where(x => x.ID)
        		    .Execute();
		    return rowsAffected;
	    }
        
	    #endregion

        #region 获取单个实体 通过主键ID
        
	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual ShopExpressSet GetQuerySingleByID(int id, IDbContext context = null) {
				if (context == null) context = Db.GetInstance().Context();
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM shopExpressSet WHERE ID=@0";
			ShopExpressSet obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}
        
		#endregion
        
        #region 删除操作  通过ID
        
        /// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DelByID(int id, IDbContext context = null) {
          	if (context == null) context = Db.GetInstance().Context();
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "DELETE FROM shopExpressSet WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 获取实体列表

		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<ShopExpressSet> GetManyShopExpressSet(int shopID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = shopID;
			string sqlStr = "SELECT s.* FROM shopExpressSet s INNER JOIN logistics l ON s.LogisticsID = l.ID WHERE l.IsEnable = 1 AND s.ShopID = @0";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据店铺ID删除

		/// <summary>
		/// 根据店铺ID删除
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DelByShopID(int shopID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = shopID;
			string sqlStr = "DELETE FROM shopExpressSet WHERE shopID = @0";
			return Del(sqlStr, context, objects);
		}

		#endregion
	}
}





