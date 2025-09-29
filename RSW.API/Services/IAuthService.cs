using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using RSW.Shared.Entities;

namespace RSW.API.Services
{
    public interface IAuthService
    {
        // Login met email en password, geeft JWT token terug
        Task<string?> LoginAsync(string email, string password);

        // Register, optioneel firstname/lastname
        Task<string?> RegisterAsync(string email, string password);

        // Genereer password reset token
        Task GeneratePasswordResetTokenAsync(string email);

        // Reset password met token
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);

        // Claims ophalen voor een user
        Task<IList<Claim>> GetClaimsForUserAsync(ApplicationUser user);

        // Logout
        Task LogoutAsync();

        // Check of huidige gebruiker of admin is
        bool IsCurrentUserOrAdmin(string userId);
        public Task<ApplicationUser> GetCurrentUserAsync();
        public bool IsCurrentUserOrAdmin(ClaimsPrincipal user, string userId);
    }
}
