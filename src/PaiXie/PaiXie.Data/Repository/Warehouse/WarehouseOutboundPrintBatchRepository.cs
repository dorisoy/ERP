using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class WarehouseOutboundPrintBatchRepository:BaseRepository<WarehouseOutboundPrintBatch> {

        #region 构造函数
     
	    private static WarehouseOutboundPrintBatchRepository _instance;
	    public static WarehouseOutboundPrintBatchRepository GetInstance() {
            if (_instance == null) {
                _instance = new WarehouseOutboundPrintBatchRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(WarehouseOutboundPrintBatch entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<WarehouseOutboundPrintBatch>("warehouseOutboundPrintBatch", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(WarehouseOutboundPrintBatch entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<WarehouseOutboundPrintBatch>("warehouseOutboundPrintBatch", entity)
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
		public virtual WarehouseOutboundPrintBatch GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseOutboundPrintBatch WHERE ID=@0";
			WarehouseOutboundPrintBatch obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM warehouseOutboundPrintBatch WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion
     
		#region 删除操作  通过出库单ID
        
        /// <summary>
		/// 删除操作  通过出库单ID
		/// </summary>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DelByOutboundID(int outboundID, IDbContext context = null) {
            Object[] objects = new Object[1];
			objects[0] = outboundID;
			string sqlStr = "DELETE FROM warehouseOutboundPrintBatch WHERE OutboundID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion
	}
}





