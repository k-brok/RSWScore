using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
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

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Select(e => _mapper.Map<Category>(e))
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Categories.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<Category>(entity);
        }

        public async Task<Category> CreateAsync(Category model)
        {
            var entity = new Category
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Weight = model.Weight
            };

            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Category>(entity);
        }

        public async Task<Category?> UpdateAsync(Guid id, Category model)
        {
            var existing = await _context.Categories.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = model.Name;
            existing.Weight = model.Weight;

            await _context.SaveChangesAsync();
            return _mapper.Map<Category>(existing);
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