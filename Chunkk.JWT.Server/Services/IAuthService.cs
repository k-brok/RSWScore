using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chunkk.JWT.Server.Services
{
    public interface IAuthService<TUser> where TUser : class
    {
        // Login met email en password, geeft JWT token terug
        Task<string?> LoginAsync(string email, string password);

        // Register, optioneel firstname/lastname
        Task<string?> RegisterAsync(string email, string password);

        // Genereer password reset token
        Task<string?> GeneratePasswordResetTokenAsync(string email);

        // Reset password met token
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);

        // Claims ophalen voor een user
        Task<IList<Claim>> GetClaimsForUserAsync(TUser user);

        // Logout
        Task LogoutAsync();

        // Check of huidige gebruiker of admin is
        bool IsCurrentUserOrAdmin(string userId);
    }
}
