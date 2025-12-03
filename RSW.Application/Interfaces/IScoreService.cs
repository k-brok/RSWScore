using RSW.Domain.Entities;

namespace RSW.Application.Interfaces
{
    public interface IScoreService
    {
        Task<IEnumerable<Score>> GetAllAsync();
        Task<Score?> GetByIdAsync(Guid id);
        Task<Score> CreateAsync(Score model);
        Task<Score?> UpdateAsync(Guid id, Score model);
        Task<Score?> SetValueAsync(Score model);
        Task<bool> DeleteAsync(Guid id);
    }
}