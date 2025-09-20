using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class GroupService : IGroupService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GroupService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GroupDto>> GetAllAsync()
        {
            return await _context.Groups
                .Select(e => _mapper.Map<GroupDto>(e))
                .ToListAsync();
        }

        public async Task<GroupDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Groups.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<GroupDto>(entity);
        }

        public async Task<GroupDto> CreateAsync(GroupCreateDto dto)
        {
            var entity = new Group
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                AssociationId = dto.AssociationId
            };

            _context.Groups.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<GroupDto>(entity);
        }

        public async Task<GroupDto?> UpdateAsync(Guid id, GroupDto dto)
        {
            var existing = await _context.Groups.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.AssociationId = dto.AssociationId;

            await _context.SaveChangesAsync();
            return _mapper.Map<GroupDto>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Groups.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Groups.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PatrolDto>>? GetPatrolsAsync(Guid id)
        {
            var existing = await _context.Groups.FirstOrDefaultAsync(e => e.Id == id);
                if (existing == null) return null;

            var patrols = await _context.Patrols.Where(p => p.GroupId == id).ToListAsync();
            return patrols.Select(p => _mapper.Map<PatrolDto>(p)).ToList();
        }
    }

}