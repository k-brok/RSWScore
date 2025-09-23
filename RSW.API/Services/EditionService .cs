using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class EditionService : IEditionService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EditionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EditionDto>> GetAllAsync()
        {
            return await _context.Editions
                .Select(e => _mapper.Map<EditionDto>(e))
                .ToListAsync();
        }

        public async Task<EditionDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Editions.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<EditionDto>(entity);
        }

        public async Task<EditionDto?> GetActiveAsync()
        {
            var entity = await _context.Editions.FirstOrDefaultAsync(e => e.IsActive);
            return _mapper.Map<EditionDto>(entity);
        }

        public async Task<EditionDto> CreateAsync(EditionCreateDto dto)
        {
            var entity = new Edition
            {
                Id = Guid.NewGuid(),
                RSWStartDate = dto.RSWStartDate,
                LSWStartDate = dto.LSWStartDate,
                Theme = dto.Theme,
                IsActive = false
            };

            _context.Editions.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<EditionDto>(entity);
        }

        public async Task<EditionDto?> UpdateAsync(Guid id, EditionDto dto)
        {
            var existing = await _context.Editions.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.RSWStartDate = dto.RSWStartDate;
            existing.LSWStartDate = dto.LSWStartDate;
            existing.Theme = dto.Theme;

            await _context.SaveChangesAsync();
            return _mapper.Map<EditionDto>(existing);
        }

        public async Task<EditionDto?> ActivateAsync(Guid id)
        {
            var newActive = await _context.Editions.FirstOrDefaultAsync(e => e.Id == id);
            if (newActive == null) return null;

            var currentActive = await _context.Editions.Where(e => e.IsActive).ToListAsync();
            foreach (var edition in currentActive)
                edition.IsActive = false;

            newActive.IsActive = true;
            await _context.SaveChangesAsync();

            return new EditionDto
            {
                Id = newActive.Id,
                RSWStartDate = newActive.RSWStartDate,
                LSWStartDate = newActive.LSWStartDate,
                Theme = newActive.Theme,
                IsActive = newActive.IsActive
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Editions.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Editions.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}