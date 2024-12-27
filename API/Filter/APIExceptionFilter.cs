using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace API.Filter
{
    public class APIExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //Check the Exception Type
            var a = "";

        }
    }
}