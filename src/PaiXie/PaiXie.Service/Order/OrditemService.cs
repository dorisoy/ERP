using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class OrditemService  : BaseService<Orditem> {
    
        #region Update
        
		public static int Update(Orditem entity, IDbContext context = null) {
			return OrditemRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(Orditem entity, IDbContext context = null) {
			return OrditemRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static Orditem GetQuerySingleByID(int id, IDbContext context = null) {
		    return OrditemRepository.GetInstance().GetQuerySingleByID(id, context);
	    }
    
	    #endregion

		#region 根据出库单ID获取订单明细

		/// <summary>
		/// 根据出库单ID获取订单明细
		/// </summary>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<Orditem> GetQueryManyByOutboundID(int outboundID, IDbContext context = null) {
			return OrditemRepository.GetInstance().GetQueryManyByOutboundID(outboundID, context);
		}

		#endregion

		#region 根据出库单ID更新发货时间

		/// <summary>
		/// 根据出库单ID更新发货时间
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="DeliveryDate">发货时间</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateDeliveryDate(string userCode, string warehouseCode, int outboundID, DateTime DeliveryDate, IDbContext context = null) {
			return OrditemRepository.GetInstance().UpdateDeliveryDate(userCode, warehouseCode, outboundID, DeliveryDate, context);
		}

		#endregion 

		#region 删除操作  通过ID

		/// <summary>
    	/// 删除操作  通过ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库对象</param>
	    /// <returns></returns>
	    public static int DelByID(int id, IDbContext context = null) {
		    return OrditemRepository.GetInstance().DelByID(id, context);
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
		public static int DelByOutboundID(string warehouseCode, int outboundID, IDbContext context = null) {
			return OrditemRepository.GetInstance().DelByOutboundID(warehouseCode, outboundID, context);
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
		public static Orditem GetSingleOrditem(string erpOrderCode, int productsSkuID, int AddType, IDbContext context = null) {
			return OrditemRepository.GetInstance().GetSingleOrditem(erpOrderCode, productsSkuID, AddType, context);
		}

		/// <summary>
		/// 获取单个实体
		/// </summary>
		/// <param name="ordouterItemID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static Orditem GetSingleOrditem(int ordouterItemID, IDbContext context = null) {
			return OrditemRepository.GetInstance().GetSingleOrditem(ordouterItemID, context);
		}

		#endregion

		#region 获取实体列表

		/// <summary>
		/// 根据系统订单号获取实体列表
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<Orditem> GetManyOrditem(string erpOrderCode, IDbContext context = null) {
			return OrditemRepository.GetInstance().GetManyOrditem(erpOrderCode, context);
		}

		/// <summary>
		/// 根据系统订单ID获取实体列表
		/// </summary>
		/// <param name="ordbaseID">系统订单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<Orditem> GetManyOrditem(int ordbaseID, IDbContext context = null) {
			return OrditemRepository.GetInstance().GetManyOrditem(ordbaseID, context);
		}

		/// <summary>
		/// 根据外部订单号获取实体列表
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="outOrderCode">外部订单号</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<Orditem> GetManyOrditem(int shopID, string outOrderCode, IDbContext context = null) {
			return OrditemRepository.GetInstance().GetManyOrditem(shopID, outOrderCode, context);
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
		public static int UpdateProductsNum(string userCode, int id, int productsNum, IDbContext context = null) {
			return OrditemRepository.GetInstance().UpdateProductsNum(userCode, id, productsNum, context);
		}

		#endregion

		#region 根据出库单ID删除商品数量为0的明细

		/// <summary>
		/// 根据出库单ID删除商品数量为0的明细
		/// </summary>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DeleteNoProductNum(int outboundID, IDbContext context = null) {
			return OrditemRepository.GetInstance().DeleteNoProductNum(outboundID, context);
		}

		#endregion

		#region 获取分配仓库时商品列表

		/// <summary>
		/// 根据系统订单号获取实体列表
		/// </summary>
		/// <param name="ordbaseID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<DistributionWarehouseInfo> GetManyDistributionWarehouseInfo(int ordbaseID, IDbContext context = null) {
			return OrditemRepository.GetInstance().GetManyDistributionWarehouseInfo(ordbaseID, context);
		}

		#endregion

		#region 根据系统订单号获取未生成出库单的订单明细

		/// <summary>
		/// 根据系统订单号获取未生成出库单的订单明细
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<Orditem> GetOrdItemListNotOutbound(string erpOrderCode, IDbContext context = null) {
			return OrditemRepository.GetInstance().GetOrdItemListNotOutbound(erpOrderCode, context);
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
		public static int ClearOutboundInfo(string userCode, int ordItemID, IDbContext context = null) {
			return OrditemRepository.GetInstance().ClearOutboundInfo(userCode, ordItemID, context);
		}

		#endregion

		#region 获取商品条数

		/// <summary>
		/// 获取商品条数
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int GetCount(string erpOrderCode, IDbContext context = null) {
			return OrditemRepository.GetInstance().GetCount(erpOrderCode, context);
		}

		#endregion

		#region 根据系统订单号获取出库单的商品发货记录

		/// <summary>
		/// 根据系统订单号获取出库单的商品发货记录
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehouseOutboundPickItemWebInfo> GetManyOutboundItem(string erpOrderCode, IDbContext context = null) {
			return OrditemRepository.GetInstance().GetManyOutboundItem(erpOrderCode, context);
		}

		#endregion
	}
}





