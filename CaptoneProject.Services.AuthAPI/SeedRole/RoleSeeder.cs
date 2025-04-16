using Microsoft.AspNetCore.Identity;

namespace CaptoneProject.Services.AuthAPI.SeedRole
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("TRAINER"))
            {
                await roleManager.CreateAsync(new IdentityRole("TRAINER"));
            }

            if (!await roleManager.RoleExistsAsync("LEARNER"))
            {
                await roleManager.CreateAsync(new IdentityRole("LEARNER"));
            }
        }
    }
}
