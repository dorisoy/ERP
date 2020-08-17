using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 打印模版表
	/// </summary>
	[Serializable]
	public partial class WarehousePrintTemplate {
		public WarehousePrintTemplate() { }


		private int _ID;
		/// <summary>
		/// 主键ID
		/// </summary>
		public int ID {
			set { _ID = value; }
			get { return _ID; }
		}


		private string _WarehouseCode;
		/// <summary>
		/// 仓库编码
		/// </summary>
		public string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}


		private string _Name;
		/// <summary>
		/// 模版名称
		/// </summary>
		public string Name {
			set { _Name = value; }
			get { return _Name; }
		}


		private int _TypeID;
		/// <summary>
		/// 模版类型 枚举值
		/// </summary>
		public int TypeID {
			set { _TypeID = value; }
			get { return _TypeID; }
		}


		private string _PrinterName;
		/// <summary>
		/// 默认打印机名称
		/// </summary>
		public string PrinterName {
			set { _PrinterName = value; }
			get { return _PrinterName; }
		}

		private decimal _Width;
		/// <summary>
		/// 纸张宽度 mm
		/// </summary>
		public decimal Width {
			set { _Width = value; }
			get { return _Width; }
		}

		private decimal _Height;
		/// <summary>
		/// 纸张高度 mm
		/// </summary>
		public decimal Height {
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

		private string _PrintProFieldWidth;
		/// <summary>
		/// 打印商品明细字段宽度百分比  多个以半角逗号隔开
		/// </summary>
		public string PrintProFieldWidth {
			set { _PrintProFieldWidth = value; }
			get { return _PrintProFieldWidth; }
		}

		private int _IsPrintPro;
		/// <summary>
		/// 是否打印商品明细 0：否 1：是
		/// </summary>
		public int IsPrintPro {
			set { _IsPrintPro = value; }
			get { return _IsPrintPro; }
		}

		private decimal _SecondPageOffset;
		/// <summary>
		/// 次页打印偏移 mm
		/// </summary>
		public decimal SecondPageOffset {
			set { _SecondPageOffset = value; }
			get { return _SecondPageOffset; }
		}

		private string _TemplateContent;
		/// <summary>
		/// 模版内容
		/// </summary>
		public string TemplateContent {
			set { _TemplateContent = value; }
			get { return _TemplateContent; }
		}

		private int _IsEnable;
		/// <summary>
		/// 状态 0：不可用 1：可用
		/// </summary>
		public int IsEnable {
			set { _IsEnable = value; }
			get { return _IsEnable; }
		}


		private int _IsDefault;
		/// <summary>
		/// 是否默认 0：否 1：是
		/// </summary>
		public int IsDefault {
			set { _IsDefault = value; }
			get { return _IsDefault; }
		}


		private string _CreatePerson;
		/// <summary>
		/// 创建人
		/// </summary>
		public string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}


		private DateTime _CreateDate;
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}


		private string _UpdatePerson;
		/// <summary>
		/// 修改人
		/// </summary>
		public string UpdatePerson {
			set { _UpdatePerson = value; }
			get { return _UpdatePerson; }
		}


		private DateTime _UpdateDate;
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}
	}
}

