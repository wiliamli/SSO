using Jwell.Application.Services.Dtos;
using Jwell.SSO.Models;
using System.Web.Mvc;
using System.Collections.Specialized;
using Jwell.Application.Services;
using System.Text;
using Jwell.Modules.Configure;
using Jwell.Framework.Extensions;
using System;
using Jwell.Application.Constant;
using System.Collections.Generic;

namespace Jwell.SSO.Controllers
{
    /// <summary>
    /// 统一登录
    /// </summary>
    public class SignOnController : BaseController
    {
        private IOAuthServersService OAuthService { get; set; }
        private ILoginService LoginService { get; set;}
        private IOAuthValidateService OAuthValidateService { get; set; }
        private IOAuthServersService OAuthServersService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oauthService"></param>
        /// <param name="loginService"></param>
        /// <param name="oauthValidateService"></param>
        /// <param name="oauthServersService"></param>
        public SignOnController(IOAuthServersService oauthService,
            ILoginService loginService,
            IOAuthValidateService oauthValidateService,
            IOAuthServersService oauthServersService)
        {
            this.OAuthService = oauthService;
            this.LoginService = loginService;
            this.OAuthValidateService = oauthValidateService;
            this.OAuthServersService = oauthServersService;
        }
         

        /// <summary>
        ///  验证未通过的返回统一登录界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(string message)
        {
            //验证消息
            //TODO:刷页面也可以，但是必须是内网
            #region 禁止外网刷新
            if (TempData["ValidRedirect"] == null)
            {
                if (!(TempData["ValidRedirect"] is bool))
                {
                    return new HttpNotFoundResult();
                }
                else
                {
                    //TODO：或者判断当前state对应的code是否存活
                    bool valid = (bool)TempData["ValidRedirect"];
                    if (!valid)
                    {
                        return new HttpNotFoundResult();
                    }
                }
            }
            #endregion
            //TODO:通过clientSecret来获取服务名称，根据请求的域名来判断部署的环境
            //并通过名称来显示环境
            //构造formUrl
            var nameValue = HttpContext.Request.QueryString;

            var oAuthService = OAuthService.GetOAuthServiceDtoByClientSecret(nameValue["clientSecret"]);

            string serviceName = oAuthService != null 
                ? $"{EnvironmentName(nameValue["returnUrl"])}_{oAuthService.ServiceSign}" : string.Empty;

            string formParams = FormQueryString(nameValue["redirectUrl"], nameValue["returnUrl"],
                nameValue["clientSecret"], nameValue["state"], nameValue["scope"]);
            ViewBag.Msg = message;
            ViewBag.FormParams = formParams;
            ViewBag.Title = serviceName;
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public RedirectResult Login(LoginInfoDto loginInfo)
        {
            string message = string.Empty;

            //TODO：对客户端构造的url进行解析，验证成功后的returnUrl
            //来查找给对应servernumber是否存在
            NameValueCollection nameValue = HttpContext.Request.QueryString;
            string curLogin = "Login?{0}&message={1}";

            ValidateRequestDto validateDto = new ValidateRequestDto()
            {
                AccessToken = nameValue[QueryKeyMenu.accessToken.ToString()],
                ClientSecret = nameValue[QueryKeyMenu.clientSecret.ToString()],
                RedirctUrl = nameValue[QueryKeyMenu.redirctUrl.ToString()],
                ReturnUrl = nameValue[QueryKeyMenu.returnUrl.ToString()],
                State = nameValue[QueryKeyMenu.state.ToString()],
                Scope = nameValue[QueryKeyMenu.scope.ToString()]
            };

            //验证逻辑
            //获取servernumber
            var oAuthServiceDto = OAuthService.GetOAuthServiceDtoByClientSecret(validateDto.ClientSecret);
            if (oAuthServiceDto != null)
            {
                //验证是否存在SSO
                bool isExist = OAuthService.IsExist(oAuthServiceDto.ServiceNumber, validateDto.ClientSecret);
                if (isExist)
                {
                    loginInfo.ServiceNumber = oAuthServiceDto.ServiceNumber;
                    loginInfo.Password = System.Web.Security.FormsAuthentication.
                        HashPasswordForStoringInConfigFile(loginInfo.Password, "MD5").ToLower();
                    //验证该用户是否存在
                    bool isLogin = LoginService.Validate(validateDto.State, validateDto.Scope, loginInfo);
                    if (isLogin)
                    {
                        //转跳到Redirect页面
                        string url = UrlString(oAuthServiceDto.DomainName, oAuthServiceDto.RedirectUri, validateDto.State,
                            loginInfo.Code, validateDto.ReturnUrl);
                        return Redirect(url);
                    }
                    else
                    {
                        message = "该用户不存在";
                    }
                }
                else
                {
                    message = "系统码与密钥不一致";
                }
            }
            else
            {
                message = "该系统未注册统一系统管理.";
            }
            TempData["ValidRedirect"] = true;//不是盗链
            return Redirect(string.Format(curLogin, nameValue, message));
        }

        /// <summary>
        /// 获取Code码
        /// </summary>
        /// <param name="responseType"></param>
        /// <param name="scope"></param>
        /// <param name="clientId"></param>
        /// <param name="redirectUrl"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCodeByState(string responseType,
            string scope, string clientId,
            string redirectUrl, string state,
            string returnUrl)
        {
            if (!Verification(responseType, scope))
            {
                //TODO:日志记录非法信息
                return new HttpNotFoundResult();
            }
            //不是盗链
            TempData["ValidRedirect"] = true;
            string clientSecret = GetClientSecret(state,clientId);
            StringBuilder formUrl = new StringBuilder();
            formUrl.Append($"http://{HttpContext.Request.Url.Authority}");
            formUrl.Append("/SignOn/Login?");
            formUrl.Append($"responseType={responseType}");
            formUrl.Append($"&scope={scope}");
            formUrl.Append($"&clientId={clientId}");
            formUrl.Append($"&clientSecret={clientSecret}");
            formUrl.Append($"&redirectUrl={redirectUrl}");
            formUrl.Append($"&state={state}");
            formUrl.Append($"&returnUrl={returnUrl}");
            return Redirect(formUrl.ToString());
        }


        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidateCode()
        {
            //生成验证码
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = validateCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        /// <summary>
        /// 获取基础设施Url
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult BasicInfoSys(string code)
        {
            var baseInfoSys = OAuthService.GetOAuthServicesByScope(ApplicationConstant.BASICINFO);

            List<string> baseInfoUrls = new List<string>();

            foreach (var sys in baseInfoSys)
            {
                baseInfoUrls.Add(UrlString(sys.DomainName,
                    "/OAuthBaseInfo/AuthorizationCallBack", string.Empty,code,string.Empty));
            }

            return Json(baseInfoUrls,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取登出url
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonResult BasicInfoLogout(string code)
        {
            var baseInfoSys = OAuthValidateService.GetBasicInfoLogin(code);

            List<string> logoutUrls = new List<string>();

            foreach (var item in baseInfoSys)
            {
                logoutUrls.Add($"http://{item.Domain}/OAuthBaseInfo/LogOut");
            }
            return Json(logoutUrls, JsonRequestBehavior.AllowGet);
        }

        #region 私有逻辑
        /// <summary>
        /// 构造客户端回调的Url
        /// </summary>
        /// <param name="demainName">域名</param>
        /// <param name="redirect">回调地址</param>
        /// <param name="state">防CQRF码</param>
        /// <param name="code">生成码</param>
        /// <param name="returnUrl">客户端转跳url</param>
        /// <returns></returns>
        private string UrlString(string demainName,
            string redirect, string state, string code, string returnUrl)
        {
            StringBuilder url = new StringBuilder();
            url.Append($"http://{demainName}{redirect}?");
            url.Append($"state={state}&");
            url.Append($"code={code}&");
            url.Append($"returnUrl={returnUrl}");

            return url.ToString();
        }

        /// <summary>
        /// 构造登录页面的get参数
        /// </summary>
        /// <param name="redirctUrl"></param>
        /// <param name="returnUrl"></param>
        /// <param name="clientSecret"></param>
        /// <param name="state"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        private string FormQueryString(string redirctUrl, string returnUrl, 
            string clientSecret,
            string state,string scope)
        {
            StringBuilder param = new StringBuilder();
            param.Append($"redirctUrl={redirctUrl}");
            param.Append($"&returnUrl={returnUrl}");
            param.Append($"&clientSecret={clientSecret}");
            param.Append($"&state={state}");
            param.Append($"&scope={scope}");
            return param.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private string GetClientSecret(string state,string clientId)
        {
            string clientSecret = string.Empty;
            //两种方式解决CSRF跨站攻击
            OAuthValidateService.GetCodeByState(state);  //生成Code
            System.Threading.Tasks.Parallel.Invoke(() =>
            {
                clientSecret = this.OAuthServersService.GetOAuthServiceDtoByServerNum(clientId).ClientSecret;
            });

            return clientSecret;
        }

        /// <summary>
        /// 参数验证
        /// </summary>
        /// <param name="responseType"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        private bool Verification(string responseType,string scope)
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(responseType) && string.IsNullOrWhiteSpace(scope))
            {
                isValid = false;
            }
            else
            {
                if (responseType != JwellConfig.GetAppSetting("responseType"))
                {
                    isValid = false;
                }
                if (scope != JwellConfig.GetAppSetting("scope"))
                {
                    isValid = false;
                }
            }
            return isValid;
        }


        /// <summary>
        /// 验证验证码是否正确
        /// </summary>
        /// <param name="num">验证码的值</param>
        /// <returns></returns>
        private bool CheckValidateCode(string num)
        {
            bool isChecked = false;

            string curNum = Session["ValidateCode"] == null ? string.Empty :
                Session["ValidateCode"].ToString();
            //判断验证码是否正确
            if (num == curNum && !string.IsNullOrWhiteSpace(num))
            {
                isChecked = true;
            }
            else
            {
                isChecked = false;
            }
            return isChecked;
        }

        /// <summary>
        /// 部署环境
        /// </summary>
        /// <returns></returns>
        private string EnvironmentName(string returnUrl)
        {
            Uri uri = new Uri(returnUrl);

            string host = uri.Host.ToUpper();
            //包含
            if (host.Contains(EnvironmentEnum.TEST.ToString()))
            {
                return $"{ EnvironmentEnum.TEST.EnumDesc()}_";
            }
            else if (host.Contains(EnvironmentEnum.QA.ToString()))
            {
                return $"{EnvironmentEnum.QA.EnumDesc()}_";
            }
            else if (host.Contains(EnvironmentEnum.GRAY.ToString()))
            {
                return $"{EnvironmentEnum.GRAY.EnumDesc()}_";
            }
            else
            {
                return EnvironmentEnum.PRODUCT.EnumDesc();
            }
        }
        #endregion
    }
}