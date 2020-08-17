using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class ShopAutogenerationRepository:BaseRepository<ShopAutogeneration> {

        #region 构造函数
     
	    private static ShopAutogenerationRepository _instance;
	    public static ShopAutogenerationRepository GetInstance() {
            if (_instance == null) {
                _instance = new ShopAutogenerationRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(ShopAutogeneration entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<ShopAutogeneration>("shopAutogeneration", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(ShopAutogeneration entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<ShopAutogeneration>("shopAutogeneration", entity)
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
		public virtual ShopAutogeneration GetQuerySingleByID(int id, IDbContext context = null) {
				if (context == null) context = Db.GetInstance().Context();
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM shopAutogeneration WHERE ID=@0";
			ShopAutogeneration obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM shopAutogeneration WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 获取实体列表

		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="shopID">网店ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<ShopAutogeneration> GetManyShopAutogeneration(IDbContext context = null) {
			string sqlStr = "SELECT a.* FROM shopAutogeneration a INNER JOIN shop s on a.ShopID = s.ID WHERE s.IsEnable = 1";
			return GetQueryMany(sqlStr, context);
		}

		#endregion

		#region 根据店铺ID获取实体

		/// <summary>
		/// 根据店铺ID获取实体
		/// </summary>
		/// <param name="shopID">网店ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual ShopAutogeneration GetSingleShopAutogeneration(int shopID,IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = shopID;
			string sqlStr = "SELECT * FROM shopAutogeneration WHERE ShopID = @0";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion
	}
}





