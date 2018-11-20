using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Framework.Mvc;
using Jwell.Framework.Paging;
using Jwell.Modules.WebApi.Attributes;
using System;
using System.Collections;
using System.Web.Http;

namespace Jwell.SSO.Controllers
{
    /// <summary>
    /// SSO注册
    /// </summary>
    [RoutePrefix("SSO/OAuthManage")]
    [System.Web.Http.Cors.EnableCors("*", "*", "*")]
    public class OAuthManageController : BaseApiController
    {
        private IOAuthServersService oauthService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oauthService"></param>
        public OAuthManageController(IOAuthServersService oauthService)
        {
            this.oauthService = oauthService;
        }

        /// <summary>
        /// SSO系统管理列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public StandardJsonResult<PageResult<OAuthServiceDto>> List(SearchOAuthParam request)
        {
            StandardJsonResult<PageResult<OAuthServiceDto>> result = 
                base.StandardAction<PageResult<OAuthServiceDto>>(() =>
            {
                PageResult<OAuthServiceDto> pageResult = oauthService.GetOAuthServersDtos(request);
                return pageResult;
            });

            return result;
        }

        /// <summary>
        /// 默认注册页面
        /// </summary>
        /// <returns></returns>
        [ApiIgnore]
        [HttpGet]
        public void Register()
        {
            
        }

        /// <summary>
        /// 注册由SSO管理系统
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>返回注册码</returns>
        [HttpPost]
        public StandardJsonResult<Hashtable> Save(OAuthServiceDto dto)
        {
            bool success = false;
            StandardJsonResult<Hashtable> result = new StandardJsonResult<Hashtable>();
            try
            {
                dto.CreatedBy = "admin";
                dto.ModifiedBy = "admin";

                Hashtable hashtable = new Hashtable(1);

                result = base.StandardAction<Hashtable> (() =>
                {
                    success = oauthService.Save(dto);
                    if (success)
                    {
                        hashtable.Add("ClientSecret",dto.ClientSecret);
                    }
                    return hashtable;
                });
                result.Success = success;
            }
            catch (Exception ex)
            {
                //TODO：ex.InnerException.InnerException.Message;
                result.Message = ex.Message;
                result.Success = false;
            }
            return result;
        }
    }
}
