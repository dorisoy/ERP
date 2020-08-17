using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class WarehouseExpressPriceRepository:BaseRepository<WarehouseExpressPrice> {

        #region 构造函数
     
	    private static WarehouseExpressPriceRepository _instance;
	    public static WarehouseExpressPriceRepository GetInstance() {
            if (_instance == null) {
                _instance = new WarehouseExpressPriceRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(WarehouseExpressPrice entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<WarehouseExpressPrice>("warehouseExpressPrice", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(WarehouseExpressPrice entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<WarehouseExpressPrice>("warehouseExpressPrice", entity)
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
		public virtual WarehouseExpressPrice GetQuerySingleByID(int id, IDbContext context = null) {
				if (context == null) context = Db.GetInstance().Context();
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseExpressPrice WHERE ID=@0";
			WarehouseExpressPrice obj = GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM warehouseExpressPrice WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 根据快递公司ID获取多个实体

		/// <summary>
		/// 根据快递公司ID获取多个实体
		/// </summary>
		/// <param name="expressID">快递公司ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<WarehouseExpressPrice> GetQueryManyByExpressID(int expressID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = expressID;
			string sqlStr = @"SELECT * FROM warehouseExpressPrice WHERE ExpressID=@0";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 检查地区是否已经存在

		/// <summary>
		/// 检查地区是否已经存在
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="expressID">快递公司ID</param>
		/// <param name="sysAreaName">地区名称</param>
		/// <param name="id">运费记录表主键ID 添加时传0，修改才传值</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public bool IsExists(string warehouseCode, int expressID, string sysAreaName, int id, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = warehouseCode;
			objects[1] = expressID;
			objects[2] = sysAreaName;
			string strWhere = string.Empty;
			if (id > 0) {
				objects[3] = id;
				strWhere = " AND ID<>@3";
			}
			string sqlStr = @"SELECT COUNT(*) FROM warehouseExpressPrice WHERE WarehouseCode=@0 AND ExpressID=@1 AND FIND_IN_SET(@2, SysAreaNames)" + strWhere;
			return GetCount(sqlStr, context, objects) > 0;
		}

		#endregion

		#region 根据快递公司ID和市级地区ID获取单个实体

		/// <summary>
		/// 根据快递公司ID和市级地区ID获取单个实体
		/// </summary>
		/// <param name="expressID">快递公司ID</param>
		/// <param name="cityID">市级地区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual WarehouseExpressPrice GetQuerySingleByExpressIDAndCityID(int expressID, int cityID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = expressID;
			objects[1] = cityID;
			string sqlStr = @"SELECT * FROM warehouseExpressPrice WHERE ExpressID=@0 AND FIND_IN_SET(@1,SysAreaIDs)";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 根据快递公司ID和省级地区ID获取单个实体

		/// <summary>
		/// 根据快递公司ID和省级地区ID获取单个实体
		/// </summary>
		/// <param name="expressID">快递公司ID</param>
		/// <param name="provinceID">省级地区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual WarehouseExpressPrice GetQuerySingleByExpressIDAndProvinceID(int expressID, int provinceID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = expressID;
			objects[1] = provinceID;
			string sqlStr = @"SELECT * FROM warehouseExpressPrice WHERE ExpressID=@0 AND FIND_IN_SET(@1,SysAreaIDs)";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 根据快递公司ID和全国获取单个实体

		/// <summary>
		/// 根据快递公司ID和全国获取单个实体
		/// </summary>
		/// <param name="expressID">快递公司ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual WarehouseExpressPrice GetQuerySingleByExpressID(int expressID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = expressID;
			objects[1] = "全国";
			string sqlStr = @"SELECT * FROM warehouseExpressPrice WHERE ExpressID=@0 AND SysAreaNames=@1";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion
	}
}





