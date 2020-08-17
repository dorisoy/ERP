using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class WarehouseConfigRepository:BaseRepository<WarehouseConfig> {

        #region 构造函数
     
	    private static WarehouseConfigRepository _instance;
	    public static WarehouseConfigRepository GetInstance() {
            if (_instance == null) {
                _instance = new WarehouseConfigRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(WarehouseConfig entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<WarehouseConfig>("warehouseConfig", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(WarehouseConfig entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<WarehouseConfig>("warehouseConfig", entity)
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
		public virtual WarehouseConfig GetQuerySingleByID(int id, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseConfig WHERE ID=@0";
			WarehouseConfig obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}
        
		#endregion

		#region 获取单个实体 通过仓库编码

	    /// <summary>
	    /// 获取单个实体 通过仓库编码
	    /// </summary>
		/// <param name="warehouseCode">仓库编码</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
		public virtual WarehouseConfig GetQuerySingleByWarehouseCode(string warehouseCode, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = warehouseCode;
			string sqlStr = "SELECT * FROM warehouseConfig WHERE WarehouseCode=@0";
			WarehouseConfig obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM warehouseConfig WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion
     
	}
}





