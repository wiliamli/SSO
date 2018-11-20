using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jwell.Modules.WebApi.Attributes
{
    /// <summary>
    /// 不作为OpenApi
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiIgnoreAttribute : Attribute
    {
    }
}