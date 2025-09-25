using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface ISignupCodeService
    {
        Task<IEnumerable<SignupCode>> GetAllAsync();
        Task<SignupCode?> GetByIdAsync(Guid id);
        Task<SignupCode> CreateAsync(SignupCode model);
        Task<SignupCode?> UpdateAsync(Guid id, SignupCode model);
        Task<bool> DeleteAsync(Guid id);
        Task<SignupCode?> ValidateAsync(Guid id);
    }
}