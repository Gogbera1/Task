using Application.Roles.Repository;
using Application.Tasks.Repositories;
using Application.User.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            

            return services;
        }
    }
}
