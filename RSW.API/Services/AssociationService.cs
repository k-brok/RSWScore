using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class AssociationService : IAssociationService
    {
        private readonly AppDbContext _context;

        public AssociationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Association>> GetAllAsync()
        {
            return await _context.Associations
                .ToListAsync();
        }

        public async Task<Association?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Associations.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<Association> CreateAsync(Association model)
        {
            var entity = new Association
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Abbreviation = model.Abbreviation
            };

            _context.Associations.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Association?> UpdateAsync(Guid id, Association model)
        {
            var existing = await _context.Associations.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = model.Name;
            existing.Abbreviation = model.Abbreviation;

            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Associations.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Associations.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Unit>> GetUnitsAsync(Guid id)
        {
            return await _context.Units.Where(g => g.AssociationId == id).ToListAsync();
        }
    }

}