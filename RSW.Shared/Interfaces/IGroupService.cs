using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetAllAsync();
        Task<GroupDto?> GetByIdAsync(Guid id);
        Task<GroupDto> CreateAsync(GroupCreateDto dto);
        Task<GroupDto?> UpdateAsync(Guid id, GroupDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<List<PatrolDto>>? GetPatrolsAsync(Guid id);
    }
}