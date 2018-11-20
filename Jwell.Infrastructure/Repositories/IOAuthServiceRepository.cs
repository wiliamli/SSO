using Jwell.Domain.Entities;
using Jwell.Framework.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Repository.Repositories
{
    public interface IOAuthServiceRepository : IRepository<OAuthService, long>
    {
        
    }
}
