using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Models;
namespace WebApplication1.Filter
{
    public class BasicAuthAttribute : ActionFilterAttribute
    { 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var account = (filterContext.HttpContext.Session[Common.Common.GetSessionID()] as UserLogin).loginname;
                var password = (filterContext.HttpContext.Session[Common.Common.GetSessionID()] as UserLogin).loginpwd;
                if (account == null || password == null)
                {
                    //用户不登陆的时候跳转到登录页面
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Shared", action = "Login", area = string.Empty }));
                }
            }
            catch
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Share", action = "Login", area = string.Empty }));
            }
        }
    }
}