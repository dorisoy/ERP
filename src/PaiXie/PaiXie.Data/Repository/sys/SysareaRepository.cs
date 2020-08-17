using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using System.Data;
namespace PaiXie.Data {
	public class SysareaRepository : BaseRepository<Sysarea> {

		#region 构造函数
		private static SysareaRepository _instance;
		public static SysareaRepository GetInstance() {
			if (_instance == null) {
				_instance = new SysareaRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(Sysarea entity) {
			int id = Db.GetInstance().Context().Insert<Sysarea>("sys_area", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return id;
		}
		#endregion

		#region Update
		public int Update(Sysarea entity) {
			int rowsAffected = Db.GetInstance().Context().Update<Sysarea>("sys_area", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		#region GetDataTable  获取区域
		/// <summary>
		/// 获取区域
		/// </summary>
		/// <returns></returns>
		public DataTable GetDataTable() {

			string sqlStr = @"
SELECT  	ID ,NAME AS   TEXT ,  IFNULL(0,ParentID)  AS	ParentID, 	'open' AS state, '0' AS attr FROM sys_area 
sys_area WHERE  ParentID=0 
UNION ALL 
SELECT  	ID ,NAME AS   TEXT ,  	ParentID, 	'open' AS state, '0' AS attr FROM sys_area 
sys_area WHERE  
parentid IN (SELECT  	ID FROM sys_area 
sys_area WHERE  ParentID=0) 
";
			return GetDataTable(sqlStr);
		}

		#endregion

		#region 区域管理列表
		/// <summary>
		/// 区域管理列表
		/// </summary>
		/// <returns></returns>
		public DataTable GetareaDataTable() {

			string sqlStr = @" SELECT *  FROM (  SELECT ID,NAME AS TEXT, IFNULL(0,ParentID) AS ParentID   ,'open' AS  state,0 AS attr FROM  sys_area WHERE ParentID=0
UNION
SELECT  ID,NAME AS TEXT,  ParentID   ,   'open' AS  state,1 AS attr FROM  sys_area WHERE ParentID IN
(
SELECT  id FROM  sys_area WHERE ParentID=0
)
UNION
SELECT   ID,NAME AS TEXT,  ParentID   ,   'open' AS  state,2 AS attr FROM  sys_area WHERE ParentID IN
(
SELECT  id FROM  sys_area WHERE ParentID IN
(
SELECT  id FROM  sys_area WHERE ParentID=0
)
)
) a";
			return GetDataTable(sqlStr);
		}

		#endregion

		#region 删除区域
		/// <summary>
		/// 删除区域
		/// </summary>
		/// <param name="id"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		public int DelArea(int id, int level) {

			Object[] objects = new Object[1];
			objects[0] = id;

			string sqlStr = "";
			if (level == 1) {
				sqlStr = @" 
DELETE  FROM 
   sys_area WHERE  id IN 
 (
 SELECT id FROM (
 SELECT ID FROM  sys_area WHERE ID  =@0
 UNION
 SELECT ID FROM  sys_area WHERE ParentID =@0
 UNION
  SELECT ID FROM  sys_area WHERE ParentID IN
  (
  SELECT ID FROM  sys_area WHERE ParentID =@0
  )
  ) a
  )
";
			}
			else {
				sqlStr = @" 		 
DELETE  FROM 
   sys_area WHERE  id IN 
 (
 SELECT id FROM (
 SELECT ID FROM  sys_area WHERE ID  =@0
 UNION
 SELECT ID FROM  sys_area WHERE ParentID =@0

  ) a
  )";

			}

			return Db.GetInstance().Context().Sql(sqlStr, objects).Execute();
		}

		#endregion

		#region 获取区域
		/// <summary>
		/// 获取区域
		/// </summary>
		/// <param name="id">主键id</param>
		/// <returns></returns>
		public Sysarea GetArea(int id) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM  sys_area WHERE  ID=@0";
			return GetQuerySingle(sqlStr, null, objects);
		}

		#endregion

		#region 恢复初始化区域设置
		/// <summary>
		/// 恢复初始化区域设置
		/// </summary>
		/// <returns></returns>
		public int initArea() {
			string sqlStr = @" TRUNCATE       sys_area  ;
 INSERT INTO sys_area  (ID,NAME,ParentID) 
 SELECT  ID,NAME,ParentID FROM 
 sys_area_bak";
			return Db.GetInstance().Context().Sql(sqlStr).Execute();
		}

		#endregion

		#region 编辑区域
		/// <summary>
		/// 编辑区域
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="id">主键id</param>
		/// <returns></returns>
		public int EditArea(string name, int id) {
			Object[] objects = new Object[2];
			objects[0] = name;
			objects[1] = id;
			string sqlStr = @" UPDATE  sys_area SET NAME=@0 WHERE  id=@1";
			return Db.GetInstance().Context().Sql(sqlStr, objects).Execute();
		}

		#endregion

		#region 获取大区列表

		/// <summary>
		/// 获取大区列表
		/// </summary>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<Sysarea> GetLargeAreaList(IDbContext context = null) {
			string sqlStr = @"SELECT LargeArea FROM sys_area WHERE LargeArea<>'' GROUP BY LargeArea ORDER BY Seq,ID ASC";
			return GetQueryMany(sqlStr, context);
		}

		#endregion

		#region 根据大区名称获取省份列表

		/// <summary>
		/// 根据大区名称获取省份列表
		/// </summary>
		/// <param name="largeAreaName">大区名称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<Sysarea> GetProvinceList(string largeAreaName, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = largeAreaName;
			string sqlStr = @"SELECT ID, AliasName FROM sys_area WHERE LargeArea=@0 AND ParentID=0 ORDER BY Seq,ID ASC";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据省份ID获取城市列表

		/// <summary>
		/// 根据省份ID获取城市列表
		/// </summary>
		/// <param name="provinceID">省份ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<Sysarea> GetCityList(int provinceID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = provinceID;
			string sqlStr = @"SELECT ID, Name FROM sys_area WHERE ParentID=@0 ORDER BY Seq,ID ASC";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 获取所有省市区街道

		/// <summary>
		/// 获取所有省市区街道
		/// </summary>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public DataTable GetManySysarea(IDbContext context = null) {
			string sqlStr = @"SELECT * FROM sys_area";
			return GetDataTable(sqlStr, context);
		}

		#endregion
	}
}