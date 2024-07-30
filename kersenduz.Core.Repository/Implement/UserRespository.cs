using kersenduz.Core.Repository.Interface;
using kersenduz.DataAccess.Context;
using kersenduz.DataAccess.Repositories.Implement;
using kersenduz.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kersenduz.Core.Repository.Implement
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
