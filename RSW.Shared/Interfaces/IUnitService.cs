using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface IUnitService
    {
        Task<IEnumerable<Unit>> GetAllAsync();
        Task<Unit?> GetByIdAsync(Guid id);
        Task<Unit> CreateAsync(Unit model);
        Task<Unit?> UpdateAsync(Guid id, Unit model);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Patrol>>? GetPatrolsAsync(Guid id);
    }
}