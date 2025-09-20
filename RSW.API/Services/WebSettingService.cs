using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
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

        public async Task<IEnumerable<WebSettingDto>> GetAllAsync()
        {
            return await _context.WebSettings
                .Select(e => _mapper.Map<WebSettingDto>(e))
                .ToListAsync();
        }

        public async Task<WebSettingDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.WebSettings.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<WebSettingDto>(entity);
        }

        public async Task<WebSettingDto> CreateAsync(WebSettingCreateDto dto)
        {
            var entity = new WebSetting
            {
                Id = Guid.NewGuid(),
                Category = dto.Category,
                Description = dto.Description,
                Key = dto.Key,
                Value = dto.Value,
                ValueType = dto.ValueType
            };

            _context.WebSettings.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<WebSettingDto>(entity);
        }

        public async Task<WebSettingDto?> UpdateAsync(Guid id, WebSettingDto dto)
        {
            var existing = await _context.WebSettings.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Category = dto.Category;
            existing.Description = dto.Description;
            existing.Key = dto.Key;
            existing.Value = dto.Value;
            existing.ValueType = dto.ValueType;

            await _context.SaveChangesAsync();
            return _mapper.Map<WebSettingDto>(existing);
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