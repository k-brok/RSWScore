using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface ICriteriaService
    {
        Task<IEnumerable<CriteriaDto>> GetAllAsync();
        Task<CriteriaDto?> GetByIdAsync(Guid id);
        Task<CriteriaDto> CreateAsync(CriteriaCreateDto dto);
        Task<CriteriaDto?> UpdateAsync(Guid id, CriteriaDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}