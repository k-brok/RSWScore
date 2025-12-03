using RSW.Domain.Entities;

namespace RSW.Application.Interfaces
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