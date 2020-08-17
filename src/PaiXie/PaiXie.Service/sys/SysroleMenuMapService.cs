using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
 	public class SysroleMenuMapService  : BaseService<SysroleMenuMap> {
    
		public static int Update(SysroleMenuMap entity) {
			return SysroleMenuMapRepository.GetInstance().Update(entity);
		}



		public static int Add(SysroleMenuMap entity, IDbContext context = null) {
			return SysroleMenuMapRepository.GetInstance().Add(entity,context);
		}
		/// <summary>
		/// 删除  角色 菜单 关联
		/// </summary>
		/// <param name="rcode">角色代码</param>
		/// <returns></returns>
		public static int DeleteroleMenuMap(string rcode, IDbContext context = null) {
			return SysroleMenuMapRepository.GetInstance().DeleteroleMenuMap(rcode,context);
		}
		/// <summary>
		/// 是否有角色菜单权限
		/// </summary>
		/// <param name="menucode">菜单代码</param>
		/// <param name="rolecode">角色代码</param>
		/// <returns></returns>
		public static int IsroleMenuMap(string menucode, string rolecode) {
			return SysroleMenuMapRepository.GetInstance().IsroleMenuMap( menucode,  rolecode);
		}
	}
}





