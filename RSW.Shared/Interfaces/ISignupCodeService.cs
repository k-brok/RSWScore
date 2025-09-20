using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface ISignupCodeService
    {
        Task<IEnumerable<SignupCodeDto>> GetAllAsync();
        Task<SignupCodeDto?> GetByIdAsync(Guid id);
        Task<SignupCodeDto> CreateAsync(SignupCodeCreateDto dto);
        Task<SignupCodeDto?> UpdateAsync(Guid id, SignupCodeDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<SignupCodeDto?> ValidateAsync(Guid id);
    }
}