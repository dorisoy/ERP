using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseMoveLocationItemService  : BaseService<WarehouseMoveLocationItem> {
    
        #region Update
        
		public static int Update(WarehouseMoveLocationItem entity, IDbContext context = null) {
			return WarehouseMoveLocationItemRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehouseMoveLocationItem entity, IDbContext context = null) {
			return WarehouseMoveLocationItemRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehouseMoveLocationItem GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehouseMoveLocationItemRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return WarehouseMoveLocationItemRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 根据移位单ID删除所有商品

		/// <summary>
		/// 根据移位单ID删除所有商品
		/// </summary>
		/// <param name="moveLocationID">移位单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DeleteByMoveLocationID(int moveLocationID, IDbContext context = null) {
			return WarehouseMoveLocationItemRepository.GetInstance().DeleteByMoveLocationID(moveLocationID, context);
		}

		#endregion

		#region 根据移位单ID获取所有商品

		/// <summary>
		/// 根据移位单ID获取所有商品
		/// </summary>
		/// <param name="moveLocationID">移位单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehouseMoveLocationItem> GetWarehouseMoveLocationItemList(int moveLocationID, IDbContext context = null) {
			return WarehouseMoveLocationItemRepository.GetInstance().GetWarehouseMoveLocationItemList(moveLocationID, context);
		}

		#endregion

		#region 根据移位单商品表主键ID删除商品

		/// <summary>
		/// 根据移位单商品表主键ID删除商品
		/// </summary>
		/// <param name="moveLocationItemIDList">移位单商品表主键ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Delete(List<int> moveLocationItemIDList, IDbContext context = null) {
			return WarehouseMoveLocationItemRepository.GetInstance().Delete(moveLocationItemIDList, context);
		}

		#endregion

		#region 根据移位单商品表主键ID获取商品

		/// <summary>
		/// 根据移位单商品表主键ID获取商品
		/// </summary>
		/// <param name="moveLocationItemIDList">移位单商品表主键ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehouseMoveLocationItem> GetWarehouseMoveLocationItemList(List<int> moveLocationItemIDList, IDbContext context = null) {
			return WarehouseMoveLocationItemRepository.GetInstance().GetWarehouseMoveLocationItemList(moveLocationItemIDList, context);
		}

		#endregion

		#region 根据移位单主键ID获取商品总数

		/// <summary>
		/// 根据移位单主键ID获取商品总数
		/// </summary>
		/// <param name="moveLocationID">移位单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetNumByMoveLocationID(int moveLocationID, IDbContext context = null) {
			return WarehouseMoveLocationItemRepository.GetInstance().GetNumByMoveLocationID(moveLocationID, context);
		}

		#endregion

		#region 获取单个实体

		/// <summary>
		/// 获取单个实体
		/// </summary>
		/// <param name="moveLocationID">移位表主键ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="outLocationID">移出库位ID</param>
		/// <param name="inLocationID">移入库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="context"></param>
		public static WarehouseMoveLocationItem GetSingleWarehouseMoveLocationItem(int moveLocationID, int productsSkuID, int outLocationID, int inLocationID, int productsBatchID, IDbContext context = null) {
			return WarehouseMoveLocationItemRepository.GetInstance().GetSingleWarehouseMoveLocationItem(moveLocationID, productsSkuID, outLocationID, inLocationID, productsBatchID, context);
		}

		#endregion

		#region 修改移入库位编码和移位数量

		/// <summary>
		/// 修改移入库位编码和移位数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="moveLocationItemID">移位单明细主键ID</param>
		/// <param name="inLocationID">移入库位ID</param>
		/// <param name="diffNum">要更新数量 差量更新可正可负</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateMoveLocationItem(string userCode, int moveLocationItemID, int inLocationID, int diffNum, IDbContext context) {
			return WarehouseMoveLocationItemRepository.GetInstance().UpdateMoveLocationItem(userCode, moveLocationItemID, inLocationID, diffNum, context);
		}

		#endregion

		#region 更新移位明细状态

		/// <summary>
		/// 更新移位明细状态
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="moveLocationItemID">移位明细主键ID</param>
		/// <param name="oldStatus">旧状态</param>
		/// <param name="newStatus">新状态</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateStatus(string userCode, int moveLocationItemID, int oldStatus, int newStatus, IDbContext context = null) {
			return WarehouseMoveLocationItemRepository.GetInstance().UpdateStatus(userCode, moveLocationItemID, oldStatus, newStatus, context);
		}

		#endregion
	}
}





