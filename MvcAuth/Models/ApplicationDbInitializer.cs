using Microsoft.AspNetCore.Identity;

namespace MvcAuth.Models
{
    public static class ApplicationDbInitializer
    {
        public static async Task EnsureRoleAsinc(RoleManager<IdentityRole> roleManager){
            var roles = new List<string> {"Admin","User"};
            foreach(var role in roles){
                if(!await roleManager.RoleExistsAsync(role)){
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}