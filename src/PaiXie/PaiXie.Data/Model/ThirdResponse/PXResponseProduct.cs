using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	public class ProductKucList {
		/// <summary>
		/// 商品间的所有关联是通过outer_id
		/// </summary>
		public string Outer_id { get; set; }

		/// <summary>
		/// 销售属性
		/// </summary>
		public string Properties_alias { get; set; }

		/// <summary>
		/// sku_id
		/// </summary>
		public string Sku_id { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public string Quantity { get; set; }

		/// <summary>
		/// 价格
		/// </summary>
		public string Price { get; set; }

	}

	public class ProductList {
		/// <summary>
		/// 商品ID
		/// </summary>
		public string goods_id { get; set; }

		/// <summary>
		/// 平台商品编码（对接使用）
		/// </summary>
		public string outer_id{get;set;}

		/// <summary>
		/// 商品货号
		/// </summary>
		public string Pro_No { get; set; }

		/// <summary>
		/// 商品名称
		/// </summary>
		public string Pro_Title { get; set; }

		/// <summary>
		/// 图片地址
		/// </summary>
		public string Img_Url { get; set; }

		/// <summary>
		/// 商品数量
		/// </summary>
		public string Num { get; set; }

		/// <summary>
		/// 商品价格
		/// </summary>
		public string Price { get; set; }

		/// <summary>
		/// 系统上架类目ID
		/// </summary>
		public string CateId { get; set; }

		/// <summary>
		/// 商家自定义分类ID，多个ID值以英文半角逗号隔开（值可能为空）
		/// </summary>
		public string CustomCateId { get; set; }

		/// <summary>
		/// 商品库存列表
		/// </summary>
		public List<ProductKucList> ProductKucList { get; set; }

	}



	public class Data {
		/// <summary>
		/// 商品总数
		/// </summary>
		public int Pro_Total { get; set; }

		/// <summary>
		/// 商品列表
		/// </summary>
		public List<ProductList> ProductList { get; set; }

	}



	public class Root {
		/// <summary>
		/// 代码
		/// </summary>
		public int code { get; set; }

		/// <summary>
		/// 消息
		/// </summary>
		public string msg { get; set; }

		/// <summary>
		/// 数据
		/// </summary>
		public Data Data { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		public int status { get; set; }
	}

	public class wxdrequest {
		/// <summary>
		/// 代码
		/// </summary>
		public int code { get; set; }

		/// <summary>
		/// 消息
		/// </summary>
		public string msg { get; set; }
	}
}
