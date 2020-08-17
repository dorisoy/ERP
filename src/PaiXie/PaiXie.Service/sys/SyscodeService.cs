using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
 	public class SyscodeService  : BaseService<Syscode> {
    
		public static int Update(Syscode entity) {
			return SyscodeRepository.GetInstance().Update(entity);
		}

	

		public static int Add(Syscode entity) {
			return SyscodeRepository.GetInstance().Add(entity);
		}

		/// <summary>
		/// ��ȡʵ��  ͨ������
		/// </summary>
		/// <param name="Code">����</param>
		/// <returns></returns>
		public static Syscode GetSyscodeByCode(string Code) {
			return SyscodeRepository.GetInstance().GetSyscodeByCode(Code);

		}
		/// <summary>
		/// �ֵ��б�
		/// </summary>
		/// <param name="CodeType">��Ŀ����</param>
		/// <returns></returns>
		public static DataTable GetSyscodeTree(string CodeType) {
			return SyscodeRepository.GetInstance().GetSyscodeTree(CodeType);
		}

		/// <summary>
		/// ��ȡʵ��  ͨ�� id 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Syscode Code(int id) {
			return SyscodeRepository.GetInstance().Code(id);
		}
		/// <summary>
		/// ɾ�� ʵ��    ͨ�� id 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static int deleteCode(string id) {
			return SyscodeRepository.GetInstance().deleteCode(id);
		}
		/// <summary>
		/// ������Ψһ��
		/// </summary>
		/// <param name="Code">����</param>
		/// <param name="ID">����</param>
		/// <returns></returns>
		public static int CheckCode(string Code, int ID) {
			return SyscodeRepository.GetInstance().CheckCode(Code, ID);
		}
		/// <summary>
		/// ������Ψһ��
		/// </summary>
		/// <param name="Code">����</param>
		/// <returns></returns>
		public static int CheckCode(string Code) {
			return SyscodeRepository.GetInstance().CheckCode(Code);
		}

	/// <summary>
	/// ��ȡ�ֵ��б�  ͨ���ֵ����� 
	/// </summary>
	/// <param name="CodeType">�ֵ�����</param>
	/// <returns></returns>
		public static List<Syscode> GetSyscodebycodetype(string CodeType) {
			return SyscodeRepository.GetInstance().GetSyscodebycodetype(CodeType);
		}

		/// <summary>
		/// ��ȡʵ��  ͨ�� �ֵ�����  �ֵ����
		/// </summary>
		/// <param name="Code">�ֵ����</param>
		/// <param name="codetype">�ֵ�����</param>
		/// <returns></returns>
		public static  Syscode GetSyscodeByCodetype(string Code, string codetype) {
			return SyscodeRepository.GetInstance().GetSyscodeByCodetype(Code, codetype);
	
		}


		/// <summary>
		/// ����ƽ̨
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
			public static  DataTable GetDataTableCode(IDbContext context = null) {
			return SyscodeRepository.GetInstance().GetDataTableCode(context);
	
		}


		


	}
}





