using RSW.Domain.Entities;

namespace RSW.Application.Interfaces
{
    public interface IJurySlotService
    {
        Task<IEnumerable<JurySlot>> GetAllAsync();
        Task<JurySlot?> GetByIdAsync(Guid id);
        Task<JurySlot?> CreateAsync(JurySlot model);
        Task<JurySlot?> UpdateAsync(Guid id, JurySlot model);
        Task<bool> DeleteAsync(Guid id);
    }
}