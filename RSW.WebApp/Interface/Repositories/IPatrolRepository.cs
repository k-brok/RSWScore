using RSW.WebApp.Entities;

namespace RSW.WebApp.Interface.Repositories
{
    public interface IPatrolRepository
    {
        Task<List<Patrol>> GetAsync();
        Task<List<Patrol>> GetByGroupIdAsync(int GroupId);
        Task<List<Patrol>> GetAsync(List<SubGroup> subGroups);
        Task<Patrol> GetAsync(int Id);
        Task Save(Patrol patrol);
        Task RevertEdits(Patrol patrol);
        Task RevertEdits(Scout scout);
        Task Delete(Patrol patrol);
        Task Delete(Scout scout);
    }
}
