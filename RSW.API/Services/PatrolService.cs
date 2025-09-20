using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class PatrolService : IPatrolService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PatrolService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatrolDto>> GetAllAsync()
        {
            return await _context.Patrols
                .Select(e => _mapper.Map<PatrolDto>(e))
                .ToListAsync();
        }

        public async Task<PatrolDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Patrols.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<PatrolDto>(entity);
        }

        public async Task<PatrolDto> CreateAsync(PatrolCreateDto dto)
        {
            var entity = new Patrol
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                GroupId = dto.GroupId,
                IsYoungest = dto.IsYoungest,
            };

            _context.Patrols.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<PatrolDto>(entity);
        }

        public async Task<PatrolDto?> UpdateAsync(Guid id, PatrolDto dto)
        {
            var existing = await _context.Patrols.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.GroupId = dto.GroupId;
            existing.IsYoungest = dto.IsYoungest;
            existing.Number = dto.Number;
            existing.SubGroupId = dto.SubGroupId;

            await _context.SaveChangesAsync();
            return _mapper.Map<PatrolDto>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Patrols.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Patrols.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}