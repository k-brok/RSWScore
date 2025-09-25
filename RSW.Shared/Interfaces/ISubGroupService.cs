using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface ISubGroupService
    {
        Task<IEnumerable<SubGroupDto>> GetAllAsync();
        Task<SubGroupDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<PatrolDto>?> GetPatrolsAsync(Guid id);
        Task<SubGroupDto> CreateAsync(SubGroupCreateDto dto);
        Task<SubGroupDto?> UpdateAsync(Guid id, SubGroupDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}