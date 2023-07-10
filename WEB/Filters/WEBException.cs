using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB;
using Utility;
using WEB.APIHelper;
using Model.Models;


namespace WEB.Filters
{
    public class WEBException : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled || filterContext.Exception is NullReferenceException)
            {
                string controllerName = Convert.ToString(filterContext.RouteData.Values["controller"]);
                string actionName = Convert.ToString(filterContext.RouteData.Values["action"]);
                var errorMsg = filterContext.Exception.Message;
                ErrorLogModel error = new ErrorLogModel();
                error.ExceptionMsg = errorMsg;
                error.ExceptionURL = controllerName + "/" + actionName;
                error.Logdate = CommonUtilities.DateNow("1");
                error.LogId = 0;
                HttpContext.Current.Session["ErrorDisplay"] = errorMsg;
                WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "LogError", "Utility", error);

                filterContext.Result = new RedirectResult("/Error/Index");
                filterContext.ExceptionHandled = true;
            }
        }
    }
}