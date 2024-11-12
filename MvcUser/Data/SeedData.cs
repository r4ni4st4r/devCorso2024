using Microsoft.AspNetCore.Identity;
using MvcUser.Models;

namespace MvcUser.Data
{
    public class SeedData
    {
       public static async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager){
            string[] roleNames = {"Admin","UserA", "UserB","UserC"};
            foreach(var roleName in roleNames)
            {
                if(!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
    
            if(await userManager.FindByEmailAsync("admin@admin.com") == null){
                var adminUser = new AppUser{
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    Code = "123456",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "Password1@");
                if(result.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, "Admin");
            }else{
                var adminUser = await userManager.FindByEmailAsync("admin@admin.com");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
       }
    }
}