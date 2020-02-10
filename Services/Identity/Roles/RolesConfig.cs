using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Snacks.Services.Identity.Roles
{
    public class RolesConfig : IRolesConfig
    {
        private readonly IConfiguration _configuration;

        public RolesConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<SelectListItem> GetRoles()
        {
            var result = new List<SelectListItem>();

            var configRoles = _configuration.GetSection("Identity:RoleNames").Get<List<string>>();
            foreach (var configRole in configRoles)
            {
                result.Add(new SelectListItem()
                {
                    Value = configRole,
                    Text = configRole
                });
            }

            result = result
                .OrderBy(o => o.Text)
                .ToList();

            result.Insert(0, new SelectListItem
            {
                Value = "",
                Text = " - "
            });

            return result;
        }
    }
}
