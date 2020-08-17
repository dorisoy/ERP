using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class SuppliersItemService  : BaseService<SuppliersItem> {
    
        #region Update
        
		public static int Update(SuppliersItem entity, IDbContext context = null) {
			return SuppliersItemRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(SuppliersItem entity, IDbContext context = null) {
			return SuppliersItemRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static SuppliersItem GetQuerySingleByID(int id, IDbContext context = null) {
		    return SuppliersItemRepository.GetInstance().GetQuerySingleByID(id, context);
	    }
    
	    #endregion

		#region 添加商品时获取SKU列表

		/// <summary>
		/// 添加商品时获取SKU列表
		/// </summary>
		/// <param name="data">sql语句实体类</param>
		/// <param name="count">总条数</param>
		/// <returns></returns>
		public static List<SuppliersSkuList> GetQueryManyForSkuList(SelectBuilder data, out int count) {
			BaseRepository<SuppliersSkuList> obj = new BaseRepository<SuppliersSkuList>();
			return obj.GetQueryManyForPage(data, out  count);
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
		    return SuppliersItemRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 根据商品ID查询关联供应商

		/// <summary>
		/// 根据商品ID查询关联供应商
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static DataTable GetQueryManyByProductsID(int productsID, IDbContext context = null) {
			return SuppliersItemRepository.GetInstance().GetQueryManyByProductsID(productsID, context);
		}

		#endregion

		#region 根据商品SKUID查询关联供应商

		/// <summary>
		/// 根据商品SKUID查询关联供应商
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static DataTable GetQueryManyByProductsSkuID(int productsSkuID, IDbContext context = null) {
			return SuppliersItemRepository.GetInstance().GetQueryManyByProductsSkuID(productsSkuID, context);
		}

		#endregion

		#region 设置供应商为SKU默认供应商，并清除之前的默认供应商

		/// <summary>
		/// 设置当前供应商为SKU默认供应商，并清除之前的默认供应商
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="suppliersID">供应商ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateIsDefault(int productsSkuID, int suppliersID, IDbContext context = null) {
			return SuppliersItemRepository.GetInstance().UpdateIsDefault(productsSkuID, suppliersID, context);
		}

		#endregion

		#region 根据商品SKUID获取默认供应商

		/// <summary>
		/// 根据商品SKUID获取默认供应商
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetDefaultSuppliersID(int productsSkuID, IDbContext context = null) {
			return SuppliersItemRepository.GetInstance().GetDefaultSuppliersID(productsSkuID, context);
		}

		#endregion

		#region 根据商品SKUID和供应商ID获取单个实体

		/// <summary>
		/// 根据商品SKUID和供应商ID获取单个实体
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="suppliersID">供应商ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static SuppliersItem GetSingleSuppliersItem(int productsSkuID, int suppliersID, IDbContext context = null) {
			return SuppliersItemRepository.GetInstance().GetSingleSuppliersItem(productsSkuID, suppliersID, context);
		}

		#endregion

		#region 获取供应商商品数量

		/// <summary>
		/// 获取供应商商品数量
		/// </summary>
		/// <param name="suppliersID">供应商ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetProductsCount(int suppliersID, IDbContext context = null) {
			return SuppliersItemRepository.GetInstance().GetProductsCount(suppliersID, context);
		}

		#endregion

		#region 根据商品ID删除供应商商品

		/// <summary>
		/// 根据商品ID删除供应商商品
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DelByProductsID(int productsID, IDbContext context = null) {
			return SuppliersItemRepository.GetInstance().DelByProductsID(productsID, context);
		}

		#endregion

		#region 根据供应商ID和商品ID获取到货周期

		/// <summary>
		/// 根据供应商ID和商品ID获取到货周期
		/// </summary>
		/// <param name="suppliersID">供应商ID</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static string GetArrivalCycle(int suppliersID, int productsID, IDbContext context = null) {
			return SuppliersItemRepository.GetInstance().GetArrivalCycle(suppliersID, productsID, context);
		}

		#endregion
	}
}





