using Jwell.Application.Services.Dtos;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Paging;
using System.Collections.Generic;

namespace Jwell.Application.Services
{
    public interface IOAuthServersService : IApplicationService
    {
        /// <summary>
        /// 设置为SSO管理
        /// </summary>
        /// <param name="dto">OAuth数据</param>
        /// <returns></returns>
        bool Save(OAuthServiceDto dto);

        /// <summary>
        /// 根据参数获取OAuth数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <returns></returns>
        PageResult<OAuthServiceDto> GetOAuthServersDtos(PageParam page);


        /// <summary>
        /// 通过服务编号查询对应OAuth注册服务
        /// </summary>
        /// <param name="serviceNumbers">服务编号</param>
        /// <returns></returns>
        OAuthServiceDto GetOAuthServiceDtoByServerNum(string serverNumbers);

        /// <summary>
        /// 通过回调url获取OAuth对象
        /// </summary>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        OAuthServiceDto GetOAuthServiceDtoByClientSecret(string clientSecret);

        /// <summary>
        /// 验证是否注册SSO
        /// </summary>
        /// <param name="serverNumber">服务号</param>
        /// <param name="clientSecret">授权码</param>
        /// <returns></returns>
        bool IsExist(string serverNumber,string clientSecret);

        /// <summary>
        /// 通过访问范围获取系统列表
        /// </summary>
        /// <param name="scope">访问范围</param>
        /// <returns>系统列表</returns>
        IEnumerable<OAuthServiceDto> GetOAuthServicesByScope(string scope);
    }
}
