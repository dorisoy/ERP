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
		/// 获取实体  通过代码
		/// </summary>
		/// <param name="Code">代码</param>
		/// <returns></returns>
		public static Syscode GetSyscodeByCode(string Code) {
			return SyscodeRepository.GetInstance().GetSyscodeByCode(Code);

		}
		/// <summary>
		/// 字典列表
		/// </summary>
		/// <param name="CodeType">项目类型</param>
		/// <returns></returns>
		public static DataTable GetSyscodeTree(string CodeType) {
			return SyscodeRepository.GetInstance().GetSyscodeTree(CodeType);
		}

		/// <summary>
		/// 获取实体  通过 id 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Syscode Code(int id) {
			return SyscodeRepository.GetInstance().Code(id);
		}
		/// <summary>
		/// 删除 实体    通过 id 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static int deleteCode(string id) {
			return SyscodeRepository.GetInstance().deleteCode(id);
		}
		/// <summary>
		/// 检查代码唯一性
		/// </summary>
		/// <param name="Code">代码</param>
		/// <param name="ID">主键</param>
		/// <returns></returns>
		public static int CheckCode(string Code, int ID) {
			return SyscodeRepository.GetInstance().CheckCode(Code, ID);
		}
		/// <summary>
		/// 检查代码唯一性
		/// </summary>
		/// <param name="Code">代码</param>
		/// <returns></returns>
		public static int CheckCode(string Code) {
			return SyscodeRepository.GetInstance().CheckCode(Code);
		}

	/// <summary>
	/// 获取字典列表  通过字典类型 
	/// </summary>
	/// <param name="CodeType">字典类型</param>
	/// <returns></returns>
		public static List<Syscode> GetSyscodebycodetype(string CodeType) {
			return SyscodeRepository.GetInstance().GetSyscodebycodetype(CodeType);
		}

		/// <summary>
		/// 获取实体  通过 字典类型  字典代码
		/// </summary>
		/// <param name="Code">字典代码</param>
		/// <param name="codetype">字典类型</param>
		/// <returns></returns>
		public static  Syscode GetSyscodeByCodetype(string Code, string codetype) {
			return SyscodeRepository.GetInstance().GetSyscodeByCodetype(Code, codetype);
	
		}


		/// <summary>
		/// 店铺平台
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
			public static  DataTable GetDataTableCode(IDbContext context = null) {
			return SyscodeRepository.GetInstance().GetDataTableCode(context);
	
		}


		


	}
}





