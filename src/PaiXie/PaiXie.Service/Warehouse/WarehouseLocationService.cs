using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
using PaiXie.Core;
namespace PaiXie.Service 
{
 	public class WarehouseLocationService  : BaseService<WarehouseLocation> {

		#region Update

		public static int Update(WarehouseLocation entity, out string oldMessage, out string newMessage, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().Update(entity, out oldMessage, out newMessage, context);
		}

		#endregion

		#region Add

		public static int Add(WarehouseLocation entity, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region 获取单个实体 通过主键ID
		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static WarehouseLocation GetQuerySingleByID(int ID, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().GetQuerySingleByID(ID, context);
		}

		#endregion

		#region 删除操作  通过ID

		/// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int DelByID(int ID, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().DelByID(ID, context = null);
		}

		#endregion

		#region 获取子节点ID

		/// <summary>
		/// 获取子节点ID
		/// </summary>
		/// <param name="parentID">父级ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<int> GetChildrenList(int parentID, IDbContext context = null) {
			List<int> idList = new List<int>();
			List<WarehouseLocation> locationList = WarehouseLocationRepository.GetInstance().GetChildrenList(parentID, context);
			foreach (var item in locationList) {
				idList.Add(item.ID);
			}
			return idList;
		}

		#endregion

		#region 删除库区下的库位

		/// <summary>
		/// 删除库区下的库位
		/// </summary>
		/// <param name="topLocationID">库区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DelLocationByTopLocationID(int topLocationID, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().DelLocationByTopLocationID(topLocationID, context);
		}

		#endregion

		#region 获取库区类型名称

		/// <summary>
		/// 获取库区类型名称
		/// </summary>
		/// <param name="typeID">库区类型</param>
		public static string GetTypeName(int typeID)
		{
			string typeName=string.Empty;
			switch(typeID){
				case (int)LocationType.备用区:
					typeName = LocationType.备用区.ToString();
					break;
				case (int)LocationType.发货区:
					typeName = LocationType.发货区.ToString();
					break;
				case (int)LocationType.废品区:
					typeName = LocationType.废品区.ToString();
					break;
				case (int)LocationType.中转区:
					typeName = LocationType.中转区.ToString();
					break;
			}
			return typeName;
		}

		#endregion

		#region 根据代码获取ID

		/// <summary>
		/// 根据代码获取ID
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="code">代码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetWarehouseLocationID(string warehouseCode, string code, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().GetWarehouseLocationID(warehouseCode,code, context);
		}

		#endregion

		#region 根据代码获取排除指定ID之外的ID(修改时使用)

		/// <summary>
		/// 根据代码获取排除指定ID之外的ID(修改时使用)
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="code">代码</param>
		/// <param name="exceptID">需要排除ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetWarehouseLocationID(string warehouseCode, string code, int exceptID, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().GetWarehouseLocationID(warehouseCode, code, exceptID, context);
		}

		#endregion

		#region 根据库区类型获取获取单个库区实体

		/// <summary>
		/// 根据库区类型获取获取单个库区实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="typeID">库区类型</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseLocation GetSingleWarehouseLocation(string warehouseCode, int typeID, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().GetSingleWarehouseLocation(warehouseCode, typeID, context);
		}

		#endregion


		#region 根据库区类型获取获取单个库位实体

		/// <summary>
		/// 根据库区类型获取获取单个库位实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="typeID">库区类型</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseLocation GetSingleSubWarehouseLocation(string warehouseCode, int typeID, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().GetSingleSubWarehouseLocation(warehouseCode, typeID, context);
		}

		#endregion

		#region 获取指定库位ID的商品库存信息

		/// <summary>
		/// 获取指定库位ID的商品库存信息
		/// </summary>
		/// <param name="locationID">库位ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static DataTable GetLocationProductsKucInfo(int locationID, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().GetLocationProductsKucInfo(locationID, context);
		}

		#endregion

		#region 获取库区列表

		/// <summary>
		/// 获取库区列表
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<WarehouseLocation> GetManyWarehouseLocation(string warehouseCode, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().GetManyWarehouseLocation(warehouseCode, context);
		}

		#endregion

		#region 获取指定库区ID的商品库存信息

		/// <summary>
		/// 获取指定库区ID的商品库存信息
		/// </summary>
		/// <param name="topLocationID">库区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static DataTable GetTopLocationProducts(int topLocationID, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().GetTopLocationProducts(topLocationID, context);
		}

		#endregion

		#region 根据代码获取单个实体

		/// <summary>
		/// 根据代码获取单个实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="code">代码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseLocation GetSingleWarehouseLocation(string warehouseCode, string code, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().GetSingleWarehouseLocation(warehouseCode, code, context);
		}

		#endregion

		#region 获取指定库区ID的库位数量

		/// <summary>
		/// 获取指定库区ID的库位数量
		/// </summary>
		/// <param name="topLocationID">库区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetLocationNum(int topLocationID, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().GetLocationNum(topLocationID, context);
		}

		#endregion

		#region 获取单个实体 通过 ParentID
		/// <summary>
		/// 获取单个实体 通过ParentID
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static WarehouseLocation GetQuerySingleByParentID(int ID, IDbContext context = null) {
			return WarehouseLocationRepository.GetInstance().GetQuerySingleByParentID(ID, context);
		}

		#endregion
	}
}





