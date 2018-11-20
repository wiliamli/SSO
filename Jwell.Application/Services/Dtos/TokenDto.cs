using Jwell.Modules.Session.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    [Serializable]
    public class TokenDto
    {
        [JsonProperty("assessToken")]
        public TokenInfo AssessToken { get; set; } = new TokenInfo();
    }

    public class TokenInfo
    {
        [JsonProperty("userID")]
        public long UserID { get; set; }

        [JsonProperty("employeeID")]
        public string EmployeeID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }
    }

    public static class TokenInfoExt
    {
        public static TokenInfoModel TokenModel(this TokenInfo token)
        {
            return new TokenInfoModel()
            {
                Department = token.Department,
                EmployeeID = token.EmployeeID,
                ID = token.UserID,
                Name = token.Name,
                UserName = token.UserName
            };
        }
    }
}
