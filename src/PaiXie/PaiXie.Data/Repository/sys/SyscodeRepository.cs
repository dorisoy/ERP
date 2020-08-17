using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using System.Data;
namespace  PaiXie.Data 
{
 public	class SyscodeRepository:BaseRepository<Syscode> {

	 #region 构造函数
	 private static SyscodeRepository _instance;
	 public static SyscodeRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SyscodeRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(Syscode entity) {
		 int id = Db.GetInstance().Context().Insert<Syscode>("sys_code", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(Syscode entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<Syscode>("sys_code", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	 #region 获取实体  by  code   codetype
	 /// <summary>
	 /// 获取实体  by  code   codetype
	 /// </summary>
	 /// <param name="Code"></param>
	 /// <param name="codetype"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public Syscode GetSyscodeByCodetype(string Code, string codetype, IDbContext context = null) {
		 Object[] objects = new Object[2];
		 objects[0] = Code;
		 objects[1] = codetype;
		 string sqlStr = "SELECT  * FROM sys_code WHERE CODE=@0 AND     codetype=@1";
		 return GetQuerySingle(sqlStr, context, objects);
	 }
	 
	 #endregion

	 #region 获取实体 通过代码
	 /// <summary>
	 /// 获取实体 通过代码
	 /// </summary>
	 /// <param name="Code"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public Syscode GetSyscodeByCode(string Code, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = Code;
		 string sqlStr = "SELECT 	* FROM sys_code WHERE Code=@0";
		 return GetQuerySingle(sqlStr, context, objects);
	 } 
	 #endregion

	 #region 字典列表

	 /// <summary>
	 /// 字典列表
	 /// </summary>
	 /// <param name="CodeType"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public DataTable GetSyscodeTree(string CodeType, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = CodeType;
		 string sqlStr = "SELECT  CODE AS 	ID ,  TEXT ,  ParentCode AS 	ParentID, 	'open' AS state, '' AS attr,Description,IsEnable  FROM sys_code  WHERE CodeType=@0";
		 return GetDataTable(sqlStr, context, objects);
	 } 
	 #endregion

	 #region 获取实体  by   codetype
	 public List<Syscode> GetSyscodebycodetype(string CodeType, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = CodeType;
		 string sqlStr = "SELECT  * FROM sys_code WHERE    codetype=@0";
		 return GetQueryMany(sqlStr, context, objects);
	 } 
	 #endregion

	 #region 获取实体   by  id

	 public Syscode Code(int id, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = id;
		 string sqlStr = " select	* FROM sys_code WHERE ID=@0";
		 return GetQuerySingle(sqlStr, context, objects);
	 } 
	 #endregion

	 #region 删除
	 /// <summary>
	 /// 删除
	 /// </summary>
	 /// <param name="id"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public int deleteCode(string id) {


		 int result = 1;
		 try {
			 using (IDbContext context = Db.GetInstance().Context()) {
				 context.UseTransaction(true);
				 string str = id;
				 string[] sArray = str.Split(',');
				 #region 循环操作
				 foreach (string i in sArray) {

					 Object[] objects = new Object[1];
					 objects[0] = i;
					 string IsEnable = Getobject("SELECT  IsEnable  FROM sys_code WHERE  CODE=@0", context, objects);
					 if (IsEnable == "1") {
						 IsEnable = "0";
					 }
					 else {
						 IsEnable = "1";
					 }
					 Object[] objects2 = new Object[2];
					 objects2[0] = i;
					 objects2[1] = IsEnable;
					 string sqlStr = " update    sys_code set IsEnable=@1 where Code=@0";
					 result = Del(sqlStr, context, objects2);

					 if (result == 0) {
						 break;
					 }
				 }
				 #endregion

				 if (result == 1) {
					 context.Commit();
				 }
				 else {
					 context.Rollback();
				 }
			 }
		 }
		 catch (Exception ex) {
			 result = 0;
		 }
		 return result;








	




	 } 
	 #endregion

	 #region 检查代码唯一性

	 public int CheckCode(string Code, int ID, IDbContext context = null) {
		 Object[] objects = new Object[2];
		 objects[0] = ID;
		 objects[1] = Code;
		 string sqlStr = " select count(0)  from sys_code where ID!=@0 and Code=@1";
		 return GetCount(sqlStr, context, objects);
	 } 

	 public int CheckCode(string Code, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = Code;
		 string sqlStr = " select count(0)  from sys_code where  Code=@0";
		 return GetCount(sqlStr, context, objects);
	 } 
	 #endregion


	 #region 店铺平台

	 public DataTable GetDataTableCode(IDbContext context = null) {
		 //店铺平台
		 DataTable Type = GetDataTable("SELECT  CODE AS VALUE , TEXT  FROM sys_code WHERE   codetype='002' AND IsEnable=1 ORDER BY seq ", context);
		 return Type;
	 }
	 #endregion

		



				
	}
}





