
using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.Infrastructure.Services
{
    public class CriteriaService : ICriteriaService
    {
        private readonly AppDbContext _context;

        public CriteriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Criteria>> GetAllAsync()
        {
            return await _context.Criterias
                .ToListAsync();
        }

        public async Task<Criteria?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Criterias.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<Criteria> CreateAsync(Criteria model)
        {
            var entity = new Criteria
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                MaxScore = model.MaxScore,
                SubCategoryId = model.SubCategoryId
            };

            _context.Criterias.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Criteria?> UpdateAsync(Guid id, Criteria model)
        {
            var existing = await _context.Criterias.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = model.Name;
            existing.Description = model.Description;
            existing.MaxScore = model.MaxScore;
            existing.SubCategoryId = model.SubCategoryId;

            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Criterias.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Criterias.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}