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
		/// 获取菜单事件
		/// </summary>
		/// <returns></returns>
		public static  List<Sysbutton> GetSysbuttonlist() {
			return SysbuttonRepository.GetInstance().GetSysbuttonlist();
		}
		/// <summary>
		/// 获取菜单事件
		/// </summary>
		/// <param name="UserCode">用户代码</param>
		/// <param name="code">事件代码</param>
		/// <returns></returns>
		public static List<Sysbutton> GetSysbuttonlist(string UserCode, string code) {
			return SysbuttonRepository.GetInstance().GetSysbuttonlist(UserCode,code);
		}


		public static  int GetPower(string UserCode, string url) {
			return SysbuttonRepository.GetInstance().GetPower(UserCode, url);
		}


		/// <summary>
		/// 获取实体 by   url
		/// </summary>
		/// <param name="url">url</param>
		/// <returns></returns>
		public static Sysbutton GetSysbutton(string url) {
			return SysbuttonRepository.GetInstance().GetSysbutton(url);
		}
		/// <summary>
		///  获取实体  通过代码 
		/// </summary>
		/// <param name="Code">代码</param>
		/// <returns></returns>
		public static  Sysbutton GetSysbuttonUrl(string Code) {
			return SysbuttonRepository.GetInstance().GetSysbuttonUrl(Code);
		}		
	}
}