using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface ISubGroupService
    {
        Task<IEnumerable<SubGroup>> GetAllAsync();
        Task<SubGroup?> GetByIdAsync(Guid id);
        Task<IEnumerable<Patrol>?> GetPatrolsAsync(Guid id);
        Task<SubGroup> CreateAsync(SubGroup model);
        Task<SubGroup?> UpdateAsync(Guid id, SubGroup model);
        Task<bool> DeleteAsync(Guid id);
    }
}