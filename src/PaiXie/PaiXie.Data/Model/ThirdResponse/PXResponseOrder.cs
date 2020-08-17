using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data.PXResponseOrder {
	/// <summary>
	/// 订单接口实体
	/// </summary>
	public class OrderRespone {
		public string status { get; set; }
		public string code { get; set; }
		public string msg { get; set; }
		public OrderInfo record { get; set; }
		public List<OrderInfo> recordset { get; set; }
		public PageInfo pageinfo { get; set; }
	}

	/// <summary>
	/// 订单详细信息
	/// </summary>
	public class OrderInfo {
		/// <summary>
		/// 订单编号
		/// </summary>
		public string order_id { get; set; }
		/// <summary>
		/// 下单时间
		/// </summary>
		public string created { get; set; }
		/// <summary>
		/// 付款时间
		/// </summary>
		public string pay_time { get; set; }
		/// <summary>
		/// 发货时间
		/// </summary>
		public string consign_time { get; set; }
		/// <summary>
		/// 结束时间
		/// </summary>
		public string end_time { get; set; }
		/// <summary>
		/// 订单最后修改时间
		/// </summary>
		public string modified { get; set; }
		/// <summary>
		/// 1物流配送2商家配送
		/// </summary>
		public string express_type { get; set; }
		/// <summary>
		/// 配送方式
		/// </summary>
		public string express_id { get; set; }
		/// <summary>
		/// 物流单号
		/// </summary>
		public string express_code { get; set; }
		/// <summary>
		/// 订单应收总金额[包括运费]
		/// </summary>
		public string total_amount { get; set; }
		/// <summary>
		/// 实际支付金额
		/// </summary>
		public string payment { get; set; }
		/// <summary>
		/// 邮费
		/// </summary>
		public string post_fee { get; set; }
		/// <summary>
		/// 订单商品总金额
		/// </summary>
		public string item_fee { get; set; }
		/// <summary>
		/// 订单优惠总金额
		/// </summary>
		public string discount_fee { get; set; }
		/// <summary>
		/// 平台费
		/// </summary>
		public string platform_fee { get; set; }
		/// <summary>
		/// 订单状态：1:等待买家付款, 2:买家已付款,等待卖家发货,3:卖家已发货,等待买家收货 4:买家确认收货,5:交易成功,6:交易关闭
		/// </summary>
		public string orderstatus { get; set; }
		/// <summary>
		/// 是否售后1正常9售后
		/// </summary>
		public string isaftersales { get; set; }
		/// <summary>
		/// 买家备注
		/// </summary>
		public string buyer_memo { get; set; }
		/// <summary>
		/// 卖家(商家)备注
		/// </summary>
		public string seller_memo { get; set; }
		/// <summary>
		/// 卖家发货超时1未超时，2超时
		/// </summary>
		public string isdeliverytimeout { get; set; }
		/// <summary>
		/// 收货人
		/// </summary>
		public string receiver_name { get; set; }
		/// <summary>
		/// 收货人的所在省份
		/// </summary>
		public string receiver_state { get; set; }
        /// <summary>
		/// 收货人的所在城市
        /// </summary>
		public string receiver_city { get; set; }
		/// <summary>
		/// 收货人的所在地区
		/// </summary>
		public string receiver_district { get; set; }
		/// <summary>
		/// 收件人地址
		/// </summary>
		public string receiver_address { get; set; }
		/// <summary>
		/// 收件人手机号
		/// </summary>
		public string receiver_mobile { get; set; }
		/// <summary>
		/// 邮政编码
		/// </summary>
		public string receiver_zip { get; set; }
        /// <summary>
		/// 订单商品信息
        /// </summary>
		public List<ItemInfo> itemlist { get; set; }
	}

	/// <summary>
	/// 订单详细信息
	/// </summary>
	public class ItemInfo {
		/// <summary>
		/// 商品名称
		/// </summary>
		public string title { get; set; }
		/// <summary>
		/// 商品单价
		/// </summary>
		public string price { get; set; }
		/// <summary>
		/// 下单商品数量
		/// </summary>
		public string nums { get; set; }
		/// <summary>
		/// 商品总优惠
		/// </summary>
		public string discount_fee { get; set; }
		/// <summary>
		/// 成交金额
		/// </summary>
		public string money { get; set; }
		/// <summary>
		/// 商家外部编码
		/// </summary>
		public string outer_id { get; set; }
		/// <summary>
		/// 外部网店自己定义的Sku编号
		/// </summary>
		public string outer_sku_id { get; set; }
		/// <summary>
		/// 售后状态：1:未申请, 2:申请中, 3:卖家初审通过, 4:卖家初审不通过, 5:买家退货, 6:卖家确认退款, 7:卖家拒绝申请, 99:未付款取消
		/// </summary>
		public string aftersalesstatus { get; set; }
	}

	/// <summary>
	/// 页码信息
	/// </summary>
	public class PageInfo {
		public string page { get; set; }
		public string pages { get; set; }
		public string page_size { get; set; }
		public string records { get; set; }
	}
}
