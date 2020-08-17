using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class SysroleService  : BaseService<Sysrole> {
    
		public static int Update(Sysrole entity) {
			return SysroleRepository.GetInstance().Update(entity);
		}

	

		public static int Add(Sysrole entity) {
			return SysroleRepository.GetInstance().Add(entity);
		}
		/// <summary>
		/// 获取实体
		/// </summary>
		/// <param name="roleid">角色id </param>
		/// <returns></returns>
		public static  Sysrole GetSysrole(string roleid) {
			return SysroleRepository.GetInstance().GetSysrole(roleid);
		}
		/// <summary>
		/// 删除角色
		/// </summary>
		/// <param name="id">角色id </param>
		/// <returns></returns>
		public static int DelSysrole(string id) {
			return SysroleRepository.GetInstance().DelSysrole(id);
		}

		/// <summary>
		/// 检查代码唯一性
		/// </summary>
		/// <param name="ID">主键</param>
		/// <param name="roleCode">角色代码</param>
		/// <returns></returns>
		public static int GetsysroleCount(int ID, string roleCode) {
			return SysroleRepository.GetInstance().GetsysroleCount(ID, roleCode);
		}

		/// <summary>
		/// 检查代码唯一性
		/// </summary>
		/// <param name="roleCode">角色代码</param>
		/// <returns></returns>
		public static int GetsysroleCount(string roleCode) {
			return SysroleRepository.GetInstance().GetsysroleCount(roleCode);
	
		}
		/// <summary>
		/// 获取 角色列表
		/// </summary>
		/// <returns></returns>
		public static List<Sysrole> GetSysrolelist() {
			return SysroleRepository.GetInstance().GetSysrolelist();
	
		}

		
	}
}





