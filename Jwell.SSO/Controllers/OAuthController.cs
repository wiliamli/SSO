using Jwell.Framework.Utilities;
using Jwell.Modules.Configure;
using Jwell.SSO.Common;
using Jwell.SSO.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace Jwell.SSO.Controllers
{
    /// <summary>
    /// OAuth回调业务端示例代码
    /// </summary>
    public class OAuthController : Controller
    {
        /// <summary>
        /// 回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult AuthorizationCallBack(string code, string state, string returnUrl)
        {
            string cookieKey = JwellConfig.GetAppSetting("accessToken");
            var stateCookie = Request.Cookies["state"];

            //判断State是否一致
            if (stateCookie != null && stateCookie.Value.Equals(state))
            {
                //stateCookie.Expires = DateTime.Now.AddDays(-1); //内存Cookie
                //stateCookie.Path = JwellConfig.AppSettings("WebRootPath");
                System.Web.HttpContext.Current.Response.AppendCookie(stateCookie);

                var param = new NameValueCollection
                {
                    ["clientId"] = JwellConfig.GetAppSetting("clientId"),
                    ["clientSecret"] = JwellConfig.GetAppSetting("clientSecret"),
                    ["redirectUrl"] = JwellConfig.GetAppSetting("redirectUrl"),
                    ["code"] = code,
                    ["grantType"] = "authorizationCode"
                };
                //根据Code申请Token
                var token = GetToken(cookieKey,param);
                if (token != null)
                {
                    int expireTime = 0;
                    int.TryParse(JwellConfig.GetAppSetting("codeExpire"), out expireTime);
                    var cookie = new HttpCookie(cookieKey, token.Value<string>())
                    {
                        //cookie过期时间固定设置为12小时，与token过期时间一致
                        Expires = DateTime.Now.AddHours(expireTime)
                    };

                    var userInfo = GetUserInfo(token.Value<string>(), cookieKey);
                    Request.RequestContext.HttpContext.Session["userContext"] = userInfo;

                    System.Web.HttpContext.Current.Response.AppendCookie(cookie);
                    if (returnUrl.Contains("~")) //解决前端URl存在#的问题
                    {
                        returnUrl = returnUrl.Replace("~", "#");
                    }
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "register");
                    }
                    return Redirect(returnUrl);
                }
            }
            return Redirect(OAuthHelper.GenerateLoginUrl(this.Url.Action("AuthorizationCallBack", "OAuth", null, Request.Url.Scheme), 
                $"http://{HttpContext.Request.Url.Authority}/register/index"));
        }

        private UserInfo GetUserInfo(string token,string cookieKey)
        {
            string jsonInfo = OAuthHelper.GetUserInfo(token, cookieKey);
            UserInfo userInfo = Serializer.FromJson<UserInfo>(jsonInfo);
            return userInfo;
        }

        private JToken GetToken(string cookieKey,NameValueCollection param)
        {
            var result = OAuthHelper.PostRequest(JwellConfig.GetAppSetting("codeApplyForTokenUrl"), param);
            var value = JObject.Parse(result);
            //取得Token存到Cookie里面
            return value[cookieKey];
        }
    }
}