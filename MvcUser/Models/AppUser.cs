using Microsoft.AspNetCore.Identity;

namespace MvcUser.Models
{
    public class AppUser: IdentityUser
    {
        public string Code{get;set;}
    }
}