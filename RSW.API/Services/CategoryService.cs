using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllAsync()
        {
            return await _context.Categories
                .Select(e => _mapper.Map<CategoryReadDto>(e))
                .ToListAsync();
        }

        public async Task<CategoryReadDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Categories.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<CategoryReadDto>(entity);
        }

        public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
        {
            var entity = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Weight = dto.Weight
            };

            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryReadDto>(entity);
        }

        public async Task<CategoryReadDto?> UpdateAsync(Guid id, CategoryUpdateDto dto)
        {
            var existing = await _context.Categories.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.Weight = dto.Weight;

            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryReadDto>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Categories.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Categories.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}