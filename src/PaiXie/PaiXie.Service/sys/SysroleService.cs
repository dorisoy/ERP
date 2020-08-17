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
		/// ��ȡʵ��
		/// </summary>
		/// <param name="roleid">��ɫid </param>
		/// <returns></returns>
		public static  Sysrole GetSysrole(string roleid) {
			return SysroleRepository.GetInstance().GetSysrole(roleid);
		}
		/// <summary>
		/// ɾ����ɫ
		/// </summary>
		/// <param name="id">��ɫid </param>
		/// <returns></returns>
		public static int DelSysrole(string id) {
			return SysroleRepository.GetInstance().DelSysrole(id);
		}

		/// <summary>
		/// ������Ψһ��
		/// </summary>
		/// <param name="ID">����</param>
		/// <param name="roleCode">��ɫ����</param>
		/// <returns></returns>
		public static int GetsysroleCount(int ID, string roleCode) {
			return SysroleRepository.GetInstance().GetsysroleCount(ID, roleCode);
		}

		/// <summary>
		/// ������Ψһ��
		/// </summary>
		/// <param name="roleCode">��ɫ����</param>
		/// <returns></returns>
		public static int GetsysroleCount(string roleCode) {
			return SysroleRepository.GetInstance().GetsysroleCount(roleCode);
	
		}
		/// <summary>
		/// ��ȡ ��ɫ�б�
		/// </summary>
		/// <returns></returns>
		public static List<Sysrole> GetSysrolelist() {
			return SysroleRepository.GetInstance().GetSysrolelist();
	
		}

		
	}
}





