using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface IAssociationService
    {
        Task<IEnumerable<Association>> GetAllAsync();
        Task<Association?> GetByIdAsync(Guid id);
        Task<IEnumerable<Group>> GetGroupsAsync(Guid id);
        Task<Association> CreateAsync(Association model);
        Task<Association?> UpdateAsync(Guid id, Association model);
        Task<bool> DeleteAsync(Guid id);
    }
}