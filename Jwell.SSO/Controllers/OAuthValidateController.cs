using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Framework.Utilities;
using Jwell.Modules.WebApi.Attributes;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Jwell.SSO.Controllers
{
    /// <summary>
    /// 授权验证
    /// </summary>
    [RoutePrefix("SSO/OAuthValidate")]
    public class OAuthValidateController : ApiController
    {
        private IOAuthValidateService OAuthValidateService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public OAuthValidateController(IOAuthValidateService oAuthValidateService)
        {
            this.OAuthValidateService = oAuthValidateService;
        }

        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiIgnore]
        public AccessTokenDto GetTokenByCode(FormDataCollection model)
        {
            if (model["grantType"] != "authorizationCode")
            {
                throw new System.Exception("grantType验证失败");
            }

            var oAuthDto = OAuthValidateService.GetOAuthTokenByCode(model["clientId"], model["clientSecret"], model["redirectUrl"], model["code"]);
            AccessTokenDto accessTokenDto = oAuthDto != null ?
                Serializer.FromJson<AccessTokenDto>(oAuthDto.AccessToken) : 
                throw new System.Exception("Token不存在");
            return accessTokenDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiIgnore]
        public AccessTokenDto GetBaseTokenByCode(FormDataCollection model)
        {
            if (model["grantType"] != "authorizationCode")
            {
                throw new System.Exception("grantType验证失败");
            }
            AccessTokenDto accessTokenDto = null;
            var oAuthDto = OAuthValidateService.GetBasicInfoTokenByEmployeeID(model["code"]);
            if (oAuthDto != null)
            {
                oAuthDto.ServiceNumber = model["clientId"];
                OAuthValidateService.Save(oAuthDto);
                accessTokenDto = Serializer.FromJson<AccessTokenDto>(oAuthDto.AccessToken);
            }
            else
            {
                throw new System.Exception("Token不存在");
            }
           
                
            return accessTokenDto;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiIgnore]
        public TokenInfo GetUserInfoByToken(FormDataCollection model)
        {
            var oAuthDto = OAuthValidateService.GetUserInfoByToken(model["accessToken"]);
            TokenDto tokenDto = new TokenDto();
            if (!string.IsNullOrWhiteSpace(oAuthDto.Token))
            {
                tokenDto = Serializer.FromJson<TokenDto>(oAuthDto.Token);
            }
            return tokenDto.AssessToken;
        }
    }
}
