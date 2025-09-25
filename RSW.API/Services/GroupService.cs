using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class GroupService : IGroupService
    {
        private readonly AppDbContext _context;

        public GroupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups
                .ToListAsync();
        }

        public async Task<Group?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Groups.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<Group> CreateAsync(Group model)
        {
            var entity = new Group
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                AssociationId = model.AssociationId
            };

            _context.Groups.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Group?> UpdateAsync(Guid id, Group model)
        {
            var existing = await _context.Groups.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = model.Name;
            existing.AssociationId = model.AssociationId;

            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Groups.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Groups.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Patrol>>? GetPatrolsAsync(Guid id)
        {
            var existing = await _context.Groups.FirstOrDefaultAsync(e => e.Id == id);
                if (existing == null) return null;

            return await _context.Patrols.Where(p => p.GroupId == id).ToListAsync();
        }
    }

}