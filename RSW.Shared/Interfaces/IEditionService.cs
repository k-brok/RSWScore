using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IEditionService
    {
        Task<IEnumerable<EditionReadDto>> GetAllAsync();
        Task<EditionReadDto?> GetByIdAsync(Guid id);
        Task<EditionReadDto?> GetActiveAsync();
        Task<EditionReadDto> CreateAsync(EditionCreateDto dto);
        Task<EditionReadDto?> UpdateAsync(Guid id, EditionUpdateDto dto);
        Task<EditionReadDto?> ActivateAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }

}