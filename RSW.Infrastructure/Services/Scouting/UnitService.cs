
using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.Infrastructure.Services
{
    public class UnitService : IUnitService
    {
        private readonly AppDbContext _context;

        public UnitService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Unit>> GetAllAsync()
        {
            return await _context.Units
                .Include(u => u.Association)
                .Select(u => new Unit
                {
                    Id = u.Id,
                    Name = u.Name,
                    AssociationId = u.AssociationId,
                    AssociationName = u.Association.Name, // hier vullen we 'm
                    Association = u.Association,
                    Patrols = u.Patrols
                })
                .ToListAsync();
        }

        public async Task<Unit?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Units
                .Include(u => u.Association)
                .Select(u => new Unit
                {
                    Id = u.Id,
                    Name = u.Name,
                    AssociationId = u.AssociationId,
                    AssociationName = u.Association.Name, // hier vullen we 'm
                    Association = u.Association,
                    Patrols = u.Patrols
                })
                .FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<Unit> CreateAsync(Unit model)
        {
            var entity = new Unit
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                AssociationId = model.AssociationId
            };

            _context.Units.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Unit?> UpdateAsync(Guid id, Unit model)
        {
            var existing = await _context.Units.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = model.Name;
            existing.AssociationId = model.AssociationId;

            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Units.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Units.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Patrol>>? GetPatrolsAsync(Guid id)
        {
            var existing = await _context.Units.FirstOrDefaultAsync(e => e.Id == id);
                if (existing == null) return null;

            return await _context.Patrols.Where(p => p.UnitId == id).ToListAsync();
        }
    }

}