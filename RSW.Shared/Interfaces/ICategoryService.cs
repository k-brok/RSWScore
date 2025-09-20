using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(Guid id);
        Task<CategoryDto> CreateAsync(CategoryCreateDto dto);
        Task<CategoryDto?> UpdateAsync(Guid id, CategoryDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}