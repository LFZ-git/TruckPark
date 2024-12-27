using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Model.Models;
using Model.Models.UserDetail;
using Utility;
using WEB.APIHelper;

namespace WEB.Filters
{
    public class ActivityLoging : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string contoller = Convert.ToString(filterContext.RouteData.Values["controller"]);
            string actionName = Convert.ToString(filterContext.RouteData.Values["action"]);

            if (contoller != "Account" && actionName != "Login")
            {
                if (HttpContext.Current.Session["UserDetails"] == null)
                {

                    var routeDictionary = new RouteValueDictionary { { "action", "SessionExpired" }, { "controller", "Error" } };
                    filterContext.Result = new RedirectToRouteResult(routeDictionary);
                    //string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
                    //filterContext.Result = new RedirectResult(baseUrl);
                    return;
                }
            }
           
            if (contoller != "Account" && actionName != "Login")
            {
                ActivityModel activityModel = new ActivityModel();
                activityModel.UDID = HttpContext.Current.Session["UserDetails"] == null ? 0 : ((UserDetailModel)HttpContext.Current.Session["UserDetails"]).UDID;
                activityModel.ModuleId = HttpContext.Current.Session["ActiveMenu"] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session["ActiveMenu"]);
                string url = contoller + "/" + actionName;
                activityModel.Activitydate = System.DateTime.Now;
                activityModel.ActivityType = "";
                activityModel.ZoneId = 0;
                activityModel.Remarks = url;
                activityModel.RoleId = 1;
                WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "LogActivity", "Utility", activityModel);

            }
            base.OnActionExecuting(filterContext);                             
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string contoller = Convert.ToString(filterContext.RouteData.Values["controller"]);
            string actionName = Convert.ToString(filterContext.RouteData.Values["action"]);
            if (contoller != "Account" && actionName != "Login" && contoller != "Menu" && actionName != "GetMenu")
            {
                ActivityModel activityModel = new ActivityModel();
                activityModel.UDID = HttpContext.Current.Session["UserDetails"] == null ? 0 : ((UserDetailModel)HttpContext.Current.Session["UserDetails"]).UDID;
                activityModel.ModuleId = HttpContext.Current.Session["ActiveMenu"] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session["ActiveMenu"]);
                string url = contoller + "/" + actionName;
                activityModel.Activitydate = CommonUtilities.DateNow("1");
                activityModel.ActivityType = "";
                activityModel.ZoneId = 0;
                activityModel.Remarks = url;
                activityModel.RoleId = 1;
                WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "LogActivity", "Utility", activityModel);

            }

            base.OnActionExecuted(filterContext);
        }

    }
}