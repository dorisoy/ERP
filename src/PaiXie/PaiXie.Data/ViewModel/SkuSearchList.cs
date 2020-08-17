using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	public class SkuSearchList {

		/// <summary>
		/// 操作结果代码 默认值1代表成功，其它都是失败
		/// </summary>
		public int result { get; set; }
		/// <summary>
		///结果消息 默认值 OK
		/// </summary>
		public string message { get; set; }
		/// <summary>
		/// 商品名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 商品属性
		/// </summary>
		public string Attribute { get; set; }
		/// <summary>
		/// 采购价
		/// </summary>
		public decimal PurchasePrice { get; set; }
		
	}
}
