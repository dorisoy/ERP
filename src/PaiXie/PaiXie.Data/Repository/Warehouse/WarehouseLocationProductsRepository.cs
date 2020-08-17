using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Utils;
using System.Data;
namespace PaiXie.Data 
{
	public class WarehouseLocationProductsRepository : BaseRepository<WarehouseLocationProducts> {

		#region 构造函数
		private static WarehouseLocationProductsRepository _instance;
		public static WarehouseLocationProductsRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehouseLocationProductsRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(WarehouseLocationProducts entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehouseLocationProducts>("warehouseLocationProducts", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region 获取单个实体 通过主键ID
		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual WarehouseLocationProducts GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseLocationProducts WHERE ID=@0";
			WarehouseLocationProducts obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM warehouseLocationProducts WHERE ID=@0";
			int rowsAffected = Del(sqlStr, context, objects);
			return rowsAffected;
		}

		#endregion

		#region 删除操作 通过库位ID

		/// <summary>
		/// 删除操作 通过库位ID
		/// </summary>
		/// <param name="locationID">库位ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DelByLocationID(int locationID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = locationID;
			string sqlStr = "DELETE FROM warehouseLocationProducts WHERE LocationID=@0";
			int rowsAffected = Del(sqlStr, context, objects);
			return rowsAffected;
		}
		#endregion

		#region Update
		public int Update(WarehouseLocationProducts entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehouseLocationProducts>("warehouseLocationProducts", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		#region 获取库位商品数量

		/// <summary>
		/// 获取库位商品数量
		/// </summary>
		/// <param name="locationIDList">库位ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetProductsNum(List<int> locationIDList, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = string.Join(",", locationIDList.ToArray());
			string sqlStr = "SELECT IFNULL(SUM(ZkNum),0) AS ProductsNum FROM warehouseLocationProducts WHERE FIND_IN_SET(LocationID, @0)";
			int productsNum = context.Sql(sqlStr, objects).QuerySingle<int>();
			return productsNum;
		}

		#endregion

		#region 获取库位商品数量实体

		/// <summary>
		/// 获取库位商品数量实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="produtcsID">商品ID</param>
		/// <param name="LocationID">库位ID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public WarehouseLocationProducts GetSingleWarehouseLocationProducts(string warehouseCode, int produtcsID,int LocationID,IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = produtcsID;
			objects[2] = LocationID;
			string sqlStr = @"SELECT IFNULL(SUM(KyNum),0) KyNum,IFNULL(SUM(ZyNum),0) ZyNum,IFNULL(SUM(DjNum),0) DjNum,IFNULL(SUM(SdNum),0) SdNum,IFNULL(SUM(ZkNum),0) ZkNum
			                  FROM warehouseLocationProducts WHERE WarehouseCode = @0 AND ProductsID = @1 AND LocationID = @2";
			WarehouseLocationProducts obj = GetQuerySingle(sqlStr, context, objects);
			return obj;	
		}

		#endregion

		#region 获取库区商品数量

		/// <summary>
		/// 获取库区商品数量
		/// </summary>
		/// <param name="topLocationID">库区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetProductsNum(int topLocationID, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = topLocationID;
			string sqlStr = "SELECT IFNULL(SUM(ZkNum),0) AS ProductsNum FROM warehouseLocationProducts WHERE TopLocationID=@0";
			int productsNum = context.Sql(sqlStr, objects).QuerySingle<int>();
			return productsNum;
		}

		#endregion

		#region 根据SkuID获取库位商品

		/// <summary>
		/// 根据SkuID获取库位商品
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SkuID</param>
		/// <param name="LocationID">库位ID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public List<WarehouseLocationProducts> GetManyWarehouseLocationProducts(string warehouseCode, int productsSkuID, int LocationID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = LocationID;
			objects[2] = productsSkuID;
			string sqlStr = @"SELECT * FROM warehouseLocationProducts WHERE WarehouseCode = @0 AND LocationID = @1 AND ProductsSkuID = @2 ORDER BY ProductionDate,ID";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据SkuID获取库位商品

		/// <summary>
		/// 根据SkuID获取库位商品
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SkuID</param>
		/// <param name="LocationID">库位ID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public List<LocationProductsKucInfo> GetManyLocationProductsKucInfo(string warehouseCode, int productsSkuID, int LocationID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = LocationID;
			objects[2] = productsSkuID;
			string sqlStr = @"SELECT p.Code ProductsCode, p.Name ProductsName,ps.Saleprop,ps.Code ProductsSkuCode,wlp.ProductsBatchCode,wlp.ZkNum
                              FROM warehouseLocationProducts wlp LEFT JOIN products p ON wlp.ProductsID = p.ID LEFT JOIN productsSku ps ON wlp.ProductsSkuID = ps.ID
                              WHERE wlp.WarehouseCode = @0 AND wlp.LocationID = @1 AND wlp.ProductsSkuID = @2";
			if (context == null) context = Db.GetInstance().Context();
			List<LocationProductsKucInfo> objlist = context.Sql(sqlStr, objects).QueryMany<LocationProductsKucInfo>();
			return objlist;
		}

		#endregion

		#region 更新库位商品冻结数量和可用数量 (冻结加，可用减)

		/// <summary>
		/// 更新库位商品冻结数量和可用数量 (冻结加，可用减)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="djNum">冻结数量 差量更新可正可负</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateDjNumAndKyNum(string userCode, int productsSkuID, int locationID, int productsBatchID, int djNum, IDbContext context = null) {
			Object[] objects = new Object[6];
			objects[0] = productsSkuID;
			objects[1] = locationID;
			objects[2] = productsBatchID;
			objects[3] = djNum;
			objects[4] = userCode;
			objects[5] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseLocationProducts SET KyNum=KyNum-(@3),DjNum=DjNum+@3,UpdatePerson=@4,UpdateDate=@5 WHERE ProductsSkuID=@0 AND LocationID=@1 AND ProductsBatchID=@2 AND KyNum-(@3)>=0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新库位商品冻结和在库 (冻结减，在库减)

		/// <summary>
		/// 更新库位商品冻结和在库 (冻结减，在库减)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="outNum">出库数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateDjNumAndZkNum(string userCode, int productsSkuID, int locationID, int productsBatchID, int outNum, IDbContext context = null) {
			Object[] objects = new Object[6];
			objects[0] = productsSkuID;
			objects[1] = locationID;
			objects[2] = productsBatchID;
			objects[3] = outNum;
			objects[4] = userCode;
			objects[5] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseLocationProducts SET ZkNum=ZkNum-(@3),DjNum=DjNum-(@3),UpdatePerson=@4,UpdateDate=@5 WHERE ProductsSkuID=@0 AND LocationID=@1 AND ProductsBatchID=@2 AND DjNum-(@3)>=0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新库位商品可用和在库 (可用加，在库加)

		/// <summary>
		/// 更新库位商品可用和在库 (可用加，在库加)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="inNum">入库数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateKyNumAndZkNum(string userCode, int productsSkuID, int locationID, int productsBatchID, int inNum, IDbContext context = null) {
			Object[] objects = new Object[6];
			objects[0] = productsSkuID;
			objects[1] = locationID;
			objects[2] = productsBatchID;
			objects[3] = inNum;
			objects[4] = userCode;
			objects[5] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseLocationProducts SET ZkNum=ZkNum+@3,KyNum=KyNum+@3,UpdatePerson=@4,UpdateDate=@5 WHERE ProductsSkuID=@0 AND LocationID=@1 AND ProductsBatchID=@2 AND KyNum+@3>=0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新库位商品占用、可用、在库 (发货时调用)

		/// <summary>
		/// 更新库位商品占用、可用、在库 (发货时调用)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="outNum">发货数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateZyNumAndKyNumAndZkNum(string userCode, int productsSkuID, int locationID, int productsBatchID, int outNum, IDbContext context = null) {
			Object[] objects = new Object[6];
			objects[0] = productsSkuID;
			objects[1] = locationID;
			objects[2] = productsBatchID;
			objects[3] = outNum;
			objects[4] = userCode;
			objects[5] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseLocationProducts SET ZkNum=ZkNum-(@3),KyNum=KyNum-(@3),ZyNum=ZyNum-(@3),UpdatePerson=@4,UpdateDate=@5 WHERE ProductsSkuID=@0 AND LocationID=@1 AND ProductsBatchID=@2 AND ZyNum-(@3)>=0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据库位ID、商品SKUID、批次ID获取库位可发货库存

		/// <summary>
		/// 根据库位ID、商品SKUID、批次ID获取库位可发货库存
		/// </summary>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="productsBatchID">批次ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetKfhNum(int locationID, int productsSkuID, int productsBatchID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0]=locationID;
			objects[1] = productsSkuID;
			objects[2] = productsBatchID;
			string sqlStr = @"SELECT (KyNum-ZyNum) as KfhNum FROM warehouseLocationProducts WHERE LocationID=@0 AND ProductsSkuID=@1 AND ProductsBatchID=@2";
			return ZConvert.StrToInt(Getobject(sqlStr, context, objects));
		}

		#endregion

		#region 根据库位ID、商品SKUID、批次ID获取单个实体

		/// <summary>
		/// 根据库位ID、商品SKUID、批次ID获取单个实体
		/// </summary>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="productsBatchID">批次ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public WarehouseLocationProducts GetSingleWarehouseLocationProducts(int locationID, int productsSkuID, int productsBatchID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = locationID;
			objects[1] = productsSkuID;
			objects[2] = productsBatchID;
			string sqlStr = @"SELECT * FROM warehouseLocationProducts WHERE LocationID=@0 AND ProductsSkuID=@1 AND ProductsBatchID=@2";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 获取 DataTable
		/// <summary>
		/// 
		/// </summary>
		/// <param name="kqid">库区id</param>
		/// <param name="skucode">sku 代码</param>
		/// <param name="WarehouseCode">仓库代码</param>
		/// <param name="context"></param>
		/// <returns></returns>

		public DataTable GetDataTable(int kqid, string skucode, string WarehouseCode, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = kqid;
			objects[1] = skucode;
			objects[2] = WarehouseCode;
			string sqlwhere = "";
			if (kqid != 0) {
				sqlwhere = " TopLocationID=@0 AND ";
			}
			return GetDataTable(@"	SELECT  LocationID ,SUM(ZkNum) AS ZkNum FROM warehouseLocationProducts WHERE " + sqlwhere + " ProductsSkuID=(SELECT id FROM productsSku  WHERE    CODE=@1) AND WarehouseCode=@2    GROUP BY  locationID", context, objects);
			
		}

		#endregion

		#region 扣减库位占用数量

		/// <summary>
		/// 扣减库位占用数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="num">占用数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DeductionZyNum(string userCode, string warehouseCode, int productsSkuID, int locationID, int productsBatchID, int num, IDbContext context = null) {
			Object[] objects = new Object[7];
			objects[0] = warehouseCode;
			objects[1] = productsSkuID;
			objects[2] = locationID;
			objects[3] = productsBatchID;
			objects[4] = num;
			objects[5] = userCode;
			objects[6] = DateTime.Now;
			string sqlStr = @"Update warehouseLocationProducts SET ZyNum=ZyNum-@4,UpdatePerson=@5,UpdateDate=@6 WHERE WarehouseCode=@0 AND ProductsSkuID=@1 AND LocationID=@2 AND ProductsBatchID=@3 AND ZyNum-@4>=0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 增加库位占用数量

		/// <summary>
		/// 增加库位占用数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="num">占用数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int IncreaseZyNum(string userCode, string warehouseCode, int productsSkuID, int locationID, int productsBatchID, int num, IDbContext context = null) {
			Object[] objects = new Object[7];
			objects[0] = warehouseCode;
			objects[1] = productsSkuID;
			objects[2] = locationID;
			objects[3] = productsBatchID;
			objects[4] = num;
			objects[5] = userCode;
			objects[6] = DateTime.Now;
			string sqlStr = @"Update warehouseLocationProducts SET ZyNum=ZyNum + @4,UpdatePerson = @5,UpdateDate = @6 WHERE WarehouseCode = @0 AND ProductsSkuID = @1 AND LocationID = @2 AND ProductsBatchID = @3 AND ZyNum + @4 <= KyNum";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 获取可发货数量大于0的库位商品列表(根据生产日期排序)

		/// <summary>
		/// 获取可发货数量大于0的库位商品列表(根据生产日期排序)
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="produtcsSkuID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public List<WarehouseLocationProducts> GetManyLocationProducts(string warehouseCode, int produtcsSkuID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = produtcsSkuID;
			string sqlStr = @"SELECT * FROM warehouseLocationProducts WHERE WarehouseCode = @0 AND ProductsSkuID = @1 AND KyNum - ZyNum > 0 ORDER BY ProductionDate";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 删除在库数量为0的记录

		/// <summary>
		/// 删除在库数量为0的记录
		/// </summary>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DeleteZeroRecord(IDbContext context = null) {
			string sqlStr = "DELETE FROM warehouseLocationProducts WHERE ZkNum=0";
			int rowsAffected = Del(sqlStr, context);
			return rowsAffected;
		}

		#endregion
	}
}





