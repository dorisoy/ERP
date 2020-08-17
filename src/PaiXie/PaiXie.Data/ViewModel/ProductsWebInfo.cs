using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 商品信息实体类 前端交互使用（例如添加和编辑商品页面）
	/// </summary>
	public class ProductsWebInfo {

		private int _ProID;
		/// <summary>
		/// 无注释
		/// </summary>
		public int ProID {
			set { _ProID = value; }
			get { return _ProID; }
		}


		private string _No;
		/// <summary>
		/// 商品货号：不能为空，自定义
		/// </summary>
		public string No {
			set { _No = value; }
			get { return _No; }
		}


		private string _ProCode;
		/// <summary>
		/// 商品编码：不能为空，不能重复，自定义。一个商品编码对应一个商品。当商品没有规格时，商品编码做为商品SKU码
		/// </summary>
		public string ProCode {
			set { _ProCode = value; }
			get { return _ProCode; }
		}


		private string _ProBarCode;
		/// <summary>
		/// 商品条码：可为空，不能重复，国家商品标准条码，也可自定义。对应商品。可扫描商品条码确认对应商品（查找、校验、出入库等）
		/// </summary>
		public string ProBarCode {
			set { _ProBarCode = value; }
			get { return _ProBarCode; }
		}


		private int _BrandID;
		/// <summary>
		/// 商品品牌ID
		/// </summary>
		public int BrandID {
			set { _BrandID = value; }
			get { return _BrandID; }
		}


		private int _CategoryID;
		/// <summary>
		/// 商品分类ID
		/// </summary>
		public int CategoryID {
			set { _CategoryID = value; }
			get { return _CategoryID; }
		}


		private int _ShelfLife;
		/// <summary>
		/// 保质期（天） 
		/// </summary>
		public int ShelfLife {
			set { _ShelfLife = value; }
			get { return _ShelfLife; }
		}


		private string _Name;
		/// <summary>
		/// 商品名称：不能为空
		/// </summary>
		public string Name {
			set { _Name = value; }
			get { return _Name; }
		}

		/// <summary>
		/// 商品类型：销售 1 物流 2 采用位运算
		/// </summary>
		public string[] SaleType { get; set; }

		private decimal _ProWeight;
		/// <summary>
		/// 商品重量
		/// </summary>
		public decimal ProWeight {
			set { _ProWeight = value; }
			get { return _ProWeight; }
		}


		private decimal _ProCostPrice;
		/// <summary>
		/// 商品成本价
		/// </summary>
		public decimal ProCostPrice {
			set { _ProCostPrice = value; }
			get { return _ProCostPrice; }
		}


		private decimal _ProSellingPrice;
		/// <summary>
		/// 商品销售价
		/// </summary>
		public decimal ProSellingPrice {
			set { _ProSellingPrice = value; }
			get { return _ProSellingPrice; }
		}


		private decimal _TaxRate;
		/// <summary>
		/// 商品税率： 必填
		/// </summary>
		public decimal TaxRate {
			set { _TaxRate = value; }
			get { return _TaxRate; }
		}


		private string _MeasurementUnitID;
		/// <summary>
		/// 单位ID 字典表维护
		/// </summary>
		public string MeasurementUnitID {
			set { _MeasurementUnitID = value; }
			get { return _MeasurementUnitID; }
		}

		/// <summary>
		/// 图片地址类型 0：本地上传 1：外部引用
		/// </summary>
		public int UrlType { get; set; }

		/// <summary>
		/// 商品小图地址(外部)
		/// </summary>
		public string PicUrl { get; set; }

		/// <summary>
		/// 商品小图地址(本地)
		/// </summary>
		public string[] SmallPic { get; set; }

		private string _Remark;
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}

		/// <summary>
		/// 商品SKUID数组
		/// </summary>
		public string[] ID { get; set; }
		/// <summary>
		/// 商品SKU码数组
		/// </summary>
		public string[] Code { get; set; }
		/// <summary>
		/// 商品销售属性数组
		/// </summary>
		public string[] Saleprop { get; set; }
		/// <summary>
		/// 商品条码数组
		/// </summary>
		public string[] BarCode { get; set; }
		/// <summary>
		/// 商品编码数组
		/// </summary>
		public string[] ProductsCode { get; set; }
		/// <summary>
		/// 商品SKU重量数组
		/// </summary>
		public string[] Weight { get; set; }
		/// <summary>
		/// 商品成本价数组
		/// </summary>
		public string[] CostPrice { get; set; }
		/// <summary>
		/// 商品销售价数组
		/// </summary>
		public string[] SellingPrice { get; set; }
	}
}
