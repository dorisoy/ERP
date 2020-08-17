using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class OrdouterItemRepository : BaseRepository<OrdouterItem> {

		#region 构造函数

		private static OrdouterItemRepository _instance;
		public static OrdouterItemRepository GetInstance() {
			if (_instance == null) {
				_instance = new OrdouterItemRepository();
			}
			return _instance;
		}

		#endregion

		#region Add

		public int Add(OrdouterItem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<OrdouterItem>("ord_outerItem", entity)
					.AutoMap(x => x.ID)
					.ExecuteReturnLastId<int>();
			return Id;
		}

		#endregion

		#region Update

		public int Update(OrdouterItem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<OrdouterItem>("ord_outerItem", entity)
					.AutoMap(x => x.ID)
					.Where(x => x.ID)
					.Execute();
			return rowsAffected;
		}

		#endregion

		#region 获取单个实体 通过主键ID

		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual OrdouterItem GetQuerySingleByID(int id, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM ord_outerItem WHERE ID=@0";
			OrdouterItem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 删除操作  通过ID

		/// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DelByID(int id, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "DELETE FROM ord_outerItem WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 获取实体列表

		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="ordouterID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual List<OrdouterItem> GetManyOrdouterItem(int ordouterID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ordouterID;
			string sqlStr = "SELECT * FROM ord_outerItem WHERE OrdouterID = @0";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 获取订单商品是否添加完成

		/// <summary>
		/// 获取订单商品是否添加完成
		/// </summary>
		/// <param name="ordouterID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int getIsProductAddFin(int ordouterID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ordouterID;
			string sqlStr = "SELECT Count(1) FROM ord_outerItem WHERE OrdouterID = @0 AND IsProductAddFin = 0";
			return GetCount(sqlStr, context, objects) > 0 ? 0 : 1;
		}

		#endregion

		#region 获取订单商品是否退款

		/// <summary>
		/// 获取订单商品是否退款
		/// </summary>
		/// <param name="ordouterID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int getIsRefund(int ordouterID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ordouterID;
			string sqlStr = "SELECT SUM(IsRefund) FROM ord_outerItem WHERE OrdouterID = @0 AND IsProductAddFin <> 2";
			return GetCount(sqlStr, context, objects);
		}

		#endregion
	}
}





