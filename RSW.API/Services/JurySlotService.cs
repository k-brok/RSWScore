using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class JurySlotService : IJurySlotService
    {
        private readonly AppDbContext _context;

        public JurySlotService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JurySlot>> GetAllAsync()
        {
            return await _context.JurySlots
                .ToListAsync();
        }

        public async Task<JurySlot?> GetByIdAsync(Guid id)
        {
            var entity = await _context.JurySlots.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<JurySlot> CreateAsync(JurySlot model)
        {
            var entity = new JurySlot
            {
                Id = Guid.NewGuid(),
                CategoryId = model.CategoryId,
                ClosingTime = model.ClosingTime,
                OpeningTime = model.OpeningTime,
                SubgroupId = model.SubgroupId
            };

            _context.JurySlots.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<JurySlot?> UpdateAsync(Guid id, JurySlot model)
        {
            var existing = await _context.JurySlots.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.CategoryId = model.CategoryId;
            existing.ClosingTime = model.ClosingTime;
            existing.OpeningTime = model.OpeningTime;
            existing.SubgroupId = model.SubgroupId;

            await _context.SaveChangesAsync();
            return existing;
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