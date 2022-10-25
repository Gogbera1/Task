using Application.User.Repositories;
using Domain.Poco;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<IdentityUser>, IUserRepository
    {
        private readonly MeamaContext _context;
        public UserRepository(MeamaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Permission>> GetUserPermisions(string roleId)
        {
            return await _context.Set<RolePermission>().Include(x => x.Permission)
                .Where(x => x.RoleId == roleId)
                .Select(x => x.Permission)
                .ToListAsync();

        }
    }
}
