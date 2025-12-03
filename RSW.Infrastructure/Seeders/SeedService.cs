using Microsoft.AspNetCore.Identity;
using RSW.Domain.Entities;

namespace RSW.Infrastructure.Services;

public class SeedService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public SeedService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedAsync(string adminEmail, string adminPassword)
{
    // 1️⃣ Rollen aanmaken
    string[] roles = { "User", "Admin" };
    foreach (var role in roles)
    {
        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // 2️⃣ Bestaande Admin check
    var adminRoleUsers = await _userManager.GetUsersInRoleAsync("Admin");
    ApplicationUser adminUser;

    if (adminRoleUsers.Any())
    {
        // Er bestaat al een Admin, gebruik deze
        adminUser = adminRoleUsers.First();
    }
    else
    {
        // Admin bestaat nog niet, maak een nieuwe aan
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(adminUser, adminPassword);
        if (!result.Succeeded)
        {
            throw new Exception("Kon Admin gebruiker niet aanmaken: " +
                                string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        await _userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

}
