using RSW.Domain.Entities;

namespace RSW.Application.Interfaces
{
    public interface IScoutService
    {
        Task<IEnumerable<Scout>> GetAllAsync();
        Task<Scout?> GetByIdAsync(Guid id);
        Task<Scout> CreateAsync(Scout model);
        Task<Scout?> UpdateAsync(Guid id, Scout model);
        Task<bool> DeleteAsync(Guid id);
    }
}