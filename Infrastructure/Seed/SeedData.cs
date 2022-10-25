using Domain.Poco;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Seed
{
    public static class SeedData
    {
        public static async System.Threading.Tasks.Task SeedEverything(IServiceProvider serviceProvider)
        {
            await SeedUsers(serviceProvider);
            await SeedPermissions(serviceProvider);
        }

        public static async System.Threading.Tasks.Task SeedPermissions(IServiceProvider serviceProvider)
        {
            var permissions = new List<Permission>()
            {
                new() { PermissionName = "Create" },
                new() { PermissionName = "Read" },
                new() { PermissionName = "Update" },
                new() { PermissionName = "Delete" },
            };


            using var scope = serviceProvider.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<MeamaContext>();

            var seeded = false;
            foreach (var permission in permissions)
            {
                if (await database.Permissions.AnyAsync(x => x.PermissionName == permission.PermissionName)) continue;

                await database.Permissions.AddAsync(permission);
                seeded = true;
            }

            if (seeded)
                await database.SaveChangesAsync();
        }


        private static async System.Threading.Tasks.Task SeedUsers(IServiceProvider provider)
        {
            //UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, MeamaContext meamaContext+
            using var scope = provider.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<MeamaContext>();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            const string adminRole = "admin";
            const string adminEmail = "tornikegogberashvili@gmail.com";
            const string adminPassword = "12345678kkK*";

            const string userRole = "user";
            const string userEmail = "user1@gmail.com";
            const string userPassword = "12345678kkK*";


            if (!(await database.Roles.AnyAsync(x => x.Name.ToLower() == adminRole)))
            {
                //var role = new IdentityRole(adminRole);
                //await roleManager.CreateAsync(role);
                var adminRole1 = new IdentityRole
                {
                    Id = "1",
                    Name = adminRole
                };
            }

            if (!(await database.Roles.AnyAsync(x => x.Name.ToLower() == userRole)))
            {
                var role = new IdentityRole(userRole);
                await roleManager.CreateAsync(role);

                var userRole1 = new IdentityRole
                {
                    Id = "2",
                    Name = userRole
                };
            }

            if (!(await database.Users.AnyAsync(x => x.Email.ToLower() == adminEmail.ToLower())))
            {
                var admin = new IdentityUser
                {
                    Id = "2",
                    Email = adminEmail,
                    UserName = adminEmail,
                };

                var user = new IdentityUser
                {
                    Id = "1",
                    Email = userEmail,
                    UserName = userEmail,
                };

                var adminResult = await userManager.CreateAsync(admin, adminPassword);
                var userResult = await userManager.CreateAsync(user, userPassword);

                await userManager.AddToRoleAsync(admin, adminRole);
                await userManager.AddToRoleAsync(user, userRole);
            }
        }
    }
}
