
using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.Infrastructure.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly AppDbContext _context;

        public SubCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories
                .ToListAsync();
        }

        public async Task<SubCategory?> GetByIdAsync(Guid id)
        {
            var entity = await _context.SubCategories.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
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

            return entity;
        }

        public async Task<SubCategory?> UpdateAsync(Guid id, SubCategory model)
        {
            var existing = await _context.SubCategories.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = model.Name;
            existing.CategoryId = model.CategoryId;
            
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.SubCategories.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.SubCategories.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Criteria>> GetCriteriasAsync(Guid id)
        {
            return await _context.Criterias.Where(C => C.SubCategoryId == id).ToListAsync();
        }
    }

}