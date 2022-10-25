

using Microsoft.AspNetCore.Identity;

namespace Domain.Poco
{
    public class RolePermission
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public int PermissionId { get; set; }

        public Permission Permission { get; set; }
        public IdentityRole Role { get; set; }
    }
}
