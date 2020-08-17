using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using System.Data;
using PaiXie.Core;
using PaiXie.Utils;
namespace PaiXie.Data 
{
	public class WarehouseOutboundPickItemRepository : BaseRepository<WarehouseOutboundPickItem> {

        #region 构造函数
     
	    private static WarehouseOutboundPickItemRepository _instance;
	    public static WarehouseOutboundPickItemRepository GetInstance() {
            if (_instance == null) {
                _instance = new WarehouseOutboundPickItemRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(WarehouseOutboundPickItem entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<WarehouseOutboundPickItem>("warehouseOutboundPickItem", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(WarehouseOutboundPickItem entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<WarehouseOutboundPickItem>("warehouseOutboundPickItem", entity)
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
		public virtual WarehouseOutboundPickItem GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseOutboundPickItem WHERE ID=@0";
			WarehouseOutboundPickItem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}
        
		#endregion

		#region 根据出库单ID获取拣货明细

		/// <summary>
		/// 根据出库单ID获取拣货明细
		/// </summary>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehouseOutboundPickItem> GetQueryManyByOutboundID(int outboundID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = outboundID;
			string sqlStr = "SELECT * FROM warehouseOutboundPickItem WHERE OutboundID=@0";
			List<WarehouseOutboundPickItem> objList = GetQueryMany(sqlStr, context, objects);
			return objList;
		}

		#endregion

		#region 根据待采出库单ID列表获取预售拣货明细 (按SKU汇总，用于生成采购计划单)

		/// <summary>
		/// 根据待采出库单ID列表获取预售拣货明细 (按SKU汇总，用于生成采购计划单)
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundIDList">待采出库单ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehouseOutboundPickItem> GetBookingPickItemList(string warehouseCode, List<int> outboundIDList, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = string.Join(",", outboundIDList.ToArray());
			string sqlStr = @"SELECT wobpi.WarehouseCode,wobpi.ProductsCode,wobpi.ProductsID,wobpi.ProductsName,wobpi.ProductsNo,wobpi.ProductsSkuID,wobpi.ProductsSkuCode,wobpi.ProductsSkuCode,wobpi.ProductsSkuSaleprop,Sum(Num) AS Num FROM warehouseOutboundPickItem wobpi
			INNER JOIN warehouseOutbound wob ON wobpi.OutboundID=wob.ID
			WHERE wobpi.WarehouseCode=@0 AND FIND_IN_SET(wobpi.OutboundID,@1) AND wobpi.LocationID=0 AND wobpi.ProductsBatchID=0 AND wob.IsApplyRefund=0 AND wob.IsWaitPurchase=1
			GROUP BY wobpi.WarehouseCode,wobpi.ProductsCode,wobpi.ProductsID,wobpi.ProductsName,wobpi.ProductsNo,wobpi.ProductsSkuID,wobpi.ProductsSkuCode,wobpi.ProductsSkuCode,wobpi.ProductsSkuSaleprop";
			List<WarehouseOutboundPickItem> objList = GetQueryMany(sqlStr, context, objects);
			return objList;
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
			string sqlStr = "DELETE FROM warehouseOutboundPickItem WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 根据订单明细ID获取拣货信息 不区分批次

		/// <summary>
		/// 根据订单明细ID获取拣货信息 不区分批次
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="ordItemID">订单明细ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual DataTable GetLocationInfoByOrdItemID(string warehouseCode, int ordItemID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = ordItemID;
			string sqlStr = @"SELECT LocationCode, Sum(Num) as Num FROM warehouseOutboundPickItem WHERE WarehouseCode=@0 AND OrdItemID=@0 GROUP BY LocationCode";
			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 根据出库单ID列表获取拣货信息 不区分批次

		/// <summary>
		/// 根据出库单ID列表获取拣货信息 不区分批次
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="idList">出库单ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual DataTable GetLocationInfoByOutboundIDList(string warehouseCode, List<int> idList, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = string.Join(",", idList.ToArray());
			string sqlStr = @"SELECT ProductsCode,ProductsName,ProductsSkuSaleprop,ProductsSkuCode,SUM(Num) as ProductsNum,LocationCode 
			FROM warehouseOutboundPickItem WHERE WarehouseCode=@0 AND FIND_IN_SET(OutboundID,@1) Group BY ProductsCode,ProductsName,ProductsSkuSaleprop,ProductsSkuCode,LocationCode";
			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 根据出库单明细ID更新拣货信息(拆分预售出库单)

		/// <summary>
		/// 根据出库单明细ID更新拣货信息(拆分预售出库单)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="oldOrdItemID">原出库单明细ID</param>
		/// <param name="billNo">新的出库单号</param>
		/// <param name="newOutboundID">新的出库单ID</param>
		/// <param name="newOrdItemID">新出库单明细ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateOutboundInfo(string userCode, int oldOrdItemID, string billNo, int newOutboundID, int newOrdItemID, IDbContext context = null) {
			Object[] objects = new Object[6];
			objects[0] = oldOrdItemID;
			objects[1] = billNo;
			objects[2] = newOutboundID;
			objects[3] = newOrdItemID;
			objects[4] = userCode;
			objects[5] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutboundPickItem SET OutboundBillNo=@1,OutboundID=@2,OrdItemID=@3,UpdatePerson=@4,UpdateDate=@5 WHERE OrdItemID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据出库单ID删除拣货明细

		/// <summary>
		/// 根据出库单ID删除拣货明细
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DelByOutboundID(string warehouseCode, int outboundID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			string sqlStr = @"DELETE FROM warehouseOutboundPickItem WHERE WarehouseCode=@0 AND OutboundID=@1";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据出库单ID获取按SKU、批次、实际销售价汇总的拣货明细

		public virtual DataTable GetBatchInfoByOutboundID(string warehouseCode, int outboundID, IDbContext context = null) {
			string strWhere = string.Empty;
			if (!string.IsNullOrEmpty(warehouseCode)) {
				strWhere += " and WarehouseCode = @0";
			}
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			string sqlStr = @"SELECT OrdItemID,ProductsID,ProductsNo,ProductsCode,ProductsName,ProductsSkuID,ProductsSkuSaleprop,ProductsSkuCode,ActualSellingPrice,ProductsBatchID,ProductsBatchCode,Sum(Num) AS ProductsNum,
			(SELECT IFNULL(Sum(RefundNum),0) FROM ord_refundItem ordi INNER JOIN ord_refund ord on ordi.OrdRefundID=ord.ID AND ord.STATUS<>" + (int)OrdRefundStatus.已取消 + @"  WHERE OrdItemID=warehouseOutboundPickItem.OrdItemID) as HasRefunNum
			FROM warehouseOutboundPickItem
			WHERE OutboundID=@1 " + strWhere + " GROUP BY OrdItemID,ProductsID,ProductsNo,ProductsCode,ProductsName,ProductsSkuID,ProductsSkuSaleprop,ProductsSkuCode,ActualSellingPrice,ProductsBatchID,ProductsBatchCode";
			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 根据商品SKUID获取预售拣货明细 按出库单ID升序

		/// <summary>
		/// 根据商品SKUID获取预售拣货明细 按出库单ID升序
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <param name="ordItemID">出库单明细表主键ID 如果有传且大于0，则只读取该ID的拣货明细</param>
		/// <returns></returns>
		public virtual List<WarehouseOutboundPickItem> GetBookingPickItemList(string warehouseCode, int productsSkuID, IDbContext context = null, int ordItemID = 0) {
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = productsSkuID;
			objects[2] = ordItemID;
			string strWhere = string.Empty;
			string strOrderBy = string.Empty;
			if (ordItemID > 0) {
				strWhere = " AND OrdItemID=@2";
			}
			else {
				strWhere = " AND ProductsSkuID=@1";
				strOrderBy = " ORDER BY OutboundID ASC";
			}
			string sqlStr = @"SELECT * FROM warehouseOutboundPickItem WHERE WarehouseCode=@0 " + strWhere + "  AND LocationID=0" + strOrderBy;
			List<WarehouseOutboundPickItem> objList = GetQueryMany(sqlStr, context, objects);
			return objList;
		}

		#endregion

		#region 根据出库单ID获取预售拣货明细条数

		/// <summary>
		/// 根据出库单ID获取预售拣货明细条数
		/// </summary>
		/// <param name="id">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int GetBookingPickItemCount(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT COUNT(0) FROM warehouseOutboundPickItem WHERE OutboundID=@0 AND LocationID=0";
			int count = ZConvert.StrToInt(Getobject(sqlStr, context, objects));
			return count;
		}
		#endregion
	}
}





