using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class SuppliersSettlementRepository:BaseRepository<SuppliersSettlement> {

        #region 构造函数
     
	    private static SuppliersSettlementRepository _instance;
	    public static SuppliersSettlementRepository GetInstance() {
            if (_instance == null) {
                _instance = new SuppliersSettlementRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(SuppliersSettlement entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<SuppliersSettlement>("SuppliersSettlement", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(SuppliersSettlement entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<SuppliersSettlement>("SuppliersSettlement", entity)
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
		public virtual SuppliersSettlement GetQuerySingleByID(int id, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM SuppliersSettlement WHERE ID=@0";
			SuppliersSettlement obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM SuppliersSettlement WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 获取单个实体 通过SourceID

		/// <summary>
		/// 获取单个实体 通过SourceID
		/// </summary>
		/// <param name="id">SourceID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual SuppliersSettlement GetQuerySingleBySourceID(int SourceID, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = SourceID;
			string sqlStr = "SELECT * FROM SuppliersSettlement WHERE SourceID=@0";
			SuppliersSettlement obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion
     
	}
}





