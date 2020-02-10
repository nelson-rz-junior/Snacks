using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snacks.Services.Identity.Roles
{
    public interface IRolesConfig
    {
        List<SelectListItem> GetRoles();
    }
}
