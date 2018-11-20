using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwell.Domain.Entities.HR;

namespace Jwell.Repository.Repositories.HR
{
    public class EmployeeInfoRepository : IEmployeeInfoRepository
    {
        public IEnumerable<EmployeeInfo> GetEmployeeInfos()
        {
            IEnumerable<EmployeeInfo> employeeInfos = null;
            IDataReader reader = SqlHelper.ExecuteReader(@"SELECT
	a.EmpCode AS EmployeeID,
	a.EmpName AS [Name],
	b.UserName,
	b.[Password],
	b.UserId,
	c.[Name] AS Department 
FROM
	HR_Employee a
	LEFT JOIN DRS_Membership b ON a.LoginName = b.UserName
	LEFT JOIN HR_Department c ON a.DeptGID = c.GID");

            employeeInfos = ReaderToEntity(reader);
            return employeeInfos;
        }

        private IEnumerable<EmployeeInfo> ReaderToEntity(IDataReader reader)
        {
            IList<EmployeeInfo> list = new List<EmployeeInfo>();

            while (reader.Read())
            {
                EmployeeInfo info = new EmployeeInfo()
                {
                    Department = reader["Department"].ToString(),
                    EmployeeID = reader["EmployeeID"].ToString(),
                    Password = reader["Password"].ToString(),
                    UserName = reader["UserName"].ToString(),
                    Name = reader["Name"].ToString()
                };
                list.Add(info);
            }
            return list;
        }
    }
}
