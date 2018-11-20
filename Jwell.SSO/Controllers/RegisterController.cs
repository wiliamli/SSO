using Jwell.SSO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jwell.SSO.Controllers
{
    /// <summary>
    /// SSO注册
    /// </summary>
    //[UserAuthorize]
    public class RegisterController : BaseController
    {
        /// <summary>
        /// GET: Register
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(UserInfo);
        }
    }
}
