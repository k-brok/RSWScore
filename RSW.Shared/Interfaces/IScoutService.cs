using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IScoutService
    {
        Task<IEnumerable<ScoutDto>> GetAllAsync();
        Task<ScoutDto?> GetByIdAsync(Guid id);
        Task<ScoutDto> CreateAsync(ScoutCreateDto dto);
        Task<ScoutDto?> UpdateAsync(Guid id, ScoutDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}