using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
namespace PaiXie.Data 
{
    public	class OrdrefundRepository:BaseRepository<Ordrefund> {

        #region 构造函数
     
	    private static OrdrefundRepository _instance;
	    public static OrdrefundRepository GetInstance() {
            if (_instance == null) {
                _instance = new OrdrefundRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(Ordrefund entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<Ordrefund>("ord_refund", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(Ordrefund entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<Ordrefund>("ord_refund", entity)
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
		public virtual Ordrefund GetQuerySingleByID(int id, IDbContext context = null) {
				if (context == null) context = Db.GetInstance().Context();
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM ord_refund WHERE ID=@0";
			Ordrefund obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
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
		public virtual Ordrefund GetQuerySingleByBillNo(string warehouseCode, string billNo, IDbContext context = null) {
			string strWhere = string.Empty;
			if (!string.IsNullOrEmpty(warehouseCode)) {
				strWhere += " and WarehouseCode = @0";
			}
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = billNo;
			string sqlStr = @"SELECT * FROM ord_refund WHERE BillNo=@1" + strWhere;
			return GetQuerySingle(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM ord_refund WHERE ID=@0";
			return Del(sqlStr, context, objects);
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
		public virtual int Updatelogistics(string userCode, int ordRefundID, string expressCompany, string waybillNo, decimal returnFreight, IDbContext context = null) {
			Object[] objects = new Object[8];
			objects[0] = ordRefundID;
			objects[1] = (int)OrdRefundStatus.等待买家退货;
			objects[2] = expressCompany;
			objects[3] = waybillNo;
			objects[4] = returnFreight;
			objects[5] = (int)OrdRefundStatus.等待卖家收货;
			objects[6] = userCode;
			objects[7] = DateTime.Now;
			string sqlStr = @"UPDATE ord_refund SET ExpressCompany=@2,WaybillNo=@3,ReturnFreight=@4,Status=@5,UpdatePerson=@6,UpdateDate=@7,SendBackDate=@7 WHERE ID=@0 AND Status=@1";
			return Update(sqlStr, context, objects);
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
		/// <param name="dutyOther">其他责任方</param>
		/// <param name="refundAmount">退金额</param>
		/// <param name="refundFreight">退运费</param>
		/// <param name="returnFreight">寄回运费</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int ReceiveNormal(string userCode, int ordRefundID, string receiveRemark, int duty, string dutyOther, decimal refundAmount, decimal refundFreight, decimal returnFreight, IDbContext context = null) {
			Object[] objects = new Object[11];
			objects[0] = ordRefundID;
			objects[1] = (int)OrdRefundStatus.等待卖家收货 + "," + (int)OrdRefundStatus.收货异常;
			objects[2] = (int)OrdRefundStatus.已完成;
			objects[3] = receiveRemark;
			objects[4] = duty;
			objects[5] = dutyOther;
			objects[6] = refundAmount;
			objects[7] = refundFreight;
			objects[8] = returnFreight;
			objects[9] = userCode;
			objects[10] = DateTime.Now;
			string sqlStr = @"UPDATE ord_refund SET Status=@2,ReceiveRemark=@3,Duty=@4,DutyOther=@5,RefundAmount=@6,RefundFreight=@7,ReturnFreight=@8,UpdatePerson=@9,UpdateDate=@10,ReceiveDate=@10 WHERE ID=@0 AND FIND_IN_SET(Status,@1)";
			return Update(sqlStr, context, objects);
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
		public virtual int ReceiveException(string userCode, int ordRefundID, string receiveRemark, IDbContext context = null) {
			Object[] objects = new Object[6];
			objects[0] = ordRefundID;
			objects[1] = (int)OrdRefundStatus.等待卖家收货;
			objects[2] = (int)OrdRefundStatus.收货异常;
			objects[3] = receiveRemark;
			objects[4] = userCode;
			objects[5] = DateTime.Now;
			string sqlStr = @"UPDATE ord_refund SET Status=@2, ReceiveRemark=@3, UpdatePerson=@4, UpdateDate=@5 WHERE ID=@0 AND Status=@1";
			return Update(sqlStr, context, objects);

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
		public virtual int Cancel(string userCode, string warehouseCode, string billNo, IDbContext context = null) {
			string strWhere = string.Empty;
			if (!string.IsNullOrEmpty(warehouseCode)) {
				strWhere += " and WarehouseCode = @0";
			}
			Object[] objects = new Object[6];
			objects[0] = warehouseCode;
			objects[1] = billNo;
			objects[2] = (int)OrdRefundStatus.已取消;
			objects[3] = (int)OrdRefundStatus.已完成;
			objects[4] = userCode;
			objects[5] = DateTime.Now;
			string sqlStr = @"UPDATE ord_refund SET Status=@2, UpdatePerson=@4, UpdateDate=@5 WHERE BillNo=@1 AND Status<>@2 AND Status<>@3" + strWhere;
			return Update(sqlStr, context, objects);

		}

		#endregion

		#region 根据订单号获取出库单记录

		/// <summary>
		/// 根据订单号获取出库单记录
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual List<OrdRefundList> GetManyOrdrefund(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = @"SELECT * FROM ord_refund where ErpOrderCode = @0";
			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QueryMany<OrdRefundList>();
		}

		#endregion
	}
}





