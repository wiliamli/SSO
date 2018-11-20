using System.Linq;
using Jwell.Application.Services.Dtos;
using Jwell.Framework.Utilities;
using Jwell.Modules.Session;
using Jwell.Modules.Session.Model;
using Jwell.Repository.Repositories;

namespace Jwell.Application.Services
{
    public class LogoutService : ILogoutService
    {
        private ISessionManager sessionManager;
        private IOAuthValidateRepository oAuthValidateRepository;

        public LogoutService(ISessionManager sessionManager,
            IOAuthValidateRepository oAuthValidateRepository)
        {
            this.sessionManager = sessionManager;
            this.oAuthValidateRepository = oAuthValidateRepository;
        }

        public bool SSOLogout(LoginInfoDto loginInfo)
        {
            bool result = false;

            var oAuthValidateDto = GetOAuthValidateDto(loginInfo.AccessToken);

            if (oAuthValidateDto != null)
            {
                SessionModel sessionModel = new SessionModel()
                {
                    SessionID = oAuthValidateDto.Code
                };

                if (sessionManager.IsExist(sessionModel))
                {
                    result = sessionManager.RemoveSession(sessionModel);
                }
                else
                {
                    result = true;
                }
                if (result)
                {
                    OAuthValidateDto dto = new OAuthValidateDto()
                    {
                        Code = oAuthValidateDto.Code
                    };

                    result = oAuthValidateRepository.Delete(dto.ToEntity()) > 0;
                }
            }
            return result;
        }

        private OAuthValidateDto GetOAuthValidateDto(string accessToken)
        {
            accessToken = Serializer.ToJson(new AccessTokenDto
            {
                AccessToken = accessToken
            });
            var entity = oAuthValidateRepository.Queryable().
                 FirstOrDefault(m => m.AccessToken == accessToken);

            return entity?.ToDto();
        }
    }
}
