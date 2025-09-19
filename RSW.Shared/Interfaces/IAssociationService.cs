using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IAssociationService
    {
        Task<IEnumerable<AssociationReadDto>> GetAllAsync();
        Task<AssociationReadDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id);
        Task<AssociationReadDto> CreateAsync(AssociationCreateDto dto);
        Task<AssociationReadDto?> UpdateAsync(Guid id, AssociationUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}