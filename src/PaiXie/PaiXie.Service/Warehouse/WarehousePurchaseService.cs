using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehousePurchaseService  : BaseService<WarehousePurchase> {
    
        #region Update
        
		public static int Update(WarehousePurchase entity, IDbContext context = null) {
			return WarehousePurchaseRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehousePurchase entity, IDbContext context = null) {
			return WarehousePurchaseRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehousePurchase GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehousePurchaseRepository.GetInstance().GetQuerySingleByID(id, context);
	    }
    
	    #endregion

		#region 获取单个实体 通过采购单号

	    /// <summary>
		/// 获取单个实体 通过采购单号
	    /// </summary>
		/// <param name="billNo">采购单号</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
		public static WarehousePurchase GetQuerySingleByBillNo(string billNo, IDbContext context = null) {
			return WarehousePurchaseRepository.GetInstance().GetQuerySingleByBillNo(billNo, context);
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
		    return WarehousePurchaseRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 确认采购单

        /// <summary>
		/// 确认采购单
	    /// </summary>
		/// <param name="purchaseID">主键ID</param>
	    /// <param name="context">数据库对象</param>
	    /// <returns></returns>
		public static int Confirm(int purchaseID, IDbContext context = null) {
			return WarehousePurchaseRepository.GetInstance().Confirm(purchaseID, context);
	    }
    
        #endregion

		#region 结束采购单

		/// <summary>
		/// 结束采购单
		/// </summary>
		/// <param name="purchaseID">主键ID</param>
		/// <param name="context">数据库对象</param>
		/// <returns></returns>
		public static int End(int purchaseID, IDbContext context = null) {
			return WarehousePurchaseRepository.GetInstance().End(purchaseID, context);
		}

		#endregion

		#region 根据采购单ID把已结束状态还原为已确认

		/// <summary>
		/// 根据采购单ID把已结束状态还原为已确认
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseID">采购单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Restore(string userCode, int purchaseID, IDbContext context = null) {
			return WarehousePurchaseRepository.GetInstance().Restore(userCode, purchaseID, context);
		}

		#endregion

		#region 更新采购单的采购数量

		/// <summary>
		/// 更新采购单的采购数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseID">采购单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateNum(string userCode, int purchaseID, IDbContext context = null) {
			return WarehousePurchaseRepository.GetInstance().UpdateNum(userCode, purchaseID, context);
		}

		#endregion

		#region 更新采购单的已入库数量

		/// <summary>
		/// 更新采购单的已入库数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseID">采购单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateInStockNum(string userCode, int purchaseID, IDbContext context = null) {
			return WarehousePurchaseRepository.GetInstance().UpdateInStockNum(userCode, purchaseID, context);
		}

		#endregion

		#region 更新采购单的入库单条数

		/// <summary>
		/// 更新采购单的入库单条数
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseID">采购单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateInStockOrderCount(string userCode, int purchaseID, IDbContext context = null) {
			return WarehousePurchaseRepository.GetInstance().UpdateInStockOrderCount(userCode, purchaseID, context);
		}

		#endregion

	    #region 更新采购单库存

		/// <summary>
		/// 更新采购单库存
		/// </summary>
		/// <param name="rkts">订单数</param>
		/// <param name="rknum">入库数量</param>
		/// <param name="id">主键id</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static  int UpdatewarehousePurchasekc(int rkts, int rknum, int id, IDbContext context = null) {
			return WarehousePurchaseRepository.GetInstance().UpdatewarehousePurchasekc(rkts,  rknum,  id,  context);
		}

		#endregion
	

	}
}





