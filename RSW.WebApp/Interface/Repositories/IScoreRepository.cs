using RSW.WebApp.Entities;

namespace RSW.WebApp.Interface.Repositories
{
    public interface IScoreRepository
    {
        Task<List<Score>> GetAllAsync();
        Task<Score> GetAsync(Patrol patrol, Criteria criteria);
        Task<List<Score>> GetAsync(Patrol patrol);
        Task<List<Score>> GetAsync(Patrol patrol, Category category);
        Task Save(Score score);
        Task<Score> GetAsync(int Id);
        Task RevertEdits(Score score);
        Task Delete(Score score);
    }
}
