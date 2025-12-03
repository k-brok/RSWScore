using RSW.Domain.Entities;

namespace RSW.Application.Interfaces
{
    public interface IVolunteerAssignmentService
    {
        Task<IEnumerable<VolunteerAssignment>> GetAllAsync();
        Task<VolunteerAssignment?> GetByIdAsync(Guid id);
        Task<VolunteerAssignment> CreateAsync(VolunteerAssignment model);
        Task<VolunteerAssignment?> UpdateAsync(Guid id, VolunteerAssignment model);
        Task<bool> DeleteAsync(Guid id);
    }
}