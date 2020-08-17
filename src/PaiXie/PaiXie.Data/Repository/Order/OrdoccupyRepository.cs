using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class OrdoccupyRepository:BaseRepository<Ordoccupy> {

        #region 构造函数
     
	    private static OrdoccupyRepository _instance;
	    public static OrdoccupyRepository GetInstance() {
            if (_instance == null) {
                _instance = new OrdoccupyRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(Ordoccupy entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<Ordoccupy>("ord_occupy", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(Ordoccupy entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<Ordoccupy>("ord_occupy", entity)
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
		public virtual Ordoccupy GetQuerySingleByID(int id, IDbContext context = null) {
				if (context == null) context = Db.GetInstance().Context();
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM ord_occupy WHERE ID=@0";
			Ordoccupy obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM ord_occupy WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 获取单个实体

		/// <summary>
		/// 获取单个实体
		/// </summary>
		/// <param name="id">系统订单明细表主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual Ordoccupy GetSingleOrdoccupy(int ordItemID, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = ordItemID;
			string sqlStr = "SELECT * FROM ord_occupy WHERE OrdItemID = @0";
			Ordoccupy obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 删除占用

		/// <summary>
		///删除占用
		/// </summary>
		/// <param name="id">系统订单明细表主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int Delete(int orditemID, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = orditemID;
			string sqlStr = "DELETE FROM ord_occupy WHERE OrditemID = @0";
			return Del(sqlStr, context, objects);
		}

		/// <summary>
		/// 根据订单号删除占用
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int DeleteByErpOrderCode(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = "DELETE FROM ord_occupy WHERE ErpOrderCode = @0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据订单明细主键ID更新订单占用数量

		/// <summary>
		/// 根据订单明细主键ID更新订单占用数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="ordItemID">订单明细主键ID</param>
		/// <param name="num">数量 正数增加，负数扣减</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateNum(string userCode, int ordItemID, int num, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = ordItemID;
			objects[1] = num;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE ord_occupy SET Num=Num+@1,UpdatePerson=@2,UpdateDate=@3 WHERE OrditemID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion
	}
}





