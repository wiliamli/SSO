using System;

namespace Jwell.Framework.Ioc
{
    /// <summary>
    /// 声明一个服务被DI框架忽略
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class IgnoreAttribute : Attribute
    {

    }
}
