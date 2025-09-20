using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategoryDto>> GetAllAsync();
        Task<SubCategoryDto?> GetByIdAsync(Guid id);
        Task<SubCategoryDto> CreateAsync(SubCategoryCreateDto dto);
        Task<SubCategoryDto?> UpdateAsync(Guid id, SubCategoryDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}