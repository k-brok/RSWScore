using RSW.WebApp.Entities;

namespace RSW.WebApp.Interface.Repositories
{
    public interface IAssociationRepository
    {
        Task<List<Association>> GetAllAsync();
        Task<Association> GetAsync(int Id);
        Task<Association> GetByNameAsync(string name);
        Task<Association> GetByAbbAsync(string abbreviation);
        Task RevertEdits(Association asssocitation);
        Task Save(Association association);
        Task Delete(Association association);
    }
}
