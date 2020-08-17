using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class SysuserRepository : BaseRepository<Sysuser> {

		#region 构造函数
		private static SysuserRepository _instance;
		public static SysuserRepository GetInstance() {
			if (_instance == null) {
				_instance = new SysuserRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(Sysuser entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int id = context.Insert<Sysuser>("sys_user", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return id;
		}
		#endregion

		#region Update
		public int Update(Sysuser entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<Sysuser>("sys_user", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		#region 登录判断
		public Sysuser Login(string UserCode, string Password) {
			Object[] objects = new Object[2];
			objects[0] = UserCode;
			objects[1] = Password;
			string strsql = @"select  ID, 
	CODE, Seq, NAME, Description, PASSWORD, RoleName, 
	OrganizeName, ConfigJSON, IsEnable, LoginCount, LastLoginDate, CreatePerson, CreateDate, UpdatePerson, UpdateDate, ModeType, 
	WarehouseCode ,IsSupper from sys_user where Code=@0 and  Password=@1  ";
			var result = GetQuerySingle(strsql, null, objects);
			return result;
		}
		#endregion

		#region 修改登录次数 时间
		public void UpdateLoginStatus(string UserCode) {
			Update(string.Format(@"
			update sys_user
			set LoginCount = IFNULL(LoginCount,0) + 1
			   ,LastLoginDate = NOW()
			where Code = '{0}' "
				   , UserCode));
		}
		#endregion

		#region 获取用户列表
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rcode">id</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public Sysuser GetSysuserlist(string UID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = UID;
			string sqlStr = "SELECT 	* FROM sys_user WHERE ID=@0";
			return GetQuerySingle(sqlStr, context, objects);
		}
		#endregion

		#region 删除
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rcode">id</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public int Deletsysuser(string UID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = UID;
			string sqlStr = "delete  from sys_user where ID=@0  and IsSupper=0";
			return Del(sqlStr, context, objects);
		}
		#endregion

		#region 唯一性
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rcode">id</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public int Getsysusercount(int ID, string UserCode, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = ID;
			objects[1] = UserCode;
			string sqlStr = "select count(0)  from sys_user where ID!=@0 and Code=@1";
			return GetCount(sqlStr, context, objects);
		}

		public int Getsysusercount(string UserCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = UserCode;
			string sqlStr = "select count(0)  from sys_user where  Code=@0";
			return GetCount(sqlStr, context, objects);
		}


		#endregion

		#region 获取用户  by  code
		/// <summary>
		/// 获取用户  by  code
		/// </summary>
		/// <param name="UserCode">用户代码</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public Sysuser GetSysuserbyUserCode(string UserCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = UserCode;
			Sysuser obj = GetQuerySingle("SELECT  *  FROM  sys_user  WHERE CODE=@0", context, objects);
			return obj;
		}
		#endregion

		#region 根据用户代码 修改用户密码
		/// <summary>
		/// 根据用户代码 修改用户密码
		/// </summary>
		/// <param name="PASSWORD"></param>
		/// <param name="CODE"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public int UpdatePwdByCode(string PASSWORD,string CODE, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] =PASSWORD;
			objects[1] = CODE;
			string sqlStr = "UPDATE sys_user SET PASSWORD=@0 WHERE CODE=@1 ";
			return Update(sqlStr, context, objects);
		}
		#endregion
		
	}
}