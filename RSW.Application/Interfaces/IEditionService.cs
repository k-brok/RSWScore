using RSW.Domain.Entities;

namespace RSW.Application.Interfaces
{
    public interface IEditionService
    {
        Task<IEnumerable<Edition>> GetAllAsync();
        Task<Edition?> GetByIdAsync(Guid id);
        Task<Edition?> GetActiveAsync();
        Task<Edition> CreateAsync(Edition model);
        Task<IEnumerable<SubGroup>> GetSubGroupsAsync(Guid id);
        Task<Edition?> UpdateAsync(Guid id, Edition model);
        Task<Edition?> ActivateAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }

}