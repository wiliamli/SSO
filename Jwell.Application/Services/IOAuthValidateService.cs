using Jwell.Application.Services.Dtos;
using Jwell.Framework.Application.Service;
using System.Collections.Generic;

namespace Jwell.Application.Services
{
    /// <summary>
    /// OAuth验证
    /// </summary>
    public interface IOAuthValidateService: IApplicationService
    {
        /// <summary>
        /// 物理存储当前系统的验证信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool Save(OAuthValidateDto dto);

        /// <summary>
        /// 通过state获取code
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        OAuthValidateDto GetCodeByState(string state);

        /// <summary>
        /// 通过code获取Token
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUrl"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        OAuthValidateDto GetOAuthTokenByCode(string clientId, string clientSecret,
            string redirectUrl, string code);

        /// <summary>
        /// 通过Token获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        OAuthValidateDto GetUserInfoByToken(string token);

        /// <summary>
        /// 根据工号获取Token
        /// </summary>
        /// <param name="employeeID">工号</param>
        /// <returns></returns>
        OAuthValidateDto GetBasicInfoTokenByEmployeeID(string employeeID);

        /// <summary>
        /// 获取该工号已登陆的基础设施系统
        /// </summary>
        /// <param name="employeeID">工号</param>
        /// <returns></returns>
        IEnumerable<OAuthValidateDto> GetBasicInfoLogin(string employeeID);
    }
}
