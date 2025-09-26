using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategory>> GetAllAsync();
        Task<SubCategory?> GetByIdAsync(Guid id);
        Task<SubCategory> CreateAsync(SubCategory model);
        Task<SubCategory?> UpdateAsync(Guid id, SubCategory model);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Criteria>> GetCriteriasAsync(Guid id);
    }
}