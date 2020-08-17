using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
    public	class ShopComancationRepository:BaseRepository<ShopComancation> {

        #region 构造函数
     
	    private static ShopComancationRepository _instance;
	    public static ShopComancationRepository GetInstance() {
            if (_instance == null) {
                _instance = new ShopComancationRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(ShopComancation entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<ShopComancation>("shopComancation", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
	    public int Update(ShopComancation entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<ShopComancation>("shopComancation", entity)
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
		public virtual ShopComancation GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM shopComancation WHERE ID=@0";
			ShopComancation obj = GetQuerySingle(sqlStr, context, objects);
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
			int rowsAffected = context.Sql("DELETE  FROM  shopComancation   WHERE ID=@0",objects)
					.Execute();
			return rowsAffected;
		}

	 #endregion

		#region 公共库存比例
		/// <summary>
		/// 公共库存比例
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual string Ranges(int ShopID, int ProductsID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = ShopID;
			objects[1] = ProductsID;
			return Getobject("SELECT Ranges FROM shopComancation  WHERE  ShopID= @0 and ProductsID=@1", context, objects);
				
		}

		#endregion

		#region 公共库存备注
		/// <summary>
		/// 公共库存备注
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual string Remark(int ProductsID, IDbContext context = null) {
			Object[] objects = new Object[1];		
			objects[0] = ProductsID;
			return Getobject("SELECT  Remark FROM shopComancation where  ProductsID=@0  LIMIT 0,1 ", context, objects);

		}

		#endregion

		#region 删除  公共库存分配
		/// <summary>
		/// 删除  公共库存分配
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int Del(int ShopID, int ProductsID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = ShopID;
			objects[1] = ProductsID;
			return Del("delete from shopComancation where ShopID=@0 and ProductsID=@1", context, objects);

		}

		#endregion


		#region 获取单个实体
		/// <summary>
		/// 获取单个实体 
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual ShopComancation GetQuerySingle(int ShopID,int ProductsID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = ShopID;
			objects[1] = ProductsID;
			ShopComancation obj = GetQuerySingle("SELECT * FROM shopComancation  WHERE  ShopID= @0 and ProductsID=@1", context, objects);
			return obj;
		}


		public virtual ShopComancation GetQuerySingle(int ShopID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ShopID;
		
			ShopComancation obj = GetQuerySingle("SELECT * FROM shopComancation  WHERE  ShopID= @0 ", context, objects);
			return obj;
		}
		#endregion


		


		
		

     
	}
}





