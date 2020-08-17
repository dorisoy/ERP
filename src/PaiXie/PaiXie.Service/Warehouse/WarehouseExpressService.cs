using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseExpressService  : BaseService<WarehouseExpress> {
    
        #region Update
        
		public static int Update(WarehouseExpress entity, IDbContext context = null) {
			return WarehouseExpressRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehouseExpress entity, IDbContext context = null) {
			return WarehouseExpressRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehouseExpress GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehouseExpressRepository.GetInstance().GetQuerySingleByID(id, context);
	    }
    
	    #endregion

		#region 根据快递名称获取快递ID

		/// <summary>
		/// 根据快递名称获取快递ID
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="name">快递名称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetExpressID(string warehouseCode, string name, IDbContext context = null) {
			return WarehouseExpressRepository.GetInstance().GetExpressID(warehouseCode, name, context);
		}

		#endregion

		#region 根据快递名称获取排除指定快递ID之外的快递ID(修改快递时使用)

		/// <summary>
		/// 根据快递名称获取排除指定快递ID之外的快递ID(修改快递时使用)
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="name">快递名称</param>
		/// <param name="exceptID">需要排除快递ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetExpressID(string warehouseCode, string name, int exceptID, IDbContext context = null) {
			return WarehouseExpressRepository.GetInstance().GetExpressID(warehouseCode, name, exceptID, context);
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
		    return WarehouseExpressRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 保存快递打印模版

		/// <summary>
		/// 保存快递打印模版
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="width">宽度</param>
		/// <param name="height">高度</param>
		/// <param name="id">快递公司ID</param>
		/// <param name="templateContent">模版内容</param>
		/// <param name="expressPrinterName">默认打印机名称 如果是恢复默认设置，不要传该参数</param>
		/// <param name="isPrintPro">是否打印商品明细 0否 1是 如果是恢复默认设置，不要传该参数</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int SavePrintTemplate(string userCode, string warehouseCode, int id, decimal width, decimal height, string templateContent, string expressPrinterName = null, int? isPrintPro = null, IDbContext context = null) {
			return WarehouseExpressRepository.GetInstance().SavePrintTemplate(userCode, warehouseCode, id, width, height, templateContent, expressPrinterName, isPrintPro, context);
		}

		#endregion

		#region 保存打印明细字段

		/// <summary>
		/// 保存打印明细字段
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="id">快递公司ID</param>
		/// <param name="skuFields">明细字段</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int SavePrintPro(string userCode, string warehouseCode, int id, string skuFields, IDbContext context = null) {
			return WarehouseExpressRepository.GetInstance().SavePrintPro(userCode, warehouseCode, id, skuFields, context);
		}

		#endregion

		#region 根据物流ID获取快递列表

		/// <summary>
		/// 根据物流ID获取快递列表
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="logisticsID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<WarehouseExpress> GetManyExpress(string warehouseCode, int logisticsID, IDbContext context = null) {
			return WarehouseExpressRepository.GetInstance().GetManyExpress(warehouseCode, logisticsID, context);
		}

		#endregion
	}
}





