using Microsoft.AspNetCore.Identity;
using RSW.Domain.Entities;

namespace RSW.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task<ApplicationUser> CreateAsync(ApplicationUser model, string password);
        Task<ApplicationUser?> UpdateAsync(string id, ApplicationUser model);
        Task<IList<string>> GetRolesAsync(string id);
        Task<IEnumerable<VolunteerAssignment>?> GetVolunteerAssignmentsAsync(string id);
        Task<bool> DeleteAsync(string id);
    }
}