using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseOutboundPickItemService  : BaseService<WarehouseOutboundPickItem> {
    
        #region Update
        
		public static int Update(WarehouseOutboundPickItem entity, IDbContext context = null) {
			return WarehouseOutboundPickItemRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehouseOutboundPickItem entity, IDbContext context = null) {
			return WarehouseOutboundPickItemRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehouseOutboundPickItem GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehouseOutboundPickItemRepository.GetInstance().GetQuerySingleByID(id, context);
	    }
    
	    #endregion

		#region 根据出库单ID获取拣货明细

		/// <summary>
		/// 根据出库单ID获取拣货明细
		/// </summary>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehouseOutboundPickItem> GetQueryManyByOutboundID(int outboundID, IDbContext context = null) {
			return WarehouseOutboundPickItemRepository.GetInstance().GetQueryManyByOutboundID(outboundID, context);
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
		public static int DelByOutboundID(string warehouseCode, int outboundID, IDbContext context = null) {
			return WarehouseOutboundPickItemRepository.GetInstance().DelByOutboundID(warehouseCode, outboundID, context);
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
		public static List<WarehouseOutboundPickItem> GetBookingPickItemList(string warehouseCode, List<int> outboundIDList, IDbContext context = null) {
			return WarehouseOutboundPickItemRepository.GetInstance().GetBookingPickItemList(warehouseCode, outboundIDList, context);
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
		    return WarehouseOutboundPickItemRepository.GetInstance().DelByID(id, context);
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
		public static DataTable GetLocationInfoByOrdItemID(string warehouseCode, int ordItemID, IDbContext context = null) {
			return WarehouseOutboundPickItemRepository.GetInstance().GetLocationInfoByOrdItemID(warehouseCode, ordItemID, context);
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
		public static DataTable GetLocationInfoByOutboundIDList(string warehouseCode, List<int> idList, IDbContext context = null) {
			return WarehouseOutboundPickItemRepository.GetInstance().GetLocationInfoByOutboundIDList(warehouseCode, idList, context);
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
		public static int UpdateOutboundInfo(string userCode, int oldOrdItemID, string billNo, int newOutboundID, int newOrdItemID, IDbContext context = null) {
			return WarehouseOutboundPickItemRepository.GetInstance().UpdateOutboundInfo(userCode, oldOrdItemID, billNo, newOutboundID, newOrdItemID, context);
		}

		#endregion

		#region 根据出库单ID获取拣货信息 按SKU、批次、实际销售价汇总的拣货明细

		/// <summary>
		/// 根据出库单ID获取拣货信息 按SKU、批次、实际销售价汇总的拣货明细
		/// </summary>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static DataTable GetBatchInfoByOutboundID(string warehouseCode, int outboundID, IDbContext context = null) {
			return WarehouseOutboundPickItemRepository.GetInstance().GetBatchInfoByOutboundID(warehouseCode, outboundID, context);
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
		public static List<WarehouseOutboundPickItem> GetBookingPickItemList(string warehouseCode, int productsSkuID, IDbContext context = null, int ordItemID = 0) {
			return WarehouseOutboundPickItemRepository.GetInstance().GetBookingPickItemList(warehouseCode, productsSkuID, context, ordItemID);
		}

		#endregion

		#region 根据出库单ID获取预售拣货明细条数

		/// <summary>
		/// 根据出库单ID获取预售拣货明细条数
		/// </summary>
		/// <param name="id">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetBookingPickItemCount(int id, IDbContext context = null) {
			return WarehouseOutboundPickItemRepository.GetInstance().GetBookingPickItemCount(id, context);
		}

		#endregion
	}
}





