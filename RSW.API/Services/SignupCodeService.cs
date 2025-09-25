using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class SignupCodeService : ISignupCodeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SignupCodeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SignupCode>> GetAllAsync()
        {
            return await _context.SignupCodes
                .Select(e => _mapper.Map<SignupCode>(e))
                .ToListAsync();
        }

        public async Task<SignupCode?> GetByIdAsync(Guid id)
        {
            var entity = await _context.SignupCodes.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<SignupCode>(entity);
        }

        public async Task<SignupCode> CreateAsync(SignupCode model)
        {
            var entity = new SignupCode
            {
                Id = Guid.NewGuid(),
                GroupId = model.GroupId,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            };

            _context.SignupCodes.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<SignupCode>(entity);
        }

        public async Task<SignupCode?> UpdateAsync(Guid id, SignupCode model)
        {
            var existing = await _context.SignupCodes.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.ExpiryDate = model.ExpiryDate;
            existing.GroupId = model.GroupId;
            existing.Lock = model.Lock;

            await _context.SaveChangesAsync();
            return _mapper.Map<SignupCode>(existing);
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

            return _mapper.Map<SignupCode>(entity);
        }
    }

}