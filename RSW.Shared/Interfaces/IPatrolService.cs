using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IPatrolService
    {
        Task<IEnumerable<PatrolDto>> GetAllAsync();
        Task<PatrolDto?> GetByIdAsync(Guid id);
        Task<PatrolDto> CreateAsync(PatrolCreateDto dto);
        Task<PatrolDto?> UpdateAsync(Guid id, PatrolDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}