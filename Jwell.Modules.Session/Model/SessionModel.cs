using System;

namespace Jwell.Modules.Session.Model
{
    public class SessionModel
    {
        /// <summary>
        /// SessionID
        /// </summary>
        public string SessionID { get; set; }
        

        /// <summary>
        /// 访问tokenKey
        /// </summary>
        public TokenInfoModel AccessToken { get; set; }

    }
}
