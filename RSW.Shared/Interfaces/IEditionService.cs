using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IEditionService
    {
        Task<IEnumerable<EditionDto>> GetAllAsync();
        Task<EditionDto?> GetByIdAsync(Guid id);
        Task<EditionDto?> GetActiveAsync();
        Task<EditionDto> CreateAsync(EditionCreateDto dto);
        Task<EditionDto?> UpdateAsync(Guid id, EditionDto dto);
        Task<EditionDto?> ActivateAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }

}