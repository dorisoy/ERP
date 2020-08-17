using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class ShopRepository : BaseRepository<Shop> {

		#region 构造函数
		private static ShopRepository _instance;
		public static ShopRepository GetInstance() {
			if (_instance == null) {
				_instance = new ShopRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(Shop entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<Shop>("shop", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region Update
		public int Update(Shop entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<Shop>("shop", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		#region 根据店铺ID获取店铺信息
		/// <summary>
		/// 根据店铺ID获取店铺信息
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public Shop GetSingleShop(int shopID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = shopID;
			string sqlStr = @"SELECT * FROM shop WHERE ID = @0";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

	    #region 禁用店铺	
		public int  DeleteShop(string  id) {
			int result = 1;
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					string str = id;
					string[] sArray = str.Split(',');
					#region 循环操作
					foreach (string i in sArray) {
						Object[] objects = new Object[1];
						objects[0] = i;
						string temp = Getobject("SELECT IsEnable FROM shop   WHERE  ID=@0", context, objects);
						if (temp == "1")
							result = Del("update   shop  set IsEnable=0  WHERE ID=@0", context, objects);
						else
							result = Del("update   shop  set IsEnable=1  WHERE ID=@0", context, objects);

						if (result == 0) {
							break;
						}
					} 
					#endregion
				
					if (result  == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				result = 0;
			}
			return result;

		}

		#endregion

		#region 检查代码唯一性
		/// <summary>
		/// 检查代码 排除自己
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public int CheckCode(string Code, int ID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = Code;
			objects[1] = ID;
			int result = GetCount("select count(0)  from shop where ID!=@1 and Code=@0", context, objects);
			return result;	

		}

		/// <summary>
		/// 检查代码
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public int CheckCode2(string Code ,IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = Code;
		
			int result = GetCount("select count(0)  from shop where  Code=@0",context,objects);
			return result;	

		}
		#endregion

		#region 检查名称唯一性
		/// <summary>
		/// 检查名称 排除自己
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public int CheckName(string Name, int ID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = Name;
			objects[1] = ID;
			int result = GetCount("select count(0)  from shop where ID!=@1 and Name=@0", context, objects);
			return result;

		}

		/// <summary>
		/// 检查名称
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public int CheckName2(string Name, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] =Name;
			int result = GetCount("select count(0)  from shop where  Name=@0", context, objects);
			return result;
		}
		#endregion

		#region 线上平台店铺列表
		public List<PaiXie.Data.Shop> shoplist(IDbContext context = null) {
			List<PaiXie.Data.Shop> shoplist = GetQueryMany("SELECT *  FROM shop  WHERE  AppKey  IS  NOT NULL AND  AppKey !=''", context);
			return shoplist;
		}
		#endregion
	}
}