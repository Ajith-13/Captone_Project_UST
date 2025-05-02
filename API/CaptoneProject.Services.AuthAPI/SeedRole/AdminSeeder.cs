using CaptoneProject.Services.AuthAPI.User;
using Microsoft.AspNetCore.Identity;

namespace CaptoneProject.Services.AuthAPI.SeedRole
{
    public class AdminSeeder
    {
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "ADMIN");
                }
                else
                {
                    throw new Exception("Failed to create the admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
      
        }
}
