using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class OrdrefundItemService  : BaseService<OrdrefundItem> {
    
        #region Update
        
		public static int Update(OrdrefundItem entity, IDbContext context = null) {
			return OrdrefundItemRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(OrdrefundItem entity, IDbContext context = null) {
			return OrdrefundItemRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static OrdrefundItem GetQuerySingleByID(int id, IDbContext context = null) {
		    return OrdrefundItemRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return OrdrefundItemRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 根据出库单明细表主键ID获取已售后数量

		/// <summary>
		/// 根据出库单明细表主键ID获取已售后数量
		/// </summary>
		/// <param name="ordItemID">出库单明细表主键ID</param>
		/// <param name="context">数据库连接对象</param>
		public static int GetHasRefundNum(int ordItemID, IDbContext context = null) {
			return OrdrefundItemRepository.GetInstance().GetHasRefundNum(ordItemID, context);
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
		public static List<OrdrefundItem> GetOrdRefundItemList(string warehouseCode, int ordRefundID, IDbContext context = null) {
			return OrdrefundItemRepository.GetInstance().GetOrdRefundItemList(warehouseCode, ordRefundID, context);
		}

		#endregion

		#region 根据订单号获取售后商品列表

		/// <summary>
		/// 根据订单号获取售后商品列表
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<OrdRefundItemList> GetManyOrdRefundItemList(string erpOrderCode, IDbContext context = null) {
			return OrdrefundItemRepository.GetInstance().GetManyOrdRefundItemList(erpOrderCode, context);
		}

		#endregion
	}
}





