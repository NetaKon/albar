using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace StoreAPI.Data;

public static class DbSeeder
{
    public static async Task SeedUsersAndRoles(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var filePath = Path.GetFullPath("DummyData/users.json");
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"User seed data file not found: {filePath}");
        }

        var jsonData = File.ReadAllText(filePath);
        var users = JsonSerializer.Deserialize<List<UserSeedModel>>(jsonData) ?? new List<UserSeedModel>();

        // Ensure roles exist
        foreach (var role in users.Select(u => u.Role).Distinct())
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Add users
        foreach (var userData in users)
        {
            var existingUser = await userManager.FindByEmailAsync(userData.Email);
            if (existingUser == null)
            {
                var user = new IdentityUser { UserName = userData.Email, Email = userData.Email, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, userData.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, userData.Role);
                }
            }
        }
    }
}

public class UserSeedModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
