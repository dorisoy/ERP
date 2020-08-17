using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Utils;
namespace PaiXie.Data 
{
    public	class WarehouseExpressRepository:BaseRepository<WarehouseExpress> {

        #region 构造函数
     
	    private static WarehouseExpressRepository _instance;
	    public static WarehouseExpressRepository GetInstance() {
            if (_instance == null) {
                _instance = new WarehouseExpressRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(WarehouseExpress entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<WarehouseExpress>("warehouseExpress", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(WarehouseExpress entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<WarehouseExpress>("warehouseExpress", entity)
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
		public virtual WarehouseExpress GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseExpress WHERE ID=@0";
			WarehouseExpress obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}
        
		#endregion

		#region 根据快递名称获取快递ID

		/// <summary>
		/// 根据快递名称获取快递ID
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="expressName">快递名称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetExpressID(string warehouseCode, string expressName, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = expressName;
			string sqlStr = "SELECT ID FROM warehouseExpress WHERE WarehouseCode=@0 AND Name=@1";
			int expressID = ZConvert.StrToInt(Getobject(sqlStr, context, objects));
			return expressID;
		}

		#endregion

		#region 根据快递名称获取排除指定快递ID之外的快递ID(修改快递时使用)

		/// <summary>
		/// 根据快递名称获取排除指定快递ID之外的快递ID(修改快递时使用)
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="expressName">快递名称</param>
		/// <param name="exceptExpressID">需要排除快递ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetExpressID(string warehouseCode, string expressName, int exceptExpressID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = expressName;
			objects[2] = exceptExpressID;
			string sqlStr = "SELECT ID FROM warehouseExpress WHERE WarehouseCode=@0 AND Name=@1 AND ID<>@2";
			int expressID = ZConvert.StrToInt(Getobject(sqlStr, context, objects));
			return expressID;
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
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "DELETE FROM warehouseExpress WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 保存快递打印模版

		/// <summary>
		/// 保存快递打印模版
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="width">宽度</param>
		/// <param name="height">高度</param>
		/// <param name="id">快递公司ID</param>
		/// <param name="templateContent">模版内容</param>
		/// <param name="expressPrinterName">默认打印机名称 如果是恢复默认设置，不要传该参数</param>
		/// <param name="isPrintPro">是否打印商品明细 0否 1是 如果是恢复默认设置，不要传该参数</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int SavePrintTemplate(string userCode, string warehouseCode, int id, decimal width, decimal height, string templateContent, string expressPrinterName = null, int? isPrintPro = null, IDbContext context = null) {
			Object[] objects = new Object[9];
			objects[0] = warehouseCode;
			objects[1] = id;
			objects[2] = width;
			objects[3] = height;
			objects[4] = templateContent;
			objects[5] = userCode;
			objects[6] = DateTime.Now;
			string fieldsStr = string.Empty;
			if (expressPrinterName != null) {
				objects[7] = expressPrinterName;
				fieldsStr += ",PrinterName=@7";
			}
			if (isPrintPro != null) {
				objects[8] = isPrintPro;
				fieldsStr += ",IsPrintPro=@8";
			}
			string sqlStr = @"UPDATE warehouseExpress SET Width=@2, Height=@3, TemplateContent=@4,UpdatePerson=@5,UpdateDate=@6" + fieldsStr + " WHERE WarehouseCode=@0 AND ID=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 保存打印明细字段

		/// <summary>
		/// 保存打印明细字段
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="id">快递公司ID</param>
		/// <param name="skuFields">明细字段</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int SavePrintPro(string userCode, string warehouseCode, int id, string skuFields, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = id;
			objects[2] = skuFields;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseExpress SET IsPrintPro=1,PrintProField=@2,UpdatePerson=@3,UpdateDate=@4 WHERE WarehouseCode=@0 AND ID=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据物流ID获取快递列表

		/// <summary>
		/// 根据物流ID获取快递列表
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="logisticsID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public List<WarehouseExpress> GetManyExpress(string warehouseCode, int logisticsID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = logisticsID;
			string sqlStr = "SELECT ID FROM warehouseExpress WHERE WarehouseCode = @0 AND LogisticsID = @1 ORDER BY ID";
			return GetQueryMany(sqlStr,context,objects);
		}

		#endregion
	}
}





