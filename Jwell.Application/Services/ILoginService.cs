using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jwell.Application.Services.Dtos;
using Jwell.Framework.Application.Service;

namespace Jwell.Application.Services
{
    public interface ILoginService: IApplicationService
    {
        /// <summary>
        /// 验证登录信息
        /// 1.需要解析ReturnUrl，提取其中的域名，或者IP
        /// 2.通过域名，或者IP查找对应的ServerNumber
        /// 3.最后通过ServerNumber在权限系统中是否有该人
        ///   并验证工号和密码
        /// </summary>
        /// <param name="state"></param>
        /// <param name="scope"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        bool Validate(string state, string scope, LoginInfoDto loginInfo);





    }
}
