using RSW.Domain.Entities;

namespace RSW.Application.Interfaces
{
    public interface IPatrolService
    {
        Task<IEnumerable<Patrol>> GetAllAsync();
        Task<Patrol?> GetByIdAsync(Guid id);
        Task<Patrol> CreateAsync(Patrol model);
        Task<Patrol?> UpdateAsync(Guid id, Patrol model);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Scout>> GetScoutsAsync(Guid Id);
        Task<IEnumerable<Score>> GetScoresAsync(Guid Id);
    }
}