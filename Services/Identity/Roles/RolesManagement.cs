using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Snacks.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snacks.Services.Identity.Roles
{
    public static class RolesManagement
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userEmail = configuration["Identity:AdminEmail"];
            var userPassword = configuration["Identity:AdminPassword"];
            var firstName = configuration["Identity:AdminFirstName"];
            var lastName = configuration["Identity:AdminLastName"];

            var roleNames = configuration.GetSection("Identity:RoleNames").Get<List<string>>();

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var role = new IdentityRole(roleName);
                    await roleManager.CreateAsync(role);
                }
            }
            
            var user = await userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                var adminUser = new ApplicationUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = userEmail,
                    Email = userEmail
                };

                var createPowerUser = await userManager.CreateAsync(adminUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
