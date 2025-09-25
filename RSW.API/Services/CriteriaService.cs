using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class CriteriaService : ICriteriaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CriteriaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Criteria>> GetAllAsync()
        {
            return await _context.Criterias
                .Select(e => _mapper.Map<Criteria>(e))
                .ToListAsync();
        }

        public async Task<Criteria?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Criterias.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<Criteria>(entity);
        }

        public async Task<Criteria> CreateAsync(Criteria model)
        {
            var entity = new Criteria
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                MaxScore = model.MaxScore,
                SubCategoryId = model.SubCategoryId
            };

            _context.Criterias.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Criteria>(entity);
        }

        public async Task<Criteria?> UpdateAsync(Guid id, Criteria model)
        {
            var existing = await _context.Criterias.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = model.Name;
            existing.MaxScore = model.MaxScore;
            existing.SubCategoryId = model.SubCategoryId;

            await _context.SaveChangesAsync();
            return _mapper.Map<Criteria>(existing);
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