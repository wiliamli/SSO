using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Jwell.SSO.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpClientHelper
    {
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="contentType"></param>
        /// <param name="labradorToken">加入HTTPHeader的token信息</param>
        /// <returns></returns>
        public static string Post(string url, string postData, string contentType = "application/json", string labradorToken = "")
        {
            string result = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(postData);
                    content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    if (!string.IsNullOrEmpty(labradorToken))
                    {
                        content.Headers.Add("Labrador-Token", labradorToken);
                    }
                    var response = client.PostAsync(url, content).Result;
                    var responseStr = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = responseStr;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}