using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Utils;
using PaiXie.Core;
namespace PaiXie.Data 
{
	public class WarehouseMoveLocationItemRepository : BaseRepository<WarehouseMoveLocationItem> {

        #region 构造函数
     
	    private static WarehouseMoveLocationItemRepository _instance;
	    public static WarehouseMoveLocationItemRepository GetInstance() {
            if (_instance == null) {
                _instance = new WarehouseMoveLocationItemRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(WarehouseMoveLocationItem entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<WarehouseMoveLocationItem>("warehouseMoveLocationItem", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(WarehouseMoveLocationItem entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<WarehouseMoveLocationItem>("warehouseMoveLocationItem", entity)
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
		public virtual WarehouseMoveLocationItem GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseMoveLocationItem WHERE ID=@0";
			WarehouseMoveLocationItem obj = GetQuerySingle(sqlStr, context, objects);
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
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "DELETE FROM warehouseMoveLocationItem WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 根据移位单ID删除所有商品

		/// <summary>
		/// 根据移位单ID删除所有商品
		/// </summary>
		/// <param name="moveLocationID">移位单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DeleteByMoveLocationID(int moveLocationID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = moveLocationID;
			string sqlStr = "DELETE FROM warehouseMoveLocationItem WHERE MoveLocationID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据移位单ID获取所有商品

		/// <summary>
		/// 根据移位单ID获取所有商品
		/// </summary>
		/// <param name="moveLocationID">移位单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehouseMoveLocationItem> GetWarehouseMoveLocationItemList(int moveLocationID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = moveLocationID;
			string sqlStr = "SELECT * FROM warehouseMoveLocationItem WHERE MoveLocationID=@0";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据移位单商品表主键ID删除商品

		/// <summary>
		/// 根据移位单商品表主键ID删除商品
		/// </summary>
		/// <param name="moveLocationItemIDList">移位单商品表主键ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int Delete(List<int> moveLocationItemIDList, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = string.Join(",", moveLocationItemIDList.ToArray());
			objects[1] = (int)MoveLocationStatus.未确认;
			string sqlStr = @"DELETE FROM warehouseMoveLocationItem WHERE FIND_IN_SET(ID,@0) AND Status=@1";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据移位单商品表主键ID获取商品

		/// <summary>
		/// 根据移位单商品表主键ID获取商品
		/// </summary>
		/// <param name="moveLocationItemIDList">移位单商品表主键ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehouseMoveLocationItem> GetWarehouseMoveLocationItemList(List<int> moveLocationItemIDList, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = string.Join(",", moveLocationItemIDList.ToArray());
			string sqlStr = @"SELECT * FROM warehouseMoveLocationItem WHERE FIND_IN_SET(ID,@0)";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据移位单主键ID获取商品总数

		/// <summary>
		/// 根据移位单主键ID获取商品总数
		/// </summary>
		/// <param name="moveLocationID">移位单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int GetNumByMoveLocationID(int moveLocationID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = moveLocationID;
			string sqlStr = "SELECT SUM(Num) FROM warehouseMoveLocationItem WHERE MoveLocationID=@0";
			string Num = Getobject(sqlStr, context, objects);
			return ZConvert.StrToInt(Num);
		}

		#endregion

		#region 获取单个实体

		/// <summary>
		/// 获取单个实体
		/// </summary>
		/// <param name="moveLocationID">移位表主键ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="outLocationID">移出库位ID</param>
		/// <param name="inLocationID">移入库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="context"></param>
		public WarehouseMoveLocationItem GetSingleWarehouseMoveLocationItem(int moveLocationID, int productsSkuID, int outLocationID, int inLocationID, int productsBatchID, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = moveLocationID;
			objects[1] = productsSkuID;
			objects[2] = outLocationID;
			objects[3] = inLocationID;
			objects[4] = productsBatchID;
			string sqlStr = @"SELECT * FROM warehouseMoveLocationItem WHERE MoveLocationID=@0 AND ProductsSkuID=@1 AND OutLocationID=@2 AND InLocationID=@3 AND ProductsBatchID=@4";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 修改移入库位编码和移位数量

		/// <summary>
		/// 修改移入库位编码和移位数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="moveLocationItemID">移位单明细主键ID</param>
		/// <param name="inLocationID">移入库位ID</param>
		/// <param name="diffNum">要更新数量 差量更新可正可负</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateMoveLocationItem(string userCode, int moveLocationItemID, int inLocationID, int diffNum, IDbContext context) {
			Object[] objects = new Object[6];
			objects[0] = moveLocationItemID;
			objects[1] = inLocationID;
			objects[2] = diffNum;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			objects[5] = (int)MoveLocationStatus.未确认;
			string sqlStr = @"UPDATE warehouseMoveLocationItem SET InLocationID=@1,Num=Num+(@2),UpdatePerson=@3,UpdateDate=@4 WHERE ID=@0 AND Status=@5 AND Num+(@2)>0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新移位明细状态

		/// <summary>
		/// 更新移位明细状态
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="moveLocationItemID">移位明细主键ID</param>
		/// <param name="oldStatus">旧状态</param>
		/// <param name="newStatus">新状态</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateStatus(string userCode, int moveLocationItemID, int oldStatus, int newStatus, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = moveLocationItemID;
			objects[1] = oldStatus;
			objects[2] = newStatus;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseMoveLocationItem SET Status=@2,UpdatePerson=@3,UpdateDate=@4,ConfirmDate=@4 WHERE ID=@0 AND Status=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion
	}
}





