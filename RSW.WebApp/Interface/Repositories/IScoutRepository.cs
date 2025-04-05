using RSW.WebApp.Entities;

namespace RSW.WebApp.Interface.Repositories
{
    public interface IScoutRepository
    {
        Task<List<Scout>> GetAsync();
        Task<Scout> GetAsync(int Id);
        Task Save(Scout scout);
        Task RevertEdits(Scout scout);
        Task Delete(Scout scout);
    }
}
