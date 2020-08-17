using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class SyscodeTypeService  : BaseService<SyscodeType> {
    
		public static int Update(SyscodeType entity) {
			return SyscodeTypeRepository.GetInstance().Update(entity);
		}

	

		public static int Add(SyscodeType entity) {
			return SyscodeTypeRepository.GetInstance().Add(entity);
		}
	/// <summary>
		/// �ֵ������б�
	/// </summary>
	/// <param name="name">����</param>
	/// <returns></returns>
		public static DataTable GetJsonTreeSyscodeType(string name="") {
			return SyscodeTypeRepository.GetInstance().GetJsonTreeSyscodeType(name);
		}
		
		/// <summary>
		/// ��ȡʵ��  
		/// </summary>
		/// <param name="codetype">����</param>
		/// <returns></returns>
		public static SyscodeType CodeType(string codetype) {
			return SyscodeTypeRepository.GetInstance().CodeType(codetype);
		}
		/// <summary>
		/// ��ȡʵ��
		/// </summary>
		/// <param name="id">����id</param>
		/// <returns></returns>
		public static SyscodeType CodeType(int id) {
			return SyscodeTypeRepository.GetInstance().CodeType(id);
		}
		/// <summary>
		/// ɾ��ʵ��   
		/// </summary>
		/// <param name="id">����id</param>
		/// <returns></returns>
		public static int deleteCodeType(string id) {
			return SyscodeTypeRepository.GetInstance().deleteCodeType(id);
		}
		/// <summary>
		/// ������Ψһ��
		/// </summary>
		/// <param name="Code">����</param>
		/// <param name="ID">����id</param>
		/// <returns></returns>
		public static int CheckCode(string Code, int ID) {
			return SyscodeTypeRepository.GetInstance().CheckCode(Code,ID);
		}
		/// <summary>
		/// ������Ψһ��
		/// </summary>
		/// <param name="Code">����</param>
		/// <returns></returns>
		public static int CheckCode(string Code) {
			return SyscodeTypeRepository.GetInstance().CheckCode(Code);
		}
	
	}
}





