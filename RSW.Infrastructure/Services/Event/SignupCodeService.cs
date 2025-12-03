
using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.Infrastructure.Services
{
    public class SignupCodeService : ISignupCodeService
    {
        private readonly AppDbContext _context;

        public SignupCodeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SignupCode>> GetAllAsync()
        {
            return await _context.SignupCodes
                .ToListAsync();
        }

        public async Task<SignupCode?> GetByIdAsync(Guid id)
        {
            var entity = await _context.SignupCodes.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<SignupCode> CreateAsync(SignupCode model)
        {
            var entity = new SignupCode
            {
                Id = Guid.NewGuid(),
                UnitId = model.UnitId,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            };

            _context.SignupCodes.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<SignupCode?> UpdateAsync(Guid id, SignupCode model)
        {
            var existing = await _context.SignupCodes.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.ExpiryDate = model.ExpiryDate;
            existing.UnitId = model.UnitId;
            existing.Lock = model.Lock;

            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.SignupCodes.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.SignupCodes.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SignupCode?> ValidateAsync(Guid id)
        {
            var entity = await _context.SignupCodes.FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
                return null;

            if (entity.Lock)
                return null;

            if (entity.ExpiryDate < DateTime.UtcNow)
                return null;

            return entity;
        }
    }

}