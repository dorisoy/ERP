using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using System.Data;
namespace  PaiXie.Data 
{
 public	class SyscodeTypeRepository:BaseRepository<SyscodeType> {

	 #region 构造函数
	 private static SyscodeTypeRepository _instance;
	 public static SyscodeTypeRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SyscodeTypeRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(SyscodeType entity) {
		 int id = Db.GetInstance().Context().Insert<SyscodeType>("sys_codeType", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(SyscodeType entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<SyscodeType>("sys_codeType", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	 #region 字典类型列表

	 /// <summary>
	 /// 字典类型列表
	 /// </summary>
	 /// <returns></returns>
	 public DataTable GetJsonTreeSyscodeType(string name="", IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = "%" + name + "%";
		 string sqlStr = "SELECT Code as ID, NAME AS TEXT, '0' AS ParentID,'open' AS  state , 'null' AS attr FROM  sys_codeType WHERE isenable=1 and Name like  @0";
		 return GetDataTable(sqlStr, context, objects);
	 } 
	 #endregion

	 #region 字典类型 by  code
	 /// <summary>
	 /// 字典类型 by  code
	 /// </summary>
	 /// <param name="codetype"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public SyscodeType CodeType(string codetype, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = codetype;
		 string sqlStr = "SELECT 	* FROM sys_codeType WHERE Code=@0";
		 return GetQuerySingle(sqlStr, context, objects);
	 } 
	 #endregion

	 #region 字典类型 by  id
	 /// <summary>
	 /// 字典类型 by  id
	 /// </summary>
	 /// <param name="id"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public SyscodeType CodeType(int id, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = id;
		 string sqlStr = " select	* FROM sys_codeType WHERE ID=@0";
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
	 public int deleteCodeType(string id, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = id;
		 string sqlStr = " delete  from sys_codeType where Code=@0";
		 return Del(sqlStr, context, objects);
	 } 
	 #endregion

	 #region 检查唯一性

	 public int CheckCode(string Code, int ID, IDbContext context = null) {
		 Object[] objects = new Object[2];
		 objects[0] = ID;
		 objects[1] = Code;
		 string sqlStr = " select count(0)  from sys_codeType where ID!=@0 and Code=@1";
		 return GetCount(sqlStr, context, objects);
	 }
	 public int CheckCode(string Code, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = Code;
		 string sqlStr = " select count(0)  from sys_codeType where  Code=@0";
		 return GetCount(sqlStr, context, objects);
	 } 
	 #endregion
				
	}
}





