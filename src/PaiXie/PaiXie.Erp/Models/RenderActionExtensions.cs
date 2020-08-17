using System.Web.Mvc.Html;
using System.Web.Routing;
using PaiXie.Utils;
namespace System.Web.Mvc
{
    public static class RenderActionExtensions
    {
	         //嵌套视图  类型用户控件  例：
			 //@{ Html.RenderActionEx("topicleft"); }    
		   // @{ Html.RenderAction("DynamicReviewMsg", new { DynamicMsgid = @item.ID }); }
        public static void RenderActionEx(this HtmlHelper htmlHelper, string actionName)
        {
            try
            {
                ChildActionExtensions.RenderAction(htmlHelper, actionName);
            }
            catch (Exception ex)
            {

				PlanLog.WriteLog(ex.ToString(),LogType.Error.ToString());
            }
        }

        public static void RenderActionEx(this HtmlHelper htmlHelper, string actionName, object routeValues)
        {
            try
            {
                ChildActionExtensions.RenderAction(htmlHelper, actionName, routeValues);
            }
            catch (Exception ex)
            {
				PlanLog.WriteLog(ex.ToString(), LogType.Error.ToString());
            }
        }

        public static void RenderActionEx(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues)
        {
            try
            {
                ChildActionExtensions.RenderAction(htmlHelper, actionName, routeValues);
            }
            catch (Exception ex)
            {
				PlanLog.WriteLog(ex.ToString(), LogType.Error.ToString());
            }
        }

        public static void RenderActionEx(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            try
            {
                ChildActionExtensions.RenderAction(htmlHelper, actionName, controllerName);
            }
            catch (Exception ex)
            {
				PlanLog.WriteLog(ex.ToString(), LogType.Error.ToString());
            }
        }

        public static void RenderActionEx(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
        {
            try
            {
                ChildActionExtensions.RenderAction(htmlHelper, actionName, controllerName, routeValues);
            }
            catch (Exception ex)
            {
				PlanLog.WriteLog(ex.ToString(), LogType.Error.ToString());
            }
        }
        public static void RenderActionEx(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            try
            {
                ChildActionExtensions.RenderAction(htmlHelper, actionName, controllerName, routeValues);
            }
            catch (Exception ex)
            {
				PlanLog.WriteLog(ex.ToString(), LogType.Error.ToString());
            }
        }

    }
}