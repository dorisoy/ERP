using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
namespace PaiXie.Data 
{
	public class WarehousePurchaseItemRepository : BaseRepository<WarehousePurchaseItem> {

		#region 构造函数

		private static WarehousePurchaseItemRepository _instance;
		public static WarehousePurchaseItemRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehousePurchaseItemRepository();
			}
			return _instance;
		}

		#endregion

		#region Add

		public int Add(WarehousePurchaseItem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehousePurchaseItem>("warehousePurchaseItem", entity)
					.AutoMap(x => x.ID)
					.ExecuteReturnLastId<int>();
			return Id;
		}

		#endregion

		#region Update
		public int Update(WarehousePurchaseItem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehousePurchaseItem>("warehousePurchaseItem", entity)
					.AutoMap(x => x.ID)
					.Where(x => x.ID)
					.Execute();
			return rowsAffected;
		}
		#endregion

		#region 根据采购单主键ID和商品SKUID获取单个实体

		/// <summary>
		/// 根据采购单主键ID和商品SKUID获取单个实体
		/// </summary>
		/// <param name="purchaseID">采购单主键ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public WarehousePurchaseItem GetSingleWarehousePurchaseItem(int purchaseID, int productsSkuID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = purchaseID;
			objects[1] = productsSkuID;
			string sqlStr = "SELECT * FROM warehousePurchaseItem WHERE PurchaseID=@0 and ProductsSkuID=@1";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 获取单个实体 通过主键ID

		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual WarehousePurchaseItem GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehousePurchaseItem WHERE ID=@0";
			WarehousePurchaseItem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 根据采购单商品表ID删除记录

		/// <summary>
		/// 根据采购单商品表ID删除记录
		/// </summary>
		/// <param name="purchaseItemIDList">采购单商品表ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Delete(List<int> purchaseItemIDList, IDbContext context = null) {
			string whereSql = whereSql = " AND PurchaseID IN (SELECT ID FROM warehousePurchase WHERE InStockOrderCount=0)";
			string sqlStr = "DELETE FROM warehousePurchaseItem Where FIND_IN_SET(ID, @0)" + whereSql;
			Object[] objects = new Object[1];
			objects[0] = string.Join(",", purchaseItemIDList.ToArray());
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据采购单ID删除记录

		/// <summary>
		/// 根据采购单ID删除记录
		/// </summary>
		/// <param name="purchaseID">采购单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DeleteByPurchaseID(int purchaseID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = purchaseID;
			string sqlStr = "DELETE FROM warehousePurchaseItem WHERE PurchaseID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据采购单ID获取所有采购单商品

		/// <summary>
		/// 根据采购单ID获取所有采购单商品
		/// </summary>
		/// <param name="purchaseID">采购单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehousePurchaseItem> GetWarehousePurchaseItemList(int purchaseID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = purchaseID;
			string sqlStr = "SELECT * FROM warehousePurchaseItem WHERE PurchaseID=@0";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据采购单商品表ID列表获取采购单商品
		
		/// <summary>
		/// 根据采购单商品表ID列表获取采购单商品
		/// </summary>
		/// <param name="purchaseItemIDList">采购单商品表ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehousePurchaseItem> GetWarehousePurchaseItemList(List<int> purchaseItemIDList, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = string.Join(",", purchaseItemIDList.ToArray());
			string sqlStr = "SELECT * FROM warehousePurchaseItem WHERE FIND_IN_SET(ID, @0)";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 修改采购单商品的采购数量
		
		/// <summary>
		/// 修改采购单商品的采购数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseItemID">采购单商品表主键ID</param>
		/// <param name="newNum">要更新数量</param>
		public int UpdateNum(string userCode, int purchaseItemID, int newNum, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = purchaseItemID;
			objects[1] = newNum;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string whereSql = " AND PurchaseID IN (SELECT ID FROM warehousePurchase WHERE Status=" + (int)PurchaseStatus.未确认 + ")";
			string sqlStr = @"UPDATE warehousePurchaseItem SET Num=@1,UpdatePerson=@2,UpdateDate=@3 WHERE ID=@0" + whereSql;
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 修改采购单商品的已入库数量

		/// <summary>
		/// 修改采购单商品的已入库数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseItemID">采购单商品表主键ID</param>
		/// <param name="diffNum">要更新数量 差量更新可正可负</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateInStockNum(string userCode, int purchaseItemID, int diffNum, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = purchaseItemID;
			objects[1] = diffNum;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE warehousePurchaseItem SET InStockNum=InStockNum+@1,UpdatePerson=@2,UpdateDate=@3 WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion
	}
}





