using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
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