using Jwell.Application.Services.Dtos;
using Jwell.Framework.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    public interface ILogoutService: IApplicationService
    {
        /// <summary>
        /// SSO登出方法
        /// 通过工号删除物理存储在OAuth验证的表数据
        /// 删除Redis里面存储的Session
        /// </summary>
        /// <returns></returns>
        bool SSOLogout(LoginInfoDto loginInfo);
    }
}
