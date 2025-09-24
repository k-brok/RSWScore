using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class VolunteerTaskService : IVolunteerTaskService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VolunteerTaskService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VolunteerTask>> GetAllAsync()
        {
            return await _context.VolunteerTasks
                .Select(e => _mapper.Map<VolunteerTask>(e))
                .ToListAsync();
        }

        public async Task<VolunteerTask?> GetByIdAsync(Guid id)
        {
            var entity = await _context.VolunteerTasks.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<VolunteerTask>(entity);
        }

        public async Task<VolunteerTask> CreateAsync(VolunteerTask model)
        {
            _context.VolunteerTasks.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<VolunteerTask?> UpdateAsync(Guid id, VolunteerTask model)
        {
            var existing = await _context.VolunteerTasks.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = model.Name;
            existing.Description = model.Description;

            await _context.SaveChangesAsync();
            return _mapper.Map<VolunteerTask>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.VolunteerTasks.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.VolunteerTasks.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}