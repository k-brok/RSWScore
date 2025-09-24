using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class VolunteerAssignmentService : IVolunteerAssignmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VolunteerAssignmentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VolunteerAssignment>> GetAllAsync()
        {
            return await _context.VolunteerAssignments
                .Select(e => _mapper.Map<VolunteerAssignment>(e))
                .ToListAsync();
        }

        public async Task<VolunteerAssignment?> GetByIdAsync(Guid id)
        {
            var entity = await _context.VolunteerAssignments.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<VolunteerAssignment>(entity);
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
            return _mapper.Map<VolunteerAssignment>(existing);
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