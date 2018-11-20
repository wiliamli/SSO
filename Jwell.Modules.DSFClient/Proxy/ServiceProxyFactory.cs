using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.DSFClient.Proxy
{
    public class ServiceProxyFactory : IServiceProxyFactory
    {
        /// <summary>
        /// 创建服务对象
        /// </summary>
        /// <typeparam name="TReturn">返回类型</typeparam>
        /// <typeparam name="TParameter">参数类型</typeparam>
        /// <param name="serviceName">服务名称</param>
        /// <param name="version">版本号</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public DSFResponse<TReturn> Create<TReturn, TParameter>(string serviceName, string version, DSFParam<TParameter> parameter) where TParameter : new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 异步创建服务对象
        /// </summary>
        /// <typeparam name="TReturn">返回类型</typeparam>
        /// <typeparam name="TParameter">参数类型</typeparam>
        /// <param name="serviceName">服务名称</param>
        /// <param name="version">版本号</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public async Task<DSFResponse<TReturn>> CreateAsync<TReturn, TParameter>(string serviceName, string version, DSFParam<TParameter> parameter) where TParameter:new ()
        {
            await Task.Run(()=> { });
            throw new NotImplementedException();
        }
    }
}
