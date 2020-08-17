using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class SysmenuService  : BaseService<Sysmenu> {
    
		public static int Update(Sysmenu entity) {
			return SysmenuRepository.GetInstance().Update(entity);
		}

	

		public static int Add(Sysmenu entity) {
			return SysmenuRepository.GetInstance().Add(entity);
		}

	/// <summary>
	/// ��ȡ�˵�
	/// </summary>
	/// <returns></returns>
		public static List<Sysmenu> GetSysmenulist() {
			return SysmenuRepository.GetInstance().GetSysmenulist();
		}
	/// <summary>
	/// ��ȡ�˵�
	/// </summary>
	/// <param name="ModeType">����</param>
	/// <param name="ck">�Ƿ�ֿ�</param>
	/// <returns></returns>
		public static DataTable GetDataTable(int ModeType = 1,string ck="") {
			return SysmenuRepository.GetInstance().GetDataTable( ModeType,ck);
		}
		/// <summary>
		/// ��ȡ�˵�
		/// </summary>
		/// <param name="userCode">�û�����</param>
		/// <param name="parentcode">��������</param>
		/// <param name="modetype">��Ŀ����</param>
		/// <param name="IsSupper">�Ƿ񳬼�����Ա</param>
		/// <returns></returns>
		public static List<Sysmenu> GetSysMenuByUserCode(string userCode, string parentcode, int modetype, bool IsSupper) {
			return SysmenuRepository.GetInstance().GetSysMenuByUserCode(userCode, parentcode, modetype, IsSupper);
		}

	}
}





