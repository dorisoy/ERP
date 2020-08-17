using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class SendShopRepository:BaseRepository<SendShop> {

        #region 构造函数
     
	    private static SendShopRepository _instance;
	    public static SendShopRepository GetInstance() {
            if (_instance == null) {
                _instance = new SendShopRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(SendShop entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<SendShop>("sendShop", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(SendShop entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<SendShop>("sendShop", entity)
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
		public virtual SendShop GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM sendShop WHERE ID=@0";
			SendShop obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}
        
		#endregion

		#region 获取单个实体 通过系统订单号

		/// <summary>
		/// 获取单个实体 通过系统订单号
	    /// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
		public virtual SendShop GetQuerySingleByErpOrderCode(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = "SELECT * FROM sendShop WHERE ErpOrderCode=@0";
			SendShop obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM sendShop WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion
     
	}
}





