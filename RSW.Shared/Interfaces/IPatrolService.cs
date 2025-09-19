using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IPatrolService
    {
        Task<IEnumerable<PatrolReadDto>> GetAllAsync();
        Task<PatrolReadDto?> GetByIdAsync(Guid id);
        Task<PatrolReadDto> CreateAsync(PatrolCreateDto dto);
        Task<PatrolReadDto?> UpdateAsync(Guid id, PatrolUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}