using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class ShopComancationService  : BaseService<ShopComancation> {
    
        #region Update
        
		public static int Update(ShopComancation entity, IDbContext context = null) {
			return ShopComancationRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(ShopComancation entity, IDbContext context = null) {
			return ShopComancationRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static ShopComancation GetQuerySingleByID(int id, IDbContext context = null) {
		    return ShopComancationRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return ShopComancationRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 公共库存比例

	/// <summary>
		/// 公共库存比例
	/// </summary>
	/// <param name="ShopID"></param>
	/// <param name="ProductsID"></param>
	/// <param name="context"></param>
	/// <returns></returns>
	    public static string Ranges(int ShopID, int ProductsID,IDbContext context = null) {
		    return ShopComancationRepository.GetInstance().Ranges( ShopID,  ProductsID, context);
	    }
    
        #endregion             

		#region 公共库存备注

	/// <summary>
		/// 公共库存备注
	/// </summary>
	/// <param name="ShopID"></param>
	/// <param name="ProductsID"></param>
	/// <param name="context"></param>
	/// <returns></returns>
	    public static string Remark(int ProductsID,IDbContext context = null) {
		    return ShopComancationRepository.GetInstance().Remark(   ProductsID, context);
	    }
    
        #endregion             

		#region 删除  公共库存分配

		/// <summary>
		/// 删除  公共库存分配
	/// </summary>
	/// <param name="ShopID"></param>
	/// <param name="ProductsID"></param>
	/// <param name="context"></param>
	/// <returns></returns>
	    public static  int Del(int ShopID, int ProductsID, IDbContext context = null) {
		    return ShopComancationRepository.GetInstance().Del( ShopID,  ProductsID, context);
	    }
    
        #endregion             			


		#region 获取单个实体
		/// <summary>
		/// 获取单个实体 
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ShopComancation GetQuerySingle(int ShopID, int ProductsID, IDbContext context = null) {
			return ShopComancationRepository.GetInstance().GetQuerySingle(ShopID, ProductsID, context);
	  
		}


		public static ShopComancation GetQuerySingle(int ShopID, IDbContext context = null) {
			return ShopComancationRepository.GetInstance().GetQuerySingle(ShopID, context);
	  
		}
		#endregion
	}
}