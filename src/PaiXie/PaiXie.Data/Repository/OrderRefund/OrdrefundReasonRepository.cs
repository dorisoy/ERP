using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class OrdrefundReasonRepository:BaseRepository<OrdrefundReason> {

        #region 构造函数
     
	    private static OrdrefundReasonRepository _instance;
	    public static OrdrefundReasonRepository GetInstance() {
            if (_instance == null) {
                _instance = new OrdrefundReasonRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(OrdrefundReason entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<OrdrefundReason>("ord_refundReason", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(OrdrefundReason entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<OrdrefundReason>("ord_refundReason", entity)
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
		public virtual OrdrefundReason GetQuerySingleByID(int id, IDbContext context = null) {
				if (context == null) context = Db.GetInstance().Context();
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM ord_refundReason WHERE ID=@0";
			OrdrefundReason obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM ord_refundReason WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion
     
	}
}




