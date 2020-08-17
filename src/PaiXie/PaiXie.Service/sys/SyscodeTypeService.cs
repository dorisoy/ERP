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
		/// 字典类型列表
	/// </summary>
	/// <param name="name">名称</param>
	/// <returns></returns>
		public static DataTable GetJsonTreeSyscodeType(string name="") {
			return SyscodeTypeRepository.GetInstance().GetJsonTreeSyscodeType(name);
		}
		
		/// <summary>
		/// 获取实体  
		/// </summary>
		/// <param name="codetype">类型</param>
		/// <returns></returns>
		public static SyscodeType CodeType(string codetype) {
			return SyscodeTypeRepository.GetInstance().CodeType(codetype);
		}
		/// <summary>
		/// 获取实体
		/// </summary>
		/// <param name="id">主键id</param>
		/// <returns></returns>
		public static SyscodeType CodeType(int id) {
			return SyscodeTypeRepository.GetInstance().CodeType(id);
		}
		/// <summary>
		/// 删除实体   
		/// </summary>
		/// <param name="id">主键id</param>
		/// <returns></returns>
		public static int deleteCodeType(string id) {
			return SyscodeTypeRepository.GetInstance().deleteCodeType(id);
		}
		/// <summary>
		/// 检查代码唯一性
		/// </summary>
		/// <param name="Code">代码</param>
		/// <param name="ID">主键id</param>
		/// <returns></returns>
		public static int CheckCode(string Code, int ID) {
			return SyscodeTypeRepository.GetInstance().CheckCode(Code,ID);
		}
		/// <summary>
		/// 检查代码唯一性
		/// </summary>
		/// <param name="Code">代码</param>
		/// <returns></returns>
		public static int CheckCode(string Code) {
			return SyscodeTypeRepository.GetInstance().CheckCode(Code);
		}
	
	}
}





