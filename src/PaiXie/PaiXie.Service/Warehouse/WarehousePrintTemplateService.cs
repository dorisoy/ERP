using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehousePrintTemplateService  : BaseService<WarehousePrintTemplate> {
    
        #region Update
        
		public static int Update(WarehousePrintTemplate entity, IDbContext context = null) {
			return WarehousePrintTemplateRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehousePrintTemplate entity, IDbContext context = null) {
			return WarehousePrintTemplateRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehousePrintTemplate GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehousePrintTemplateRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return WarehousePrintTemplateRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 根据模版名称和模版类型获取模版ID

		/// <summary>
		/// 根据模版名称和模版类型获取模版ID
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="name">模版名称</param>
		/// <param name="typeID">模版类型</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetPrintTemplateID(string warehouseCode, string name, int typeID, IDbContext context = null) {
			return WarehousePrintTemplateRepository.GetInstance().GetPrintTemplateID(warehouseCode, name, typeID, context);
		}

		#endregion

		#region 根据模版名称和模版类型获取排除指定模版ID之外的模版ID(修改模版时使用)

		/// <summary>
		/// 根据模版名称和模版类型获取排除指定模版ID之外的模版ID(修改模版时使用)
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="name">模版名称</param>
		/// <param name="typeID">模版类型</param>
		/// <param name="exceptID">需要排除模版ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetPrintTemplateID(string warehouseCode, string name, int typeID, int exceptID, IDbContext context = null) {
			return WarehousePrintTemplateRepository.GetInstance().GetPrintTemplateID(warehouseCode, name, typeID, exceptID, context);
		}

		#endregion

		#region 根据模版类型取消指定ID之外的默认模版

		/// <summary>
		/// 根据模版类型取消指定ID之外的默认模版
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="typeID">模版类型 枚举值</param>
		/// <param name="exceptID">排除的ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int CancelDefault(string userCode, string warehouseCode, int typeID, int exceptID, IDbContext context = null) {
			return WarehousePrintTemplateRepository.GetInstance().CancelDefault(userCode, warehouseCode, typeID, exceptID, context);
		}

		#endregion

		#region 保存打印模版

		/// <summary>
		/// 保存打印模版
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="width">宽度</param>
		/// <param name="height">高度</param>
		/// <param name="id">打印模版ID</param>
		/// <param name="templateContent">模版内容</param>
		/// <param name="secondPageOffset">次页打印偏移</param>
		/// <param name="printerName">默认打印机名称 如果是恢复默认设置，不要传该参数</param>
		/// <param name="isPrintPro">是否打印商品明细 0否 1是 如果是恢复默认设置，不要传该参数</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int SavePrintTemplate(string userCode, string warehouseCode, int id, decimal width, decimal height, string templateContent, decimal secondPageOffset, string printerName = null, int? isPrintPro = null, IDbContext context = null) {
			return WarehousePrintTemplateRepository.GetInstance().SavePrintTemplate(userCode, warehouseCode, id, width, height, templateContent, secondPageOffset, printerName, isPrintPro, context);
		}

		#endregion

		#region 保存打印明细字段

		/// <summary>
		/// 保存打印明细字段
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="id">打印模版ID</param>
		/// <param name="skuFields">明细字段</param>
		/// <param name="printProFieldWidth">明细字段宽度占比</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int SavePrintPro(string userCode, string warehouseCode, int id, string skuFields, string printProFieldWidth, IDbContext context = null) {
			return WarehousePrintTemplateRepository.GetInstance().SavePrintPro(userCode, warehouseCode, id, skuFields, printProFieldWidth, context);
		}

		#endregion
	}
}





