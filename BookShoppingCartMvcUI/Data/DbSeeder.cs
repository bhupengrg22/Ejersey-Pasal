using Microsoft.AspNetCore.Identity;
using jerseyShoppingCartMvcUI.Constants;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace jerseyShoppingCartMvcUI.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userMgr = service.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = service.GetRequiredService<RoleManager<IdentityRole>>();

            // Seed roles based on the Roles enum
            foreach (var roleName in Enum.GetNames(typeof(Roles)))
            {
                if (!await roleMgr.RoleExistsAsync(roleName))
                {
                    await roleMgr.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed admin user
            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            var userInDb = await userMgr.FindByEmailAsync(admin.Email);
            if (userInDb == null)
            {
                await userMgr.CreateAsync(admin, "Admin@123");
                await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
    }
}
