using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
namespace PaiXie.Data {
	public class OrditemRepository : BaseRepository<Orditem> {

		#region 构造函数

		private static OrditemRepository _instance;
		public static OrditemRepository GetInstance() {
			if (_instance == null) {
				_instance = new OrditemRepository();
			}
			return _instance;
		}

		#endregion

		#region Add

		public int Add(Orditem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<Orditem>("ord_item", entity)
					.AutoMap(x => x.ID)
					.ExecuteReturnLastId<int>();
			return Id;
		}

		#endregion

		#region Update

		public int Update(Orditem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<Orditem>("ord_item", entity)
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
		public virtual Orditem GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM ord_item WHERE ID=@0";
			Orditem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 根据出库单ID获取订单明细

		/// <summary>
		/// 根据出库单ID获取订单明细
		/// </summary>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<Orditem> GetQueryManyByOutboundID(int outboundID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = outboundID;
			string sqlStr = "SELECT * FROM ord_item WHERE OutboundID=@0";
			List<Orditem> objList = GetQueryMany(sqlStr, context, objects);
			return objList;
		}

		#endregion

		#region 根据出库单ID更新发货时间

		/// <summary>
		/// 根据出库单ID更新发货时间
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="deliveryDate">发货时间</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateDeliveryDate(string userCode, string warehouseCode, int outboundID, DateTime deliveryDate, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			objects[2] = deliveryDate;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			string sqlStr = "UPDATE ord_item SET DeliveryDate=@2,UpdatePerson=@3,UpdateDate=@4 WHERE WarehouseCode=@0 AND OutboundID=@1";
			return Update(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM ord_item WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据出库单ID删除出库单明细

		/// <summary>
		/// 根据出库单ID删除出库单明细
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DelByOutboundID(string warehouseCode, int outboundID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			string sqlStr = @"DELETE FROM ord_item WHERE WarehouseCode=@0 AND OutboundID=@1";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 获取单个实体

		/// <summary>
		/// 获取单个实体
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="productsSkuID"></param>
		/// <param name="AddType"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual Orditem GetSingleOrditem(string erpOrderCode, int productsSkuID, int AddType, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = erpOrderCode;
			objects[1] = productsSkuID;
			objects[2] = AddType;
			string sqlStr = "SELECT * FROM ord_item WHERE ErpOrderCode = @0 AND ProductsSkuID = @1 AND AddType = @2";
			Orditem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		/// <summary>
		/// 获取单个实体
		/// </summary>
		/// <param name="ordouterItemID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual Orditem GetSingleOrditem(int ordouterItemID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ordouterItemID;
			string sqlStr = "SELECT * FROM ord_item WHERE OrdouterItemID = @0 AND WarehouseCode IS NULL";
			Orditem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 获取实体列表

		/// <summary>
		/// 根据系统订单号获取实体列表
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<Orditem> GetManyOrditem(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = "SELECT * FROM ord_item WHERE ErpOrderCode = @0";
			return GetQueryMany(sqlStr, context, objects);
		}

		/// <summary>
		/// 根据系统订单ID获取实体列表
		/// </summary>
		/// <param name="ordbaseID">系统订单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<Orditem> GetManyOrditem(int ordbaseID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ordbaseID;
			string sqlStr = "SELECT * FROM ord_item WHERE OrdbaseID = @0";
			return GetQueryMany(sqlStr, context, objects);
		}

		/// <summary>
		/// 根据外部订单号获取实体列表
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="outOrderCode">外部订单号</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual List<Orditem> GetManyOrditem(int shopID, string outOrderCode, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = shopID;
			objects[1] = outOrderCode;
			string sqlStr = "SELECT * FROM ord_item WHERE ShopID = @0 AND OutOrderCode = @1";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 更新订单明细数量

		/// <summary>
		/// 更新订单明细数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">订单明细主键ID</param>
		/// <param name="productsNum">数量 正数增加，负数扣减</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateProductsNum(string userCode, int id, int productsNum, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = id;
			objects[1] = productsNum;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE ord_item SET ProductsNum=ProductsNum+@1,UpdatePerson=@2,UpdateDate=@3 WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据出库单ID删除商品数量为0的明细

		/// <summary>
		/// 根据出库单ID删除商品数量为0的明细
		/// </summary>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DeleteNoProductNum(int outboundID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = outboundID;
			string sqlStr = @"DELETE FROM ord_item WHERE outboundID=@0 AND ProductsNum=0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 获取分配仓库时商品列表

		/// <summary>
		/// 获取分配仓库时商品列表
		/// </summary>
		/// <param name="ordbaseID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual List<DistributionWarehouseInfo> GetManyDistributionWarehouseInfo(int ordbaseID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ordbaseID;
			string sqlStr = @"SELECT ID,ProductsID,ProductsCode,ProductsName,ProductsSkuID,ProductsSkuCode,ProductsSkuSaleprop,ProductsNum WfpNum
                              FROM ord_item WHERE OrdbaseID = @0 and OutboundID = 0";
			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QueryMany<DistributionWarehouseInfo>();
		}

		#endregion

		#region 清除出库单明细的出库单信息

		/// <summary>
		/// 清除出库单明细的出库单信息
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int ClearOutboundInfo(string userCode, string warehouseCode, int outboundID, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"Update ord_item SET OutboundBillNo='',OutboundID=0,WarehouseCode='',UpdatePerson=@2,UpdateDate=@3 WHERE WarehouseCode=@0 AND OutboundID=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据系统订单号获取未生成出库单的订单明细

		/// <summary>
		/// 根据系统订单号获取未生成出库单的订单明细
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<Orditem> GetOrdItemListNotOutbound(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = @"SELECT * FROM ord_item WHERE ErpOrderCode=@0 AND OutboundID=0";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 清空出库单明细的仓库编码、出库单号、出库单ID

		/// <summary>
		/// 清空出库单明细的仓库编码、出库单号、出库单ID
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="ordItemID">出库单明细ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int ClearOutboundInfo(string userCode, int ordItemID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = ordItemID;
			objects[1] = userCode;
			objects[2] = DateTime.Now;
			string sqlStr = @"UPDATE ord_item SET WarehouseCode='',OutboundID=0,OutboundBillNo='',UpdatePerson=@1,UpdateDate=@2 WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 获取商品条数

		/// <summary>
		/// 获取商品条数
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int GetCount(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = "SELECT COUNT(1) FROM ord_item WHERE ErpOrderCode = @0";
			return GetCount(sqlStr, context, objects);
		}

		#endregion

		#region 根据系统订单号获取出库单的商品发货记录

		/// <summary>
		/// 根据系统订单号获取出库单的商品发货记录
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehouseOutboundPickItemWebInfo> GetManyOutboundItem(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = @"SELECT i.ProductsCode,i.ProductsName,i.ProductsSkuSaleprop,i.ProductsSkuCode,i.Unit AS ProductsUnit,i.TaxRate,i.ActualSellingPrice,i.DiscountAmount,i.ProductsWeight,wobpi.Num,wobpi.ProductsBatchCode,wobpi.OutboundID,b.CostPrice 
                              FROM ord_item i LEFT JOIN warehouseOutboundPickItem wobpi ON i.ID = wobpi.OrdItemID
                              LEFT JOIN warehouseProductsBatch b ON b.ID = wobpi.ProductsBatchID
                              WHERE i.ErpOrderCode = @0";
			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QueryMany<WarehouseOutboundPickItemWebInfo>();
		}

		#endregion
	}
}





