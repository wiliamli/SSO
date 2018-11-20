using Jwell.Framework.Mvc;
using Jwell.Modules.Configure;
using Jwell.SSO.Common;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web.Mvc;
using System.Linq;
using Jwell.SSO.Models;
using Jwell.Framework.Utilities;

namespace Jwell.SSO.Controllers
{
    /// <summary>
    /// 基础Controller
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 标准返回
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public StandardJsonResult StandardAction(Action action)
        {
            var result = new StandardJsonResult();
            result.StandardAction(action);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public StandardJsonResult<T> StandardAction<T>(Func<T> action)
        {
            var result = new StandardJsonResult<T>();
            result.StandardAction(() =>
            {
                result.Data = action();
            });
            return result;
        }

        /// <summary>
        /// 流文件下载
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileStream">文件流</param>
        public void DownloadFile(string fileName, byte[] fileStream)
        {
            Response.ContentType = "application/ms-excel";
            Response.AddHeader(@"Content-Disposition", string.Format("attachment; filename={0}",
                System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8)));
            Response.BinaryWrite(fileStream);
            Response.Flush();
            Response.End();
        }

        /// <summary>
        /// 获取get参数值
        /// </summary>
        /// <param name="key">get参数key</param>
        /// <returns></returns>
        protected string GetQueryString(string key)
        {
            string value = string.Empty;
            if (string.IsNullOrWhiteSpace(key))
            {
                value = HttpContext.Request.QueryString[key];
            }
            return value;
        }

        /// <summary>
        /// 是否是有效域名
        /// </summary>
        /// <returns></returns>
        protected bool IsValidDomainName()
        {
            return HttpContext.Request.UrlReferrer.Host == 
                JwellConfig.GetAppSetting("Host");
        }

        /// <summary>
        /// 是否为有效参数
        /// </summary>
        /// <returns></returns>
        protected bool IsValidParams()
        {
            //TODO：固定配置未来到统一配置
            string[] names = { "responseType", "scope", "clientId",
                "redirectUrl", "state","returnUrl"};
            NameValueCollection nameValue = HttpContext.Request.QueryString;
            bool isValid = (from a in names
                            join b in nameValue.AllKeys on a equals b
                            select a).Count() > 0;

            return isValid;
        }

        /// <summary>
        /// 用户登录信息
        /// </summary>
        protected UserInfo UserInfo
        {
            get
            {
                string callback = this.Url.Action("AuthorizationCallBack", "OAuth", null, Request.Url.Scheme);

                string cookieKey = JwellConfig.GetAppSetting("AccessToken");
                var token = HttpContext.Request.Cookies[cookieKey];
                if (string.IsNullOrEmpty(token?.Value))
                {
                    //通过输入登陆信息，到SSO登陆界面,进行验证，并返回访问授权
                    HttpContext.Response.Redirect(OAuthHelper.GenerateLoginUrl(callback, 
                        Request.Url == null ? string.Empty : Request.Url.ToString()));
                    return null;
                }

                ////如果回话没有登录,但是token未过期，先用token获取用户信息，然后保持到回话session中
                var userInfo = Request.RequestContext.HttpContext.Session["userInfo"] as UserInfo;
                if (userInfo == null)
                {
                    var tokenJson = Serializer.FromJson<UserInfo>(OAuthHelper.GetUserInfo(token?.Value, cookieKey));
                    if (string.IsNullOrWhiteSpace(token.Name))
                    {
                        HttpContext.Response.Redirect(OAuthHelper.GenerateLoginUrl(callback,
                           Request.Url == null ? string.Empty : Request.Url.ToString()));
                        return null;

                    }
                    Request.RequestContext.HttpContext.Session["userInfo"] = userInfo = tokenJson;
                }

                return userInfo;
            }
        }

        /// <summary>
        /// 登出方法
        /// </summary>
        public ActionResult LogOut()
        {
            string accessToken = JwellConfig.GetAppSetting("AccessToken");

            StringBuilder fullLogoutUri = new StringBuilder().Append(JwellConfig.GetAppSetting("SSOLogoutUri"));
            var cookies = HttpContext.Request.Cookies[accessToken];
            if (cookies != null)
            {
                fullLogoutUri.Append("?").
                    Append(accessToken).Append("=").Append(cookies.Value);
                cookies.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookies);
                Request.Cookies.Remove(accessToken);
            }

            string result = HttpClientHelper.Post(fullLogoutUri.ToString(), string.Empty);
            //返回系统首页
            return Redirect($"http://{HttpContext.Request.Url.Authority}");
        }
    }
}