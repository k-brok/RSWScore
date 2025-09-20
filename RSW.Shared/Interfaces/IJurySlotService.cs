using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IJurySlotService
    {
        Task<IEnumerable<JurySlotDto>> GetAllAsync();
        Task<JurySlotDto?> GetByIdAsync(Guid id);
        Task<JurySlotDto> CreateAsync(JurySlotCreateDto dto);
        Task<JurySlotDto?> UpdateAsync(Guid id, JurySlotDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}