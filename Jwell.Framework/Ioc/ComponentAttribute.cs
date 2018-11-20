using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Ioc
{
    /// <summary>
    /// 声明一个组件
    /// 用于DI框架自动发现和注册
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class ComponentAttribute : Attribute
    {
        /// <summary>
        /// 组件生命周期
        /// </summary>
        public ServiceLifetime Lifetime { get; private set; }

        /// <summary>
        /// 是否自动注入属性
        /// 默认为true
        /// </summary>
        public bool PropertyAutoWired { get; set; } = true;

        /// <summary>
        /// 是否注册为实现的接口
        /// 默认为true
        /// </summary>
        public bool AsImplementedInterfaces { get; set; } = true;

        /// <summary>
        /// 是否注册为当前类型
        /// 默认为true
        /// </summary>
        public bool AsSelf { get; set; } = true;

        /// <summary>
        /// 是否自动激活服务
        /// 默认为false
        /// </summary>
        public bool AutoActivate { get; set; } = false;

        public ComponentAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}
