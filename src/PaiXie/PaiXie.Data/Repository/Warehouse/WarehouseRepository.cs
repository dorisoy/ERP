using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
 public	class WarehouseRepository:BaseRepository<Warehouse> {

	 #region 构造函数
	 private static WarehouseRepository _instance;
	 public static WarehouseRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new WarehouseRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(Warehouse entity, IDbContext context = null) {
        if (context == null) context = Db.GetInstance().Context();
		 int Id = context.Insert<Warehouse>("warehouse", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		
		 return Id;
	 }
	 #endregion

	 #region Update
	 public int Update(Warehouse entity, IDbContext context = null) {
         if (context == null) context = Db.GetInstance().Context();
		 int rowsAffected = context.Update<Warehouse>("warehouse", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	 #region 唯一性
	
	 public int Getwarehousecount(int ID, string Code, IDbContext context = null) {
		 Object[] objects = new Object[2];
		 objects[0] = ID;
		 objects[1] = Code;
		 string sqlStr = "select count(0)  from warehouse where ID!=@0 and Code=@1";
		 return GetCount(sqlStr, context, objects);
	 }

	 public int Getwarehousecount(string Code, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = Code;
		 string sqlStr = "select count(0)  from warehouse where  Code=@0";
		 return GetCount(sqlStr, context, objects);
	 }


	 #endregion

	 #region 删除
	 /// <summary>
	 /// 
	 /// </summary>
	 /// <param name="rcode">id</param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public int Deletewarehouse(string ID, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = ID;
		 string sqlStr = "delete  from warehouse where ID=@0";
		 return Del(sqlStr, context, objects);
	 }
	 #endregion

	 #region 获取实体
	 /// <summary>
	 /// 
	 /// </summary>
	 /// <param name="rcode">id</param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public Warehouse Getwarehouse(string ID, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = ID;
		 string sqlStr = "SELECT 	* FROM warehouse WHERE ID=@0";
		 return GetQuerySingle(sqlStr, context, objects);
	 }
	 #endregion

	 #region 仓库 启用 禁用 
	 /// <summary>
	 /// 
	 /// </summary>
	 /// <param name="rcode">id</param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public int IsEnablewarehouse(int wid, int IsEnable, IDbContext context = null) {
		 if (context == null) context = Db.GetInstance().Context();
		 Object[] objects = new Object[2];
		 objects[0] = wid;
		 objects[1] = IsEnable;
		 return context.Sql("UPDATE   warehouse SET  IsEnable=@1 WHERE  id=@0", objects).Execute();
		
	 }
	 #endregion

	 #region 获取实体
	 /// <summary>
	 /// 
	 /// </summary>
	 /// <param name="rcode">id</param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public Warehouse GetwarehousebyCode(string Code, IDbContext context = null) {
		 if (context == null) context = Db.GetInstance().Context();
		 Object[] objects = new Object[1];
		 objects[0] = Code;
		 string sqlStr = "SELECT 	* FROM warehouse WHERE Code=@0";
		 return GetQuerySingle(sqlStr, context, objects);
	 }
	 #endregion				

	 #region 获取可用仓库列表
	 /// <summary>
	 /// 获取可用仓库列表
	 /// </summary>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public List<Warehouse> GetAvailableWarehouse(IDbContext context = null) {
		 if (context == null) context = Db.GetInstance().Context();
		 string sqlStr = "SELECT * FROM warehouse WHERE IsEnable = 1 ORDER BY Seq,ID";
		 return GetQueryMany(sqlStr, context);
	 }
	 #endregion				
	}
}