using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class WarehouseMoveLocationRepository:BaseRepository<WarehouseMoveLocation> {

        #region 构造函数
     
	    private static WarehouseMoveLocationRepository _instance;
	    public static WarehouseMoveLocationRepository GetInstance() {
            if (_instance == null) {
                _instance = new WarehouseMoveLocationRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(WarehouseMoveLocation entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<WarehouseMoveLocation>("warehouseMoveLocation", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(WarehouseMoveLocation entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<WarehouseMoveLocation>("warehouseMoveLocation", entity)
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
		public virtual WarehouseMoveLocation GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseMoveLocation WHERE ID=@0";
			WarehouseMoveLocation obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM warehouseMoveLocation WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 修改移位单状态

		/// <summary>
		/// 修改移位单状态
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">移位单主键ID</param>
		/// <param name="oldStatus">旧状态</param>
		/// <param name="newStatus">新状态</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateStatus(string userCode, int id, int oldStatus, int newStatus, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = userCode;
			objects[1] = id;
			objects[2] = oldStatus;
			objects[3] = newStatus;
			objects[4] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseMoveLocation SET Status=@3,UpdatePerson=@0,UpdateDate=@4,ConfirmDate=@4 WHERE ID=@1 AND Status=@2";
			return Update(sqlStr, context, objects);
		}

		#endregion
	}
}





