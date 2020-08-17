using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
using PaiXie.Utils;
namespace PaiXie.Data 
{
    public	class WarehouseOutInStockRepository:BaseRepository<WarehouseOutInStock> {

        #region 构造函数
     
	    private static WarehouseOutInStockRepository _instance;
	    public static WarehouseOutInStockRepository GetInstance() {
            if (_instance == null) {
                _instance = new WarehouseOutInStockRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(WarehouseOutInStock entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<WarehouseOutInStock>("warehouseOutInStock", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(WarehouseOutInStock entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<WarehouseOutInStock>("warehouseOutInStock", entity)
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
		public virtual WarehouseOutInStock GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseOutInStock WHERE ID=@0";
			WarehouseOutInStock obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}
        
		#endregion

		#region 获取单个实体 通过出入库单号

		/// <summary>
		/// 获取单个实体 通过出入库单号
	    /// </summary>
		/// <param name="billNo">出入库单号</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
		public virtual WarehouseOutInStock GetQuerySingleByBillNo(string billNo, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = billNo;
			string sqlStr = "SELECT * FROM warehouseOutInStock WHERE BillNo=@0";
			WarehouseOutInStock obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}
		
		#endregion

		#region 删除操作  通过ID  未提交状态才可以删除

		/// <summary>
		/// 删除操作  通过ID 未提交状态才可以删除
		/// </summary>
		/// <param name="ID">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DelByID(int ID, IDbContext context = null) {
            Object[] objects = new Object[2];
			objects[0] = ID;
			objects[1] = (int)WarehouseOutInStockStatus.未提交;
			string sqlStr = "DELETE FROM warehouseOutInStock WHERE ID=@0 AND Status=@1";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 获取主键ID  通过 BillNo

	/// <summary>
		/// 获取主键ID  通过 BillNo
	/// </summary>
	/// <param name="BillNo"></param>
	/// <param name="context"></param>
	/// <returns></returns>
	
		public virtual string GetidByBillNo(string BillNo, IDbContext context = null) {

			string sqlStr = "SELECT id FROM warehouseOutInStock WHERE BillNo='" + BillNo + "'";
			string  obj = Getobject(sqlStr, context);
			return obj;
		}

		#endregion

		#region 将某一状态的出入库单更新为另外一个状态

		/// <summary>
		/// 将某一状态的出入库单更新为另外一个状态
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">出入库单主键ID</param>
		/// <param name="oldStatus">旧状态</param>
		/// <param name="newStatus">新状态</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateStatus(string userCode, int id, int oldStatus, int newStatus, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = id;
			objects[1] = oldStatus;
			objects[2] = newStatus;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutInStock SET ConfirmDate=@4,UpdateDate=@4,UpdatePerson=@3,MainName=@3, Status=@2 WHERE ID=@0 AND Status=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 获取实体  通过 BillNo

		/// <summary>
		/// 获取实体  通过 BillNo
		/// </summary>
		/// <param name="BillNo"></param>
		/// <param name="context"></param>
		/// <returns></returns>

		public virtual WarehouseOutInStock GetModelByBillNo(string BillNo, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = BillNo;
			string sqlStr = "SELECT * FROM warehouseOutInStock WHERE BillNo=@0";
			WarehouseOutInStock obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 获取列表 通过 ids 
		public virtual List<WarehouseOutInStock> Getlistbyids(string ids, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = ids;
			string sqlStr = "SELECT * FROM warehouseOutInStock WHERE FIND_IN_SET(id, @0)";
			List<WarehouseOutInStock>  obj = GetQueryMany(sqlStr, context);
			return obj;
		}

		#endregion

		#region 获取价格
		public virtual string PurchasePrice(string skucode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = skucode;
			return Getobject(" SELECT PurchasePrice FROM  suppliersItem  WHERE ProductsSkuCode=@0  ORDER BY PurchasePrice ASC LIMIT 1 ", context, objects);					
		}
		#endregion

		#region 获取价格
		public virtual string CostPrice(int  ProductsID, IDbContext context = null) {

			Object[] objects = new Object[1];
			objects[0] = ProductsID;
				return Getobject(" SELECT CostPrice  FROM products WHERE  ID = @0",context,objects);							
		}

		#endregion
		
		#region 获取价格
		public virtual string PurchasePrice(string skucode, int SuppliersID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = skucode;
			objects[1] = SuppliersID;
			  return  Getobject(" SELECT PurchasePrice FROM  suppliersItem  WHERE ProductsSkuCode=@0 AND SuppliersID=@1 ORDER BY PurchasePrice ASC LIMIT 1 ",context,objects);			
		}

		#endregion

		#region 入库单状态更新
		public virtual int updatestatus( int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			return context.Sql("UPDATE    warehouseOutInStock  SET ConfirmDate=NOW(),MainName='" + FormsAuth.GetUserCode() + "' ,  STATUS=" + (int)WarehouseOutInStockStatus.待审核 + " WHERE id =@0 and  STATUS=" + (int)WarehouseOutInStockStatus.未提交, objects).Execute();
		}

		#endregion

		#region 入库单数量
		/// <summary>
		/// 入库单数量
		/// </summary>
		/// <param name="sourceid">源id</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int warehouseOutInStockCOUNT(int sourceid, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = sourceid;
			return ZConvert.StrToInt(Getobject("SELECT  COUNT(0)  FROM  warehouseOutInStock  WHERE sourceid=@0", context, objects), 0);			
		}
		#endregion				
	}
}