using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chunkk.JWT.Server.Services
{
    public class AuthService<TUser> : IAuthService<TUser> where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthService<TUser>> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(
            UserManager<TUser> userManager, 
            IConfiguration config, 
            ILogger<AuthService<TUser>> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _config = config;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        // ---------------------------
        // LOGIN
        // ---------------------------
        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var check = await _userManager.CheckPasswordAsync(user, password);
            if (!check) return null;

            return await GenerateJwtToken(user);
        }

        // ---------------------------
        // REGISTER
        // ---------------------------
        public async Task<string?> RegisterAsync(string email, string password)
        {
            // Maak user via reflection / dynamisch, library hoeft velden niet te kennen
            var user = Activator.CreateInstance<TUser>();
            if (user == null) throw new InvalidOperationException("Cannot create user instance");

            // Stel UserName & Email
            var userNameProp = typeof(TUser).GetProperty("UserName");
            var emailProp = typeof(TUser).GetProperty("Email");

            if (userNameProp != null) userNameProp.SetValue(user, email);
            if (emailProp != null) emailProp.SetValue(user, email);

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                foreach(var err in result.Errors)
                    Console.WriteLine(err.Description);
                return null;
            }

            // Optioneel: rol toevoegen als er een role manager is
            await _userManager.AddToRoleAsync(user, "User");

            return await GenerateJwtToken(user);
        }

        // ---------------------------
        // GENERATE JWT
        // ---------------------------
        private async Task<string> GenerateJwtToken(TUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, await _userManager.GetUserIdAsync(user)!),
                new(ClaimTypes.Name, await _userManager.GetUserNameAsync(user)!)
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var jwtSettings = _config.GetSection("Jwt");
            Console.WriteLine(jwtSettings["Key"]);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpireMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // ---------------------------
        // PASSWORD RESET TOKEN
        // ---------------------------
        public async Task<string?> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        // ---------------------------
        // RESET PASSWORD
        // ---------------------------
        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }

        // ---------------------------
        // GET USER CLAIMS
        // ---------------------------
        public async Task<IList<Claim>> GetClaimsForUserAsync(TUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, await _userManager.GetUserIdAsync(user)!),
                new(ClaimTypes.Name, await _userManager.GetUserNameAsync(user)!)
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            return claims;
        }

        // ---------------------------
        // LOGOUT (optioneel)
        // ---------------------------
        public async Task LogoutAsync()
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx != null)
            {
                // Cookies verwijderen als aanwezig
                await ctx.SignOutAsync(IdentityConstants.ApplicationScheme);
            }
        }

        // ---------------------------
        // CHECK IF CURRENT USER OR ADMIN
        // ---------------------------
        public bool IsCurrentUserOrAdmin(string userId)
        {
            var ctx = _httpContextAccessor.HttpContext;
            var currentUserId = ctx?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = ctx?.User.IsInRole("Admin") ?? false;

            return currentUserId == userId || isAdmin;
        }
    }
}
