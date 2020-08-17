using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 售后收货实体类
	/// </summary>
	public class OrdRefundReceiveInfo {

		/// <summary>
		/// 售后表主键ID
		/// </summary>
		public int OrdRefundID { get; set; }

		/// <summary>
		/// 系统订单号
		/// </summary>
		public string ErpOrderCode { get; set; }

		/// <summary>
		/// 收货状态
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// 收货备注
		/// </summary>
		public string ReceiveRemark { get; set; }

		/// <summary>
		/// 责任方
		/// </summary>
		public int Duty { get; set; }

		/// <summary>
		/// 其他责任方
		/// </summary>
		public string DutyOther { get; set; }

		/// <summary>
		/// 退金额
		/// </summary>
		public decimal RefundAmount { get; set; }

		/// <summary>
		/// 退运费
		/// </summary>
		public decimal RefundFreight { get; set; }

		/// <summary>
		/// 寄回运费
		/// </summary>
		public decimal ReturnFreight { get; set; }

		/// <summary>
		/// 售后明细表主键ID
		/// </summary>
		public int[] OrdRefundItemID { get; set; }

		/// <summary>
		/// 订单明细表主键ID
		/// </summary>
		public int[] OrdItemID { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public int[] ProductsID { get; set; }

		/// <summary>
		/// 商品名称
		/// </summary>
		public string[] ProductsName { get; set; }

		/// <summary>
		/// 商品编码
		/// </summary>
		public string[] ProductsCode { get; set; }

		/// <summary>
		/// 商品货号
		/// </summary>
		public string[] ProductsNo { get; set; }

		/// <summary>
		/// 商品属性
		/// </summary>
		public string[] ProductsSkuSaleprop { get; set; }

		/// <summary>
		/// 商品SKUID
		/// </summary>
		public int[] ProductsSkuID { get; set; }

		/// <summary>
		/// 商品SKU码
		/// </summary>
		public string[] ProductsSkuCode { get; set; }

		/// <summary>
		/// 批次ID
		/// </summary>
		public int[] ProductsBatchID { get; set; }

		/// <summary>
		/// 批次号
		/// </summary>
		public string[] ProductsBatchCode { get; set; }

		/// <summary>
		/// 收货数量
		/// </summary>
		public string[] ReceiveNum { get; set; }

	}
}
