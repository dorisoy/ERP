using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseAreaStructService  : BaseService<WarehouseAreaStruct> {

		#region Update

		public static int Update(WarehouseAreaStruct entity, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(WarehouseAreaStruct entity, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region 根据结构名称和父级ID获取结构ID

		/// <summary>
		/// 根据结构名称和父级ID获取结构ID
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="areaStructName">结构名称</param>
		/// <param name="parentID">父级ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetWarehouseAreaStructID(string warehouseCode, string areaStructName, int parentID, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().GetWarehouseAreaStructID(warehouseCode, areaStructName, parentID, context);
		}

		#endregion

		#region 根据结构名称和父级ID获取排除指定结构ID之外的结构ID(修改结构时使用)

		/// <summary>
		/// 根据结构名称和父级ID获取排除指定结构ID之外的结构ID(修改结构时使用)
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="areaStructName">结构名称</param>
		/// <param name="parentID">父级ID</param>
		/// <param name="exceptWarehouseAreaStructID">需要排除结构ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetWarehouseAreaStructID(string warehouseCode, string areaStructName, int parentID, int exceptWarehouseAreaStructID, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().GetWarehouseAreaStructID(warehouseCode, areaStructName, parentID, exceptWarehouseAreaStructID, context);
		}

		#endregion

		#region 删除库区结构

		/// <summary>
		/// 删除库区结构
		/// </summary>
		/// <param name="warehouseAreaStructID">库区结构ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Del(int warehouseAreaStructID, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().Del(warehouseAreaStructID, context);
		}

		#endregion

		#region 根据库区结构ID获取库区结构信息

		/// <summary>
		/// 根据库区结构ID获取库区结构信息
		/// </summary>
		/// <param name="warehouseAreaStructID">库区结构ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseAreaStruct GetSingleWarehouseAreaStruct(int warehouseAreaStructID, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().GetSingleWarehouseAreaStruct(warehouseAreaStructID, context);
		}

		#endregion

		#region 根据父级库区结构ID获取直属子级库区结构ID列表

		/// <summary>
		/// 根据父级库区结构ID获取直属子级库区结构ID列表
		/// </summary>
		/// <param name="parentID">父级库区结构ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<int> GetChildWarehouseAreaStructID(int parentID, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().GetChildWarehouseAreaStructID(parentID, context);
		}

		#endregion

		#region 根据父级结构ID递归获取所有子结构 排序从上到下

		/// <summary>
		/// 根据父级结构ID递归获取所有子结构 排序从上到下
		/// </summary>
		/// <param name="parentID">父级结构ID</param>
		/// <returns></returns>
		public static void GetChildWarehouseAreaStruct(int parentID, ref List<WarehouseAreaStruct> warehouseAreaStructList, IDbContext context = null) {
			WarehouseAreaStructRepository.GetInstance().GetChildWarehouseAreaStruct(parentID, ref warehouseAreaStructList, context);
		}

		#endregion
	}
}





