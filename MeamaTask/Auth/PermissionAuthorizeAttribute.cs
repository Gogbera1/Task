using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MeamaTask.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string[] _permissions;

        public PermissionAuthorizeAttribute(string permission)
        {
            if (string.IsNullOrWhiteSpace(permission))
                throw new ArgumentException(nameof(permission));
            _permissions = permission.Split(',');
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity?.IsAuthenticated ?? false)
                return Task.CompletedTask;


            var isAuthorized = user?.Claims
                ?.Where(x => x.Type == "permission")
                ?.Any(t => _permissions.Contains(t.Value) || t.Value == "Admin") ?? false;

            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult(403);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
