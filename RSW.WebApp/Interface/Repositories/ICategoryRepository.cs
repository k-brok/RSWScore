using RSW.WebApp.Entities;

namespace RSW.WebApp.Interface.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByNameAsync(string name);
        Task Save(Category category);
        Task RevertEdits(Category category);
        Task Delete(Category category);
    }
}
