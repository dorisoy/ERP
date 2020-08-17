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
		/// ɾ�� �û� ��ɫ ����
		/// </summary>
		/// <param name="ucode">�û�����</param>
		/// <returns></returns>
		public static int DelsysuserRoleMap(string ucode, IDbContext context = null) {
			return SysuserRoleMapRepository.GetInstance().DelsysuserRoleMap(ucode,context);
		}
		/// <summary>
		/// ɾ�� �û���ɫ  ����
		/// </summary>
		/// <param name="rcode">��ɫ����</param>
		/// <returns></returns>
		public static int DelsysuserRoleMapbyrole(string rcode, IDbContext context = null) {
			return SysuserRoleMapRepository.GetInstance().DelsysuserRoleMapbyrole(rcode,context);
		}
		/// <summary>
		/// �û���ɫ��������
		/// </summary>
		/// <param name="usercode">�û�����</param>
		/// <param name="rolecode">��ɫ����</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns>����</returns>
			public static int Getsys_userRoleMapCount(string usercode, string rolecode, IDbContext context = null) {
			return SysuserRoleMapRepository.GetInstance().Getsys_userRoleMapCount(usercode,  rolecode,  context);
		}

		

	}
}





