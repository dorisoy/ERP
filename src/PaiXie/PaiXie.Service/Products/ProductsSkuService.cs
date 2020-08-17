using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
using Newtonsoft.Json;
using PaiXie.Utils;
using Newtonsoft.Json.Converters;
namespace  PaiXie.Service 
{
	public class ProductsSkuService : BaseService<ProductsSku> {

		#region Update

		public static int Update(ProductsSku entity, out string oldMessage, out string newMessage, IDbContext context) {
			oldMessage = string.Empty;
			newMessage = string.Empty;
			//����֮ǰҪ�Ȳ����������ŵ� oldMessage
			ProductsSku newProductsSku = GetSingleProductsSku(entity.ID, context);
			oldMessage = JsonConvert.SerializeObject(newProductsSku, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
			newProductsSku.BarCode = entity.BarCode;
			newProductsSku.Code = entity.Code;
			newProductsSku.CostPrice = entity.CostPrice;
			newProductsSku.ProductsCode = entity.ProductsCode;
			newProductsSku.ProductsID = entity.ProductsID;
			newProductsSku.Saleprop = entity.Saleprop;
			newProductsSku.SellingPrice = entity.SellingPrice;
			newProductsSku.UpdateDate = entity.UpdateDate;
			newProductsSku.UpdatePerson = entity.UpdatePerson;
			newProductsSku.Weight = entity.Weight;
			int rowsAffected = ProductsSkuRepository.GetInstance().Update(newProductsSku, context);
			if (rowsAffected > 0) {
				newMessage = JsonConvert.SerializeObject(newProductsSku, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
			}
			return rowsAffected;
		}

		#endregion

		#region Add

		public static int Add(ProductsSku entity, IDbContext context) {
			return ProductsSkuRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region ������ƷIDɾ������Ʒ����Sku

		/// <summary>
		/// ������ƷIDɾ������Ʒ����Sku
		/// </summary>
		/// <param name="productsID">��Ʒ���ʶ</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int DelByProductsID(int productsID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().DelByProductsID(productsID, context);
		}

		#endregion

		#region ����SKUIDɾ��ָ��Sku

		/// <summary>
		/// ����SKUIDɾ��ָ��Sku
		/// </summary>
		/// <param name="productsSkuID">��ƷSKU���ʶ</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Del(int productsSkuID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().Del(productsSkuID, context);
		}

		#endregion

		#region ������ƷSKUID��ȡ��ƷSku��Ϣ

		/// <summary>
		/// ������ƷSKUID��ȡ��ƷSku��Ϣ
		/// </summary>
		/// <param name="productsSkuID">��ƷSKUID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static ProductsSku GetSingleProductsSku(int productsSkuID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetSingleProductsSku(productsSkuID, context);
		}

		#endregion

		#region ������ƷSku���ȡ��ƷSku��Ϣ

		/// <summary>
		/// ������ƷSku���ȡ��ƷSku��Ϣ
		/// </summary>
		/// <param name="productsSkuCode">��ƷSku��</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static ProductsSku GetSingleProductsSku(string productsSkuCode, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetSingleProductsSku(productsSkuCode, context);
		}

		#endregion

		#region ������ƷSku���ȡ��ƷSku��Ϣ ����Ʒ���ƺͻ���

		/// <summary>
		/// ������ƷSku���ȡ��ƷSku��Ϣ ����Ʒ���ƺͻ���
		/// </summary>
		/// <param name="productsSkuCode">��ƷSku��</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static ProductsSkuInfo GetSingleProductsSkuInfo(string productsSkuCode, IDbContext context = null) {
			DataTable dt = ProductsSkuRepository.GetInstance().GetSingleProductsSkuInfo(productsSkuCode, context);
			ProductsSkuInfo productsSkuInfo = null;
			if (dt.Rows.Count > 0) {
				productsSkuInfo = new ProductsSkuInfo();
				DataRow dr = dt.Rows[0];
				productsSkuInfo.ID = ZConvert.StrToInt(dr["ID"]);
				productsSkuInfo.Code = ZConvert.ToString(dr["Code"]);
				productsSkuInfo.BarCode = ZConvert.ToString(dr["BarCode"]);
				productsSkuInfo.CostPrice = ZConvert.StrToDecimal(dr["CostPrice"]);
				productsSkuInfo.SellingPrice = ZConvert.StrToDecimal(dr["SellingPrice"]);
				productsSkuInfo.ProductsCode = ZConvert.ToString(dr["ProductsCode"]);
				productsSkuInfo.ProductsID = ZConvert.StrToInt(dr["ProductsID"]);
				productsSkuInfo.ProductsName = ZConvert.ToString(dr["ProductsName"]);
				productsSkuInfo.ProductsNo = ZConvert.ToString(dr["ProductsNo"]);
				productsSkuInfo.Saleprop = ZConvert.ToString(dr["Saleprop"]);
				productsSkuInfo.Weight = ZConvert.StrToDecimal(dr["Weight"]);
				productsSkuInfo.CreatePerson = ZConvert.ToString(dr["CreatePerson"]);
				productsSkuInfo.CreateDate = ZConvert.StrToDateTime(dr["CreateDate"], DateTime.Now);
				productsSkuInfo.UpdatePerson = ZConvert.ToString(dr["UpdatePerson"]);
				productsSkuInfo.UpdateDate = ZConvert.StrToDateTime(dr["UpdateDate"], DateTime.Now);
			}
			return productsSkuInfo;
		}

		#endregion

		#region ������ƷSku���ȡ�ų�ָ��SkuID֮�����ƷSku��Ϣ(�޸�Skuʱʹ��)

		/// <summary>
		/// ������ƷSku���ȡ�ų�ָ��SkuID֮�����ƷSku��Ϣ(�޸�Skuʱʹ��)
		/// </summary>
		/// <param name="productsSkuCode">��ƷSku��</param>
		/// <param name="exceptProductsSkuID">��Ҫ�ų���SkuID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static ProductsSku GetSingleProductsSku(string productsSkuCode, int exceptProductsSkuID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetSingleProductsSku(productsSkuCode, exceptProductsSkuID, context);
		}

		#endregion

		#region ������ƷID����SKUʵ���б�

		/// <summary>
		/// ������ƷID����SKUʵ���б�
		/// </summary>
		/// <param name="productsId">��ƷID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<ProductsSku> GetManyProductsSku(int productsID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetManyProductsSku(productsID, context);
		}

		#endregion

		#region ������ƷSKU���ȡSKUID

		/// <summary>
		/// ������ƷSKU���ȡSKUID
		/// </summary>
		/// <param name="productsSkuCode">��ƷSKU��</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetProductsSkuID(string productsSkuCode, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetProductsSkuID(productsSkuCode, context);
		}

		#endregion

		#region ������ƷSKUID��ȡ�����Ϣ

		/// <summary>
		/// ������ƷSKUID��ȡ�����Ϣ
		/// </summary>
		/// <param name="productsSkuID">��ƷSKUID</param>
		/// <param name="warehouseCode">�ֿ���</param>
		/// <param name="isFilterStopSelling">�Ƿ�����¼ܿ�� 0���� 1����</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static ProductsSkuInventory GetProductsSkuInventory(int productsSkuID,string warehouseCode,int isFilterStopSelling, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetProductsSkuInventory(productsSkuID, warehouseCode, isFilterStopSelling, context);
		}

		#endregion

		#region ������ƷSKUID��ȡ�ֿ�ɷ��������Ϣ

		/// <summary>
		/// ������ƷSKUID��ȡ�ֿ�ɷ��������Ϣ
		/// </summary>
		/// <param name="productsSkuID">��ƷSKUID</param>
		/// <param name="warehouseCode">�ֿ���</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<ProductsSkuInventory> GetWarehouseSkuInventory(int productsSkuID, string warehouseCode, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetWarehouseSkuInventory(productsSkuID, warehouseCode, context);
		}

		#endregion

		#region ��ȡ�ɷ������

		/// <summary>
		/// ��ȡ�ɷ������
		/// </summary>
		/// <param name="productsSkuID">��ƷSKUID</param>
		/// <param name="isFilterStopSelling">�Ƿ�����¼ܿ�� 0���� 1����</param>
		/// <returns></returns>
		public static int GetKfhNumByProductsSkuID(int productsSkuID, int isFilterStopSelling = 0, IDbContext context = null) {
			int KfhNum = 0;
			ProductsSkuInventory skuInventory = GetProductsSkuInventory(productsSkuID, "", isFilterStopSelling, context);
			if (skuInventory != null) {
				KfhNum = skuInventory.KyNum - skuInventory.ZyNum - skuInventory.OrdZyNum + skuInventory.BookingKyNum;
			}
			return KfhNum;
		}

		/// <summary>
		/// ��ȡ�ɷ������
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsSkuID">��ƷSKUID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int GetKfhNumByProductsSkuID(string warehouseCode, int productsSkuID, IDbContext context = null) {
			int KfhNum = 0;
			ProductsSkuInventory skuInventory = GetProductsSkuInventory(productsSkuID, warehouseCode, 0, context);
			if (skuInventory != null) {
				KfhNum = skuInventory.KyNum - skuInventory.ZyNum + skuInventory.BookingKyNum;
			}
			return KfhNum;
		}

		#endregion

		#region ��ȡ��ƷSKU�����������ж�SKU�Ƿ��ɾ��
		/// <summary>
		/// ��ȡ��ƷSKU�����������ж�SKU�Ƿ��ɾ��
		/// </summary>
		/// <param name="productsSkuID">��ƷSKUID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetTotalNum(int productsSkuID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetTotalNum(productsSkuID, context);
		}

		#endregion

		#region ��Ʒsku ��Ϣ ��Ӷ���

		/// <summary>
		/// ��Ʒsku ��Ϣ ��Ӷ���
		/// </summary>
		/// <param name="ProductsID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<ProductsSkuList> GetProductsSkuList(int ProductsID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetProductsSkuList(ProductsID, context);
		}

		#endregion

		#region ��ȡ ʵ��

		/// <summary>
		/// ��ȡ ʵ��
		/// </summary>
		/// <param name="WarehouseCode">�ֿ����</param>
		/// <param name="skucode">sku ��</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static ProductsSku GetProductsSku(string WarehouseCode, string skucode, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetProductsSku(WarehouseCode, skucode, context);
		}

		#endregion

		#region ��ȡSKU�ɹ���

		/// <summary>
		/// ��ȡSKU�ɹ���
		/// </summary>
		/// <param name="productsSkuID">��ƷSKUID</param>
		/// <param name="context">���ݿ����</param>
		/// <returns></returns>
		public static decimal GetCostPrice(int productsSkuID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetCostPrice(productsSkuID, context);
		}

		#endregion
	}
}





