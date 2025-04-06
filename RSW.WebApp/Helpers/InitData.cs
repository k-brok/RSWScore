using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;

namespace RSW.WebApp.Helpers
{
    public class InitData
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly ApplicationDbContext _context;
        public InitData(UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> rolemanager, ApplicationDbContext context)
        {
            _usermanager = usermanager;
            _rolemanager = rolemanager;
            _context = context;
        }

        public async Task Initialize()
        {
            await SeedRoles();
            await SeedAdminUser();
            await SeedWebSettings();
        }
        
        private readonly string[] Roles = new string[] { "Admin", "Manager", "Member" };
        public async Task SeedRoles()
        {
            foreach (var role in Roles)
            {
                if (!await _rolemanager.RoleExistsAsync(role))
                {
                    await _rolemanager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public async Task SeedAdminUser()
        {
            // 1. Controleer of er al een admin-gebruiker is
            var adminUsers = await _usermanager.GetUsersInRoleAsync("Admin");

            if (!adminUsers.Any()) // Correcte check
            {
                ApplicationUser defaultAdminUser = new ApplicationUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };

                var result = await _usermanager.CreateAsync(defaultAdminUser, "Changeme1!");
                if (result.Succeeded)
                {
                    await _usermanager.AddToRoleAsync(defaultAdminUser, "Admin");
                    Console.WriteLine("Admin-gebruiker aangemaakt en toegevoegd aan de Admin-rol.");
                }
                else
                {
                    Console.WriteLine($"Fout bij aanmaken admin-gebruiker: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                Console.WriteLine("Er bestaat al een admin-gebruiker.");
            }
        }
        public async Task SeedWebSettings()
        {
            var webSettings = new List<WebSetting>
            {
                new WebSetting { Key = "Date format", Value = "dd-MM-yyyy" },
                new WebSetting { Key = "Time format", Value = "HH:mm" },
                new WebSetting { Key = "Timezone", Value = "Central European Standard Time" }
            };

            foreach (var setting in webSettings)
            {
                if (!await _context.WebSettings.AnyAsync(w => w.Key == setting.Key))
                {
                    _context.WebSettings.Add(setting);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
