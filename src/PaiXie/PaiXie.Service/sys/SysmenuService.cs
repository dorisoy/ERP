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
	/// 获取菜单
	/// </summary>
	/// <returns></returns>
		public static List<Sysmenu> GetSysmenulist() {
			return SysmenuRepository.GetInstance().GetSysmenulist();
		}
	/// <summary>
	/// 获取菜单
	/// </summary>
	/// <param name="ModeType">类型</param>
	/// <param name="ck">是否仓库</param>
	/// <returns></returns>
		public static DataTable GetDataTable(int ModeType = 1,string ck="") {
			return SysmenuRepository.GetInstance().GetDataTable( ModeType,ck);
		}
		/// <summary>
		/// 获取菜单
		/// </summary>
		/// <param name="userCode">用户代码</param>
		/// <param name="parentcode">父级代码</param>
		/// <param name="modetype">项目类型</param>
		/// <param name="IsSupper">是否超级管理员</param>
		/// <returns></returns>
		public static List<Sysmenu> GetSysMenuByUserCode(string userCode, string parentcode, int modetype, bool IsSupper) {
			return SysmenuRepository.GetInstance().GetSysMenuByUserCode(userCode, parentcode, modetype, IsSupper);
		}

	}
}





