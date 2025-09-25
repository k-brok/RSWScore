using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class PatrolService : IPatrolService
    {
        private readonly AppDbContext _context;

        public PatrolService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patrol>> GetAllAsync()
        {
            return await _context.Patrols
                .ToListAsync();
        }

        public async Task<Patrol?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Patrols.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<IEnumerable<Scout>> GetScoutsAsync(Guid patrolId)
        {
            return await _context.Scouts
                .Where(s => s.PatrolId == patrolId)
                .Select(s => new Scout
                {
                    Id = s.Id,
                    Firstname = s.Firstname,
                    Lastname = s.Lastname,
                    DateOfBirth = s.DateOfBirth,
                    PatrolId = s.PatrolId,
                }).ToListAsync();
        }

        public async Task<Patrol> CreateAsync(Patrol model)
        {
            var entity = new Patrol
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                UnitId = model.UnitId,
                IsYoungest = model.IsYoungest,
            };

            _context.Patrols.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Patrol?> UpdateAsync(Guid id, Patrol model)
        {
            var existing = await _context.Patrols.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = model.Name;
            existing.UnitId = model.UnitId;
            existing.IsYoungest = model.IsYoungest;
            existing.Number = model.Number;
            existing.SubGroupId = model.SubGroupId;

            await _context.SaveChangesAsync();
            return existing;
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