using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class JurySlotService : IJurySlotService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public JurySlotService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JurySlotDto>> GetAllAsync()
        {
            return await _context.JurySlots
                .Select(e => _mapper.Map<JurySlotDto>(e))
                .ToListAsync();
        }

        public async Task<JurySlotDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.JurySlots.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<JurySlotDto>(entity);
        }

        public async Task<JurySlotDto> CreateAsync(JurySlotCreateDto dto)
        {
            var entity = new JurySlot
            {
                Id = Guid.NewGuid(),
                CategoryId = dto.CategoryId,
                ClosingTime = dto.ClosingTime,
                OpeningTime = dto.OpeningTime,
                SubgroupId = dto.SubgroupId
            };

            _context.JurySlots.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<JurySlotDto>(entity);
        }

        public async Task<JurySlotDto?> UpdateAsync(Guid id, JurySlotDto dto)
        {
            var existing = await _context.JurySlots.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.CategoryId = dto.CategoryId;
            existing.ClosingTime = dto.ClosingTime;
            existing.OpeningTime = dto.OpeningTime;
            existing.SubgroupId = dto.SubgroupId;

            await _context.SaveChangesAsync();
            return _mapper.Map<JurySlotDto>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.JurySlots.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.JurySlots.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}