using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface IJurySlotService
    {
        Task<IEnumerable<JurySlot>> GetAllAsync();
        Task<JurySlot?> GetByIdAsync(Guid id);
        Task<JurySlot> CreateAsync(JurySlot model);
        Task<JurySlot?> UpdateAsync(Guid id, JurySlot model);
        Task<bool> DeleteAsync(Guid id);
    }
}