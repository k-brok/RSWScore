using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetAllAsync();
        Task<Group?> GetByIdAsync(Guid id);
        Task<Group> CreateAsync(Group model);
        Task<Group?> UpdateAsync(Guid id, Group model);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Patrol>>? GetPatrolsAsync(Guid id);
    }
}