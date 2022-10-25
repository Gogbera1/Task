using System.Collections.Generic;

namespace Domain.Poco
{
    public class Permission
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }

        public ICollection<RolePermission> RolePermmisions { get; set; }
    }
}
