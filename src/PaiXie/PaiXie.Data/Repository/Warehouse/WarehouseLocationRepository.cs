using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Converters;

namespace PaiXie.Data 
{
	public class WarehouseLocationRepository : BaseRepository<WarehouseLocation> {

		#region 构造函数
		private static WarehouseLocationRepository _instance;
		public static WarehouseLocationRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehouseLocationRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(WarehouseLocation entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehouseLocation>("warehouseLocation", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region 获取单个实体 通过主键ID
		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual WarehouseLocation GetQuerySingleByID(int ID, IDbContext context = null) {

			if (context == null) context = Db.GetInstance().Context();

			string sql = "SELECT  *  FROM warehouseLocation  WHERE ID=" + ID;
			WarehouseLocation obj = context.Sql(sql).QuerySingle<WarehouseLocation>();

			return obj;
		}
		#endregion

		#region 删除操作  通过ID
		/// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int DelByID(int ID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ID;
			string sqlStr = "DELETE FROM warehouseLocation Where ID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 删除库区下的库位

		/// <summary>
		/// 删除库区下的库位
		/// </summary>
		/// <param name="topLocationID">库区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int DelLocationByTopLocationID(int topLocationID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = topLocationID;
			string sqlStr = "DELETE FROM warehouseLocation Where ParentID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region Update
		public int Update(WarehouseLocation entity, out string oldMessage, out string newMessage, IDbContext context = null) {
			oldMessage = string.Empty;
			newMessage = string.Empty;
			if (context == null) context = Db.GetInstance().Context();
			//更新之前要先查出来，并存放到 oldMessage
			WarehouseLocation newWarehouseLocation = GetQuerySingleByID(entity.ID, context);
			oldMessage = JsonConvert.SerializeObject(newWarehouseLocation, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
			newWarehouseLocation.Name = entity.Name;
			newWarehouseLocation.Code = entity.Code;
			newWarehouseLocation.TypeID = entity.TypeID;
			newWarehouseLocation.UpdatePerson = entity.UpdatePerson;
			newWarehouseLocation.UpdateDate = entity.UpdateDate;
			int rowsAffected = context.Update<WarehouseLocation>("warehouseLocation", newWarehouseLocation)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			if (rowsAffected > 0) {
				newMessage = JsonConvert.SerializeObject(newWarehouseLocation, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
			}
			return rowsAffected;
		}
		#endregion

		#region 获取库区下所有子节点

		/// <summary>
		/// 获取库区下所有子节点
		/// </summary>
		/// <param name="parentID">父级ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<WarehouseLocation> GetChildrenList(int parentID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = parentID;
			string sqlStr = "SELECT * FROM warehouseLocation Where ParentID=@0";
			List<WarehouseLocation> locationList = GetQueryMany(sqlStr, context, objects);
			return locationList;
		}

		#endregion

		#region 根据代码获取ID

		/// <summary>
		/// 根据代码和仓库编码获取ID
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="code">代码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetWarehouseLocationID(string warehouseCode, string code, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = code;
			objects[1] = warehouseCode;
			string sqlStr = "SELECT ID FROM warehouseLocation WHERE Code=@0 and WarehouseCode=@1";
			WarehouseLocation warehouseLocation = GetQuerySingle(sqlStr, context, objects);
			int warehouseLocationID = 0;
			if (warehouseLocation != null) {
				warehouseLocationID = warehouseLocation.ID;
			}
			return warehouseLocationID;
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
		public int GetWarehouseLocationID(string warehouseCode, string code, int exceptID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = code;
			objects[1] = exceptID;
			objects[2] = warehouseCode;
			string sqlStr = "SELECT ID FROM warehouseLocation WHERE Code=@0 and ID<>@1 and WarehouseCode=@2";
			WarehouseLocation warehouseLocation = GetQuerySingle(sqlStr, context, objects);
			int warehouseLocationID = 0;
			if (warehouseLocation != null) {
				warehouseLocationID = warehouseLocation.ID;
			}
			return warehouseLocationID;
		}

		#endregion

		#region 获取指定库位ID的商品库存信息

		/// <summary>
		/// 获取指定库位ID的商品库存信息
		/// </summary>
		/// <param name="locationID">库位ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public DataTable GetLocationProductsKucInfo(int locationID, IDbContext context = null) {
			//预售和占用数量，等待实现
			Object[] objects = new Object[1];
			objects[0] = locationID;
			string sqlStr = @"SELECT p.Code AS ProductsCode,p.Name AS ProductsName,ps.Saleprop,ps.Code AS ProductsSkuCode,wlp.ProductsBatchCode,wlp.ZkNum,wlp.ZyNum,wlp.DjNum,wlp.KyNum FROM warehouseLocationProducts wlp
			LEFT JOIN products p ON wlp.ProductsID=p.ID
			LEFT JOIN productsSku ps ON wlp.ProductsSkuID=ps.ID
			WHERE wlp.LocationID=@0 
			ORDER BY wlp.ID DESC";
			return GetDataTable(sqlStr, context, objects);
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
		public WarehouseLocation GetSingleWarehouseLocation(string warehouseCode, int typeID, IDbContext context = null) {
			string sqlStr = @"SELECT *
                              FROM warehouseLocation
                              WHERE WarehouseCode = @0 AND TypeID = @1";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = typeID;
			return GetQuerySingle(sqlStr, context, objects);
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
		public WarehouseLocation GetSingleSubWarehouseLocation(string warehouseCode, int typeID, IDbContext context = null) {
			string sqlStr = @"SELECT *
                              FROM warehouseLocation
                              WHERE WarehouseCode = @0 AND TypeID = @1 AND ParentID>0";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = typeID;
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 获取库区列表

		/// <summary>
		/// 获取库区列表
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public List<WarehouseLocation> GetManyWarehouseLocation(string warehouseCode,IDbContext context = null) {
			string sqlStr = @"SELECT *
                              FROM warehouseLocation
                              WHERE WarehouseCode = @0 AND ParentID = 0 ORDER BY TypeID DESC";
			Object[] objects = new Object[1];
			objects[0] = warehouseCode;
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 获取指定库区ID的商品库存信息

		/// <summary>
		/// 获取指定库区ID的商品库存信息
		/// </summary>
		/// <param name="topLocationID">库区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public DataTable GetTopLocationProducts(int topLocationID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = topLocationID;
			string sqlStr = @"SELECT p.Code AS ProductsCode,p.Name AS ProductsName,p.No AS ProductsNo,ps.Saleprop AS ProductsSkuSaleprop,ps.Code AS ProductsSkuCode,wl.Code AS LocationCode,wl.Name AS LocationName,
                              wlp.ID AS LocationProductsID,wlp.TopLocationID,wlp.LocationID,wlp.LocationTypeID,wlp.ProductsID,wlp.ProductsSkuID,wlp.ProductsBatchID,wlp.ProductsBatchCode,wlp.ProductionDate,wlp.ShelfLife,wlp.KyNum,wlp.ZyNum,wlp.DjNum,wlp.SdNum,wlp.ZkNum
                              FROM warehouseLocationProducts wlp
							  INNER JOIN warehouseLocation wl on wl.ID = wlp.LocationID
							  LEFT JOIN products p ON wlp.ProductsID=p.ID
							  LEFT JOIN productsSku ps ON wlp.ProductsSkuID=ps.ID
							  WHERE wlp.TopLocationID=@0 and wlp.ZkNum > 0
							  ORDER BY wlp.ID DESC";
			return GetDataTable(sqlStr, context, objects);
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
		public WarehouseLocation GetSingleWarehouseLocation(string warehouseCode, string code, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = code;
			objects[1] = warehouseCode;
			string sqlStr = "SELECT * FROM warehouseLocation WHERE Code=@0 and WarehouseCode=@1";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 获取指定库区ID的库位数量

		/// <summary>
		/// 获取指定库区ID的库位数量
		/// </summary>
		/// <param name="topLocationID">库区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetLocationNum(int topLocationID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = topLocationID;
			string sqlStr = "SELECT Count(0) FROM warehouseLocation WHERE ParentID=@0";
			return GetCount(sqlStr, context, objects);
		}

		#endregion

		#region 获取单个实体 通过主键ID
		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual WarehouseLocation GetQuerySingleByParentID(int ID, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			string sql = "SELECT  *  FROM warehouseLocation  WHERE ParentID=" + ID;
			WarehouseLocation obj = context.Sql(sql).QuerySingle<WarehouseLocation>();
			return obj;
		}
		#endregion
	}
}





