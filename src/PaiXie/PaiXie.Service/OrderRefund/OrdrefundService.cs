using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class OrdrefundService  : BaseService<Ordrefund> {
    
        #region Update
        
		public static int Update(Ordrefund entity, IDbContext context = null) {
			return OrdrefundRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(Ordrefund entity, IDbContext context = null) {
			return OrdrefundRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static Ordrefund GetQuerySingleByID(int id, IDbContext context = null) {
		    return OrdrefundRepository.GetInstance().GetQuerySingleByID(id, context);
	    }
    
	    #endregion

		#region 获取单个实体 通过售后单号

		/// <summary>
		/// 获取单个实体 通过售后单号
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="billNo">售后单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static Ordrefund GetQuerySingleByBillNo(string warehouseCode, string billNo, IDbContext context = null) {
			return OrdrefundRepository.GetInstance().GetQuerySingleByBillNo(warehouseCode, billNo, context);
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
		    return OrdrefundRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 更新售后寄回物流信息

		/// <summary>
		/// 更新售后寄回物流信息
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="ordRefundID">售后表主键ID</param>
		/// <param name="expressCompany">物流公司</param>
		/// <param name="waybillNo">运单号</param>
		/// <param name="returnFreight">寄回运费</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Updatelogistics(string userCode, int ordRefundID, string expressCompany, string waybillNo, decimal returnFreight, IDbContext context = null) {
			return OrdrefundRepository.GetInstance().Updatelogistics(userCode, ordRefundID, expressCompany, waybillNo, returnFreight, context);
		}

		#endregion

		#region 收货正常

		/// <summary>
		/// 收货正常
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="ordRefundID">售后单主键ID</param>
		/// <param name="receiveRemark">收货备注</param>
		/// <param name="duty">责任方</param>
		/// <param name="DutyOther">其他责任方</param>
		/// <param name="RefundAmount">退金额</param>
		/// <param name="RefundFreight">退运费</param>
		/// <param name="ReturnFreight">寄回运费</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int ReceiveNormal(string userCode, int ordRefundID, string receiveRemark, int duty, string DutyOther, decimal RefundAmount, decimal RefundFreight, decimal ReturnFreight, IDbContext context = null) {
			return OrdrefundRepository.GetInstance().ReceiveNormal(userCode, ordRefundID, receiveRemark, duty, DutyOther, RefundAmount, RefundFreight, ReturnFreight, context);
		}

		#endregion

		#region 更新为收货异常

		/// <summary>
		/// 更新为收货异常
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="ordRefundID">售后单主键ID</param>
		/// <param name="receiveRemark">收货备注</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int ReceiveException(string userCode, int ordRefundID, string receiveRemark, IDbContext context = null) {
			return OrdrefundRepository.GetInstance().ReceiveException(userCode, ordRefundID, receiveRemark, context);
		}

		#endregion

		#region 取消售后

		/// <summary>
		/// 取消售后
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="billNo">售后单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Cancel(string userCode, string warehouseCode, string billNo, IDbContext context = null) {
			return OrdrefundRepository.GetInstance().Cancel(userCode, warehouseCode, billNo, context);
		}

		#endregion

		#region 根据订单号获取出库单记录

		/// <summary>
		/// 根据订单号获取出库单记录
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<OrdRefundList> GetManyOrdrefund(string erpOrderCode, IDbContext context = null) {
			return OrdrefundRepository.GetInstance().GetManyOrdrefund(erpOrderCode, context);
		}

		#endregion
	}
}





