using RSW.WebApp.Entities;

namespace RSW.WebApp.Repositories
{
    public interface ISubGroupRepository
    {
        Task<List<SubGroup>> GetAllAsync();
        Task Save(SubGroup subgroup);
        Task RevertEdits(SubGroup subgroup);
        Task Delete(SubGroup subgroup);
    }
}
