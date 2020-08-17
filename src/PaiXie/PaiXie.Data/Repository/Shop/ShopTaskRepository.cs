using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
namespace PaiXie.Data 
{
	public class ShopTaskRepository : BaseRepository<ShopTask> {

		#region 构造函数
		private static ShopTaskRepository _instance;
		public static ShopTaskRepository GetInstance() {
			if (_instance == null) {
				_instance = new ShopTaskRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(ShopTask entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<ShopTask>("shopTask", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region 更新任务状态

		/// <summary>
		/// 更新任务状态
		/// </summary>
		/// <param name="taskID">任务标识</param>
		/// <param name="taskStatus">任务状态 枚举值</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateStatus(string taskID, int taskStatus, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = taskID;
			objects[1] = taskStatus;
			objects[2] = DateTime.Now;
			string sqlStr = "Update shopTask set TaskStatus=@1,UpdateDate=@2 Where TaskID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新任务总条数

		/// <summary>
		/// 更新任务总条数
		/// </summary>
		/// <param name="taskID">任务标识</param>
		/// <param name="totalCount">任务总条数</param>
		/// <param name="requestMessage">请求报文</param>
		/// <param name="responseMessage">响应报文</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateTotalCount(string taskID, int totalCount, string requestMessage, string responseMessage, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = taskID;
			objects[1] = totalCount;
			objects[2] = DateTime.Now;
			objects[3] = requestMessage;
			objects[4] = responseMessage;
			string sqlStr = @"Update shopTask set TotalCount=@1,UpdateDate=@2,RequestMessage=@3,ResponseMessage=@4 Where TaskID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新完成数量(指定数量)

		/// <summary>
		/// 更新完成数量(指定数量)
		/// </summary>
		/// <param name="taskID"></param>
		/// <param name="finshCount"></param>
		/// <param name="taskStatus"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public int UpdateFinshCount(string taskID, int finshCount, int taskStatus, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = taskID;
			objects[1] = finshCount;
			objects[2] = DateTime.Now;
			objects[3] = taskStatus;
			string sqlStr = @"Update shopTask set FinshCount=@1,UpdateDate=@2,TaskStatus=@3 Where TaskID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新完成数量(自动加1)

		/// <summary>
		/// 更新完成数量(自动加1)
		/// </summary>
		/// <param name="taskID">任务标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateFinshCount(string taskID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = taskID;
			objects[1] = DateTime.Now;
			string sqlStr = @"Update shopTask set FinshCount=FinshCount+1,UpdateDate=@1 Where TaskID=@0";
			int count = Update(sqlStr, context, objects);
			if (count > 0) {
				//检查任务是否完成或超过5分钟未更新
				ShopTask shopTask = GetSingleShopTask(taskID, context);
				if (shopTask.FinshCount >= shopTask.TotalCount || shopTask.UpdateDate < DateTime.Now.AddMinutes(-5)) {
					UpdateStatus(taskID, (int)ShopTaskStatus.已结束, context);
				}
			}
			return count;
		}

		#endregion

		#region 根据任务ID获取任务信息

		/// <summary>
		/// 根据任务ID获取任务信息
		/// </summary>
		/// <param name="taskID">任务ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public ShopTask GetSingleShopTask(string taskID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = taskID;
			string sqlStr = @"SELECT * FROM shopTask WHERE TaskID = @0";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 根据店铺ID、任务类型、获取最后一个任务信息

		/// <summary>
		/// 根据店铺ID、任务类型、获取最后一个任务信息
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="taskType">任务类型 枚举</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public ShopTask GetSingleShopTask(int shopID, int taskType, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = shopID;
			objects[1] = taskType;
			string sqlStr = @"SELECT * FROM shopTask WHERE ShopID = @0 and TaskType = @1 ORDER BY ID DESC LIMIT 0,1";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion
	}
}





