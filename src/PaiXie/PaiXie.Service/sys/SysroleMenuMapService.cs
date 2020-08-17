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
		/// ɾ��  ��ɫ �˵� ����
		/// </summary>
		/// <param name="rcode">��ɫ����</param>
		/// <returns></returns>
		public static int DeleteroleMenuMap(string rcode, IDbContext context = null) {
			return SysroleMenuMapRepository.GetInstance().DeleteroleMenuMap(rcode,context);
		}
		/// <summary>
		/// �Ƿ��н�ɫ�˵�Ȩ��
		/// </summary>
		/// <param name="menucode">�˵�����</param>
		/// <param name="rolecode">��ɫ����</param>
		/// <returns></returns>
		public static int IsroleMenuMap(string menucode, string rolecode) {
			return SysroleMenuMapRepository.GetInstance().IsroleMenuMap( menucode,  rolecode);
		}
	}
}





