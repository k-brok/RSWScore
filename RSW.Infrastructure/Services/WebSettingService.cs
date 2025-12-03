
using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.Infrastructure.Services
{
    public class WebSettingService : IWebSettingService
    {
        private readonly AppDbContext _context;

        public WebSettingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WebSetting>> GetAllAsync()
        {
            return await _context.WebSettings
                .ToListAsync();
        }

        public async Task<WebSetting?> GetByIdAsync(Guid id)
        {
            var entity = await _context.WebSettings.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
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

            return entity;
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
            return existing;
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