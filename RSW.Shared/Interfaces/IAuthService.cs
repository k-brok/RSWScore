using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using RSW.Shared.Dto;
using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface IAuthService
    {
        // Login met email en password, geeft JWT token terug
        Task<LoginResponse?> LoginAsync(LoginRequest request, bool rememberMe);

        // Register, optioneel firstname/lastname
        Task<RegisterResponse?> RegisterAsync(RegisterRequest request, bool rememberMe);

        // Genereer password reset token
        Task GeneratePasswordResetTokenAsync(ResetPasswordTokenRequest request);

        // Reset password met token
        Task<bool> ResetPasswordAsync(ResetPasswordRequest request);

        // Claims ophalen voor een user
        Task<IList<Claim>> GetClaimsForUserAsync(ApplicationUser user);
        // Logout
        Task LogoutAsync();
        public Task<ApplicationUser?> GetCurrentUserAsync();
    }
}
