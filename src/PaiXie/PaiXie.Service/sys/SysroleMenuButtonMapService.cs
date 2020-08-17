using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
 	public class SysroleMenuButtonMapService  : BaseService<SysroleMenuButtonMap> {
    
		public static int Update(SysroleMenuButtonMap entity) {
			return SysroleMenuButtonMapRepository.GetInstance().Update(entity);
		}



		public static int Add(SysroleMenuButtonMap entity, IDbContext context = null) {
			return SysroleMenuButtonMapRepository.GetInstance().Add(entity,context);
		}
		/// <summary>
		/// 删除角色菜单事件
		/// </summary>
		/// <param name="rcode">角色代码</param>
		/// <returns></returns>
		public static int DeleteroleMenuButtonMap(string rcode, IDbContext context = null) {
			return SysroleMenuButtonMapRepository.GetInstance().DeleteroleMenuButtonMap(rcode,context);
		}


		/// <summary>
		/// 是否有角色菜单事件权限
		/// </summary>
		/// <param name="rolecode">角色代码</param>
		/// <param name="buttoncode">事件代码</param>
		/// <returns></returns>
		public static int IsroleMenuButtonMap(string rolecode, string buttoncode) {
			return SysroleMenuButtonMapRepository.GetInstance().IsroleMenuButtonMap( rolecode,  buttoncode);
	
		}
	
	}
}





