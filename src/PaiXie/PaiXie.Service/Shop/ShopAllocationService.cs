using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class ShopAllocationService  : BaseService<ShopAllocation> {
    
        #region Update
        
		public static int Update(ShopAllocation entity, IDbContext context = null) {
			return ShopAllocationRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(ShopAllocation entity, IDbContext context = null) {
			return ShopAllocationRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static ShopAllocation GetQuerySingleByID(int id, IDbContext context = null) {
		    return ShopAllocationRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return ShopAllocationRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 私有库存列表

	/// <summary>
		/// 私有库存列表
	/// </summary>
	/// <param name="ProductsID"></param>
	/// <param name="context"></param>
	/// <returns></returns>
	    public static DataTable GetshopAllocationDataTable(int ProductsID, IDbContext context = null) {
		    return ShopAllocationRepository.GetInstance().GetshopAllocationDataTable(ProductsID, context);
	    }
    
        #endregion             

		#region 删除  独享  根据店铺  商品

		/// <summary>
		/// 删除  独享  根据店铺  商品
	/// </summary>
	/// <param name="ProductsID"></param>
	/// <param name="context"></param>
	/// <returns></returns>
	    public static int Del(string shopid, string ProductsID, IDbContext context = null) {
		    return ShopAllocationRepository.GetInstance().Del( shopid,  ProductsID, context);
	    }
    
        #endregion     
        
		#region 独享 数量

		/// <summary>
		/// 独享 数量
	/// </summary>
	/// <param name="ProductsID"></param>
	/// <param name="context"></param>
	/// <returns></returns>
	    public static string SaleInventory(int ProductsSkuID, int shopid, int ProductsID, IDbContext context = null) {
		    return ShopAllocationRepository.GetInstance().SaleInventory(  ProductsSkuID,  shopid,  ProductsID, context);
	    }
    
        #endregion   

		#region  是否  独享

		/// <summary>
		///  是否  独享
	/// </summary>
	/// <param name="ProductsID"></param>
	/// <param name="context"></param>
	/// <returns></returns>
	    public static string IsSalePub( int shopid, int ProductsID, IDbContext context = null) {
		    return ShopAllocationRepository.GetInstance().IsSalePub(    shopid,  ProductsID, context);
	    }
    
        #endregion   

		#region 获取单个实体 通过 SKU  SHOPID
		/// <summary>
		/// 获取单个实体 通过 SKU  SHOPID
		/// </summary>
		/// <param name="ShopID"></param>
		/// <param name="ProductsSkuID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static ShopAllocation GetQuerySingle(int ShopID, int ProductsSkuID, IDbContext context = null) {
			return ShopAllocationRepository.GetInstance().GetQuerySingle(ShopID, ProductsSkuID, context);
	 
		}

		#endregion

		#region 获取单个实体 通过 ProductsID
		/// <summary>
		/// 获取单个实体 通过 ProductsID
		/// </summary>
		/// <param name="ProductsID">ProductsID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<ShopAllocation> GetQuerySingleByProductsID(int ProductsID, IDbContext context = null) {
			return ShopAllocationRepository.GetInstance().GetQuerySingleByProductsID(ProductsID, context); 
		}

		#endregion

		#region 获取单个实体 通过 ProductsSkuID
		/// <summary>
		/// 获取单个实体 通过 ProductsSkuID
		/// </summary>
		/// <param name="ProductsSkuID">ProductsSkuID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<ShopAllocation> GetQuerySingleByProductsSkuID(int ProductsSkuID, IDbContext context = null) {
			return ShopAllocationRepository.GetInstance().GetQuerySingleByProductsSkuID(ProductsSkuID, context);
		}

		#endregion
	}
}