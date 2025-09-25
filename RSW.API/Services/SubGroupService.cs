using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class SubGroupService : ISubGroupService
    {
        private readonly AppDbContext _context;

        public SubGroupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubGroup>> GetAllAsync()
        {
            return await _context.SubGroups
                .ToListAsync();
        }

        public async Task<SubGroup?> GetByIdAsync(Guid id)
        {
            var entity = await _context.SubGroups.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<SubGroup> CreateAsync(SubGroup model)
        {
            var entity = new SubGroup
            {
                Id = Guid.NewGuid(),
                Color = model.Color,
                EditionId = model.EditionId
            };

            _context.SubGroups.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<SubGroup?> UpdateAsync(Guid id, SubGroup model)
        {
            var existing = await _context.SubGroups.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Color = model.Color;
            existing.EditionId = model.EditionId;

            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.SubGroups.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.SubGroups.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Patrol>?> GetPatrolsAsync(Guid id)
        {
            return await _context.Patrols
                .Where(P => P.SubGroupId == id)
                .ToListAsync();
        }
    }

}