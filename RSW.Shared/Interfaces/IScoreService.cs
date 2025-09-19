using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IScoreService
    {
        Task<IEnumerable<ScoreReadDto>> GetAllAsync();
        Task<ScoreReadDto?> GetByIdAsync(Guid id);
        Task<ScoreReadDto> CreateAsync(ScoreCreateDto dto);
        Task<ScoreReadDto?> UpdateAsync(Guid id, ScoreUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}