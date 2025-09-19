using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IScoutService
    {
        Task<IEnumerable<ScoutReadDto>> GetAllAsync();
        Task<ScoutReadDto?> GetByIdAsync(Guid id);
        Task<ScoutReadDto> CreateAsync(ScoutCreateDto dto);
        Task<ScoutReadDto?> UpdateAsync(Guid id, ScoutUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}