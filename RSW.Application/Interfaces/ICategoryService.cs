using RSW.Domain.Entities;

namespace RSW.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<SubCategory>> GetSubCategorysAsync(Guid id);
        Task<Category> CreateAsync(Category model);
        Task<Category?> UpdateAsync(Guid id, Category model);
        Task<bool> DeleteAsync(Guid id);
    }
}