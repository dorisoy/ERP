using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Utils;
using PaiXie.Core;
namespace PaiXie.Data 
{
    public	class WarehouseOutInStockItemRepository:BaseRepository<WarehouseOutInStockItem> {

        #region 构造函数
     
	    private static WarehouseOutInStockItemRepository _instance;
	    public static WarehouseOutInStockItemRepository GetInstance() {
            if (_instance == null) {
                _instance = new WarehouseOutInStockItemRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(WarehouseOutInStockItem entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<WarehouseOutInStockItem>("warehouseOutInStockItem", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(WarehouseOutInStockItem entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<WarehouseOutInStockItem>("warehouseOutInStockItem", entity)
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
		public virtual WarehouseOutInStockItem GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseOutInStockItem WHERE ID=@0";
			WarehouseOutInStockItem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}
        
		#endregion
        
        #region 删除操作  通过ID
        
        /// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int DelByID(int ID, IDbContext context = null) {
            Object[] objects = new Object[1];
			objects[0] = ID;
			string sqlStr = "DELETE FROM warehouseOutInStockItem WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 根据出入库单ID删除所有商品

		/// <summary>
		/// 根据出入库单ID删除所有商品
		/// </summary>
		/// <param name="outInStockID">出入库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DeleteByOutInStockID(int outInStockID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = outInStockID;
			string sqlStr = "DELETE FROM warehouseOutInStockItem WHERE OutInStockID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据出入库单ID获取所有商品

		/// <summary>
		/// 根据出入库单ID获取所有商品
		/// </summary>
		/// <param name="outInStockID">出入库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehouseOutInStockItem> GetWarehouseOutInStockItemList(int outInStockID, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = outInStockID;
			string sqlStr = "SELECT * FROM warehouseOutInStockItem WHERE OutInStockID=@0";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据出入库单商品表主键ID删除商品

		/// <summary>
		/// 根据出入库单商品表主键ID删除商品
		/// </summary>
		/// <param name="outInStockItemIDList">出入库单商品表主键ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int Delete(List<int> outInStockItemIDList, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = string.Join(",", outInStockItemIDList.ToArray());
			objects[1] = (int)WarehouseOutInStockStatus.未提交;
			string sqlStr = @"DELETE FROM warehouseOutInStockItem WHERE FIND_IN_SET(ID,@0) AND Status=@1";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据出入库单商品表主键ID获取商品

		/// <summary>
		/// 根据出入库单商品表主键ID获取商品
		/// </summary>
		/// <param name="outInStockItemIDList">出入库单商品表主键ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehouseOutInStockItem> GetWarehouseOutInStockItemList(List<int> outInStockItemIDList, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = string.Join(",", outInStockItemIDList.ToArray());
			string sqlStr = @"SELECT * FROM warehouseOutInStockItem WHERE FIND_IN_SET(ID,@0)";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据出入库单ID获取商品数量

		/// <summary>
		/// 根据出入库单ID获取商品数量
		/// </summary>
		/// <param name="outInStockID">出入库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int GetProductsNumByOutInStockID(int outInStockID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = outInStockID;
			string sqlStr = "SELECT SUM(ProductsNum) FROM warehouseOutInStockItem WHERE OutInStockID=@0";
			string productsNum = Getobject(sqlStr, context, objects);
			return ZConvert.StrToInt(productsNum);
		}

		#endregion

		#region 根据出入库单ID、商品SKUID、批次ID 获取单个实体

		/// <summary>
		/// 根据出入库单ID、商品SKUID、批次ID 获取单个实体
		/// </summary>
		/// <param name="outInStockID">出入库单ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="productsBatchID">批次ID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual WarehouseOutInStockItem GetSingleWarehouseOutInStockItem(int outInStockID, int productsSkuID, int productsBatchID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = outInStockID;
			objects[1] = productsSkuID;
			objects[2] = productsBatchID;
			string sqlStr = "SELECT * FROM warehouseOutInStockItem WHERE OutInStockID=@0 AND ProductsSkuID=@1 AND ProductsBatchID=@2";
			WarehouseOutInStockItem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 根据出入库单ID、商品SKUID、库位ID、批次ID 获取单个实体

		/// <summary>
		/// 根据出入库单ID、商品SKUID、库位ID、批次ID 获取单个实体
		/// </summary>
		/// <param name="outInStockID">出入库单ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">批次ID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual WarehouseOutInStockItem GetSingleWarehouseOutInStockItem(int outInStockID, int productsSkuID, int locationID, int productsBatchID, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = outInStockID;
			objects[1] = productsSkuID;
			objects[2] = locationID;
			objects[3] = productsBatchID;
			string sqlStr = "SELECT * FROM warehouseOutInStockItem WHERE OutInStockID=@0 AND ProductsSkuID=@1 AND LocationID=@2 AND ProductsBatchID=@3";
			WarehouseOutInStockItem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 根据出入库单ID、商品SKUID 获取单个实体

		/// <summary>
		/// 根据出入库单ID、商品SKUID 获取单个实体
		/// </summary>
		/// <param name="outInStockID">出入库单ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual WarehouseOutInStockItem GetSingleWarehouseOutInStockItem(int outInStockID, int productsSkuID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = outInStockID;
			objects[1] = productsSkuID;
			string sqlStr = "SELECT * FROM warehouseOutInStockItem WHERE OutInStockID=@0 AND ProductsSkuID=@1";
			WarehouseOutInStockItem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region  根据出入库单明细ID更新出入库数量

		/// <summary>
		/// 根据出入库单明细ID更新出入库数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">出入库单明细ID</param>
		/// <param name="diffNum">要更新数量 差量更新可正可负</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateProductsNum(string userCode, int id, int diffNum, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = id;
			objects[1] = diffNum;
			objects[2] = (int)WarehouseOutInStockStatus.未提交;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutInStockItem SET ProductsNum=ProductsNum+(@1),UpdatePerson=@3,UpdateDate=@4 WHERE ID=@0 AND Status=@2 AND ProductsNum+(@1)>0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 将某一状态的出入库单明细更新为另外一个状态

		/// <summary>
		/// 将某一状态的出入库单明细更新为另外一个状态
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">出入库单明细主键ID</param>
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
			string sqlStr = @"UPDATE warehouseOutInStockItem SET Status=@2,UpdatePerson=@3,UpdateDate=@4,ConfirmDate=@4 WHERE ID=@0 AND Status=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region  根据单号获取 入库单 入库数

		public string GetSumProductsNum(string BillNo, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = BillNo;
			string sqlStr = @"SELECT  SUM(ProductsNum) FROM warehouseOutInStockItem WHERE OutInStockBillNo=@0";
			return Getobject(sqlStr, context, objects);
		}

		#endregion

		#region  根据单号获取入库单 总价格

		public string GetSumPrice(string BillNo, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = BillNo;
			string sqlStr = @"SELECT  SUM( CostPrice*ProductsNum ) FROM warehouseOutInStockItem WHERE OutInStockBillNo=@0";
			return Getobject(sqlStr, context, objects);
		}

		#endregion

	   #region  更新财务审核状态

		public int  UpdatecwshenheStatus(int newstatus,int oldstatus,int outinstockid,  IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = newstatus;
			objects[1] = oldstatus;
			objects[2] = outinstockid;
			int result = context.Sql("UPDATE  warehouseOutInStockItem SET STATUS=@0, IsAuditPrice=1 WHERE outinstockid=@2 and  STATUS=@1 and  IsAuditPrice=0", objects).Execute();				
			return result;
		}

		#endregion

	   #region  已经入库数量  其他库位的入库数量
		/// <summary>
		/// 已经入库数量  其他库位的入库数量
		/// </summary>
		/// <param name="ProductsSkuID"></param>
		/// <param name="OutInStockID"></param>
		/// <param name="LocationID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public string  GetOtherProductsNum(int ProductsSkuID, int OutInStockID, int LocationID, IDbContext context = null) {

			Object[] objects = new Object[3];
			objects[0] = ProductsSkuID;
			objects[1] = OutInStockID;
			objects[2] = LocationID;
			return  Getobject("SELECT  SUM(ProductsNum)  FROM  warehouseOutInStockItem WHERE ProductsSkuID=@0 AND OutInStockID=@1 AND LocationID!=@2",context,objects);
			
		}

		#endregion

	   #region  根据 sku吗  库位id   出入库单号id  获取实体
		/// <summary>
		/// 根据 sku吗  库位id   出入库单号id  获取实体
		/// </summary>
		/// <param name="productsSkuCode"></param>
		/// <param name="LocationID"></param>
		/// <param name="OutInStockID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public WarehouseOutInStockItem GetWarehouseOutInStockItem(string productsSkuCode, int LocationID, int OutInStockID, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[3];
			objects[0] = productsSkuCode;
			objects[1] = LocationID;
			objects[2] = OutInStockID;
			return GetQuerySingle("SELECT  * FROM   warehouseOutInStockItem WHERE  ProductsSkuCode=@0 AND  LocationID=@1  AND OutInStockID= @2", context, objects);
		
				
		}

		#endregion

	   #region  更新时间
		/// <summary>
		/// 更新时间
		/// </summary>
		/// <param name="scdate">时间</param>
		/// <param name="ids">id 列表</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public int updateProductionDate(string  scdate, string ids, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = scdate;
			objects[1] = ids;
			return Update("UPDATE    warehouseOutInStockItem  SET  ProductionDate=@0 WHERE FIND_IN_SET(id, @1)", context, objects);	
			
		}

		#endregion

		#region  更新库位id
	/// <summary>
		/// 更新库位id
	/// </summary>
	/// <param name="Locationid">库位id</param>
	/// <param name="ids">id 列表</param>
	/// <param name="context"></param>
	/// <returns></returns>
		public int updateLocationID(int Locationid, string ids, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = Locationid;
			objects[1] = ids;
			return Update("UPDATE    warehouseOutInStockItem  SET  LocationID=@0 WHERE FIND_IN_SET(id, @1)", context, objects);

		}

		#endregion

		#region  更新批次号  确认状态

		public int updateproductsBatch(int productsBatchID, string billNo, int id, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = productsBatchID;
			objects[1] = billNo;
			objects[2] = id;
			return context.Sql("UPDATE    warehouseOutInStockItem  SET ProductsBatchID=@0 , ProductsBatchCode=@1, ConfirmDate=NOW(), STATUS=" + (int)WarehouseOutInStockStatus.待审核 + " WHERE id =@2  and   STATUS=" + (int)WarehouseOutInStockStatus.未提交, objects).Execute();
				

		}

		#endregion

		#region  获取总数量
		/// <summary>
		/// 获取总数量
		/// </summary>
		/// <param name="SourceItemID">来源明细id </param>
		/// <param name="context"></param>
		/// <returns></returns>
		public string ProductsNum(int SourceItemID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = SourceItemID;
			return Getobject("SELECT  SUM(ProductsNum)  FROM  warehouseOutInStockItem  WHERE SourceItemID=@0", context, objects);				
		}
		#endregion

		#region  更新采购商品数量
	/// <summary>
		/// 更新采购商品数量
	/// </summary>
	/// <param name="InStockNum"></param>
	/// <param name="id"></param>
	/// <param name="context"></param>
	/// <returns></returns>
		public int updateInStockNum(int InStockNum, int id, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = InStockNum;
			objects[1] = id;
			string strsql = "UPDATE   warehousePurchaseItem SET  InStockNum = @0 WHERE id=@1";
			int result = context.Sql(strsql, objects).Execute();
			return result;
		}
		#endregion
		}
}