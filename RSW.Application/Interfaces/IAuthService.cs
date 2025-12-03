using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using RSW.Domain.Dto;
using RSW.Domain.Entities;

namespace RSW.Application.Interfaces
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
        Task<ConfirmEmailResponse> ConfirmEmailAsync(ConfirmEmailRequest request);
        Task<bool> ResendEmailConfirmationAsync(string email);
        Task<bool> InitiateChangeEmailAsync(string userId, string newEmail);
        Task<ConfirmEmailResponse> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest request);
    }
}
