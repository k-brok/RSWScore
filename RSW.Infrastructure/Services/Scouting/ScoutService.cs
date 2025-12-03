
using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.Infrastructure.Services
{
    public class ScoutService : IScoutService
    {
        private readonly AppDbContext _context;

        public ScoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Scout>> GetAllAsync()
        {
            return await _context.Scouts
                .ToListAsync();
        }

        public async Task<Scout?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Scouts.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<Scout> CreateAsync(Scout model)
        {
            var entity = new Scout
            {
                Id = Guid.NewGuid(),
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                PatrolId = model.PatrolId,
                DateOfBirth = model.DateOfBirth
            };

            _context.Scouts.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Scout?> UpdateAsync(Guid id, Scout model)
        {
            var existing = await _context.Scouts.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Firstname = model.Firstname;
            existing.Lastname = model.Lastname;
            existing.IsAPL = model.IsAPL;
            existing.IsPL = model.IsPL;
            existing.PatrolId = model.PatrolId;
            existing.DateOfBirth = model.DateOfBirth;

            await _context.SaveChangesAsync();
            return existing;
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