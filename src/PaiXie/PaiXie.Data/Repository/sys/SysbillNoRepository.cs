using FluentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data {
	public class SysbillnoRepository : BaseRepository<Sysbillno> {

		#region 构造函数
		private static SysbillnoRepository _instance;
		public static SysbillnoRepository GetInstance() {
			if (_instance == null) {
				_instance = new SysbillnoRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(Sysbillno entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int id = context.Insert<Sysbillno>("sys_billno", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
			return id;
		}
		#endregion

		#region Update
		public int Update(Sysbillno entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<Sysbillno>("sys_billno", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion
	}
}





