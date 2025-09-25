using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
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

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories
                .Select(e => _mapper.Map<SubCategory>(e))
                .ToListAsync();
        }

        public async Task<SubCategory?> GetByIdAsync(Guid id)
        {
            var entity = await _context.SubCategories.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<SubCategory>(entity);
        }

        public async Task<SubCategory> CreateAsync(SubCategory model)
        {
            var entity = new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                CategoryId = model.CategoryId
            };

            _context.SubCategories.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<SubCategory>(entity);
        }

        public async Task<SubCategory?> UpdateAsync(Guid id, SubCategory model)
        {
            var existing = await _context.SubCategories.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = model.Name;
            existing.CategoryId = model.CategoryId;
            
            await _context.SaveChangesAsync();
            return _mapper.Map<SubCategory>(existing);
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