using Application.Jwt.Service;
using Application.Jwt.ServiceImpl;
using Application.Roles.Service;
using Application.Roles.ServiceImpl;
using Application.Tasks.Service;
using Application.Tasks.ServiceImpl;
using Application.User.Service;
using Application.User.ServiceImpl;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }
    }
}
