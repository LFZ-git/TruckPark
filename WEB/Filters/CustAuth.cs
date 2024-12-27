using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WEB.Filters
{
    public class CustAuth : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            bool authorize = false;
            string contoller = Convert.ToString(filterContext.RouteData.Values["controller"]);
            string actionName = Convert.ToString(filterContext.RouteData.Values["action"]);
            if (HttpContext.Current.Session["token"] == null)
            {
                if (contoller != "Account" && actionName != "Login")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
                   
                }
            }
        }
    }
}
