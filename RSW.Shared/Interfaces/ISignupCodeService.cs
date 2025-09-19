using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface ISignupCodeService
    {
        Task<IEnumerable<SignupCodeReadDto>> GetAllAsync();
        Task<SignupCodeReadDto?> GetByIdAsync(Guid id);
        Task<SignupCodeReadDto> CreateAsync(SignupCodeCreateDto dto);
        Task<SignupCodeReadDto?> UpdateAsync(Guid id, SignupCodeUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<SignupCodeReadDto?> ValidateAsync(Guid id);
    }
}