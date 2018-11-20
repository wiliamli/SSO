using Jwell.Modules.Configure;
using Jwell.Modules.WebApi.Attributes;
using Jwell.SSO.Common;
using Jwell.SSO.Models;
using System.Web.Http;

namespace Jwell.SSO.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("SSO/Default")]
    [System.Web.Http.Cors.EnableCors("*", "*", "*")]
    public class DefaultController : ApiController
    {
        /// <summary>
        /// 登录Url
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [ApiIgnore]
        [HttpGet]
        public string LoginUrl(string returnUrl)
        {
            return OAuthHelper.GenerateLoginUrl($"http://{Url.Request.Headers.Host}/AuthorizationCallBack/OAuth", returnUrl);
        }
    }
}
