#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PaiXie.Utils;
using PaiXie.Data;
using PaiXie.Api.Bll;
using PaiXie.Core; 
#endregion
namespace PaiXie.Erp
{
    public class BaseController : Controller
    {
		#region js 菜单事件权限判断 1  有权限 -99 否
		/// <summary>
		/// js 菜单事件权限判断 1  有权限 -99 否   后台调用
		/// </summary>
		/// <param name="Code">事件代码</param>
		/// <returns></returns>
		//public BaseResult  IsAuthJs(string Code) {
		//		BaseResult BaseResult = new BaseResult();
		//		//bool result = false;
		//		bool result = true;// new Users().IsAuthJs(Code);
		//	if (result) {

		//		BaseResult.result = 1;				
		//	}
		//	else {
		//		BaseResult.result = -99;
		//		BaseResult.message = "没有权限！";
		//	} return BaseResult;
		//}
		/// <summary>
		/// 前端调用
		/// </summary>
		/// <param name="Code"></param>
		/// <returns></returns>
		//public ActionResult GetIsAuthJs(string Code) {
		//	BaseResult BaseResult = new BaseResult();
		//	//权限判断
		//	BaseResult = IsAuthJs(Code);
			
		//	return JsonDate(BaseResult);

		//}
		#endregion

		#region 返回处理过的时间的Json字符串

		/// <summary>
		/// 返回处理过的时间的Json字符串
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public ContentResult JsonDate(object date) {
			var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
			return Content(JsonConvert.SerializeObject(date, Formatting.Indented, timeConverter));
		}

		public string JsonStr(object date) {
			var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
			return JsonConvert.SerializeObject(date, Formatting.Indented, timeConverter);
		} 
		#endregion

		#region 根据字典类型获取对应的字典数据，方便UI控件的绑定

		/// <summary>
		/// 根据字典类型获取对应的字典数据，方便UI控件的绑定
		/// </summary>
		/// <param name="dictTypeName">字典类型名称</param>
		/// <returns></returns>
		public ActionResult GetDictJson(string dictTypeName, int hasPleaseSelect = 1) {
			List<CListItem> treeList = new List<CListItem>();
			CListItem cListItem = new CListItem();
			if (hasPleaseSelect == 1) {
				cListItem.Text = "请选择";
				cListItem.Value = "0";
				treeList.Add(cListItem);
			}
			string sqlStr = XmlHelper.XmlDeserializeFromFile(dictTypeName);	
			if (!string.IsNullOrEmpty(sqlStr)) {
				List<CListItem> objlist = Db.GetInstance().Context().Sql(sqlStr).QueryMany<CListItem>();
				treeList.AddRange(objlist);
			}
			return JsonDate(treeList);
		}
		
		#endregion
    }
}