using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class EmailConfigService : IEmailConfigService
    {
        private readonly AppDbContext _context;

        public EmailConfigService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmailConfig>> GetAllAsync()
        {
            return await _context.EmailConfigs.ToListAsync();
        }

        public async Task<EmailConfig?> GetByIdAsync(Guid id)
        {
            return await _context.EmailConfigs.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<EmailConfig?> GetByNameAsync(string name)
        {
            return _context.EmailConfigs.FirstOrDefaultAsync(e => e.Name == name);
        }

        public Task<EmailConfig?> UpdateAsync(Guid id, EmailConfig model)
        {
            var entity = _context.EmailConfigs.FirstOrDefault(e => e.Id == id);
            if (entity == null)
                return Task.FromResult<EmailConfig?>(null);
            
            entity.Name = model.Name;
            entity.Template = model.Template;

            _context.EmailConfigs.Update(entity);
            _context.SaveChanges();

            return Task.FromResult<EmailConfig?>(entity);
        }
    }
}