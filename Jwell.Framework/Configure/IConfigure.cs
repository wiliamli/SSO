using Jwell.Framework.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Configure
{
    [Singleton]
    public interface IConfigure
    {
        /// <summary>
        /// 根据键获取本地对应的配置信息
        /// </summary>
        /// <param name="key">对应的键值</param>
        /// <returns></returns>
        string GetAppSetting(string key);

        /// <summary>
        /// 根据键获取统一配置信息
        /// </summary>
        /// <param name="key">对应的键值</param>
        /// <returns></returns>
        string GetConfig(string key);
    }
}
