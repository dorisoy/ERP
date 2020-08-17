using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 售后实体类 前端交互
	/// </summary>
	public class OrdRefundWebInfo {

		/// <summary>
		/// 订单来源 枚举
		/// </summary>
		public int OrderSource { get; set; }

		/// <summary>
		/// 店铺ID
		/// </summary>
		public int ShopID { get; set; }

		/// <summary>
		/// 售后类型 枚举
		/// </summary>
		public int RefundType { get; set; }

		/// <summary>
		/// 售后责任方
		/// </summary>
		public int Duty { get; set; }

		/// <summary>
		/// 其他责任方
		/// </summary>
		public string DutyOther { get; set; }

		/// <summary>
		/// 系统订单号
		/// </summary>
		public string ErpOrderCode { get; set; }

		/// <summary>
		/// 外部订单号
		/// </summary>
		public string OutOrderCode { get; set; }

		/// <summary>
		/// 销售出库单号
		/// </summary>
		public string OutboundBillNo { get; set; }

		/// <summary>
		/// 买家地址
		/// </summary>
		public string BuyAddr { get; set; }

		/// <summary>
		/// 买家姓名
		/// </summary>
		public string BuyName { get; set; }

		/// <summary>
		/// 买家手机
		/// </summary>
		public string BuyMtel { get; set; }

		/// <summary>
		/// 买家电话
		/// </summary>
		public string BuyTel { get; set; }

		/// <summary>
		/// 售后收货人
		/// </summary>
		public string ReceivePerson { get; set; }

		/// <summary>
		/// 售后收货人电话或手机
		/// </summary>
		public string ReceiveTel { get; set; }

		/// <summary>
		/// 售后收货人地址
		/// </summary>
		public string ReceiveAddress { get; set; }

		/// <summary>
		/// 售后收货人邮编
		/// </summary>
		public string ReceivePostCode { get; set; }

		/// <summary>
		/// 寄回物流
		/// </summary>
		public string ExpressCompany { get; set; }

		/// <summary>
		/// 寄回运单号
		/// </summary>
		public string WaybillNo { get; set; }

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
		public string ReturnFreight { get; set; }

		/// <summary>
		/// 售后原因
		/// </summary>
		public string Reason { get; set; }

		/// <summary>
		/// 售后详细原因
		/// </summary>
		public string ReasonDetail { get; set; }

		/// <summary>
		/// 订单明细表主键ID
		/// </summary>
		public int[] OrdItemID { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public int[] ProductsID { get; set; }

		/// <summary>
		/// 商品编码
		/// </summary>
		public string[] ProductsCode { get; set; }

		/// <summary>
		/// 商品名称
		/// </summary>
		public string[] ProductsName { get; set; }

		/// <summary>
		/// 商品货号
		/// </summary>
		public string[] ProductsNo { get; set; }

		/// <summary>
		/// 商品SKUID
		/// </summary>
		public int[] ProductsSkuID { get; set; }

		/// <summary>
		/// 商品SKU码
		/// </summary>
		public string[] ProductsSkuCode { get; set; }

		/// <summary>
		/// 销售属性
		/// </summary>
		public string[] ProductsSkuSaleprop { get; set; }

		/// <summary>
		/// 批次ID
		/// </summary>
		public int[] ProductsBatchID { get; set; }

		/// <summary>
		/// 批次号
		/// </summary>
		public string[] ProductsBatchCode { get; set; }

		/// <summary>
		/// 下单数量
		/// </summary>
		public int[] ProductsNum { get; set; }

		/// <summary>
		/// 售后数量
		/// </summary>
		public int[] RefundNum { get; set; }

		/// <summary>
		/// 商品实际销售价 扣除优惠之后的价格
		/// </summary>
		public decimal[] ActualSellingPrice { get; set; }
	}
}
