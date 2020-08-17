using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class ShopTaskService  : BaseService<ShopTask> {
    
		public static int Add(ShopTask entity, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// 更新任务状态
		/// </summary>
		/// <param name="taskID">任务标识</param>
		/// <param name="taskStatus">任务状态 枚举值</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateStatus(string taskID, int taskStatus, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().UpdateStatus(taskID, taskStatus, context);
		}

		#region 更新完成数量(指定数量)

		/// <summary>
		/// 更新完成数量(指定数量)
		/// </summary>
		/// <param name="taskID"></param>
		/// <param name="finshCount"></param>
		/// <param name="taskStatus"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int UpdateFinshCount(string taskID, int finshCount, int taskStatus, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().UpdateFinshCount(taskID, finshCount, taskStatus, context);
		}

		#endregion

		/// <summary>
		/// 更新完成数量(自动加1)
		/// </summary>
		/// <param name="taskID">任务标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateFinshCount(string taskID, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().UpdateFinshCount(taskID, context);
		}

		/// <summary>
		/// 更新任务总条数
		/// </summary>
		/// <param name="taskID">任务标识</param>
		/// <param name="TotalCount">任务总条数</param>
		/// <param name="requestMessage">请求报文</param>
		/// <param name="responseMessage">响应报文</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateTotalCount(string taskID, int totalCount, string requestMessage, string responseMessage, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().UpdateTotalCount(taskID, totalCount, requestMessage, responseMessage, context);
		}

		/// <summary>
		/// 根据任务ID获取任务信息
		/// </summary>
		/// <param name="taskID">任务ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ShopTask GetSingleShopTask(string taskID, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().GetSingleShopTask(taskID, context);
		}

		/// <summary>
		/// 根据店铺ID、任务类型、获取最后一个任务信息
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="taskType">任务类型 枚举</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ShopTask GetSingleShopTask(int shopID, int taskType, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().GetSingleShopTask(shopID, taskType, context);
		}
	}
}





