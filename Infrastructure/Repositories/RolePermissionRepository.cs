using Application.Roles.Repository;
using Domain.Poco;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(MeamaContext dbContext) : base(dbContext)
        {
        }
    }
}
