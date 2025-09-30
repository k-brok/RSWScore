using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RSW.Shared.Dto
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        public string? Password { get; set; }

        public LoginRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        // Parameterloze constructor (verplicht voor databinding in Blazor)
        public LoginRequest() { }
    }

    public class RegisterRequest
    {
        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [MinLength(6, ErrorMessage = "Wachtwoord moet minstens 6 tekens bevatten")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Bevestig wachtwoord is verplicht")]
        [Compare("Password", ErrorMessage = "Wachtwoorden komen niet overeen")]
        public string? ConfirmPassword { get; set; }

        public RegisterRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        // Parameterloze constructor (verplicht voor databinding in Blazor)
        public RegisterRequest() { }
    }

    public class ResetPasswordTokenRequest
    {
        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres")]
        public string? Email { get; set; }

        public ResetPasswordTokenRequest(string email)
        {
            Email = email;
        }

        public ResetPasswordTokenRequest() { }
    }

    public class ResetPasswordRequest
    {
        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Token is verplicht")]
        public string? Token { get; set; }

        [Required(ErrorMessage = "Nieuw wachtwoord is verplicht")]
        [MinLength(6, ErrorMessage = "Wachtwoord moet minstens 6 tekens bevatten")]
        public string? NewPassword { get; set; }

        public ResetPasswordRequest(string email, string token, string newPassword)
        {
            Email = email;
            Token = token;
            NewPassword = newPassword;
        }

        public ResetPasswordRequest() { }
    }

    public class LoginResponse
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public string? Token { get; set; }

        public LoginResponse(string message, bool success = false, string token = null!)
        {
            Message = message;
            Success = success;
            Token = token;
        }

        public LoginResponse() { }
    }

    public class RegisterResponse
    {
        public IdentityResult IdentityResult { get; set; }
        public string? Token { get; set; }

        public RegisterResponse(IdentityResult identityResult, string token = null!)
        {
            IdentityResult = identityResult;
            Token = token;
        }

        public RegisterResponse() { }
    }
    public class ConfirmEmailRequest
    {
        public string? UserId { get; set; }
        public string? Token { get; set; }
    }

    public class ConfirmEmailResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
