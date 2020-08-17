using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class WarehouseConversionLogRepository:BaseRepository<WarehouseConversionLog> {

        #region 构造函数
     
	    private static WarehouseConversionLogRepository _instance;
	    public static WarehouseConversionLogRepository GetInstance() {
            if (_instance == null) {
                _instance = new WarehouseConversionLogRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(WarehouseConversionLog entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<WarehouseConversionLog>("warehouseConversionLog", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
	    public int Update(WarehouseConversionLog entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<WarehouseConversionLog>("warehouseConversionLog", entity)
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
		public virtual WarehouseConversionLog GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM WarehouseConversionLog WHERE ID=@0";
			WarehouseConversionLog obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}
        
		#endregion
        
             #region 删除操作  通过ID
        /// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int DelByID(int ID, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Sql("DELETE  FROM  warehouseConversionLog   WHERE ID=" + ID)
					.Execute();
			return rowsAffected;
		}

	 #endregion
     
	}
}





