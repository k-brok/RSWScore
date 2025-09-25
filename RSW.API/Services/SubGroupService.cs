using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class SubGroupService : ISubGroupService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SubGroupService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubGroupDto>> GetAllAsync()
        {
            return await _context.SubGroups
                .Select(e => _mapper.Map<SubGroupDto>(e))
                .ToListAsync();
        }

        public async Task<SubGroupDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.SubGroups.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<SubGroupDto>(entity);
        }

        public async Task<SubGroupDto> CreateAsync(SubGroupCreateDto dto)
        {
            var entity = new SubGroup
            {
                Id = Guid.NewGuid(),
                Color = dto.Color,
                EditionId = dto.EditionId
            };

            _context.SubGroups.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<SubGroupDto>(entity);
        }

        public async Task<SubGroupDto?> UpdateAsync(Guid id, SubGroupDto dto)
        {
            var existing = await _context.SubGroups.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Color = dto.Color;
            existing.EditionId = dto.EditionId;

            await _context.SaveChangesAsync();
            return _mapper.Map<SubGroupDto>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.SubGroups.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.SubGroups.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PatrolDto>?> GetPatrolsAsync(Guid id)
        {
            return await _context.Patrols
                .Where(P => P.SubGroupId == id)
                .Select(e => _mapper.Map<PatrolDto>(e))
                .ToListAsync();
        }
    }

}