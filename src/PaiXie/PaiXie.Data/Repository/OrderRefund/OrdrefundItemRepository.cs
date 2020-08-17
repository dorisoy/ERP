using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Utils;
using PaiXie.Core;
namespace PaiXie.Data 
{
    public	class OrdrefundItemRepository:BaseRepository<OrdrefundItem> {

        #region 构造函数
     
	    private static OrdrefundItemRepository _instance;
	    public static OrdrefundItemRepository GetInstance() {
            if (_instance == null) {
                _instance = new OrdrefundItemRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(OrdrefundItem entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<OrdrefundItem>("ord_refundItem", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(OrdrefundItem entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<OrdrefundItem>("ord_refundItem", entity)
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
		public virtual OrdrefundItem GetQuerySingleByID(int id, IDbContext context = null) {
				if (context == null) context = Db.GetInstance().Context();
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM ord_refundItem WHERE ID=@0";
			OrdrefundItem obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM ord_refundItem WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion
     
		#region 根据出库单明细表主键ID获取已售后数量

		/// <summary>
		/// 根据出库单明细表主键ID获取已售后数量
		/// </summary>
		/// <param name="ordItemID">出库单明细表主键ID</param>
		/// <param name="context">数据库连接对象</param>
		public virtual int GetHasRefundNum(int ordItemID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ordItemID;
			string sqlStr = @"SELECT IFNULL(Sum(RefundNum),0) FROM ord_refundItem ordi INNER JOIN ord_refund ord ON ordi.OrdRefundID=ord.ID AND ord.STATUS<>" + (int)OrdRefundStatus.已取消 + " WHERE OrdItemID=@0";
			return ZConvert.StrToInt(Getobject(sqlStr, context, objects));
		}

		#endregion

		#region 根据售后表主键ID获取售后商品列表

		/// <summary>
		/// 根据售后表主键ID获取售后商品列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="ordRefundID">售后表主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<OrdrefundItem> GetOrdRefundItemList(string warehouseCode, int ordRefundID, IDbContext context = null) {
			string strWhere = string.Empty;
			if (!string.IsNullOrEmpty(warehouseCode)) {
				strWhere += " and WarehouseCode = @0";
			}
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = ordRefundID;
			string sqlStr = @"SELECT * FROM ord_refundItem WHERE OrdRefundID=@1" + strWhere;
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据订单号获取售后商品列表

		/// <summary>
		/// 根据订单号获取售后商品列表
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual List<OrdRefundItemList> GetManyOrdRefundItemList(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = @"SELECT ri.OrdRefundID,ri.ProductsCode,ri.ProductsName,ri.ProductsSkuSaleprop,ri.ProductsSkuCode,ri.ActualSellingPrice,ri.RefundNum,ri.ProductsBatchCode,i.Unit,i.ProductsWeight FROM ord_refundItem ri INNER JOIN ord_item i ON i.ID = ri.OrdItemID
							  WHERE ri.ErpOrderCode = @0";
			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QueryMany<OrdRefundItemList>();
		}

		#endregion
	}
}





