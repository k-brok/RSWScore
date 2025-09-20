using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class ScoutService : IScoutService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ScoutService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScoutDto>> GetAllAsync()
        {
            return await _context.Scouts
                .Select(e => _mapper.Map<ScoutDto>(e))
                .ToListAsync();
        }

        public async Task<ScoutDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Scouts.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<ScoutDto>(entity);
        }

        public async Task<ScoutDto> CreateAsync(ScoutCreateDto dto)
        {
            var entity = new Scout
            {
                Id = Guid.NewGuid(),
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                PatrolId = dto.PatrolId,
                DateOfBirth = dto.DateOfBirth
            };

            _context.Scouts.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ScoutDto>(entity);
        }

        public async Task<ScoutDto?> UpdateAsync(Guid id, ScoutDto dto)
        {
            var existing = await _context.Scouts.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Firstname = dto.Firstname;
            existing.Lastname = dto.Lastname;
            existing.IsAPL = dto.IsAPL;
            existing.IsPL = dto.IsPL;
            existing.PatrolId = dto.PatrolId;
            existing.DateOfBirth = dto.DateOfBirth;

            await _context.SaveChangesAsync();
            return _mapper.Map<ScoutDto>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Scouts.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Scouts.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}