using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Core;
using Newtonsoft.Json;
using PaiXie.Utils;
using PaiXie.Api.Bll;
//using PaiXie.Bll;
namespace PaiXie.Erp {
	/// <summary>
	/// 权限控制
	/// </summary>
	public class MvcMenuFilter : ActionFilterAttribute {

		#region 权限控制

		private bool _isEnable = true;

		public MvcMenuFilter() {
			_isEnable = true;
		}

		public MvcMenuFilter(bool IsEnable) {
			_isEnable = IsEnable;
		}
        	
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{		
			
			try {
				
				#region 默认true 执行验证
		   if (_isEnable)
		   {
					var route = filterContext.RouteData.Values;
					string area = filterContext.RouteData.DataTokens["area"].ToString();
					var url = string.Format("/{0}/{1}/{2}", area, route["controller"], route["action"]);
				   
					  
					 
			#region   //没有权限	
		 //if (!new Users().IsAuth(url) &&  !FormsAuth.GetIsSupper()) 
			  	if (false) 
			{
					
						BaseResult BaseResult = new BaseResult();
						BaseResult.result = -99;
						BaseResult.message = "没有权限！";
						string str = JsonConvert.SerializeObject(BaseResult, Formatting.Indented);
						ContentResult ContentResult = new ContentResult();
						ContentResult.Content = str;
						filterContext.Result = ContentResult;
				} 
	#endregion
				} 
	#endregion
				
			}
			catch (Exception ex)
			{
				PlanLog.WriteLog(ex.ToString(), "MvcMenuFilter"); 
			}
		
			
			base.OnActionExecuting(filterContext);
		} 
	
		#endregion
	}
}