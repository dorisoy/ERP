using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseStocktakingItemService  : BaseService<WarehouseStocktakingItem> {
    
        #region Update
        
		public static int Update(WarehouseStocktakingItem entity, IDbContext context = null) {
			return WarehouseStocktakingItemRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehouseStocktakingItem entity, IDbContext context = null) {
			return WarehouseStocktakingItemRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehouseStocktakingItem GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehouseStocktakingItemRepository.GetInstance().GetQuerySingleByID(id, context);
	    }
    
	    #endregion

		#region 获取单个实体 通过StocktakingID

		/// <summary>
		/// 获取单个实体 通过StocktakingID
	    /// </summary>
		/// <param name="id">StocktakingID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
		public static List<WarehouseStocktakingItem> GetQuerySingleByStocktakingID(int id, IDbContext context = null) {
			return WarehouseStocktakingItemRepository.GetInstance().GetQuerySingleByStocktakingID(id, context);
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
		    return WarehouseStocktakingItemRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 根据盘点记录ID删除盘点商品
        
		/// <summary>
		/// 根据盘点记录ID删除盘点商品
		/// </summary>
		/// <param name="stocktakingID">盘点记录ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Delete(int stocktakingID, IDbContext context = null) {
			return WarehouseStocktakingItemRepository.GetInstance().Delete(stocktakingID, context);
		}

		#endregion

		#region 获取盘点商品实体

		/// <summary>
		/// 获取盘点商品实体
		/// </summary>
		/// <param name="stocktakingID">盘点记录ID</param>
		/// <param name="locationCode">库位编码</param>
		/// <param name="productsBatchCode">批次号</param>
		/// <param name="productsSkuCode">商品SKU码</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static WarehouseStocktakingItem GetSingleWarehouseStocktakingItem(int stocktakingID, string locationCode, string productsBatchCode, string productsSkuCode, IDbContext context = null) {
			return WarehouseStocktakingItemRepository.GetInstance().GetSingleWarehouseStocktakingItem(stocktakingID, locationCode, productsBatchCode, productsSkuCode, context);
		}

		#endregion

		#region 根据商品SKUID、批次ID、库位ID 判断是否正在盘点

		/// <summary>
		/// 根据商品SKUID、批次ID、库位ID 判断是否正在盘点
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="productsBatchID">批次ID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static bool IsExists(int productsSkuID, int productsBatchID, int locationID, IDbContext context = null) {
			return WarehouseStocktakingItemRepository.GetInstance().IsExists(productsSkuID, productsBatchID, locationID, context);
		}

		#endregion

		#region 获取未盘点的商品数量

		/// <summary>
		/// 获取未盘点的商品数量
		/// </summary>
		/// <param name="stocktakingID">盘点记录ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetNotImportCount(int stocktakingID, IDbContext context = null) {
			return WarehouseStocktakingItemRepository.GetInstance().GetNotImportCount(stocktakingID, context);
		}

		#endregion

		#region 获取盘点商品实体列表

		/// <summary>
		/// 获取盘点商品实体列表
		/// </summary>
		/// <param name="stocktakingID">盘点记录ID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<WarehouseStocktakingItem> GetManyWarehouseStocktakingItem(int stocktakingID, IDbContext context = null) {
			return WarehouseStocktakingItemRepository.GetInstance().GetManyWarehouseStocktakingItem(stocktakingID, context);
		}

		#endregion

		#region 获取单个实体[盘点单未确认]

		/// <summary>
		/// 获取单个实体[盘点单未确认]
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseStocktakingItem GetSingleUnconfirmed(int id, IDbContext context = null) {
			return WarehouseStocktakingItemRepository.GetInstance().GetSingleUnconfirmed(id, context);
		}

		#endregion
	}
}





