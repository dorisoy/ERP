using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class ShopStockUpdateRepository:BaseRepository<ShopStockUpdate> {

        #region 构造函数
     
	    private static ShopStockUpdateRepository _instance;
	    public static ShopStockUpdateRepository GetInstance() {
            if (_instance == null) {
                _instance = new ShopStockUpdateRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(ShopStockUpdate entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<ShopStockUpdate>("shopStockUpdate", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
	    public int Update(ShopStockUpdate entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<ShopStockUpdate>("shopStockUpdate", entity)
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
		public virtual ShopStockUpdate GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM shopStockUpdate WHERE ID=@0";
			ShopStockUpdate obj = GetQuerySingle(sqlStr, context, objects);
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
			int rowsAffected = context.Sql("DELETE  FROM  shopStockUpdate   WHERE ID=@0",objects)
					.Execute();
			return rowsAffected;
		}
	 #endregion

		#region 店铺库存更新状态
		/// <summary>
		/// 店铺库存更新状态
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual List<ShopStockUpdate> ShopStockUpdatelist(int shopid,int PlatformType,  IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = shopid;
			objects[1] = PlatformType;
			return  GetQueryMany("SELECT *  FROM  shopStockUpdate WHERE  shopid=@0 AND  PlatformType=@1",context,objects);		
		}
		#endregion

		#region 店铺库存更新状态  完成数
		/// <summary>
		/// 店铺库存更新状态 完成数
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual string shopStockUpdatecount(int shopid, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = shopid;			
			return  Getobject("SELECT  COUNT(0) FROM  shopStockUpdate WHERE shopid=@0",context,objects );			
		}
		#endregion	

			#region 店铺库存更新状态  最后更新时间
	     /// <summary>
		/// 店铺库存更新状态  最后更新时间
	     /// </summary>
	     /// <param name="shopid"></param>
	     /// <param name="context"></param>
	     /// <returns></returns>
		public virtual object LastTimeshopStockUpdate(int shopid, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = shopid;			
			return  Getobject("	SELECT  UpdateTime  FROM shopstockupdate WHERE  shopid=@0  ORDER BY  UpdateTime DESC  LIMIT 0 , 1 ",context,objects );			
		}
		#endregion	
	    
 

	
	}
}