using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    public record LoginRequest(string Email, string Password);

    

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

        
    public record ResetPasswordTokenRequest(string Email);

    public record ResetPasswordRequest(string Email, string Token, string NewPassword);

    public record LoginResponse(string Message, bool Success, string Token);

    public record RegisterResponse(string Message, bool Success, string Token);
}