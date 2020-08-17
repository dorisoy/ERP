using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Utils;
namespace PaiXie.Data 
{
    public	class WarehousePrintTemplateRepository:BaseRepository<WarehousePrintTemplate> {

        #region 构造函数
     
	    private static WarehousePrintTemplateRepository _instance;
	    public static WarehousePrintTemplateRepository GetInstance() {
            if (_instance == null) {
                _instance = new WarehousePrintTemplateRepository();
            }
            return _instance;
        }
     
        #endregion

	    #region Add
     
	    public int  Add(WarehousePrintTemplate entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int Id = context.Insert<WarehousePrintTemplate>("warehousePrintTemplate", entity)
			        .AutoMap(x => x.ID)
                    .ExecuteReturnLastId<int>();
		    return Id;
	    }
     
	    #endregion

	    #region Update
        
	    public int Update(WarehousePrintTemplate entity, IDbContext context = null) {
            if (context == null) context = Db.GetInstance().Context();
		    int rowsAffected = context.Update<WarehousePrintTemplate>("warehousePrintTemplate", entity)
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
		public virtual WarehousePrintTemplate GetQuerySingleByID(int id, IDbContext context = null) {
				if (context == null) context = Db.GetInstance().Context();
            Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehousePrintTemplate WHERE ID=@0";
			WarehousePrintTemplate obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
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
			string sqlStr = "DELETE FROM warehousePrintTemplate WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

	    #endregion

		#region 根据模版名称和模版类型获取模版ID

		/// <summary>
		/// 根据模版名称和模版类型获取模版ID
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="name">模版名称</param>
		/// <param name="typeID">模版类型</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetPrintTemplateID(string warehouseCode, string name, int typeID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = name;
			objects[2] = typeID;
			string sqlStr = @"SELECT ID FROM warehousePrintTemplate WHERE WarehouseCode=@0 AND Name=@1 AND TypeID=@2";
			return ZConvert.StrToInt(Getobject(sqlStr, context, objects));
		}

		#endregion

		#region 根据模版名称和模版类型获取排除指定模版ID之外的模版ID(修改模版时使用)

		/// <summary>
		/// 根据模版名称和模版类型获取排除指定模版ID之外的模版ID(修改模版时使用)
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="name">模版名称</param>
		/// <param name="typeID">模版类型</param>
		/// <param name="exceptID">需要排除模版ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetPrintTemplateID(string warehouseCode, string name, int typeID, int exceptID, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = warehouseCode;
			objects[1] = name;
			objects[2] = typeID;
			objects[3] = exceptID;
			string sqlStr = @"SELECT ID FROM warehousePrintTemplate WHERE WarehouseCode=@0 AND Name=@1 AND TypeID=@2 AND ID<>@3";
			return ZConvert.StrToInt(Getobject(sqlStr, context, objects));
		}

		#endregion

		#region 根据模版类型取消指定ID之外的默认模版

		/// <summary>
		/// 根据模版类型取消指定ID之外的默认模版
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="typeID">模版类型 枚举值</param>
		/// <param name="exceptID">排除的ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int CancelDefault(string userCode, string warehouseCode, int typeID, int exceptID, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = typeID;
			objects[2] = exceptID;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			string sqlStr = @"UPDATE warehousePrintTemplate SET IsDefault=0,UpdatePerson=@3,UpdateDate=@4 WHERE WarehouseCode=@0 AND TypeID=@1 AND ID<>@2";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 保存打印模版

		/// <summary>
		/// 保存打印模版
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="width">宽度</param>
		/// <param name="height">高度</param>
		/// <param name="id">打印模版ID</param>
		/// <param name="templateContent">模版内容</param>
		/// <param name="secondPageOffset">次页打印偏移</param>
		/// <param name="printerName">默认打印机名称 如果是恢复默认设置，不要传该参数</param>
		/// <param name="isPrintPro">是否打印商品明细 0否 1是 如果是恢复默认设置，不要传该参数</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int SavePrintTemplate(string userCode, string warehouseCode, int id, decimal width, decimal height, string templateContent, decimal secondPageOffset, string printerName = null, int? isPrintPro = null, IDbContext context = null) {
			Object[] objects = new Object[10];
			objects[0] = warehouseCode;
			objects[1] = id;
			objects[2] = width;
			objects[3] = height;
			objects[4] = templateContent;
			objects[5] = userCode;
			objects[6] = DateTime.Now;
			objects[7] = secondPageOffset;
			string fieldsStr = string.Empty;
			if (printerName != null) {
				objects[8] = printerName;
				fieldsStr += ",PrinterName=@8";
			}
			if (isPrintPro != null) {
				objects[9] = isPrintPro;
				fieldsStr += ",IsPrintPro=@9";
			}
			string sqlStr = @"UPDATE warehousePrintTemplate SET Width=@2, Height=@3, TemplateContent=@4,UpdatePerson=@5,UpdateDate=@6,SecondPageOffset=@7" + fieldsStr + " WHERE WarehouseCode=@0 AND ID=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 保存打印明细字段

		/// <summary>
		/// 保存打印明细字段
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="id">打印模版ID</param>
		/// <param name="skuFields">明细字段</param>
		/// <param name="printProFieldWidth">明细字段宽度占比</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int SavePrintPro(string userCode, string warehouseCode, int id, string skuFields, string printProFieldWidth, IDbContext context = null) {
			Object[] objects = new Object[6];
			objects[0] = warehouseCode;
			objects[1] = id;
			objects[2] = skuFields;
			objects[3] = printProFieldWidth;
			objects[4] = userCode;
			objects[5] = DateTime.Now;
			string sqlStr = @"UPDATE warehousePrintTemplate SET IsPrintPro=1,PrintProField=@2,PrintProFieldWidth=@3,UpdatePerson=@4,UpdateDate=@5 WHERE WarehouseCode=@0 AND ID=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion
	}
}





