using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 仓库快递表
	/// </summary>
	[Serializable]
	public partial class WarehouseExpress {
		public WarehouseExpress() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编号
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  string _Name;
	    /// <summary>
	    /// 快递名称
	    /// </summary>
		public  string Name {
			set { _Name = value; }
			get { return _Name; }
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
	    /// 联系电话
	    /// </summary>
		public  string ContactTel {
			set { _ContactTel = value; }
			get { return _ContactTel; }
		}
        
        private  int _LogisticsID;
	    /// <summary>
	    /// 物流表标识
	    /// </summary>
		public  int LogisticsID {
			set { _LogisticsID = value; }
			get { return _LogisticsID; }
		}

        
        private  int _PrinterType;
	    /// <summary>
	    /// 打印类型：针式、热敏，必选， 枚举类型。针式=0 热敏=1
	    /// </summary>
		public  int PrinterType {
			set { _PrinterType = value; }
			get { return _PrinterType; }
		}

        
        private  string _PrinterName;
	    /// <summary>
	    /// 默认打印机名称
	    /// </summary>
		public  string PrinterName {
			set { _PrinterName = value; }
			get { return _PrinterName; }
		}

        
        private  decimal _Width;
	    /// <summary>
	    /// 纸张宽度 mm
	    /// </summary>
		public  decimal Width {
			set { _Width = value; }
			get { return _Width; }
		}

        
        private  decimal _Height;
	    /// <summary>
	    /// 纸张高度 mm
	    /// </summary>
		public  decimal Height {
			set { _Height = value; }
			get { return _Height; }
		}

		private string _PrintProField;
		/// <summary>
		/// 打印商品明细字段  多个字段以半角逗号隔开
		/// </summary>
		public string PrintProField {
			set { _PrintProField = value; }
			get { return _PrintProField; }
		}

		private int _IsPrintPro;
		/// <summary>
		/// 是否打印商品明细 0否 1是
		/// </summary>
		public int IsPrintPro {
			set { _IsPrintPro = value; }
			get { return _IsPrintPro; }
		}
        
        private  string _TemplateContent;
	    /// <summary>
	    /// 模版内容
	    /// </summary>
		public string TemplateContent {
			set { _TemplateContent = value; }
			get { return _TemplateContent; }
		}
        
        private  string _CustId;
	    /// <summary>
	    /// 客户编号
	    /// </summary>
		public  string CustId {
			set { _CustId = value; }
			get { return _CustId; }
		}

        
        private  string _CustKey;
	    /// <summary>
	    /// 客户密码
	    /// </summary>
		public  string CustKey {
			set { _CustKey = value; }
			get { return _CustKey; }
		}

        
        private  string _InterFaceUrl;
	    /// <summary>
	    /// 接口地址
	    /// </summary>
		public  string InterFaceUrl {
			set { _InterFaceUrl = value; }
			get { return _InterFaceUrl; }
		}

        
        private  int _BillingMethods;
	    /// <summary>
	    /// 计费方式 0：按重计费 1：按件计费
	    /// </summary>
		public  int BillingMethods {
			set { _BillingMethods = value; }
			get { return _BillingMethods; }
		}

        
        private  int _Seq;
	    /// <summary>
	    /// 排序
	    /// </summary>
		public  int Seq {
			set { _Seq = value; }
			get { return _Seq; }
		}

        
        private  int _IsEnable;
	    /// <summary>
	    /// 是否可用 0：否 1：是
	    /// </summary>
		public  int IsEnable {
			set { _IsEnable = value; }
			get { return _IsEnable; }
		}

        
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

