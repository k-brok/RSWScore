using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface IEmailConfigService
    {
        Task<IEnumerable<EmailConfig>> GetAllAsync();
        Task<EmailConfig?> GetByIdAsync(Guid id);
        Task<EmailConfig?> GetByNameAsync(string name);
        Task<EmailConfig?> UpdateAsync(Guid id, EmailConfig model);
    }

}