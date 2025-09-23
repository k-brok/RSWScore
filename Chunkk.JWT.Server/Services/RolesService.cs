using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chunkk.JWT.Server;
public static class RolesService
{
    // ---------------------------
    // Standaardrollen aanmaken
    // RoleManager wordt als parameter meegegeven
    // ---------------------------
    public static async Task EnsureDefaultRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] defaultRoles = new[] { "User", "Admin" };
        foreach (var role in defaultRoles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    // ---------------------------
    // Check of huidige gebruiker of admin
    // ---------------------------
    public static bool IsCurrentUserOrAdmin(HttpContext? ctx, string userId)
    {
        if (ctx == null) return false;

        var currentUserId = ctx.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = ctx.User.IsInRole("Admin");

        return currentUserId == userId || isAdmin;
    }
}
