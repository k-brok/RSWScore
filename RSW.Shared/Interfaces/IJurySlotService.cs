using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IJurySlotService
    {
        Task<IEnumerable<JurySlotReadDto>> GetAllAsync();
        Task<JurySlotReadDto?> GetByIdAsync(Guid id);
        Task<JurySlotReadDto> CreateAsync(JurySlotCreateDto dto);
        Task<JurySlotReadDto?> UpdateAsync(Guid id, JurySlotUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}