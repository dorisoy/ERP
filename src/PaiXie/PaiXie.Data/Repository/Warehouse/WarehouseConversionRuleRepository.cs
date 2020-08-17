using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class WarehouseConversionRuleRepository : BaseRepository<WarehouseConversionRule> {

		#region 构造函数
		private static WarehouseConversionRuleRepository _instance;
		public static WarehouseConversionRuleRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehouseConversionRuleRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(WarehouseConversionRule entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehouseConversionRule>("warehouseConversionRule", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region Update
		public int Update(WarehouseConversionRule entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehouseConversionRule>("warehouseConversionRule", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		/// <summary>
		/// 获取商品转换规则实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="ruleID">规则ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public WarehouseConversionRule GetSingleWarehouseConversionRule(string warehouseCode, int ruleID, IDbContext context = null) {
			string sqlStr = @"SELECT * FROM warehouseConversionRule WHERE WarehouseCode = @0 AND ID = @1";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = ruleID;
			return GetQuerySingle(sqlStr, context, objects);
		}

		/// <summary>
		/// 删除转换规则
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsIDList">规则ID列表</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public int Delete(string warehouseCode, List<int> ruleIDList, IDbContext context = null) {
			string sqlStr = @"DELETE FROM warehouseConversionRule WHERE WarehouseCode = @0 AND FIND_IN_SET(ID, @1)";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = string.Join(",", ruleIDList.ToArray());
			return Del(sqlStr, context, objects);
		}

		/// <summary>
		/// 获取商品转换规则实体列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SkuID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public List<WarehouseConversionRule> GetManyWarehouseConversionRule(string warehouseCode, int productsSkuID, IDbContext context = null) {
			string sqlStr = @"SELECT * FROM warehouseConversionRule wcr WHERE WarehouseCode = @0 AND EXISTS (SELECT 1 FROM warehouseConversionRuleItem wcri WHERE wcri.RuleID = wcr.ID AND ProductsSkuID = @1)";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = productsSkuID;
			return GetQueryMany(sqlStr, context, objects);
		}
	}
}





