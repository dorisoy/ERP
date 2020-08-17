using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class OrdremarkRepository:BaseRepository<Ordremark> {

        #region 构造函数
     
	    private static OrdremarkRepository _instance;
	    public static OrdremarkRepository GetInstance() {
            if (_instance == null) {
                _instance = new OrdremarkRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(Ordremark entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<Ordremark>("ord_remark", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(Ordremark entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<Ordremark>("ord_remark", entity)
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
		public virtual Ordremark GetQuerySingleByID(int id, IDbContext context = null) {
				if (context == null) context = Db.GetInstance().Context();
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM ord_remark WHERE ID=@0";
			Ordremark obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM ord_remark WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 获取实体列表

		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual List<Ordremark> GetManyOrdremark(string erpOrderCode, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = "SELECT * FROM ord_remark WHERE ErpOrderCode = @0 ORDER BY CreateDate DESC";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion
	}
}





