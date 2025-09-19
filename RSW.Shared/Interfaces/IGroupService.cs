using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupReadDto>> GetAllAsync();
        Task<GroupReadDto?> GetByIdAsync(Guid id);
        Task<GroupReadDto> CreateAsync(GroupCreateDto dto);
        Task<GroupReadDto?> UpdateAsync(Guid id, GroupUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}