using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class WarehouseAreaStructRepository : BaseRepository<WarehouseAreaStruct> {

		#region 构造函数
		private static WarehouseAreaStructRepository _instance;
		public static WarehouseAreaStructRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehouseAreaStructRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(WarehouseAreaStruct entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehouseAreaStruct>("warehouseAreaStruct", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region Update
		public int Update(WarehouseAreaStruct entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehouseAreaStruct>("warehouseAreaStruct", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
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
		public int GetWarehouseAreaStructID(string warehouseCode, string areaStructName, int parentID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = areaStructName;
			objects[1] = parentID;
			objects[2] = warehouseCode;
			string sqlStr = "SELECT ID FROM warehouseAreaStruct WHERE Name=@0 AND ParentID=@1 AND WarehouseCode=@2";
			WarehouseAreaStruct warehouseAreaStruct = GetQuerySingle(sqlStr, context, objects);
			int warehouseAreaStructID = 0;
			if (warehouseAreaStruct != null) {
				warehouseAreaStructID = warehouseAreaStruct.ID;
			}
			return warehouseAreaStructID;
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
		public int GetWarehouseAreaStructID(string warehouseCode, string areaStructName, int parentID, int exceptWarehouseAreaStructID, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = areaStructName;
			objects[1] = parentID;
			objects[2] = exceptWarehouseAreaStructID;
			objects[3] = warehouseCode;
			string sqlStr = "SELECT ID FROM warehouseAreaStruct WHERE Name=@0 and ParentID=@1 and ID<>@2 AND WarehouseCode=@2";
			WarehouseAreaStruct warehouseAreaStruct = GetQuerySingle(sqlStr, context, objects);
			int warehouseAreaStructID = 0;
			if (warehouseAreaStruct != null) {
				warehouseAreaStructID = warehouseAreaStruct.ID;
			}
			return warehouseAreaStructID;
		}

		#endregion

		#region 删除库区结构

		/// <summary>
		/// 删除库区结构
		/// </summary>
		/// <param name="warehouseAreaStructID">库区结构ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Del(int warehouseAreaStructID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = warehouseAreaStructID;
			string sqlStr = "DELETE FROM warehouseAreaStruct Where ID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据库区结构ID获取库区结构信息

		/// <summary>
		/// 根据库区结构ID获取库区结构信息
		/// </summary>
		/// <param name="warehouseAreaStructID">库区结构ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public WarehouseAreaStruct GetSingleWarehouseAreaStruct(int warehouseAreaStructID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = warehouseAreaStructID;
			string sqlStr = "SELECT * FROM warehouseAreaStruct Where ID=@0";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 根据父级库区结构ID获取直属子级库区结构ID列表

		/// <summary>
		/// 根据父级库区结构ID获取直属子级库区结构ID列表
		/// </summary>
		/// <param name="parentID">父级库区结构ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<int> GetChildWarehouseAreaStructID(int parentID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = parentID;
			string sqlStr = "SELECT ID FROM warehouseAreaStruct WHERE ParentID=@0";
			List<WarehouseAreaStruct> warehouseAreaStructList = GetQueryMany(sqlStr, context, objects);
			List<int> warehouseAreaStructIDList = new List<int>();
			foreach (var item in warehouseAreaStructList) {
				warehouseAreaStructIDList.Add(item.ID);
			}
			return warehouseAreaStructIDList;
		}

		#endregion

		#region 根据父级结构ID递归获取所有子结构 排序从上到下

		/// <summary>
		/// 根据父级结构ID递归获取所有子结构 排序从上到下
		/// </summary>
		/// <param name="parentID">父级库区结构ID</param>
		/// <param name="warehouseAreaStructList">子结构列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public void GetChildWarehouseAreaStruct(int parentID, ref List<WarehouseAreaStruct> warehouseAreaStructList, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = parentID;
			string sqlStr = "SELECT * FROM warehouseAreaStruct WHERE ParentID=@0";
			List<WarehouseAreaStruct> currentWarehouseAreaStructList = GetQueryMany(sqlStr, context, objects);
			foreach (var item in currentWarehouseAreaStructList) {
				warehouseAreaStructList.Add(item);
				GetChildWarehouseAreaStruct(item.ID, ref warehouseAreaStructList, context);
			}
		}

		#endregion
	}
}





