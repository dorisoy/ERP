using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using PaiXie.Core;
using FluentData;
namespace  PaiXie.Service 
{
 	public class SysuserRoleMapService  : BaseService<SysuserRoleMap> {
    
		public static int Update(SysuserRoleMap entity) {
			return SysuserRoleMapRepository.GetInstance().Update(entity);
		}



		public static int Add(SysuserRoleMap entity, IDbContext context = null) {
			return SysuserRoleMapRepository.GetInstance().Add(entity,context);
		}

	
		/// <summary>
		/// 删除 用户 角色 关联
		/// </summary>
		/// <param name="ucode">用户代码</param>
		/// <returns></returns>
		public static int DelsysuserRoleMap(string ucode, IDbContext context = null) {
			return SysuserRoleMapRepository.GetInstance().DelsysuserRoleMap(ucode,context);
		}
		/// <summary>
		/// 删除 用户角色  关联
		/// </summary>
		/// <param name="rcode">角色代码</param>
		/// <returns></returns>
		public static int DelsysuserRoleMapbyrole(string rcode, IDbContext context = null) {
			return SysuserRoleMapRepository.GetInstance().DelsysuserRoleMapbyrole(rcode,context);
		}
		/// <summary>
		/// 用户角色关联计数
		/// </summary>
		/// <param name="usercode">用户代码</param>
		/// <param name="rolecode">角色代码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns>个数</returns>
			public static int Getsys_userRoleMapCount(string usercode, string rolecode, IDbContext context = null) {
			return SysuserRoleMapRepository.GetInstance().Getsys_userRoleMapCount(usercode,  rolecode,  context);
		}

		

	}
}





