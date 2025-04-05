using RSW.WebApp.Entities;

namespace RSW.WebApp.Interface.Repositories
{
    public interface IEditionRepository
    {
        Task<List<Edition>> GetAllAsync();
        Task<Edition> GetByYearAsync(int year);
        Task<List<int>> GetListYears();
        Task Save(Edition edition);
        Task<Edition> GetActive();
        Task Activate(Edition edition);
        Task RevertEdits(Edition edition);
        Task Delete(Edition edition);
    }
}
