using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface ICriteriaService
    {
        Task<IEnumerable<CriteriaReadDto>> GetAllAsync();
        Task<CriteriaReadDto?> GetByIdAsync(Guid id);
        Task<CriteriaReadDto> CreateAsync(CriteriaCreateDto dto);
        Task<CriteriaReadDto?> UpdateAsync(Guid id, CriteriaUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}