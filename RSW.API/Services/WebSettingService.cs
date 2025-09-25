using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class WebSettingService : IWebSettingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public WebSettingService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WebSetting>> GetAllAsync()
        {
            return await _context.WebSettings
                .Select(e => _mapper.Map<WebSetting>(e))
                .ToListAsync();
        }

        public async Task<WebSetting?> GetByIdAsync(Guid id)
        {
            var entity = await _context.WebSettings.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<WebSetting>(entity);
        }

        public async Task<WebSetting> CreateAsync(WebSetting model)
        {
            var entity = new WebSetting
            {
                Id = Guid.NewGuid(),
                Category = model.Category,
                Description = model.Description,
                Key = model.Key,
                Value = model.Value,
                ValueType = model.ValueType
            };

            _context.WebSettings.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<WebSetting>(entity);
        }

        public async Task<WebSetting?> UpdateAsync(Guid id, WebSetting model)
        {
            var existing = await _context.WebSettings.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Category = model.Category;
            existing.Description = model.Description;
            existing.Key = model.Key;
            existing.Value = model.Value;
            existing.ValueType = model.ValueType;

            await _context.SaveChangesAsync();
            return _mapper.Map<WebSetting>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.WebSettings.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.WebSettings.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}