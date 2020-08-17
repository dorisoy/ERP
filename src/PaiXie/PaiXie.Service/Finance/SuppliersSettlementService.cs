using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class SuppliersSettlementService  : BaseService<SuppliersSettlement> {
    
        #region Update
        
		public static int Update(SuppliersSettlement entity, IDbContext context = null) {
			return SuppliersSettlementRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(SuppliersSettlement entity, IDbContext context = null) {
			return SuppliersSettlementRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static SuppliersSettlement GetQuerySingleByID(int id, IDbContext context = null) {
		    return SuppliersSettlementRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return SuppliersSettlementRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion     
        
		#region 获取单个实体 通过SourceID

		/// <summary>
		/// 获取单个实体 通过SourceID
	    /// </summary>
		/// <param name="id">SourceID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static SuppliersSettlement GetQuerySingleBySourceID(int SourceID, IDbContext context = null) {
		    return SuppliersSettlementRepository.GetInstance().GetQuerySingleBySourceID(SourceID, context);
	    }
    
	    #endregion		
	}
}