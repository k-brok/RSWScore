using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategoryReadDto>> GetAllAsync();
        Task<SubCategoryReadDto?> GetByIdAsync(Guid id);
        Task<SubCategoryReadDto> CreateAsync(SubCategoryCreateDto dto);
        Task<SubCategoryReadDto?> UpdateAsync(Guid id, SubCategoryUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}