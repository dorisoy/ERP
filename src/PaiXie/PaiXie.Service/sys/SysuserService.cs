using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
 	public class SysuserService  : BaseService<Sysuser> {

		public static int Update(Sysuser entity, IDbContext context = null) {
			return SysuserRepository.GetInstance().Update(entity, context);
		}



		public static int Add(Sysuser entity, IDbContext context = null) {
			return SysuserRepository.GetInstance().Add(entity, context);
		}
        

			#region 登录判断
		/// <summary>
		/// 登录判断
		/// </summary>
		/// <param name="UserCode">用户代码</param>
		/// <param name="Password">密码</param>
		/// <returns></returns>
			public static Sysuser Login(string UserCode, string Password) {
				return SysuserRepository.GetInstance().Login(UserCode,  Password);
			}
			#endregion


			#region 修改登录次数 时间
		/// <summary>
			///  修改登录次数 时间
		/// </summary>
		/// <param name="UserCode"></param>
			public static void UpdateLoginStatus(string UserCode) {
				 SysuserRepository.GetInstance().UpdateLoginStatus(UserCode);
		
			}
			#endregion
		/// <summary>
		/// 获取实体
		/// </summary>
		/// <param name="UID">用户id</param>
		/// <returns></returns>
			public static Sysuser GetSysuserlist(string UID) {
				return SysuserRepository.GetInstance().GetSysuserlist(UID);
			}
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="UID">用户id</param>
		/// <returns></returns>
			public static int Deletsysuser(string UID) {
				return SysuserRepository.GetInstance().Deletsysuser(UID);
			}
		/// <summary>
		/// 检查用户代码唯一性
		/// </summary>
		/// <param name="ID">主键id</param>
		/// <param name="UserCode">用户代码</param>
		/// <returns></returns>
			public static  int Getsysusercount(int ID, string UserCode) {
				return SysuserRepository.GetInstance().Getsysusercount( ID,  UserCode);
			}
		/// <summary>
			/// 检查用户代码唯一性
		/// </summary>
		/// <param name="UserCode">用户代码</param>
		/// <returns></returns>
			public static  int Getsysusercount(string UserCode) {
				return SysuserRepository.GetInstance().Getsysusercount( UserCode);		
			}


		/// <summary>
		/// 获取 实体  通过 用户代码
		/// </summary>
		/// <param name="UserCode">用户代码</param>
		/// <returns></returns>
			public static Sysuser GetSysuserbyUserCode(string UserCode) {
				return SysuserRepository.GetInstance().GetSysuserbyUserCode(UserCode);		
			}


			#region 根据用户代码 修改用户密码
			/// <summary>
			/// 根据用户代码 修改用户密码
			/// </summary>
			/// <param name="PASSWORD"></param>
			/// <param name="CODE"></param>
			/// <param name="context"></param>
			/// <returns></returns>
			public static int UpdatePwdByCode(string PASSWORD, string CODE, IDbContext context = null) {
				return SysuserRepository.GetInstance().UpdatePwdByCode( PASSWORD,  CODE,  context);	
			}
			#endregion
	}
}