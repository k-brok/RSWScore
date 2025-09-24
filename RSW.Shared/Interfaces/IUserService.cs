using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task<ApplicationUser> CreateAsync(ApplicationUser model, string password);
        Task<ApplicationUser?> UpdateAsync(string id, ApplicationUser model);
        Task<bool> DeleteAsync(string id);
    }
}