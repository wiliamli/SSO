using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jwell.SSO.Models
{
    /// <summary>
    /// 基础常量
    /// </summary>
    public static class CommonConstants
    {
        /// <summary>
        /// 过期时间(S)
        /// </summary>
        public static int CodeExpire => int.Parse(System.Configuration.ConfigurationManager.AppSettings["codeExpire"]);
    }
}