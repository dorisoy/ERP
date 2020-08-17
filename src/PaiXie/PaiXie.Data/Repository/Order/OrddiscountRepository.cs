using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class OrddiscountRepository:BaseRepository<Orddiscount> {

        #region 构造函数
     
	    private static OrddiscountRepository _instance;
	    public static OrddiscountRepository GetInstance() {
            if (_instance == null) {
                _instance = new OrddiscountRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(Orddiscount entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<Orddiscount>("ord_discount", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(Orddiscount entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<Orddiscount>("ord_discount", entity)
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
		public virtual Orddiscount GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM ord_discount WHERE ID=@0";
			Orddiscount obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM ord_discount WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 获取订单优惠实体列表

		/// <summary>
		/// 获取订单优惠实体列表
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<Orddiscount> GetManyOrddiscount(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = "SELECT * FROM ord_discount WHERE ErpOrderCode = @0";
			return  GetQueryMany(sqlStr, context, objects);
		}

		/// <summary>
		/// 获取订单优惠实体列表
		/// </summary>
		/// <param name="ordbaseID">订单表主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<Orddiscount> GetManyOrddiscount(int ordbaseID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ordbaseID;
			string sqlStr = "SELECT * FROM ord_discount WHERE OrdbaseID = @0";
			return GetQueryMany(sqlStr, context, objects);
		}

		/// <summary>
		/// 获取订单优惠实体列表
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="erpOrderCode">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<Orddiscount> GetManyOrddiscount(string erpOrderCode,int productsSkuID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = erpOrderCode;
			objects[1] = productsSkuID;
			string sqlStr = "SELECT * FROM ord_discount WHERE ErpOrderCode = @0 AND FIND_IN_SET(@1, LibProductsSkuID)";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion
	}
}





