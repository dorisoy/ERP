using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using System.Data;
namespace PaiXie.Data 
{
    public	class ShopAllocationRepository:BaseRepository<ShopAllocation> {

        #region 构造函数
     
	    private static ShopAllocationRepository _instance;
	    public static ShopAllocationRepository GetInstance() {
            if (_instance == null) {
                _instance = new ShopAllocationRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(ShopAllocation entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<ShopAllocation>("shopAllocation", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
	    public int Update(ShopAllocation entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<ShopAllocation>("shopAllocation", entity)
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
		public virtual ShopAllocation GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM shopAllocation WHERE ID=@0";
			ShopAllocation obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}
        
		#endregion
        
        #region 删除操作  通过ID
        /// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int DelByID(int ID, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = ID;
			int rowsAffected = context.Sql("DELETE  FROM  shopAllocation   WHERE ID=@0", objects)
					.Execute();
			return rowsAffected;
		}

	 #endregion

		#region 私有库存列表
		/// <summary>
		/// 私有库存列表
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual DataTable GetshopAllocationDataTable(int ProductsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ProductsID;
			DataTable dt = GetDataTable("SELECT  ShopID,IsSalePub,SUM(SaleInventory) AS xsnum FROM  shopAllocation    WHERE  ProductsID=@0 GROUP BY ShopID", context, objects);
			return dt;
		}

		#endregion

		#region 删除  独享  根据店铺  商品
		/// <summary>
		/// 删除  独享  根据店铺  商品
	/// </summary>
	/// <param name="shopid"></param>
	/// <param name="ProductsID"></param>
	/// <param name="context"></param>
	/// <returns></returns>
		public virtual int Del(string shopid, string ProductsID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = shopid;
			objects[1] = ProductsID;
			return Del("DELETE   FROM  shopAllocation WHERE  ProductsID=@1 AND   ShopID=@0",context, objects);

	
		}

		#endregion

		#region 独享 数量
		/// <summary>
		/// 独享 数量
		/// </summary>
		/// <param name="shopid"></param>
		/// <param name="ProductsID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual string SaleInventory(int ProductsSkuID, int shopid, int ProductsID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = ProductsSkuID;
			objects[1] = shopid;
			objects[2] = ProductsID;
			return Getobject("SELECT SaleInventory FROM shopAllocation WHERE  ProductsSkuID=@0  AND  ProductsID=@2  AND   ShopID=@1",context,objects);
			

		}

		#endregion

		#region 是否  独享
		/// <summary>
		/// 是否  独享
		/// </summary>
		/// <param name="shopid"></param>
		/// <param name="ProductsID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual string IsSalePub(int shopid, int ProductsID, IDbContext context = null) {
			Object[] objects = new Object[2];			
			objects[0] = shopid;
			objects[1] = ProductsID;
			return Getobject("SELECT IsSalePub FROM shopAllocation WHERE    ProductsID=@1  AND   ShopID=@0", context, objects);
		}

		#endregion

		#region 获取单个实体 通过 SKU  SHOPID
	/// <summary>
		/// 获取单个实体 通过 SKU  SHOPID
	/// </summary>
	/// <param name="ShopID"></param>
	/// <param name="ProductsSkuID"></param>
	/// <param name="context"></param>
	/// <returns></returns>
		public virtual ShopAllocation GetQuerySingle(int ShopID,int ProductsSkuID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = ShopID;
			objects[1] = ProductsSkuID;
			string sqlStr = "SELECT *  FROM shopallocation  WHERE ShopID=@0 AND ProductsSkuID=@1";
			ShopAllocation obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 获取单个实体 通过 ProductsID
		/// <summary>
		/// 获取单个实体 通过 ProductsID
		/// </summary>
		/// <param name="ProductsID">ProductsID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual List<ShopAllocation> GetQuerySingleByProductsID(int ProductsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ProductsID;
			string sqlStr = "SELECT *  FROM shopallocation  WHERE ProductsID=@0";
			 List<ShopAllocation>  obj = GetQueryMany(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 获取单个实体 通过 ProductsSkuID
		/// <summary>
		/// 获取单个实体 通过 ProductsSkuID
		/// </summary>
		/// <param name="ProductsSkuID">ProductsSkuID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual List<ShopAllocation> GetQuerySingleByProductsSkuID(int ProductsSkuID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ProductsSkuID;
			string sqlStr = "SELECT *  FROM shopallocation  WHERE ProductsSkuID=@0";
			 List<ShopAllocation>  obj = GetQueryMany(sqlStr, context, objects);
			return obj;
		}

		#endregion
		
		
	}
}