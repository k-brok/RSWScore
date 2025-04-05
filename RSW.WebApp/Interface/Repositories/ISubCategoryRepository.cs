using RSW.WebApp.Entities;

namespace RSW.WebApp.Repositories
{
    public interface ISubCategoryRepository
    {
        Task<List<SubCategory>> GetAllAsync();
        Task<SubCategory> GetByNameAsync(string name);
        Task Save(SubCategory subcategory);
        Task RevertEdits(SubCategory subcategory);
        Task Delete(SubCategory subcategory);
    }
}
