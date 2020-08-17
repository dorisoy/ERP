using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data {
	/// <summary>
	/// ����ⵥ��ϸ��
	/// </summary>
	[Serializable]
	public partial class WarehouseOutInStockItem {
		public WarehouseOutInStockItem() { }


		private int _ID;
		/// <summary>
		/// ����ID
		/// </summary>
		public int ID {
			set { _ID = value; }
			get { return _ID; }
		}


		private int _OutInStockID;
		/// <summary>
		/// ����ⵥ���ʶ
		/// </summary>
		public int OutInStockID {
			set { _OutInStockID = value; }
			get { return _OutInStockID; }
		}


		private string _OutInStockBillNo;
		/// <summary>
		/// ����ⵥ�ݱ�� ϵͳ����Ψһ
		/// </summary>
		public string OutInStockBillNo {
			set { _OutInStockBillNo = value; }
			get { return _OutInStockBillNo; }
		}


		private int _BillType;
		/// <summary>
		/// �������� ö�� �ɹ����10,�ɹ��˻�20,�������30,��������40,�˻����50,���۳���60,�̵�70,��λ80,�������90,��������100
		/// </summary>
		public int BillType {
			set { _BillType = value; }
			get { return _BillType; }
		}


		private int _SourceID;
		/// <summary>
		/// ��Դ���ݱ�ʶ
		/// </summary>
		public int SourceID {
			set { _SourceID = value; }
			get { return _SourceID; }
		}


		private string _SourceNo;
		/// <summary>
		/// ��Դ���ݱ��
		/// </summary>
		public string SourceNo {
			set { _SourceNo = value; }
			get { return _SourceNo; }
		}


		private int _StockWay;
		/// <summary>
		/// ����ⷽ�� -1���� 1���
		/// </summary>
		public int StockWay {
			set { _StockWay = value; }
			get { return _StockWay; }
		}


		private string _WarehouseCode;
		/// <summary>
		/// �ֿ���
		/// </summary>
		public string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}


		private int _Status;
		/// <summary>
		/// ����״̬ ö��  ״̬: δ�ύ=1�����=2�����=3
		/// </summary>
		public int Status {
			set { _Status = value; }
			get { return _Status; }
		}


		private int _IsAuditPrice;
		/// <summary>
		/// �Ƿ����0��1��
		/// </summary>
		public int IsAuditPrice {
			set { _IsAuditPrice = value; }
			get { return _IsAuditPrice; }
		}


		private int _ProductsID;
		/// <summary>
		/// ��Ʒ���ʶ
		/// </summary>
		public int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
		}


		private string _ProductsCode;
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public string ProductsCode {
			set { _ProductsCode = value; }
			get { return _ProductsCode; }
		}


		private string _ProductsName;
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public string ProductsName {
			set { _ProductsName = value; }
			get { return _ProductsName; }
		}


		private string _ProductsNo;
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public string ProductsNo {
			set { _ProductsNo = value; }
			get { return _ProductsNo; }
		}


		private int _ProductsSkuID;
		/// <summary>
		/// ��ƷSku���ʶ
		/// </summary>
		public int ProductsSkuID {
			set { _ProductsSkuID = value; }
			get { return _ProductsSkuID; }
		}


		private string _ProductsSkuCode;
		/// <summary>
		/// Sku��
		/// </summary>
		public string ProductsSkuCode {
			set { _ProductsSkuCode = value; }
			get { return _ProductsSkuCode; }
		}


		private string _ProductsSkuSaleprop;
		/// <summary>
		/// ��������(��ɫ����ɫ ���S)
		/// </summary>
		public string ProductsSkuSaleprop {
			set { _ProductsSkuSaleprop = value; }
			get { return _ProductsSkuSaleprop; }
		}


		private int _ProductsNum;
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public int ProductsNum {
			set { _ProductsNum = value; }
			get { return _ProductsNum; }
		}


		private int _LocationID;
		/// <summary>
		/// ��λ���ʶ
		/// </summary>
		public int LocationID {
			set { _LocationID = value; }
			get { return _LocationID; }
		}


		private int _ProductsBatchID;
		/// <summary>
		/// ��Ʒ���α��ʶ
		/// </summary>
		public int ProductsBatchID {
			set { _ProductsBatchID = value; }
			get { return _ProductsBatchID; }
		}


		private string _ProductsBatchCode;
		/// <summary>
		/// ��Ʒ���κ�
		/// </summary>
		public string ProductsBatchCode {
			set { _ProductsBatchCode = value; }
			get { return _ProductsBatchCode; }
		}


		private DateTime _ProductionDate;
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime ProductionDate {
			set { _ProductionDate = value; }
			get { return _ProductionDate; }
		}


		private decimal _CostPrice;
		/// <summary>
		/// �ɱ��� 
		/// </summary>
		public decimal CostPrice {
			set { _CostPrice = value; }
			get { return _CostPrice; }
		}


		private string _Remark;
		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}


		private DateTime _ConfirmDate;
		/// <summary>
		/// ȷ��ʱ��
		/// </summary>
		public DateTime ConfirmDate {
			set { _ConfirmDate = value; }
			get { return _ConfirmDate; }
		}


		private string _CreatePerson;
		/// <summary>
		/// ������
		/// </summary>
		public string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}


		private DateTime _CreateDate;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}


		private string _UpdatePerson;
		/// <summary>
		/// �޸���
		/// </summary>
		public string UpdatePerson {
			set { _UpdatePerson = value; }
			get { return _UpdatePerson; }
		}


		private DateTime _UpdateDate;
		/// <summary>
		/// �޸�ʱ��
		/// </summary>
		public DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}


		private int _SourceItemID;
		/// <summary>
		/// ��Դ������ϸ��ʶ
		/// </summary>
		public int SourceItemID {
			set { _SourceItemID = value; }
			get { return _SourceItemID; }
		}
	}
}

