using Jwell.Framework.Mvc;
using Jwell.Framework.Utilities;
using Jwell.Modules.Configure;
using Jwell.Modules.WebApi.Attributes;
using Jwell.SSO.Common;
using Jwell.SSO.Models;
using System;
using System.Web;
using System.Web.Http;

namespace Jwell.SSO.Controllers
{
    /// <summary>
    ///  基类
    /// </summary>
    //[UserAuthorizeApi]
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult StandardAction(Action action)
        {
            var result = new StandardJsonResult();
            result.StandardAction(action);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult<T> StandardAction<T>(Func<T> action)
        {
            var result = new StandardJsonResult<T>();
            result.StandardAction(() =>
            {
                result.Data = action();
            });
            return result;
        }
    }
}