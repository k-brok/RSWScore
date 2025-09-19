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

        public async Task<IEnumerable<JurySlotReadDto>> GetAllAsync()
        {
            return await _context.JurySlots
                .Select(e => _mapper.Map<JurySlotReadDto>(e))
                .ToListAsync();
        }

        public async Task<JurySlotReadDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.JurySlots.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<JurySlotReadDto>(entity);
        }

        public async Task<JurySlotReadDto> CreateAsync(JurySlotCreateDto dto)
        {
            var entity = new JurySlot
            {
                Id = Guid.NewGuid(),
                CategoryId = dto.CategoryId,
                ClosingTime = dto.ClosingTime,
                Code = dto.Code,
                EditionId = dto.EditionId,
                OpeningTime = dto.OpeningTime,
                SubgroupId = dto.SubgroupId
            };

            _context.JurySlots.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<JurySlotReadDto>(entity);
        }

        public async Task<JurySlotReadDto?> UpdateAsync(Guid id, JurySlotUpdateDto dto)
        {
            var existing = await _context.JurySlots.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.CategoryId = dto.CategoryId;
            existing.ClosingTime = dto.ClosingTime;
            existing.Code = dto.Code;
            existing.EditionId = dto.EditionId;
            existing.OpeningTime = dto.OpeningTime;
            existing.SubgroupId = dto.SubgroupId;

            await _context.SaveChangesAsync();
            return _mapper.Map<JurySlotReadDto>(existing);
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