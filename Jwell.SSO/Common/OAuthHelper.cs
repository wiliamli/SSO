using Jwell.Modules.Configure;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;

namespace Jwell.SSO.Common
{
    /// <summary>
    /// OAuth帮助工具
    /// </summary>
    public static class OAuthHelper
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cookieKey"></param>
        /// <returns></returns>
        public static string GetUserInfo(string token, string cookieKey)
        {
            var param = new NameValueCollection
            {
                [cookieKey] = token
            };
            return PostRequest(JwellConfig.GetAppSetting("tokenApplyForUserInfoUrl"), param);
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static string PostRequest(string url, NameValueCollection param)
        {
            var webClient = new WebClient();

            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            byte[] responseData = webClient.UploadValues(url, "POST", param);
            string result = Encoding.UTF8.GetString(responseData);
            return result;
        }

        /// <summary>
        /// 生成授权登录登录地址
        /// </summary>
        /// <returns></returns>
        public static string GenerateLoginUrl(string authUri, string returnUrl)
        {
            // 用于防止跨站请求伪造（CSRF）攻击
            var state = Guid.NewGuid().ToString("N");
            var cookie = new HttpCookie("state", state)
            {
                //Path = JwellConfig.AppSettings("WebRootPath")
                HttpOnly = true
            };
            HttpContext.Current.Response.AppendCookie(cookie);
            var fullUri = new StringBuilder();
            fullUri.AppendFormat("{0}?", JwellConfig.GetAppSetting("StateApplyForCodeUrl"))
                .AppendFormat("responseType={0}", JwellConfig.GetAppSetting("responseType"))
                .AppendFormat("&scope={0}", JwellConfig.GetAppSetting("scope"))
                .AppendFormat("&clientId={0}", JwellConfig.GetAppSetting("clientId")) //项目标示 
                .AppendFormat("&redirectUrl={0}",
                              HttpUtility.UrlEncode(authUri, Encoding.UTF8)) //验证地址,申请Token,写入Cookie
                .AppendFormat("&state={0}", state)
                .AppendFormat("&returnUrl={0}", HttpUtility.UrlEncode(returnUrl, Encoding.UTF8)); //验证通过转跳地址
            return fullUri.ToString();
        }
    }
}