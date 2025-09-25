using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category> CreateAsync(Category model);
        Task<Category?> UpdateAsync(Guid id, Category dto);
        Task<bool> DeleteAsync(Guid id);
    }
}