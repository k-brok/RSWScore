using RSW.Shared.Entities;

namespace RSW.WebApp.Interface.Repositories
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetAllAsync();
        Task<Group> GetByNameAsync(string name);
        Task<Group> GetAsync(Guid Id);
        Task<Group> GetByNameAsync(string name, Association association);
        Task Save(Group group);
        Task RevertEdits(Group group);
        Task Delete(Group group);
    }
}
