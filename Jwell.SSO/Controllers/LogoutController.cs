using Jwell.Application.Services;

namespace Jwell.SSO.Controllers
{
    /// <summary>
    /// 退出
    /// 删除当前用户在Session中的记录
    /// </summary>
    public class LogoutController : BaseApiController
    {
        private ILogoutService LogoutService { get; set; }

        /// <summary>
        /// 通过请求触发Delete
        /// </summary>
        public LogoutController(ILogoutService logoutService)
        {
            this.LogoutService = logoutService;
        }

        /// <summary>
        /// SSO退出
        /// <param name="accessToken"></param>
        /// </summary>
        public void SSOLogout(string accessToken)
        {
            LogoutService.SSOLogout(new Application.Services.Dtos.LoginInfoDto()
            {
                AccessToken = accessToken
            });
        }
    }
}