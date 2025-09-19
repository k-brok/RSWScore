using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SubCategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubCategoryReadDto>> GetAllAsync()
        {
            return await _context.SubCategories
                .Select(e => _mapper.Map<SubCategoryReadDto>(e))
                .ToListAsync();
        }

        public async Task<SubCategoryReadDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.SubCategories.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<SubCategoryReadDto>(entity);
        }

        public async Task<SubCategoryReadDto> CreateAsync(SubCategoryCreateDto dto)
        {
            var entity = new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                CategoryId = dto.CategoryId
            };

            _context.SubCategories.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<SubCategoryReadDto>(entity);
        }

        public async Task<SubCategoryReadDto?> UpdateAsync(Guid id, SubCategoryUpdateDto dto)
        {
            var existing = await _context.SubCategories.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.CategoryId = dto.CategoryId;
            
            await _context.SaveChangesAsync();
            return _mapper.Map<SubCategoryReadDto>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.SubCategories.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.SubCategories.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}