using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
 public	class WarehouseAreaMapRepository:BaseRepository<WarehouseAreaMap> {

	 #region 构造函数
	 private static WarehouseAreaMapRepository _instance;
	 public static WarehouseAreaMapRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new WarehouseAreaMapRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(WarehouseAreaMap entity, IDbContext context = null) {
        if (context == null) context = Db.GetInstance().Context();
		 int Id = context.Insert<WarehouseAreaMap>("WarehouseAreaMap", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return Id;
	 }
	 #endregion

	 #region Update
	 public int Update(WarehouseAreaMap entity, IDbContext context = null) {
         if (context == null) context = Db.GetInstance().Context();
		 int rowsAffected = context.Update<WarehouseAreaMap>("WarehouseAreaMap", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	 #region 是否设置仓库区域
	 /// <summary>
	 /// 是否设置仓库区域
	 /// </summary>
	 /// <param name="areaid"></param>
	 /// <param name="wcode"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public int IswareaMap(string areaid, string wcode, IDbContext context = null) {
		 Object[] objects = new Object[2];
		 objects[0] = areaid;
		 objects[1] = wcode;
		 string sqlStr = "SELECT count(0)  FROM WarehouseAreaMap WHERE AreaID=@0 AND WarehouseID=@1";
		 return GetCount(sqlStr, context, objects);
	 } 
	 #endregion

	 #region 清除配送区域
	 /// <summary>
	 /// 
	 /// </summary>
	 /// <param name="wid"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public int DeleteWarehouseAreaMap(string wid, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = wid;
		 string sqlStr = "delete  from WarehouseAreaMap where WarehouseID= @0";
		 return Del(sqlStr, context, objects);
	 }
	 #endregion

	#region 根据仓库id 获取仓库设置区域
	 /// <summary>
	 /// 根据仓库id 获取仓库设置区域
	 /// </summary>
	 /// <param name="wid"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public List<WarehouseAreaMap> GetWarehouseAreaMapList(int wid, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = wid;
		 string sqlStr = "SELECT *  FROM WarehouseAreaMap  WHERE WarehouseID=@0";
		 return GetQueryMany(sqlStr, context, objects);
	 }
	 #endregion

	 

	}
}