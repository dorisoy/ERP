using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehousePurchaseItemService  : BaseService<WarehousePurchaseItem> {
    
        #region Update
        
		public static int Update(WarehousePurchaseItem entity, IDbContext context = null) {
			return WarehousePurchaseItemRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehousePurchaseItem entity, IDbContext context = null) {
			return WarehousePurchaseItemRepository.GetInstance().Add(entity, context);
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
		public static WarehousePurchaseItem GetSingleWarehousePurchaseItem(int purchaseID, int productsSkuID, IDbContext context = null) {
			return WarehousePurchaseItemRepository.GetInstance().GetSingleWarehousePurchaseItem(purchaseID, productsSkuID, context);
		}

		#endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehousePurchaseItem GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehousePurchaseItemRepository.GetInstance().GetQuerySingleByID(id, context);
	    }
    
	    #endregion

		#region 添加商品时获取SKU列表

		/// <summary>
		/// 添加商品时获取SKU列表
		/// </summary>
		/// <param name="data">sql语句实体类</param>
		/// <param name="count">总条数</param>
		/// <returns></returns>
		public static List<WarehousePurchaseSkuList> GetQueryManyForSkuList(SelectBuilder data, out int count) {
			BaseRepository<WarehousePurchaseSkuList> obj = new BaseRepository<WarehousePurchaseSkuList>();
			return obj.GetQueryManyForPage(data, out  count);
		}

		#endregion
        
     	#region 根据采购单商品表ID删除记录

		/// <summary>
		/// 根据采购单商品表ID删除记录
		/// </summary>
		/// <param name="purchaseItemIDList">采购单商品表ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Delete(List<int> purchaseItemIDList, IDbContext context = null) {
			return WarehousePurchaseItemRepository.GetInstance().Delete(purchaseItemIDList, context);
		}

		#endregion

		#region 根据采购单ID删除记录

		/// <summary>
		/// 根据采购单ID删除记录
		/// </summary>
		/// <param name="purchaseID">采购单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DeleteByPurchaseID(int purchaseID, IDbContext context = null) {
			return WarehousePurchaseItemRepository.GetInstance().DeleteByPurchaseID(purchaseID, context);
	    }

		#endregion

		#region 根据采购单ID获取所有采购单商品

		/// <summary>
		/// 根据采购单ID获取所有采购单商品
		/// </summary>
		/// <param name="purchaseID">采购单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehousePurchaseItem> GetWarehousePurchaseItemList(int purchaseID, IDbContext context = null) {
			return WarehousePurchaseItemRepository.GetInstance().GetWarehousePurchaseItemList(purchaseID, context);
		}

		#endregion

		#region 根据采购单商品表ID列表获取采购单商品
		
		/// <summary>
		/// 根据采购单商品表ID列表获取采购单商品
		/// </summary>
		/// <param name="purchaseItemIDList">采购单商品表ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehousePurchaseItem> GetWarehousePurchaseItemList(List<int> purchaseItemIDList, IDbContext context = null) {
			return WarehousePurchaseItemRepository.GetInstance().GetWarehousePurchaseItemList(purchaseItemIDList, context);
		}

		#endregion

		#region 根据页面条件获取采购单商品ID列表

		/// <summary>
		/// 根据页面条件获取采购单商品ID列表
		/// </summary>
		/// <param name="data">sql语句实体类</param>
		/// <param name="count">总条数</param>
		/// <returns></returns>
		public static List<int> GetPurchaseItemIDListForPage(SelectBuilder data, out int count) {
			BaseRepository<WarehousePurchaseItem> obj = new BaseRepository<WarehousePurchaseItem>();
			List<WarehousePurchaseItem> list = obj.GetQueryManyForPage(data, out  count);
			List<int> purchaseItemIDList = new List<int>();
			purchaseItemIDList.AddRange(list.Select(row => row.ID));
			return purchaseItemIDList;
		}

		#endregion

		#region 修改采购单商品的采购数量

		/// <summary>
		/// 修改采购单商品的采购数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseItemID">采购单商品表主键ID</param>
		/// <param name="newNum">要更新数量</param>
		/// <param name="context">数据库连接对象</param>
		public static int UpdateNum(string userCode, int purchaseItemID, int newNum, IDbContext context = null) {
			return WarehousePurchaseItemRepository.GetInstance().UpdateNum(userCode, purchaseItemID, newNum, context);
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
		public static int UpdateInStockNum(string userCode, int purchaseItemID, int diffNum, IDbContext context = null) {
			return WarehousePurchaseItemRepository.GetInstance().UpdateInStockNum(userCode, purchaseItemID, diffNum, context);
		}

		#endregion
	}
}





