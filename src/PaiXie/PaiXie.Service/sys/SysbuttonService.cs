using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class SysbuttonService  : BaseService<Sysbutton> {   
		public static int Update(Sysbutton entity) {
			return SysbuttonRepository.GetInstance().Update(entity);
		}
		public static int Add(Sysbutton entity) {
			return SysbuttonRepository.GetInstance().Add(entity);
		}
		/// <summary>
		/// ��ȡ�˵��¼�
		/// </summary>
		/// <returns></returns>
		public static  List<Sysbutton> GetSysbuttonlist() {
			return SysbuttonRepository.GetInstance().GetSysbuttonlist();
		}
		/// <summary>
		/// ��ȡ�˵��¼�
		/// </summary>
		/// <param name="UserCode">�û�����</param>
		/// <param name="code">�¼�����</param>
		/// <returns></returns>
		public static List<Sysbutton> GetSysbuttonlist(string UserCode, string code) {
			return SysbuttonRepository.GetInstance().GetSysbuttonlist(UserCode,code);
		}


		public static  int GetPower(string UserCode, string url) {
			return SysbuttonRepository.GetInstance().GetPower(UserCode, url);
		}


		/// <summary>
		/// ��ȡʵ�� by   url
		/// </summary>
		/// <param name="url">url</param>
		/// <returns></returns>
		public static Sysbutton GetSysbutton(string url) {
			return SysbuttonRepository.GetInstance().GetSysbutton(url);
		}
		/// <summary>
		///  ��ȡʵ��  ͨ������ 
		/// </summary>
		/// <param name="Code">����</param>
		/// <returns></returns>
		public static  Sysbutton GetSysbuttonUrl(string Code) {
			return SysbuttonRepository.GetInstance().GetSysbuttonUrl(Code);
		}		
	}
}