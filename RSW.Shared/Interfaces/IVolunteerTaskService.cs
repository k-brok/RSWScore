using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface IVolunteerTaskService
    {
        Task<IEnumerable<VolunteerTask>> GetAllAsync();
        Task<VolunteerTask?> GetByIdAsync(Guid id);
        Task<VolunteerTask> CreateAsync(VolunteerTask model);
        Task<VolunteerTask?> UpdateAsync(Guid id, VolunteerTask model);
        Task<bool> DeleteAsync(Guid id);
    }
}