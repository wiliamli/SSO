using Jwell.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    public class EmployeeInfoDto
    {
        public string Department { get; set; }

        public string EmployeeID { get; set; }

        public string UserName { get; set; }

        public long UserID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }

    public static class ToTokenInfo
    {
        public static TokenInfo Token(this EmployeeInfoDto employeeInfo)
        {
            return new TokenInfo()
            {
                Department = employeeInfo.Department,
                EmployeeID = employeeInfo.EmployeeID,
                Name = employeeInfo.Name,
                UserID = employeeInfo.UserID,
                UserName = employeeInfo.UserName
            };
        }

        public static IEnumerable<EmployeeInfoDto> ToDtos(this IEnumerable<EmployeeInfo> employeeInfoes)
        {
            return from a in employeeInfoes
                   select new EmployeeInfoDto()
                   {
                       Department = a.Department,
                       EmployeeID = a.EmployeeID,
                       Password = a.Password,
                       UserID = a.UserID,
                       UserName = a.UserName,
                       Name = a.Name
                   };
        }
    }
}
