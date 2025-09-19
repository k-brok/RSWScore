using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface ISubGroupService
    {
        Task<IEnumerable<SubGroupReadDto>> GetAllAsync();
        Task<SubGroupReadDto?> GetByIdAsync(Guid id);
        Task<SubGroupReadDto> CreateAsync(SubGroupCreateDto dto);
        Task<SubGroupReadDto?> UpdateAsync(Guid id, SubGroupUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}