using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class OrdbaseService  : BaseService<Ordbase> {
    
        #region Update
        
		public static int Update(Ordbase entity, IDbContext context = null) {
			return OrdbaseRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(Ordbase entity, IDbContext context = null) {
			return OrdbaseRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static Ordbase GetQuerySingleByID(int id, IDbContext context = null) {
		    return OrdbaseRepository.GetInstance().GetQuerySingleByID(id, context);
	    }
    
	    #endregion

		#region 获取单个实体 通过系统订单号

		/// <summary>
	    /// 获取单个实体 通过系统订单号
	    /// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
		public static Ordbase GetQuerySingleByErpOrderCode(string erpOrderCode, IDbContext context = null) {
			return OrdbaseRepository.GetInstance().GetQuerySingleByErpOrderCode(erpOrderCode, context);
	    }
    
	    #endregion

		#region 将订单由一状态改为另外一状态 (发货时间和物流信息可不传)

		/// <summary>
		/// 将订单由一状态改为另外一状态 (发货时间和物流信息可不传)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="oldOrderStatus">旧状态</param>
		/// <param name="newOrderStatus">新状态</param>
		/// <param name="context">数据库连接对象</param>
		/// <param name="deliveryDate">发货时间</param>
		/// <param name="deliveryExpressID">发货快递公司ID</param>
		/// <param name="waybillNo">运单号</param>
		/// <returns></returns>
		public static int UpdateOrderStatus(string userCode, string erpOrderCode, int oldOrderStatus, int newOrderStatus, IDbContext context = null, DateTime? deliveryDate = null, int deliveryExpressID = 0, string waybillNo = "") {
			return OrdbaseRepository.GetInstance().UpdateOrderStatus(userCode, erpOrderCode, oldOrderStatus, newOrderStatus, context, deliveryDate, deliveryExpressID, waybillNo);
		}

		#endregion

		#region 删除操作  通过ID

		/// <summary>
    	/// 删除操作  通过ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库对象</param>
	    /// <returns></returns>
	    public static int DelByID(int id, IDbContext context = null) {
		    return OrdbaseRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 获取未审核订单数量

		/// <summary>
		/// 获取未审核订单数量
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int getUncommittedCount(IDbContext context = null) {
			return OrdbaseRepository.GetInstance().getUncommittedCount(context);
		}

		#endregion

		#region

		/// <summary>
		/// 更新物流ID
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="logisticsID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int UpdateLogisticsID(string erpOrderCode, int logisticsID, IDbContext context = null) {
			return OrdbaseRepository.GetInstance().UpdateLogisticsID(erpOrderCode, logisticsID, context);
		}

		#endregion

		#region 订单是否生成完成

		/// <summary>
		/// 订单是否生成完成
		/// </summary>
		/// <param name="ordbaseID"></param>
		/// <param name="context"></param>
		/// <returns>1：是 0：否</returns>
		public static int IsGenerateComplete(int ordbaseID, IDbContext context = null) {
			return OrdbaseRepository.GetInstance().IsGenerateComplete(ordbaseID, context);
		}

		#endregion

		#region 更新订单主表商品金额和数量

		/// <summary>
		/// 更新订单主表商品金额和数量
		/// </summary>
		/// <param name="erpOrderCode">订单编号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateOrdbaseAmount(string erpOrderCode, IDbContext context = null) {
			return OrdbaseRepository.GetInstance().UpdateOrdbaseAmount(erpOrderCode, context);
		}

		/// <summary>
		/// 更新订单主表实收金额
		/// </summary>
		/// <param name="erpOrderCode">订单编号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateRealAmount(string erpOrderCode, IDbContext context = null) {
			return OrdbaseRepository.GetInstance().UpdateRealAmount(erpOrderCode, context);
		}

		#endregion

		#region 获取符合整单原则的仓库列表

		/// <summary>
		/// 获取符合整单原则的仓库列表
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static DataTable GetMatchingWarehouse(string erpOrderCode, IDbContext context = null) {
			return OrdbaseRepository.GetInstance().GetMatchingWarehouse(erpOrderCode, context);
		}

		#endregion

		#region 修改订单为已驳回，并记录驳回备注

		/// <summary>
		/// 修改订单为已驳回，并记录驳回备注
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="rejectRemark">驳回备注</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateRejectStatus(string erpOrderCode, string rejectRemark, IDbContext context = null) {
			return OrdbaseRepository.GetInstance().UpdateRejectStatus(erpOrderCode, rejectRemark, context);
		}

		#endregion
	}
}





