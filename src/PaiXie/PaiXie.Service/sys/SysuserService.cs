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
        

			#region ��¼�ж�
		/// <summary>
		/// ��¼�ж�
		/// </summary>
		/// <param name="UserCode">�û�����</param>
		/// <param name="Password">����</param>
		/// <returns></returns>
			public static Sysuser Login(string UserCode, string Password) {
				return SysuserRepository.GetInstance().Login(UserCode,  Password);
			}
			#endregion


			#region �޸ĵ�¼���� ʱ��
		/// <summary>
			///  �޸ĵ�¼���� ʱ��
		/// </summary>
		/// <param name="UserCode"></param>
			public static void UpdateLoginStatus(string UserCode) {
				 SysuserRepository.GetInstance().UpdateLoginStatus(UserCode);
		
			}
			#endregion
		/// <summary>
		/// ��ȡʵ��
		/// </summary>
		/// <param name="UID">�û�id</param>
		/// <returns></returns>
			public static Sysuser GetSysuserlist(string UID) {
				return SysuserRepository.GetInstance().GetSysuserlist(UID);
			}
		/// <summary>
		/// ɾ��
		/// </summary>
		/// <param name="UID">�û�id</param>
		/// <returns></returns>
			public static int Deletsysuser(string UID) {
				return SysuserRepository.GetInstance().Deletsysuser(UID);
			}
		/// <summary>
		/// ����û�����Ψһ��
		/// </summary>
		/// <param name="ID">����id</param>
		/// <param name="UserCode">�û�����</param>
		/// <returns></returns>
			public static  int Getsysusercount(int ID, string UserCode) {
				return SysuserRepository.GetInstance().Getsysusercount( ID,  UserCode);
			}
		/// <summary>
			/// ����û�����Ψһ��
		/// </summary>
		/// <param name="UserCode">�û�����</param>
		/// <returns></returns>
			public static  int Getsysusercount(string UserCode) {
				return SysuserRepository.GetInstance().Getsysusercount( UserCode);		
			}


		/// <summary>
		/// ��ȡ ʵ��  ͨ�� �û�����
		/// </summary>
		/// <param name="UserCode">�û�����</param>
		/// <returns></returns>
			public static Sysuser GetSysuserbyUserCode(string UserCode) {
				return SysuserRepository.GetInstance().GetSysuserbyUserCode(UserCode);		
			}


			#region �����û����� �޸��û�����
			/// <summary>
			/// �����û����� �޸��û�����
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