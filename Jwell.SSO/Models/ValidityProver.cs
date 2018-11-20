using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jwell.Framework.Utilities;
using Jwell.Modules.Configure;
using Jwell.SSO.Common;

namespace Jwell.SSO.Models
{
    /// <summary>
    /// 登录有效性验证
    /// </summary>
    public class ValidityProver
    {
        /// <summary>
        /// 
        /// </summary>
        public static ValidityProver Prover
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    throw new InvalidOperationException("无法获取HttpContext.Current属性, 请在Web环境下调用");
                }

                Type type = typeof(ValidityProver);

                if (!HttpContext.Current.Items.Contains(type))
                {
                    ValidityProver prover = new ValidityProver(HttpContext.Current);
                    HttpContext.Current.Items.Add(type, prover);
                }
                return (ValidityProver)HttpContext.Current.Items[type];
            }
        }

        private string AccessToken
        {
            get
            {
                var accessToken = HttpContext.Request.Cookies["accessToken"];

                return accessToken != null ? accessToken.Value : string.Empty;
            }
        }

        private HttpContext HttpContext { get; set; }
        private ValidityProver(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AccessToken))
                {
                    throw new UnauthorizedAccessException("未登录");
                }
                else
                {
                    return true;
                }
            }
        }

        private UserInfo userInfo;
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public UserInfo UserInfo
        {
            get
            {
                if (userInfo == null)
                {
                    string cookieKey = JwellConfig.GetAppSetting("AccessToken");

                    string jsonInfo = OAuthHelper.GetUserInfo(AccessToken, cookieKey);
                    
                    UserInfo curUserInfo = Serializer.FromJson<UserInfo>(jsonInfo);

                    if (curUserInfo == null || curUserInfo.UserID <= 0)
                    {
                        
                        //ExceptionFilter 会处理该异常
                        throw new UnauthorizedAccessException("未登录");
                    }

                    userInfo = curUserInfo;
                }

                return userInfo;
            }
        }

        /// <summary>
        /// 转跳到登录界面
        /// </summary>
        public void RedirectLogin()
        {
            string redirect = $"{HttpContext.Request.Url.Scheme}/OAuth/AuthorizationCallBack";
            string returnUrl = HttpContext.Request.RawUrl;  //TODO：可以约定页面路径与api路径一致,多apiRoute
            string loginUrl = OAuthHelper.GenerateLoginUrl(redirect, returnUrl);

            HttpContext.Response.Redirect(loginUrl);
        }
    }
}