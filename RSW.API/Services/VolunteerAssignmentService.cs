using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class VolunteerAssignmentService : IVolunteerAssignmentService
    {
        private readonly AppDbContext _context;

        public VolunteerAssignmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VolunteerAssignment>> GetAllAsync()
        {
            return await _context.VolunteerAssignments
                .ToListAsync();
        }

        public async Task<VolunteerAssignment?> GetByIdAsync(Guid id)
        {
            var entity = await _context.VolunteerAssignments.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<VolunteerAssignment> CreateAsync(VolunteerAssignment model)
        {
            _context.VolunteerAssignments.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<VolunteerAssignment?> UpdateAsync(Guid id, VolunteerAssignment model)
        {
            var existing = await _context.VolunteerAssignments.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.EditionId = model.EditionId;
            existing.TaskId = model.TaskId;
            existing.UserId = model.UserId;

            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.VolunteerAssignments.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.VolunteerAssignments.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}