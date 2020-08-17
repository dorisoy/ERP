using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 库区信息实体类 前端交互使用（例如添加和编辑库区页面）
	/// </summary>
	public class WarehouseLocationInfo {

		/// <summary>
		/// 库区ID
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// 库区名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 库区代码
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// 库区类型
		/// </summary>
		public int TypeID { get; set; }

		/// <summary>
		/// 结构数量
		/// </summary>
		public int[] StructCount { get; set; }

		/// <summary>
		/// 结构名称
		/// </summary>
		public string[] StructName { get; set; }

		/// <summary>
		/// 结构代码
		/// </summary>
		public string[] StructCode { get; set; }
	}
}
