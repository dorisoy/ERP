using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseExpressPriceService  : BaseService<WarehouseExpressPrice> {
    
        #region Update
        
		public static int Update(WarehouseExpressPrice entity, IDbContext context = null) {
			return WarehouseExpressPriceRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehouseExpressPrice entity, IDbContext context = null) {
			return WarehouseExpressPriceRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehouseExpressPrice GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehouseExpressPriceRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return WarehouseExpressPriceRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 根据快递公司ID获取多个实体

		/// <summary>
		/// 根据快递公司ID获取多个实体
		/// </summary>
		/// <param name="expressID">快递公司ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehouseExpressPrice> GetQueryManyByExpressID(int expressID, IDbContext context = null) {
			return WarehouseExpressPriceRepository.GetInstance().GetQueryManyByExpressID(expressID, context);
		}

		#endregion

		#region 检查地区是否已经存在

		/// <summary>
		/// 检查地区是否已经存在
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="expressID">快递公司ID</param>
		/// <param name="sysAreaName">地区名称</param>
		/// <param name="id">运费记录表主键ID 添加时传0，修改才传值</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static bool IsExists(string warehouseCode, int expressID, string sysAreaName, int id, IDbContext context = null) {
			return WarehouseExpressPriceRepository.GetInstance().IsExists(warehouseCode, expressID, sysAreaName, id, context);
		}

		#endregion

		#region 根据快递公司ID和市级地区ID获取单个实体

		/// <summary>
		/// 根据快递公司ID和市级地区ID获取单个实体
		/// </summary>
		/// <param name="expressID">快递公司ID</param>
		/// <param name="cityID">市级地区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseExpressPrice GetQuerySingleByExpressIDAndCityID(int expressID, int cityID, IDbContext context = null) {
			return WarehouseExpressPriceRepository.GetInstance().GetQuerySingleByExpressIDAndCityID(expressID, cityID, context);
		}

		#endregion

		#region 根据快递公司ID和省级地区ID获取单个实体

		/// <summary>
		/// 根据快递公司ID和省级地区ID获取单个实体
		/// </summary>
		/// <param name="expressID">快递公司ID</param>
		/// <param name="provinceID">省级地区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseExpressPrice GetQuerySingleByExpressIDAndProvinceID(int expressID, int provinceID, IDbContext context = null) {
			return WarehouseExpressPriceRepository.GetInstance().GetQuerySingleByExpressIDAndProvinceID(expressID, provinceID, context);
		}

		#endregion

		#region 根据快递公司ID和全国获取单个实体

		/// <summary>
		/// 根据快递公司ID和全国获取单个实体
		/// </summary>
		/// <param name="expressID">快递公司ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseExpressPrice GetQuerySingleByExpressID(int expressID, IDbContext context = null) {
			return WarehouseExpressPriceRepository.GetInstance().GetQuerySingleByExpressID(expressID, context);
		}

		#endregion
	}
}





