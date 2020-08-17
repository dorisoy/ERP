using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseOutboundPrintBatchService  : BaseService<WarehouseOutboundPrintBatch> {
    
        #region Update
        
		public static int Update(WarehouseOutboundPrintBatch entity, IDbContext context = null) {
			return WarehouseOutboundPrintBatchRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehouseOutboundPrintBatch entity, IDbContext context = null) {
			return WarehouseOutboundPrintBatchRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehouseOutboundPrintBatch GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehouseOutboundPrintBatchRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return WarehouseOutboundPrintBatchRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 删除操作  通过出库单ID

		/// <summary>
		/// 删除操作  通过出库单ID
		/// </summary>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库对象</param>
		/// <returns></returns>
		public static int DelByOutboundID(int outboundID, IDbContext context = null) {
			return WarehouseOutboundPrintBatchRepository.GetInstance().DelByOutboundID(outboundID, context);
		}

		#endregion             
	}
}





