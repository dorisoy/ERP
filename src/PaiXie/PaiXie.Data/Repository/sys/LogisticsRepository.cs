using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class LogisticsRepository:BaseRepository<Logistics> {

	 #region 构造函数
	 private static LogisticsRepository _instance;
	 public static LogisticsRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new LogisticsRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(Logistics entity, IDbContext context = null) {
        if (context == null) context = Db.GetInstance().Context();
		 int Id = context.Insert<Logistics>("logistics", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return Id;
	 }
	 #endregion

	 #region Update
	 public int Update(Logistics entity, IDbContext context = null) {
         if (context == null) context = Db.GetInstance().Context();
		 int rowsAffected = context.Update<Logistics>("logistics", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	 #region 获取实体
	/// <summary>
	 /// 获取实体
	/// </summary>
	/// <param name="id">主键id</param>
	/// <param name="context"></param>
	/// <returns></returns>
	 public Logistics GetLogistics(int id, IDbContext context = null) {
		 if (context == null) context = Db.GetInstance().Context();
		 Object[] objects = new Object[1];
		 objects[0] = id;
		 string sqlStr = "SELECT 	* FROM logistics WHERE ID=@0";
		 return GetQuerySingle(sqlStr, context, objects);
	 }
	 #endregion

	 #region 删除物流
	/// <summary>
	 /// 删除物流
	/// </summary>
	/// <param name="id">主键id</param>
	/// <param name="context"></param>
	/// <returns></returns>
	 public int DelLogistics(string id, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = id;
		 string sqlStr = "delete  from logistics where ID=@0";
		 return Del(sqlStr, context, objects);
	 }
	 #endregion

	 #region 检查代码唯一性
	 /// <summary>
	 /// 
	 /// </summary>
	 /// <param name="rcode"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public int GetLogisticsCount( int ID, string roleCode, IDbContext context = null) {
		 Object[] objects = new Object[2];
		 objects[0] = ID;
		 objects[1] = roleCode;
		 string sqlStr = "select count(0)  from logistics where ID!=@0 and Code=@1";
		 return GetCount(sqlStr, context, objects);
	 }


	 public int GetLogisticsCount(string roleCode, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = roleCode;
		 string sqlStr = "select count(0)  from logistics where  Code=@0";
		 return GetCount(sqlStr, context, objects);
	 }
	 #endregion

	 #region 禁用、启用物流公司
/// <summary>
	 /// 禁用、启用物流公司
/// </summary>
/// <param name="id">物流公司id </param>
/// <param name="context"></param>
/// <returns></returns>
	 public int SetIsEnable(int id, IDbContext context = null) {
		 if (context == null) context = Db.GetInstance().Context();
		 Logistics Logistics = GetLogistics(id, context);
		int IsEnable = 0;
		if (Logistics != null) {
			IsEnable = Logistics.IsEnable==1?0:1;
		}
		 Object[] objects = new Object[2];
		 objects[0] = id;
		 objects[1] = IsEnable;
		 string sqlStr = "update logistics set IsEnable=@1  where ID=@0";
		 return Del(sqlStr, context, objects);
	 }
	 #endregion

	 #region 获取可用物流列表

	 /// <summary>
	 /// 获取可用物流列表
	 /// </summary>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public List<Logistics> GetManyLogistics(IDbContext context = null) {
		 string sqlStr = "SELECT * FROM logistics WHERE IsEnable = 1";
		 return GetQueryMany(sqlStr, context);
	 }

	 #endregion

	}
}





