using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class WarehouseConversionRuleItemRepository : BaseRepository<WarehouseConversionRuleItem> {

		#region 构造函数
		private static WarehouseConversionRuleItemRepository _instance;
		public static WarehouseConversionRuleItemRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehouseConversionRuleItemRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(WarehouseConversionRuleItem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehouseConversionRuleItem>("warehouseConversionRuleItem", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region Update
		public int Update(WarehouseConversionRuleItem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehouseConversionRuleItem>("warehouseConversionRuleItem", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		/// <summary>
		/// 获取转换规则商品实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="ruleID">规则ID</param>
		/// <param name="productsSkuCode">商品SKU码</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public WarehouseConversionRuleItem GetSingleWarehouseConversionRuleItem(string warehouseCode, int ruleID, string productsSkuCode, IDbContext context = null) {
			string sqlStr = @"SELECT * FROM warehouseConversionRuleItem WHERE WarehouseCode = @0 RuleID = @1 AND ProductsSkuCode = @2";
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = ruleID;
			objects[2] = productsSkuCode;
			return GetQuerySingle(sqlStr, context, objects);
		}

		/// <summary>
		/// 获取转换规则商品列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="ruleID">规则ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public List<WarehouseConversionRuleItem> GetManyWarehouseConversionRuleItem(string warehouseCode, int ruleID, IDbContext context = null) {
			string sqlStr = @"SELECT * FROM warehouseConversionRuleItem WHERE WarehouseCode = @0 AND RuleID = @1";
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = ruleID;
			return GetQueryMany(sqlStr, context, objects);
		}

		/// <summary>
		/// 获取转换规则商品列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="ruleID">规则ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public List<WarehouseConversionRuleItemInfo> GetManyWarehouseConversionRuleItemInfo(string warehouseCode, int ruleID, IDbContext context = null) {
			string sqlStr = @"SELECT ps.ID AS ProductsSkuID,ps.Code AS ProductsSkuCode,wcri.Num,wcri.ConversionWay,
                              (SELECT IFNULL(SUM(KyNum),0) FROM warehouseLocationProducts WHERE WarehouseCode = @0 AND LocationTypeID = @2 AND ProductsSkuID = ps.ID) AS KyNum 
                              FROM warehouseConversionRuleItem wcri INNER JOIN productsSku ps ON wcri.ProductsSkuID = ps.ID WHERE wcri.WarehouseCode = @0 AND wcri.RuleID = @1";
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = ruleID;
			objects[2] = (int)PaiXie.Core.LocationType.中转区;
			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QueryMany<WarehouseConversionRuleItemInfo>();
		}

		/// <summary>
		/// 根据规则商品表ID删除记录
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param> 
		/// <param name="ruleItemID">规则商品表ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Delete(string warehouseCode,int ruleItemID, IDbContext context = null) {
			string sqlStr = "DELETE FROM warehouseConversionRuleItem Where WarehouseCode = @0 AND ID = @1";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = ruleItemID;
			return Del(sqlStr, context, objects);
		}

		/// <summary>
		/// 根据规则ID删除记录
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="ruleItemID">规则ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Delete(string warehouseCode, List<int> ruleIDList, IDbContext context = null) {
			string sqlStr = "DELETE FROM warehouseConversionRuleItem Where WarehouseCode = @0 AND FIND_IN_SET(RuleID, @1)";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = string.Join(",", ruleIDList.ToArray());
			return Del(sqlStr, context, objects);
		}
	}
}





