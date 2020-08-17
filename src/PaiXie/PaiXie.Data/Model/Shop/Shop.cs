using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 店铺信息表
	/// </summary>
	[Serializable]
	public partial class Shop {
		public Shop() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}


		private string _Code;
		/// <summary>
		/// 店铺编码 唯一值
		/// </summary>
		public string Code {
			set { _Code = value; }
			get { return _Code; }
		}

        
        private  string _Name;
	    /// <summary>
	    /// 店铺名称
	    /// </summary>
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
		}

        
        private  string _Type;
	    /// <summary>
	    /// 店铺类型code(网店、官网、门店）
	    /// </summary>
		public  string Type {
			set { _Type = value; }
			get { return _Type; }
		}

        
        private  int _PlatformType;
	    /// <summary>
	    /// 平台类型 枚举值
	    /// </summary>
		public  int PlatformType {
			set { _PlatformType = value; }
			get { return _PlatformType; }
		}

        
        private  string _StoreAddr;
	    /// <summary>
	    /// 门店地址 （省市区 街道）
	    /// </summary>
		public  string StoreAddr {
			set { _StoreAddr = value; }
			get { return _StoreAddr; }
		}

        
        private  string _Longitude;
	    /// <summary>
	    /// 经度
	    /// </summary>
		public  string Longitude {
			set { _Longitude = value; }
			get { return _Longitude; }
		}

        
        private  string _Latitude;
	    /// <summary>
	    /// 纬度
	    /// </summary>
		public  string Latitude {
			set { _Latitude = value; }
			get { return _Latitude; }
		}

        
        private  string _AppKey;
	    /// <summary>
	    /// AppKey
	    /// </summary>
		public  string AppKey {
			set { _AppKey = value; }
			get { return _AppKey; }
		}

        
        private  string _AppSecret;
	    /// <summary>
	    /// AppSecret
	    /// </summary>
		public  string AppSecret {
			set { _AppSecret = value; }
			get { return _AppSecret; }
		}

        
        private  string _AppSession;
	    /// <summary>
	    /// AppSession
	    /// </summary>
		public  string AppSession {
			set { _AppSession = value; }
			get { return _AppSession; }
		}

        
        private  string _RefreshToken;
	    /// <summary>
	    /// RefreshToken
	    /// </summary>
		public  string RefreshToken {
			set { _RefreshToken = value; }
			get { return _RefreshToken; }
		}

        
        private  string _ContactPerson;
	    /// <summary>
	    /// 联系人
	    /// </summary>
		public  string ContactPerson {
			set { _ContactPerson = value; }
			get { return _ContactPerson; }
		}

        
        private  string _ContactTel;
	    /// <summary>
	    /// 联系人电话
	    /// </summary>
		public  string ContactTel {
			set { _ContactTel = value; }
			get { return _ContactTel; }
		}

        
        private  string _Website;
	    /// <summary>
	    /// 网址
	    /// </summary>
		public  string Website {
			set { _Website = value; }
			get { return _Website; }
		}

        
        private  string _Remark;
	    /// <summary>
	    /// 备注
	    /// </summary>
		public  string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}

        
        private  int _IsEnable;
	    /// <summary>
	    /// 是否可用１是０否
	    /// </summary>
		public  int IsEnable {
			set { _IsEnable = value; }
			get { return _IsEnable; }
		}

        
        private  string _CreatePerson;
	    /// <summary>
	    /// 创建人（创建用户姓名）
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
	    /// 修改人（用户姓名）
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

