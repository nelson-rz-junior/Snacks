using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snacks.Services.Identity.Roles
{
    public class RolesManagement
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public RolesManagement(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public async Task CreateRoles()
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            var userEmail = _configuration.GetSection("Identity")["AdminEmail"];
            var userPassword = _configuration.GetSection("Identity")["AdminPassword"];
            var roleNames = _configuration.GetSection("Identity:RoleNames").Get<List<string>>();

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
                var adminUser = new IdentityUser
                {
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
