using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class OrdaccountsBillRepository:BaseRepository<OrdaccountsBill> {

        #region 构造函数
     
	    private static OrdaccountsBillRepository _instance;
	    public static OrdaccountsBillRepository GetInstance() {
            if (_instance == null) {
                _instance = new OrdaccountsBillRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(OrdaccountsBill entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<OrdaccountsBill>("ord_accountsBill", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(OrdaccountsBill entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<OrdaccountsBill>("ord_accountsBill", entity)
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
		public virtual OrdaccountsBill GetQuerySingleByID(int id, IDbContext context = null) {
				if (context == null) context = Db.GetInstance().Context();
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM ord_accountsBill WHERE ID=@0";
			OrdaccountsBill obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM ord_accountsBill WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 获取实体列表

		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<OrdaccountsBill> GetManyOrdaccountsBill(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = "SELECT * FROM ord_accountsBill WHERE ErpOrderCode = @0 ORDER BY ID DESC";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 获取单个实体

		/// <summary>
		/// 根据单据号获取单个实体
		/// </summary>
		/// <param name="billNo"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual OrdaccountsBill GetSingleByBillNo(string billNo, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = billNo;
			string sqlStr = "SELECT * FROM ord_accountsBill WHERE BillNo = @0";
			OrdaccountsBill obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion
	}
}





