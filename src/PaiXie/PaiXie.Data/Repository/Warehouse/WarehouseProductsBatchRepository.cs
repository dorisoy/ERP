using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class WarehouseProductsBatchRepository : BaseRepository<WarehouseProductsBatch> {

		#region 构造函数

		private static WarehouseProductsBatchRepository _instance;
		public static WarehouseProductsBatchRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehouseProductsBatchRepository();
			}
			return _instance;
		}

		#endregion

		#region Add

		public int Add(WarehouseProductsBatch entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehouseProductsBatch>("warehouseProductsBatch", entity)
					.AutoMap(x => x.ID)
					.ExecuteReturnLastId<int>();
			return Id;
		}

		#endregion

		#region Update
		public int Update(WarehouseProductsBatch entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehouseProductsBatch>("warehouseProductsBatch", entity)
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
		public virtual WarehouseProductsBatch GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseProductsBatch WHERE ID=@0";
			WarehouseProductsBatch obj = GetQuerySingle(sqlStr, context, objects);
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
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Sql("DELETE  FROM  warehouseProductsBatch   WHERE ID=" + ID)
					.Execute();
			return rowsAffected;
		}

		#endregion

		#region SkuID获取最新批次实体

		/// <summary>
		/// 根据SkuID获取最新批次实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public WarehouseProductsBatch GetLatestWarehouseProductsBatch(string warehouseCode, int productsSkuID, IDbContext context = null) {
			string sqlStr = @"SELECT *
                              FROM warehouseProductsBatch
                              WHERE WarehouseCode = @0 AND ProductsSkuID = @1 ORDER BY ProductionDate,ID DESC";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = productsSkuID;
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 更新批次商品冻结数量和可用数量 (冻结加，可用减)

		/// <summary>
		/// 更新批次商品冻结数量和可用数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="djNum">冻结数量 差量更新可正可负</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateDjNumAndKyNum(string userCode, int productsBatchID, int djNum, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = productsBatchID;
			objects[1] = djNum;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseProductsBatch SET KyNum=KyNum-(@1),DjNum=DjNum+@1,UpdatePerson=@2,UpdateDate=@3 WHERE ID=@0 AND KyNum-(@1)>=0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新批次商品冻结和在库 (冻结减，在库减)

		/// <summary>
		/// 更新批次商品冻结和在库 (冻结减，在库减)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="outNum">出库数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateDjNumAndZkNum(string userCode, int productsBatchID, int outNum, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = productsBatchID;
			objects[1] = outNum;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseProductsBatch SET ZkNum=ZkNum-(@1),DjNum=DjNum-(@1),UpdatePerson=@2,UpdateDate=@3 WHERE ID=@0 AND DjNum-(@1)>=0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新批次商品占用、可用、在库 (发货时调用)

		/// <summary>
		/// 更新批次商品占用、可用、在库 (发货时调用)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="outNum">发货数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateZyNumAndKyNumAndZkNum(string userCode, int productsBatchID, int outNum, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = productsBatchID;
			objects[1] = outNum;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseProductsBatch SET ZkNum=ZkNum-(@1),ZyNum=ZyNum-(@1),KyNum=KyNum-(@1),UpdatePerson=@2,UpdateDate=@3 WHERE ID=@0 AND ZyNum-(@1)>=0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据仓库编码、商品SKUID、批次 获取单个实体

		/// <summary>
		/// 根据仓库编码、商品SKUID、批次 获取单个实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="batchCode">批次</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public WarehouseProductsBatch GetSingleWarehouseProductsBatch(string warehouseCode, int productsSkuID, string batchCode, IDbContext context = null) {
			string sqlStr = @"SELECT *
                              FROM warehouseProductsBatch
                              WHERE WarehouseCode = @0 AND ProductsSkuID = @1 AND BatchCode=@2";
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = productsSkuID;
			objects[2] = batchCode;
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 扣减占用(取消出库单时调用)

		/// <summary>
		/// 扣减占用(取消出库单时调用)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="num">占用数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DeductionZyNum(string userCode, int productsBatchID, int num, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = productsBatchID;
			objects[1] = num;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseProductsBatch SET ZyNum=ZyNum-@1,UpdatePerson=@2,UpdateDate=@3 WHERE ID=@0 AND ZyNum=@1>=0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 增加占用(生成出库单时调用)

		/// <summary>
		/// 增加占用(生成出库单时调用)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="num">占用数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int IncreaseZyNum(string userCode, int productsBatchID, int num, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = productsBatchID;
			objects[1] = num;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseProductsBatch SET ZyNum = ZyNum + @1,UpdatePerson = @2,UpdateDate = @3 WHERE ID = @0 AND ZyNum + @1 <= KyNum";
			return Update(sqlStr, context, objects);
		}

		#endregion
	}
}





