namespace Application.Roles.Request
{
    public class SetRolePermissionRequestModel
    {
        public string RoleId { get; set; }
        public int[] PermissionIds { get; set; }
    }
}
