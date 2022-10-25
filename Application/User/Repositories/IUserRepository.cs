using Common.Repository;
using Domain.Poco;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Repositories
{
    public interface IUserRepository : IGenericRepository<IdentityUser>
    {
        Task<List<Permission>> GetUserPermisions(string roleId);
    }
}
