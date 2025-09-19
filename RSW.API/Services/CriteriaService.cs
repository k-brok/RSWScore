using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
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

        public async Task<IEnumerable<CriteriaReadDto>> GetAllAsync()
        {
            return await _context.Criterias
                .Select(e => _mapper.Map<CriteriaReadDto>(e))
                .ToListAsync();
        }

        public async Task<CriteriaReadDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Criterias.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<CriteriaReadDto>(entity);
        }

        public async Task<CriteriaReadDto> CreateAsync(CriteriaCreateDto dto)
        {
            var entity = new Criteria
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                MaxScore = dto.MaxScore,
                SubCategoryId = dto.SubCategoryId
            };

            _context.Criterias.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<CriteriaReadDto>(entity);
        }

        public async Task<CriteriaReadDto?> UpdateAsync(Guid id, CriteriaUpdateDto dto)
        {
            var existing = await _context.Criterias.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.MaxScore = dto.MaxScore;
            existing.SubCategoryId = dto.SubCategoryId;

            await _context.SaveChangesAsync();
            return _mapper.Map<CriteriaReadDto>(existing);
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