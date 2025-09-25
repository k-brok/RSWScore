using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface ICriteriaService
    {
        Task<IEnumerable<Criteria>> GetAllAsync();
        Task<Criteria?> GetByIdAsync(Guid id);
        Task<Criteria> CreateAsync(Criteria model);
        Task<Criteria?> UpdateAsync(Guid id, Criteria model);
        Task<bool> DeleteAsync(Guid id);
    }
}