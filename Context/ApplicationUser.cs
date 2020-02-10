using Microsoft.AspNetCore.Identity;

namespace Snacks.Context
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
