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
using RSW.Shared.Interfaces;
using RSW.Shared.Dto;

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
        public async Task<LoginResponse?> LoginAsync(LoginRequest request, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return null;

            var check = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!check) return null;

            string Token = await GenerateJwtToken(user);

            return new LoginResponse("Login is gelukt!",true,Token);
        }
        public async Task<RegisterResponse?> RegisterAsync(RegisterRequest request, bool rememberMe)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new RegisterResponse(result);
            }

            await _userManager.AddToRoleAsync(user, "User");

            string Token = await GenerateJwtToken(user);

            return new RegisterResponse(result, Token);
        }
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

        public async  Task GeneratePasswordResetTokenAsync(ResetPasswordTokenRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = WebUtility.UrlEncode(token);
            var BaseUrl = _config["Jwt:Audience"];
            var resetLink = $"{BaseUrl}/PasswordReset?email={request.Email}&token={encodedToken}";

            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "PasswordReset.html");
            var template = await File.ReadAllTextAsync(templatePath);

            var body = template
                .Replace("{{name}}", user.UserName ?? "gebruiker")
                .Replace("{{resetLink}}", resetLink)
                .Replace("{{expiryMinutes}}", "30")
                .Replace("{{supportEmail}}", "support@regiodelangstraat.nl")
                .Replace("{{year}}", DateTime.UtcNow.Year.ToString());

            await _graphMailService.SendAsync(request.Email, "Wachtwoord reset voor RSW", body);
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return false;

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            return result.Succeeded;
        }
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
        public async Task LogoutAsync()
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx != null)
            {
                await ctx.SignOutAsync(IdentityConstants.ApplicationScheme);
            }
        }
        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine(userId);
            if (string.IsNullOrEmpty(userId)) return null;

            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }
    }
}
