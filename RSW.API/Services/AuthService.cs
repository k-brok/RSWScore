using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RSW.Shared.Entities;
using System.Net;

namespace RSW.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GraphMailService _graphMailService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IConfiguration config,
            ILogger<AuthService> logger,
            IHttpContextAccessor httpContextAccessor,
            GraphMailService graphMailService)
        {
            _userManager = userManager;
            _config = config;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _graphMailService = graphMailService;
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
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
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
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, await _userManager.GetUserIdAsync(user)!),
                new(ClaimTypes.Name, await _userManager.GetUserNameAsync(user)!)
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var jwtSettings = _config.GetSection("Jwt");
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

        public async Task GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = WebUtility.UrlEncode(token);
            var BaseUrl = _config["Jwt:Audience"];
            var resetLink = $"{BaseUrl}/PasswordReset?email={email}&token={encodedToken}";

            // HTML template inlezen
            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "PasswordReset.html");
            var template = await File.ReadAllTextAsync(templatePath);

            // Placeholder vervanging
            var body = template
                .Replace("{{name}}", user.UserName ?? "gebruiker")
                .Replace("{{resetLink}}", resetLink)
                .Replace("{{expiryMinutes}}", "30")
                .Replace("{{supportEmail}}", "support@regiodelangstraat.nl")
                .Replace("{{year}}", DateTime.UtcNow.Year.ToString());

            await _graphMailService.SendAsync(email, "Wachtwoord reset voor RSW", body);
        }

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
        public async Task<IList<Claim>> GetClaimsForUserAsync(ApplicationUser user)
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
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine(userId);
            if (string.IsNullOrEmpty(userId)) return null;

            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public bool IsCurrentUserOrAdmin(ClaimsPrincipal user, string userId)
        {
            var currentUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = user.IsInRole("Admin");

            return currentUserId == userId || isAdmin;
        }
    }
}
