using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Jwell.Application.Constant;
using Jwell.Application.Services.Dtos;
using Jwell.Framework.Utilities;
using Jwell.Modules.Cache;
using Jwell.Modules.Session;
using Jwell.Modules.Session.Model;
using Jwell.Repository.Repositories;
using System.Collections.Generic;
using Jwell.Repository.Repositories.HR;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 登录验证
    /// </summary>
    public class LoginService : ILoginService
    {
        private ICacheClient CacheClient { get; set; }
        private ISessionManager SessionManager { get; set; }
        private ISessionChangeDB  SessionChangeDB { get; set; }
        private IOAuthValidateService OAuthValidateService { get; set; }
        //private IAuthUserRepository AuthUserRepository { get; set; }
        //private IAuthOrganizationRepository AuthOrganizationRepository { get; set; }

        
        private TokenDto Token { get; set; }
        private AccessTokenDto AccessToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionManager"></param>
        /// <param name="oAuthValidateService"></param>
        public LoginService(ICacheClient cacheClient,
            ISessionChangeDB sessionChangeDB,
            ISessionManager sessionManager, 
            IOAuthValidateService oAuthValidateService
            //IEmployeeInfoRepository employeeInfoRepository
             )
        {
            this.CacheClient = cacheClient;
            this.SessionChangeDB = sessionChangeDB;
            this.SessionManager = sessionManager;
            this.OAuthValidateService = oAuthValidateService;
            //this.EmployeeInfoRepository = employeeInfoRepository;
        }

        /// <summary>
        /// 到权限获取是否存在
        /// 目前先写SQL吧
        /// 分为各种系统不同的登录验证
        /// </summary>
        /// <param name="state"></param>
        /// <param name="scope"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public bool Validate(string state,string scope, LoginInfoDto loginInfo)
        {
            bool reuslt = false;

            //先假验证成功，需要权限系统接口，
            //等表建立，先写SQL
            //这块以后写入到权限模块
            try
            {

                var employeeInfo = this.CacheClient.GetCache<IEnumerable<EmployeeInfoDto>>
                    (ApplicationConstant.EMPLOYEEKEY);
                //TODO：目前的补偿措施，等待schedual
                if (employeeInfo == null)
                {
                    employeeInfo = GetEmployeeInfos();
                }
            
                if (employeeInfo != null)
                {
                    var query = employeeInfo.FirstOrDefault(m => m.UserName == loginInfo.EmployeeID
                    && m.Password == loginInfo.Password);
                    if (query != null)
                    {
                        Token = new TokenDto()
                        {
                            AssessToken = query.Token()
                        };

                        AccessToken = new AccessTokenDto
                        {
                            AccessToken = MD5Hash(Serializer.ToJson(query))
                        };

                        reuslt = true;
                    }
                    if (reuslt)
                    {
                        SaveValidate(state, scope, loginInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
             return reuslt;
        }

        private void SaveValidate(string state,string scope, LoginInfoDto loginInfo)
        {
            //分配sessionId
            loginInfo.Code = this.SessionChangeDB.Pop(state);
            if (string.IsNullOrWhiteSpace(loginInfo.Code))
            {
                //TODO：这个地方有时候会有问题，要加入日志
                throw new Exception("Code不能为空");
            }
            System.Threading.Tasks.Parallel.Invoke(() =>
            {
                OAuthValidateDto oAuthValidateDto = new OAuthValidateDto
                {
                    Code = loginInfo.Code,
                    EmployeeID = loginInfo.EmployeeID,
                    Password = loginInfo.Password,
                    ServiceNumber = loginInfo.ServiceNumber,
                    CodeExpire = 1200, //默认值
                    CreatedBy = "admin",//TODO：待修改
                    ModifiedBy = "admin",//TODO：待修改
                    State = state,
                    Token = Serializer.ToJson(Token),
                    Scope = scope,
                    AccessToken = Serializer.ToJson(AccessToken)
                };

                OAuthValidateService.Save(oAuthValidateDto);
            }, () =>
            {
                SaveSession(loginInfo.Code);
            });
        }

        /// <summary>
        /// 保存session
        /// </summary>
        /// <param name="loginInfo"></param>
        private void SaveSession(string sessionId)
        {
            SessionModel sessionModel = new SessionModel
            {
                //分配sessionID
                SessionID = sessionId
            };
            if (!SessionManager.IsExist(sessionModel))
            {
                sessionModel.AccessToken = Token.AssessToken.TokenModel();
                SessionManager.SetSession(sessionModel);
            }
        }


        private string MD5Hash(string tokenJson)
        {
           return BitConverter.ToString(MD5.Create().
               ComputeHash(Encoding.UTF8.GetBytes(tokenJson))).Replace("-", "");
        }

        /// <summary>
        /// 目前的员工信息缓存补偿措施，等待schedual
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EmployeeInfoDto> GetEmployeeInfos()
        {
            IEmployeeInfoRepository employeeInfoRepository = new EmployeeInfoRepository();
            IEnumerable<EmployeeInfoDto> employeeInfo = employeeInfoRepository.GetEmployeeInfos().ToDtos();
            System.Threading.Tasks.Task.Run(() =>
            {
                this.CacheClient.SetCache(ApplicationConstant.EMPLOYEEKEY, employeeInfo, 3600 * 12);
            });
            return employeeInfo;
        }
    }
}
