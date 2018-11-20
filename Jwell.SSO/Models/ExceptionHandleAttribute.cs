using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Jwell.SSO
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionHandleAttribute: ExceptionFilterAttribute
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "系统错误";

            if (actionExecutedContext.Exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = "请登录";
            }
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(statusCode, message);
        }
    }
}