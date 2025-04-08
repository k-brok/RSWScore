using RSW.WebApp.Entities;

namespace RSW.WebApp.Repositories
{
    public interface ICriteriaRepository
    {
        Task<List<Criteria>> GetAllAsync();
        Task Save(Criteria criteria);
        Task RevertEdits(Criteria criteria);
        Task Delete(Criteria criteria);
    }
}
