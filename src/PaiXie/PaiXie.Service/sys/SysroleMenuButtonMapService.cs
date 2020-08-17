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
		/// ɾ����ɫ�˵��¼�
		/// </summary>
		/// <param name="rcode">��ɫ����</param>
		/// <returns></returns>
		public static int DeleteroleMenuButtonMap(string rcode, IDbContext context = null) {
			return SysroleMenuButtonMapRepository.GetInstance().DeleteroleMenuButtonMap(rcode,context);
		}


		/// <summary>
		/// �Ƿ��н�ɫ�˵��¼�Ȩ��
		/// </summary>
		/// <param name="rolecode">��ɫ����</param>
		/// <param name="buttoncode">�¼�����</param>
		/// <returns></returns>
		public static int IsroleMenuButtonMap(string rolecode, string buttoncode) {
			return SysroleMenuButtonMapRepository.GetInstance().IsroleMenuButtonMap( rolecode,  buttoncode);
	
		}
	
	}
}





