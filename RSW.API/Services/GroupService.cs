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

        public async Task<IEnumerable<GroupReadDto>> GetAllAsync()
        {
            return await _context.Groups
                .Select(e => _mapper.Map<GroupReadDto>(e))
                .ToListAsync();
        }

        public async Task<GroupReadDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Groups.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<GroupReadDto>(entity);
        }

        public async Task<GroupReadDto> CreateAsync(GroupCreateDto dto)
        {
            var entity = new Group
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                AssociationId = dto.AssociationId
            };

            _context.Groups.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<GroupReadDto>(entity);
        }

        public async Task<GroupReadDto?> UpdateAsync(Guid id, GroupUpdateDto dto)
        {
            var existing = await _context.Groups.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.AssociationId = dto.AssociationId;

            await _context.SaveChangesAsync();
            return _mapper.Map<GroupReadDto>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Groups.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Groups.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}