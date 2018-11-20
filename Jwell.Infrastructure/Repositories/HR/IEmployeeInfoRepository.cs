using Jwell.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Repository.Repositories.HR
{
    public interface IEmployeeInfoRepository
    {
        IEnumerable<EmployeeInfo> GetEmployeeInfos();
    }
}
