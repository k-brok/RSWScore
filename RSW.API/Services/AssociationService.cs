using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class AssociationService : IAssociationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AssociationService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssociationReadDto>> GetAllAsync()
        {
            return await _context.Associations
                .Select(e => _mapper.Map<AssociationReadDto>(e))
                .ToListAsync();
        }

        public async Task<AssociationReadDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Associations.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<AssociationReadDto>(entity);
        }

        public async Task<AssociationReadDto> CreateAsync(AssociationCreateDto dto)
        {
            var entity = new Association
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Abbreviation = dto.Abbreviation
            };

            _context.Associations.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<AssociationReadDto>(entity);
        }

        public async Task<AssociationReadDto?> UpdateAsync(Guid id, AssociationUpdateDto dto)
        {
            var existing = await _context.Associations.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.Abbreviation = dto.Abbreviation;

            await _context.SaveChangesAsync();
            return _mapper.Map<AssociationReadDto>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Associations.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Associations.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            return await _context.Groups.Where(g => g.AssociationId == id).Select(g => _mapper.Map<GroupReadDto>(g)).ToListAsync();
        }
    }

}