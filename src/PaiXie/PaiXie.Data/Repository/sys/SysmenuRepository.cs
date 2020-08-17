#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using System.Data;
using PaiXie.Utils;
#endregion
namespace PaiXie.Data {
	public class SysmenuRepository : BaseRepository<Sysmenu> {

		#region 构造函数
		private static SysmenuRepository _instance;
		public static SysmenuRepository GetInstance() {
			if (_instance == null) {
				_instance = new SysmenuRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(Sysmenu entity) {
			int id = Db.GetInstance().Context().Insert<Sysmenu>("sys_menu", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return id;
		}
		#endregion

		#region Update
		public int Update(Sysmenu entity) {
			int rowsAffected = Db.GetInstance().Context().Update<Sysmenu>("sys_menu", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		#region 获取菜单
		/// <summary>
		/// 获取菜单
		/// </summary>
		/// <param name="rcode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public List<Sysmenu> GetSysmenulist() {
			string sqlStr = "SELECT  *  FROM sys_menu";
			return GetQueryMany(sqlStr);
		}
		#endregion

		#region GetDataTable
		public DataTable GetDataTable(int ModeType = 1, string ck = "") {
			string sqlStr = "";
			if (ModeType == 1) {
				if (ck == "ck") {
					#region 管理端  仓库

					sqlStr = @"

SELECT *  FROM (
SELECT  '9999' AS 	ID ,'仓库端' AS   TEXT ,  '-1' AS 	ParentID, 	'open' AS state, '0' AS attr   FROM sys_menu
 LIMIT 0,1 ) t3
UNION ALL
SELECT *  FROM (
SELECT  CODE AS 	ID ,NAME AS   TEXT ,  ParentCode AS 	ParentID, 	'open' AS state, '0' AS attr FROM sys_menu
WHERE ModeType=2
ORDER BY Seq ASC
) t4";
					#endregion
				}
				else {
					#region 管理端

					sqlStr = @"
SELECT *  FROM (
SELECT  '999' AS 	ID ,'管理端' AS   TEXT ,  '-1' AS 	ParentID, 	'open' AS state, '0' AS attr   FROM sys_menu
ORDER BY Seq ASC   LIMIT 0,1 ) t1
UNION ALL
SELECT *  FROM (
SELECT  CODE AS 	ID ,NAME AS   TEXT ,  ParentCode AS 	ParentID, 	'open' AS state, '0' AS attr FROM sys_menu
WHERE ModeType=1
ORDER BY Seq ASC
) t2
UNION ALL
SELECT *  FROM (
SELECT  '9999' AS 	ID ,'仓库端' AS   TEXT ,  '-1' AS 	ParentID, 	'open' AS state, '0' AS attr   FROM sys_menu
 LIMIT 0,1 ) t3
UNION ALL
SELECT *  FROM (
SELECT  CODE AS 	ID ,NAME AS   TEXT ,  ParentCode AS 	ParentID, 	'open' AS state, '0' AS attr FROM sys_menu
WHERE ModeType=2
ORDER BY Seq ASC
) t4";
					#endregion
				}
			}
			if (ModeType == 2) {
				#region 仓库端
				sqlStr = @"
SELECT *  FROM (
SELECT  '9999' AS 	ID ,'仓库端' AS   TEXT ,  '-1' AS 	ParentID, 	'open' AS state, '0' AS attr   FROM sys_menu
 LIMIT 0,1 ) t3
UNION ALL
SELECT *  FROM (
SELECT  CODE AS 	ID ,NAME AS   TEXT ,  ParentCode AS 	ParentID, 	'open' AS state, '0' AS attr FROM sys_menu
WHERE ModeType=2
ORDER BY Seq ASC
) t4";
				#endregion
			}
			return GetDataTable(sqlStr);
		}

		#endregion

		#region 获取权限菜单
		/// <summary>
		/// 获取权限菜单
		/// </summary>
		/// <param name="userCode">用户代码</param>
		/// <param name="parentcode">父级代码</param>
		/// <param name="modetype">类型</param>
		/// <param name="IsSupper">是否超管</param>
		/// <returns></returns>
		public List<Sysmenu> GetSysMenuByUserCode(string userCode, string parentcode, int modetype, bool IsSupper) {
			string sqlStr = "";
			sqlStr = " SELECT  *  FROM sys_menu    WHERE  ";
			if (!IsSupper) {
				sqlStr += " CODE IN   ";
				sqlStr += " (SELECT  MenuCode  FROM  ";
				sqlStr += " sys_roleMenuMap  WHERE  RoleCode IN   ";
				sqlStr += " (  ";
				sqlStr += " SELECT RoleCode FROM sys_userRoleMap  WHERE UserCode='" + userCode + "'))  and  ";
			}
			sqlStr += " 	 parentcode='" + parentcode + "'";
			sqlStr += " AND isenable=1  and ModeType=" + modetype + "  ORDER BY seq ASC ";
			return GetQueryMany(sqlStr);
		}
		#endregion
	}
}