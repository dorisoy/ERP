using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace PaiXie.Erp 
{
    public class MyExceptionFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            //获取系统异常消息记录
            string strException = filterContext.Exception.Message;
            if (!string.IsNullOrEmpty(strException))
            {
               
                Exception exception = filterContext.Exception;
                if (exception != null)
                {
					PlanLog.WriteLog(strException + "----" + exception, LogType.Error.ToString());
             
                }
                else
                {

					PlanLog.WriteLog(strException, LogType.Error.ToString());
                }
            }
         filterContext.HttpContext.Response.Redirect("~/Shared/Error");
        }
    }
}