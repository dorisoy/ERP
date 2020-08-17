using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
using PaiXie.Data.ViewModel;
namespace PaiXie.Service {
	public class WarehouseOutInStockLogService : BaseService<WarehouseOutInStockLog> {

		#region Update

		public static int Update(WarehouseOutInStockLog entity, IDbContext context = null) {
			return WarehouseOutInStockLogRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(WarehouseOutInStockLog entity, IDbContext context = null) {
			return WarehouseOutInStockLogRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region 获取单个实体 通过主键ID

		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseOutInStockLog GetQuerySingleByID(int id, IDbContext context = null) {
			return WarehouseOutInStockLogRepository.GetInstance().GetQuerySingleByID(id, context);
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
			return WarehouseOutInStockLogRepository.GetInstance().DelByID(id, context);
		}

		#endregion

		#region 获取出入库数量统计

		/// <summary>
		/// 获取出入库数量统计
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="productsID"></param>
		/// <param name="productsSkuID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static DataTable GetManyOutInStockLog(string warehouseCode, int productsID, int productsSkuID, string startDate, string endDate, IDbContext context = null) {
			return WarehouseOutInStockLogRepository.GetInstance().GetManyOutInStockLog(warehouseCode, productsID, productsSkuID, startDate, endDate, context);
		}

		#endregion

		#region 获取期初或期末信息

		/// <summary>
		/// 获取期初或期末信息
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="productsID"></param>
		/// <param name="productsSkuID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static DataTable GetInitialOutInStockLog(string warehouseCode, int productsID, int productsSkuID, string startDate, string endDate, IDbContext context = null) {
			return WarehouseOutInStockLogRepository.GetInstance().GetInitialOutInStockLog(warehouseCode, productsID, productsSkuID, startDate, endDate, context);
		}

		#endregion

		#region 获取批次号出入库数量统计

		/// <summary>
		/// 获取批次号出入库数量统计
		/// </summary>
		/// <param name="productBatchCode"></param>
		/// <param name="productsID"></param>
		/// <param name="productsSkuID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static DataTable GetManyOutInStockLog(string productBatchCode, int productsID, int productsSkuID, IDbContext context = null) {
			return WarehouseOutInStockLogRepository.GetInstance().GetManyOutInStockLog(productBatchCode, productsID, productsSkuID, context);
		}

		#endregion
	}
}