using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 店铺商品下载更新信息表
	/// </summary>
	[Serializable]
	public partial class ShopUpdateProducts {
		public ShopUpdateProducts() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  int _ShopID;
	    /// <summary>
	    /// 店铺表标识
	    /// </summary>
		public  int ShopID {
			set { _ShopID = value; }
			get { return _ShopID; }
		}

        
        private  int _PlatformType;
	    /// <summary>
	    /// 平台类型 枚举
	    /// </summary>
		public  int PlatformType {
			set { _PlatformType = value; }
			get { return _PlatformType; }
		}

        
        private  int _ProductsID;
	    /// <summary>
	    /// 商品表标识 大于0表示导入成功
	    /// </summary>
		public  int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
		}

        
        private  int _GoodsId;
	    /// <summary>
	    /// 平台商品ID
	    /// </summary>
		public  int GoodsId {
			set { _GoodsId = value; }
			get { return _GoodsId; }
		}

        
        private  string _ProNo;
	    /// <summary>
	    /// 平台商品货号
	    /// </summary>
		public  string ProNo {
			set { _ProNo = value; }
			get { return _ProNo; }
		}

		/// <summary>
		/// 平台商品编码（对接使用）
		/// </summary>
		public string OuterId { get; set; }

        
        private  string _ProTitle;
	    /// <summary>
	    /// 平台商品名称
	    /// </summary>
		public  string ProTitle {
			set { _ProTitle = value; }
			get { return _ProTitle; }
		}
        
        private  string _ImgUrl;
	    /// <summary>
	    /// 平台商品图片地址
	    /// </summary>
		public  string ImgUrl {
			set { _ImgUrl = value; }
			get { return _ImgUrl; }
		}

        
        private  decimal _Price;
	    /// <summary>
	    /// 平台单价
	    /// </summary>
		public  decimal Price {
			set { _Price = value; }
			get { return _Price; }
		}

		private decimal _Num;
		/// <summary>
		/// 平台数量
		/// </summary>
		public decimal Num {
			set { _Num = value; }
			get { return _Num; }
		}
        
        private  int _CateId;
	    /// <summary>
	    /// 平台系统上架类目ID
	    /// </summary>
		public  int CateId {
			set { _CateId = value; }
			get { return _CateId; }
		}

        
        private  string _CustomCateId;
	    /// <summary>
	    /// 商家自定义分类ID，多个ID值以英文半角逗号隔开（值可能为空）
	    /// </summary>
		public  string CustomCateId {
			set { _CustomCateId = value; }
			get { return _CustomCateId; }
		}

        
        private  string _ProductKucList;
	    /// <summary>
	    /// Sku销售属性json字符串
	    /// </summary>
		public  string ProductKucList {
			set { _ProductKucList = value; }
			get { return _ProductKucList; }
		}

		/// <summary>
		/// 商品类型 1出售中(上架中)  2下架商品(除无货下架外)  3下架商品(缺货)  目前只有微小店才有区分2和3的两种下架，其它的下架都是2
		/// </summary>
		public int ProductsStatus { get; set; }

		/// <summary>
		/// 错误提示消息
		/// </summary>
		public string ErrorMessage { get; set; }

        private  string _CreatePerson;
	    /// <summary>
	    /// 创建人
	    /// </summary>
		public  string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}

        
        private  DateTime _CreateDate;
	    /// <summary>
	    /// 创建时间
	    /// </summary>
		public  DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}

        
        private  string _UpdatePerson;
	    /// <summary>
	    /// 修改人
	    /// </summary>
		public  string UpdatePerson {
			set { _UpdatePerson = value; }
			get { return _UpdatePerson; }
		}

        
        private  DateTime _UpdateDate;
	    /// <summary>
	    /// 修改时间
	    /// </summary>
		public  DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}

		
	}
}

