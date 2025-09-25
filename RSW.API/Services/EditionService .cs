using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
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

        public async Task<IEnumerable<Edition>> GetAllAsync()
        {
            return await _context.Editions
                .Select(e => _mapper.Map<Edition>(e))
                .ToListAsync();
        }

        public async Task<Edition?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Editions.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<Edition>(entity);
        }

        public async Task<Edition?> GetActiveAsync()
        {
            var entity = await _context.Editions.FirstOrDefaultAsync(e => e.IsActive);
            if (entity == null)
                return null;
            
            return new Edition
            {
                Id = entity.Id,
                RSWStartDate = entity.RSWStartDate,
                LSWStartDate = entity.LSWStartDate,
                Theme = entity.Theme,
                IsActive = entity.IsActive,
                PreSignupClose = entity.PreSignupClose
            };
        }

        public async Task<Edition> CreateAsync(Edition model)
        {
            var entity = new Edition
            {
                Id = Guid.NewGuid(),
                RSWStartDate = model.RSWStartDate,
                LSWStartDate = model.LSWStartDate,
                Theme = model.Theme,
                IsActive = false
            };

            _context.Editions.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Edition>(entity);
        }

        public async Task<Edition?> UpdateAsync(Guid id, Edition model)
        {
            var existing = await _context.Editions.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.RSWStartDate = model.RSWStartDate;
            existing.LSWStartDate = model.LSWStartDate;
            existing.Theme = model.Theme;
            existing.PreSignupClose = model.PreSignupClose;

            await _context.SaveChangesAsync();
            return _mapper.Map<Edition>(existing);
        }

        public async Task<Edition?> ActivateAsync(Guid id)
        {
            var newActive = await _context.Editions.FirstOrDefaultAsync(e => e.Id == id);
            if (newActive == null) return null;

            var currentActive = await _context.Editions.Where(e => e.IsActive).ToListAsync();
            foreach (var edition in currentActive)
                edition.IsActive = false;

            newActive.IsActive = true;
            await _context.SaveChangesAsync();

            return new Edition
            {
                Id = newActive.Id,
                RSWStartDate = newActive.RSWStartDate,
                LSWStartDate = newActive.LSWStartDate,
                Theme = newActive.Theme,
                IsActive = newActive.IsActive,
                PreSignupClose = newActive.PreSignupClose
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