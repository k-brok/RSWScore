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

        public async Task<IEnumerable<WebSettingReadDto>> GetAllAsync()
        {
            return await _context.WebSettings
                .Select(e => _mapper.Map<WebSettingReadDto>(e))
                .ToListAsync();
        }

        public async Task<WebSettingReadDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.WebSettings.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<WebSettingReadDto>(entity);
        }

        public async Task<WebSettingReadDto> CreateAsync(WebSettingCreateDto dto)
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

            return _mapper.Map<WebSettingReadDto>(entity);
        }

        public async Task<WebSettingReadDto?> UpdateAsync(Guid id, WebSettingUpdateDto dto)
        {
            var existing = await _context.WebSettings.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.Category = dto.Category;
            existing.Description = dto.Description;
            existing.Key = dto.Key;
            existing.Value = dto.Value;
            existing.ValueType = dto.ValueType;

            await _context.SaveChangesAsync();
            return _mapper.Map<WebSettingReadDto>(existing);
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