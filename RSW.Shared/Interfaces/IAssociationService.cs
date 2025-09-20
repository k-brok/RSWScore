using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IAssociationService
    {
        Task<IEnumerable<AssociationDto>> GetAllAsync();
        Task<AssociationDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<GroupDto>> GetGroupsAsync(Guid id);
        Task<AssociationDto> CreateAsync(AssociationCreateDto dto);
        Task<AssociationDto?> UpdateAsync(Guid id, AssociationDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}