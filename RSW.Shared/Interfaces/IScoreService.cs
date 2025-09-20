using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IScoreService
    {
        Task<IEnumerable<ScoreDto>> GetAllAsync();
        Task<ScoreDto?> GetByIdAsync(Guid id);
        Task<ScoreDto> CreateAsync(ScoreCreateDto dto);
        Task<ScoreDto?> UpdateAsync(Guid id, ScoreDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}