using System;
using System.Collections.Generic;
using System.Linq;
using Jwell.Application.Constant;
using Jwell.Application.Services.Dtos;
using Jwell.Domain.Entities;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Domain.Repositories;
using Jwell.Framework.Utilities;
using Jwell.Modules.Session;
using Jwell.Modules.Session.Model;
using Jwell.Repository.Repositories;

namespace Jwell.Application.Services
{
    /// <summary>
    /// OAuth验证
    /// </summary>
    public class OAuthValidateService : ApplicationService, IOAuthValidateService
    {
        private IOAuthValidateRepository Repository { get; set; }

        private IOAuthServiceRepository OAuthServiceRepository { get; set; }

        private ISessionManager SessionManager { get; set; }

        private ISessionChangeDB SessionChangeDB { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public OAuthValidateService(IOAuthValidateRepository repository,
            IOAuthServiceRepository oAuthServiceRepository,
            ISessionManager sessionManager,
            ISessionChangeDB sessionChangeDB)
        {
            this.Repository = repository;
            this.OAuthServiceRepository = oAuthServiceRepository;
            this.SessionManager = sessionManager;
            this.SessionChangeDB = sessionChangeDB;
        }

        /// <summary>
        /// 登录时调用，由
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public OAuthValidateDto GetCodeByState(string state)
        {
            //先保存在缓存session中
            string code = SetCodeByState(state);

            //var entity = Repository.Queryable().FirstOrDefault(m => m.State == state);

            return new OAuthValidateDto() { Code = code, State = state };
        }

        /// <summary>
        /// 根据code获取AccessToken信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public OAuthValidateDto GetOAuthTokenByCode(string clientId, string clientSecret,
            string redirectUrl, string code)
        {
            OAuthValidateDto oAuthValidateDto = null;
            var oAuthServiceDto = GetOAuthServiceInfo(clientId, clientSecret, redirectUrl);

            if (oAuthServiceDto != null)
            {
                oAuthValidateDto = GetOAuthValidateInfo(code);
                if (oAuthValidateDto == null) throw new Exception("Session不存在");
            }
            else
            {
                throw new Exception("该ClientID对应值未纳入验证管理");
            }
            return oAuthValidateDto;
        }

        public OAuthValidateDto GetUserInfoByToken(string token)
        {
            AccessTokenDto accessTokenDto = new AccessTokenDto()
            {
                AccessToken = token
            };

            string accessToken = Serializer.ToJson(accessTokenDto);

            var entity = Repository.Queryable().FirstOrDefault(m => m.AccessToken == accessToken);

            if (entity != null)
            {
                if (!this.SessionManager.IsExist(new SessionModel() { SessionID = entity.Code }))
                {
                    Repository.Delete(entity);
                    entity = null;
                }
            }

            return entity != null ? entity.ToDto() : new OAuthValidateDto();
        }


        /// <summary>
        /// 保存验证成功后的你登录信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Save(OAuthValidateDto dto)
        {
            //是否已经登录
            bool result = false;
            var loginModel = IsLogin(dto);
            if (loginModel != null)
            {
                //
                Repository.Delete(loginModel.Code);
            }
            result = Repository.Add(dto.ToEntity()) > 0;

            return result;
        }

        /// <summary>
        /// 根据code获取基础
        /// </summary>
        /// <param name="employeeID">工号</param> 
        /// <returns></returns>
        public OAuthValidateDto GetBasicInfoTokenByEmployeeID(string employeeID)
        {
            return Repository.Queryable().FirstOrDefault(m => m.EmployeeID == employeeID).ToDto();
        }

        /// <summary>
        /// 获取该工号已登陆的基础设施系统
        /// </summary>
        /// <param name="employeeID">工号</param>
        /// <returns></returns>
        public IEnumerable<OAuthValidateDto> GetBasicInfoLogin(string employeeID)
        {
            var validateList = Repository.Queryable().Where(m => m.EmployeeID == employeeID 
            && m.Scope== ApplicationConstant.BASICINFO);

            var result = from a in validateList
                         join b in OAuthServiceRepository.Queryable() on a.ServiceNumber equals b.ServiceNumber
                         select new OAuthValidateDto()
                         {
                             AccessToken = a.AccessToken,
                             Code = a.Code,
                             CodeExpire = a.CodeExpire,
                             CreatedBy = a.CreatedBy,
                             CreatedTime = a.CreatedTime,
                             Domain = b.DomainName,
                             EmployeeID = a.EmployeeID,
                             ID = a.ID,
                             ModifiedBy = a.ModifiedBy,
                             ModifiedTime = a.ModifiedTime,
                             Password = a.Password,
                             Scope = a.Scope,
                             ServiceNumber = a.ServiceNumber,
                             State = a.State,
                             Token = a.Token
                         };

            return result.ToList();
        }


        #region 私有方法
        /// <summary>
        /// 是否已经登录
        /// </summary>
        /// <param name="dto">登录验证信息</param>
        /// <returns></returns>
        private OAuthValidateDto IsLogin(OAuthValidateDto dto)
        {
            var model = Repository.Queryable().FirstOrDefault(m => m.ServiceNumber == dto.ServiceNumber
              && m.EmployeeID == dto.EmployeeID
              && m.State == dto.State);
            return model?.ToDto();
        }

        /// <summary>
        /// 验证该SSO管理服务是否存在
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        private OAuthServiceDto GetOAuthServiceInfo(string clientId, string clientSecret,
            string redirectUrl)
        {
            var query = OAuthServiceRepository.Queryable().FirstOrDefault(m => m.ClientSecret == clientSecret &&
            m.ServiceNumber == clientId && m.RedirectUri == redirectUrl);
            return query?.ToDto();
        }

        /// <summary>
        /// 通过Code获取OAuth验证信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private OAuthValidateDto GetOAuthValidateInfo(string code)
        {
            OAuthValidateDto oAuthValidateDto = null;

            var session = SessionManager.GetSession(new SessionModel()
            {
                SessionID = code
            });

            if (session != null)
            {
                var entity = Repository.Queryable().FirstOrDefault(m => m.Code == code);
                if (entity != null)
                    oAuthValidateDto = entity.ToDto();
            }
            else
            {
                DeleteOAuthValidate(code);
            }

            return oAuthValidateDto;
        }

        private void DeleteOAuthValidate(string code)
        {
            Repository.Delete(new OAuthValidate
            {
                Code = code
            });
        }

        /// <summary>
        /// 根据state获取/设置code值,code默认1分钟
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private string SetCodeByState(string state)
        {
            bool isExist = this.SessionChangeDB.IsExist(state);
            string code = Guid.NewGuid().ToString("N"); //默认值

            if (!isExist)
            {
                //设置state到code,一个code只可以使用一次
                this.SessionChangeDB.Push(state, code);
            }
            else
            {
                code = this.SessionChangeDB.Pop(state);
            }
            return code;
        }

        #endregion
    }
}
