using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Jwell.SSO.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UserAuthorizeApiAttribute: AuthorizationFilterAttribute
    {
        /// <summary>
        /// 进行页面验证，查看Token是否存在
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(false).Any(g => g.GetType() == typeof(AllowAnonymousAttribute)))
            {
                base.OnAuthorization(filterContext);
                return;
            }
            if (ValidityProver.Prover.IsLogin)
            {
                base.OnAuthorization(filterContext);
            }
        }
    }
}