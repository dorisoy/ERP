using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
namespace PaiXie.Data 
{
    public	class WarehouseStocktakingRepository:BaseRepository<WarehouseStocktaking> {

        #region 构造函数
     
	    private static WarehouseStocktakingRepository _instance;
	    public static WarehouseStocktakingRepository GetInstance() {
            if (_instance == null) {
                _instance = new WarehouseStocktakingRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(WarehouseStocktaking entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<WarehouseStocktaking>("warehouseStocktaking", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(WarehouseStocktaking entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<WarehouseStocktaking>("warehouseStocktaking", entity)
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
		public virtual WarehouseStocktaking GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseStocktaking WHERE ID=@0";
			WarehouseStocktaking obj = GetQuerySingle(sqlStr, context, objects);
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
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "DELETE FROM warehouseStocktaking WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 获取未确认盘点记录数量

		/// <summary>
		/// 获取未确认盘点记录数量
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int GetUnconfirmedCount(string warehouseCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = warehouseCode;
			string sqlStr = "SELECT * FROM warehouseStocktaking WHERE WarehouseCode = @0 AND Status = " + (int)StocktakingStatus.未确认;
			return GetCount(sqlStr,context,objects);
		}

		#endregion
	}
}





