using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class OrdouterRepository:BaseRepository<Ordouter> {

        #region 构造函数
     
	    private static OrdouterRepository _instance;
	    public static OrdouterRepository GetInstance() {
            if (_instance == null) {
                _instance = new OrdouterRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(Ordouter entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<Ordouter>("ord_outer", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(Ordouter entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<Ordouter>("ord_outer", entity)
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
		public virtual Ordouter GetQuerySingleByID(int id, IDbContext context = null) {
				if (context == null) context = Db.GetInstance().Context();
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM ord_outer WHERE ID=@0";
			Ordouter obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM ord_outer WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 获取订单数量

		/// <summary>
		/// 获取订单数量
		/// </summary>
		/// <param name="outOrderCode">外部订单号</param>
		/// <param name="shopID">店铺ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int GetCount(string outOrderCode,int shopID,IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = outOrderCode;
			objects[1] = shopID;
			string sqlStr = "SELECT 1 FROM ord_outer WHERE outOrderCode = @0 AND ShopID = @1";
			return GetCount(sqlStr, context, objects);
		}

		#endregion

		#region 获取TOP数量的实体列表

		/// <summary>
		/// 获取TOP数量的实体列表（自动生成时用）
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="topNum"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual List<Ordouter> GetManyOrdouterByTop(int shopID, int topNum, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = shopID;
			objects[1] = topNum;
			string sqlStr = "SELECT * FROM ord_outer WHERE ShopID = @0 AND GenerateState = 0 AND (SELECT MAX(1) FROM ord_outerItem WHERE OrdouterID = ord_outer.ID AND IsProductAddFin <> 0) > 0 limit 0,@1";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据系统订单号获取单个实体

		/// <summary>
		/// 根据系统订单号获取单个实体
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual Ordouter GetQuerySingleByErpOrderCode(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = "SELECT * FROM ord_outer WHERE ErpOrderCode = @0";
			Ordouter obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion
	}
}





