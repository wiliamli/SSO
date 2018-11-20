using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Routing;

//[assembly: WebActivator.PreApplicationStartMethod(typeof(Jwell.SSO.App_Start.SwaggerNet), "PreStart")]
//[assembly: WebActivator.PostApplicationStartMethod(typeof(Jwell.SSO.App_Start.SwaggerNet), "PostStart")]
namespace Jwell.SSO.App_Start 
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerNet 
    {
        /// <summary>
        /// 
        /// </summary>
        public static void PreStart() 
        {
            RouteTable.Routes.MapHttpRoute(
                name: "SwaggerApi",
                routeTemplate: "api/docs/{controller}",
                defaults: new { swagger = true }
            );            
        }
    }
}